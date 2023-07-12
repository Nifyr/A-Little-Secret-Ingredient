namespace ALittleSecretIngredient.Forms
{
    public partial class TypeOfSoldierForm : Form
    {
        private GlobalData GlobalData { get; }
        internal SelectionDistributionForm StyleName { get; }
        internal SelectionDistributionForm MoveType { get; }
        internal SelectionDistributionForm Weapon { get; }
        internal NumericDistributionForm WeaponBaseCount { get; }
        internal NumericDistributionForm WeaponAdvancedCount { get; }
        internal NumericDistributionForm MaxWeaponLevelBase { get; }
        internal NumericDistributionForm MaxWeaponLevelAdvanced { get; }
        internal NumericDistributionForm BaseHpBase { get; }
        internal NumericDistributionForm BaseStrBase { get; }
        internal NumericDistributionForm BaseTechBase { get; }
        internal NumericDistributionForm BaseQuickBase { get; }
        internal NumericDistributionForm BaseLuckBase { get; }
        internal NumericDistributionForm BaseDefBase { get; }
        internal NumericDistributionForm BaseMagicBase { get; }
        internal NumericDistributionForm BaseMdefBase { get; }
        internal NumericDistributionForm BasePhysBase { get; }
        internal NumericDistributionForm BaseSightBase { get; }
        internal NumericDistributionForm BaseMoveBase { get; }
        internal NumericDistributionForm BaseHpAdvanced { get; }
        internal NumericDistributionForm BaseStrAdvanced { get; }
        internal NumericDistributionForm BaseTechAdvanced { get; }
        internal NumericDistributionForm BaseQuickAdvanced { get; }
        internal NumericDistributionForm BaseLuckAdvanced { get; }
        internal NumericDistributionForm BaseDefAdvanced { get; }
        internal NumericDistributionForm BaseMagicAdvanced { get; }
        internal NumericDistributionForm BaseMdefAdvanced { get; }
        internal NumericDistributionForm BasePhysAdvanced { get; }
        internal NumericDistributionForm BaseSightAdvanced { get; }
        internal NumericDistributionForm BaseMoveAdvanced { get; }
        internal NumericDistributionForm BaseTotalBase { get; }
        internal NumericDistributionForm BaseTotalAdvanced { get; }
        internal NumericDistributionForm LimitHpBase { get; }
        internal NumericDistributionForm LimitStrBase { get; }
        internal NumericDistributionForm LimitTechBase { get; }
        internal NumericDistributionForm LimitQuickBase { get; }
        internal NumericDistributionForm LimitLuckBase { get; }
        internal NumericDistributionForm LimitDefBase { get; }
        internal NumericDistributionForm LimitMagicBase { get; }
        internal NumericDistributionForm LimitMdefBase { get; }
        internal NumericDistributionForm LimitPhysBase { get; }
        internal NumericDistributionForm LimitSightBase { get; }
        internal NumericDistributionForm LimitMoveBase { get; }
        internal NumericDistributionForm LimitHpAdvanced { get; }
        internal NumericDistributionForm LimitStrAdvanced { get; }
        internal NumericDistributionForm LimitTechAdvanced { get; }
        internal NumericDistributionForm LimitQuickAdvanced { get; }
        internal NumericDistributionForm LimitLuckAdvanced { get; }
        internal NumericDistributionForm LimitDefAdvanced { get; }
        internal NumericDistributionForm LimitMagicAdvanced { get; }
        internal NumericDistributionForm LimitMdefAdvanced { get; }
        internal NumericDistributionForm LimitPhysAdvanced { get; }
        internal NumericDistributionForm LimitSightAdvanced { get; }
        internal NumericDistributionForm LimitMoveAdvanced { get; }
        internal NumericDistributionForm LimitTotalBase { get; }
        internal NumericDistributionForm LimitTotalAdvanced { get; }
        internal NumericDistributionForm BaseGrowHpBase { get; }
        internal NumericDistributionForm BaseGrowStrBase { get; }
        internal NumericDistributionForm BaseGrowTechBase { get; }
        internal NumericDistributionForm BaseGrowQuickBase { get; }
        internal NumericDistributionForm BaseGrowLuckBase { get; }
        internal NumericDistributionForm BaseGrowDefBase { get; }
        internal NumericDistributionForm BaseGrowMagicBase { get; }
        internal NumericDistributionForm BaseGrowMdefBase { get; }
        internal NumericDistributionForm BaseGrowPhysBase { get; }
        internal NumericDistributionForm BaseGrowSightBase { get; }
        internal NumericDistributionForm BaseGrowMoveBase { get; }
        internal NumericDistributionForm BaseGrowHpAdvanced { get; }
        internal NumericDistributionForm BaseGrowStrAdvanced { get; }
        internal NumericDistributionForm BaseGrowTechAdvanced { get; }
        internal NumericDistributionForm BaseGrowQuickAdvanced { get; }
        internal NumericDistributionForm BaseGrowLuckAdvanced { get; }
        internal NumericDistributionForm BaseGrowDefAdvanced { get; }
        internal NumericDistributionForm BaseGrowMagicAdvanced { get; }
        internal NumericDistributionForm BaseGrowMdefAdvanced { get; }
        internal NumericDistributionForm BaseGrowPhysAdvanced { get; }
        internal NumericDistributionForm BaseGrowSightAdvanced { get; }
        internal NumericDistributionForm BaseGrowMoveAdvanced { get; }
        internal NumericDistributionForm BaseGrowTotalBase { get; }
        internal NumericDistributionForm BaseGrowTotalAdvanced { get; }
        internal NumericDistributionForm DiffGrowHpBase { get; }
        internal NumericDistributionForm DiffGrowStrBase { get; }
        internal NumericDistributionForm DiffGrowTechBase { get; }
        internal NumericDistributionForm DiffGrowQuickBase { get; }
        internal NumericDistributionForm DiffGrowLuckBase { get; }
        internal NumericDistributionForm DiffGrowDefBase { get; }
        internal NumericDistributionForm DiffGrowMagicBase { get; }
        internal NumericDistributionForm DiffGrowMdefBase { get; }
        internal NumericDistributionForm DiffGrowPhysBase { get; }
        internal NumericDistributionForm DiffGrowSightBase { get; }
        internal NumericDistributionForm DiffGrowMoveBase { get; }
        internal NumericDistributionForm DiffGrowHpAdvanced { get; }
        internal NumericDistributionForm DiffGrowStrAdvanced { get; }
        internal NumericDistributionForm DiffGrowTechAdvanced { get; }
        internal NumericDistributionForm DiffGrowQuickAdvanced { get; }
        internal NumericDistributionForm DiffGrowLuckAdvanced { get; }
        internal NumericDistributionForm DiffGrowDefAdvanced { get; }
        internal NumericDistributionForm DiffGrowMagicAdvanced { get; }
        internal NumericDistributionForm DiffGrowMdefAdvanced { get; }
        internal NumericDistributionForm DiffGrowPhysAdvanced { get; }
        internal NumericDistributionForm DiffGrowSightAdvanced { get; }
        internal NumericDistributionForm DiffGrowMoveAdvanced { get; }
        internal NumericDistributionForm DiffGrowTotalBase { get; }
        internal NumericDistributionForm DiffGrowTotalAdvanced { get; }
        internal SelectionDistributionForm LearningSkill { get; }
        internal SelectionDistributionForm LunaticSkill { get; }
        internal TypeOfSoldierForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            StyleName = new(GlobalData, RandomizerDistribution.StyleName, "Unit Types");
            MoveType = new(GlobalData, RandomizerDistribution.MoveType, "Movement Types");
            Weapon = new(GlobalData, RandomizerDistribution.Weapon, "Weapon Types");
            WeaponBaseCount = new(GlobalData, RandomizerDistribution.WeaponBaseCount, "Base Class Weapon Type Count");
            WeaponAdvancedCount = new(GlobalData, RandomizerDistribution.WeaponAdvancedCount, "Advanced Class Weapon Type Count");
            MaxWeaponLevelBase = new(GlobalData, RandomizerDistribution.MaxWeaponLevelBase, "Base Class Weapon Rank");
            MaxWeaponLevelAdvanced = new(GlobalData, RandomizerDistribution.MaxWeaponLevelAdvanced, "Advanced Class Weapon Rank");
            BaseHpBase = new(GlobalData, RandomizerDistribution.BaseHpBase, "Base Class HP Base");
            BaseStrBase = new(GlobalData, RandomizerDistribution.BaseStrBase, "Base Class Strength Base");
            BaseTechBase = new(GlobalData, RandomizerDistribution.BaseTechBase, "Base Class Dexterity Base");
            BaseQuickBase = new(GlobalData, RandomizerDistribution.BaseQuickBase, "Base Class Speed Base");
            BaseLuckBase = new(GlobalData, RandomizerDistribution.BaseLuckBase, "Base Class Luck Base");
            BaseDefBase = new(GlobalData, RandomizerDistribution.BaseDefBase, "Base Class Defense Base");
            BaseMagicBase = new(GlobalData, RandomizerDistribution.BaseMagicBase, "Base Class Magic Base");
            BaseMdefBase = new(GlobalData, RandomizerDistribution.BaseMdefBase, "Base Class Resistance Base");
            BasePhysBase = new(GlobalData, RandomizerDistribution.BasePhysBase, "Base Class Build Base");
            BaseSightBase = new(GlobalData, RandomizerDistribution.BaseSightBase, "Base Class Sight Base");
            BaseMoveBase = new(GlobalData, RandomizerDistribution.BaseMoveBase, "Base Class Movement Base");
            BaseHpAdvanced = new(GlobalData, RandomizerDistribution.BaseHpAdvanced, "Advanced Class HP Base");
            BaseStrAdvanced = new(GlobalData, RandomizerDistribution.BaseStrAdvanced, "Advanced Class Strength Base");
            BaseTechAdvanced = new(GlobalData, RandomizerDistribution.BaseTechAdvanced, "Advanced Class Dexterity Base");
            BaseQuickAdvanced = new(GlobalData, RandomizerDistribution.BaseQuickAdvanced, "Advanced Class Speed Base");
            BaseLuckAdvanced = new(GlobalData, RandomizerDistribution.BaseLuckAdvanced, "Advanced Class Luck Base");
            BaseDefAdvanced = new(GlobalData, RandomizerDistribution.BaseDefAdvanced, "Advanced Class Defense Base");
            BaseMagicAdvanced = new(GlobalData, RandomizerDistribution.BaseMagicAdvanced, "Advanced Class Magic Base");
            BaseMdefAdvanced = new(GlobalData, RandomizerDistribution.BaseMdefAdvanced, "Advanced Class Resistance Base");
            BasePhysAdvanced = new(GlobalData, RandomizerDistribution.BasePhysAdvanced, "Advanced Class Build Base");
            BaseSightAdvanced = new(GlobalData, RandomizerDistribution.BaseSightAdvanced, "Advanced Class Sight Base");
            BaseMoveAdvanced = new(GlobalData, RandomizerDistribution.BaseMoveAdvanced, "Advanced Class Movement Base");
            BaseTotalBase = new(GlobalData, RandomizerDistribution.BaseTotalBase, "Base Class Base Stat Total");
            BaseTotalAdvanced = new(GlobalData, RandomizerDistribution.BaseTotalAdvanced, "Advanced Class Base Stat Total");
            LimitHpBase = new(GlobalData, RandomizerDistribution.LimitHpBase, "Base Class HP Limit");
            LimitStrBase = new(GlobalData, RandomizerDistribution.LimitStrBase, "Base Class Strength Limit");
            LimitTechBase = new(GlobalData, RandomizerDistribution.LimitTechBase, "Base Class Dexterity Limit");
            LimitQuickBase = new(GlobalData, RandomizerDistribution.LimitQuickBase, "Base Class Speed Limit");
            LimitLuckBase = new(GlobalData, RandomizerDistribution.LimitLuckBase, "Base Class Luck Limit");
            LimitDefBase = new(GlobalData, RandomizerDistribution.LimitDefBase, "Base Class Defense Limit");
            LimitMagicBase = new(GlobalData, RandomizerDistribution.LimitMagicBase, "Base Class Magic Limit");
            LimitMdefBase = new(GlobalData, RandomizerDistribution.LimitMdefBase, "Base Class Resistance Limit");
            LimitPhysBase = new(GlobalData, RandomizerDistribution.LimitPhysBase, "Base Class Build Limit");
            LimitSightBase = new(GlobalData, RandomizerDistribution.LimitSightBase, "Base Class Sight Limit");
            LimitMoveBase = new(GlobalData, RandomizerDistribution.LimitMoveBase, "Base Class Movement Limit");
            LimitHpAdvanced = new(GlobalData, RandomizerDistribution.LimitHpAdvanced, "Advanced Class HP Limit");
            LimitStrAdvanced = new(GlobalData, RandomizerDistribution.LimitStrAdvanced, "Advanced Class Strength Limit");
            LimitTechAdvanced = new(GlobalData, RandomizerDistribution.LimitTechAdvanced, "Advanced Class Dexterity Limit");
            LimitQuickAdvanced = new(GlobalData, RandomizerDistribution.LimitQuickAdvanced, "Advanced Class Speed Limit");
            LimitLuckAdvanced = new(GlobalData, RandomizerDistribution.LimitLuckAdvanced, "Advanced Class Luck Limit");
            LimitDefAdvanced = new(GlobalData, RandomizerDistribution.LimitDefAdvanced, "Advanced Class Defense Limit");
            LimitMagicAdvanced = new(GlobalData, RandomizerDistribution.LimitMagicAdvanced, "Advanced Class Magic Limit");
            LimitMdefAdvanced = new(GlobalData, RandomizerDistribution.LimitMdefAdvanced, "Advanced Class Resistance Limit");
            LimitPhysAdvanced = new(GlobalData, RandomizerDistribution.LimitPhysAdvanced, "Advanced Class Build Limit");
            LimitSightAdvanced = new(GlobalData, RandomizerDistribution.LimitSightAdvanced, "Advanced Class Sight Limit");
            LimitMoveAdvanced = new(GlobalData, RandomizerDistribution.LimitMoveAdvanced, "Advanced Class Movement Limit");
            LimitTotalBase = new(GlobalData, RandomizerDistribution.LimitTotalBase, "Base Class Stat Limit Total");
            LimitTotalAdvanced = new(GlobalData, RandomizerDistribution.LimitTotalAdvanced, "Advanced Class Stat Limit Total");
            BaseGrowHpBase = new(GlobalData, RandomizerDistribution.BaseGrowHpBase, "Enemy Base Class HP Growth");
            BaseGrowStrBase = new(GlobalData, RandomizerDistribution.BaseGrowStrBase, "Enemy Base Class Strength Growth");
            BaseGrowTechBase = new(GlobalData, RandomizerDistribution.BaseGrowTechBase, "Enemy Base Class Dexterity Growth");
            BaseGrowQuickBase = new(GlobalData, RandomizerDistribution.BaseGrowQuickBase, "Enemy Base Class Speed Growth");
            BaseGrowLuckBase = new(GlobalData, RandomizerDistribution.BaseGrowLuckBase, "Enemy Base Class Luck Growth");
            BaseGrowDefBase = new(GlobalData, RandomizerDistribution.BaseGrowDefBase, "Enemy Base Class Defense Growth");
            BaseGrowMagicBase = new(GlobalData, RandomizerDistribution.BaseGrowMagicBase, "Enemy Base Class Magic Growth");
            BaseGrowMdefBase = new(GlobalData, RandomizerDistribution.BaseGrowMdefBase, "Enemy Base Class Resistance Growth");
            BaseGrowPhysBase = new(GlobalData, RandomizerDistribution.BaseGrowPhysBase, "Enemy Base Class Build Growth");
            BaseGrowSightBase = new(GlobalData, RandomizerDistribution.BaseGrowSightBase, "Enemy Base Class Sight Growth");
            BaseGrowMoveBase = new(GlobalData, RandomizerDistribution.BaseGrowMoveBase, "Enemy Base Class Movement Growth");
            BaseGrowHpAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowHpAdvanced, "Enemy Advanced Class HP Growth");
            BaseGrowStrAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowStrAdvanced, "Enemy Advanced Class Strength Growth");
            BaseGrowTechAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowTechAdvanced, "Enemy Advanced Class Dexterity Growth");
            BaseGrowQuickAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowQuickAdvanced, "Enemy Advanced Class Speed Growth");
            BaseGrowLuckAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowLuckAdvanced, "Enemy Advanced Class Luck Growth");
            BaseGrowDefAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowDefAdvanced, "Enemy Advanced Class Defense Growth");
            BaseGrowMagicAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowMagicAdvanced, "Enemy Advanced Class Magic Growth");
            BaseGrowMdefAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowMdefAdvanced, "Enemy Advanced Class Resistance Growth");
            BaseGrowPhysAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowPhysAdvanced, "Enemy Advanced Class Build Growth");
            BaseGrowSightAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowSightAdvanced, "Enemy Advanced Class Sight Growth");
            BaseGrowMoveAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowMoveAdvanced, "Enemy Advanced Class Movement Growth");
            BaseGrowTotalBase = new(GlobalData, RandomizerDistribution.BaseGrowTotalBase, "Enemy Base Class Stat Growth Total");
            BaseGrowTotalAdvanced = new(GlobalData, RandomizerDistribution.BaseGrowTotalAdvanced, "Enemy Advanced Class Stat Growth Total");
            DiffGrowHpBase = new(GlobalData, RandomizerDistribution.DiffGrowHpBase, "Base Class HP Growth Modifier");
            DiffGrowStrBase = new(GlobalData, RandomizerDistribution.DiffGrowStrBase, "Base Class Strength Growth Modifier");
            DiffGrowTechBase = new(GlobalData, RandomizerDistribution.DiffGrowTechBase, "Base Class Dexterity Growth Modifier");
            DiffGrowQuickBase = new(GlobalData, RandomizerDistribution.DiffGrowQuickBase, "Base Class Speed Growth Modifier");
            DiffGrowLuckBase = new(GlobalData, RandomizerDistribution.DiffGrowLuckBase, "Base Class Luck Growth Modifier");
            DiffGrowDefBase = new(GlobalData, RandomizerDistribution.DiffGrowDefBase, "Base Class Defense Growth Modifier");
            DiffGrowMagicBase = new(GlobalData, RandomizerDistribution.DiffGrowMagicBase, "Base Class Magic Growth Modifier");
            DiffGrowMdefBase = new(GlobalData, RandomizerDistribution.DiffGrowMdefBase, "Base Class Resistance Growth Modifier");
            DiffGrowPhysBase = new(GlobalData, RandomizerDistribution.DiffGrowPhysBase, "Base Class Build Growth Modifier");
            DiffGrowSightBase = new(GlobalData, RandomizerDistribution.DiffGrowSightBase, "Base Class Sight Growth Modifier");
            DiffGrowMoveBase = new(GlobalData, RandomizerDistribution.DiffGrowMoveBase, "Base Class Movement Growth Modifier");
            DiffGrowHpAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowHpAdvanced, "Advanced Class HP Growth Modifier");
            DiffGrowStrAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowStrAdvanced, "Advanced Class Strength Growth Modifier");
            DiffGrowTechAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowTechAdvanced, "Advanced Class Dexterity Growth Modifier");
            DiffGrowQuickAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowQuickAdvanced, "Advanced Class Speed Growth Modifier");
            DiffGrowLuckAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowLuckAdvanced, "Advanced Class Luck Growth Modifier");
            DiffGrowDefAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowDefAdvanced, "Advanced Class Defense Growth Modifier");
            DiffGrowMagicAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowMagicAdvanced, "Advanced Class Magic Growth Modifier");
            DiffGrowMdefAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowMdefAdvanced, "Advanced Class Resistance Growth Modifier");
            DiffGrowPhysAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowPhysAdvanced, "Advanced Class Build Growth Modifier");
            DiffGrowSightAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowSightAdvanced, "Advanced Class Sight Growth Modifier");
            DiffGrowMoveAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowMoveAdvanced, "Advanced Class Movement Growth Modifier");
            DiffGrowTotalBase = new(GlobalData, RandomizerDistribution.DiffGrowTotalBase, "Base Class Stat Growth Modifier Total");
            DiffGrowTotalAdvanced = new(GlobalData, RandomizerDistribution.DiffGrowTotalAdvanced, "Advanced Class Stat Growth Modifier Total");
            LearningSkill = new(GlobalData, RandomizerDistribution.LearningSkill, "Class Skills");
            LunaticSkill = new(GlobalData, RandomizerDistribution.LunaticSkill, "Maddening Enemy Skills");
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            StyleName.Show();
            StyleName.Activate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MoveType.Show();
            MoveType.Activate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Weapon.Show();
            Weapon.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            WeaponBaseCount.Show();
            WeaponBaseCount.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            WeaponAdvancedCount.Show();
            WeaponAdvancedCount.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            MaxWeaponLevelBase.Show();
            MaxWeaponLevelBase.Activate();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MaxWeaponLevelAdvanced.Show();
            MaxWeaponLevelAdvanced.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            BaseHpBase.Show();
            BaseHpBase.Activate();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            BaseStrBase.Show();
            BaseStrBase.Activate();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            BaseTechBase.Show();
            BaseTechBase.Activate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            BaseQuickBase.Show();
            BaseQuickBase.Activate();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            BaseLuckBase.Show();
            BaseLuckBase.Activate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            BaseDefBase.Show();
            BaseDefBase.Activate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            BaseMagicBase.Show();
            BaseMagicBase.Activate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            BaseMdefBase.Show();
            BaseMdefBase.Activate();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            BasePhysBase.Show();
            BasePhysBase.Activate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            BaseSightBase.Show();
            BaseSightBase.Activate();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            BaseMoveBase.Show();
            BaseMoveBase.Activate();
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            BaseHpAdvanced.Show();
            BaseHpAdvanced.Activate();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            BaseStrAdvanced.Show();
            BaseStrAdvanced.Activate();
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            BaseTechAdvanced.Show();
            BaseTechAdvanced.Activate();
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            BaseQuickAdvanced.Show();
            BaseQuickAdvanced.Activate();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            BaseLuckAdvanced.Show();
            BaseLuckAdvanced.Activate();
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            BaseDefAdvanced.Show();
            BaseDefAdvanced.Activate();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            BaseMagicAdvanced.Show();
            BaseMagicAdvanced.Activate();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            BaseMdefAdvanced.Show();
            BaseMdefAdvanced.Activate();
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            BasePhysAdvanced.Show();
            BasePhysAdvanced.Activate();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            BaseSightAdvanced.Show();
            BaseSightAdvanced.Activate();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            BaseMoveAdvanced.Show();
            BaseMoveAdvanced.Activate();
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            BaseTotalBase.Show();
            BaseTotalBase.Activate();
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            BaseTotalAdvanced.Show();
            BaseTotalAdvanced.Activate();
        }

        private void Button55_Click(object sender, EventArgs e)
        {
            LimitHpBase.Show();
            LimitHpBase.Activate();
        }

        private void Button54_Click(object sender, EventArgs e)
        {
            LimitStrBase.Show();
            LimitStrBase.Activate();
        }

        private void Button53_Click(object sender, EventArgs e)
        {
            LimitTechBase.Show();
            LimitTechBase.Activate();
        }

        private void Button52_Click(object sender, EventArgs e)
        {
            LimitQuickBase.Show();
            LimitQuickBase.Activate();
        }

        private void Button51_Click(object sender, EventArgs e)
        {
            LimitLuckBase.Show();
            LimitLuckBase.Activate();
        }

        private void Button50_Click(object sender, EventArgs e)
        {
            LimitDefBase.Show();
            LimitDefBase.Activate();
        }

        private void Button49_Click(object sender, EventArgs e)
        {
            LimitMagicBase.Show();
            LimitMagicBase.Activate();
        }

        private void Button48_Click(object sender, EventArgs e)
        {
            LimitMdefBase.Show();
            LimitMdefBase.Activate();
        }

        private void Button47_Click(object sender, EventArgs e)
        {
            LimitPhysBase.Show();
            LimitPhysBase.Activate();
        }

        private void Button46_Click(object sender, EventArgs e)
        {
            LimitSightBase.Show();
            LimitSightBase.Activate();
        }

        private void Button45_Click(object sender, EventArgs e)
        {
            LimitMoveBase.Show();
            LimitMoveBase.Activate();
        }

        private void Button44_Click(object sender, EventArgs e)
        {
            LimitHpAdvanced.Show();
            LimitHpAdvanced.Activate();
        }

        private void Button43_Click(object sender, EventArgs e)
        {
            LimitStrAdvanced.Show();
            LimitStrAdvanced.Activate();
        }

        private void Button42_Click(object sender, EventArgs e)
        {
            LimitTechAdvanced.Show();
            LimitTechAdvanced.Activate();
        }

        private void Button41_Click(object sender, EventArgs e)
        {
            LimitQuickAdvanced.Show();
            LimitQuickAdvanced.Activate();
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            LimitLuckAdvanced.Show();
            LimitLuckAdvanced.Activate();
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            LimitDefAdvanced.Show();
            LimitDefAdvanced.Activate();
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            LimitMagicAdvanced.Show();
            LimitMagicAdvanced.Activate();
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            LimitMdefAdvanced.Show();
            LimitMdefAdvanced.Activate();
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            LimitPhysAdvanced.Show();
            LimitPhysAdvanced.Activate();
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            LimitSightAdvanced.Show();
            LimitSightAdvanced.Activate();
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            LimitMoveAdvanced.Show();
            LimitMoveAdvanced.Activate();
        }

        private void Button33_Click(object sender, EventArgs e)
        {
            LimitTotalBase.Show();
            LimitTotalBase.Activate();
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            LimitTotalAdvanced.Show();
            LimitTotalAdvanced.Activate();
        }

        private void Button79_Click(object sender, EventArgs e)
        {
            BaseGrowHpBase.Show();
            BaseGrowHpBase.Activate();
        }

        private void Button78_Click(object sender, EventArgs e)
        {
            BaseGrowStrBase.Show();
            BaseGrowStrBase.Activate();
        }

        private void Button77_Click(object sender, EventArgs e)
        {
            BaseGrowTechBase.Show();
            BaseGrowTechBase.Activate();
        }

        private void Button76_Click(object sender, EventArgs e)
        {
            BaseGrowQuickBase.Show();
            BaseGrowQuickBase.Activate();
        }

        private void Button75_Click(object sender, EventArgs e)
        {
            BaseGrowLuckBase.Show();
            BaseGrowLuckBase.Activate();
        }

        private void Button74_Click(object sender, EventArgs e)
        {
            BaseGrowDefBase.Show();
            BaseGrowDefBase.Activate();
        }

        private void Button73_Click(object sender, EventArgs e)
        {
            BaseGrowMagicBase.Show();
            BaseGrowMagicBase.Activate();
        }

        private void Button72_Click(object sender, EventArgs e)
        {
            BaseGrowMdefBase.Show();
            BaseGrowMdefBase.Activate();
        }

        private void Button71_Click(object sender, EventArgs e)
        {
            BaseGrowPhysBase.Show();
            BaseGrowPhysBase.Activate();
        }

        private void Button70_Click(object sender, EventArgs e)
        {
            BaseGrowSightBase.Show();
            BaseGrowSightBase.Activate();
        }

        private void Button69_Click(object sender, EventArgs e)
        {
            BaseGrowMoveBase.Show();
            BaseGrowMoveBase.Activate();
        }

        private void Button68_Click(object sender, EventArgs e)
        {
            BaseGrowHpAdvanced.Show();
            BaseGrowHpAdvanced.Activate();
        }

        private void Button67_Click(object sender, EventArgs e)
        {
            BaseGrowStrAdvanced.Show();
            BaseGrowStrAdvanced.Activate();
        }

        private void Button66_Click(object sender, EventArgs e)
        {
            BaseGrowTechAdvanced.Show();
            BaseGrowTechAdvanced.Activate();
        }

        private void Button65_Click(object sender, EventArgs e)
        {
            BaseGrowQuickAdvanced.Show();
            BaseGrowQuickAdvanced.Activate();
        }

        private void Button64_Click(object sender, EventArgs e)
        {
            BaseGrowLuckAdvanced.Show();
            BaseGrowLuckAdvanced.Activate();
        }

        private void Button63_Click(object sender, EventArgs e)
        {
            BaseGrowDefAdvanced.Show();
            BaseGrowDefAdvanced.Activate();
        }

        private void Button62_Click(object sender, EventArgs e)
        {
            BaseGrowMagicAdvanced.Show();
            BaseGrowMagicAdvanced.Activate();
        }

        private void Button61_Click(object sender, EventArgs e)
        {
            BaseGrowMdefAdvanced.Show();
            BaseGrowMdefAdvanced.Activate();
        }

        private void Button60_Click(object sender, EventArgs e)
        {
            BaseGrowPhysAdvanced.Show();
            BaseGrowPhysAdvanced.Activate();
        }

        private void Button59_Click(object sender, EventArgs e)
        {
            BaseGrowSightAdvanced.Show();
            BaseGrowSightAdvanced.Activate();
        }

        private void Button58_Click(object sender, EventArgs e)
        {
            BaseGrowMoveAdvanced.Show();
            BaseGrowMoveAdvanced.Activate();
        }

        private void Button57_Click(object sender, EventArgs e)
        {
            BaseGrowTotalBase.Show();
            BaseGrowTotalBase.Activate();
        }

        private void Button56_Click(object sender, EventArgs e)
        {
            BaseGrowTotalAdvanced.Show();
            BaseGrowTotalAdvanced.Activate();
        }

        private void Button103_Click(object sender, EventArgs e)
        {
            DiffGrowHpBase.Show();
            DiffGrowHpBase.Activate();
        }

        private void Button102_Click(object sender, EventArgs e)
        {
            DiffGrowStrBase.Show();
            DiffGrowStrBase.Activate();
        }

        private void Button101_Click(object sender, EventArgs e)
        {
            DiffGrowTechBase.Show();
            DiffGrowTechBase.Activate();
        }

        private void Button100_Click(object sender, EventArgs e)
        {
            DiffGrowQuickBase.Show();
            DiffGrowQuickBase.Activate();
        }

        private void Button99_Click(object sender, EventArgs e)
        {
            DiffGrowLuckBase.Show();
            DiffGrowLuckBase.Activate();
        }

        private void Button98_Click(object sender, EventArgs e)
        {
            DiffGrowDefBase.Show();
            DiffGrowDefBase.Activate();
        }

        private void Button97_Click(object sender, EventArgs e)
        {
            DiffGrowMagicBase.Show();
            DiffGrowMagicBase.Activate();
        }

        private void Button96_Click(object sender, EventArgs e)
        {
            DiffGrowMdefBase.Show();
            DiffGrowMdefBase.Activate();
        }

        private void Button95_Click(object sender, EventArgs e)
        {
            DiffGrowPhysBase.Show();
            DiffGrowPhysBase.Activate();
        }

        private void Button94_Click(object sender, EventArgs e)
        {
            DiffGrowSightBase.Show();
            DiffGrowSightBase.Activate();
        }

        private void Button93_Click(object sender, EventArgs e)
        {
            DiffGrowMoveBase.Show();
            DiffGrowMoveBase.Activate();
        }

        private void Button92_Click(object sender, EventArgs e)
        {
            DiffGrowHpAdvanced.Show();
            DiffGrowHpAdvanced.Activate();
        }

        private void Button91_Click(object sender, EventArgs e)
        {
            DiffGrowStrAdvanced.Show();
            DiffGrowStrAdvanced.Activate();
        }

        private void Button90_Click(object sender, EventArgs e)
        {
            DiffGrowTechAdvanced.Show();
            DiffGrowTechAdvanced.Activate();
        }

        private void Button89_Click(object sender, EventArgs e)
        {
            DiffGrowQuickAdvanced.Show();
            DiffGrowQuickAdvanced.Activate();
        }

        private void Button88_Click(object sender, EventArgs e)
        {
            DiffGrowLuckAdvanced.Show();
            DiffGrowLuckAdvanced.Activate();
        }

        private void Button87_Click(object sender, EventArgs e)
        {
            DiffGrowDefAdvanced.Show();
            DiffGrowDefAdvanced.Activate();
        }

        private void Button86_Click(object sender, EventArgs e)
        {
            DiffGrowMagicAdvanced.Show();
            DiffGrowMagicAdvanced.Activate();
        }

        private void Button85_Click(object sender, EventArgs e)
        {
            DiffGrowMdefAdvanced.Show();
            DiffGrowMdefAdvanced.Activate();
        }

        private void Button84_Click(object sender, EventArgs e)
        {
            DiffGrowPhysAdvanced.Show();
            DiffGrowPhysAdvanced.Activate();
        }

        private void Button83_Click(object sender, EventArgs e)
        {
            DiffGrowSightAdvanced.Show();
            DiffGrowSightAdvanced.Activate();
        }

        private void Button82_Click(object sender, EventArgs e)
        {
            DiffGrowMoveAdvanced.Show();
            DiffGrowMoveAdvanced.Activate();
        }

        private void Button81_Click(object sender, EventArgs e)
        {
            DiffGrowTotalBase.Show();
            DiffGrowTotalBase.Activate();
        }

        private void Button80_Click(object sender, EventArgs e)
        {
            DiffGrowTotalAdvanced.Show();
            DiffGrowTotalAdvanced.Activate();
        }

        private void Button104_Click(object sender, EventArgs e)
        {
            LearningSkill.Show();
            LearningSkill.Activate();
        }

        private void Button105_Click(object sender, EventArgs e)
        {
            LunaticSkill.Show();
            LunaticSkill.Activate();
        }
    }
}
