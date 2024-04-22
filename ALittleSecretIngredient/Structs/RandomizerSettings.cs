using System.Reflection;
using System.Runtime.Serialization;
using static ALittleSecretIngredient.Probability;

namespace ALittleSecretIngredient.Structs
{
    public class RandomizerSettings
    {
        public bool Remember { get; set; } = new();
        public bool SaveChangelog { get; set; } = true;
        public bool ExportCobalt { get; set; } = true;
        public bool ExportLayeredFS { get; set; } = true;

        public AssetTableSettings AssetTable { get; set; } = new();
        public GodGeneralSettings GodGeneral { get; set; } = new();
        public GrowthTableSettings GrowthTable { get; set; } = new();
        public BondLevelSettings BondLevel { get; set; } = new();
        public TypeOfSoldierSettings TypeOfSoldier { get; set; } = new();
        public IndividualSettings Individual { get; set; } = new();
        public ArrangementSettings Arrangement { get; set; } = new();

        public abstract class RandomizerTableSettings
        {
            internal bool Any()
            {
                foreach (FieldInfo fi in GetType().GetRuntimeFields())
                    switch (fi.FieldType.Name)
                    {
                        case "RandomizerFieldSettings":
                            RandomizerFieldSettings rfs = (RandomizerFieldSettings)fi.GetValue(this)!;
                            if (rfs.Enabled || rfs.Args.Any(o => o.GetType().Name == "Boolean" && (bool)o))
                                return true;
                            break;
                        case "Boolean":
                            if ((bool)fi.GetValue(this)!)
                                return true;
                            break;
                    }
                return false;
            } 
        }

        public class AssetTableSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings ModelSwap { get; set; } = new();
            public RandomizerFieldSettings OutfitSwap { get; set; } = new();
            public RandomizerFieldSettings ColorPalette { get; set; } = new();
            public bool ShuffleRideDressModel { get; set; }
            public RandomizerFieldSettings InfoAnim { get; set; } = new();
            public bool ShuffleTalkAnims { get; set; }
            public RandomizerFieldSettings DemoAnim { get; set; } = new();
            public bool ShuffleHubAnims { get; set; }
            public bool RandomizeModelParameters { get; set; } = new();
            public RandomizerFieldSettings ScaleAll { get; set; } = new();
            public RandomizerFieldSettings ScaleHead { get; set; } = new();
            public RandomizerFieldSettings ScaleNeck { get; set; } = new();
            public RandomizerFieldSettings ScaleTorso { get; set; } = new();
            public RandomizerFieldSettings ScaleShoulders { get; set; } = new();
            public RandomizerFieldSettings ScaleArms { get; set; } = new();
            public RandomizerFieldSettings ScaleHands { get; set; } = new();
            public RandomizerFieldSettings ScaleLegs { get; set; } = new();
            public RandomizerFieldSettings ScaleFeet { get; set; } = new();
            public RandomizerFieldSettings VolumeArms { get; set; } = new();
            public RandomizerFieldSettings VolumeLegs { get; set; } = new();
            public RandomizerFieldSettings VolumeBust { get; set; } = new();
            public RandomizerFieldSettings VolumeAbdomen { get; set; } = new();
            public RandomizerFieldSettings VolumeTorso { get; set; } = new();
            public RandomizerFieldSettings VolumeScaleArms { get; set; } = new();
            public RandomizerFieldSettings VolumeScaleLegs { get; set; } = new();
            public RandomizerFieldSettings MapScaleAll { get; set; } = new();
            public RandomizerFieldSettings MapScaleHead { get; set; } = new();
            public RandomizerFieldSettings MapScaleWing { get; set; } = new();
        }

