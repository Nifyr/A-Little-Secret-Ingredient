namespace ALittleSecretIngredient.Forms
{
    public partial class IndividualForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm Age { get; set; }
        internal NumericDistributionForm Level { get; set; }
        internal NumericDistributionForm InternalLevel { get; set; }
        internal IndividualForm(GlobalData globalData)
        {
            GlobalData = globalData;
            Age = new(GlobalData, RandomizerDistribution.Age, "Character Age");
            Level = new(GlobalData, RandomizerDistribution.Level, "Starting Level");
            InternalLevel = new(GlobalData, RandomizerDistribution.InternalLevel, "Starting Internal Level");
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Age.Show();
            Age.Activate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Level.Show();
            Level.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            InternalLevel.Show();
            InternalLevel.Activate();
        }
    }
}
