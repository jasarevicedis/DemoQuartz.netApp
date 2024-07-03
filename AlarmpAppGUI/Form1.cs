namespace AlarmpAppGUI
{
    public partial class Form1 : Form
    {
        private int alarmCount = 0;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150)); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
        private void AddAlarm()
        {
            var card = new AlarmCard();
            card.Name = "card" + alarmCount++;
            card.Dock = DockStyle.Fill;

            // Add an event handler for the delete button
            //card.DeleteButton.Click += (sender, e) => DeleteCard(card);

            tableLayoutPanel1.Controls.Add(card);
            tableLayoutPanel1.RowCount = (int)Math.Ceiling(tableLayoutPanel1.Controls.Count / 3.0);

            // Update the rows to ensure new cards are placed in the right row
            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            }
        }

        private void addAlarmButton_Click_1(object sender, EventArgs e)
        {
            using (var addAlarmForm = new AddAlarmForm())
            {
                if (addAlarmForm.ShowDialog() == DialogResult.OK)
                {
                    AddAlarm();
                }
            }
        }
    }
}
