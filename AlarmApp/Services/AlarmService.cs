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
        public void SendAlarm(string alarmName)
        {
            Log.Debug($"###[New alarm sending]###");
            Log.Debug($"Alarm name: {alarmName}, Time: {DateTime.Now}");
        }
    }
}
