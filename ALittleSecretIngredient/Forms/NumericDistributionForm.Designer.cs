
namespace ALittleSecretIngredient.Forms
{
    partial class NumericDistributionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumericDistributionForm));
            distributionSelectComboBox = new ComboBox();
            label1 = new Label();
            argLabel1 = new Label();
            argNumericUpDown1 = new NumericUpDown();
            argLabel2 = new Label();
            argNumericUpDown2 = new NumericUpDown();
            argNumericUpDown3 = new NumericUpDown();
            argLabel3 = new Label();
            descriptionLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown3).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // distributionSelectComboBox
            // 
            distributionSelectComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            distributionSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            distributionSelectComboBox.FormattingEnabled = true;
            distributionSelectComboBox.Location = new Point(6, 21);
            distributionSelectComboBox.Name = "distributionSelectComboBox";
            distributionSelectComboBox.Size = new Size(171, 23);
            distributionSelectComboBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(132, 15);
            label1.TabIndex = 1;
            label1.Text = "Probability Distribution:";
            // 
            // argLabel1
            // 
            argLabel1.AutoSize = true;
            argLabel1.Location = new Point(183, 3);
            argLabel1.Name = "argLabel1";
            argLabel1.Size = new Size(32, 15);
            argLabel1.TabIndex = 2;
            argLabel1.Text = "Arg1";
            // 
            // argNumericUpDown1
            // 
            argNumericUpDown1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            argNumericUpDown1.AutoSize = true;
            argNumericUpDown1.DecimalPlaces = 3;
            argNumericUpDown1.Location = new Point(183, 21);
            argNumericUpDown1.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            argNumericUpDown1.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            argNumericUpDown1.Name = "argNumericUpDown1";
            argNumericUpDown1.Size = new Size(171, 23);
            argNumericUpDown1.TabIndex = 3;
            // 
            // argLabel2
            // 
            argLabel2.AutoSize = true;
            argLabel2.Location = new Point(183, 47);
            argLabel2.Name = "argLabel2";
            argLabel2.Size = new Size(32, 15);
            argLabel2.TabIndex = 4;
            argLabel2.Text = "Arg2";
            // 
            // argNumericUpDown2
            // 
            argNumericUpDown2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            argNumericUpDown2.AutoSize = true;
            argNumericUpDown2.DecimalPlaces = 3;
            argNumericUpDown2.Location = new Point(183, 65);
            argNumericUpDown2.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            argNumericUpDown2.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            argNumericUpDown2.Name = "argNumericUpDown2";
            argNumericUpDown2.Size = new Size(171, 23);
            argNumericUpDown2.TabIndex = 5;
            // 
            // argNumericUpDown3
            // 
            argNumericUpDown3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            argNumericUpDown3.AutoSize = true;
            argNumericUpDown3.DecimalPlaces = 3;
            argNumericUpDown3.Location = new Point(183, 109);
            argNumericUpDown3.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            argNumericUpDown3.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            argNumericUpDown3.Name = "argNumericUpDown3";
            argNumericUpDown3.Size = new Size(171, 23);
            argNumericUpDown3.TabIndex = 7;
            // 
            // argLabel3
            // 
            argLabel3.AutoSize = true;
            argLabel3.Location = new Point(183, 91);
            argLabel3.Name = "argLabel3";
            argLabel3.Size = new Size(32, 15);
            argLabel3.TabIndex = 6;
            argLabel3.Text = "Arg3";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(6, 47);
            descriptionLabel.Name = "descriptionLabel";
            tableLayoutPanel1.SetRowSpan(descriptionLabel, 4);
            descriptionLabel.Size = new Size(67, 15);
            descriptionLabel.TabIndex = 9;
            descriptionLabel.Text = "Description";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(argNumericUpDown3, 1, 5);
            tableLayoutPanel1.Controls.Add(descriptionLabel, 0, 2);
            tableLayoutPanel1.Controls.Add(argLabel3, 1, 4);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(argNumericUpDown2, 1, 3);
            tableLayoutPanel1.Controls.Add(distributionSelectComboBox, 0, 1);
            tableLayoutPanel1.Controls.Add(argLabel2, 1, 2);
            tableLayoutPanel1.Controls.Add(argLabel1, 1, 0);
            tableLayoutPanel1.Controls.Add(argNumericUpDown1, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(3);
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(360, 137);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // NumericDistributionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 137);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NumericDistributionForm";
            Text = "Randomizer Options";
            Load += NumericDistribution_Load;
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown3).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox distributionSelectComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label argLabel1;
        private System.Windows.Forms.NumericUpDown argNumericUpDown1;
        private System.Windows.Forms.Label argLabel2;
        private System.Windows.Forms.NumericUpDown argNumericUpDown2;
        private System.Windows.Forms.NumericUpDown argNumericUpDown3;
        private System.Windows.Forms.Label argLabel3;
        private System.Windows.Forms.Label descriptionLabel;
        private TableLayoutPanel tableLayoutPanel1;
    }
}