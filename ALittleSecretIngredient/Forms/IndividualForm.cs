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
        internal NumericDistributionForm SkillPoint { get; set; }
        internal SelectionDistributionForm Aptitude { get; set; }
        internal NumericDistributionForm AptitudeCount { get; set; }
        internal SelectionDistributionForm SubAptitude { get; set; }
        internal NumericDistributionForm SubAptitudeCount { get; set; }
        internal IndividualForm(GlobalData globalData)
        {
            GlobalData = globalData;
            Age = new(GlobalData, RandomizerDistribution.Age, "Character Age");
            LevelAlly = new(GlobalData, RandomizerDistribution.LevelAlly, "Ally Starting Level");
            LevelEnemy = new(GlobalData, RandomizerDistribution.LevelEnemy, "Enemy Level");
            InternalLevel = new(GlobalData, RandomizerDistribution.InternalLevel, "Starting Internal Level");
            SupportCategory = new(GlobalData, RandomizerDistribution.SupportCategory, "Support Categories");
            SkillPoint = new(GlobalData, RandomizerDistribution.SkillPoint, "Starting Skill Points");
            Aptitude = new(GlobalData, RandomizerDistribution.IndividualAptitude, "Primary Proficiencies");
            AptitudeCount = new(GlobalData, RandomizerDistribution.IndividualAptitude, "Primary Proficiencies");
            SubAptitude = new(GlobalData, RandomizerDistribution.SubAptitude, "Secondary Proficiencies");
            SubAptitudeCount = new(GlobalData, RandomizerDistribution.SubAptitude, "Secondary Proficiencies");
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

        private void Button6_Click(object sender, EventArgs e)
        {
            SkillPoint.Show();
            SkillPoint.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Aptitude.Show();
            Aptitude.Activate();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            AptitudeCount.Show();
            AptitudeCount.Activate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            SubAptitude.Show();
            SubAptitude.Activate();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            SubAptitudeCount.Show();
            SubAptitudeCount.Activate();
        }
    }
}
