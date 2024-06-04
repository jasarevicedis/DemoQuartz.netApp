using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Components
{
    internal class JobListener : IJobListener
    {
        public string Name => "Alarm job listener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Vetoed alarm: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Executing alarm: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Executed alarm: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }
    }
}
