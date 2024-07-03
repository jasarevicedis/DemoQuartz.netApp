namespace AlarmpAppGUI
{
    partial class Form1
    {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            alarmTab = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            addAlarmButton = new Button();
            panel1.SuspendLayout();
            alarmTab.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 10, 3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 758);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // button3
            // 
            button3.BackColor = Color.PowderBlue;
            button3.Location = new Point(12, 130);
            button3.Name = "button3";
            button3.Size = new Size(224, 53);
            button3.TabIndex = 2;
            button3.Text = "Settings";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.PowderBlue;
            button2.Location = new Point(12, 71);
            button2.Name = "button2";
            button2.Size = new Size(224, 53);
            button2.TabIndex = 1;
            button2.Text = "Timers";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.PowderBlue;
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(224, 53);
            button1.TabIndex = 0;
            button1.Text = "Alarms";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // alarmTab
            // 
            alarmTab.BackColor = SystemColors.ControlLight;
            alarmTab.Controls.Add(tableLayoutPanel1);
            alarmTab.Controls.Add(addAlarmButton);
            alarmTab.Location = new Point(256, 0);
            alarmTab.Name = "alarmTab";
            alarmTab.Size = new Size(1039, 746);
            alarmTab.TabIndex = 1;
            alarmTab.Paint += alarmTab_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Bottom;
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.0991F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.9009F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 349F));
            tableLayoutPanel1.Location = new Point(16, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1011, 652);
            tableLayoutPanel1.TabIndex = 1;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // addAlarmButton
            // 
            addAlarmButton.BackColor = SystemColors.GradientActiveCaption;
            addAlarmButton.Location = new Point(840, 686);
            addAlarmButton.Name = "addAlarmButton";
            addAlarmButton.Size = new Size(187, 48);
            addAlarmButton.TabIndex = 0;
            addAlarmButton.Text = "Add alarm";
            addAlarmButton.UseVisualStyleBackColor = false;
            addAlarmButton.Click += addAlarmButton_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1307, 758);
            Controls.Add(alarmTab);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Alarm app";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            alarmTab.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Button button3;
        private Button button2;
        private Panel alarmTab;
        private Button addAlarmButton;
        private TableLayoutPanel tableLayoutPanel1;
    }

    


}
