using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.Logging;

namespace AlarmApp.Jobs
{
    internal class AlarmJob : IJob
    {
        private readonly ILogger<AlarmJob> _logger;

        public AlarmJob(ILogger<AlarmJob> logger) {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"[[ Alarm ]] --> Time: {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
