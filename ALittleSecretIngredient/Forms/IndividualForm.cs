namespace ALittleSecretIngredient.Forms
{
    public partial class IndividualForm : Form
    {
        private GlobalData GlobalData { get; }
        internal NumericDistributionForm Age { get; set; }
        internal IndividualForm(GlobalData globalData)
        {
            GlobalData = globalData;
            Age = new(GlobalData, RandomizerDistribution.Age, "Character Age");
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Age.Show();
            Age.Activate();
        }
    }
}
