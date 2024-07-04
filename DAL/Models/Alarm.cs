using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.DAL.Models
{
    [Serializable]
    public class Alarm
    {
        public Alarm(string name, int snoozeTime, string cronExpression)
        {
            Name = name;
            SnoozeTime = snoozeTime;
            CronExpression = cronExpression;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int SnoozeTime { get; set; }
        public string CronExpression { get; set; }
    }
}
