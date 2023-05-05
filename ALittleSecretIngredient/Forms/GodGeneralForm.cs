namespace ALittleSecretIngredient.Forms
{
    public partial class GodGeneralForm : Form
    {
        private GlobalData GlobalData { get; }
        public NumericDistributionForm EngageCount { get; set; }
        public SelectionDistributionForm Link { get; set; }
        public SelectionDistributionForm EngageAttackAlly { get; set; }
        public SelectionDistributionForm EngageAttackEnemy { get; set; }
        public SelectionDistributionForm EngageAttackLink { get; set; }
        public SelectionDistributionForm LinkGid { get; set; }
        public NumericDistributionForm EngravePower { get; set; }
        public NumericDistributionForm EngraveWeight { get; set; }
        public NumericDistributionForm EngraveHit { get; set; }
        public NumericDistributionForm EngraveCritical { get; set; }
        public NumericDistributionForm EngraveAvoid { get; set; }
        public NumericDistributionForm EngraveSecure { get; set; }
        public NumericDistributionForm SynchroEnhanceHpAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceStrAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceTechAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceQuickAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceLuckAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceDefAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceMagicAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceMdefAlly { get; set; }
        public NumericDistributionForm SynchroEnhancePhysAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceMoveAlly { get; set; }
        public NumericDistributionForm SynchroEnhanceHpEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceStrEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceTechEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceQuickEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceLuckEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceDefEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceMagicEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceMdefEnemy { get; set; }
        public NumericDistributionForm SynchroEnhancePhysEnemy { get; set; }
        public NumericDistributionForm SynchroEnhanceMoveEnemy { get; set; }
        internal GodGeneralForm(GlobalData globalData)
        {
            GlobalData = globalData;
            EngageCount = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngageCount, "Engage Meter Size");
            Link = new SelectionDistributionForm(GlobalData, RandomizerDistribution.Link, "Engage+ Characters");
            EngageAttackAlly = new SelectionDistributionForm(GlobalData, RandomizerDistribution.EngageAttackAlly, "Ally Engage Attacks");
            EngageAttackEnemy = new SelectionDistributionForm(GlobalData, RandomizerDistribution.EngageAttackEnemy, "Enemy Engage Attacks");
            EngageAttackLink = new SelectionDistributionForm(GlobalData, RandomizerDistribution.EngageAttackLink, "Bond Link Skills");
            LinkGid = new SelectionDistributionForm(GlobalData, RandomizerDistribution.LinkGid, "Bond Link Emblems");
            EngravePower = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngravePower, "Engraving Might Bonus");
            EngraveWeight = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngraveWeight, "Engraving Weight Bonus");
            EngraveHit = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngraveHit, "Engraving Hit Bonus");
            EngraveCritical = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngraveCritical, "Engraving Crit Bonus");
            EngraveAvoid = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngraveAvoid, "Engraving Avoid Bonus");
            EngraveSecure = new NumericDistributionForm(GlobalData, RandomizerDistribution.EngraveSecure, "Engraving Dodge Bonus");
            SynchroEnhanceHpAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceHpAlly, "Ally Static Sync HP Bonus");
            SynchroEnhanceStrAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceStrAlly, "Ally Static Sync Strength Bonus");
            SynchroEnhanceTechAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceTechAlly, "Ally Static Sync Dexterity Bonus");
            SynchroEnhanceQuickAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceQuickAlly, "Ally Static Sync Speed Bonus");
            SynchroEnhanceLuckAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceLuckAlly, "Ally Static Sync Luck Bonus");
            SynchroEnhanceDefAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceDefAlly, "Ally Static Sync Defense Bonus");
            SynchroEnhanceMagicAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceMagicAlly, "Ally Static Sync Magic Bonus");
            SynchroEnhanceMdefAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceMdefAlly, "Ally Static Sync Resistance Bonus");
            SynchroEnhancePhysAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhancePhysAlly, "Ally Static Sync Build Bonus");
            SynchroEnhanceMoveAlly = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceMoveAlly, "Ally Static Sync Move Bonus");
            SynchroEnhanceHpEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceHpEnemy, "Enemy Static Sync HP Bonus");
            SynchroEnhanceStrEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceStrEnemy, "Enemy Static Sync Strength Bonus");
            SynchroEnhanceTechEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceTechEnemy, "Enemy Static Sync Dexterity Bonus");
            SynchroEnhanceQuickEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceQuickEnemy, "Enemy Static Sync Speed Bonus");
            SynchroEnhanceLuckEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceLuckEnemy, "Enemy Static Sync Luck Bonus");
            SynchroEnhanceDefEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceDefEnemy, "Enemy Static Sync Defense Bonus");
            SynchroEnhanceMagicEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceMagicEnemy, "Enemy Static Sync Magic Bonus");
            SynchroEnhanceMdefEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceMdefEnemy, "Enemy Static Sync Resistance Bonus");
            SynchroEnhancePhysEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhancePhysEnemy, "Enemy Static Sync Build Bonus");
            SynchroEnhanceMoveEnemy = new NumericDistributionForm(GlobalData, RandomizerDistribution.SynchroEnhanceMoveEnemy, "Enemy Static Sync Move Bonus");
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Link.Show();
            Link.Activate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            EngageCount.Show();
            EngageCount.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            EngageAttackAlly.Show();
            EngageAttackAlly.Activate();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            EngageAttackEnemy.Show();
            EngageAttackEnemy.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            EngageAttackLink.Show();
            EngageAttackLink.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LinkGid.Show();
            LinkGid.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            EngravePower.Show();
            EngravePower.Activate();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            EngraveWeight.Show();
            EngraveWeight.Activate();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            EngraveHit.Show();
            EngraveHit.Activate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            EngraveCritical.Show();
            EngraveCritical.Activate();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            EngraveAvoid.Show();
            EngraveAvoid.Activate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            EngraveSecure.Show();
            EngraveSecure.Activate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            SynchroEnhanceHpAlly.Show();
            SynchroEnhanceHpAlly.Activate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            SynchroEnhanceStrAlly.Show();
            SynchroEnhanceStrAlly.Activate();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            SynchroEnhanceTechAlly.Show();
            SynchroEnhanceTechAlly.Activate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            SynchroEnhanceQuickAlly.Show();
            SynchroEnhanceQuickAlly.Activate();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            SynchroEnhanceLuckAlly.Show();
            SynchroEnhanceLuckAlly.Activate();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            SynchroEnhanceDefAlly.Show();
            SynchroEnhanceDefAlly.Activate();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            SynchroEnhanceMagicAlly.Show();
            SynchroEnhanceMagicAlly.Activate();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            SynchroEnhanceMdefAlly.Show();
            SynchroEnhanceMdefAlly.Activate();
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            SynchroEnhancePhysAlly.Show();
            SynchroEnhancePhysAlly.Activate();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            SynchroEnhanceMoveAlly.Show();
            SynchroEnhanceMoveAlly.Activate();
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            SynchroEnhanceHpEnemy.Show();
            SynchroEnhanceHpEnemy.Activate();
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            SynchroEnhanceStrEnemy.Show();
            SynchroEnhanceStrEnemy.Activate();
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            SynchroEnhanceTechEnemy.Show();
            SynchroEnhanceTechEnemy.Activate();
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            SynchroEnhanceQuickEnemy.Show();
            SynchroEnhanceQuickEnemy.Activate();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            SynchroEnhanceLuckEnemy.Show();
            SynchroEnhanceLuckEnemy.Activate();
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            SynchroEnhanceDefEnemy.Show();
            SynchroEnhanceDefEnemy.Activate();
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            SynchroEnhanceMagicEnemy.Show();
            SynchroEnhanceMagicEnemy.Activate();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            SynchroEnhanceMdefEnemy.Show();
            SynchroEnhanceMdefEnemy.Activate();
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            SynchroEnhancePhysEnemy.Show();
            SynchroEnhancePhysEnemy.Activate();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            SynchroEnhanceMoveEnemy.Show();
            SynchroEnhanceMoveEnemy.Activate();
        }
    }
}
