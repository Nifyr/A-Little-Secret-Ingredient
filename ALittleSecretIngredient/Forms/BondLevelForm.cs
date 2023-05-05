namespace ALittleSecretIngredient.Forms
{
    public partial class BondLevelForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm Exp { get; }
        internal NumericDistributionForm Cost { get; }
        internal BondLevelForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            Exp = new(GlobalData, RandomizerDistribution.Exp, "Exp Requirement Per Bond Level");
            Cost = new(GlobalData, RandomizerDistribution.Cost, "Bond Fragment Cost Per Bond Level");
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            Exp.Show();
            Exp.Activate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Cost.Show();
            Cost.Activate();
        }
    }
}
