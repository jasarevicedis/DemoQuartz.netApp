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
    internal class SchedulerListener : ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Added new alarm: {jobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Deleted alarm: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Interrupted alarm: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Paused alarm: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Resumed alarm: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Scheduled alarm: {trigger.JobKey.Name} + with trigger: {trigger.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Paused job group: {jobGroup}");
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Resumed job group: {jobGroup}");
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Unscheduled job: {triggerKey.Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Scheduler error: {msg}");
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            Log.Information($"Scheduler in standby MODE");
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            Log.Information($"Scheduler shutdown");
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            Log.Information($"Scheduler shutttingdown");
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            Log.Information($"Scheduler started");
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            Log.Information($"Scheduler starting");
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            Log.Information($"Scheduler data cleared");
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Trigger finalized: {trigger.Key.Name}");
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Trigger paused: {triggerKey.Name}");
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Trigger resumed: {triggerKey.Name}");
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Triggers paused on group : {triggerGroup}");
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            Log.Debug($"Triggers resumed on group : {triggerGroup}");
            return Task.CompletedTask;
        }
    }
}
