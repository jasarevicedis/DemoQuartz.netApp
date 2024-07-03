using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmpAppGUI
{
    public partial class AlarmCard : UserControl
    {
        public int Id { get; set; }
        public string AlarmName
        {
            get => lblAlarmName.Text;
            set => lblAlarmName.Text = value;
        }

        /*
        public int AlarmSnoozeTime
        {
            get => int.Parse(lblSnoozeTime.Text);
            set => lblSnoozeTime.Text = value.ToString();
        }

        public string AlarmCronExpression
        {
            get => lblCronExpression.Text;
            set => lblCronExpression.Text = value;
        }*/

        public AlarmCard()
        {
            InitializeComponent();
            InitializeCustomComponents();
            this.MaximumSize = new Size(320, 180);

            this.Size = new Size(320, 180);
        }
        private void InitializeCustomComponents()
        {
            this.MouseEnter += new EventHandler(AlarmCard_MouseEnter);
            this.MouseLeave += new EventHandler(AlarmCard_MouseLeave);          
        }
        private void  AlarmCard_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGray; // Hover color
        }

        private void AlarmCard_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray; // Default color
        }

        private void AlarmCard_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
