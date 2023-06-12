namespace ALittleSecretIngredient.Forms
{
    public partial class IndividualForm : Form
    {
        private GlobalData GlobalData { get; }
        internal SelectionDistributionForm JidAlly { get; set; }
        internal SelectionDistributionForm JidEnemy { get; set; }
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
        internal NumericDistributionForm LimitHp { get; set; }
        internal NumericDistributionForm LimitStr { get; set; }
        internal NumericDistributionForm LimitTech { get; set; }
        internal NumericDistributionForm LimitQuick { get; set; }
        internal NumericDistributionForm LimitLuck { get; set; }
        internal NumericDistributionForm LimitDef { get; set; }
        internal NumericDistributionForm LimitMagic { get; set; }
        internal NumericDistributionForm LimitMdef { get; set; }
        internal NumericDistributionForm LimitPhys { get; set; }
        internal NumericDistributionForm LimitSight { get; set; }
        internal NumericDistributionForm LimitMove { get; set; }
        internal NumericDistributionForm GrowHp { get; set; }
        internal NumericDistributionForm GrowStr { get; set; }
        internal NumericDistributionForm GrowTech { get; set; }
        internal NumericDistributionForm GrowQuick { get; set; }
        internal NumericDistributionForm GrowLuck { get; set; }
        internal NumericDistributionForm GrowDef { get; set; }
        internal NumericDistributionForm GrowMagic { get; set; }
        internal NumericDistributionForm GrowMdef { get; set; }
        internal NumericDistributionForm GrowPhys { get; set; }
        internal NumericDistributionForm GrowSight { get; set; }
        internal NumericDistributionForm GrowMove { get; set; }
        internal NumericDistributionForm GrowTotal { get; set; }
        internal SelectionDistributionForm ItemsWeapons { get; set; }
        internal NumericDistributionForm ItemsWeaponCount { get; set; }
        internal SelectionDistributionForm ItemsItems { get; set; }
        internal NumericDistributionForm ItemsItemCount { get; set; }
        internal SelectionDistributionForm AttrsAlly { get; set; }
        internal NumericDistributionForm AttrsAllyCount { get; set; }
        internal SelectionDistributionForm AttrsEnemy { get; set; }
        internal NumericDistributionForm AttrsEnemyCount { get; set; }
        internal SelectionDistributionForm CommonSids { get; set; }
        internal NumericDistributionForm CommonSidsCount { get; set; }
        internal IndividualForm(GlobalData globalData)
        {
            GlobalData = globalData;
            JidAlly = new(GlobalData, RandomizerDistribution.JidAlly, "Ally Starting Classes");
            JidEnemy = new(GlobalData, RandomizerDistribution.JidEnemy, "Enemy Classes");
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
            OffsetNHpAlly = new(GlobalData, RandomizerDistribution.OffsetNHpAlly, "Ally HP Base Stat Modifiers");
            OffsetNStrAlly = new(GlobalData, RandomizerDistribution.OffsetNStrAlly, "Ally Strength Base Stat Modifiers");
            OffsetNTechAlly = new(GlobalData, RandomizerDistribution.OffsetNTechAlly, "Ally Dexterity Base Stat Modifiers");
            OffsetNQuickAlly = new(GlobalData, RandomizerDistribution.OffsetNQuickAlly, "Ally Speed Base Stat Modifiers");
            OffsetNLuckAlly = new(GlobalData, RandomizerDistribution.OffsetNLuckAlly, "Ally Luck Base Stat Modifiers");
            OffsetNDefAlly = new(GlobalData, RandomizerDistribution.OffsetNDefAlly, "Ally Defense Base Stat Modifiers");
            OffsetNMagicAlly = new(GlobalData, RandomizerDistribution.OffsetNMagicAlly, "Ally Magic Base Stat Modifiers");
            OffsetNMdefAlly = new(GlobalData, RandomizerDistribution.OffsetNMdefAlly, "Ally Resistance Base Stat Modifiers");
            OffsetNPhysAlly = new(GlobalData, RandomizerDistribution.OffsetNPhysAlly, "Ally Build Base Stat Modifiers");
            OffsetNSightAlly = new(GlobalData, RandomizerDistribution.OffsetNSightAlly, "Ally Sight Base Stat Modifiers");
            OffsetNMoveAlly = new(GlobalData, RandomizerDistribution.OffsetNMoveAlly, "Ally Movement Base Stat Modifiers");
            OffsetNTotalAlly = new(GlobalData, RandomizerDistribution.OffsetNTotalAlly, "Ally Base Stat Modifiers Total");
            OffsetNHpEnemy = new(GlobalData, RandomizerDistribution.OffsetNHpEnemy, "Enemy Normal HP Base Stat Modifiers");
            OffsetNStrEnemy = new(GlobalData, RandomizerDistribution.OffsetNStrEnemy, "Enemy Normal Strength Base Stat Modifiers");
            OffsetNTechEnemy = new(GlobalData, RandomizerDistribution.OffsetNTechEnemy, "Enemy Normal Dexterity Base Stat Modifiers");
            OffsetNQuickEnemy = new(GlobalData, RandomizerDistribution.OffsetNQuickEnemy, "Enemy Normal Speed Base Stat Modifiers");
            OffsetNLuckEnemy = new(GlobalData, RandomizerDistribution.OffsetNLuckEnemy, "Enemy Normal Luck Base Stat Modifiers");
            OffsetNDefEnemy = new(GlobalData, RandomizerDistribution.OffsetNDefEnemy, "Enemy Normal Defense Base Stat Modifiers");
            OffsetNMagicEnemy = new(GlobalData, RandomizerDistribution.OffsetNMagicEnemy, "Enemy Normal Magic Base Stat Modifiers");
            OffsetNMdefEnemy = new(GlobalData, RandomizerDistribution.OffsetNMdefEnemy, "Enemy Normal Resistance Base Stat Modifiers");
            OffsetNPhysEnemy = new(GlobalData, RandomizerDistribution.OffsetNPhysEnemy, "Enemy Normal Build Base Stat Modifiers");
            OffsetNSightEnemy = new(GlobalData, RandomizerDistribution.OffsetNSightEnemy, "Enemy Normal Sight Base Stat Modifiers");
            OffsetNMoveEnemy = new(GlobalData, RandomizerDistribution.OffsetNMoveEnemy, "Enemy Normal Movement Base Stat Modifiers");
            OffsetNTotalEnemy = new(GlobalData, RandomizerDistribution.OffsetNTotalEnemy, "Enemy Normal Base Stat Modifiers Total");
            OffsetHHpEnemy = new(GlobalData, RandomizerDistribution.OffsetHHpEnemy, "Enemy Hard HP Base Stat Modifiers");
            OffsetHStrEnemy = new(GlobalData, RandomizerDistribution.OffsetHStrEnemy, "Enemy Hard Strength Base Stat Modifiers");
            OffsetHTechEnemy = new(GlobalData, RandomizerDistribution.OffsetHTechEnemy, "Enemy Hard Dexterity Base Stat Modifiers");
            OffsetHQuickEnemy = new(GlobalData, RandomizerDistribution.OffsetHQuickEnemy, "Enemy Hard Speed Base Stat Modifiers");
            OffsetHLuckEnemy = new(GlobalData, RandomizerDistribution.OffsetHLuckEnemy, "Enemy Hard Luck Base Stat Modifiers");
            OffsetHDefEnemy = new(GlobalData, RandomizerDistribution.OffsetHDefEnemy, "Enemy Hard Defense Base Stat Modifiers");
            OffsetHMagicEnemy = new(GlobalData, RandomizerDistribution.OffsetHMagicEnemy, "Enemy Hard Magic Base Stat Modifiers");
            OffsetHMdefEnemy = new(GlobalData, RandomizerDistribution.OffsetHMdefEnemy, "Enemy Hard Resistance Base Stat Modifiers");
            OffsetHPhysEnemy = new(GlobalData, RandomizerDistribution.OffsetHPhysEnemy, "Enemy Hard Build Base Stat Modifiers");
            OffsetHSightEnemy = new(GlobalData, RandomizerDistribution.OffsetHSightEnemy, "Enemy Hard Sight Base Stat Modifiers");
            OffsetHMoveEnemy = new(GlobalData, RandomizerDistribution.OffsetHMoveEnemy, "Enemy Hard Movement Base Stat Modifiers");
            OffsetHTotalEnemy = new(GlobalData, RandomizerDistribution.OffsetHTotalEnemy, "Enemy Hard Base Stat Modifiers Total");
            OffsetLHpEnemy = new(GlobalData, RandomizerDistribution.OffsetLHpEnemy, "Enemy Maddening HP Base Stat Modifiers");
            OffsetLStrEnemy = new(GlobalData, RandomizerDistribution.OffsetLStrEnemy, "Enemy Maddening Strength Base Stat Modifiers");
            OffsetLTechEnemy = new(GlobalData, RandomizerDistribution.OffsetLTechEnemy, "Enemy Maddening Dexterity Base Stat Modifiers");
            OffsetLQuickEnemy = new(GlobalData, RandomizerDistribution.OffsetLQuickEnemy, "Enemy Maddening Speed Base Stat Modifiers");
            OffsetLLuckEnemy = new(GlobalData, RandomizerDistribution.OffsetLLuckEnemy, "Enemy Maddening Luck Base Stat Modifiers");
            OffsetLDefEnemy = new(GlobalData, RandomizerDistribution.OffsetLDefEnemy, "Enemy Maddening Defense Base Stat Modifiers");
            OffsetLMagicEnemy = new(GlobalData, RandomizerDistribution.OffsetLMagicEnemy, "Enemy Maddening Magic Base Stat Modifiers");
            OffsetLMdefEnemy = new(GlobalData, RandomizerDistribution.OffsetLMdefEnemy, "Enemy Maddening Resistance Base Stat Modifiers");
            OffsetLPhysEnemy = new(GlobalData, RandomizerDistribution.OffsetLPhysEnemy, "Enemy Maddening Build Base Stat Modifiers");
            OffsetLSightEnemy = new(GlobalData, RandomizerDistribution.OffsetLSightEnemy, "Enemy Maddening Sight Base Stat Modifiers");
            OffsetLMoveEnemy = new(GlobalData, RandomizerDistribution.OffsetLMoveEnemy, "Enemy Maddening Movement Base Stat Modifiers");
            OffsetLTotalEnemy = new(GlobalData, RandomizerDistribution.OffsetLTotalEnemy, "Enemy Maddening Base Stat Modifiers Total");
            LimitHp = new(GlobalData, RandomizerDistribution.LimitHp, "HP Stat Limit Modifiers");
            LimitStr = new(GlobalData, RandomizerDistribution.LimitStr, "Strength Stat Limit Modifiers");
            LimitTech = new(GlobalData, RandomizerDistribution.LimitTech, "Dexterity Stat Limit Modifiers");
            LimitQuick = new(GlobalData, RandomizerDistribution.LimitQuick, "Speed Stat Limit Modifiers");
            LimitLuck = new(GlobalData, RandomizerDistribution.LimitLuck, "Luck Stat Limit Modifiers");
            LimitDef = new(GlobalData, RandomizerDistribution.LimitDef, "Defense Stat Limit Modifiers");
            LimitMagic = new(GlobalData, RandomizerDistribution.LimitMagic, "Magic Stat Limit Modifiers");
            LimitMdef = new(GlobalData, RandomizerDistribution.LimitMdef, "Resistance Stat Limit Modifiers");
            LimitPhys = new(GlobalData, RandomizerDistribution.LimitPhys, "Build Stat Limit Modifiers");
            LimitSight = new(GlobalData, RandomizerDistribution.LimitSight, "Sight Stat Limit Modifiers");
            LimitMove = new(GlobalData, RandomizerDistribution.LimitMove, "Movement Stat Limit Modifiers");
            GrowHp = new(GlobalData, RandomizerDistribution.GrowHp, "HP Stat Growths");
            GrowStr = new(GlobalData, RandomizerDistribution.GrowStr, "Strength Stat Growths");
            GrowTech = new(GlobalData, RandomizerDistribution.GrowTech, "Dexterity Stat Growths");
            GrowQuick = new(GlobalData, RandomizerDistribution.GrowQuick, "Speed Stat Growths");
            GrowLuck = new(GlobalData, RandomizerDistribution.GrowLuck, "Luck Stat Growths");
            GrowDef = new(GlobalData, RandomizerDistribution.GrowDef, "Defense Stat Growths");
            GrowMagic = new(GlobalData, RandomizerDistribution.GrowMagic, "Magic Stat Growths");
            GrowMdef = new(GlobalData, RandomizerDistribution.GrowMdef, "Resistance Stat Growths");
            GrowPhys = new(GlobalData, RandomizerDistribution.GrowPhys, "Build Stat Growths");
            GrowSight = new(GlobalData, RandomizerDistribution.GrowSight, "Sight Stat Growths");
            GrowMove = new(GlobalData, RandomizerDistribution.GrowMove, "Movement Stat Growths");
            GrowTotal = new(GlobalData, RandomizerDistribution.GrowTotal, "Stat Growth Totals");
            ItemsWeapons = new(GlobalData, RandomizerDistribution.ItemsWeapons, "Static Starting Weapons");
            ItemsWeaponCount = new(GlobalData, RandomizerDistribution.ItemsWeapons, "Static Starting Weapons");
            ItemsItems = new(GlobalData, RandomizerDistribution.ItemsItems, "Static Starting Items");
            ItemsItemCount = new(GlobalData, RandomizerDistribution.ItemsItems, "Static Starting Items");
            AttrsAlly = new(GlobalData, RandomizerDistribution.AttrsAlly, "Ally Character Attributes");
            AttrsAllyCount = new(GlobalData, RandomizerDistribution.AttrsAlly, "Ally Character Attributes");
            AttrsEnemy = new(GlobalData, RandomizerDistribution.AttrsEnemy, "Enemy Character Attributes");
            AttrsEnemyCount = new(GlobalData, RandomizerDistribution.AttrsEnemy, "Enemy Character Attributes");
            CommonSids = new(GlobalData, RandomizerDistribution.CommonSids, "Personal Skills");
            CommonSidsCount = new(GlobalData, RandomizerDistribution.CommonSids, "Personal Skills");
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button59_Click(object sender, EventArgs e)
        {
            JidAlly.Show();
            JidAlly.Activate();
        }

        private void Button71_Click(object sender, EventArgs e)
        {
            JidEnemy.Show();
            JidEnemy.Activate();
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

        private void Button70_Click(object sender, EventArgs e)
        {
            LimitHp.Show();
            LimitHp.Activate();
        }

        private void Button69_Click(object sender, EventArgs e)
        {
            LimitStr.Show();
            LimitStr.Activate();
        }

        private void Button68_Click(object sender, EventArgs e)
        {
            LimitTech.Show();
            LimitTech.Activate();
        }

        private void Button67_Click(object sender, EventArgs e)
        {
            LimitQuick.Show();
            LimitQuick.Activate();
        }

        private void Button66_Click(object sender, EventArgs e)
        {
            LimitLuck.Show();
            LimitLuck.Activate();
        }

        private void Button65_Click(object sender, EventArgs e)
        {
            LimitDef.Show();
            LimitDef.Activate();
        }

        private void Button64_Click(object sender, EventArgs e)
        {
            LimitMagic.Show();
            LimitMagic.Activate();
        }

        private void Button63_Click(object sender, EventArgs e)
        {
            LimitMdef.Show();
            LimitMdef.Activate();
        }

        private void Button62_Click(object sender, EventArgs e)
        {
            LimitPhys.Show();
            LimitPhys.Activate();
        }

        private void Button61_Click(object sender, EventArgs e)
        {
            LimitSight.Show();
            LimitSight.Activate();
        }

        private void Button60_Click(object sender, EventArgs e)
        {
            LimitMove.Show();
            LimitMove.Activate();
        }

        private void Button83_Click(object sender, EventArgs e)
        {
            GrowHp.Show();
            GrowHp.Activate();
        }

        private void Button82_Click(object sender, EventArgs e)
        {
            GrowStr.Show();
            GrowStr.Activate();
        }

        private void Button81_Click(object sender, EventArgs e)
        {
            GrowTech.Show();
            GrowTech.Activate();
        }

        private void Button80_Click(object sender, EventArgs e)
        {
            GrowQuick.Show();
            GrowQuick.Activate();
        }

        private void Button79_Click(object sender, EventArgs e)
        {
            GrowLuck.Show();
            GrowLuck.Activate();
        }

        private void Button78_Click(object sender, EventArgs e)
        {
            GrowDef.Show();
            GrowDef.Activate();
        }

        private void Button77_Click(object sender, EventArgs e)
        {
            GrowMagic.Show();
            GrowMagic.Activate();
        }

        private void Button76_Click(object sender, EventArgs e)
        {
            GrowMdef.Show();
            GrowMdef.Activate();
        }

        private void Button75_Click(object sender, EventArgs e)
        {
            GrowPhys.Show();
            GrowPhys.Activate();
        }

        private void Button74_Click(object sender, EventArgs e)
        {
            GrowSight.Show();
            GrowSight.Activate();
        }

        private void Button73_Click(object sender, EventArgs e)
        {
            GrowMove.Show();
            GrowMove.Activate();
        }

        private void Button72_Click(object sender, EventArgs e)
        {
            GrowTotal.Show();
            GrowTotal.Activate();
        }

        private void Button84_Click(object sender, EventArgs e)
        {
            ItemsWeapons.Show();
            ItemsWeapons.Activate();
        }

        private void Button86_Click(object sender, EventArgs e)
        {
            ItemsWeaponCount.Show();
            ItemsWeaponCount.Activate();
        }

        private void Button85_Click(object sender, EventArgs e)
        {
            ItemsItems.Show();
            ItemsItems.Activate();
        }

        private void Button87_Click(object sender, EventArgs e)
        {
            ItemsItemCount.Show();
            ItemsItemCount.Activate();
        }

        private void Button89_Click(object sender, EventArgs e)
        {
            AttrsAlly.Show();
            AttrsAlly.Activate();
        }

        private void Button88_Click(object sender, EventArgs e)
        {
            AttrsAllyCount.Show();
            AttrsAllyCount.Activate();
        }

        private void Button91_Click(object sender, EventArgs e)
        {
            AttrsEnemy.Show();
            AttrsEnemy.Activate();
        }

        private void Button90_Click(object sender, EventArgs e)
        {
            AttrsEnemyCount.Show();
            AttrsEnemyCount.Activate();
        }

        private void Button93_Click(object sender, EventArgs e)
        {
            CommonSids.Show();
            CommonSids.Activate();
        }

        private void Button92_Click(object sender, EventArgs e)
        {
            CommonSidsCount.Show();
            CommonSidsCount.Activate();
        }
    }
}
