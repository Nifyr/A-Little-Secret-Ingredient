namespace ALittleSecretIngredient.Forms
{
    partial class BondLevelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BondLevelForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox7 = new GroupBox();
            button18 = new Button();
            checkBox20 = new CheckBox();
            groupBox1 = new GroupBox();
            button1 = new Button();
            checkBox1 = new CheckBox();
            toolTip1 = new ToolTip(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(732, 325);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // groupBox7
            // 
            groupBox7.AutoSize = true;
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(tableLayoutPanel1);
            groupBox7.Location = new Point(3, 3);
            groupBox7.MaximumSize = new Size(180, 0);
            groupBox7.MinimumSize = new Size(180, 0);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(180, 78);
            groupBox7.TabIndex = 13;
            groupBox7.TabStop = false;
            groupBox7.Text = "Exp Requirement";
            // 
            // button18
            // 
            button18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button18.AutoSize = true;
            button18.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button18.Location = new Point(3, 28);
            button18.Name = "button18";
            button18.Size = new Size(168, 25);
            button18.TabIndex = 1;
            button18.Text = "Exp Requirement";
            button18.UseVisualStyleBackColor = true;
            button18.Click += Button18_Click;
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new Point(3, 3);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new Size(85, 19);
            checkBox20.TabIndex = 0;
            checkBox20.Text = "Randomize";
            toolTip1.SetToolTip(checkBox20, "This pertains to the amount of experience *required* for each bond level.");
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Location = new Point(3, 87);
            groupBox1.MaximumSize = new Size(180, 0);
            groupBox1.MinimumSize = new Size(180, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(180, 78);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Bond Fragment Cost";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.AutoSize = true;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(3, 28);
            button1.Name = "button1";
            button1.Size = new Size(168, 25);
            button1.TabIndex = 1;
            button1.Text = "Bond Fragment Cost";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(3, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(85, 19);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Randomize";
            toolTip1.SetToolTip(checkBox1, "This alters the amount of bond fragments *required* to progress through each bond level.");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 60000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(button18, 0, 1);
            tableLayoutPanel1.Controls.Add(checkBox20, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(174, 56);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(button1, 0, 1);
            tableLayoutPanel2.Controls.Add(checkBox1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(174, 56);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // BondLevelForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 325);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "BondLevelForm";
            Text = "Bond Level Requirement Settings";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox7;
        public Button button18;
        public CheckBox checkBox20;
        private GroupBox groupBox1;
        public Button button1;
        public CheckBox checkBox1;
        private ToolTip toolTip1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}