namespace ALittleSecretIngredient.Forms
{
    public partial class ArrangementForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm DeploymentSlots { get; }
        internal NumericDistributionForm EnemyCount { get; }
        internal NumericDistributionForm ForcedDeployment { get; }
        internal SelectionDistributionForm ItemsWeaponsAlly { get; }
        internal NumericDistributionForm ItemsWeaponsAllyCount { get; }
        internal SelectionDistributionForm ItemsItemsAlly { get; }
        internal NumericDistributionForm ItemsItemsAllyCount { get; }
        internal SelectionDistributionForm ItemsWeaponsEnemy { get; }
        internal NumericDistributionForm ItemsWeaponsEnemyCount { get; }
        internal SelectionDistributionForm ItemsItemsEnemy { get; }
        internal NumericDistributionForm ItemsItemsEnemyCount { get; }
        internal SelectionDistributionForm Sid { get; }
        internal SelectionDistributionForm Gid { get; }
        internal NumericDistributionForm HpStockCount { get; }
        internal ArrangementForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            DeploymentSlots = new(GlobalData, RandomizerDistribution.DeploymentSlots, "Deployment Slots");
            EnemyCount = new(GlobalData, RandomizerDistribution.EnemyCount, "Enemy Count");
            ForcedDeployment = new(GlobalData, RandomizerDistribution.ForcedDeployment, "Forced Deployments");
            ItemsWeaponsAlly = new(GlobalData, RandomizerDistribution.ItemsWeaponsAlly, "Starting Weapons");
            ItemsWeaponsAllyCount = new(GlobalData, RandomizerDistribution.ItemsWeaponsAlly, "Starting Weapons");
            ItemsItemsAlly = new(GlobalData, RandomizerDistribution.ItemsItemsAlly, "Starting Items");
            ItemsItemsAllyCount = new(GlobalData, RandomizerDistribution.ItemsItemsAlly, "Starting Items");
            ItemsWeaponsEnemy = new(GlobalData, RandomizerDistribution.ItemsWeaponsEnemy, "Enemy Weapons");
            ItemsWeaponsEnemyCount = new(GlobalData, RandomizerDistribution.ItemsWeaponsEnemy, "Enemy Weapons");
            ItemsItemsEnemy = new(GlobalData, RandomizerDistribution.ItemsItemsEnemy, "Enemy Items");
            ItemsItemsEnemyCount = new(GlobalData, RandomizerDistribution.ItemsItemsEnemy, "Enemy Items");
            Sid = new(GlobalData, RandomizerDistribution.Sid, "Extra Enemy Skill");
            Gid = new(GlobalData, RandomizerDistribution.Gid, "Enemy Emblem");
            HpStockCount = new(GlobalData, RandomizerDistribution.HpStockCount, "Revival Stones");
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DeploymentSlots.Show();
            DeploymentSlots.Activate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            EnemyCount.Show();
            EnemyCount.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ForcedDeployment.Show();
            ForcedDeployment.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ItemsWeaponsAlly.Show();
            ItemsWeaponsAlly.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ItemsWeaponsAllyCount.Show();
            ItemsWeaponsAllyCount.Activate();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ItemsItemsAlly.Show();
            ItemsItemsAlly.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ItemsItemsAllyCount.Show();
            ItemsItemsAllyCount.Activate();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            ItemsWeaponsEnemy.Show();
            ItemsWeaponsEnemy.Activate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            ItemsWeaponsEnemyCount.Show();
            ItemsWeaponsEnemyCount.Activate();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            ItemsItemsEnemy.Show();
            ItemsItemsEnemy.Activate();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ItemsItemsEnemyCount.Show();
            ItemsItemsEnemyCount.Activate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Sid.Show();
            Sid.Activate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Gid.Show();
            Gid.Activate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            HpStockCount.Show();
            HpStockCount.Activate();
        }
    }
}
