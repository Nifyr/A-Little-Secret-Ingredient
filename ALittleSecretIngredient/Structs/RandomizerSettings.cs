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
