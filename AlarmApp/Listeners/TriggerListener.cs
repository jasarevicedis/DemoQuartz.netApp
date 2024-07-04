using AlarmApp.Util.Configuration;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Console.Listeners
{
    public class TriggerListener : ITriggerListener
    {
        public string Name => "Alarm trigger listener";
        private readonly IAppSettingsConfiguration _appSettings;

        public TriggerListener(IAppSettingsConfiguration appSettings)
        {
            _appSettings = appSettings;
        }

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            string triggerKeyName = _appSettings.VariableColor + trigger.Key.Name + _appSettings.ResetColor;
            Log.Information("Trigger with name: {triggerKeyName} completed \n", triggerKeyName);
            
            return Task.CompletedTask;
        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            string triggerKeyName = _appSettings.VariableColor + trigger.Key.Name + _appSettings.ResetColor;
            Log.Information("Trigger with name: {triggerKeyName} fired", triggerKeyName);
            return Task.CompletedTask;
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            string triggerKeyName = _appSettings.VariableColor + trigger.Key.Name + _appSettings.ResetColor;
            Log.Information("Trigger with name: {triggerKeyName} misfired", triggerKeyName);
            return Task.CompletedTask;
        }

        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //Log.Information($"Job veto!!!");
            return false;
        }
    }
}
