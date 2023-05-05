
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
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown3).BeginInit();
            SuspendLayout();
            // 
            // distributionSelectComboBox
            // 
            distributionSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            distributionSelectComboBox.FormattingEnabled = true;
            distributionSelectComboBox.Location = new Point(12, 32);
            distributionSelectComboBox.Name = "distributionSelectComboBox";
            distributionSelectComboBox.Size = new Size(189, 28);
            distributionSelectComboBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(166, 20);
            label1.TabIndex = 1;
            label1.Text = "Probability Distribution:";
            // 
            // argLabel1
            // 
            argLabel1.AutoSize = true;
            argLabel1.Location = new Point(208, 9);
            argLabel1.Name = "argLabel1";
            argLabel1.Size = new Size(41, 20);
            argLabel1.TabIndex = 2;
            argLabel1.Text = "Arg1";
            // 
            // argNumericUpDown1
            // 
            argNumericUpDown1.DecimalPlaces = 3;
            argNumericUpDown1.Location = new Point(208, 33);
            argNumericUpDown1.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            argNumericUpDown1.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            argNumericUpDown1.Name = "argNumericUpDown1";
            argNumericUpDown1.Size = new Size(189, 27);
            argNumericUpDown1.TabIndex = 3;
            // 
            // argLabel2
            // 
            argLabel2.AutoSize = true;
            argLabel2.Location = new Point(208, 63);
            argLabel2.Name = "argLabel2";
            argLabel2.Size = new Size(41, 20);
            argLabel2.TabIndex = 4;
            argLabel2.Text = "Arg2";
            // 
            // argNumericUpDown2
            // 
            argNumericUpDown2.DecimalPlaces = 3;
            argNumericUpDown2.Location = new Point(208, 86);
            argNumericUpDown2.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            argNumericUpDown2.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            argNumericUpDown2.Name = "argNumericUpDown2";
            argNumericUpDown2.Size = new Size(189, 27);
            argNumericUpDown2.TabIndex = 5;
            // 
            // argNumericUpDown3
            // 
            argNumericUpDown3.DecimalPlaces = 3;
            argNumericUpDown3.Location = new Point(208, 139);
            argNumericUpDown3.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            argNumericUpDown3.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            argNumericUpDown3.Name = "argNumericUpDown3";
            argNumericUpDown3.Size = new Size(189, 27);
            argNumericUpDown3.TabIndex = 7;
            // 
            // argLabel3
            // 
            argLabel3.AutoSize = true;
            argLabel3.Location = new Point(208, 116);
            argLabel3.Name = "argLabel3";
            argLabel3.Size = new Size(41, 20);
            argLabel3.TabIndex = 6;
            argLabel3.Text = "Arg3";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(12, 63);
            descriptionLabel.MaximumSize = new Size(189, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(85, 20);
            descriptionLabel.TabIndex = 9;
            descriptionLabel.Text = "Description";
            // 
            // NumericDistribution
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 183);
            Controls.Add(descriptionLabel);
            Controls.Add(argNumericUpDown3);
            Controls.Add(argLabel3);
            Controls.Add(argNumericUpDown2);
            Controls.Add(argLabel2);
            Controls.Add(argNumericUpDown1);
            Controls.Add(argLabel1);
            Controls.Add(label1);
            Controls.Add(distributionSelectComboBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NumericDistribution";
            Text = "Randomizer Options";
            Load += NumericDistribution_Load;
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)argNumericUpDown3).EndInit();
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
    }
}