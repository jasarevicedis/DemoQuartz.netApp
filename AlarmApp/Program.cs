using AlarmApp.Components;
using AlarmApp.Jobs;
using AlarmApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.AddTransient<AlarmJob>();
        services.AddSingleton<IAlarmService, AlarmService>();
        
    }).Build();

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = factory.GetScheduler().Result;

scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
scheduler.ListenerManager.AddJobListener(new JobListener());
scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());



IJobDetail alarm = JobBuilder.Create<AlarmJob>()
                                       .WithIdentity("Alarm1", "Testing alarms")
                                       .StoreDurably()
                                       .RequestRecovery()
                                       .Build();

await scheduler.AddJob(alarm, true);

ITrigger trigger = TriggerBuilder.Create()
                                 .ForJob(alarm)
                                 .UsingJobData("triggerparam", "Simple trigger 1 Parameter")
                                 .WithIdentity("testtrigger", "quartzexamples")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(5).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                                 .Build();
ITrigger trigger2 = TriggerBuilder.Create()
                                .ForJob(alarm)
                                .UsingJobData("triggerparam", "Simple trigger 2 Parameter")
                                .WithIdentity("testtrigger2", "quartzexamples")
                                .StartNow()
                                .WithSimpleSchedule(z => z.WithIntervalInSeconds(5).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                                .Build();
ITrigger trigger3 = TriggerBuilder.Create()
                                .ForJob(alarm)
                                .UsingJobData("triggerparam", "Simple trigger 3 Parameter")
                                .WithIdentity("testtrigger3", "quartzexamples")
                                .StartNow()
                                .WithSimpleSchedule(z => z.WithIntervalInSeconds(5).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                                .Build();

await scheduler.ScheduleJob(trigger);
await scheduler.ScheduleJob(trigger2);
await scheduler.ScheduleJob(trigger3);

await builder.RunAsync();

