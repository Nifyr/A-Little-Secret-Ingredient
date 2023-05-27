namespace ALittleSecretIngredient.Forms
{
    public partial class IndividualForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm Age { get; set; }
        internal NumericDistributionForm LevelAlly { get; set; }
        internal NumericDistributionForm LevelEnemy { get; set; }
        internal NumericDistributionForm InternalLevel { get; set; }
        internal SelectionDistributionForm SupportCategory { get; set; }
        internal IndividualForm(GlobalData globalData)
        {
            GlobalData = globalData;
            Age = new(GlobalData, RandomizerDistribution.Age, "Character Age");
            LevelAlly = new(GlobalData, RandomizerDistribution.LevelAlly, "Ally Starting Level");
            LevelEnemy = new(GlobalData, RandomizerDistribution.LevelEnemy, "Enemy Level");
            InternalLevel = new(GlobalData, RandomizerDistribution.InternalLevel, "Starting Internal Level");
            SupportCategory = new(GlobalData, RandomizerDistribution.SupportCategory, "Support Categories");
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
            LevelAlly.Show();
            LevelAlly.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            LevelEnemy.Show();
            LevelEnemy.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            InternalLevel.Show();
            InternalLevel.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SupportCategory.Show();
            SupportCategory.Activate();
        }
    }
}
