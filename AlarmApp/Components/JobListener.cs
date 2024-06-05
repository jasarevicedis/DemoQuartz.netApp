using AlarmApp.Configuration;
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
        private readonly IAppSettingsConfiguration _appSettings;

        public JobListener(IAppSettingsConfiguration appSettings)
        {
            _appSettings = appSettings;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            string jobName = _appSettings.VariableColor + context.JobDetail.Key.Name + _appSettings.ResetColor;
            Log.Information("Vetoed alarm: {jobName}", jobName);
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            string jobName = _appSettings.VariableColor + context.JobDetail.Key.Name + _appSettings.ResetColor;
            Log.Information("Executing alarm: {jobName}", jobName);
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            string jobName = _appSettings.VariableColor + context.JobDetail.Key.Name + _appSettings.ResetColor;
            Log.Information("Executed alarm: {jobName}", jobName);
            return Task.CompletedTask;
        }
    }
}
