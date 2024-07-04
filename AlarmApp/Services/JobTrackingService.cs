using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl.Matchers;

namespace AlarmApp.Console.Services
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
        private readonly IConsolePrintingService _consolePrintingService;


        public JobTrackingService(ILogger<IHostedService> logger, IScheduler scheduler, IConsolePrintingService consolePrintingService)
        {
            _logger = logger;
            _scheduler = scheduler;
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            _consolePrintingService = consolePrintingService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _logger.LogInformation("Job tracking service is starting");
                
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

        }
        private async void DoWork(object state)
        {
            var jobDetails = new List<String>();
            var jobGroupNames = await _scheduler.GetJobGroupNames();

            foreach (var groupName in jobGroupNames)
            {
                jobDetails.Add("Group: " + groupName);
                var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));

                foreach (var jobKey in jobKeys)
                {
                    jobDetails.Add("\tJob: " + jobKey);
                    var jobDetail = await _scheduler.GetJobDetail(jobKey);
                    var triggers = await _scheduler.GetTriggersOfJob(jobKey);

                    foreach (ITrigger trigger in triggers)
                    {

                        var triggerState = await _scheduler.GetTriggerState(trigger.Key);

                        jobDetails.Add($"\t\tTrigger: {trigger.Key}, State: {triggerState}");

                        DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                        
                        if (nextFireTime.HasValue)
                        {
                            var nextFireTimeUtc2 = TimeZoneInfo.ConvertTimeFromUtc(nextFireTime.Value.DateTime, _timeZone);
                            TimeSpan difference = nextFireTimeUtc2 - TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
                            double seconds = difference.TotalSeconds;
                            jobDetails.Add($"\t\tNext job execution for {jobKey} at {nextFireTimeUtc2} (Seconds left: {(int)seconds})");
                        }
                    }
                }
            }
            
            _consolePrintingService.PrintWindow("Jobs status", jobDetails);
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
