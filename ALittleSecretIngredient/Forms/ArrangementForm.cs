namespace ALittleSecretIngredient.Forms
{
    public partial class ArrangementForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm DeploymentSlots { get; }
        internal NumericDistributionForm EnemyCount { get; }
        internal ArrangementForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            DeploymentSlots = new(GlobalData, RandomizerDistribution.DeploymentSlots, "Deployment Slots Per Map");
            EnemyCount = new(GlobalData, RandomizerDistribution.EnemyCount, "Number of Enemies Per Map");
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
    }
}
