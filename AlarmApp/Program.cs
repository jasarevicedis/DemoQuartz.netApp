using AlarmApp.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((cxt, services) =>
    {
        services.AddQuartz();
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    }).Build();

var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

// define the job and tie it to our HelloJob class
var job = JobBuilder.Create<AlarmJob>()
    .WithIdentity("Alarm", "group1")
    .Build();

// Trigger the job to run now, and then every 40 seconds
var trigger = TriggerBuilder.Create()
    .WithIdentity("alarmTrigger", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

await builder.RunAsync();
