using ALittleSecretIngredient.Structs;
using System.Configuration;

namespace ALittleSecretIngredient.Forms
{
    public partial class MainForm : Form
    {
        private GlobalData GlobalData { get; }
        private AssetTableForm AssetTable { get; set; }
        private GodGeneralForm GodGeneral { get; set; }
        private GrowthTableForm GrowthTable { get; set; }
        private BondLevelForm BondLevel { get; set; }
        private IndividualForm Individual { get; set; }
        public MainForm()
        {
            GlobalData = new();
            InitializeComponent();
            AssetTable = new(GlobalData);
            GodGeneral = new(GlobalData);
            GrowthTable = new(GlobalData);
            BondLevel = new(GlobalData);
            Individual = new(GlobalData);
        }
        private static DialogResult LoadDumpDialog()
        {
            return MessageBox.Show("*Firstly*, could you kindly provide me with a game dump of Fire Emblem Engage 2.0.0? " +
                "Please select the folder *labelled* \"romfs\".",
            "Load Dump", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private static DialogResult RetryLoadDumpDialog()
        {
            return MessageBox.Show("To proceed, it is *necessary* that I acquire this game dump. Would you like to load the dump?",
                "Load Cancelled", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private static string? GetRomfsDirDialog()
        {
            FolderBrowserDialog fbd = new()
            {
                Description = "Choose the folder *designated* as \"romfs\"."
            };
            if (fbd.ShowDialog() != DialogResult.OK)
                return null;
            return fbd.SelectedPath;
        }

        internal static void ShowDataError()
        {
            MessageBox.Show("The input data provided is *invalid*.",
                "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ShowSettingsLoadError()
        {
            MessageBox.Show("An error occurred while attempting to load some *settings*.",
                "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ExportSuccessMessage()
        {
            MessageBox.Show("The Randomizer mod has been successfully exported and duly titled 'Output'. It has been placed *alongside* my executable.",
                "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void NoChangesMessage()
        {
            MessageBox.Show("It appears that there are no changes made. Could it be that you forgot to activate any of the *randomization* options?",
                "No Options Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private RandomizerSettings RandomizerSettings
        {
            get
            {
                RandomizerSettings rs = new()
                {
                    Remember = rememberSettingsCheckBox.Checked,
                    SaveChangelog = saveChangelogCheckBox.Checked
                };

                rs.AssetTable.ModelSwap = new(false, null!, new object[] { AssetTable.checkBox20.Checked, AssetTable.checkBox1.Checked,
                    AssetTable.checkBox2.Checked, AssetTable.checkBox3.Checked, AssetTable.checkBox4.Checked, AssetTable.checkBox5.Checked,
                    AssetTable.checkBox6.Checked });
                rs.AssetTable.OutfitSwap = new(false, null!, new object[] { AssetTable.checkBox13.Checked, AssetTable.checkBox7.Checked,
                    AssetTable.checkBox8.Checked, AssetTable.checkBox9.Checked, AssetTable.checkBox10.Checked, AssetTable.checkBox11.Checked,
                    AssetTable.checkBox12.Checked });
                rs.AssetTable.ColorPalette = new(AssetTable.checkBox21.Checked, null!, new object[] { AssetTable.checkBox14.Checked });
                rs.AssetTable.ShuffleRideDressModel = AssetTable.checkBox16.Checked;
                rs.AssetTable.InfoAnim = new(AssetTable.checkBox15.Checked, null!, new object[] { AssetTable.checkBox17.Checked });
                rs.AssetTable.ShuffleTalkAnims = AssetTable.checkBox19.Checked;
                rs.AssetTable.DemoAnim = new(AssetTable.checkBox22.Checked, null!, new object[] { AssetTable.checkBox18.Checked });
                rs.AssetTable.ShuffleHubAnims = AssetTable.checkBox23.Checked;
                rs.AssetTable.RandomizeModelParameters = AssetTable.checkBox24.Checked;
                rs.AssetTable.ScaleAll = new(false, AssetTable.ScaleAll.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleHead = new(false, AssetTable.ScaleHead.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleNeck = new(false, AssetTable.ScaleNeck.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleTorso = new(false, AssetTable.ScaleTorso.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleShoulders = new(false, AssetTable.ScaleShoulders.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleArms = new(false, AssetTable.ScaleArms.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleHands = new(false, AssetTable.ScaleHands.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleLegs = new(false, AssetTable.ScaleLegs.Get(), Array.Empty<object>());
                rs.AssetTable.ScaleFeet = new(false, AssetTable.ScaleFeet.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeArms = new(false, AssetTable.VolumeArms.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeLegs = new(false, AssetTable.VolumeLegs.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeBust = new(false, AssetTable.VolumeBust.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeAbdomen = new(false, AssetTable.VolumeAbdomen.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeTorso = new(false, AssetTable.VolumeTorso.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeScaleArms = new(false, AssetTable.VolumeScaleArms.Get(), Array.Empty<object>());
                rs.AssetTable.VolumeScaleLegs = new(false, AssetTable.VolumeScaleLegs.Get(), Array.Empty<object>());
                rs.AssetTable.MapScaleAll = new(false, AssetTable.MapScaleAll.Get(), Array.Empty<object>());
                rs.AssetTable.MapScaleHead = new(false, AssetTable.MapScaleHead.Get(), Array.Empty<object>());
                rs.AssetTable.MapScaleWing = new(false, AssetTable.MapScaleWing.Get(), Array.Empty<object>());

                rs.GodGeneral.EngageCount = new(GodGeneral.checkBox1.Checked,
                    GodGeneral.EngageCount.Get(), Array.Empty<object>());
                rs.GodGeneral.Link = new(GodGeneral.checkBox2.Checked,
                    GodGeneral.Link.Get(), new object[]
                    {
                        GodGeneral.checkBox3.Checked, (double)GodGeneral.numericUpDown1.Value
                    });
                rs.GodGeneral.EngageAttackAlly = new(GodGeneral.checkBox4.Checked,
                    GodGeneral.EngageAttackAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.EngageAttackEnemy = new(GodGeneral.checkBox9.Checked,
                    GodGeneral.EngageAttackEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.EngageAttackLink = new(GodGeneral.checkBox5.Checked,
                    GodGeneral.EngageAttackLink.Get(), Array.Empty<object>());
                rs.GodGeneral.LinkGid = new(GodGeneral.checkBox6.Checked,
                    GodGeneral.LinkGid.Get(), new object[] { GodGeneral.checkBox7.Checked });
                rs.GodGeneral.ShuffleGrowTableAlly = GodGeneral.checkBox8.Checked;
                rs.GodGeneral.ShuffleGrowTableEnemy = GodGeneral.checkBox10.Checked;
                rs.GodGeneral.RandomizeEngravingStats = GodGeneral.checkBox11.Checked;
                rs.GodGeneral.EngravePower = new(true,
                    GodGeneral.EngravePower.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveWeight = new(true,
                    GodGeneral.EngraveWeight.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveHit = new(true,
                    GodGeneral.EngraveHit.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveCritical = new(true,
                    GodGeneral.EngraveCritical.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveAvoid = new(true,
                    GodGeneral.EngraveAvoid.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveSecure = new(true,
                    GodGeneral.EngraveSecure.Get(), Array.Empty<object>());
                rs.GodGeneral.RandomizeAllyStaticSyncStats = GodGeneral.checkBox17.Checked;
                rs.GodGeneral.SynchroEnhanceHpAlly = new(true,
                    GodGeneral.SynchroEnhanceHpAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceStrAlly = new(true,
                    GodGeneral.SynchroEnhanceStrAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceTechAlly = new(true,
                    GodGeneral.SynchroEnhanceTechAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceQuickAlly = new(true,
                    GodGeneral.SynchroEnhanceQuickAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceLuckAlly = new(true,
                    GodGeneral.SynchroEnhanceLuckAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceDefAlly = new(true,
                    GodGeneral.SynchroEnhanceDefAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMagicAlly = new(true,
                    GodGeneral.SynchroEnhanceMagicAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMdefAlly = new(true,
                    GodGeneral.SynchroEnhanceMdefAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhancePhysAlly = new(true,
                    GodGeneral.SynchroEnhancePhysAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMoveAlly = new(true,
                    GodGeneral.SynchroEnhanceMoveAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.RandomizeEnemyStaticSyncStats = GodGeneral.checkBox12.Checked;
                rs.GodGeneral.SynchroEnhanceHpEnemy = new(true,
                    GodGeneral.SynchroEnhanceHpEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceStrEnemy = new(true,
                    GodGeneral.SynchroEnhanceStrEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceTechEnemy = new(true,
                    GodGeneral.SynchroEnhanceTechEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceQuickEnemy = new(true,
                    GodGeneral.SynchroEnhanceQuickEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceLuckEnemy = new(true,
                    GodGeneral.SynchroEnhanceLuckEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceDefEnemy = new(true,
                    GodGeneral.SynchroEnhanceDefEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMagicEnemy = new(true,
                    GodGeneral.SynchroEnhanceMagicEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMdefEnemy = new(true,
                    GodGeneral.SynchroEnhanceMdefEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhancePhysEnemy = new(true,
                    GodGeneral.SynchroEnhancePhysEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMoveEnemy = new(true,
                    GodGeneral.SynchroEnhanceMoveEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.WeaponRestriction = new(GodGeneral.checkBox13.Checked,
                    null!, new object[] { (double)GodGeneral.numericUpDown2.Value });

                rs.GrowthTable.InheritanceSkills = new(GrowthTable.checkBox1.Checked,
                    GrowthTable.InheritanceSkills.Get(), Array.Empty<object>());
                rs.GrowthTable.InheritanceSkillsCount = new(GrowthTable.checkBox2.Checked,
                    GrowthTable.InheritanceSkillsCount.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroStatSkillsAlly = new(GrowthTable.checkBox4.Checked,
                    GrowthTable.SynchroStatSkillsAlly.Get(), new object[] { GrowthTable.checkBox5.Checked });
                rs.GrowthTable.SynchroStatSkillsAllyCount = new(GrowthTable.checkBox3.Checked,
                    GrowthTable.SynchroStatSkillsAllyCount.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroStatSkillsEnemy = new(GrowthTable.checkBox7.Checked,
                    GrowthTable.SynchroStatSkillsEnemy.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroStatSkillsEnemyCount = new(GrowthTable.checkBox6.Checked,
                    GrowthTable.SynchroStatSkillsEnemyCount.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroGeneralSkillsAlly = new(GrowthTable.checkBox12.Checked,
                    GrowthTable.SynchroGeneralSkillsAlly.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroGeneralSkillsAllyCount = new(GrowthTable.checkBox11.Checked,
                    GrowthTable.SynchroGeneralSkillsAllyCount.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroGeneralSkillsEnemy = new(GrowthTable.checkBox9.Checked,
                    GrowthTable.SynchroGeneralSkillsEnemy.Get(), Array.Empty<object>());
                rs.GrowthTable.SynchroGeneralSkillsEnemyCount = new(GrowthTable.checkBox8.Checked,
                    GrowthTable.SynchroGeneralSkillsEnemyCount.Get(), Array.Empty<object>());
                rs.GrowthTable.EngageSkills = new(GrowthTable.checkBox15.Checked,
                    GrowthTable.EngageSkills.Get(), Array.Empty<object>());
                rs.GrowthTable.EngageSkillsCount = new(GrowthTable.checkBox14.Checked,
                    GrowthTable.EngageSkillsCount.Get(), Array.Empty<object>());
                rs.GrowthTable.EngageItems = new(GrowthTable.checkBox13.Checked,
                    GrowthTable.EngageItems.Get(), new object[] { GrowthTable.checkBox16.Checked, (double)GrowthTable.numericUpDown1.Value });
                rs.GrowthTable.EngageItemsCount = new(GrowthTable.checkBox10.Checked,
                    GrowthTable.EngageItemsCount.Get(), Array.Empty<object>());
                rs.GrowthTable.Aptitude = new(GrowthTable.checkBox18.Checked,
                    GrowthTable.Aptitude.Get(), Array.Empty<object>());
                rs.GrowthTable.AptitudeCount = new(GrowthTable.checkBox17.Checked,
                    GrowthTable.AptitudeCount.Get(), Array.Empty<object>());
                rs.GrowthTable.SkillInheritanceLevel = new(GrowthTable.checkBox20.Checked,
                    GrowthTable.SkillInheritanceLevel.Get(), Array.Empty<object>());
                rs.GrowthTable.StrongBondLevel = new(GrowthTable.checkBox19.Checked,
                    GrowthTable.StrongBondLevel.Get(), Array.Empty<object>());
                rs.GrowthTable.DeepSynergyLevel = new(GrowthTable.checkBox21.Checked,
                    GrowthTable.DeepSynergyLevel.Get(), Array.Empty<object>());

                rs.BondLevel.Exp = new(BondLevel.checkBox20.Checked,
                    BondLevel.Exp.Get(), Array.Empty<object>());
                rs.BondLevel.Cost = new(BondLevel.checkBox1.Checked,
                    BondLevel.Cost.Get(), Array.Empty<object>());

                rs.Individual.Age = new(Individual.checkBox20.Checked, Individual.Age.Get(), Array.Empty<object>());
                rs.Individual.RandomizeBirthday = Individual.checkBox1.Checked;
                rs.Individual.LevelAlly = new(Individual.checkBox2.Checked, Individual.LevelAlly.Get(), Array.Empty<object>());
                rs.Individual.LevelEnemy = new(Individual.checkBox4.Checked, Individual.LevelEnemy.Get(), Array.Empty<object>());
                rs.Individual.InternalLevel = new(Individual.checkBox3.Checked, Individual.InternalLevel.Get(), Array.Empty<object>());
                rs.Individual.SupportCategory = new(Individual.checkBox5.Checked, Individual.SupportCategory.Get(), Array.Empty<object>());
                rs.Individual.SkillPoint = new(Individual.checkBox6.Checked, Individual.SkillPoint.Get(), Array.Empty<object>());
                rs.Individual.Aptitude = new(Individual.checkBox7.Checked, Individual.Aptitude.Get(), Array.Empty<object>());
                rs.Individual.AptitudeCount = new(Individual.checkBox8.Checked, Individual.AptitudeCount.Get(), Array.Empty<object>());
                rs.Individual.SubAptitude = new(Individual.checkBox10.Checked, Individual.SubAptitude.Get(), Array.Empty<object>());
                rs.Individual.SubAptitudeCount = new(Individual.checkBox9.Checked, Individual.SubAptitudeCount.Get(), Array.Empty<object>());
                rs.Individual.RandomizeAllyBases = Individual.checkBox11.Checked;
                rs.Individual.OffsetNHpAlly = new(false, Individual.OffsetNHpAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNStrAlly = new(false, Individual.OffsetNStrAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNTechAlly = new(false, Individual.OffsetNTechAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNQuickAlly = new(false, Individual.OffsetNQuickAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNLuckAlly = new(false, Individual.OffsetNLuckAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNDefAlly = new(false, Individual.OffsetNDefAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNMagicAlly = new(false, Individual.OffsetNMagicAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNMdefAlly = new(false, Individual.OffsetNMdefAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNPhysAlly = new(false, Individual.OffsetNPhysAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNSightAlly = new(false, Individual.OffsetNSightAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNMoveAlly = new(false, Individual.OffsetNMoveAlly.Get(), Array.Empty<object>());
                rs.Individual.OffsetNTotalAlly = new(Individual.checkBox13.Checked, Individual.OffsetNTotalAlly.Get(), Array.Empty<object>());
                rs.Individual.StrongerProtagonist = Individual.checkBox21.Checked;
                rs.Individual.StrongerAllyNPCs = Individual.checkBox12.Checked;
                rs.Individual.RandomizeEnemyBasesNormal = Individual.checkBox16.Checked;
                rs.Individual.OffsetNHpEnemy = new(false, Individual.OffsetNHpEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNStrEnemy = new(false, Individual.OffsetNStrEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNTechEnemy = new(false, Individual.OffsetNTechEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNQuickEnemy = new(false, Individual.OffsetNQuickEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNLuckEnemy = new(false, Individual.OffsetNLuckEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNDefEnemy = new(false, Individual.OffsetNDefEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNMagicEnemy = new(false, Individual.OffsetNMagicEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNMdefEnemy = new(false, Individual.OffsetNMdefEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNPhysEnemy = new(false, Individual.OffsetNPhysEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNSightEnemy = new(false, Individual.OffsetNSightEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNMoveEnemy = new(false, Individual.OffsetNMoveEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetNTotalEnemy = new(Individual.checkBox15.Checked, Individual.OffsetNTotalEnemy.Get(), Array.Empty<object>());
                rs.Individual.RandomizeEnemyBasesHard = Individual.checkBox17.Checked;
                rs.Individual.OffsetHHpEnemy = new(false, Individual.OffsetHHpEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHStrEnemy = new(false, Individual.OffsetHStrEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHTechEnemy = new(false, Individual.OffsetHTechEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHQuickEnemy = new(false, Individual.OffsetHQuickEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHLuckEnemy = new(false, Individual.OffsetHLuckEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHDefEnemy = new(false, Individual.OffsetHDefEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHMagicEnemy = new(false, Individual.OffsetHMagicEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHMdefEnemy = new(false, Individual.OffsetHMdefEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHPhysEnemy = new(false, Individual.OffsetHPhysEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHSightEnemy = new(false, Individual.OffsetHSightEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHMoveEnemy = new(false, Individual.OffsetHMoveEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetHTotalEnemy = new(Individual.checkBox14.Checked, Individual.OffsetHTotalEnemy.Get(), Array.Empty<object>());
                rs.Individual.RandomizeEnemyBasesMaddening = Individual.checkBox19.Checked;
                rs.Individual.OffsetLHpEnemy = new(false, Individual.OffsetLHpEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLStrEnemy = new(false, Individual.OffsetLStrEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLTechEnemy = new(false, Individual.OffsetLTechEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLQuickEnemy = new(false, Individual.OffsetLQuickEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLLuckEnemy = new(false, Individual.OffsetLLuckEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLDefEnemy = new(false, Individual.OffsetLDefEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLMagicEnemy = new(false, Individual.OffsetLMagicEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLMdefEnemy = new(false, Individual.OffsetLMdefEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLPhysEnemy = new(false, Individual.OffsetLPhysEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLSightEnemy = new(false, Individual.OffsetLSightEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLMoveEnemy = new(false, Individual.OffsetLMoveEnemy.Get(), Array.Empty<object>());
                rs.Individual.OffsetLTotalEnemy = new(Individual.checkBox18.Checked, Individual.OffsetLTotalEnemy.Get(), Array.Empty<object>());

                return rs;
            }
            set
            {
                rememberSettingsCheckBox.Checked = value.Remember;
                saveChangelogCheckBox.Checked = value.SaveChangelog;

                AssetTable.checkBox20.Checked = value.AssetTable.ModelSwap.GetArg<bool>(0);
                AssetTable.checkBox1.Checked = value.AssetTable.ModelSwap.GetArg<bool>(1);
                AssetTable.checkBox2.Checked = value.AssetTable.ModelSwap.GetArg<bool>(2);
                AssetTable.checkBox3.Checked = value.AssetTable.ModelSwap.GetArg<bool>(3);
                AssetTable.checkBox4.Checked = value.AssetTable.ModelSwap.GetArg<bool>(4);
                AssetTable.checkBox5.Checked = value.AssetTable.ModelSwap.GetArg<bool>(5);
                AssetTable.checkBox6.Checked = value.AssetTable.ModelSwap.GetArg<bool>(6);
                AssetTable.checkBox13.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(0);
                AssetTable.checkBox7.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(1);
                AssetTable.checkBox8.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(2);
                AssetTable.checkBox9.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(3);
                AssetTable.checkBox10.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(4);
                AssetTable.checkBox11.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(5);
                AssetTable.checkBox12.Checked = value.AssetTable.OutfitSwap.GetArg<bool>(6);
                AssetTable.checkBox21.Checked = value.AssetTable.ColorPalette.Enabled;
                AssetTable.checkBox14.Checked = value.AssetTable.ColorPalette.GetArg<bool>(0);
                AssetTable.checkBox16.Checked = value.AssetTable.ShuffleRideDressModel;
                AssetTable.checkBox15.Checked = value.AssetTable.InfoAnim.Enabled;
                AssetTable.checkBox17.Checked = value.AssetTable.InfoAnim.GetArg<bool>(0);
                AssetTable.checkBox19.Checked = value.AssetTable.ShuffleTalkAnims;
                AssetTable.checkBox22.Checked = value.AssetTable.DemoAnim.Enabled;
                AssetTable.checkBox18.Checked = value.AssetTable.DemoAnim.GetArg<bool>(0);
                AssetTable.checkBox23.Checked = value.AssetTable.ShuffleHubAnims;
                AssetTable.checkBox24.Checked = value.AssetTable.RandomizeModelParameters;
                AssetTable.ScaleAll.Set(value.AssetTable.ScaleAll.Distribution);
                AssetTable.ScaleHead.Set(value.AssetTable.ScaleHead.Distribution);
                AssetTable.ScaleNeck.Set(value.AssetTable.ScaleNeck.Distribution);
                AssetTable.ScaleTorso.Set(value.AssetTable.ScaleTorso.Distribution);
                AssetTable.ScaleShoulders.Set(value.AssetTable.ScaleShoulders.Distribution);
                AssetTable.ScaleArms.Set(value.AssetTable.ScaleArms.Distribution);
                AssetTable.ScaleHands.Set(value.AssetTable.ScaleHands.Distribution);
                AssetTable.ScaleLegs.Set(value.AssetTable.ScaleLegs.Distribution);
                AssetTable.ScaleFeet.Set(value.AssetTable.ScaleFeet.Distribution);
                AssetTable.VolumeArms.Set(value.AssetTable.VolumeArms.Distribution);
                AssetTable.VolumeLegs.Set(value.AssetTable.VolumeLegs.Distribution);
                AssetTable.VolumeBust.Set(value.AssetTable.VolumeBust.Distribution);
                AssetTable.VolumeAbdomen.Set(value.AssetTable.VolumeAbdomen.Distribution);
                AssetTable.VolumeTorso.Set(value.AssetTable.VolumeTorso.Distribution);
                AssetTable.VolumeScaleArms.Set(value.AssetTable.VolumeScaleArms.Distribution);
                AssetTable.VolumeScaleLegs.Set(value.AssetTable.VolumeScaleLegs.Distribution);
                AssetTable.MapScaleAll.Set(value.AssetTable.MapScaleAll.Distribution);
                AssetTable.MapScaleHead.Set(value.AssetTable.MapScaleHead.Distribution);
                AssetTable.MapScaleWing.Set(value.AssetTable.MapScaleWing.Distribution);

                GodGeneral.checkBox1.Checked = value.GodGeneral.EngageCount.Enabled;
                GodGeneral.EngageCount.Set(value.GodGeneral.EngageCount.Distribution);
                GodGeneral.checkBox2.Checked = value.GodGeneral.Link.Enabled;
                GodGeneral.Link.Set(value.GodGeneral.Link.Distribution);
                GodGeneral.checkBox3.Checked = (bool)value.GodGeneral.Link.Args[0];
                GodGeneral.numericUpDown1.Value = (decimal)(double)value.GodGeneral.Link.Args[1];
                GodGeneral.checkBox4.Checked = value.GodGeneral.EngageAttackAlly.Enabled;
                GodGeneral.EngageAttackAlly.Set(value.GodGeneral.EngageAttackAlly.Distribution);
                GodGeneral.checkBox9.Checked = value.GodGeneral.EngageAttackEnemy.Enabled;
                GodGeneral.EngageAttackEnemy.Set(value.GodGeneral.EngageAttackEnemy.Distribution);
                GodGeneral.checkBox5.Checked = value.GodGeneral.EngageAttackLink.Enabled;
                GodGeneral.EngageAttackLink.Set(value.GodGeneral.EngageAttackLink.Distribution);
                GodGeneral.checkBox6.Checked = value.GodGeneral.LinkGid.Enabled;
                GodGeneral.LinkGid.Set(value.GodGeneral.LinkGid.Distribution);
                GodGeneral.checkBox7.Checked = (bool)value.GodGeneral.LinkGid.Args[0];
                GodGeneral.checkBox8.Checked = value.GodGeneral.ShuffleGrowTableAlly;
                GodGeneral.checkBox10.Checked = value.GodGeneral.ShuffleGrowTableEnemy;
                GodGeneral.checkBox11.Checked = value.GodGeneral.RandomizeEngravingStats;
                GodGeneral.EngravePower.Set(value.GodGeneral.EngravePower.Distribution);
                GodGeneral.EngraveWeight.Set(value.GodGeneral.EngraveWeight.Distribution);
                GodGeneral.EngraveHit.Set(value.GodGeneral.EngraveHit.Distribution);
                GodGeneral.EngraveCritical.Set(value.GodGeneral.EngraveCritical.Distribution);
                GodGeneral.EngraveAvoid.Set(value.GodGeneral.EngraveAvoid.Distribution);
                GodGeneral.EngraveSecure.Set(value.GodGeneral.EngraveSecure.Distribution);
                GodGeneral.checkBox17.Checked = value.GodGeneral.RandomizeAllyStaticSyncStats;
                GodGeneral.SynchroEnhanceHpAlly.Set(value.GodGeneral.SynchroEnhanceHpAlly.Distribution);
                GodGeneral.SynchroEnhanceStrAlly.Set(value.GodGeneral.SynchroEnhanceStrAlly.Distribution);
                GodGeneral.SynchroEnhanceTechAlly.Set(value.GodGeneral.SynchroEnhanceTechAlly.Distribution);
                GodGeneral.SynchroEnhanceQuickAlly.Set(value.GodGeneral.SynchroEnhanceQuickAlly.Distribution);
                GodGeneral.SynchroEnhanceLuckAlly.Set(value.GodGeneral.SynchroEnhanceLuckAlly.Distribution);
                GodGeneral.SynchroEnhanceDefAlly.Set(value.GodGeneral.SynchroEnhanceDefAlly.Distribution);
                GodGeneral.SynchroEnhanceMagicAlly.Set(value.GodGeneral.SynchroEnhanceMagicAlly.Distribution);
                GodGeneral.SynchroEnhanceMdefAlly.Set(value.GodGeneral.SynchroEnhanceMdefAlly.Distribution);
                GodGeneral.SynchroEnhancePhysAlly.Set(value.GodGeneral.SynchroEnhancePhysAlly.Distribution);
                GodGeneral.SynchroEnhanceMoveAlly.Set(value.GodGeneral.SynchroEnhanceMoveAlly.Distribution);
                GodGeneral.checkBox12.Checked = value.GodGeneral.RandomizeEnemyStaticSyncStats;
                GodGeneral.SynchroEnhanceHpEnemy.Set(value.GodGeneral.SynchroEnhanceHpEnemy.Distribution);
                GodGeneral.SynchroEnhanceStrEnemy.Set(value.GodGeneral.SynchroEnhanceStrEnemy.Distribution);
                GodGeneral.SynchroEnhanceTechEnemy.Set(value.GodGeneral.SynchroEnhanceTechEnemy.Distribution);
                GodGeneral.SynchroEnhanceQuickEnemy.Set(value.GodGeneral.SynchroEnhanceQuickEnemy.Distribution);
                GodGeneral.SynchroEnhanceLuckEnemy.Set(value.GodGeneral.SynchroEnhanceLuckEnemy.Distribution);
                GodGeneral.SynchroEnhanceDefEnemy.Set(value.GodGeneral.SynchroEnhanceDefEnemy.Distribution);
                GodGeneral.SynchroEnhanceMagicEnemy.Set(value.GodGeneral.SynchroEnhanceMagicEnemy.Distribution);
                GodGeneral.SynchroEnhanceMdefEnemy.Set(value.GodGeneral.SynchroEnhanceMdefEnemy.Distribution);
                GodGeneral.SynchroEnhancePhysEnemy.Set(value.GodGeneral.SynchroEnhancePhysEnemy.Distribution);
                GodGeneral.SynchroEnhanceMoveEnemy.Set(value.GodGeneral.SynchroEnhanceMoveEnemy.Distribution);
                GodGeneral.checkBox13.Checked = value.GodGeneral.WeaponRestriction.Enabled;
                GodGeneral.numericUpDown2.Value = (decimal)(double)value.GodGeneral.WeaponRestriction.Args[0];

                GrowthTable.checkBox1.Checked = value.GrowthTable.InheritanceSkills.Enabled;
                GrowthTable.InheritanceSkills.Set(value.GrowthTable.InheritanceSkills.Distribution);
                GrowthTable.checkBox2.Checked = value.GrowthTable.InheritanceSkillsCount.Enabled;
                GrowthTable.InheritanceSkillsCount.Set(value.GrowthTable.InheritanceSkillsCount.Distribution);
                GrowthTable.checkBox4.Checked = value.GrowthTable.SynchroStatSkillsAlly.Enabled;
                GrowthTable.SynchroStatSkillsAlly.Set(value.GrowthTable.SynchroStatSkillsAlly.Distribution);
                GrowthTable.checkBox3.Checked = value.GrowthTable.SynchroStatSkillsAllyCount.Enabled;
                GrowthTable.SynchroStatSkillsAllyCount.Set(value.GrowthTable.SynchroStatSkillsAllyCount.Distribution);
                GrowthTable.checkBox5.Checked = value.GrowthTable.SynchroStatSkillsAlly.GetArg<bool>(0);
                GrowthTable.checkBox7.Checked = value.GrowthTable.SynchroStatSkillsEnemy.Enabled;
                GrowthTable.SynchroStatSkillsEnemy.Set(value.GrowthTable.SynchroStatSkillsEnemy.Distribution);
                GrowthTable.checkBox6.Checked = value.GrowthTable.SynchroStatSkillsEnemyCount.Enabled;
                GrowthTable.SynchroStatSkillsEnemyCount.Set(value.GrowthTable.SynchroStatSkillsEnemyCount.Distribution);
                GrowthTable.checkBox12.Checked = value.GrowthTable.SynchroGeneralSkillsAlly.Enabled;
                GrowthTable.SynchroGeneralSkillsAlly.Set(value.GrowthTable.SynchroGeneralSkillsAlly.Distribution);
                GrowthTable.checkBox11.Checked = value.GrowthTable.SynchroGeneralSkillsAllyCount.Enabled;
                GrowthTable.SynchroGeneralSkillsAllyCount.Set(value.GrowthTable.SynchroGeneralSkillsAllyCount.Distribution);
                GrowthTable.checkBox9.Checked = value.GrowthTable.SynchroGeneralSkillsEnemy.Enabled;
                GrowthTable.SynchroGeneralSkillsEnemy.Set(value.GrowthTable.SynchroGeneralSkillsEnemy.Distribution);
                GrowthTable.checkBox8.Checked = value.GrowthTable.SynchroGeneralSkillsEnemyCount.Enabled;
                GrowthTable.SynchroGeneralSkillsEnemyCount.Set(value.GrowthTable.SynchroGeneralSkillsEnemyCount.Distribution);
                GrowthTable.checkBox15.Checked = value.GrowthTable.EngageSkills.Enabled;
                GrowthTable.EngageSkills.Set(value.GrowthTable.EngageSkills.Distribution);
                GrowthTable.checkBox14.Checked = value.GrowthTable.EngageSkillsCount.Enabled;
                GrowthTable.EngageSkillsCount.Set(value.GrowthTable.EngageSkillsCount.Distribution);
                GrowthTable.checkBox13.Checked = value.GrowthTable.EngageItems.Enabled;
                GrowthTable.EngageItems.Set(value.GrowthTable.EngageItems.Distribution);
                GrowthTable.checkBox10.Checked = value.GrowthTable.EngageItemsCount.Enabled;
                GrowthTable.EngageItemsCount.Set(value.GrowthTable.EngageItemsCount.Distribution);
                GrowthTable.checkBox16.Checked = value.GrowthTable.EngageItems.GetArg<bool>(0);
                GrowthTable.numericUpDown1.Value = (decimal)value.GrowthTable.EngageItems.GetArg<double>(1);
                GrowthTable.checkBox18.Checked = value.GrowthTable.Aptitude.Enabled;
                GrowthTable.Aptitude.Set(value.GrowthTable.Aptitude.Distribution);
                GrowthTable.checkBox17.Checked = value.GrowthTable.AptitudeCount.Enabled;
                GrowthTable.AptitudeCount.Set(value.GrowthTable.AptitudeCount.Distribution);
                GrowthTable.checkBox20.Checked = value.GrowthTable.SkillInheritanceLevel.Enabled;
                GrowthTable.SkillInheritanceLevel.Set(value.GrowthTable.SkillInheritanceLevel.Distribution);
                GrowthTable.checkBox19.Checked = value.GrowthTable.StrongBondLevel.Enabled;
                GrowthTable.StrongBondLevel.Set(value.GrowthTable.StrongBondLevel.Distribution);
                GrowthTable.checkBox21.Checked = value.GrowthTable.DeepSynergyLevel.Enabled;
                GrowthTable.DeepSynergyLevel.Set(value.GrowthTable.DeepSynergyLevel.Distribution);

                BondLevel.checkBox20.Checked = value.BondLevel.Exp.Enabled;
                BondLevel.Exp.Set(value.BondLevel.Exp.Distribution);
                BondLevel.checkBox1.Checked = value.BondLevel.Cost.Enabled;
                BondLevel.Cost.Set(value.BondLevel.Cost.Distribution);

                Individual.checkBox20.Checked = value.Individual.Age.Enabled;
                Individual.Age.Set(value.Individual.Age.Distribution);
                Individual.checkBox1.Checked = value.Individual.RandomizeBirthday;
                Individual.checkBox2.Checked = value.Individual.LevelAlly.Enabled;
                Individual.LevelAlly.Set(value.Individual.LevelAlly.Distribution);
                Individual.checkBox4.Checked = value.Individual.LevelEnemy.Enabled;
                Individual.LevelEnemy.Set(value.Individual.LevelEnemy.Distribution);
                Individual.checkBox3.Checked = value.Individual.InternalLevel.Enabled;
                Individual.InternalLevel.Set(value.Individual.InternalLevel.Distribution);
                Individual.checkBox5.Checked = value.Individual.SupportCategory.Enabled;
                Individual.SupportCategory.Set(value.Individual.SupportCategory.Distribution);
                Individual.checkBox6.Checked = value.Individual.SkillPoint.Enabled;
                Individual.SkillPoint.Set(value.Individual.SkillPoint.Distribution);
                Individual.checkBox7.Checked = value.Individual.Aptitude.Enabled;
                Individual.Aptitude.Set(value.Individual.Aptitude.Distribution);
                Individual.checkBox8.Checked = value.Individual.AptitudeCount.Enabled;
                Individual.AptitudeCount.Set(value.Individual.AptitudeCount.Distribution);
                Individual.checkBox10.Checked = value.Individual.SubAptitude.Enabled;
                Individual.SubAptitude.Set(value.Individual.SubAptitude.Distribution);
                Individual.checkBox9.Checked = value.Individual.SubAptitudeCount.Enabled;
                Individual.SubAptitudeCount.Set(value.Individual.SubAptitudeCount.Distribution);
                Individual.checkBox11.Checked = value.Individual.RandomizeAllyBases;
                Individual.OffsetNHpAlly.Set(value.Individual.OffsetNHpAlly.Distribution);
                Individual.OffsetNStrAlly.Set(value.Individual.OffsetNStrAlly.Distribution);
                Individual.OffsetNTechAlly.Set(value.Individual.OffsetNTechAlly.Distribution);
                Individual.OffsetNQuickAlly.Set(value.Individual.OffsetNQuickAlly.Distribution);
                Individual.OffsetNLuckAlly.Set(value.Individual.OffsetNLuckAlly.Distribution);
                Individual.OffsetNDefAlly.Set(value.Individual.OffsetNDefAlly.Distribution);
                Individual.OffsetNMagicAlly.Set(value.Individual.OffsetNMagicAlly.Distribution);
                Individual.OffsetNMdefAlly.Set(value.Individual.OffsetNMdefAlly.Distribution);
                Individual.OffsetNPhysAlly.Set(value.Individual.OffsetNPhysAlly.Distribution);
                Individual.OffsetNSightAlly.Set(value.Individual.OffsetNSightAlly.Distribution);
                Individual.OffsetNMoveAlly.Set(value.Individual.OffsetNMoveAlly.Distribution);
                Individual.checkBox13.Checked = value.Individual.OffsetNTotalAlly.Enabled;
                Individual.OffsetNTotalAlly.Set(value.Individual.OffsetNTotalAlly.Distribution);
                Individual.checkBox21.Checked = value.Individual.StrongerProtagonist;
                Individual.checkBox12.Checked = value.Individual.StrongerAllyNPCs;
                Individual.checkBox16.Checked = value.Individual.RandomizeEnemyBasesNormal;
                Individual.OffsetNHpEnemy.Set(value.Individual.OffsetNHpEnemy.Distribution);
                Individual.OffsetNStrEnemy.Set(value.Individual.OffsetNStrEnemy.Distribution);
                Individual.OffsetNTechEnemy.Set(value.Individual.OffsetNTechEnemy.Distribution);
                Individual.OffsetNQuickEnemy.Set(value.Individual.OffsetNQuickEnemy.Distribution);
                Individual.OffsetNLuckEnemy.Set(value.Individual.OffsetNLuckEnemy.Distribution);
                Individual.OffsetNDefEnemy.Set(value.Individual.OffsetNDefEnemy.Distribution);
                Individual.OffsetNMagicEnemy.Set(value.Individual.OffsetNMagicEnemy.Distribution);
                Individual.OffsetNMdefEnemy.Set(value.Individual.OffsetNMdefEnemy.Distribution);
                Individual.OffsetNPhysEnemy.Set(value.Individual.OffsetNPhysEnemy.Distribution);
                Individual.OffsetNSightEnemy.Set(value.Individual.OffsetNSightEnemy.Distribution);
                Individual.OffsetNMoveEnemy.Set(value.Individual.OffsetNMoveEnemy.Distribution);
                Individual.checkBox15.Checked = value.Individual.OffsetNTotalEnemy.Enabled;
                Individual.OffsetNTotalEnemy.Set(value.Individual.OffsetNTotalEnemy.Distribution);
                Individual.checkBox17.Checked = value.Individual.RandomizeEnemyBasesHard;
                Individual.OffsetHHpEnemy.Set(value.Individual.OffsetHHpEnemy.Distribution);
                Individual.OffsetHStrEnemy.Set(value.Individual.OffsetHStrEnemy.Distribution);
                Individual.OffsetHTechEnemy.Set(value.Individual.OffsetHTechEnemy.Distribution);
                Individual.OffsetHQuickEnemy.Set(value.Individual.OffsetHQuickEnemy.Distribution);
                Individual.OffsetHLuckEnemy.Set(value.Individual.OffsetHLuckEnemy.Distribution);
                Individual.OffsetHDefEnemy.Set(value.Individual.OffsetHDefEnemy.Distribution);
                Individual.OffsetHMagicEnemy.Set(value.Individual.OffsetHMagicEnemy.Distribution);
                Individual.OffsetHMdefEnemy.Set(value.Individual.OffsetHMdefEnemy.Distribution);
                Individual.OffsetHPhysEnemy.Set(value.Individual.OffsetHPhysEnemy.Distribution);
                Individual.OffsetHSightEnemy.Set(value.Individual.OffsetHSightEnemy.Distribution);
                Individual.OffsetHMoveEnemy.Set(value.Individual.OffsetHMoveEnemy.Distribution);
                Individual.checkBox14.Checked = value.Individual.OffsetHTotalEnemy.Enabled;
                Individual.OffsetHTotalEnemy.Set(value.Individual.OffsetHTotalEnemy.Distribution);
                Individual.checkBox19.Checked = value.Individual.RandomizeEnemyBasesMaddening;
                Individual.OffsetLHpEnemy.Set(value.Individual.OffsetLHpEnemy.Distribution);
                Individual.OffsetLStrEnemy.Set(value.Individual.OffsetLStrEnemy.Distribution);
                Individual.OffsetLTechEnemy.Set(value.Individual.OffsetLTechEnemy.Distribution);
                Individual.OffsetLQuickEnemy.Set(value.Individual.OffsetLQuickEnemy.Distribution);
                Individual.OffsetLLuckEnemy.Set(value.Individual.OffsetLLuckEnemy.Distribution);
                Individual.OffsetLDefEnemy.Set(value.Individual.OffsetLDefEnemy.Distribution);
                Individual.OffsetLMagicEnemy.Set(value.Individual.OffsetLMagicEnemy.Distribution);
                Individual.OffsetLMdefEnemy.Set(value.Individual.OffsetLMdefEnemy.Distribution);
                Individual.OffsetLPhysEnemy.Set(value.Individual.OffsetLPhysEnemy.Distribution);
                Individual.OffsetLSightEnemy.Set(value.Individual.OffsetLSightEnemy.Distribution);
                Individual.OffsetLMoveEnemy.Set(value.Individual.OffsetLMoveEnemy.Distribution);
                Individual.checkBox18.Checked = value.Individual.OffsetLTotalEnemy.Enabled;
                Individual.OffsetLTotalEnemy.Set(value.Individual.OffsetLTotalEnemy.Distribution);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!InitFromConfig())
                if (!InitFromInput())
                {
                    Close();
                    return;
                }
            RandomizerSettings? rs = XmlParser.ReadRandomizerSettings();
            if (rs != null)
                try { RandomizerSettings = rs; }
                catch (NullReferenceException)
                { ShowSettingsLoadError(); }
                catch (ArgumentOutOfRangeException)
                { ShowSettingsLoadError(); }
            Activate();
        }

        private bool InitFromConfig()
        {
            string dumpPath = ConfigurationManager.AppSettings["dumpPath"]!;
            return GlobalData.FM.TryInitialize(dumpPath, out _);
        }

        private bool InitFromInput()
        {
            if (LoadDumpDialog() == DialogResult.Cancel &&
                RetryLoadDumpDialog() == DialogResult.No)
            {
                Close();
                return false;
            }

            string? romfsDir;

            while (true)
            {
                romfsDir = GetRomfsDirDialog();
                string? error = null;

                if (romfsDir == null || !GlobalData.FM.TryInitialize(romfsDir, out error))
                {
                    if (error != null)
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (RetryLoadDumpDialog() == DialogResult.Yes)
                        continue;

                    Close();
                    return false;
                }
                break;
            }

            Configuration c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            c.AppSettings.Settings["dumpPath"].Value = romfsDir;
            c.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(c.AppSettings.SectionInformation.Name);
            return true;
        }

        public static void CancelFormClosing(object? sender, FormClosingEventArgs e)
        {
            ((Form)sender!).Hide();
            e.Cancel = true;
        }

        private void RandomizeAndExportButton_Click(object sender, EventArgs e)
        {
            if (rememberSettingsCheckBox.Checked)
                XmlParser.WriteRandomizerSettings(RandomizerSettings);
            else
                FileManager.DeleteRandomizerSettings();
            GlobalData.R.Randomize(RandomizerSettings);
            bool success = GlobalData.Export();
            if (success)
            {
                ExportSuccessMessage();
                Close();
            }
            else
                NoChangesMessage();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rememberSettingsCheckBox.Checked)
                XmlParser.WriteRandomizerSettings(RandomizerSettings);
            else
                FileManager.DeleteRandomizerSettings();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GodGeneral.Show();
            GodGeneral.Activate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GrowthTable.Show();
            GrowthTable.Activate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            BondLevel.Show();
            BondLevel.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AssetTable.Show();
            AssetTable.Activate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Individual.Show();
            Individual.Activate();
        }
    }
}