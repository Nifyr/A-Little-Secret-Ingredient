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
    }
}
