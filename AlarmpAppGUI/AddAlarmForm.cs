using AlarmApp.Models;
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
    public partial class AddAlarmForm : Form
    {
        public Alarm Alarm { get; private set; }
        public AddAlarmForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddAlarmForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string alarmName = txtAlarmName.Text;
            //string description = txtDescription.Text;
            //int snoozeTime = int.Parse(txtSnoozeTime.Text);
            //string cronExpression = txtCronExpression.Text;

            Alarm = new Alarm(alarmName,0,"");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