        public class GodGeneralSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings Link { get; set; } = new();
            public RandomizerFieldSettings EngageCount { get; set; } = new();
            public RandomizerFieldSettings EngageAttackAlly { get; set; } = new();
            public RandomizerFieldSettings EngageAttackEnemy { get; set; } = new();
            public RandomizerFieldSettings EngageAttackLink { get; set; } = new();
            public RandomizerFieldSettings LinkGid { get; set; } = new();
            public bool ShuffleGrowTableAlly { get; set; }
            public bool ShuffleGrowTableEnemy { get; set; }
            public bool RandomizeEngravingStats { get; set; }
            public RandomizerFieldSettings EngravePower { get; set; } = new();
            public RandomizerFieldSettings EngraveWeight { get; set; } = new();
            public RandomizerFieldSettings EngraveHit { get; set; } = new();
            public RandomizerFieldSettings EngraveCritical { get; set; } = new();
            public RandomizerFieldSettings EngraveAvoid { get; set; } = new();
            public RandomizerFieldSettings EngraveSecure { get; set; } = new();
            public bool RandomizeAllyStaticSyncStats { get; set; }
            public RandomizerFieldSettings SynchroEnhanceHpAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceStrAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceTechAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceQuickAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceLuckAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceDefAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceMagicAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceMdefAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhancePhysAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceMoveAlly { get; set; } = new();
            public bool RandomizeEnemyStaticSyncStats { get; set; }
            public RandomizerFieldSettings SynchroEnhanceHpEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceStrEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceTechEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceQuickEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceLuckEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceDefEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceMagicEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceMdefEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhancePhysEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroEnhanceMoveEnemy { get; set; } = new();
            public RandomizerFieldSettings WeaponRestriction { get; set; } = new();
        }
        public class GrowthTableSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings InheritanceSkills { get; set; } = new();
            public RandomizerFieldSettings InheritanceSkillsCount { get; set; } = new();
            public RandomizerFieldSettings SynchroStatSkillsAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroStatSkillsAllyCount { get; set; } = new();
            public RandomizerFieldSettings SynchroStatSkillsEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroStatSkillsEnemyCount { get; set; } = new();
            public RandomizerFieldSettings SynchroGeneralSkillsAlly { get; set; } = new();
            public RandomizerFieldSettings SynchroGeneralSkillsAllyCount { get; set; } = new();
            public RandomizerFieldSettings SynchroGeneralSkillsEnemy { get; set; } = new();
            public RandomizerFieldSettings SynchroGeneralSkillsEnemyCount { get; set; } = new();
            public RandomizerFieldSettings EngageSkills { get; set; } = new();
            public RandomizerFieldSettings EngageSkillsCount { get; set; } = new();
            public RandomizerFieldSettings EngageItems { get; set; } = new();
            public RandomizerFieldSettings EngageItemsCount { get; set; } = new();
            public RandomizerFieldSettings Aptitude { get; set; } = new();
            public RandomizerFieldSettings AptitudeCount { get; set; } = new();
            public RandomizerFieldSettings SkillInheritanceLevel { get; set; } = new();
            public RandomizerFieldSettings StrongBondLevel { get; set; } = new();
            public RandomizerFieldSettings DeepSynergyLevel { get; set; } = new();
        }
        public class BondLevelSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings Exp { get; set; } = new();
            public RandomizerFieldSettings Cost { get; set; } = new();
        }

        public class TypeOfSoldierSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings StyleName { get; set; } = new();
            public RandomizerFieldSettings MoveType { get; set; } = new();
            public RandomizerFieldSettings Weapon { get; set; } = new();
            public bool RandomizeWeaponTypeCount { get; set; } = new();
            public RandomizerFieldSettings WeaponBaseCount { get; set; } = new();
            public RandomizerFieldSettings WeaponAdvancedCount { get; set; } = new();
            public RandomizerFieldSettings RandomizeWeaponRank { get; set; } = new();
            public RandomizerFieldSettings MaxWeaponLevelBase { get; set; } = new();
            public RandomizerFieldSettings MaxWeaponLevelAdvanced { get; set; } = new();
            public RandomizerFieldSettings RandomizeBaseStats { get; set; } = new();
            public RandomizerFieldSettings BaseHpBase { get; set; } = new();
            public RandomizerFieldSettings BaseStrBase { get; set; } = new();
            public RandomizerFieldSettings BaseTechBase { get; set; } = new();
            public RandomizerFieldSettings BaseQuickBase { get; set; } = new();
            public RandomizerFieldSettings BaseLuckBase { get; set; } = new();
            public RandomizerFieldSettings BaseDefBase { get; set; } = new();
            public RandomizerFieldSettings BaseMagicBase { get; set; } = new();
            public RandomizerFieldSettings BaseMdefBase { get; set; } = new();
            public RandomizerFieldSettings BasePhysBase { get; set; } = new();
            public RandomizerFieldSettings BaseSightBase { get; set; } = new();
            public RandomizerFieldSettings BaseMoveBase { get; set; } = new();
            public RandomizerFieldSettings BaseHpAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseStrAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseTechAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseQuickAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseLuckAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseDefAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseMagicAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseMdefAdvanced { get; set; } = new();
            public RandomizerFieldSettings BasePhysAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseSightAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseMoveAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseTotalBase { get; set; } = new();
            public RandomizerFieldSettings BaseTotalAdvanced { get; set; } = new();
            public RandomizerFieldSettings RandomizeStatLimits { get; set; } = new();
            public RandomizerFieldSettings LimitHpBase { get; set; } = new();
            public RandomizerFieldSettings LimitStrBase { get; set; } = new();
            public RandomizerFieldSettings LimitTechBase { get; set; } = new();
            public RandomizerFieldSettings LimitQuickBase { get; set; } = new();
            public RandomizerFieldSettings LimitLuckBase { get; set; } = new();
            public RandomizerFieldSettings LimitDefBase { get; set; } = new();
            public RandomizerFieldSettings LimitMagicBase { get; set; } = new();
            public RandomizerFieldSettings LimitMdefBase { get; set; } = new();
            public RandomizerFieldSettings LimitPhysBase { get; set; } = new();
            public RandomizerFieldSettings LimitSightBase { get; set; } = new();
            public RandomizerFieldSettings LimitMoveBase { get; set; } = new();
            public RandomizerFieldSettings LimitHpAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitStrAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitTechAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitQuickAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitLuckAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitDefAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitMagicAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitMdefAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitPhysAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitSightAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitMoveAdvanced { get; set; } = new();
            public RandomizerFieldSettings LimitTotalBase { get; set; } = new();
            public RandomizerFieldSettings LimitTotalAdvanced { get; set; } = new();
            public RandomizerFieldSettings RandomizeEnemyStatGrows { get; set; } = new();
            public RandomizerFieldSettings BaseGrowHpBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowStrBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowTechBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowQuickBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowLuckBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowDefBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowMagicBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowMdefBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowPhysBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowSightBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowMoveBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowHpAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowStrAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowTechAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowQuickAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowLuckAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowDefAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowMagicAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowMdefAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowPhysAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowSightAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowMoveAdvanced { get; set; } = new();
            public RandomizerFieldSettings BaseGrowTotalBase { get; set; } = new();
            public RandomizerFieldSettings BaseGrowTotalAdvanced { get; set; } = new();
            public RandomizerFieldSettings RandomizeStatGrowthModifiers { get; set; } = new();
            public RandomizerFieldSettings DiffGrowHpBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowStrBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowTechBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowQuickBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowLuckBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowDefBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowMagicBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowMdefBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowPhysBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowSightBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowMoveBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowHpAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowStrAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowTechAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowQuickAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowLuckAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowDefAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowMagicAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowMdefAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowPhysAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowSightAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowMoveAdvanced { get; set; } = new();
            public RandomizerFieldSettings DiffGrowTotalBase { get; set; } = new();
            public RandomizerFieldSettings DiffGrowTotalAdvanced { get; set; } = new();
            public RandomizerFieldSettings LearningSkill { get; set; } = new();
            public RandomizerFieldSettings LunaticSkill { get; set; } = new();
            public RandomizerFieldSettings Attrs { get; set; } = new();
            public RandomizerFieldSettings AttrsCount { get; set; } = new();

            internal RandomizerFieldSettings[] GetBaseBaseStatSettings() => new RandomizerFieldSettings[]
            {
                BaseHpBase, BaseStrBase, BaseTechBase, BaseQuickBase,
                BaseLuckBase, BaseDefBase, BaseMagicBase, BaseMdefBase,
                BasePhysBase, BaseSightBase, BaseMoveBase, BaseTotalBase,
            };

            internal RandomizerFieldSettings[] GetAdvancedBaseStatSettings() => new RandomizerFieldSettings[]
            {
                BaseHpAdvanced, BaseStrAdvanced, BaseTechAdvanced, BaseQuickAdvanced,
                BaseLuckAdvanced, BaseDefAdvanced, BaseMagicAdvanced, BaseMdefAdvanced,
                BasePhysAdvanced, BaseSightAdvanced, BaseMoveAdvanced, BaseTotalAdvanced,
            };

            internal RandomizerFieldSettings[] GetBaseStatLimitSettings() => new RandomizerFieldSettings[]
            {
                LimitHpBase, LimitStrBase, LimitTechBase, LimitQuickBase,
                LimitLuckBase, LimitDefBase, LimitMagicBase, LimitMdefBase,
                LimitPhysBase, LimitSightBase, LimitMoveBase, LimitTotalBase,
            };

            internal RandomizerFieldSettings[] GetAdvancedStatLimitSettings() => new RandomizerFieldSettings[]
            {
                LimitHpAdvanced, LimitStrAdvanced, LimitTechAdvanced, LimitQuickAdvanced,
                LimitLuckAdvanced, LimitDefAdvanced, LimitMagicAdvanced, LimitMdefAdvanced,
                LimitPhysAdvanced, LimitSightAdvanced, LimitMoveAdvanced, LimitTotalAdvanced,
            };

            internal RandomizerFieldSettings[] GetBaseEnemyGrowthSettings() => new RandomizerFieldSettings[]
            {
                BaseGrowHpBase, BaseGrowStrBase, BaseGrowTechBase, BaseGrowQuickBase,
                BaseGrowLuckBase, BaseGrowDefBase, BaseGrowMagicBase, BaseGrowMdefBase,
                BaseGrowPhysBase, BaseGrowSightBase, BaseGrowMoveBase, BaseGrowTotalBase,
            };

            internal RandomizerFieldSettings[] GetAdvancedEnemyGrowthSettings() => new RandomizerFieldSettings[]
            {
                BaseGrowHpAdvanced, BaseGrowStrAdvanced, BaseGrowTechAdvanced, BaseGrowQuickAdvanced,
                BaseGrowLuckAdvanced, BaseGrowDefAdvanced, BaseGrowMagicAdvanced, BaseGrowMdefAdvanced,
                BaseGrowPhysAdvanced, BaseGrowSightAdvanced, BaseGrowMoveAdvanced, BaseGrowTotalAdvanced,
            };

            internal RandomizerFieldSettings[] GetBaseGrowthModifierSettings() => new RandomizerFieldSettings[]
            {
                DiffGrowHpBase, DiffGrowStrBase, DiffGrowTechBase, DiffGrowQuickBase,
                DiffGrowLuckBase, DiffGrowDefBase, DiffGrowMagicBase, DiffGrowMdefBase,
                DiffGrowPhysBase, DiffGrowSightBase, DiffGrowMoveBase, DiffGrowTotalBase,
            };

            internal RandomizerFieldSettings[] GetAdvancedGrowthModifierSettings() => new RandomizerFieldSettings[]
            {
                DiffGrowHpAdvanced, DiffGrowStrAdvanced, DiffGrowTechAdvanced, DiffGrowQuickAdvanced,
                DiffGrowLuckAdvanced, DiffGrowDefAdvanced, DiffGrowMagicAdvanced, DiffGrowMdefAdvanced,
                DiffGrowPhysAdvanced, DiffGrowSightAdvanced, DiffGrowMoveAdvanced, DiffGrowTotalAdvanced,
            };
        }

        public class IndividualSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings JidAlly { get; set; } = new();
            public RandomizerFieldSettings JidEnemy { get; set; } = new();
            public bool ForceUsableWeapon { get; set; } = new();
            public RandomizerFieldSettings Age { get; set; } = new();
            public bool RandomizeBirthday { get; set; } = new();
            public RandomizerFieldSettings LevelAlly { get; set; } = new();
            public RandomizerFieldSettings LevelEnemy { get; set; } = new();
            public RandomizerFieldSettings InternalLevel { get; set; } = new();
            public RandomizerFieldSettings SupportCategory { get; set; } = new();
            public RandomizerFieldSettings SkillPoint { get; set; } = new();
            public RandomizerFieldSettings Aptitude { get; set; } = new();
            public RandomizerFieldSettings AptitudeCount { get; set; } = new();
            public RandomizerFieldSettings SubAptitude { get; set; } = new();
            public RandomizerFieldSettings SubAptitudeCount { get; set; } = new();
            public bool RandomizeAllyBases { get; set; } = new();
            public RandomizerFieldSettings OffsetNHpAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNStrAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNTechAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNQuickAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNLuckAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNDefAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNMagicAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNMdefAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNPhysAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNSightAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNMoveAlly { get; set; } = new();
            public RandomizerFieldSettings OffsetNTotalAlly { get; set; } = new();
            public bool StrongerProtagonist { get; set; } = new();
            public bool StrongerAllyNPCs{ get; set; } = new();
            public bool RandomizeEnemyBasesNormal { get; set; } = new();
            public RandomizerFieldSettings OffsetNHpEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNStrEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNTechEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNQuickEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNLuckEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNDefEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNMagicEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNMdefEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNPhysEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNSightEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNMoveEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetNTotalEnemy { get; set; } = new();
            public bool RandomizeEnemyBasesHard { get; set; } = new();
            public RandomizerFieldSettings OffsetHHpEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHStrEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHTechEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHQuickEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHLuckEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHDefEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHMagicEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHMdefEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHPhysEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHSightEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHMoveEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetHTotalEnemy { get; set; } = new();
            public bool RandomizeEnemyBasesMaddening { get; set; } = new();
            public RandomizerFieldSettings OffsetLHpEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLStrEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLTechEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLQuickEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLLuckEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLDefEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLMagicEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLMdefEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLPhysEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLSightEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLMoveEnemy { get; set; } = new();
            public RandomizerFieldSettings OffsetLTotalEnemy { get; set; } = new();
            public bool RandomizeStatLimits { get; set; } = new();
            public RandomizerFieldSettings LimitHp { get; set; } = new();
            public RandomizerFieldSettings LimitStr { get; set; } = new();
            public RandomizerFieldSettings LimitTech { get; set; } = new();
            public RandomizerFieldSettings LimitQuick { get; set; } = new();
            public RandomizerFieldSettings LimitLuck { get; set; } = new();
            public RandomizerFieldSettings LimitDef { get; set; } = new();
            public RandomizerFieldSettings LimitMagic { get; set; } = new();
            public RandomizerFieldSettings LimitMdef { get; set; } = new();
            public RandomizerFieldSettings LimitPhys { get; set; } = new();
            public RandomizerFieldSettings LimitSight { get; set; } = new();
            public RandomizerFieldSettings LimitMove { get; set; } = new();
            public bool RandomizeAllyStatGrowths { get; set; } = new();
            public RandomizerFieldSettings GrowHp { get; set; } = new();
            public RandomizerFieldSettings GrowStr { get; set; } = new();
            public RandomizerFieldSettings GrowTech { get; set; } = new();
            public RandomizerFieldSettings GrowQuick { get; set; } = new();
            public RandomizerFieldSettings GrowLuck { get; set; } = new();
            public RandomizerFieldSettings GrowDef { get; set; } = new();
            public RandomizerFieldSettings GrowMagic { get; set; } = new();
            public RandomizerFieldSettings GrowMdef { get; set; } = new();
            public RandomizerFieldSettings GrowPhys { get; set; } = new();
            public RandomizerFieldSettings GrowSight { get; set; } = new();
            public RandomizerFieldSettings GrowMove { get; set; } = new();
            public RandomizerFieldSettings GrowTotal { get; set; } = new();
            public bool RandomizeEnemyStatGrowths { get; set; } = new();
            public RandomizerFieldSettings ItemsWeapons { get; set; } = new();
            public RandomizerFieldSettings ItemsWeaponCount { get; set; } = new();
            public RandomizerFieldSettings ItemsItems { get; set; } = new();
            public RandomizerFieldSettings ItemsItemCount { get; set; } = new();
            public bool RandomizeEnemyInventories { get; set; } = new();
            public RandomizerFieldSettings AttrsAlly { get; set; } = new();
            public RandomizerFieldSettings AttrsAllyCount { get; set; } = new();
            public RandomizerFieldSettings AttrsEnemy { get; set; } = new();
            public RandomizerFieldSettings AttrsEnemyCount { get; set; } = new();
            public RandomizerFieldSettings CommonSids { get; set; } = new();
            public RandomizerFieldSettings CommonSidsCount { get; set; } = new();

            internal RandomizerFieldSettings[] GetOffsetNAllySettings() => new RandomizerFieldSettings[]
            {
                OffsetNHpAlly, OffsetNStrAlly, OffsetNTechAlly, OffsetNQuickAlly,
                OffsetNLuckAlly, OffsetNDefAlly, OffsetNMagicAlly, OffsetNMdefAlly,
                OffsetNPhysAlly, OffsetNSightAlly, OffsetNMoveAlly, OffsetNTotalAlly,
            };

            internal RandomizerFieldSettings[] GetOffsetNEnemySettings() => new RandomizerFieldSettings[]
            {
                OffsetNHpEnemy, OffsetNStrEnemy, OffsetNTechEnemy, OffsetNQuickEnemy,
                OffsetNLuckEnemy, OffsetNDefEnemy, OffsetNMagicEnemy, OffsetNMdefEnemy,
                OffsetNPhysEnemy, OffsetNSightEnemy, OffsetNMoveEnemy, OffsetNTotalEnemy,
            };

            internal RandomizerFieldSettings[] GetOffsetHEnemySettings() => new RandomizerFieldSettings[]
            {
                OffsetHHpEnemy, OffsetHStrEnemy, OffsetHTechEnemy, OffsetHQuickEnemy,
                OffsetHLuckEnemy, OffsetHDefEnemy, OffsetHMagicEnemy, OffsetHMdefEnemy,
                OffsetHPhysEnemy, OffsetHSightEnemy, OffsetHMoveEnemy, OffsetHTotalEnemy,
            };

            internal RandomizerFieldSettings[] GetOffsetLEnemySettings() => new RandomizerFieldSettings[]
            {
                OffsetLHpEnemy, OffsetLStrEnemy, OffsetLTechEnemy, OffsetLQuickEnemy,
                OffsetLLuckEnemy, OffsetLDefEnemy, OffsetLMagicEnemy, OffsetLMdefEnemy,
                OffsetLPhysEnemy, OffsetLSightEnemy, OffsetLMoveEnemy, OffsetLTotalEnemy,
            };

            internal RandomizerFieldSettings[] GetGrowthSettings() => new RandomizerFieldSettings[]
            {
                GrowHp, GrowStr, GrowTech, GrowQuick,
                GrowLuck, GrowDef, GrowMagic, GrowMdef,
                GrowPhys, GrowSight, GrowMove, GrowTotal,
            };
        }

        public class ArrangementSettings : RandomizerTableSettings
        {
            public RandomizerFieldSettings DeploymentSlots { get; set; } = new();
            public RandomizerFieldSettings EnemyCount { get; set; } = new();
        }
    }

    [KnownType(typeof(UniformConstant)), KnownType(typeof(UniformRelative)), KnownType(typeof(UniformProportional)),
        KnownType(typeof(NormalConstant)), KnownType(typeof(NormalRelative)), KnownType(typeof(NormalProportional)),
        KnownType(typeof(Empirical)), KnownType(typeof(UniformSelection)), KnownType(typeof(Redistribution))]
    public class RandomizerFieldSettings
    {
        
        public bool Enabled { get; set; }

        public IDistribution Distribution { get; set; }
        
        public List<object> Args { get; set; }

        public RandomizerFieldSettings()
        {
            Enabled = false;
            Distribution = null!;
            Args = new();
        }

        public RandomizerFieldSettings(bool enabled, IDistribution distribution, object[] args)
        {
            Enabled = enabled;
            Distribution = distribution;
            Args = args.ToList();
        }

        internal T GetArg<T>(int index) => (T)Args[index];
    }
}
