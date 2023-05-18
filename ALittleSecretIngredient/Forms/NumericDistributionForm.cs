using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ALittleSecretIngredient.Probability;

namespace ALittleSecretIngredient.Forms
{
    public partial class NumericDistributionForm : Form
    {
        public IDistribution[] distributions = new IDistribution[]
        {
                new UniformConstant(100, 0, 100),
                new UniformRelative(100, -25, 25),
                new UniformProportional(100, 0.5, 1.5),
                new NormalConstant(100, 50, 25),
                new NormalRelative(100, 25),
                new NormalProportional(100, 0.25),
                new Redistribution(100)
        }; // Just some example data for testing purposes, don't worry about it ;)
        public int idx;

        private GlobalData GlobalData { get; }
        private RandomizerDistribution RD { get; }
        private bool HasValue { get; set; }
        private bool Initialized { get; set; }

        internal NumericDistributionForm(GlobalData globalData, RandomizerDistribution rd, string title)
        {
            GlobalData = globalData;
            RD = rd;
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;

            Text = title;
            distributionSelectComboBox.DataSource = numericDistributionNames;

            distributionSelectComboBox.SelectedIndexChanged += SelectedDistributionChanged;
            argNumericUpDown1.ValueChanged += CommitEdit;
            argNumericUpDown2.ValueChanged += CommitEdit;
            argNumericUpDown3.ValueChanged += CommitEdit;
        }

        private void NumericDistribution_Load(object sender, EventArgs e)
        {
            if (!Initialized)
                Initialize(GlobalData.DDS.GetNumericDistributionSetup(RD));
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
            descriptionLabel.Text = numericDistributionDescriptions[idx];
            List<double> distributionConfig = Get().GetConfig();
            if (distributionConfig.Count > 1)
            {
                argLabel1.Visible = true;
                argNumericUpDown1.Visible = true;
                argLabel1.Text = numericDistributionArgNames[idx].Item1;
                argNumericUpDown1.Value = (decimal)distributionConfig[1];
            }
            else
            {
                argLabel1.Visible = false;
                argNumericUpDown1.Visible = false;
            }
            if (distributionConfig.Count > 2)
            {
                argLabel2.Visible = true;
                argNumericUpDown2.Visible = true;
                argLabel2.Text = numericDistributionArgNames[idx].Item2;
                argNumericUpDown2.Value = (decimal)distributionConfig[2];
            }
            else
            {
                argLabel2.Visible = false;
                argNumericUpDown2.Visible = false;
            }
            if (distributionConfig.Count > 3)
            {
                argLabel3.Visible = true;
                argNumericUpDown3.Visible = true;
                argLabel3.Text = numericDistributionArgNames[idx].Item3;
                argNumericUpDown3.Value = (decimal)distributionConfig[3];
            }
            else
            {
                argLabel3.Visible = false;
                argNumericUpDown3.Visible = false;
            }
        }

        private void CommitEdit(object? sender, EventArgs e)
        {
            double arg0 = idx;
            if (Get() is Redistribution)
                arg0 = 8;
            List<double> args = new()
            {
                arg0,
                (double)argNumericUpDown1.Value,
                (double)argNumericUpDown2.Value,
                (double)argNumericUpDown3.Value
            };
            SetCurrent(CreateDistribution(args));
        }

        public IDistribution Get()
        {
            if (!HasValue)
                Initialize(GlobalData.DDS.GetNumericDistributionSetup(RD));
            return distributions[idx];
        }

        internal void Set(IDistribution d)
        {
            idx = (int)d.GetConfig()[0];
            if (idx == 8) idx = 6;
            SetCurrent(d);
            HasValue = true;
        }

        private void SetCurrent(IDistribution d)
        {
            distributions[idx] = d;
        }

        internal void Initialize(NumericDistributionSetup nds)
        {
            if (HasValue)
            {
                for (int i = 0; i < distributions.Length; i++)
                    if (i != idx)
                        distributions[i] = nds.distributions[i];
            }
            else
            {
                distributions = nds.distributions;
                idx = nds.idx;
            }
            Initialized = true;
            HasValue = true;
        }
    }
}
