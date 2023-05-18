#pragma warning disable IDE0052
namespace ALittleSecretIngredient.Forms
{
    public partial class AssetTableForm : Form
    {
        private GlobalData GlobalData { get; set; }
        internal NumericDistributionForm ScaleAll { get; set; }
        internal NumericDistributionForm ScaleHead { get; set; }
        internal NumericDistributionForm ScaleNeck { get; set; }
        internal NumericDistributionForm ScaleTorso { get; set; }
        internal NumericDistributionForm ScaleShoulders { get; set; }
        internal NumericDistributionForm ScaleArms { get; set; }
        internal NumericDistributionForm ScaleHands { get; set; }
        internal NumericDistributionForm ScaleLegs { get; set; }
        internal NumericDistributionForm ScaleFeet { get; set; }
        internal NumericDistributionForm VolumeArms { get; set; }
        internal NumericDistributionForm VolumeLegs { get; set; }
        internal NumericDistributionForm VolumeBust { get; set; }
        internal NumericDistributionForm VolumeAbdomen { get; set; }
        internal NumericDistributionForm VolumeTorso { get; set; }
        internal NumericDistributionForm VolumeScaleArms { get; set; }
        internal NumericDistributionForm VolumeScaleLegs { get; set; }
        internal NumericDistributionForm MapScaleAll { get; set; }
        internal NumericDistributionForm MapScaleHead { get; set; }
        internal NumericDistributionForm MapScaleWing { get; set; }
        internal AssetTableForm(GlobalData globalData)
        {
            GlobalData = globalData;
            ScaleAll = new(globalData, RandomizerDistribution.ScaleAll, "Scale All");
            ScaleHead = new(globalData, RandomizerDistribution.ScaleHead, "Scale Head");
            ScaleNeck = new(globalData, RandomizerDistribution.ScaleNeck, "Scale Neck");
            ScaleTorso = new(globalData, RandomizerDistribution.ScaleTorso, "Scale Torso");
            ScaleShoulders = new(globalData, RandomizerDistribution.ScaleShoulders, "Scale Shoulders");
            ScaleArms = new(globalData, RandomizerDistribution.ScaleArms, "Scale Arms");
            ScaleHands = new(globalData, RandomizerDistribution.ScaleHands, "Scale Hands");
            ScaleLegs = new(globalData, RandomizerDistribution.ScaleLegs, "Scale Legs");
            ScaleFeet = new(globalData, RandomizerDistribution.ScaleFeet, "Scale Feet");
            VolumeArms = new(globalData, RandomizerDistribution.VolumeArms, "Volume Arms");
            VolumeLegs = new(globalData, RandomizerDistribution.VolumeLegs, "Volume Legs");
            VolumeBust = new(globalData, RandomizerDistribution.VolumeBust, "Volume Bust");
            VolumeAbdomen = new(globalData, RandomizerDistribution.VolumeAbdomen, "Volume Abdomen");
            VolumeTorso = new(globalData, RandomizerDistribution.VolumeTorso, "Volume Torso");
            VolumeScaleArms = new(globalData, RandomizerDistribution.VolumeScaleArms, "Volume Scale Arms");
            VolumeScaleLegs = new(globalData, RandomizerDistribution.VolumeScaleLegs, "Volume Scale Legs");
            MapScaleAll = new(globalData, RandomizerDistribution.MapScaleAll, "Map Scale All");
            MapScaleHead = new(globalData, RandomizerDistribution.MapScaleHead, "Map Scale Head");
            MapScaleWing = new(globalData, RandomizerDistribution.MapScaleWing, "Map Scale Wing");
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            ScaleAll.Show();
            ScaleAll.Activate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ScaleHead.Show();
            ScaleHead.Activate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ScaleNeck.Show();
            ScaleNeck.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ScaleTorso.Show();
            ScaleTorso.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ScaleShoulders.Show();
            ScaleShoulders.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ScaleArms.Show();
            ScaleArms.Activate();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ScaleHands.Show();
            ScaleHands.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ScaleLegs.Show();
            ScaleLegs.Activate();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ScaleFeet.Show();
            ScaleFeet.Activate();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            VolumeArms.Show();
            VolumeArms.Activate();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            VolumeLegs.Show();
            VolumeLegs.Activate();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            VolumeBust.Show();
            VolumeBust.Activate();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            VolumeAbdomen.Show();
            VolumeAbdomen.Activate();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            VolumeTorso.Show();
            VolumeTorso.Activate();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            VolumeScaleArms.Show();
            VolumeScaleArms.Activate();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            VolumeScaleLegs.Show();
            VolumeScaleLegs.Activate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            MapScaleAll.Show();
            MapScaleAll.Activate();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            MapScaleHead.Show();
            MapScaleHead.Activate();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            MapScaleWing.Show();
            MapScaleWing.Activate();
        }
    }
}
