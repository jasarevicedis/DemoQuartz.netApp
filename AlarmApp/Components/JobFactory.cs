using AlarmApp.Jobs;
using Quartz;


namespace AlarmApp.Components
{
    public static class JobFactory
    {
        public static void AddJobAndTrigger<T>(
            this IServiceCollectionQuartzConfigurator quartz, 
            string jobName,
            string jobGroup, 
            string triggerName, 
            string triggerGroup,
            string dataSyncSchedule
        ) where T : IJob
        {
            //string jobName = typeof(T).Name;
            //string _dataSyncIdentity = $"{jobName}_DataSyncTrigger";

            if (string.IsNullOrEmpty(dataSyncSchedule))
            {
                throw new Exception($"Quartz.NET Cron schedule invalid for job {jobName}");
            }

            var jobKey = new JobKey(jobName, jobGroup);

            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                                    .ForJob(jobKey)
                                    .WithIdentity(triggerName, triggerGroup)
                                    .WithCronSchedule(dataSyncSchedule)).InterruptJobsOnShutdown = true;
        }
    }
}