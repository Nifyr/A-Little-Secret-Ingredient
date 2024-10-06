
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
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pNumericUpDown).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 3;
            label1.Text = "Mode:";
            // 
            // distributionSelectComboBox
            // 
            distributionSelectComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            distributionSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            distributionSelectComboBox.FormattingEnabled = true;
            distributionSelectComboBox.Location = new Point(6, 21);
            distributionSelectComboBox.Name = "distributionSelectComboBox";
            distributionSelectComboBox.Size = new Size(172, 23);
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
            tableLayoutPanel1.SetColumnSpan(dataGridView1, 2);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(184, 6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            tableLayoutPanel1.SetRowSpan(dataGridView1, 7);
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.Size = new Size(352, 478);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellEndEdit += CommitEdit;
            dataGridView1.DataError += DataError;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(6, 91);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(67, 15);
            descriptionLabel.TabIndex = 10;
            descriptionLabel.Text = "Description";
            // 
            // pNumericUpDown
            // 
            pNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pNumericUpDown.DecimalPlaces = 3;
            pNumericUpDown.Location = new Point(6, 65);
            pNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            pNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            pNumericUpDown.Name = "pNumericUpDown";
            pNumericUpDown.Size = new Size(172, 23);
            pNumericUpDown.TabIndex = 12;
            // 
            // argLabel1
            // 
            argLabel1.AutoSize = true;
            argLabel1.Location = new Point(6, 47);
            argLabel1.Name = "argLabel1";
            argLabel1.Size = new Size(147, 15);
            argLabel1.TabIndex = 11;
            argLabel1.Text = "Randomization Probability";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(99, 437);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(79, 22);
            button1.TabIndex = 13;
            button1.Text = "Empty";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Empty_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(99, 463);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(79, 22);
            button2.TabIndex = 14;
            button2.Text = "Fill";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Fill_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(button2, 0, 6);
            tableLayoutPanel1.Controls.Add(dataGridView1, 1, 0);
            tableLayoutPanel1.Controls.Add(button1, 0, 5);
            tableLayoutPanel1.Controls.Add(distributionSelectComboBox, 0, 1);
            tableLayoutPanel1.Controls.Add(descriptionLabel, 0, 4);
            tableLayoutPanel1.Controls.Add(pNumericUpDown, 0, 3);
            tableLayoutPanel1.Controls.Add(argLabel1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(3);
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(542, 490);
            tableLayoutPanel1.TabIndex = 15;
            // 
            // SelectionDistributionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 490);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectionDistributionForm";
            Text = "Randomizer Options";
            Load += SelectionDistributionForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pNumericUpDown).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
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
        private TableLayoutPanel tableLayoutPanel1;
    }
}