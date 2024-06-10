using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Services
{
    public interface IMyHostedService
    {
        //Empty!
    }

    public class JobTrackingService : IHostedService, IMyHostedService, IDisposable
    {
        private readonly ILogger<IHostedService> _logger;
        private Timer _timer;
        private readonly IScheduler _scheduler;
        private readonly TimeZoneInfo _timeZone;


        public JobTrackingService(ILogger<IHostedService> logger, IScheduler scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); 
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _logger.LogInformation("Job tracking service is starting");
                
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

        }
        private async void DoWork(object state)
        {
            //_logger.LogInformation("Job tracking service is working. Time: {time}", DateTimeOffset.Now);
            Console.WriteLine("--------------------------[ Current schedule ]------------------------------");
            var jobGroupNames = await _scheduler.GetJobGroupNames();

            foreach (var groupName in jobGroupNames)
            {
                Console.WriteLine("Group: " + groupName);
                var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));

                foreach (var jobKey in jobKeys)
                {
                    Console.WriteLine("\tJob: " + jobKey);
                    var jobDetail = await _scheduler.GetJobDetail(jobKey);
                    var triggers = await _scheduler.GetTriggersOfJob(jobKey);

                    foreach (ITrigger trigger in triggers)
                    {

                        var triggerState = await _scheduler.GetTriggerState(trigger.Key);

                        Console.WriteLine($"\t\tTrigger: {trigger.Key}, State: {triggerState}");

                        DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                        
                        if (nextFireTime.HasValue)
                        {
                            var nextFireTimeUtc2 = TimeZoneInfo.ConvertTimeFromUtc(nextFireTime.Value.DateTime, _timeZone);
                            TimeSpan difference = nextFireTimeUtc2 - TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
                            double seconds = difference.TotalSeconds;
                            Console.WriteLine($"\t\tNext job execution for {jobKey} at {nextFireTimeUtc2} (Seconds left: {(int)seconds})");
                        }
                    }
                }
            }
            Console.WriteLine("[__________________________________________________________________________]");

        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Job tracking service is stopping");
            _timer?.Change(Timeout.Infinite, 0);

        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
