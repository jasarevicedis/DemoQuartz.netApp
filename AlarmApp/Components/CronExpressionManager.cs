using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Components
{
    public class CronExpressionManager
    {
        private List<DayOfWeek> _daysOfWeek;
        private int _hour;
        private int _minute;

        public CronExpressionManager()
        {
            _daysOfWeek = new List<DayOfWeek>();
        }

        public CronExpressionManager SetTime(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentException("Hour must be between 0 and 23.");

            if (minute < 0 || minute > 59)
                throw new ArgumentException("Minute must be between 0 and 59.");

            _hour = hour;
            _minute = minute;
            return this;
        }

        public CronExpressionManager SetDaysOfWeek(params DayOfWeek[] daysOfWeek)
        {
            _daysOfWeek = daysOfWeek.ToList();
            return this;
        }

        public string Build()
        {
            var daysOfWeekPart = string.Join(",", _daysOfWeek.Select(d => ((int)d + 1) % 7));
            return $"{_minute} {_hour} ? * {daysOfWeekPart}";
        }
    }
}
