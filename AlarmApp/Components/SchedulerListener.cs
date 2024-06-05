using AlarmApp.Configuration;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Components
{
    public class SchedulerListener : ISchedulerListener
    {
        public string Name => "Alarm scheduler listener";
        private readonly IAppSettingsConfiguration _appSettings;

        public SchedulerListener(IAppSettingsConfiguration appSettings)
        {
            _appSettings = appSettings;
        }

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobDetail.Key.Name + _appSettings.ResetColor;
            Log.Information("Added new alarm: {alarmName}", alarmName);
            return Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobKey.Name + _appSettings.ResetColor;
            Log.Information("Deleted alarm: {alarmName}", alarmName);
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobKey.Name + _appSettings.ResetColor;
            Log.Information("Interrupted alarm: {alarmName}",alarmName);
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobKey.Name + _appSettings.ResetColor;
            Log.Information($"Paused alarm: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobKey.Name + _appSettings.ResetColor;
            Log.Information($"Resumed alarm: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + trigger.JobKey.Name + _appSettings.ResetColor;
            Log.Information($"Scheduled alarm: {trigger.JobKey.Name} + with trigger: {trigger.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobGroup + _appSettings.ResetColor;
            Log.Information($"Paused job group: {jobGroup}");
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + jobGroup + _appSettings.ResetColor;
            Log.Information($"Resumed job group: {jobGroup}");
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + triggerKey.Name + _appSettings.ResetColor;
            Log.Information($"Unscheduled job: {triggerKey.Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            string alarmName = _appSettings.VariableColor + msg + _appSettings.ResetColor;
            Log.Information($"Scheduler error: {msg}");
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            string notification = _appSettings.VariableColor + "Scheduler in standby MODE" + _appSettings.ResetColor;
            Log.Information(notification);
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            string notification = _appSettings.VariableColor + "Scheduler shutdown" + _appSettings.ResetColor;
            Log.Information(notification);
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            string notification = _appSettings.VariableColor + "Scheduler shutttingdown" + _appSettings.ResetColor;
            Log.Information(notification);
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            string notification = _appSettings.VariableColor + "Scheduler started" + _appSettings.ResetColor;
            Log.Information(notification);
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            string notification = _appSettings.VariableColor + "Scheduler starting" + _appSettings.ResetColor;
            Log.Information(notification);
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            string notification = _appSettings.VariableColor + "Scheduler data cleared" + _appSettings.ResetColor;
            Log.Information(notification);
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            string triggerName = _appSettings.VariableColor + trigger.Key.Name + _appSettings.ResetColor;
            Log.Information("Trigger finalized: {triggerName}", triggerName);
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            string triggerName = _appSettings.VariableColor + triggerKey.Name + _appSettings.ResetColor;
            Log.Information("Trigger paused: {triggerName}", triggerName);
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            string triggerName = _appSettings.VariableColor + triggerKey.Name + _appSettings.ResetColor;
            Log.Information("Trigger resumed: {triggerName}", triggerName);
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            triggerGroup = _appSettings.VariableColor + triggerGroup + _appSettings.ResetColor;
            Log.Information("Triggers paused on group : {triggerGroup}", triggerGroup);
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            triggerGroup = _appSettings.VariableColor + triggerGroup + _appSettings.ResetColor;
            Log.Information("Triggers resumed on group : {triggerGroup}", triggerGroup);
            return Task.CompletedTask;
        }
    }
}
