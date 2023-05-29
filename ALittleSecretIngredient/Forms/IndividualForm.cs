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
        internal NumericDistributionForm OffsetNHpAlly { get; set; }
        internal NumericDistributionForm OffsetNStrAlly { get; set; }
        internal NumericDistributionForm OffsetNTechAlly { get; set; }
        internal NumericDistributionForm OffsetNQuickAlly { get; set; }
        internal NumericDistributionForm OffsetNLuckAlly { get; set; }
        internal NumericDistributionForm OffsetNDefAlly { get; set; }
        internal NumericDistributionForm OffsetNMagicAlly { get; set; }
        internal NumericDistributionForm OffsetNMdefAlly { get; set; }
        internal NumericDistributionForm OffsetNPhysAlly { get; set; }
        internal NumericDistributionForm OffsetNSightAlly { get; set; }
        internal NumericDistributionForm OffsetNMoveAlly { get; set; }
        internal NumericDistributionForm OffsetNTotalAlly { get; set; }
        internal NumericDistributionForm OffsetNHpEnemy { get; set; }
        internal NumericDistributionForm OffsetNStrEnemy { get; set; }
        internal NumericDistributionForm OffsetNTechEnemy { get; set; }
        internal NumericDistributionForm OffsetNQuickEnemy { get; set; }
        internal NumericDistributionForm OffsetNLuckEnemy { get; set; }
        internal NumericDistributionForm OffsetNDefEnemy { get; set; }
        internal NumericDistributionForm OffsetNMagicEnemy { get; set; }
        internal NumericDistributionForm OffsetNMdefEnemy { get; set; }
        internal NumericDistributionForm OffsetNPhysEnemy { get; set; }
        internal NumericDistributionForm OffsetNSightEnemy { get; set; }
        internal NumericDistributionForm OffsetNMoveEnemy { get; set; }
        internal NumericDistributionForm OffsetNTotalEnemy { get; set; }
        internal NumericDistributionForm OffsetHHpEnemy { get; set; }
        internal NumericDistributionForm OffsetHStrEnemy { get; set; }
        internal NumericDistributionForm OffsetHTechEnemy { get; set; }
        internal NumericDistributionForm OffsetHQuickEnemy { get; set; }
        internal NumericDistributionForm OffsetHLuckEnemy { get; set; }
        internal NumericDistributionForm OffsetHDefEnemy { get; set; }
        internal NumericDistributionForm OffsetHMagicEnemy { get; set; }
        internal NumericDistributionForm OffsetHMdefEnemy { get; set; }
        internal NumericDistributionForm OffsetHPhysEnemy { get; set; }
        internal NumericDistributionForm OffsetHSightEnemy { get; set; }
        internal NumericDistributionForm OffsetHMoveEnemy { get; set; }
        internal NumericDistributionForm OffsetHTotalEnemy { get; set; }
        internal NumericDistributionForm OffsetLHpEnemy { get; set; }
        internal NumericDistributionForm OffsetLStrEnemy { get; set; }
        internal NumericDistributionForm OffsetLTechEnemy { get; set; }
        internal NumericDistributionForm OffsetLQuickEnemy { get; set; }
        internal NumericDistributionForm OffsetLLuckEnemy { get; set; }
        internal NumericDistributionForm OffsetLDefEnemy { get; set; }
        internal NumericDistributionForm OffsetLMagicEnemy { get; set; }
        internal NumericDistributionForm OffsetLMdefEnemy { get; set; }
        internal NumericDistributionForm OffsetLPhysEnemy { get; set; }
        internal NumericDistributionForm OffsetLSightEnemy { get; set; }
        internal NumericDistributionForm OffsetLMoveEnemy { get; set; }
        internal NumericDistributionForm OffsetLTotalEnemy { get; set; }
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
            OffsetNHpAlly = new(GlobalData, RandomizerDistribution.OffsetNHpAlly, "Ally HP Base Stat");
            OffsetNStrAlly = new(GlobalData, RandomizerDistribution.OffsetNStrAlly, "Ally Strength Base Stat");
            OffsetNTechAlly = new(GlobalData, RandomizerDistribution.OffsetNTechAlly, "Ally Dexterity Base Stat");
            OffsetNQuickAlly = new(GlobalData, RandomizerDistribution.OffsetNQuickAlly, "Ally Speed Base Stat");
            OffsetNLuckAlly = new(GlobalData, RandomizerDistribution.OffsetNLuckAlly, "Ally Luck Base Stat");
            OffsetNDefAlly = new(GlobalData, RandomizerDistribution.OffsetNDefAlly, "Ally Defense Base Stat");
            OffsetNMagicAlly = new(GlobalData, RandomizerDistribution.OffsetNMagicAlly, "Ally Magic Base Stat");
            OffsetNMdefAlly = new(GlobalData, RandomizerDistribution.OffsetNMdefAlly, "Ally Resistance Base Stat");
            OffsetNPhysAlly = new(GlobalData, RandomizerDistribution.OffsetNPhysAlly, "Ally Build Base Stat");
            OffsetNSightAlly = new(GlobalData, RandomizerDistribution.OffsetNSightAlly, "Ally Sight Base Stat");
            OffsetNMoveAlly = new(GlobalData, RandomizerDistribution.OffsetNMoveAlly, "Ally Movement Base Stat");
            OffsetNTotalAlly = new(GlobalData, RandomizerDistribution.OffsetNTotalAlly, "Ally Base Stat Total");
            OffsetNHpEnemy = new(GlobalData, RandomizerDistribution.OffsetNHpEnemy, "Enemy Normal HP Base Stat");
            OffsetNStrEnemy = new(GlobalData, RandomizerDistribution.OffsetNStrEnemy, "Enemy Normal Strength Base Stat");
            OffsetNTechEnemy = new(GlobalData, RandomizerDistribution.OffsetNTechEnemy, "Enemy Normal Dexterity Base Stat");
            OffsetNQuickEnemy = new(GlobalData, RandomizerDistribution.OffsetNQuickEnemy, "Enemy Normal Speed Base Stat");
            OffsetNLuckEnemy = new(GlobalData, RandomizerDistribution.OffsetNLuckEnemy, "Enemy Normal Luck Base Stat");
            OffsetNDefEnemy = new(GlobalData, RandomizerDistribution.OffsetNDefEnemy, "Enemy Normal Defense Base Stat");
            OffsetNMagicEnemy = new(GlobalData, RandomizerDistribution.OffsetNMagicEnemy, "Enemy Normal Magic Base Stat");
            OffsetNMdefEnemy = new(GlobalData, RandomizerDistribution.OffsetNMdefEnemy, "Enemy Normal Resistance Base Stat");
            OffsetNPhysEnemy = new(GlobalData, RandomizerDistribution.OffsetNPhysEnemy, "Enemy Normal Build Base Stat");
            OffsetNSightEnemy = new(GlobalData, RandomizerDistribution.OffsetNSightEnemy, "Enemy Normal Sight Base Stat");
            OffsetNMoveEnemy = new(GlobalData, RandomizerDistribution.OffsetNMoveEnemy, "Enemy Normal Movement Base Stat");
            OffsetNTotalEnemy = new(GlobalData, RandomizerDistribution.OffsetNTotalEnemy, "Enemy Normal Base Stat Total");
            OffsetHHpEnemy = new(GlobalData, RandomizerDistribution.OffsetHHpEnemy, "Enemy Hard HP Base Stat");
            OffsetHStrEnemy = new(GlobalData, RandomizerDistribution.OffsetHStrEnemy, "Enemy Hard Strength Base Stat");
            OffsetHTechEnemy = new(GlobalData, RandomizerDistribution.OffsetHTechEnemy, "Enemy Hard Dexterity Base Stat");
            OffsetHQuickEnemy = new(GlobalData, RandomizerDistribution.OffsetHQuickEnemy, "Enemy Hard Speed Base Stat");
            OffsetHLuckEnemy = new(GlobalData, RandomizerDistribution.OffsetHLuckEnemy, "Enemy Hard Luck Base Stat");
            OffsetHDefEnemy = new(GlobalData, RandomizerDistribution.OffsetHDefEnemy, "Enemy Hard Defense Base Stat");
            OffsetHMagicEnemy = new(GlobalData, RandomizerDistribution.OffsetHMagicEnemy, "Enemy Hard Magic Base Stat");
            OffsetHMdefEnemy = new(GlobalData, RandomizerDistribution.OffsetHMdefEnemy, "Enemy Hard Resistance Base Stat");
            OffsetHPhysEnemy = new(GlobalData, RandomizerDistribution.OffsetHPhysEnemy, "Enemy Hard Build Base Stat");
            OffsetHSightEnemy = new(GlobalData, RandomizerDistribution.OffsetHSightEnemy, "Enemy Hard Sight Base Stat");
            OffsetHMoveEnemy = new(GlobalData, RandomizerDistribution.OffsetHMoveEnemy, "Enemy Hard Movement Base Stat");
            OffsetHTotalEnemy = new(GlobalData, RandomizerDistribution.OffsetHTotalEnemy, "Enemy Hard Base Stat Total");
            OffsetLHpEnemy = new(GlobalData, RandomizerDistribution.OffsetLHpEnemy, "Enemy Maddening HP Base Stat");
            OffsetLStrEnemy = new(GlobalData, RandomizerDistribution.OffsetLStrEnemy, "Enemy Maddening Strength Base Stat");
            OffsetLTechEnemy = new(GlobalData, RandomizerDistribution.OffsetLTechEnemy, "Enemy Maddening Dexterity Base Stat");
            OffsetLQuickEnemy = new(GlobalData, RandomizerDistribution.OffsetLQuickEnemy, "Enemy Maddening Speed Base Stat");
            OffsetLLuckEnemy = new(GlobalData, RandomizerDistribution.OffsetLLuckEnemy, "Enemy Maddening Luck Base Stat");
            OffsetLDefEnemy = new(GlobalData, RandomizerDistribution.OffsetLDefEnemy, "Enemy Maddening Defense Base Stat");
            OffsetLMagicEnemy = new(GlobalData, RandomizerDistribution.OffsetLMagicEnemy, "Enemy Maddening Magic Base Stat");
            OffsetLMdefEnemy = new(GlobalData, RandomizerDistribution.OffsetLMdefEnemy, "Enemy Maddening Resistance Base Stat");
            OffsetLPhysEnemy = new(GlobalData, RandomizerDistribution.OffsetLPhysEnemy, "Enemy Maddening Build Base Stat");
            OffsetLSightEnemy = new(GlobalData, RandomizerDistribution.OffsetLSightEnemy, "Enemy Maddening Sight Base Stat");
            OffsetLMoveEnemy = new(GlobalData, RandomizerDistribution.OffsetLMoveEnemy, "Enemy Maddening Movement Base Stat");
            OffsetLTotalEnemy = new(GlobalData, RandomizerDistribution.OffsetLTotalEnemy, "Enemy Maddening Base Stat Total");
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

        private void Button11_Click(object sender, EventArgs e)
        {
            OffsetNHpAlly.Show();
            OffsetNHpAlly.Activate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            OffsetNStrAlly.Show();
            OffsetNStrAlly.Activate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            OffsetNTechAlly.Show();
            OffsetNTechAlly.Activate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            OffsetNQuickAlly.Show();
            OffsetNQuickAlly.Activate();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            OffsetNLuckAlly.Show();
            OffsetNLuckAlly.Activate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            OffsetNDefAlly.Show();
            OffsetNDefAlly.Activate();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            OffsetNMagicAlly.Show();
            OffsetNMagicAlly.Activate();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            OffsetNMdefAlly.Show();
            OffsetNMdefAlly.Activate();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            OffsetNPhysAlly.Show();
            OffsetNPhysAlly.Activate();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            OffsetNSightAlly.Show();
            OffsetNSightAlly.Activate();
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            OffsetNMoveAlly.Show();
            OffsetNMoveAlly.Activate();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            OffsetNTotalAlly.Show();
            OffsetNTotalAlly.Activate();
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            OffsetNHpEnemy.Show();
            OffsetNHpEnemy.Activate();
        }

        private void Button33_Click(object sender, EventArgs e)
        {
            OffsetNStrEnemy.Show();
            OffsetNStrEnemy.Activate();
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            OffsetNTechEnemy.Show();
            OffsetNTechEnemy.Activate();
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            OffsetNQuickEnemy.Show();
            OffsetNQuickEnemy.Activate();
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            OffsetNLuckEnemy.Show();
            OffsetNLuckEnemy.Activate();
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            OffsetNDefEnemy.Show();
            OffsetNDefEnemy.Activate();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            OffsetNMagicEnemy.Show();
            OffsetNMagicEnemy.Activate();
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            OffsetNMdefEnemy.Show();
            OffsetNMdefEnemy.Activate();
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            OffsetNPhysEnemy.Show();
            OffsetNPhysEnemy.Activate();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            OffsetNSightEnemy.Show();
            OffsetNSightEnemy.Activate();
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            OffsetNMoveEnemy.Show();
            OffsetNMoveEnemy.Activate();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            OffsetNTotalEnemy.Show();
            OffsetNTotalEnemy.Activate();
        }

        private void Button46_Click(object sender, EventArgs e)
        {
            OffsetHHpEnemy.Show();
            OffsetHHpEnemy.Activate();
        }

        private void Button45_Click(object sender, EventArgs e)
        {
            OffsetHStrEnemy.Show();
            OffsetHStrEnemy.Activate();
        }

        private void Button44_Click(object sender, EventArgs e)
        {
            OffsetHTechEnemy.Show();
            OffsetHTechEnemy.Activate();
        }

        private void Button43_Click(object sender, EventArgs e)
        {
            OffsetHQuickEnemy.Show();
            OffsetHQuickEnemy.Activate();
        }

        private void Button42_Click(object sender, EventArgs e)
        {
            OffsetHLuckEnemy.Show();
            OffsetHLuckEnemy.Activate();
        }

        private void Button41_Click(object sender, EventArgs e)
        {
            OffsetHDefEnemy.Show();
            OffsetHDefEnemy.Activate();
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            OffsetHMagicEnemy.Show();
            OffsetHMagicEnemy.Activate();
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            OffsetHMdefEnemy.Show();
            OffsetHMdefEnemy.Activate();
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            OffsetHPhysEnemy.Show();
            OffsetHPhysEnemy.Activate();
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            OffsetHSightEnemy.Show();
            OffsetHSightEnemy.Activate();
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            OffsetHMoveEnemy.Show();
            OffsetHMoveEnemy.Activate();
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            OffsetHTotalEnemy.Show();
            OffsetHTotalEnemy.Activate();
        }

        private void Button58_Click(object sender, EventArgs e)
        {
            OffsetLHpEnemy.Show();
            OffsetLHpEnemy.Activate();
        }

        private void Button57_Click(object sender, EventArgs e)
        {
            OffsetLStrEnemy.Show();
            OffsetLStrEnemy.Activate();
        }

        private void Button56_Click(object sender, EventArgs e)
        {
            OffsetLTechEnemy.Show();
            OffsetLTechEnemy.Activate();
        }

        private void Button55_Click(object sender, EventArgs e)
        {
            OffsetLQuickEnemy.Show();
            OffsetLQuickEnemy.Activate();
        }

        private void Button54_Click(object sender, EventArgs e)
        {
            OffsetLLuckEnemy.Show();
            OffsetLLuckEnemy.Activate();
        }

        private void Button53_Click(object sender, EventArgs e)
        {
            OffsetLDefEnemy.Show();
            OffsetLDefEnemy.Activate();
        }

        private void Button52_Click(object sender, EventArgs e)
        {
            OffsetLMagicEnemy.Show();
            OffsetLMagicEnemy.Activate();
        }

        private void Button51_Click(object sender, EventArgs e)
        {
            OffsetLMdefEnemy.Show();
            OffsetLMdefEnemy.Activate();
        }

        private void Button50_Click(object sender, EventArgs e)
        {
            OffsetLPhysEnemy.Show();
            OffsetLPhysEnemy.Activate();
        }

        private void Button49_Click(object sender, EventArgs e)
        {
            OffsetLSightEnemy.Show();
            OffsetLSightEnemy.Activate();
        }

        private void Button48_Click(object sender, EventArgs e)
        {
            OffsetLMoveEnemy.Show();
            OffsetLMoveEnemy.Activate();
        }

        private void Button47_Click(object sender, EventArgs e)
        {
            OffsetLTotalEnemy.Show();
            OffsetLTotalEnemy.Activate();
        }
    }
}
