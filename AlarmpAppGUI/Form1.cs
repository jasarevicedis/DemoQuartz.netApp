
using AlarmApp.DAL.Models;
using AlarmApp.Console.Services;

namespace AlarmApp.GUI
{
    public partial class Form1 : Form
    {
        private int alarmCount = 0;
        private readonly IAlarmManager _alarmManager;
        public Form1(IAlarmManager alarmManager)
        {
            InitializeComponent();
            _alarmManager = alarmManager;
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadAlarms();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async Task LoadAlarms()
        {
            var alarms = await _alarmManager.GetAlarms();

            foreach (var alarm in alarms)
            {
                AddAlarm(alarm);
            }
        }
        private void AddAlarm(Alarm alarm)
        {
            var card = new AlarmCard
            {
                Id = alarm.Id,
                Name = "card" + alarmCount++,
                Dock = DockStyle.Fill,
                AlarmName = alarm.Name,
                //AlarmSnoozeTime = alarm.SnoozeTime,
                //AlarmCronExpression = alarm.CronExpression
            };
            /*
            var card = new AlarmCard();
            card.Name = "card" + alarmCount++;
            card.Dock = DockStyle.Fill;
            */
            // Add an event handler for the delete button
            //card.DeleteButton.Click += (sender, e) => DeleteCard(card);
            var deleteButton = new Button
            {
                Text = "Delete",
                Tag = alarm.Id
            };
            deleteButton.Click += async (sender, e) => await DeleteAlarm((int)deleteButton.Tag);

            card.Controls.Add(deleteButton);

            tableLayoutPanel1.Controls.Add(card);
            tableLayoutPanel1.RowCount = (int)Math.Ceiling(tableLayoutPanel1.Controls.Count / 3.0);

            // Update the rows to ensure new cards are placed in the right row
            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            }
        }
        private async Task DeleteAlarm(int alarmId)
        {
            await _alarmManager.DeleteAlarm(alarmId);

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is AlarmCard card && card.Id == alarmId)
                {
                    tableLayoutPanel1.Controls.Remove(control);
                    break;
                }
            }
        }

        private void addAlarmButton_Click_1(object sender, EventArgs e)
        {
            using (var addAlarmForm = new AddAlarmForm())
            {
                if (addAlarmForm.ShowDialog() == DialogResult.OK)
                {
                    var alarm = addAlarmForm.Alarm;
                    _alarmManager.AddAlarm(alarm);
                    AddAlarm(alarm);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void alarmTab_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
