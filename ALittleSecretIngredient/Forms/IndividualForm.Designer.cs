namespace ALittleSecretIngredient.Forms
{
    partial class IndividualForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndividualForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox7 = new GroupBox();
            button1 = new Button();
            checkBox20 = new CheckBox();
            groupBox1 = new GroupBox();
            checkBox1 = new CheckBox();
            groupBox2 = new GroupBox();
            button4 = new Button();
            checkBox4 = new CheckBox();
            button2 = new Button();
            checkBox2 = new CheckBox();
            groupBox3 = new GroupBox();
            button3 = new Button();
            checkBox3 = new CheckBox();
            groupBox4 = new GroupBox();
            button5 = new Button();
            checkBox5 = new CheckBox();
            groupBox5 = new GroupBox();
            button6 = new Button();
            checkBox6 = new CheckBox();
            toolTip1 = new ToolTip(components);
            flowLayoutPanel1.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Controls.Add(groupBox3);
            flowLayoutPanel1.Controls.Add(groupBox4);
            flowLayoutPanel1.Controls.Add(groupBox5);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(836, 433);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // groupBox7
            // 
            groupBox7.AutoSize = true;
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(button1);
            groupBox7.Controls.Add(checkBox20);
            groupBox7.Location = new Point(3, 3);
            groupBox7.MinimumSize = new Size(200, 0);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(200, 111);
            groupBox7.TabIndex = 13;
            groupBox7.TabStop = false;
            groupBox7.Text = "Character Age";
            // 
            // button1
            // 
            button1.Location = new Point(6, 56);
            button1.Name = "button1";
            button1.Size = new Size(188, 29);
            button1.TabIndex = 5;
            button1.Text = "Age";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new Point(6, 26);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new Size(106, 24);
            checkBox20.TabIndex = 0;
            checkBox20.Text = "Randomize";
            toolTip1.SetToolTip(checkBox20, "This pertains to the amount of experience *required* for each bond level.");
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Location = new Point(3, 120);
            groupBox1.MinimumSize = new Size(200, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 76);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Character Birthday";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 26);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(106, 24);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Randomize";
            toolTip1.SetToolTip(checkBox1, "This pertains to the amount of experience *required* for each bond level.");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(checkBox4);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(checkBox2);
            groupBox2.Location = new Point(3, 202);
            groupBox2.MinimumSize = new Size(200, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 176);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Starting Level";
            // 
            // button4
            // 
            button4.Location = new Point(6, 121);
            button4.Name = "button4";
            button4.Size = new Size(188, 29);
            button4.TabIndex = 7;
            button4.Text = "Enemy Level";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(6, 91);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(165, 24);
            checkBox4.TabIndex = 6;
            checkBox4.Text = "Randomize enemies";
            toolTip1.SetToolTip(checkBox4, "This pertains to the amount of experience *required* for each bond level.");
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(6, 56);
            button2.Name = "button2";
            button2.Size = new Size(188, 29);
            button2.TabIndex = 5;
            button2.Text = "Ally Level";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(6, 26);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(144, 24);
            checkBox2.TabIndex = 0;
            checkBox2.Text = "Randomize allies";
            toolTip1.SetToolTip(checkBox2, "This pertains to the amount of experience *required* for each bond level.");
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.AutoSize = true;
            groupBox3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(checkBox3);
            groupBox3.Location = new Point(209, 3);
            groupBox3.MinimumSize = new Size(200, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 111);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Internal Level";
            // 
            // button3
            // 
            button3.Location = new Point(6, 56);
            button3.Name = "button3";
            button3.Size = new Size(188, 29);
            button3.TabIndex = 5;
            button3.Text = "Internal Level";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(6, 26);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(106, 24);
            checkBox3.TabIndex = 0;
            checkBox3.Text = "Randomize";
            toolTip1.SetToolTip(checkBox3, "This pertains to the amount of experience *required* for each bond level.");
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.AutoSize = true;
            groupBox4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox4.Controls.Add(button5);
            groupBox4.Controls.Add(checkBox5);
            groupBox4.Location = new Point(209, 120);
            groupBox4.MinimumSize = new Size(200, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(200, 111);
            groupBox4.TabIndex = 17;
            groupBox4.TabStop = false;
            groupBox4.Text = "Support Category";
            // 
            // button5
            // 
            button5.Location = new Point(6, 56);
            button5.Name = "button5";
            button5.Size = new Size(188, 29);
            button5.TabIndex = 5;
            button5.Text = "Support Categories";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 26);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(106, 24);
            checkBox5.TabIndex = 0;
            checkBox5.Text = "Randomize";
            toolTip1.SetToolTip(checkBox5, "This pertains to the amount of experience *required* for each bond level.");
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.AutoSize = true;
            groupBox5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox5.Controls.Add(button6);
            groupBox5.Controls.Add(checkBox6);
            groupBox5.Location = new Point(209, 237);
            groupBox5.MinimumSize = new Size(200, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(200, 111);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Skill Points";
            // 
            // button6
            // 
            button6.Location = new Point(6, 56);
            button6.Name = "button6";
            button6.Size = new Size(188, 29);
            button6.TabIndex = 5;
            button6.Text = "Starting SP";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(6, 26);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(106, 24);
            checkBox6.TabIndex = 0;
            checkBox6.Text = "Randomize";
            toolTip1.SetToolTip(checkBox6, "This pertains to the amount of experience *required* for each bond level.");
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 60000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            // 
            // IndividualForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 433);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "IndividualForm";
            Text = "Character Settings";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox7;
        public CheckBox checkBox20;
        private ToolTip toolTip1;
        private Button button1;
        private GroupBox groupBox1;
        public CheckBox checkBox1;
        private GroupBox groupBox2;
        private Button button2;
        public CheckBox checkBox2;
        private GroupBox groupBox3;
        private Button button3;
        public CheckBox checkBox3;
        private Button button4;
        public CheckBox checkBox4;
        private GroupBox groupBox4;
        private Button button5;
        public CheckBox checkBox5;
        private GroupBox groupBox5;
        private Button button6;
        public CheckBox checkBox6;
    }
}