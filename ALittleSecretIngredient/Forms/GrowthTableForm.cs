namespace ALittleSecretIngredient.Forms
{
    public partial class GrowthTableForm : Form
    {
        private GlobalData GlobalData { get; }
        internal SelectionDistributionForm InheritanceSkills { get; }
        internal NumericDistributionForm InheritanceSkillsCount { get; }
        internal SelectionDistributionForm SynchroStatSkillsAlly { get; }
        internal NumericDistributionForm SynchroStatSkillsAllyCount { get; }
        internal SelectionDistributionForm SynchroStatSkillsEnemy { get; }
        internal NumericDistributionForm SynchroStatSkillsEnemyCount { get; }
        internal SelectionDistributionForm SynchroGeneralSkillsAlly { get; }
        internal NumericDistributionForm SynchroGeneralSkillsAllyCount { get; }
        internal SelectionDistributionForm SynchroGeneralSkillsEnemy { get; }
        internal NumericDistributionForm SynchroGeneralSkillsEnemyCount { get; }
        internal SelectionDistributionForm EngageSkills { get; }
        internal NumericDistributionForm EngageSkillsCount { get; }
        internal SelectionDistributionForm EngageItems { get; }
        internal NumericDistributionForm EngageItemsCount { get; }
        internal SelectionDistributionForm Aptitude { get; }
        internal NumericDistributionForm AptitudeCount { get; }
        internal NumericDistributionForm SkillInheritanceLevel { get; }
        internal NumericDistributionForm StrongBondLevel { get; }
        internal NumericDistributionForm DeepSynergyLevel { get; }
        internal GrowthTableForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            InheritanceSkills = new(GlobalData, RandomizerDistribution.InheritanceSkills, "Inheritable Skills");
            InheritanceSkillsCount = new(GlobalData, RandomizerDistribution.InheritanceSkills, "Inheritable Skills");
            SynchroStatSkillsAlly = new(GlobalData, RandomizerDistribution.SynchroStatSkillsAlly, "Ally Sync Stat Skills");
            SynchroStatSkillsAllyCount = new(GlobalData, RandomizerDistribution.SynchroStatSkillsAlly, "Ally Sync Stat Skills");
            SynchroStatSkillsEnemy = new(GlobalData, RandomizerDistribution.SynchroStatSkillsEnemy, "Enemy Sync Stat Skills");
            SynchroStatSkillsEnemyCount = new(GlobalData, RandomizerDistribution.SynchroStatSkillsEnemy, "Enemy Sync Stat Skills");
            SynchroGeneralSkillsAlly = new(GlobalData, RandomizerDistribution.SynchroGeneralSkillsAlly, "Ally Sync Skills");
            SynchroGeneralSkillsAllyCount = new(GlobalData, RandomizerDistribution.SynchroGeneralSkillsAlly, "Ally Sync Skills");
            SynchroGeneralSkillsEnemy = new(GlobalData, RandomizerDistribution.SynchroGeneralSkillsEnemy, "Enemy Sync Skills");
            SynchroGeneralSkillsEnemyCount = new(GlobalData, RandomizerDistribution.SynchroGeneralSkillsEnemy, "Enemy Sync Skills");
            EngageSkills = new(GlobalData, RandomizerDistribution.EngageSkills, "Engage Skills");
            EngageSkillsCount = new(GlobalData, RandomizerDistribution.EngageSkills, "Engage Skills");
            EngageItems = new(GlobalData, RandomizerDistribution.EngageItems, "Engage Weapons");
            EngageItemsCount = new(GlobalData, RandomizerDistribution.EngageItems, "Engage Weapons");
            Aptitude = new(GlobalData, RandomizerDistribution.GrowthTableAptitude, "Proficiency Unlocks");
            AptitudeCount = new(GlobalData, RandomizerDistribution.GrowthTableAptitude, "Proficiency Unlocks");
            SkillInheritanceLevel = new(GlobalData, RandomizerDistribution.SkillInheritanceLevel, "Skill Inheritance Unlock Level");
            StrongBondLevel = new(GlobalData, RandomizerDistribution.StrongBondLevel, "Strong Bond Unlock Level");
            DeepSynergyLevel = new(GlobalData, RandomizerDistribution.DeepSynergyLevel, "Deep Synergy Unlock Level");
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InheritanceSkills.Show();
            InheritanceSkills.Activate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            InheritanceSkillsCount.Show();
            InheritanceSkillsCount.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SynchroStatSkillsAlly.Show();
            SynchroStatSkillsAlly.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SynchroStatSkillsAllyCount.Show();
            SynchroStatSkillsAllyCount.Activate();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SynchroStatSkillsEnemy.Show();
            SynchroStatSkillsEnemy.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SynchroStatSkillsEnemyCount.Show();
            SynchroStatSkillsEnemyCount.Activate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            SynchroGeneralSkillsAlly.Show();
            SynchroGeneralSkillsAlly.Activate();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            SynchroGeneralSkillsAllyCount.Show();
            SynchroGeneralSkillsAllyCount.Activate();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SynchroGeneralSkillsEnemy.Show();
            SynchroGeneralSkillsEnemy.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SynchroGeneralSkillsEnemyCount.Show();
            SynchroGeneralSkillsEnemyCount.Activate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            EngageSkills.Show();
            EngageSkills.Activate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            EngageSkillsCount.Show();
            EngageSkillsCount.Activate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            EngageItems.Show();
            EngageItems.Activate();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            EngageItemsCount.Show();
            EngageItemsCount.Activate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            Aptitude.Show();
            Aptitude.Activate();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            AptitudeCount.Show();
            AptitudeCount.Activate();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            SkillInheritanceLevel.Show();
            SkillInheritanceLevel.Activate();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            StrongBondLevel.Show();
            StrongBondLevel.Activate();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            DeepSynergyLevel.Show();
            DeepSynergyLevel.Activate();
        }
    }
}
