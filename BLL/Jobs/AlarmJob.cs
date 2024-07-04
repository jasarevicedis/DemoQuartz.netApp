using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.Logging;
using AlarmApp.Console.Services;
using Serilog;

namespace AlarmApp.BLL.Jobs
{
    internal class AlarmJob : IJob
    {
        private readonly IAlarmService _alarmService;
        private readonly ILogger<AlarmJob> _logger;

        public AlarmJob(ILogger<AlarmJob> logger, IAlarmService alarmService)
        {
            _logger = logger;
            _alarmService = alarmService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _alarmService.SendAlarm(context.JobDetail.Key.Name);
            return Task.CompletedTask;
        }
    }
}
