using AlarmApp.Jobs;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Services
{
    public interface IAlarmService
    {
        void SendAlarm(string alarmName);
    }
    internal class AlarmService : IAlarmService
    {

        private readonly ILogger<AlarmService> _logger;

        public AlarmService(ILogger<AlarmService> logger)
        {
            _logger = logger;
        }

        public void SendAlarm(string alarmName)
        {
            _logger.LogInformation(
                $"\n ###[New alarm sending]### \n " +$"Alarm name: {alarmName}, Time: {DateTime.Now}\n"
            );
        }
    }
}
