using ALittleSecretIngredient.Structs;
using System.Configuration;
using System.Runtime.Serialization;

namespace ALittleSecretIngredient.Forms
{
    public partial class MainForm : Form
    {
        private GlobalData GlobalData { get; }
        private AssetTableForm AssetTable { get; set; }
        private GodGeneralForm GodGeneral { get; set; }
        private GrowthTableForm GrowthTable { get; set; }
        private TypeOfSoldierForm TypeOfSoldier { get; set; }
        private BondLevelForm BondLevel { get; set; }
        private IndividualForm Individual { get; set; }
        private ArrangementForm Arrangement { get; set; }
        public MainForm()
        {
            GlobalData = new();
            InitializeComponent();
            AssetTable = new(GlobalData);
            GodGeneral = new(GlobalData);
            GrowthTable = new(GlobalData);
            BondLevel = new(GlobalData);
            TypeOfSoldier = new(GlobalData);
            Individual = new(GlobalData);
            Arrangement = new(GlobalData);
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
            MessageBox.Show("The Randomizer mod has been successfully exported and deposited into the 'Output' folder, which " +
                "is *positioned* alongside my executable.",
                "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void NoChangesMessage()
        {
            MessageBox.Show("It appears that there are no changes made. Could it be that you forgot to activate any of the " +
                "*randomization* options?",
                "No Options Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void NoExportTargetsMessage()
        {
            MessageBox.Show("The export of a mod cannot be executed if no format has been selected. Kindly ensure that you " +
                "choose at *least* one option.",
                "No Export Formats Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private RandomizerSettings RandomizerSettings
        {
            get
            {
                RandomizerSettings rs = new()
                {
                    Remember = rememberSettingsCheckBox.Checked,
                    SaveChangelog = saveChangelogCheckBox.Checked,
                    ExportCobalt = exportCobaltCheckBox.Checked,
                    ExportLayeredFS = exportLayeredFSCheckBox.Checked,
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
                /* Removed
                rs.GodGeneral.ShuffleGrowTableAlly = GodGeneral.checkBox8.Checked;
                rs.GodGeneral.ShuffleGrowTableEnemy = GodGeneral.checkBox10.Checked;
                */
                rs.GodGeneral.RandomizeEngravingStats = GodGeneral.checkBox11.Checked;
                rs.GodGeneral.EngravePower = new(false,
                    GodGeneral.EngravePower.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveWeight = new(false,
                    GodGeneral.EngraveWeight.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveHit = new(false,
                    GodGeneral.EngraveHit.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveCritical = new(false,
                    GodGeneral.EngraveCritical.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveAvoid = new(false,
                    GodGeneral.EngraveAvoid.Get(), Array.Empty<object>());
                rs.GodGeneral.EngraveSecure = new(false,
                    GodGeneral.EngraveSecure.Get(), Array.Empty<object>());
                rs.GodGeneral.RandomizeAllyStaticSyncStats = GodGeneral.checkBox17.Checked;
                rs.GodGeneral.SynchroEnhanceHpAlly = new(false,
                    GodGeneral.SynchroEnhanceHpAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceStrAlly = new(false,
                    GodGeneral.SynchroEnhanceStrAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceTechAlly = new(false,
                    GodGeneral.SynchroEnhanceTechAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceQuickAlly = new(false,
                    GodGeneral.SynchroEnhanceQuickAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceLuckAlly = new(false,
                    GodGeneral.SynchroEnhanceLuckAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceDefAlly = new(false,
                    GodGeneral.SynchroEnhanceDefAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMagicAlly = new(false,
                    GodGeneral.SynchroEnhanceMagicAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMdefAlly = new(false,
                    GodGeneral.SynchroEnhanceMdefAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhancePhysAlly = new(false,
                    GodGeneral.SynchroEnhancePhysAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMoveAlly = new(false,
                    GodGeneral.SynchroEnhanceMoveAlly.Get(), Array.Empty<object>());
                rs.GodGeneral.RandomizeEnemyStaticSyncStats = GodGeneral.checkBox12.Checked;
                rs.GodGeneral.SynchroEnhanceHpEnemy = new(false,
                    GodGeneral.SynchroEnhanceHpEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceStrEnemy = new(false,
                    GodGeneral.SynchroEnhanceStrEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceTechEnemy = new(false,
                    GodGeneral.SynchroEnhanceTechEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceQuickEnemy = new(false,
                    GodGeneral.SynchroEnhanceQuickEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceLuckEnemy = new(false,
                    GodGeneral.SynchroEnhanceLuckEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceDefEnemy = new(false,
                    GodGeneral.SynchroEnhanceDefEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMagicEnemy = new(false,
                    GodGeneral.SynchroEnhanceMagicEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMdefEnemy = new(false,
                    GodGeneral.SynchroEnhanceMdefEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhancePhysEnemy = new(false,
                    GodGeneral.SynchroEnhancePhysEnemy.Get(), Array.Empty<object>());
                rs.GodGeneral.SynchroEnhanceMoveEnemy = new(false,
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
                /* Removed
                rs.GrowthTable.EngageItemsCount = new(GrowthTable.checkBox10.Checked,
                    GrowthTable.EngageItemsCount.Get(), Array.Empty<object>());
                */
                rs.GrowthTable.Aptitude = new(GrowthTable.checkBox18.Checked,
                    GrowthTable.Aptitude.Get(), Array.Empty<object>());
                rs.GrowthTable.AptitudeCount = new(GrowthTable.checkBox17.Checked,
                    GrowthTable.AptitudeCount.Get(), Array.Empty<object>());
                /* Removed
                rs.GrowthTable.SkillInheritanceLevel = new(GrowthTable.checkBox20.Checked,
                    GrowthTable.SkillInheritanceLevel.Get(), Array.Empty<object>());
                */
                rs.GrowthTable.StrongBondLevel = new(GrowthTable.checkBox19.Checked,
                    GrowthTable.StrongBondLevel.Get(), Array.Empty<object>());
                rs.GrowthTable.DeepSynergyLevel = new(GrowthTable.checkBox21.Checked,
                    GrowthTable.DeepSynergyLevel.Get(), Array.Empty<object>());

                rs.BondLevel.Exp = new(BondLevel.checkBox20.Checked,
                    BondLevel.Exp.Get(), Array.Empty<object>());
                rs.BondLevel.Cost = new(BondLevel.checkBox1.Checked,
                    BondLevel.Cost.Get(), Array.Empty<object>());

                rs.TypeOfSoldier.StyleName = new(TypeOfSoldier.checkBox20.Checked, TypeOfSoldier.StyleName.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.MoveType = new(TypeOfSoldier.checkBox1.Checked, TypeOfSoldier.MoveType.Get(), new object[] { TypeOfSoldier.checkBox2.Checked });
                rs.TypeOfSoldier.Weapon = new(TypeOfSoldier.checkBox3.Checked, TypeOfSoldier.Weapon.Get(), new object[] { TypeOfSoldier.checkBox5.Checked });
                rs.TypeOfSoldier.RandomizeWeaponTypeCount = TypeOfSoldier.checkBox4.Checked;
                rs.TypeOfSoldier.WeaponBaseCount = new(false, TypeOfSoldier.WeaponBaseCount.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.WeaponAdvancedCount = new(false, TypeOfSoldier.WeaponAdvancedCount.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.RandomizeWeaponRank = new(TypeOfSoldier.checkBox6.Checked, null!, new object[] { TypeOfSoldier.checkBox7.Checked });
                rs.TypeOfSoldier.MaxWeaponLevelBase = new(false, TypeOfSoldier.MaxWeaponLevelBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.MaxWeaponLevelAdvanced = new(false, TypeOfSoldier.MaxWeaponLevelAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.RandomizeBaseStats = new(TypeOfSoldier.checkBox9.Checked, null!, new object[] { TypeOfSoldier.checkBox8.Checked });
                rs.TypeOfSoldier.BaseHpBase = new(false, TypeOfSoldier.BaseHpBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseStrBase = new(false, TypeOfSoldier.BaseStrBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseTechBase = new(false, TypeOfSoldier.BaseTechBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseQuickBase = new(false, TypeOfSoldier.BaseQuickBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseLuckBase = new(false, TypeOfSoldier.BaseLuckBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseDefBase = new(false, TypeOfSoldier.BaseDefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseMagicBase = new(false, TypeOfSoldier.BaseMagicBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseMdefBase = new(false, TypeOfSoldier.BaseMdefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BasePhysBase = new(false, TypeOfSoldier.BasePhysBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseSightBase = new(false, TypeOfSoldier.BaseSightBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseMoveBase = new(false, TypeOfSoldier.BaseMoveBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseHpAdvanced = new(false, TypeOfSoldier.BaseHpAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseStrAdvanced = new(false, TypeOfSoldier.BaseStrAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseTechAdvanced = new(false, TypeOfSoldier.BaseTechAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseQuickAdvanced = new(false, TypeOfSoldier.BaseQuickAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseLuckAdvanced = new(false, TypeOfSoldier.BaseLuckAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseDefAdvanced = new(false, TypeOfSoldier.BaseDefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseMagicAdvanced = new(false, TypeOfSoldier.BaseMagicAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseMdefAdvanced = new(false, TypeOfSoldier.BaseMdefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BasePhysAdvanced = new(false, TypeOfSoldier.BasePhysAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseSightAdvanced = new(false, TypeOfSoldier.BaseSightAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseMoveAdvanced = new(false, TypeOfSoldier.BaseMoveAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseTotalBase = new(TypeOfSoldier.checkBox8.Checked, TypeOfSoldier.BaseTotalBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseTotalAdvanced = new(TypeOfSoldier.checkBox8.Checked, TypeOfSoldier.BaseTotalAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.RandomizeStatLimits = new(TypeOfSoldier.checkBox11.Checked, null!, new object[] { TypeOfSoldier.checkBox10.Checked });
                rs.TypeOfSoldier.LimitHpBase = new(false, TypeOfSoldier.LimitHpBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitStrBase = new(false, TypeOfSoldier.LimitStrBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitTechBase = new(false, TypeOfSoldier.LimitTechBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitQuickBase = new(false, TypeOfSoldier.LimitQuickBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitLuckBase = new(false, TypeOfSoldier.LimitLuckBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitDefBase = new(false, TypeOfSoldier.LimitDefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitMagicBase = new(false, TypeOfSoldier.LimitMagicBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitMdefBase = new(false, TypeOfSoldier.LimitMdefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitPhysBase = new(false, TypeOfSoldier.LimitPhysBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitSightBase = new(false, TypeOfSoldier.LimitSightBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitMoveBase = new(false, TypeOfSoldier.LimitMoveBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitHpAdvanced = new(false, TypeOfSoldier.LimitHpAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitStrAdvanced = new(false, TypeOfSoldier.LimitStrAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitTechAdvanced = new(false, TypeOfSoldier.LimitTechAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitQuickAdvanced = new(false, TypeOfSoldier.LimitQuickAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitLuckAdvanced = new(false, TypeOfSoldier.LimitLuckAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitDefAdvanced = new(false, TypeOfSoldier.LimitDefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitMagicAdvanced = new(false, TypeOfSoldier.LimitMagicAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitMdefAdvanced = new(false, TypeOfSoldier.LimitMdefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitPhysAdvanced = new(false, TypeOfSoldier.LimitPhysAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitSightAdvanced = new(false, TypeOfSoldier.LimitSightAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitMoveAdvanced = new(false, TypeOfSoldier.LimitMoveAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitTotalBase = new(TypeOfSoldier.checkBox10.Checked, TypeOfSoldier.LimitTotalBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LimitTotalAdvanced = new(TypeOfSoldier.checkBox10.Checked, TypeOfSoldier.LimitTotalAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.RandomizeEnemyStatGrows = new(TypeOfSoldier.checkBox13.Checked, null!, new object[] { TypeOfSoldier.checkBox12.Checked });
                rs.TypeOfSoldier.BaseGrowHpBase = new(false, TypeOfSoldier.BaseGrowHpBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowStrBase = new(false, TypeOfSoldier.BaseGrowStrBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowTechBase = new(false, TypeOfSoldier.BaseGrowTechBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowQuickBase = new(false, TypeOfSoldier.BaseGrowQuickBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowLuckBase = new(false, TypeOfSoldier.BaseGrowLuckBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowDefBase = new(false, TypeOfSoldier.BaseGrowDefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowMagicBase = new(false, TypeOfSoldier.BaseGrowMagicBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowMdefBase = new(false, TypeOfSoldier.BaseGrowMdefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowPhysBase = new(false, TypeOfSoldier.BaseGrowPhysBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowSightBase = new(false, TypeOfSoldier.BaseGrowSightBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowMoveBase = new(false, TypeOfSoldier.BaseGrowMoveBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowHpAdvanced = new(false, TypeOfSoldier.BaseGrowHpAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowStrAdvanced = new(false, TypeOfSoldier.BaseGrowStrAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowTechAdvanced = new(false, TypeOfSoldier.BaseGrowTechAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowQuickAdvanced = new(false, TypeOfSoldier.BaseGrowQuickAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowLuckAdvanced = new(false, TypeOfSoldier.BaseGrowLuckAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowDefAdvanced = new(false, TypeOfSoldier.BaseGrowDefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowMagicAdvanced = new(false, TypeOfSoldier.BaseGrowMagicAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowMdefAdvanced = new(false, TypeOfSoldier.BaseGrowMdefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowPhysAdvanced = new(false, TypeOfSoldier.BaseGrowPhysAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowSightAdvanced = new(false, TypeOfSoldier.BaseGrowSightAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowMoveAdvanced = new(false, TypeOfSoldier.BaseGrowMoveAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowTotalBase = new(TypeOfSoldier.checkBox12.Checked, TypeOfSoldier.BaseGrowTotalBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.BaseGrowTotalAdvanced = new(TypeOfSoldier.checkBox12.Checked, TypeOfSoldier.BaseGrowTotalAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.RandomizeStatGrowthModifiers = new(TypeOfSoldier.checkBox15.Checked, null!, new object[] { TypeOfSoldier.checkBox14.Checked });
                rs.TypeOfSoldier.DiffGrowHpBase = new(false, TypeOfSoldier.DiffGrowHpBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowStrBase = new(false, TypeOfSoldier.DiffGrowStrBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowTechBase = new(false, TypeOfSoldier.DiffGrowTechBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowQuickBase = new(false, TypeOfSoldier.DiffGrowQuickBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowLuckBase = new(false, TypeOfSoldier.DiffGrowLuckBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowDefBase = new(false, TypeOfSoldier.DiffGrowDefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowMagicBase = new(false, TypeOfSoldier.DiffGrowMagicBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowMdefBase = new(false, TypeOfSoldier.DiffGrowMdefBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowPhysBase = new(false, TypeOfSoldier.DiffGrowPhysBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowSightBase = new(false, TypeOfSoldier.DiffGrowSightBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowMoveBase = new(false, TypeOfSoldier.DiffGrowMoveBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowHpAdvanced = new(false, TypeOfSoldier.DiffGrowHpAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowStrAdvanced = new(false, TypeOfSoldier.DiffGrowStrAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowTechAdvanced = new(false, TypeOfSoldier.DiffGrowTechAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowQuickAdvanced = new(false, TypeOfSoldier.DiffGrowQuickAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowLuckAdvanced = new(false, TypeOfSoldier.DiffGrowLuckAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowDefAdvanced = new(false, TypeOfSoldier.DiffGrowDefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowMagicAdvanced = new(false, TypeOfSoldier.DiffGrowMagicAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowMdefAdvanced = new(false, TypeOfSoldier.DiffGrowMdefAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowPhysAdvanced = new(false, TypeOfSoldier.DiffGrowPhysAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowSightAdvanced = new(false, TypeOfSoldier.DiffGrowSightAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowMoveAdvanced = new(false, TypeOfSoldier.DiffGrowMoveAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowTotalBase = new(TypeOfSoldier.checkBox14.Checked, TypeOfSoldier.DiffGrowTotalBase.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.DiffGrowTotalAdvanced = new(TypeOfSoldier.checkBox14.Checked, TypeOfSoldier.DiffGrowTotalAdvanced.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LearningSkill = new(TypeOfSoldier.checkBox17.Checked, TypeOfSoldier.LearningSkill.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.LunaticSkill = new(TypeOfSoldier.checkBox16.Checked, TypeOfSoldier.LunaticSkill.Get(), Array.Empty<object>());
                rs.TypeOfSoldier.Attrs = new(TypeOfSoldier.checkBox18.Checked, TypeOfSoldier.Attrs.Get(), new object[] { TypeOfSoldier.checkBox21.Checked });
                rs.TypeOfSoldier.AttrsCount = new(TypeOfSoldier.checkBox19.Checked, TypeOfSoldier.AttrsCount.Get(), Array.Empty<object>());

                rs.Individual.JidAlly = new(Individual.checkBox22.Checked, Individual.JidAlly.Get(), new object[] { Individual.checkBox23.Checked });
                rs.Individual.JidEnemy = new(Individual.checkBox24.Checked, Individual.JidEnemy.Get(), new object[] { Individual.checkBox26.Checked });
                rs.Individual.ForceUsableWeapon = Individual.checkBox30.Checked;
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
                rs.Individual.RandomizeStatLimits = Individual.checkBox25.Checked;
                rs.Individual.LimitHp = new(false, Individual.LimitHp.Get(), Array.Empty<object>());
                rs.Individual.LimitStr = new(false, Individual.LimitStr.Get(), Array.Empty<object>());
                rs.Individual.LimitTech = new(false, Individual.LimitTech.Get(), Array.Empty<object>());
                rs.Individual.LimitQuick = new(false, Individual.LimitQuick.Get(), Array.Empty<object>());
                rs.Individual.LimitLuck = new(false, Individual.LimitLuck.Get(), Array.Empty<object>());
                rs.Individual.LimitDef = new(false, Individual.LimitDef.Get(), Array.Empty<object>());
                rs.Individual.LimitMagic = new(false, Individual.LimitMagic.Get(), Array.Empty<object>());
                rs.Individual.LimitMdef = new(false, Individual.LimitMdef.Get(), Array.Empty<object>());
                rs.Individual.LimitPhys = new(false, Individual.LimitPhys.Get(), Array.Empty<object>());
                rs.Individual.LimitSight = new(false, Individual.LimitSight.Get(), Array.Empty<object>());
                rs.Individual.LimitMove = new(false, Individual.LimitMove.Get(), Array.Empty<object>());
                rs.Individual.RandomizeAllyStatGrowths = Individual.checkBox28.Checked;
                rs.Individual.GrowHp = new(false, Individual.GrowHp.Get(), Array.Empty<object>());
                rs.Individual.GrowStr = new(false, Individual.GrowStr.Get(), Array.Empty<object>());
                rs.Individual.GrowTech = new(false, Individual.GrowTech.Get(), Array.Empty<object>());
                rs.Individual.GrowQuick = new(false, Individual.GrowQuick.Get(), Array.Empty<object>());
                rs.Individual.GrowLuck = new(false, Individual.GrowLuck.Get(), Array.Empty<object>());
                rs.Individual.GrowDef = new(false, Individual.GrowDef.Get(), Array.Empty<object>());
                rs.Individual.GrowMagic = new(false, Individual.GrowMagic.Get(), Array.Empty<object>());
                rs.Individual.GrowMdef = new(false, Individual.GrowMdef.Get(), Array.Empty<object>());
                rs.Individual.GrowPhys = new(false, Individual.GrowPhys.Get(), Array.Empty<object>());
                rs.Individual.GrowSight = new(false, Individual.GrowSight.Get(), Array.Empty<object>());
                rs.Individual.GrowMove = new(false, Individual.GrowMove.Get(), Array.Empty<object>());
                rs.Individual.GrowTotal = new(Individual.checkBox27.Checked, Individual.GrowTotal.Get(), Array.Empty<object>());
                rs.Individual.RandomizeEnemyStatGrowths = Individual.checkBox29.Checked;
                rs.Individual.ItemsWeapons = new(Individual.checkBox31.Checked, Individual.ItemsWeapons.Get(),
                    new object[] { Individual.checkBox33.Checked, Individual.checkBox44.Checked });
                rs.Individual.ItemsWeaponCount = new(Individual.checkBox34.Checked, Individual.ItemsWeaponCount.Get(), Array.Empty<object>());
                rs.Individual.ItemsItems = new(Individual.checkBox32.Checked, Individual.ItemsItems.Get(), Array.Empty<object>());
                rs.Individual.ItemsItemCount = new(Individual.checkBox35.Checked, Individual.ItemsItemCount.Get(), Array.Empty<object>());
                rs.Individual.RandomizeEnemyInventories = Individual.checkBox36.Checked;
                rs.Individual.AttrsAlly = new(Individual.checkBox38.Checked, Individual.AttrsAlly.Get(), Array.Empty<object>());
                rs.Individual.AttrsAllyCount = new(Individual.checkBox37.Checked, Individual.AttrsAllyCount.Get(), Array.Empty<object>());
                rs.Individual.AttrsEnemy = new(Individual.checkBox40.Checked, Individual.AttrsEnemy.Get(), Array.Empty<object>());
                rs.Individual.AttrsEnemyCount = new(Individual.checkBox39.Checked, Individual.AttrsEnemyCount.Get(), Array.Empty<object>());
                rs.Individual.CommonSids = new(Individual.checkBox42.Checked, Individual.CommonSids.Get(),
                    new object[] { Individual.checkBox43.Checked, Individual.checkBox45.Checked });
                rs.Individual.CommonSidsCount = new(Individual.checkBox41.Checked, Individual.CommonSidsCount.Get(), Array.Empty<object>());

                rs.Arrangement.DeploymentSlots = new(Arrangement.checkBox2.Checked, Arrangement.DeploymentSlots.Get(), new object[] { Arrangement.checkBox1.Checked });
                rs.Arrangement.EnemyCount = new(Arrangement.checkBox4.Checked, Arrangement.EnemyCount.Get(), Array.Empty<object>());

                return rs;
            }
            set
            {
                rememberSettingsCheckBox.Checked = value.Remember;
                saveChangelogCheckBox.Checked = value.SaveChangelog;
                exportCobaltCheckBox.Checked = value.ExportCobalt;
                exportLayeredFSCheckBox.Checked = value.ExportLayeredFS;

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
                /* Removed
                GodGeneral.checkBox8.Checked = value.GodGeneral.ShuffleGrowTableAlly;
                GodGeneral.checkBox10.Checked = value.GodGeneral.ShuffleGrowTableEnemy;
                */
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
                /* Removed
                GrowthTable.checkBox10.Checked = value.GrowthTable.EngageItemsCount.Enabled;
                GrowthTable.EngageItemsCount.Set(value.GrowthTable.EngageItemsCount.Distribution);
                */
                GrowthTable.checkBox16.Checked = value.GrowthTable.EngageItems.GetArg<bool>(0);
                GrowthTable.numericUpDown1.Value = (decimal)value.GrowthTable.EngageItems.GetArg<double>(1);
                GrowthTable.checkBox18.Checked = value.GrowthTable.Aptitude.Enabled;
                GrowthTable.Aptitude.Set(value.GrowthTable.Aptitude.Distribution);
                GrowthTable.checkBox17.Checked = value.GrowthTable.AptitudeCount.Enabled;
                GrowthTable.AptitudeCount.Set(value.GrowthTable.AptitudeCount.Distribution);
                /* Removed
                GrowthTable.checkBox20.Checked = value.GrowthTable.SkillInheritanceLevel.Enabled;
                GrowthTable.SkillInheritanceLevel.Set(value.GrowthTable.SkillInheritanceLevel.Distribution);
                */
                GrowthTable.checkBox19.Checked = value.GrowthTable.StrongBondLevel.Enabled;
                GrowthTable.StrongBondLevel.Set(value.GrowthTable.StrongBondLevel.Distribution);
                GrowthTable.checkBox21.Checked = value.GrowthTable.DeepSynergyLevel.Enabled;
                GrowthTable.DeepSynergyLevel.Set(value.GrowthTable.DeepSynergyLevel.Distribution);

                BondLevel.checkBox20.Checked = value.BondLevel.Exp.Enabled;
                BondLevel.Exp.Set(value.BondLevel.Exp.Distribution);
                BondLevel.checkBox1.Checked = value.BondLevel.Cost.Enabled;
                BondLevel.Cost.Set(value.BondLevel.Cost.Distribution);

                TypeOfSoldier.checkBox20.Checked = value.TypeOfSoldier.StyleName.Enabled;
                TypeOfSoldier.StyleName.Set(value.TypeOfSoldier.StyleName.Distribution);
                TypeOfSoldier.checkBox1.Checked = value.TypeOfSoldier.MoveType.Enabled;
                TypeOfSoldier.MoveType.Set(value.TypeOfSoldier.MoveType.Distribution);
                TypeOfSoldier.checkBox2.Checked = value.TypeOfSoldier.MoveType.GetArg<bool>(0);
                TypeOfSoldier.checkBox3.Checked = value.TypeOfSoldier.Weapon.Enabled;
                TypeOfSoldier.Weapon.Set(value.TypeOfSoldier.Weapon.Distribution);
                TypeOfSoldier.checkBox5.Checked = value.TypeOfSoldier.Weapon.GetArg<bool>(0);
                TypeOfSoldier.checkBox4.Checked = value.TypeOfSoldier.RandomizeWeaponTypeCount;
                TypeOfSoldier.WeaponBaseCount.Set(value.TypeOfSoldier.WeaponBaseCount.Distribution);
                TypeOfSoldier.WeaponAdvancedCount.Set(value.TypeOfSoldier.WeaponAdvancedCount.Distribution);
                TypeOfSoldier.checkBox6.Checked = value.TypeOfSoldier.RandomizeWeaponRank.Enabled;
                TypeOfSoldier.checkBox7.Checked = value.TypeOfSoldier.RandomizeWeaponRank.GetArg<bool>(0);
                TypeOfSoldier.MaxWeaponLevelBase.Set(value.TypeOfSoldier.MaxWeaponLevelBase.Distribution);
                TypeOfSoldier.MaxWeaponLevelAdvanced.Set(value.TypeOfSoldier.MaxWeaponLevelAdvanced.Distribution);
                TypeOfSoldier.checkBox9.Checked = value.TypeOfSoldier.RandomizeBaseStats.Enabled;
                TypeOfSoldier.checkBox8.Checked = value.TypeOfSoldier.RandomizeBaseStats.GetArg<bool>(0);
                TypeOfSoldier.BaseHpBase.Set(value.TypeOfSoldier.BaseHpBase.Distribution);
                TypeOfSoldier.BaseStrBase.Set(value.TypeOfSoldier.BaseStrBase.Distribution);
                TypeOfSoldier.BaseTechBase.Set(value.TypeOfSoldier.BaseTechBase.Distribution);
                TypeOfSoldier.BaseQuickBase.Set(value.TypeOfSoldier.BaseQuickBase.Distribution);
                TypeOfSoldier.BaseLuckBase.Set(value.TypeOfSoldier.BaseLuckBase.Distribution);
                TypeOfSoldier.BaseDefBase.Set(value.TypeOfSoldier.BaseDefBase.Distribution);
                TypeOfSoldier.BaseMagicBase.Set(value.TypeOfSoldier.BaseMagicBase.Distribution);
                TypeOfSoldier.BaseMdefBase.Set(value.TypeOfSoldier.BaseMdefBase.Distribution);
                TypeOfSoldier.BasePhysBase.Set(value.TypeOfSoldier.BasePhysBase.Distribution);
                TypeOfSoldier.BaseSightBase.Set(value.TypeOfSoldier.BaseSightBase.Distribution);
                TypeOfSoldier.BaseMoveBase.Set(value.TypeOfSoldier.BaseMoveBase.Distribution);
                TypeOfSoldier.BaseHpAdvanced.Set(value.TypeOfSoldier.BaseHpAdvanced.Distribution);
                TypeOfSoldier.BaseStrAdvanced.Set(value.TypeOfSoldier.BaseStrAdvanced.Distribution);
                TypeOfSoldier.BaseTechAdvanced.Set(value.TypeOfSoldier.BaseTechAdvanced.Distribution);
                TypeOfSoldier.BaseQuickAdvanced.Set(value.TypeOfSoldier.BaseQuickAdvanced.Distribution);
                TypeOfSoldier.BaseLuckAdvanced.Set(value.TypeOfSoldier.BaseLuckAdvanced.Distribution);
                TypeOfSoldier.BaseDefAdvanced.Set(value.TypeOfSoldier.BaseDefAdvanced.Distribution);
                TypeOfSoldier.BaseMagicAdvanced.Set(value.TypeOfSoldier.BaseMagicAdvanced.Distribution);
                TypeOfSoldier.BaseMdefAdvanced.Set(value.TypeOfSoldier.BaseMdefAdvanced.Distribution);
                TypeOfSoldier.BasePhysAdvanced.Set(value.TypeOfSoldier.BasePhysAdvanced.Distribution);
                TypeOfSoldier.BaseSightAdvanced.Set(value.TypeOfSoldier.BaseSightAdvanced.Distribution);
                TypeOfSoldier.BaseMoveAdvanced.Set(value.TypeOfSoldier.BaseMoveAdvanced.Distribution);
                TypeOfSoldier.BaseTotalBase.Set(value.TypeOfSoldier.BaseTotalBase.Distribution);
                TypeOfSoldier.BaseTotalAdvanced.Set(value.TypeOfSoldier.BaseTotalAdvanced.Distribution);
                TypeOfSoldier.checkBox11.Checked = value.TypeOfSoldier.RandomizeStatLimits.Enabled;
                TypeOfSoldier.checkBox10.Checked = value.TypeOfSoldier.RandomizeStatLimits.GetArg<bool>(0);
                TypeOfSoldier.LimitHpBase.Set(value.TypeOfSoldier.LimitHpBase.Distribution);
                TypeOfSoldier.LimitStrBase.Set(value.TypeOfSoldier.LimitStrBase.Distribution);
                TypeOfSoldier.LimitTechBase.Set(value.TypeOfSoldier.LimitTechBase.Distribution);
                TypeOfSoldier.LimitQuickBase.Set(value.TypeOfSoldier.LimitQuickBase.Distribution);
                TypeOfSoldier.LimitLuckBase.Set(value.TypeOfSoldier.LimitLuckBase.Distribution);
                TypeOfSoldier.LimitDefBase.Set(value.TypeOfSoldier.LimitDefBase.Distribution);
                TypeOfSoldier.LimitMagicBase.Set(value.TypeOfSoldier.LimitMagicBase.Distribution);
                TypeOfSoldier.LimitMdefBase.Set(value.TypeOfSoldier.LimitMdefBase.Distribution);
                TypeOfSoldier.LimitPhysBase.Set(value.TypeOfSoldier.LimitPhysBase.Distribution);
                TypeOfSoldier.LimitSightBase.Set(value.TypeOfSoldier.LimitSightBase.Distribution);
                TypeOfSoldier.LimitMoveBase.Set(value.TypeOfSoldier.LimitMoveBase.Distribution);
                TypeOfSoldier.LimitHpAdvanced.Set(value.TypeOfSoldier.LimitHpAdvanced.Distribution);
                TypeOfSoldier.LimitStrAdvanced.Set(value.TypeOfSoldier.LimitStrAdvanced.Distribution);
                TypeOfSoldier.LimitTechAdvanced.Set(value.TypeOfSoldier.LimitTechAdvanced.Distribution);
                TypeOfSoldier.LimitQuickAdvanced.Set(value.TypeOfSoldier.LimitQuickAdvanced.Distribution);
                TypeOfSoldier.LimitLuckAdvanced.Set(value.TypeOfSoldier.LimitLuckAdvanced.Distribution);
                TypeOfSoldier.LimitDefAdvanced.Set(value.TypeOfSoldier.LimitDefAdvanced.Distribution);
                TypeOfSoldier.LimitMagicAdvanced.Set(value.TypeOfSoldier.LimitMagicAdvanced.Distribution);
                TypeOfSoldier.LimitMdefAdvanced.Set(value.TypeOfSoldier.LimitMdefAdvanced.Distribution);
                TypeOfSoldier.LimitPhysAdvanced.Set(value.TypeOfSoldier.LimitPhysAdvanced.Distribution);
                TypeOfSoldier.LimitSightAdvanced.Set(value.TypeOfSoldier.LimitSightAdvanced.Distribution);
                TypeOfSoldier.LimitMoveAdvanced.Set(value.TypeOfSoldier.LimitMoveAdvanced.Distribution);
                TypeOfSoldier.LimitTotalBase.Set(value.TypeOfSoldier.LimitTotalBase.Distribution);
                TypeOfSoldier.LimitTotalAdvanced.Set(value.TypeOfSoldier.LimitTotalAdvanced.Distribution);
                TypeOfSoldier.checkBox13.Checked = value.TypeOfSoldier.RandomizeEnemyStatGrows.Enabled;
                TypeOfSoldier.checkBox12.Checked = value.TypeOfSoldier.RandomizeEnemyStatGrows.GetArg<bool>(0);
                TypeOfSoldier.BaseGrowHpBase.Set(value.TypeOfSoldier.BaseGrowHpBase.Distribution);
                TypeOfSoldier.BaseGrowStrBase.Set(value.TypeOfSoldier.BaseGrowStrBase.Distribution);
                TypeOfSoldier.BaseGrowTechBase.Set(value.TypeOfSoldier.BaseGrowTechBase.Distribution);
                TypeOfSoldier.BaseGrowQuickBase.Set(value.TypeOfSoldier.BaseGrowQuickBase.Distribution);
                TypeOfSoldier.BaseGrowLuckBase.Set(value.TypeOfSoldier.BaseGrowLuckBase.Distribution);
                TypeOfSoldier.BaseGrowDefBase.Set(value.TypeOfSoldier.BaseGrowDefBase.Distribution);
                TypeOfSoldier.BaseGrowMagicBase.Set(value.TypeOfSoldier.BaseGrowMagicBase.Distribution);
                TypeOfSoldier.BaseGrowMdefBase.Set(value.TypeOfSoldier.BaseGrowMdefBase.Distribution);
                TypeOfSoldier.BaseGrowPhysBase.Set(value.TypeOfSoldier.BaseGrowPhysBase.Distribution);
                TypeOfSoldier.BaseGrowSightBase.Set(value.TypeOfSoldier.BaseGrowSightBase.Distribution);
                TypeOfSoldier.BaseGrowMoveBase.Set(value.TypeOfSoldier.BaseGrowMoveBase.Distribution);
                TypeOfSoldier.BaseGrowHpAdvanced.Set(value.TypeOfSoldier.BaseGrowHpAdvanced.Distribution);
                TypeOfSoldier.BaseGrowStrAdvanced.Set(value.TypeOfSoldier.BaseGrowStrAdvanced.Distribution);
                TypeOfSoldier.BaseGrowTechAdvanced.Set(value.TypeOfSoldier.BaseGrowTechAdvanced.Distribution);
                TypeOfSoldier.BaseGrowQuickAdvanced.Set(value.TypeOfSoldier.BaseGrowQuickAdvanced.Distribution);
                TypeOfSoldier.BaseGrowLuckAdvanced.Set(value.TypeOfSoldier.BaseGrowLuckAdvanced.Distribution);
                TypeOfSoldier.BaseGrowDefAdvanced.Set(value.TypeOfSoldier.BaseGrowDefAdvanced.Distribution);
                TypeOfSoldier.BaseGrowMagicAdvanced.Set(value.TypeOfSoldier.BaseGrowMagicAdvanced.Distribution);
                TypeOfSoldier.BaseGrowMdefAdvanced.Set(value.TypeOfSoldier.BaseGrowMdefAdvanced.Distribution);
                TypeOfSoldier.BaseGrowPhysAdvanced.Set(value.TypeOfSoldier.BaseGrowPhysAdvanced.Distribution);
                TypeOfSoldier.BaseGrowSightAdvanced.Set(value.TypeOfSoldier.BaseGrowSightAdvanced.Distribution);
                TypeOfSoldier.BaseGrowMoveAdvanced.Set(value.TypeOfSoldier.BaseGrowMoveAdvanced.Distribution);
                TypeOfSoldier.BaseGrowTotalBase.Set(value.TypeOfSoldier.BaseGrowTotalBase.Distribution);
                TypeOfSoldier.BaseGrowTotalAdvanced.Set(value.TypeOfSoldier.BaseGrowTotalAdvanced.Distribution);
                TypeOfSoldier.checkBox15.Checked = value.TypeOfSoldier.RandomizeStatGrowthModifiers.Enabled;
                TypeOfSoldier.checkBox14.Checked = value.TypeOfSoldier.RandomizeStatGrowthModifiers.GetArg<bool>(0);
                TypeOfSoldier.DiffGrowHpBase.Set(value.TypeOfSoldier.DiffGrowHpBase.Distribution);
                TypeOfSoldier.DiffGrowStrBase.Set(value.TypeOfSoldier.DiffGrowStrBase.Distribution);
                TypeOfSoldier.DiffGrowTechBase.Set(value.TypeOfSoldier.DiffGrowTechBase.Distribution);
                TypeOfSoldier.DiffGrowQuickBase.Set(value.TypeOfSoldier.DiffGrowQuickBase.Distribution);
                TypeOfSoldier.DiffGrowLuckBase.Set(value.TypeOfSoldier.DiffGrowLuckBase.Distribution);
                TypeOfSoldier.DiffGrowDefBase.Set(value.TypeOfSoldier.DiffGrowDefBase.Distribution);
                TypeOfSoldier.DiffGrowMagicBase.Set(value.TypeOfSoldier.DiffGrowMagicBase.Distribution);
                TypeOfSoldier.DiffGrowMdefBase.Set(value.TypeOfSoldier.DiffGrowMdefBase.Distribution);
                TypeOfSoldier.DiffGrowPhysBase.Set(value.TypeOfSoldier.DiffGrowPhysBase.Distribution);
                TypeOfSoldier.DiffGrowSightBase.Set(value.TypeOfSoldier.DiffGrowSightBase.Distribution);
                TypeOfSoldier.DiffGrowMoveBase.Set(value.TypeOfSoldier.DiffGrowMoveBase.Distribution);
                TypeOfSoldier.DiffGrowHpAdvanced.Set(value.TypeOfSoldier.DiffGrowHpAdvanced.Distribution);
                TypeOfSoldier.DiffGrowStrAdvanced.Set(value.TypeOfSoldier.DiffGrowStrAdvanced.Distribution);
                TypeOfSoldier.DiffGrowTechAdvanced.Set(value.TypeOfSoldier.DiffGrowTechAdvanced.Distribution);
                TypeOfSoldier.DiffGrowQuickAdvanced.Set(value.TypeOfSoldier.DiffGrowQuickAdvanced.Distribution);
                TypeOfSoldier.DiffGrowLuckAdvanced.Set(value.TypeOfSoldier.DiffGrowLuckAdvanced.Distribution);
                TypeOfSoldier.DiffGrowDefAdvanced.Set(value.TypeOfSoldier.DiffGrowDefAdvanced.Distribution);
                TypeOfSoldier.DiffGrowMagicAdvanced.Set(value.TypeOfSoldier.DiffGrowMagicAdvanced.Distribution);
                TypeOfSoldier.DiffGrowMdefAdvanced.Set(value.TypeOfSoldier.DiffGrowMdefAdvanced.Distribution);
                TypeOfSoldier.DiffGrowPhysAdvanced.Set(value.TypeOfSoldier.DiffGrowPhysAdvanced.Distribution);
                TypeOfSoldier.DiffGrowSightAdvanced.Set(value.TypeOfSoldier.DiffGrowSightAdvanced.Distribution);
                TypeOfSoldier.DiffGrowMoveAdvanced.Set(value.TypeOfSoldier.DiffGrowMoveAdvanced.Distribution);
                TypeOfSoldier.DiffGrowTotalBase.Set(value.TypeOfSoldier.DiffGrowTotalBase.Distribution);
                TypeOfSoldier.DiffGrowTotalAdvanced.Set(value.TypeOfSoldier.DiffGrowTotalAdvanced.Distribution);
                TypeOfSoldier.checkBox17.Checked = value.TypeOfSoldier.LearningSkill.Enabled;
                TypeOfSoldier.LearningSkill.Set(value.TypeOfSoldier.LearningSkill.Distribution);
                TypeOfSoldier.checkBox16.Checked = value.TypeOfSoldier.LunaticSkill.Enabled;
                TypeOfSoldier.LunaticSkill.Set(value.TypeOfSoldier.LunaticSkill.Distribution);
                TypeOfSoldier.checkBox18.Checked = value.TypeOfSoldier.Attrs.Enabled;
                TypeOfSoldier.checkBox21.Checked = value.TypeOfSoldier.Attrs.GetArg<bool>(0);
                TypeOfSoldier.Attrs.Set(value.TypeOfSoldier.Attrs.Distribution);
                TypeOfSoldier.checkBox19.Checked = value.TypeOfSoldier.AttrsCount.Enabled;
                TypeOfSoldier.AttrsCount.Set(value.TypeOfSoldier.AttrsCount.Distribution);

                Individual.checkBox22.Checked = value.Individual.JidAlly.Enabled;
                Individual.JidAlly.Set(value.Individual.JidAlly.Distribution);
                Individual.checkBox23.Checked = value.Individual.JidAlly.GetArg<bool>(0);
                Individual.checkBox24.Checked = value.Individual.JidEnemy.Enabled;
                Individual.JidEnemy.Set(value.Individual.JidEnemy.Distribution);
                Individual.checkBox26.Checked = value.Individual.JidEnemy.GetArg<bool>(0);
                Individual.checkBox20.Checked = value.Individual.Age.Enabled;
                Individual.checkBox30.Checked = value.Individual.ForceUsableWeapon;
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
                Individual.checkBox25.Checked = value.Individual.RandomizeStatLimits;
                Individual.LimitHp.Set(value.Individual.LimitHp.Distribution);
                Individual.LimitStr.Set(value.Individual.LimitStr.Distribution);
                Individual.LimitTech.Set(value.Individual.LimitTech.Distribution);
                Individual.LimitQuick.Set(value.Individual.LimitQuick.Distribution);
                Individual.LimitLuck.Set(value.Individual.LimitLuck.Distribution);
                Individual.LimitDef.Set(value.Individual.LimitDef.Distribution);
                Individual.LimitMagic.Set(value.Individual.LimitMagic.Distribution);
                Individual.LimitMdef.Set(value.Individual.LimitMdef.Distribution);
                Individual.LimitPhys.Set(value.Individual.LimitPhys.Distribution);
                Individual.LimitSight.Set(value.Individual.LimitSight.Distribution);
                Individual.LimitMove.Set(value.Individual.LimitMove.Distribution);
                Individual.checkBox28.Checked = value.Individual.RandomizeAllyStatGrowths;
                Individual.GrowHp.Set(value.Individual.GrowHp.Distribution);
                Individual.GrowStr.Set(value.Individual.GrowStr.Distribution);
                Individual.GrowTech.Set(value.Individual.GrowTech.Distribution);
                Individual.GrowQuick.Set(value.Individual.GrowQuick.Distribution);
                Individual.GrowLuck.Set(value.Individual.GrowLuck.Distribution);
                Individual.GrowDef.Set(value.Individual.GrowDef.Distribution);
                Individual.GrowMagic.Set(value.Individual.GrowMagic.Distribution);
                Individual.GrowMdef.Set(value.Individual.GrowMdef.Distribution);
                Individual.GrowPhys.Set(value.Individual.GrowPhys.Distribution);
                Individual.GrowSight.Set(value.Individual.GrowSight.Distribution);
                Individual.GrowMove.Set(value.Individual.GrowMove.Distribution);
                Individual.checkBox27.Checked = value.Individual.GrowTotal.Enabled;
                Individual.GrowTotal.Set(value.Individual.GrowTotal.Distribution);
                Individual.checkBox29.Checked = value.Individual.RandomizeEnemyStatGrowths;
                Individual.checkBox31.Checked = value.Individual.ItemsWeapons.Enabled;
                Individual.ItemsWeapons.Set(value.Individual.ItemsWeapons.Distribution);
                Individual.checkBox34.Checked = value.Individual.ItemsWeaponCount.Enabled;
                Individual.ItemsWeaponCount.Set(value.Individual.ItemsWeaponCount.Distribution);
                Individual.checkBox33.Checked = value.Individual.ItemsWeapons.GetArg<bool>(0);
                Individual.checkBox44.Checked = value.Individual.ItemsWeapons.GetArg<bool>(1);
                Individual.checkBox32.Checked = value.Individual.ItemsItems.Enabled;
                Individual.ItemsItems.Set(value.Individual.ItemsItems.Distribution);
                Individual.checkBox35.Checked = value.Individual.ItemsItemCount.Enabled;
                Individual.ItemsItemCount.Set(value.Individual.ItemsItemCount.Distribution);
                Individual.checkBox36.Checked = value.Individual.RandomizeEnemyInventories;
                Individual.checkBox38.Checked = value.Individual.AttrsAlly.Enabled;
                Individual.AttrsAlly.Set(value.Individual.AttrsAlly.Distribution);
                Individual.checkBox37.Checked = value.Individual.AttrsAllyCount.Enabled;
                Individual.AttrsAllyCount.Set(value.Individual.AttrsAllyCount.Distribution);
                Individual.checkBox40.Checked = value.Individual.AttrsEnemy.Enabled;
                Individual.AttrsEnemy.Set(value.Individual.AttrsEnemy.Distribution);
                Individual.checkBox39.Checked = value.Individual.AttrsEnemyCount.Enabled;
                Individual.AttrsEnemyCount.Set(value.Individual.AttrsEnemyCount.Distribution);
                Individual.checkBox42.Checked = value.Individual.CommonSids.Enabled;
                Individual.CommonSids.Set(value.Individual.CommonSids.Distribution);
                Individual.checkBox41.Checked = value.Individual.CommonSidsCount.Enabled;
                Individual.CommonSidsCount.Set(value.Individual.CommonSidsCount.Distribution);
                Individual.checkBox43.Checked = value.Individual.CommonSids.GetArg<bool>(0);
                Individual.checkBox45.Checked = value.Individual.CommonSids.GetArg<bool>(1);

                Arrangement.checkBox2.Checked = value.Arrangement.DeploymentSlots.Enabled;
                Arrangement.DeploymentSlots.Set(value.Arrangement.DeploymentSlots.Distribution);
                Arrangement.checkBox1.Checked = value.Arrangement.DeploymentSlots.GetArg<bool>(0);
                Arrangement.checkBox4.Checked = value.Arrangement.EnemyCount.Enabled;
                Arrangement.EnemyCount.Set(value.Arrangement.EnemyCount.Distribution);
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
            try
            {
                RandomizerSettings? rs = XmlParser.ReadRandomizerSettings();
                if (rs != null)
                    RandomizerSettings = rs;
            }
            catch (NullReferenceException)
            { ShowSettingsLoadError(); }
            catch (ArgumentOutOfRangeException)
            { ShowSettingsLoadError(); }
            catch (SerializationException)
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
            RandomizerSettings rs = RandomizerSettings;
            if (rememberSettingsCheckBox.Checked)
                XmlParser.WriteRandomizerSettings(rs);
            else
                FileManager.DeleteRandomizerSettings();
            List<ExportFormat> targets = new();
            if (rs.ExportCobalt) targets.Add(ExportFormat.Cobalt);
            if (rs.ExportLayeredFS) targets.Add(ExportFormat.LayeredFS);
            if (!targets.Any())
            {
                NoExportTargetsMessage();
                return;
            }
            GlobalData.R.Randomize(rs);
            switch (GlobalData.Export(targets))
            {
                case ExportResult.Success:
                    ExportSuccessMessage();
                    Close();
                    break;
                case ExportResult.NoChanges:
                    NoChangesMessage();
                    break;
                case ExportResult.NoExportTargets:
                    // This is handled before randomization, so this can't happen.
                    throw new NotImplementedException();
            }
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

        private void Button6_Click(object sender, EventArgs e)
        {
            TypeOfSoldier.Show();
            TypeOfSoldier.Activate();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Arrangement.Show();
            Arrangement.Activate();
        }
    }
}