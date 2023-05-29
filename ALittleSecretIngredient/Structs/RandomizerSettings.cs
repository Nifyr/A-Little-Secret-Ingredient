using System.Runtime.Serialization;
using static ALittleSecretIngredient.Probability;

namespace ALittleSecretIngredient.Structs
{
    public class RandomizerSettings
    {
        public bool Remember { get; set; } = new();
        public bool SaveChangelog { get; set; } = new();

        public AssetTableSettings AssetTable { get; set; } = new();
        public GodGeneralSettings GodGeneral { get; set; } = new();
        public GrowthTableSettings GrowthTable { get; set; } = new();
        public BondLevelSettings BondLevel { get; set; } = new();
        public IndividualSettings Individual { get; set; } = new();

        public class AssetTableSettings
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

        public class GodGeneralSettings
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
        public class GrowthTableSettings
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
        public class BondLevelSettings
        {
            public RandomizerFieldSettings Exp { get; set; } = new();
            public RandomizerFieldSettings Cost { get; set; } = new();
        }

        public class IndividualSettings
        {
            public RandomizerFieldSettings Age { get; set; } = new();
            public bool RandomizeBirthday { get; set; }
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

            internal RandomizerFieldSettings[] GetOffsetNAllySettings()
            {
                return new RandomizerFieldSettings[]
                {
                    OffsetNHpAlly, OffsetNStrAlly, OffsetNTechAlly, OffsetNQuickAlly,
                    OffsetNLuckAlly, OffsetNDefAlly, OffsetNMagicAlly, OffsetNMdefAlly,
                    OffsetNPhysAlly, OffsetNSightAlly, OffsetNMoveAlly, OffsetNTotalAlly,
                };
            }

            internal RandomizerFieldSettings[] GetOffsetNEnemySettings()
            {
                return new RandomizerFieldSettings[]
                {
                    OffsetNHpEnemy, OffsetNStrEnemy, OffsetNTechEnemy, OffsetNQuickEnemy,
                    OffsetNLuckEnemy, OffsetNDefEnemy, OffsetNMagicEnemy, OffsetNMdefEnemy,
                    OffsetNPhysEnemy, OffsetNSightEnemy, OffsetNMoveEnemy, OffsetNTotalEnemy,
                };
            }

            internal RandomizerFieldSettings[] GetOffsetHEnemySettings()
            {
                return new RandomizerFieldSettings[]
                {
                    OffsetHHpEnemy, OffsetHStrEnemy, OffsetHTechEnemy, OffsetHQuickEnemy,
                    OffsetHLuckEnemy, OffsetHDefEnemy, OffsetHMagicEnemy, OffsetHMdefEnemy,
                    OffsetHPhysEnemy, OffsetHSightEnemy, OffsetHMoveEnemy, OffsetHTotalEnemy,
                };
            }

            internal RandomizerFieldSettings[] GetOffsetLEnemySettings()
            {
                return new RandomizerFieldSettings[]
                {
                    OffsetLHpEnemy, OffsetLStrEnemy, OffsetLTechEnemy, OffsetLQuickEnemy,
                    OffsetLLuckEnemy, OffsetLDefEnemy, OffsetLMagicEnemy, OffsetLMdefEnemy,
                    OffsetLPhysEnemy, OffsetLSightEnemy, OffsetLMoveEnemy, OffsetLTotalEnemy,
                };
            }
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
