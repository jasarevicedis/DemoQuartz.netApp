using AlarmApp.Components;
using AlarmApp.Configuration;
using AlarmApp.Jobs;
using AlarmApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using static Quartz.Logging.OperationName;


var builder = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<QuartzSettings>(context.Configuration.GetSection("QuartzSettings"));
        services.AddQuartz(
            options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var quartzSettings = context.Configuration.GetSection("QuartzSettings").Get<QuartzSettings>();
                options.SchedulerId = quartzSettings.SchedulerId;
                options.SchedulerName = quartzSettings.SchedulerName;
                options.MaxBatchSize = quartzSettings.MaxBatchSize;
                options.InterruptJobsOnShutdown = quartzSettings.InterruptJobsOnShutdown;
                options.InterruptJobsOnShutdownWithWait = quartzSettings.InterruptJobsOnShutdownWithWait;
                

            });
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.AddTransient<AlarmJob>();
        services.AddSingleton<IAlarmService, AlarmService>();
        

    }).Build();



var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

Console.WriteLine($"Scheduler Id: {scheduler.SchedulerInstanceId}, Scheduler name: {scheduler.SchedulerName}");

scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
scheduler.ListenerManager.AddJobListener(new JobListener());
scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());


var alarm1 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 1", "Repeating alarms")
                                       .Build();
var alarm2 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 2", "Repeating alarms")
                                       .Build();
var alarm3 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 3", "Repeating alarms")
                                       .Build();
var alarm4 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 4", "Specific alarms")
                                       .Build();

//await scheduler.AddJob(alarm, true);

var trigger1 = TriggerBuilder.Create()
                                 .WithIdentity("Trigger 1", "Repeating triggers")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(1).RepeatForever())
                                 .Build();

var trigger2 = TriggerBuilder.Create()
                                 .WithIdentity("Trigger 2", "Repeating triggers")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(5).RepeatForever())
                                 .Build();

var trigger3 = TriggerBuilder.Create()
                                 .WithIdentity("Trigger 3", "Repeating triggers")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(20).RepeatForever())
                                 .Build();

var trigger4 = TriggerBuilder.Create()
                                 .WithIdentity("Trigger 4", "Cron triggers")
                                 .WithSchedule(CronScheduleBuilder
                                    .DailyAtHourAndMinute(8,11)
                                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")))
                                 .Build();

//scheduler.Start();

await scheduler.ScheduleJob(alarm1, trigger1);
await scheduler.ScheduleJob(alarm2, trigger2);
await scheduler.ScheduleJob(alarm3, trigger3);
await scheduler.ScheduleJob(alarm4, trigger4);

await builder.RunAsync();

