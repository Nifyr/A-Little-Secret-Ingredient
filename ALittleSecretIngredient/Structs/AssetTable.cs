#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{
    internal class Asset : DataParam
    {
        internal event Action<Asset> OnModified;
        private void Set<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnModified?.Invoke(this);
        }
        private string _out;
        private string _presetName;
        private sbyte _mode;
        private string[] _conditions;
        private string _bodyModel;
        private string _dressModel;
        private byte _maskColor100R;
        private byte _maskColor100G;
        private byte _maskColor100B;
        private byte _maskColor075R;
        private byte _maskColor075G;
        private byte _maskColor075B;
        private byte _maskColor050R;
        private byte _maskColor050G;
        private byte _maskColor050B;
        private byte _maskColor025R;
        private byte _maskColor025G;
        private byte _maskColor025B;
        private string _headModel;
        private string _hairModel;
        private byte _hairR;
        private byte _hairG;
        private byte _hairB;
        private byte _gradR;
        private byte _gradG;
        private byte _gradB;
        private byte _skinR;
        private byte _skinG;
        private byte _skinB;
        private byte _toonR;
        private byte _toonG;
        private byte _toonB;
        private string _rideModel;
        private string _rideDressModel;
        private string _leftHand;
        private string _rightHand;
        private string _trail;
        private string _magic;
        private string _acc1Locator;
        private string _acc1Model;
        private string _acc2Locator;
        private string _acc2Model;
        private string _acc3Locator;
        private string _acc3Model;
        private string _acc4Locator;
        private string _acc4Model;
        private string _acc5Locator;
        private string _acc5Model;
        private string _acc6Locator;
        private string _acc6Model;
        private string _acc7Locator;
        private string _acc7Model;
        private string _acc8Locator;
        private string _acc8Model;
        private string _bodyAnim;
        private string _infoAnim;
        private string _talkAnim;
        private string _demoAnim;
        private string _hubAnim;
        private float _scaleAll;
        private float _scaleHead;
        private float _scaleNeck;
        private float _scaleTorso;
        private float _scaleShoulders;
        private float _scaleArms;
        private float _scaleHands;
        private float _scaleLegs;
        private float _scaleFeet;
        private float _volumeArms;
        private float _volumeLegs;
        private float _volumeBust;
        private float _volumeAbdomen;
        private float _volumeTorso;
        private float _volumeScaleArms;
        private float _volumeScaleLegs;
        private float _mapScaleAll;
        private float _mapScaleHead;
        private float _mapScaleWing;
        private string _voice;
        private string _footStep;
        private string _material;
        internal string Out { get => _out; set => Set(ref _out, value); }
        internal string PresetName { get => _presetName; set => Set(ref _presetName, value); }
        internal sbyte Mode { get => _mode; set => Set(ref _mode, value); }
        internal string[] Conditions { get => _conditions; set => Set(ref _conditions, value); }
        internal string BodyModel { get => _bodyModel; set => Set(ref _bodyModel, value); }
        internal string DressModel { get => _dressModel; set => Set(ref _dressModel, value); }
        internal byte MaskColor100R { get => _maskColor100R; set => Set(ref _maskColor100R, value); }
        internal byte MaskColor100G { get => _maskColor100G; set => Set(ref _maskColor100G, value); }
        internal byte MaskColor100B { get => _maskColor100B; set => Set(ref _maskColor100B, value); }
        internal byte MaskColor075R { get => _maskColor075R; set => Set(ref _maskColor075R, value); }
        internal byte MaskColor075G { get => _maskColor075G; set => Set(ref _maskColor075G, value); }
        internal byte MaskColor075B { get => _maskColor075B; set => Set(ref _maskColor075B, value); }
        internal byte MaskColor050R { get => _maskColor050R; set => Set(ref _maskColor050R, value); }
        internal byte MaskColor050G { get => _maskColor050G; set => Set(ref _maskColor050G, value); }
        internal byte MaskColor050B { get => _maskColor050B; set => Set(ref _maskColor050B, value); }
        internal byte MaskColor025R { get => _maskColor025R; set => Set(ref _maskColor025R, value); }
        internal byte MaskColor025G { get => _maskColor025G; set => Set(ref _maskColor025G, value); }
        internal byte MaskColor025B { get => _maskColor025B; set => Set(ref _maskColor025B, value); }
        internal string HeadModel { get => _headModel; set => Set(ref _headModel, value); }
        internal string HairModel { get => _hairModel; set => Set(ref _hairModel, value); }
        internal byte HairR { get => _hairR; set => Set(ref _hairR, value); }
        internal byte HairG { get => _hairG; set => Set(ref _hairG, value); }
        internal byte HairB { get => _hairB; set => Set(ref _hairB, value); }
        internal byte GradR { get => _gradR; set => Set(ref _gradR, value); }
        internal byte GradG { get => _gradG; set => Set(ref _gradG, value); }
        internal byte GradB { get => _gradB; set => Set(ref _gradB, value); }
        internal byte SkinR { get => _skinR; set => Set(ref _skinR, value); }
        internal byte SkinG { get => _skinG; set => Set(ref _skinG, value); }
        internal byte SkinB { get => _skinB; set => Set(ref _skinB, value); }
        internal byte ToonR { get => _toonR; set => Set(ref _toonR, value); }
        internal byte ToonG { get => _toonG; set => Set(ref _toonG, value); }
        internal byte ToonB { get => _toonB; set => Set(ref _toonB, value); }
        internal string RideModel { get => _rideModel; set => Set(ref _rideModel, value); }
        internal string RideDressModel { get => _rideDressModel; set => Set(ref _rideDressModel, value); }
        internal string LeftHand { get => _leftHand; set => Set(ref _leftHand, value); }
        internal string RightHand { get => _rightHand; set => Set(ref _rightHand, value); }
        internal string Trail { get => _trail; set => Set(ref _trail, value); }
        internal string Magic { get => _magic; set => Set(ref _magic, value); }
        internal string Acc1Locator { get => _acc1Locator; set => Set(ref _acc1Locator, value); }
        internal string Acc1Model { get => _acc1Model; set => Set(ref _acc1Model, value); }
        internal string Acc2Locator { get => _acc2Locator; set => Set(ref _acc2Locator, value); }
        internal string Acc2Model { get => _acc2Model; set => Set(ref _acc2Model, value); }
        internal string Acc3Locator { get => _acc3Locator; set => Set(ref _acc3Locator, value); }
        internal string Acc3Model { get => _acc3Model; set => Set(ref _acc3Model, value); }
        internal string Acc4Locator { get => _acc4Locator; set => Set(ref _acc4Locator, value); }
        internal string Acc4Model { get => _acc4Model; set => Set(ref _acc4Model, value); }
        internal string Acc5Locator { get => _acc5Locator; set => Set(ref _acc5Locator, value); }
        internal string Acc5Model { get => _acc5Model; set => Set(ref _acc5Model, value); }
        internal string Acc6Locator { get => _acc6Locator; set => Set(ref _acc6Locator, value); }
        internal string Acc6Model { get => _acc6Model; set => Set(ref _acc6Model, value); }
        internal string Acc7Locator { get => _acc7Locator; set => Set(ref _acc7Locator, value); }
        internal string Acc7Model { get => _acc7Model; set => Set(ref _acc7Model, value); }
        internal string Acc8Locator { get => _acc8Locator; set => Set(ref _acc8Locator, value); }
        internal string Acc8Model { get => _acc8Model; set => Set(ref _acc8Model, value); }
        internal string BodyAnim { get => _bodyAnim; set => Set(ref _bodyAnim, value); }
        internal string InfoAnim { get => _infoAnim; set => Set(ref _infoAnim, value); }
        internal string TalkAnim { get => _talkAnim; set => Set(ref _talkAnim, value); }
        internal string DemoAnim { get => _demoAnim; set => Set(ref _demoAnim, value); }
        internal string HubAnim { get => _hubAnim; set => Set(ref _hubAnim, value); }
        internal float ScaleAll { get => _scaleAll; set => Set(ref _scaleAll, value); }
        internal float ScaleHead { get => _scaleHead; set => Set(ref _scaleHead, value); }
        internal float ScaleNeck { get => _scaleNeck; set => Set(ref _scaleNeck, value); }
        internal float ScaleTorso { get => _scaleTorso; set => Set(ref _scaleTorso, value); }
        internal float ScaleShoulders { get => _scaleShoulders; set => Set(ref _scaleShoulders, value); }
        internal float ScaleArms { get => _scaleArms; set => Set(ref _scaleArms, value); }
        internal float ScaleHands { get => _scaleHands; set => Set(ref _scaleHands, value); }
        internal float ScaleLegs { get => _scaleLegs; set => Set(ref _scaleLegs, value); }
        internal float ScaleFeet { get => _scaleFeet; set => Set(ref _scaleFeet, value); }
        internal float VolumeArms { get => _volumeArms; set => Set(ref _volumeArms, value); }
        internal float VolumeLegs { get => _volumeLegs; set => Set(ref _volumeLegs, value); }
        internal float VolumeBust { get => _volumeBust; set => Set(ref _volumeBust, value); }
        internal float VolumeAbdomen { get => _volumeAbdomen; set => Set(ref _volumeAbdomen, value); }
        internal float VolumeTorso { get => _volumeTorso; set => Set(ref _volumeTorso, value); }
        internal float VolumeScaleArms { get => _volumeScaleArms; set => Set(ref _volumeScaleArms, value); }
        internal float VolumeScaleLegs { get => _volumeScaleLegs; set => Set(ref _volumeScaleLegs, value); }
        internal float MapScaleAll { get => _mapScaleAll; set => Set(ref _mapScaleAll, value); }
        internal float MapScaleHead { get => _mapScaleHead; set => Set(ref _mapScaleHead, value); }
        internal float MapScaleWing { get => _mapScaleWing; set => Set(ref _mapScaleWing, value); }
        internal string Voice { get => _voice; set => Set(ref _voice, value); }
        internal string FootStep { get => _footStep; set => Set(ref _footStep, value); }
        internal string Material { get => _material; set => Set(ref _material, value); }
        internal string Comment { get; set; }

        public override string ToString()
        {
            return $"({PresetName}, {Mode}, {Conditions})";
        }

        internal bool HasMaskColor()
        {
            return (MaskColor100R | MaskColor100G | MaskColor100B | MaskColor075R | MaskColor075G | MaskColor075B |
                MaskColor050R | MaskColor050G | MaskColor050B | MaskColor025R | MaskColor025G | MaskColor025B) > 0;
        }

        internal bool HasHairColor()
        {
            return (HairR | HairG | HairB) > 0;
        }
    }
}
