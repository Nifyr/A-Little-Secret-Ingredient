using System.Data;
using System.Net;
using static ALittleSecretIngredient.Probability;

namespace ALittleSecretIngredient.Forms
{
    public partial class SelectionDistributionForm : Form
    {
        public IDistribution[] distributions = new IDistribution[]
        {
            new Empirical(100, (new int[] { 1, 0, 2, 0, 3, 0, 4 }).ToList()),
            new UniformSelection(100, (new bool[] { true, false, true, false, true, false, true }).ToList()),
            new Redistribution(100)
        };
        public List<string> itemNames = new(new string[]
        {
            "Item0", "Item1", "Item2", "Item3",
            "Item4", "Item5", "Item6"
        });
        public int idx;

        private bool HasValue { get; set; }
        private bool Initialized { get; set; }
        private GlobalData GlobalData { get; }
        private RandomizerDistribution RD { get; }

        internal SelectionDistributionForm(GlobalData globalData, RandomizerDistribution rd, string title)
        {
            GlobalData = globalData;
            RD = rd;

            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
            Text = title;
            distributionSelectComboBox.DataSource = selectionDistributionNames;
            pNumericUpDown.ValueChanged += CommitEdit;

            distributionSelectComboBox.SelectedIndexChanged += SelectedDistributionChanged;
        }

        private void SelectionDistributionForm_Load(object sender, EventArgs e)
        {
            if (!Initialized)
                Initialize(GlobalData.DDS.GetSelectionDistributionSetup(RD));
            distributionSelectComboBox.SelectedIndex = idx;
            RefreshDistributionDisplay();
        }

        private void SelectedDistributionChanged(object? sender, EventArgs e)
        {
            idx = ((ComboBox)sender!).SelectedIndex;
            RefreshDistributionDisplay();
        }

        /// <summary>
        ///  Fills the form with data from the currently selected distribution.
        /// </summary>
        private void RefreshDistributionDisplay()
        {
            descriptionLabel.Text = selectionDistributionDescriptions[idx];

            pNumericUpDown.ValueChanged -= CommitEdit;
            pNumericUpDown.Value = (decimal)Get().GetConfig()[1];
            pNumericUpDown.ValueChanged += CommitEdit;

            DataTable dataTable = new();
            DataColumn[] columns = { new DataColumn("Name"), new DataColumn() };
            columns[0].ReadOnly = true;
            dataTable.Columns.AddRange(columns);
            switch (Get().GetConfig().First())
            {
                case 6:
                    columns[1].ColumnName = "Weight";
                    columns[1].DataType = typeof(int);
                    List<int> intValues = Get().GetConfig().Skip(2).Select(d => (int)d).ToList();
                    for (int i = 0; i < itemNames.Count; i++)
                        dataTable.Rows.Add(new object[] { itemNames[i], intValues[i] });
                    break;
                case 7:
                    columns[1].ColumnName = "Included";
                    columns[1].DataType = typeof(bool);
                    List<bool> boolValues = Get().GetConfig().Skip(2).Select(d => d == 1).ToList();
                    for (int i = 0; i < itemNames.Count; i++)
                        dataTable.Rows.Add(new object[] { itemNames[i], boolValues[i] });
                    break;
            }
            dataGridView1.DataSource = dataTable;
        }

        private void DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MainForm.ShowDataError();
        }

        private void CommitEdit(object? sender, EventArgs e)
        {
            DataGridViewRowCollection dgvrc = dataGridView1.Rows;
            List<object> data = new();
            for (int row = 0; row < dgvrc.Count; row++)
                data.Add(dgvrc[row].Cells[1].Value);
            List<double> args = new()
            {
                Get().GetConfig()[0],
                (double)pNumericUpDown.Value
            };
            switch (args[0])
            {
                case 6:
                    args.AddRange(data.Select(o => (double)(int)o));
                    break;
                case 7:
                    args.AddRange(data.Select(o => (bool)o ? 1.0 : 0.0));
                    break;
            }
            SetCurrent(CreateDistribution(args));
        }

        private void Empty_Click(object sender, EventArgs e)
        {
            if (idx == 0)
                SetAll(0);
            else if (idx == 1)
                SetAll(false);
        }

        private void Fill_Click(object sender, EventArgs e)
        {
            if (idx == 0)
                SetAll(12);
            else if (idx == 1)
                SetAll(true);
        }

        private void SetAll(object o)
        {
            for (int itemID = 0; itemID < dataGridView1.RowCount; itemID++)
                dataGridView1.Rows[itemID].Cells[1].Value = o;
            CommitEdit(null, null!);
        }

        public IDistribution Get()
        {
            if (!HasValue)
                Initialize(GlobalData.DDS.GetSelectionDistributionSetup(RD));
            return distributions[idx];
        }

        internal void Set(IDistribution d)
        {
            idx = d.GetConfig()[0] switch
            {
                6 => 0,
                7 => 1,
                8 => 2,
                _ => throw new ArgumentException("Unsupported distribution: " + d.GetType().Name)
            };
            SetCurrent(d);
            HasValue = true;
        }

        public void SetCurrent(IDistribution d)
        {
            distributions[idx] = d;
        }

        internal void Initialize(SelectionDistributionSetup sds)
        {
            if (HasValue)
            {
                for (int i = 0; i < distributions.Length; i++)
                    if (i != idx)
                        distributions[i] = sds.distributions[i];
            }
            else
            {
                distributions = sds.distributions;
                idx = sds.idx;
            }
            itemNames = sds.names;
            HasValue = true;
            Initialized = true;
        }
    }
}
