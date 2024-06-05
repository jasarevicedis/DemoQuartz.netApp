using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp
{
    public class QuartzSettings
    {
        public string SchedulerId { get; set; } = String.Empty;
        public string SchedulerName {  get; set; } = String.Empty;
        public int MaxBatchSize { get; set; } = 10;
        public bool InterruptJobsOnShutdown { get; set; } = true;
        public bool InterruptJobsOnShutdownWithWait { get; set; } = true;
    }
}
