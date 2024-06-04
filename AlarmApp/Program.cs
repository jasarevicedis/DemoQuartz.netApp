using AlarmApp.Components;
using AlarmApp.Jobs;
using AlarmApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
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
        services.AddQuartz(
            q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
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

scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
scheduler.ListenerManager.AddJobListener(new JobListener());
scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());


var alarm1 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 1", "Testing alarms")
                                       .Build();
var alarm2 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 2", "Testing alarms")
                                       .Build();
var alarm3 = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm 3", "Testing alarms")
                                       .Build();

//await scheduler.AddJob(alarm, true);

var trigger1 = TriggerBuilder.Create()
                                 .WithIdentity("Test trigger 1", "Testing triggers")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(5).RepeatForever())
                                 .Build();

var trigger2 = TriggerBuilder.Create()
                                 .WithIdentity("Test trigger 2", "Testing triggers")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(7).RepeatForever())
                                 .Build();

var trigger3 = TriggerBuilder.Create()
                                 .WithIdentity("Test trigger 3", "Testing triggers")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(9).RepeatForever())
                                 .Build();

//scheduler.Start();

await scheduler.ScheduleJob(alarm1, trigger1);
await scheduler.ScheduleJob(alarm2, trigger2);
await scheduler.ScheduleJob(alarm3, trigger3);

await builder.RunAsync();

