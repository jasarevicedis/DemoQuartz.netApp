using AlarmApp.Util.Configuration;
using AlarmApp.Console.Jobs;
using AlarmApp.Console.Services;
using AlarmApp.Console.Listeners;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Serilog;
using System;
using System.Text.Json;
using System.Collections.Specialized;
using System.Reflection;
using static Quartz.Logging.OperationName;
using AlarmApp.DAL.Models;
using Microsoft.Extensions.Options;

namespace AlarmApp.Console
{
    public class Program
    {
        private static IConfiguration _configuration;
        private static readonly IAppSettingsConfiguration _appSettings = new AppSettingsConfiguration();

        public static async Task Main(string[] args)
        {
            string currentExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            

            await System.Console.Out.WriteLineAsync($"Current path: {currentExecutionPath}");

            var configFileName = "AppSettings.json";

            var config = new ConfigurationBuilder()
            .SetBasePath(currentExecutionPath)
            .AddJsonFile(configFileName, optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>()
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            
            _configuration = CustomConfigurationBuilder.Build(currentExecutionPath, "AppSettings", "Debug");

            _configuration.Bind("Configuration", _appSettings);

            Log.Information("Application has been intitialized at {time}", DateTime.Now);

            var builder = CreateHostBuilder(args);
            var host = builder.Build();
            
            var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
            var scheduler = await schedulerFactory.GetScheduler();

            //here is when jobs and triggers are stored to database
            Log.Information($"Scheduler Id: {scheduler.SchedulerInstanceId}, Scheduler name: {scheduler.SchedulerName}");

            Alarm alarm1 = new Alarm("TestAlarm401", 200, "0/2 * * ? * * *");
            Alarm alarm2 = new Alarm("TestAlarm501", 500, "0/2 * * ? * * *");
            Alarm alarm3 = new Alarm("TestAlarm701", 500, "0/2 * * ? * * *");
            var alarmManager = host.Services.GetRequiredService<IAlarmManager>();

            await alarmManager.AddAlarm(alarm1);
            /*
            var alarms = await alarmManager.GetAlarms();
            foreach (var alarm in alarms)
            {
                System.Console.WriteLine($"Alarm: {alarm.Name}, Snooze: {alarm.SnoozeTime}, Cron: {alarm.CronExpression}");
            }*/

            

            scheduler.ListenerManager.AddTriggerListener(new TriggerListener(_appSettings));
            scheduler.ListenerManager.AddJobListener(new JobListener(_appSettings));
            scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener(_appSettings));

            
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();
                    
                        config.AddUserSecrets<Program>();
                    
                })
                .ConfigureServices((hostContext, services) =>
                {
                    
                    string connectionString = "Data Source=C:\\Users\\EdisJasarevic\\Source\\Repos\\AlarmApp\\DAL\\Data\\quartz.db";

                    services.AddSingleton(typeof(IAppSettingsConfiguration), _appSettings);

                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = false);



                    NameValueCollection props = new NameValueCollection
                    {
                        { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
                        { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz" },
                        { "quartz.serializer.type", "json" },
                        { "quartz.jobStore.useProperties", "true" },
                        { "quartz.jobStore.dataSource", "myDS" },
                        { "quartz.dataSource.myDS.connectionStringName", connectionString},
                        { "quartz.dataSource.myDS.provider", "SQLite-Microsoft" },
                        { "quartz.jobStore.performSchemaValidation", "false" }
                    };

                    services.AddSingleton<IScheduler>(provider =>
                    {
                        StdSchedulerFactory schedulerFactory = new StdSchedulerFactory(props);
                        return schedulerFactory.GetScheduler().Result;
                    });

                    services.AddQuartz(
                    options =>
                    {
                        options.UseMicrosoftDependencyInjectionJobFactory();
                        
                        
                        options.UsePersistentStore(s =>
                        {
                            s.UseProperties = true;
                            s.UseSQLite(connectionString);
                            s.UseJsonSerializer();
                        });
                    }); 


                    services.AddHostedService<JobTrackingService>();

                    //services.AddTransient<AlarmJob>();
                    services.AddTransient<IConsolePrintingService, ConsolePrintingService>();
                    services.AddSingleton<IAlarmService, AlarmService>();
                    services.AddSingleton<IAlarmManager, AlarmManager>();


                })
            .UseSerilog();
    }
}
