using AlarmApp.Console.Jobs;
using AlarmApp.DAL.Models;
using Microsoft.Data.Sqlite;
using Quartz;
using Quartz.Core;


namespace AlarmApp.Console.Services
{

    public interface IAlarmManager
    {
        Task AddAlarm(Alarm alarm);
        Task<List<Alarm>> GetAlarms();
        Task DeleteAlarm(int id);
    }
    public class AlarmManager: IAlarmManager
    {
        private static readonly string connectionString = "Data Source=C:\\Users\\EdisJasarevic\\source\\repos\\AlarmApp\\DAL\\Data\\quartz.db;";
        private readonly ISchedulerFactory _schedulerFactory;
        //private static readonly string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quartz.db")}";

        public AlarmManager(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }
        public async Task AddAlarm(
            //this IServiceCollectionQuartzConfigurator quartz, 
            /*
            string jobName,
            string jobGroup, 
            string triggerName, 
            string triggerGroup,
            string dataSyncSchedule*/
            Alarm alarm
        )
        {

            var scheduler = await _schedulerFactory.GetScheduler();
            //string jobName = typeof(T).Name;
            //string _dataSyncIdentity = $"{jobName}_DataSyncTrigger";

            if (string.IsNullOrEmpty(alarm.CronExpression))
            {
                throw new Exception($"Quartz.NET Cron schedule invalid for job {alarm.Name}");
            }

            var jobKey = new JobKey(alarm.Name, "Alarm Jobs");
            /*
            quartz.AddJob<AlarmJob>(opts => opts.WithIdentity(jobKey));

            string triggerName = alarm.Name + "_trigger";
            string triggerGroup = "Alarm triggers";
            

            quartz.AddTrigger(opts => opts
                                    .ForJob(jobKey)
                                    .WithIdentity(triggerName, triggerGroup)
                                    .WithCronSchedule(alarm.CronExpression)).InterruptJobsOnShutdown = true;*/

            var jobDetail = JobBuilder.Create<AlarmJob>()
                .WithIdentity(jobKey)
                .Build();

            var trigger = TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithIdentity(alarm.Name + "_trigger", "Alarm triggers")
                .WithCronSchedule(alarm.CronExpression)
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            AddAlarmToDatabase(alarm);
        }

        private static void AddAlarmToDatabase(Alarm alarm)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO ALARMS (ALARM_NAME, SNOOZE_TIME, SCHED_NAME, TRIGGER_NAME, TRIGGER_GROUP)
                    VALUES ($alarmName, $snoozeTime, $schedName, $triggerName, $triggerGroup)";
                command.Parameters.AddWithValue("$alarmName", alarm.Name);
                command.Parameters.AddWithValue("$snoozeTime", alarm.SnoozeTime);
                command.Parameters.AddWithValue("$schedName", "QuartzScheduler");
                command.Parameters.AddWithValue("$triggerName", alarm.Name+"_trigger");
                command.Parameters.AddWithValue("$triggerGroup", "Alarm triggers");

                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Alarm>> GetAlarms()
        {
            var alarms = new List<Alarm>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT ALARM_NAME, SNOOZE_TIME FROM ALARMS";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var alarm = new Alarm(reader.GetString(0), reader.GetInt32(1), "2222");
                        
                        alarms.Add(alarm);
                    }
                }
            }

            return alarms;
        }

        public async Task DeleteAlarm(int alarmId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = await GetJobKeyById(alarmId);
            if (jobKey != null)
            {
                await scheduler.DeleteJob(jobKey);
            }
            DeleteAlarmFromDatabase(alarmId);
        }

        private static void DeleteAlarmFromDatabase(int alarmId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM ALARMS WHERE ID = $alarmId";
                command.Parameters.AddWithValue("$alarmId", alarmId);

                command.ExecuteNonQuery();
            }
        }
        private async Task<JobKey> GetJobKeyById(int alarmId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT ALARM_NAME FROM ALARMS WHERE ID = $alarmId";
                command.Parameters.AddWithValue("$alarmId", alarmId);

                var alarmName = (string)command.ExecuteScalar();
                if (!string.IsNullOrEmpty(alarmName))
                {
                    return new JobKey(alarmName, "Alarm Jobs");
                }
            }
            return null;
        }



    }
}