
namespace ALittleSecretIngredient.Forms
{
    partial class SelectionDistributionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionDistributionForm));
            label1 = new Label();
            distributionSelectComboBox = new ComboBox();
            dataGridView1 = new DataGridView();
            descriptionLabel = new Label();
            pNumericUpDown = new NumericUpDown();
            argLabel1 = new Label();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 3;
            label1.Text = "Mode:";
            // 
            // distributionSelectComboBox
            // 
            distributionSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            distributionSelectComboBox.FormattingEnabled = true;
            distributionSelectComboBox.Location = new Point(12, 32);
            distributionSelectComboBox.Name = "distributionSelectComboBox";
            distributionSelectComboBox.Size = new Size(189, 28);
            distributionSelectComboBox.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Location = new Point(207, 32);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.Size = new Size(400, 609);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellEndEdit += CommitEdit;
            dataGridView1.DataError += DataError;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(12, 117);
            descriptionLabel.MaximumSize = new Size(189, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(85, 20);
            descriptionLabel.TabIndex = 10;
            descriptionLabel.Text = "Description";
            // 
            // pNumericUpDown
            // 
            pNumericUpDown.DecimalPlaces = 3;
            pNumericUpDown.Location = new Point(12, 87);
            pNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            pNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            pNumericUpDown.Name = "pNumericUpDown";
            pNumericUpDown.Size = new Size(189, 27);
            pNumericUpDown.TabIndex = 12;
            // 
            // argLabel1
            // 
            argLabel1.AutoSize = true;
            argLabel1.Location = new Point(12, 63);
            argLabel1.Name = "argLabel1";
            argLabel1.Size = new Size(186, 20);
            argLabel1.TabIndex = 11;
            argLabel1.Text = "Randomization Probability";
            // 
            // button1
            // 
            button1.Location = new Point(111, 577);
            button1.Name = "button1";
            button1.Size = new Size(90, 29);
            button1.TabIndex = 13;
            button1.Text = "Empty";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Empty_Click;
            // 
            // button2
            // 
            button2.Location = new Point(111, 612);
            button2.Name = "button2";
            button2.Size = new Size(90, 29);
            button2.TabIndex = 14;
            button2.Text = "Fill";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Fill_Click;
            // 
            // SelectionDistributionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(619, 653);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pNumericUpDown);
            Controls.Add(argLabel1);
            Controls.Add(descriptionLabel);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Controls.Add(distributionSelectComboBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectionDistributionForm";
            Text = "Randomizer Options";
            Load += SelectionDistributionForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox distributionSelectComboBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.NumericUpDown pNumericUpDown;
        private System.Windows.Forms.Label argLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}