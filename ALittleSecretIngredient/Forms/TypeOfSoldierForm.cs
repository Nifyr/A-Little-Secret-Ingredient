namespace ALittleSecretIngredient.Forms
{
    public partial class TypeOfSoldierForm : Form
    {
        private GlobalData GlobalData { get; }
        internal SelectionDistributionForm StyleName { get; }
        internal SelectionDistributionForm MoveType { get; }
        internal TypeOfSoldierForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            StyleName = new(GlobalData, RandomizerDistribution.StyleName, "Unit Types");
            MoveType = new(GlobalData, RandomizerDistribution.MoveType, "Movement Types");
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
    }
}
