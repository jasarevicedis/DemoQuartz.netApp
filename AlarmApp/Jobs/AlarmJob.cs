using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.Logging;
using AlarmApp.Services;

namespace AlarmApp.Jobs
{
    internal class AlarmJob : IJob
    {
        IAlarmService _alarmService;

        public AlarmJob(IAlarmService alarmService) {
            _alarmService = alarmService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _alarmService.SendAlarm("Novi alarm");
            return Task.CompletedTask;
        }
    }
}
