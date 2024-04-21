namespace ALittleSecretIngredient.Forms
{
    public partial class ArrangementForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm DeploymentSlots { get; }
        internal ArrangementForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            DeploymentSlots = new(GlobalData, RandomizerDistribution.DeploymentSlots, "Deployment Slots Per Map");
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DeploymentSlots.Show();
            DeploymentSlots.Activate();
        }
    }
}
