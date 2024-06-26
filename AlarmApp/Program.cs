﻿using AlarmApp.Components;
using AlarmApp.Configuration;
using AlarmApp.Jobs;
using AlarmApp.Services;
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

namespace AlarmApp
{
    public class Program
    {
        private static IConfiguration _configuration;
        private static readonly IAppSettingsConfiguration _appSettings = new AppSettingsConfiguration();

        public static async Task Main(string[] args)
        {
            string currentExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            

            await Console.Out.WriteLineAsync($"Current path: {currentExecutionPath}");

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

            Log.Information($"Scheduler Id: {scheduler.SchedulerInstanceId}, Scheduler name: {scheduler.SchedulerName}");
            
            scheduler.ListenerManager.AddTriggerListener(new TriggerListener(_appSettings));
            scheduler.ListenerManager.AddJobListener(new JobListener(_appSettings));
            scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener(_appSettings));

            
            /*
            foreach (var jobDetail in from jobGroupName in scheduler.JobGroupNames
                                      from jobName in scheduler.GetJobNames(jobGroupName)
                                      select scheduler.GetJobDetail(jobName, jobGroupName))
            {
                //Get props about job from jobDetail
            }

            foreach (var triggerDetail in from triggerGroupName in scheduler.TriggerGroupNames
                                          from triggerName in scheduler.GetTriggerNames(triggerGroupName)
                                          select scheduler.GetTrigger(triggerName, triggerGroupName))
            {
                //Get props about trigger from triggerDetail
            }
            */
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();
                    //if (context.HostingEnvironment.IsDevelopment())
                    //{
                        config.AddUserSecrets<Program>();
                    //}
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Debugging code to print out configuration values
                    //foreach (var kvp in hostContext.Configuration.AsEnumerable())
                    //{
                        //Log.Error($"{kvp.Key} = {kvp.Value}");
                    //}
                    //string connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
                    string connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
                    
                    services.AddSingleton(typeof(IAppSettingsConfiguration), _appSettings);

                    services.AddQuartz(
                    options =>
                    {
                        options.UseMicrosoftDependencyInjectionJobFactory();

                        options.AddJobAndTrigger<AlarmJob>("SecondBasedAlarm88", "Cron alarms88", "SecondBasedTrigger88", "Cron triggers", "0/4 * * ? * * *");
                        options.AddJobAndTrigger<AlarmJob>("MinuteBasedAlarm", "Cron alarms", "MinuteBasedTrigger", "Cron triggers", "0 0/1 * 1/1 * ? *");
                        options.AddJobAndTrigger<AlarmJob>("Alarm 3", "Cron alarms", "Trigger 3", "Cron triggers", _appSettings.AlarmJobSchedule);
                        options.UsePersistentStore(s =>
                        {
                            s.UseProperties = true;
                            //_configuration.GetConnectionString("BlazingQuartzDb")
                            s.UseSQLite(connectionString);
                            s.UseJsonSerializer();
                        });
                    }); 



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

                    //services.AddSingleton<IHostedService, MyHostedService>();
                    services.AddHostedService<JobTrackingService>();

                    services.AddTransient<AlarmJob>();
                    services.AddTransient<IConsolePrintingService, ConsolePrintingService>();
                    services.AddSingleton<IAlarmService, AlarmService>();
                    

                })
            .UseSerilog();
    }
}

/*
var builder = Host.CreateDefaultBuilder()
.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
})





await scheduler.ScheduleJob(alarm1, trigger1);
await scheduler.ScheduleJob(alarm2, trigger2);
await scheduler.ScheduleJob(alarm3, trigger3);
await scheduler.ScheduleJob(alarm4, trigger4);

await builder.RunAsync();

*/