namespace ALittleSecretIngredient.Forms
{
    partial class ArrangementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArrangementForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            checkBox1 = new CheckBox();
            button2 = new Button();
            checkBox2 = new CheckBox();
            groupBox2 = new GroupBox();
            button1 = new Button();
            checkBox4 = new CheckBox();
            toolTip1 = new ToolTip(components);
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(836, 433);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(203, 141);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Deployment Slots";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(9, 91);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(177, 24);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Max deployment slots";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(9, 56);
            button2.Name = "button2";
            button2.Size = new Size(188, 29);
            button2.TabIndex = 3;
            button2.Text = "Deployment Slots";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(9, 26);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(147, 24);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Randomize count";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(checkBox4);
            groupBox2.Location = new Point(3, 150);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(203, 111);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Enemy Count";
            // 
            // button1
            // 
            button1.Location = new Point(9, 56);
            button1.Name = "button1";
            button1.Size = new Size(188, 29);
            button1.TabIndex = 3;
            button1.Text = "Enemy Count";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(9, 26);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(147, 24);
            checkBox4.TabIndex = 2;
            checkBox4.Text = "Randomize count";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 60000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            // 
            // ArrangementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 433);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ArrangementForm";
            Text = "Map Unit Settings";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
        public Button button2;
        public CheckBox checkBox2;
        private ToolTip toolTip1;
        public CheckBox checkBox1;
        private GroupBox groupBox2;
        public Button button1;
        public CheckBox checkBox4;
    }
}