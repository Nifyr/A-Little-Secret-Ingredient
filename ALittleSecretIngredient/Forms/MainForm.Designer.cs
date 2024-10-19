namespace ALittleSecretIngredient.Forms
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            exportLayeredFSCheckBox = new CheckBox();
            randomizeAndExportButton = new Button();
            exportCobaltCheckBox = new CheckBox();
            rememberSettingsCheckBox = new CheckBox();
            saveChangelogCheckBox = new CheckBox();
            groupBox2 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button4 = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button6 = new Button();
            button5 = new Button();
            button7 = new Button();
            toolTip1 = new ToolTip(components);
            button8 = new Button();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(458, 225);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 4);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.MaximumSize = new Size(206, 0);
            groupBox1.MinimumSize = new Size(206, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(206, 217);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Main";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(exportLayeredFSCheckBox, 0, 4);
            tableLayoutPanel2.Controls.Add(randomizeAndExportButton, 0, 0);
            tableLayoutPanel2.Controls.Add(exportCobaltCheckBox, 0, 3);
            tableLayoutPanel2.Controls.Add(rememberSettingsCheckBox, 0, 1);
            tableLayoutPanel2.Controls.Add(saveChangelogCheckBox, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 24);
            tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(200, 189);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // exportLayeredFSCheckBox
            // 
            exportLayeredFSCheckBox.AutoSize = true;
            exportLayeredFSCheckBox.Checked = true;
            exportLayeredFSCheckBox.CheckState = CheckState.Checked;
            exportLayeredFSCheckBox.Location = new Point(3, 138);
            exportLayeredFSCheckBox.Margin = new Padding(3, 4, 3, 4);
            exportLayeredFSCheckBox.Name = "exportLayeredFSCheckBox";
            exportLayeredFSCheckBox.Size = new Size(168, 24);
            exportLayeredFSCheckBox.TabIndex = 4;
            exportLayeredFSCheckBox.Text = "Export for LayeredFS";
            toolTip1.SetToolTip(exportLayeredFSCheckBox, "This ensures the generation of a *mod* formatted to be loaded with LayeredFS during the exporting process.");
            exportLayeredFSCheckBox.UseVisualStyleBackColor = true;
            // 
            // randomizeAndExportButton
            // 
            randomizeAndExportButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            randomizeAndExportButton.AutoSize = true;
            randomizeAndExportButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            randomizeAndExportButton.Location = new Point(3, 4);
            randomizeAndExportButton.Margin = new Padding(3, 4, 3, 4);
            randomizeAndExportButton.Name = "randomizeAndExportButton";
            randomizeAndExportButton.Size = new Size(194, 30);
            randomizeAndExportButton.TabIndex = 0;
            randomizeAndExportButton.Text = "Randomize and Export";
            toolTip1.SetToolTip(randomizeAndExportButton, "Uses the current settings to randomize the game and subsequently *exports* a mod containing the modifications.");
            randomizeAndExportButton.UseVisualStyleBackColor = true;
            randomizeAndExportButton.Click += RandomizeAndExportButton_Click;
            // 
            // exportCobaltCheckBox
            // 
            exportCobaltCheckBox.AutoSize = true;
            exportCobaltCheckBox.Checked = true;
            exportCobaltCheckBox.CheckState = CheckState.Checked;
            exportCobaltCheckBox.Location = new Point(3, 106);
            exportCobaltCheckBox.Margin = new Padding(3, 4, 3, 4);
            exportCobaltCheckBox.Name = "exportCobaltCheckBox";
            exportCobaltCheckBox.Size = new Size(145, 24);
            exportCobaltCheckBox.TabIndex = 3;
            exportCobaltCheckBox.Text = "Export for Cobalt";
            toolTip1.SetToolTip(exportCobaltCheckBox, "This guarantees the creation of a mod formatted to be loaded by Cobalt during the *exporting* process.");
            exportCobaltCheckBox.UseVisualStyleBackColor = true;
            // 
            // rememberSettingsCheckBox
            // 
            rememberSettingsCheckBox.AutoSize = true;
            rememberSettingsCheckBox.Location = new Point(3, 42);
            rememberSettingsCheckBox.Margin = new Padding(3, 4, 3, 4);
            rememberSettingsCheckBox.Name = "rememberSettingsCheckBox";
            rememberSettingsCheckBox.Size = new Size(159, 24);
            rememberSettingsCheckBox.TabIndex = 1;
            rememberSettingsCheckBox.Text = "Remember settings";
            toolTip1.SetToolTip(rememberSettingsCheckBox, "This *guarantees* that the current settings will persist and be restored the next time the application is launched.");
            rememberSettingsCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveChangelogCheckBox
            // 
            saveChangelogCheckBox.AutoSize = true;
            saveChangelogCheckBox.Checked = true;
            saveChangelogCheckBox.CheckState = CheckState.Checked;
            saveChangelogCheckBox.Location = new Point(3, 74);
            saveChangelogCheckBox.Margin = new Padding(3, 4, 3, 4);
            saveChangelogCheckBox.Name = "saveChangelogCheckBox";
            saveChangelogCheckBox.Size = new Size(136, 24);
            saveChangelogCheckBox.TabIndex = 2;
            saveChangelogCheckBox.Text = "Save changelog";
            toolTip1.SetToolTip(saveChangelogCheckBox, "This will result in the creation of a text document that *showcases* changes made in the exported mod.");
            saveChangelogCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(flowLayoutPanel1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(215, 4);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(240, 217);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Options";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(button4);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(button3);
            flowLayoutPanel1.Controls.Add(button6);
            flowLayoutPanel1.Controls.Add(button5);
            flowLayoutPanel1.Controls.Add(button7);
            flowLayoutPanel1.Controls.Add(button8);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(234, 189);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // button4
            // 
            button4.AutoSize = true;
            button4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button4.Location = new Point(3, 4);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.MaximumSize = new Size(194, 0);
            button4.MinimumSize = new Size(194, 0);
            button4.Name = "button4";
            button4.Size = new Size(194, 30);
            button4.TabIndex = 3;
            button4.Text = "Asset Table";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(3, 42);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.MaximumSize = new Size(194, 0);
            button1.MinimumSize = new Size(194, 0);
            button1.Name = "button1";
            button1.Size = new Size(194, 30);
            button1.TabIndex = 4;
            button1.Text = "General Emblem Data";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button2.Location = new Point(3, 80);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.MaximumSize = new Size(194, 0);
            button2.MinimumSize = new Size(194, 0);
            button2.Name = "button2";
            button2.Size = new Size(194, 30);
            button2.TabIndex = 5;
            button2.Text = "Bond Level Bonuses";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button3.Location = new Point(3, 118);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.MaximumSize = new Size(194, 0);
            button3.MinimumSize = new Size(194, 0);
            button3.Name = "button3";
            button3.Size = new Size(194, 30);
            button3.TabIndex = 6;
            button3.Text = "Bond Level Requirements";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // button6
            // 
            button6.AutoSize = true;
            button6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button6.Location = new Point(3, 156);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.MaximumSize = new Size(194, 0);
            button6.MinimumSize = new Size(194, 0);
            button6.Name = "button6";
            button6.Size = new Size(194, 30);
            button6.TabIndex = 7;
            button6.Text = "Class Data";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // button5
            // 
            button5.AutoSize = true;
            button5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button5.Location = new Point(3, 194);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.MaximumSize = new Size(194, 0);
            button5.MinimumSize = new Size(194, 0);
            button5.Name = "button5";
            button5.Size = new Size(194, 30);
            button5.TabIndex = 8;
            button5.Text = "Character Data";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // button7
            // 
            button7.AutoSize = true;
            button7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button7.Location = new Point(3, 232);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.MaximumSize = new Size(194, 0);
            button7.MinimumSize = new Size(194, 0);
            button7.Name = "button7";
            button7.Size = new Size(194, 30);
            button7.TabIndex = 9;
            button7.Text = "Map Units";
            button7.UseVisualStyleBackColor = true;
            button7.Click += Button7_Click;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 60000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            // 
            // button8
            // 
            button8.AutoSize = true;
            button8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button8.Location = new Point(3, 270);
            button8.Margin = new Padding(3, 4, 3, 4);
            button8.MaximumSize = new Size(194, 0);
            button8.MinimumSize = new Size(194, 0);
            button8.Name = "button8";
            button8.Size = new Size(194, 30);
            button8.TabIndex = 10;
            button8.Text = "Text Data";
            button8.UseVisualStyleBackColor = true;
            button8.Click += Button8_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 225);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "A Little *Secret* Ingredient";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private Button randomizeAndExportButton;
        private GroupBox groupBox2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button1;
        private CheckBox rememberSettingsCheckBox;
        private CheckBox saveChangelogCheckBox;
        private Button button2;
        private Button button3;
        private ToolTip toolTip1;
        private Button button4;
        private Button button5;
        private CheckBox exportLayeredFSCheckBox;
        private CheckBox exportCobaltCheckBox;
        private Button button6;
        private Button button7;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button8;
    }
}