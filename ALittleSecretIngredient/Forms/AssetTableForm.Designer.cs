namespace ALittleSecretIngredient.Forms
{
    partial class AssetTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetTableForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox7 = new GroupBox();
            checkBox20 = new CheckBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox6 = new CheckBox();
            flowLayoutPanel1.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(836, 433);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // groupBox7
            // 
            groupBox7.AutoSize = true;
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(checkBox6);
            groupBox7.Controls.Add(checkBox5);
            groupBox7.Controls.Add(checkBox4);
            groupBox7.Controls.Add(checkBox3);
            groupBox7.Controls.Add(checkBox2);
            groupBox7.Controls.Add(checkBox1);
            groupBox7.Controls.Add(checkBox20);
            groupBox7.Location = new Point(3, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(200, 256);
            groupBox7.TabIndex = 13;
            groupBox7.TabStop = false;
            groupBox7.Text = "Model Swap";
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new Point(6, 26);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new Size(137, 24);
            checkBox20.TabIndex = 0;
            checkBox20.Text = "Shuffle Playable";
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(21, 56);
            checkBox1.Margin = new Padding(18, 3, 17, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(159, 24);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Include Protagonist";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(6, 86);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(165, 24);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Shuffle named NPCs";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(6, 116);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(142, 24);
            checkBox3.TabIndex = 3;
            checkBox3.Text = "Shuffle Emblems";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(21, 146);
            checkBox4.Margin = new Padding(18, 3, 3, 3);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(150, 24);
            checkBox4.TabIndex = 4;
            checkBox4.Text = "Include Corrupted";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 176);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(106, 24);
            checkBox5.TabIndex = 5;
            checkBox5.Text = "Mix Groups";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(6, 206);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(152, 24);
            checkBox6.TabIndex = 6;
            checkBox6.Text = "Restrict By Gender";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // AssetTableForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 433);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AssetTableForm";
            Text = "Asset Table Settings";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox7;
        public CheckBox checkBox5;
        public CheckBox checkBox4;
        public CheckBox checkBox3;
        public CheckBox checkBox2;
        public CheckBox checkBox1;
        public CheckBox checkBox20;
        public CheckBox checkBox6;
    }
}