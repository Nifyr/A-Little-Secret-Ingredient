#pragma warning disable IDE0052
namespace ALittleSecretIngredient.Forms
{
    public partial class AssetTableForm : Form
    {
        private GlobalData GlobalData { get; set; }
        internal AssetTableForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }
    }
}
