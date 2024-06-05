namespace AlarmApp.Configuration
{
    public interface IAppSettingsConfiguration
    {
        QuartzSettings QuartzSettings { get; set; }
        string AppName { get; set; }
        string AlarmJobSchedule { get; set; }
        string VariableColor { get; set; }
        string ResetColor { get; set; }
    }

    public class AppSettingsConfiguration : IAppSettingsConfiguration
    {
        public QuartzSettings QuartzSettings { get; set; }
        public string AppName { get; set; }
        public string AlarmJobSchedule { get; set; }
        public string VariableColor { get; set; }
        public string ResetColor { get; set; }
    }
}