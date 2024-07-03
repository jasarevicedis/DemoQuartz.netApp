namespace AlarmpAppGUI
{
    partial class AlarmCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //private Label lblAlarmName;
        //private Label lblSnoozeTime;
        //private Label lblCronExpression;
        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            lblAlarmName = new Label();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(13, 18);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0, 27);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(176, 81);
            label1.TabIndex = 1;
            label1.Text = "12:00";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(153, 42);
            label2.Name = "label2";
            label2.Size = new Size(43, 28);
            label2.TabIndex = 2;
            label2.Text = "AM";
            label2.Click += label2_Click;
            // 
            // lblAlarmName
            // 
            lblAlarmName.AutoSize = true;
            lblAlarmName.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAlarmName.ForeColor = SystemColors.ButtonFace;
            lblAlarmName.Location = new Point(13, 101);
            lblAlarmName.Name = "lblAlarmName";
            lblAlarmName.Size = new Size(85, 31);
            lblAlarmName.TabIndex = 3;
            lblAlarmName.Text = "Alarm1";
            //lblAlarmName.Click += lblAlarmName_Click;
            // 
            // checkBox1
            // 
            checkBox1.Location = new Point(285, 18);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(25, 25);
            checkBox1.TabIndex = 4;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // AlarmCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            Controls.Add(checkBox1);
            Controls.Add(lblAlarmName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "AlarmCard";
            Size = new Size(320, 180);
            Load += AlarmCard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label lblAlarmName;
        private CheckBox checkBox1;
    }
}
