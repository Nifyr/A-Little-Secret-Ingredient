#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{
    internal class Asset : DataParam
    {
        internal string Out { get; set; }
        internal string PresetName { get; set; }
        internal sbyte Mode { get; set; }
        internal string[] Conditions { get; set; }
        internal string BodyModel { get; set; }
        internal string DressModel { get; set; }
        internal byte MaskColor100R { get; set; }
        internal byte MaskColor100G { get; set; }
        internal byte MaskColor100B { get; set; }
        internal byte MaskColor075R { get; set; }
        internal byte MaskColor075G { get; set; }
        internal byte MaskColor075B { get; set; }
        internal byte MaskColor050R { get; set; }
        internal byte MaskColor050G { get; set; }
        internal byte MaskColor050B { get; set; }
        internal byte MaskColor025R { get; set; }
        internal byte MaskColor025G { get; set; }
        internal byte MaskColor025B { get; set; }
        internal string HeadModel { get; set; }
        internal string HairModel { get; set; }
        internal byte HairR { get; set; }
        internal byte HairG { get; set; }
        internal byte HairB { get; set; }
        internal byte GradR { get; set; }
        internal byte GradG { get; set; }
        internal byte GradB { get; set; }
        internal byte SkinR { get; set; }
        internal byte SkinG { get; set; }
        internal byte SkinB { get; set; }
        internal byte ToonR { get; set; }
        internal byte ToonG { get; set; }
        internal byte ToonB { get; set; }
        internal string RideModel { get; set; }
        internal string RideDressModel { get; set; }
        internal string LeftHand { get; set; }
        internal string RightHand { get; set; }
        internal string Trail { get; set; }
        internal string Magic { get; set; }
        internal string Acc1Locator { get; set; }
        internal string Acc1Model { get; set; }
        internal string Acc2Locator { get; set; }
        internal string Acc2Model { get; set; }
        internal string Acc3Locator { get; set; }
        internal string Acc3Model { get; set; }
        internal string Acc4Locator { get; set; }
        internal string Acc4Model { get; set; }
        internal string Acc5Locator { get; set; }
        internal string Acc5Model { get; set; }
        internal string Acc6Locator { get; set; }
        internal string Acc6Model { get; set; }
        internal string Acc7Locator { get; set; }
        internal string Acc7Model { get; set; }
        internal string Acc8Locator { get; set; }
        internal string Acc8Model { get; set; }
        internal string BodyAnim { get; set; }
        internal string InfoAnim { get; set; }
        internal string TalkAnim { get; set; }
        internal string DemoAnim { get; set; }
        internal string HubAnim { get; set; }
        internal float ScaleAll { get; set; }
        internal float ScaleHead { get; set; }
        internal float ScaleNeck { get; set; }
        internal float ScaleTorso { get; set; }
        internal float ScaleShoulders { get; set; }
        internal float ScaleArms { get; set; }
        internal float ScaleHands { get; set; }
        internal float ScaleLegs { get; set; }
        internal float ScaleFeet { get; set; }
        internal float VolumeArms { get; set; }
        internal float VolumeLegs { get; set; }
        internal float VolumeBust { get; set; }
        internal float VolumeAbdomen { get; set; }
        internal float VolumeTorso { get; set; }
        internal float VolumeScaleArms { get; set; }
        internal float VolumeScaleLegs { get; set; }
        internal float MapScaleAll { get; set; }
        internal float MapScaleHead { get; set; }
        internal float MapScaleWing { get; set; }
        internal string Voice { get; set; }
        internal string FootStep { get; set; }
        internal string Material { get; set; }
        internal string Comment { get; set; }

        public override string ToString()
        {
            return $"({PresetName}, {Mode}, {Conditions})";
        }
    }
}
