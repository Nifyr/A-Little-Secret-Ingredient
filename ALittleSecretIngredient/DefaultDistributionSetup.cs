using ALittleSecretIngredient.Structs;
using static ALittleSecretIngredient.Probability;
using static ALittleSecretIngredient.GameDataLookup;
using System.Linq;

namespace ALittleSecretIngredient
{
    internal class DefaultDistributionSetup
    {
        private GameData GD { get; }
        internal DefaultDistributionSetup(GameData gd)
        {
            GD = gd;
        }

        internal NumericDistributionSetup GetNumericDistributionSetup(RandomizerDistribution dfe)
        {
            DataSetEnum dse = DistributionToDataSet[dfe];
            DataSet dataSet = GD.Get(dse);
            switch (dse)
            {
                case DataSetEnum.GodGeneral:
                    List<GodGeneral> ggs = dataSet.Params.Cast<GodGeneral>().ToList();
                    switch (dfe)
                    {
                        case RandomizerDistribution.EngageCount:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllySyncableEmblems),
                                gg => gg.EngageCount);
                            nds0.distributions[3] = new NormalConstant(100, 7, 2);
                            nds0.idx = 3;
                            return nds0;
                        case RandomizerDistribution.EngravePower:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems), gg =>
                                gg.EngravePower);
                        case RandomizerDistribution.EngraveWeight:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems), gg =>
                                gg.EngraveWeight);
                        case RandomizerDistribution.EngraveHit:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems), gg =>
                                gg.EngraveHit);
                        case RandomizerDistribution.EngraveCritical:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems), gg =>
                                gg.EngraveCritical);
                        case RandomizerDistribution.EngraveAvoid:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems), gg =>
                                gg.EngraveAvoid);
                        case RandomizerDistribution.EngraveSecure:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems), gg =>
                                gg.EngraveSecure);
                        case RandomizerDistribution.SynchroEnhanceHpAlly:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceHp);
                            nds1.distributions[3] = new NormalConstant(10, 0, 8);
                            nds1.idx = 3;
                            return nds1;
                        case RandomizerDistribution.SynchroEnhanceStrAlly:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceStr);
                            nds2.distributions[3] = new NormalConstant(10, 0, 5);
                            nds2.idx = 3;
                            return nds2;
                        case RandomizerDistribution.SynchroEnhanceTechAlly:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceTech);
                            nds3.distributions[3] = new NormalConstant(10, 0, 5);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.SynchroEnhanceQuickAlly:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceQuick);
                            nds4.distributions[3] = new NormalConstant(10, 0, 5);
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.SynchroEnhanceLuckAlly:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceLuck);
                            nds5.distributions[3] = new NormalConstant(10, 0, 5);
                            nds5.idx = 3;
                            return nds5;
                        case RandomizerDistribution.SynchroEnhanceDefAlly:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceDef);
                            nds6.distributions[3] = new NormalConstant(10, 0, 5);
                            nds6.idx = 3;
                            return nds6;
                        case RandomizerDistribution.SynchroEnhanceMagicAlly:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceMagic);
                            nds7.distributions[3] = new NormalConstant(10, 0, 5);
                            nds7.idx = 3;
                            return nds7;
                        case RandomizerDistribution.SynchroEnhanceMdefAlly:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceMdef);
                            nds8.distributions[3] = new NormalConstant(10, 0, 5);
                            nds8.idx = 3;
                            return nds8;
                        case RandomizerDistribution.SynchroEnhancePhysAlly:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhancePhys);
                            nds9.distributions[3] = new NormalConstant(10, 0, 5);
                            nds9.idx = 3;
                            return nds9;
                        case RandomizerDistribution.SynchroEnhanceMoveAlly:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems),
                                gg => gg.SynchroEnhanceMove);
                            nds10.distributions[3] = new NormalConstant(10, 0, 0.5);
                            nds10.idx = 3;
                            return nds10;
                        case RandomizerDistribution.SynchroEnhanceHpEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceHp);
                        case RandomizerDistribution.SynchroEnhanceStrEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceStr);
                        case RandomizerDistribution.SynchroEnhanceTechEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceTech);
                        case RandomizerDistribution.SynchroEnhanceQuickEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceQuick);
                        case RandomizerDistribution.SynchroEnhanceLuckEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceLuck);
                        case RandomizerDistribution.SynchroEnhanceDefEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceDef);
                        case RandomizerDistribution.SynchroEnhanceMagicEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceMagic);
                        case RandomizerDistribution.SynchroEnhanceMdefEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceMdef);
                        case RandomizerDistribution.SynchroEnhancePhysEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhancePhys);
                        case RandomizerDistribution.SynchroEnhanceMoveEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems), gg =>
                                gg.SynchroEnhanceMove);
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    };
                case DataSetEnum.GrowthTable:
                    List<ParamGroup> pgs = dataSet.Params.Cast<ParamGroup>().ToList();
                    List<string> syncStatSkillIDs = GD.SyncStatSkills.GetIDs();
                    List<string> generalSkillIDs0 = GD.GeneralSkills.GetIDs();
                    switch (dfe)
                    {
                        case RandomizerDistribution.InheritanceSkills:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables),
                                pg => pg.Group.Cast<GrowthTable>().Select(gt => gt.InheritanceSkills.Length).Sum());
                            nds0.idx = 3;
                            return nds0;
                        case RandomizerDistribution.SynchroStatSkillsAlly:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(syncStatSkillIDs.Contains)).Sum());
                            nds1.idx = 3;
                            return nds1;
                        case RandomizerDistribution.SynchroStatSkillsEnemy:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(syncStatSkillIDs.Contains)).Sum());
                            nds2.distributions[3] = new NormalConstant(10, 10, 1);
                            nds2.idx = 3;
                            return nds2;
                        case RandomizerDistribution.SynchroGeneralSkillsAlly:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(generalSkillIDs0.Contains)).Sum());
                            nds3.distributions[3] = new NormalConstant(100, 3, 1);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.SynchroGeneralSkillsEnemy:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(generalSkillIDs0.Contains)).Sum());
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.EngageSkills:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.BondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.EngageSkills.Count(generalSkillIDs0.Contains)).Sum());
                            nds5.distributions[0] = new UniformConstant(10, 0, 2);
                            nds5.idx = 0;
                            return nds5;
                        case RandomizerDistribution.EngageItems:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.BondLevelTables), pg =>
                            pg.Group.Cast<GrowthTable>().Select(gt => gt.EngageSkills.Length + gt.EngageDragons.Length).Sum());
                            nds6.distributions[0] = new UniformConstant(10, 2, 4);
                            nds6.idx = 0;
                            return nds6;
                        case RandomizerDistribution.GrowthTableAptitude:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.GetAptitudes().Count).Sum());
                            nds7.idx = 3;
                            return nds7;
                        case RandomizerDistribution.SkillInheritanceLevel:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().First(gt => gt.GetFlag(0)).Level);
                            nds8.distributions[3] = new NormalConstant(10, 5, 1);
                            nds8.idx = 3;
                            return nds8;
                        case RandomizerDistribution.StrongBondLevel:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().First(gt => gt.GetFlag(1)).Level);
                            nds9.distributions[3] = new NormalConstant(10, 11, 3);
                            nds9.idx = 3;
                            return nds9;
                        case RandomizerDistribution.DeepSynergyLevel:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().First(gt => gt.GetFlag(2)).Level);
                            nds10.distributions[3] = new NormalConstant(10, 20, 5);
                            nds10.idx = 3;
                            return nds10;
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                case DataSetEnum.BondLevel:
                    List<BondLevel> bls = dataSet.Params.Cast<BondLevel>().ToList();
                    switch (dfe)
                    {
                        case RandomizerDistribution.Exp:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(bls.FilterData(bl => bl.Level, GD.BondLevelsFromExp), bl => bl.Exp);
                            nds0.idx = 5;
                            return nds0;
                        case RandomizerDistribution.Cost:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(bls.FilterData(bl => bl.Level, GD.BondLevelsFromExp), bl => bl.Cost);
                            nds1.idx = 5;
                            return nds1;
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                case DataSetEnum.Asset:
                    List<Asset> assets = dataSet.Params.Cast<Asset>().ToList();
                    switch (dfe)
                    {
                        case RandomizerDistribution.ScaleAll:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(assets.Where(a => a.ScaleAll != 0), a => a.ScaleAll);
                            nds0.idx = 3;
                            return nds0;
                        case RandomizerDistribution.ScaleHead:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(assets.Where(a => a.ScaleHead != 0), a => a.ScaleHead);
                            nds1.idx = 4;
                            return nds1;
                        case RandomizerDistribution.ScaleNeck:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(assets.Where(a => a.ScaleNeck != 0), a => a.ScaleNeck);
                            nds2.idx = 3;
                            return nds2;
                        case RandomizerDistribution.ScaleTorso:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(assets.Where(a => a.ScaleTorso != 0), a => a.ScaleTorso);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.ScaleShoulders:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(assets.Where(a => a.ScaleShoulders != 0), a => a.ScaleShoulders);
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.ScaleArms:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(assets.Where(a => a.ScaleArms != 0), a => a.ScaleArms);
                            nds5.idx = 3;
                            return nds5;
                        case RandomizerDistribution.ScaleHands:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(assets.Where(a => a.ScaleHands != 0), a => a.ScaleHands);
                            nds6.idx = 3;
                            return nds6;
                        case RandomizerDistribution.ScaleLegs:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(assets.Where(a => a.ScaleLegs != 0), a => a.ScaleLegs);
                            nds7.idx = 3;
                            return nds7;
                        case RandomizerDistribution.ScaleFeet:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(assets.Where(a => a.ScaleFeet != 0), a => a.ScaleFeet);
                            nds8.idx = 3;
                            return nds8;
                        case RandomizerDistribution.VolumeArms:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(assets.Where(a => a.VolumeArms != 0), a => a.VolumeArms);
                            nds9.idx = 3;
                            return nds9;
                        case RandomizerDistribution.VolumeLegs:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(assets.Where(a => a.VolumeLegs != 0), a => a.VolumeLegs);
                            nds10.idx = 3;
                            return nds10;
                        case RandomizerDistribution.VolumeBust:
                            NumericDistributionSetup nds11 = GetNumericDistributionSetup(assets.Where(a => a.VolumeBust != 0), a => a.VolumeBust);
                            // To counteract the large amount of npcs lowering the standard deviation. Why specifically this parameter? Who knows?
                            ((NormalConstant)nds11.distributions[3]).standardDeviation *= 2;
                            nds11.idx = 3;
                            return nds11;
                        case RandomizerDistribution.VolumeAbdomen:
                            NumericDistributionSetup nds12 = GetNumericDistributionSetup(assets.Where(a => a.VolumeAbdomen != 0), a => a.VolumeAbdomen);
                            nds12.idx = 3;
                            return nds12;
                        case RandomizerDistribution.VolumeTorso:
                            NumericDistributionSetup nds13 = GetNumericDistributionSetup(assets.Where(a => a.VolumeTorso != 0), a => a.VolumeTorso);
                            nds13.idx = 3;
                            return nds13;
                        case RandomizerDistribution.VolumeScaleArms:
                            NumericDistributionSetup nds14 = GetNumericDistributionSetup(assets.Where(a => a.VolumeScaleArms != 0), a => a.VolumeScaleArms);
                            nds14.idx = 3;
                            return nds14;
                        case RandomizerDistribution.VolumeScaleLegs:
                            NumericDistributionSetup nds15 = GetNumericDistributionSetup(assets.Where(a => a.VolumeScaleLegs != 0), a => a.VolumeScaleLegs);
                            nds15.idx = 3;
                            return nds15;
                        case RandomizerDistribution.MapScaleAll:
                            NumericDistributionSetup nds16 = GetNumericDistributionSetup(assets.Where(a => a.MapScaleAll != 0), a => a.MapScaleAll);
                            nds16.idx = 4;
                            return nds16;
                        case RandomizerDistribution.MapScaleHead:
                            NumericDistributionSetup nds17 = GetNumericDistributionSetup(assets.Where(a => a.MapScaleHead != 0), a => a.MapScaleHead);
                            nds17.idx = 4;
                            return nds17;
                        case RandomizerDistribution.MapScaleWing:
                            NumericDistributionSetup nds18 = GetNumericDistributionSetup(assets.Where(a => a.MapScaleWing != 0), a => a.MapScaleWing);
                            nds18.idx = 3;
                            return nds18;
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                case DataSetEnum.TypeOfSoldier:
                    List<TypeOfSoldier> toss0 = dataSet.Params.Cast<TypeOfSoldier>().ToList();
                    List<TypeOfSoldier> generalClasses = toss0.FilterData(tos => tos.Jid, GD.GeneralClasses);
                    List<TypeOfSoldier> allClasses = toss0.FilterData(tos => tos.Jid, GD.AllClasses);
                    switch (dfe)
                    {
                        case RandomizerDistribution.WeaponBaseCount:
                            return GetNumericDistributionSetup(generalClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.GetBasicWeaponRequirementCount());
                        case RandomizerDistribution.WeaponAdvancedCount:
                            return GetNumericDistributionSetup(generalClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.GetBasicWeaponRequirementCount());
                        case RandomizerDistribution.MaxWeaponLevelBase:
                            return GetNumericDistributionSetup(generalClasses.Where(tos => !tos.IsAdvancedOrSpecial()).SelectMany(tos => tos.GetMaxWeaponLevels().Where(s => s != "N")),
                                s => (int)s.ToProficiencyLevel());
                        case RandomizerDistribution.MaxWeaponLevelAdvanced:
                            return GetNumericDistributionSetup(generalClasses.Where(tos => tos.IsAdvancedOrSpecial()).SelectMany(tos => tos.GetMaxWeaponLevels().Where(s => s != "N")),
                                s => (int)s.ToProficiencyLevel());
                        case RandomizerDistribution.BaseHpBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseHp);
                        case RandomizerDistribution.BaseStrBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseStr);
                        case RandomizerDistribution.BaseTechBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseTech);
                        case RandomizerDistribution.BaseQuickBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseQuick);
                        case RandomizerDistribution.BaseLuckBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseLuck);
                        case RandomizerDistribution.BaseDefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseDef);
                        case RandomizerDistribution.BaseMagicBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseMagic);
                        case RandomizerDistribution.BaseMdefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseMdef);
                        case RandomizerDistribution.BasePhysBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BasePhys);
                        case RandomizerDistribution.BaseSightBase:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseSight);
                            nds0.distributions[0] = new UniformConstant(10, 2, 4);
                            nds0.idx = 0;
                            return nds0;
                        case RandomizerDistribution.BaseMoveBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseMove);
                        case RandomizerDistribution.BaseHpAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseHp);
                        case RandomizerDistribution.BaseStrAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseStr);
                        case RandomizerDistribution.BaseTechAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseTech);
                        case RandomizerDistribution.BaseQuickAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseQuick);
                        case RandomizerDistribution.BaseLuckAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseLuck);
                        case RandomizerDistribution.BaseDefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseDef);
                        case RandomizerDistribution.BaseMagicAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseMagic);
                        case RandomizerDistribution.BaseMdefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseMdef);
                        case RandomizerDistribution.BasePhysAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BasePhys);
                        case RandomizerDistribution.BaseSightAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseSight);
                        case RandomizerDistribution.BaseMoveAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseMove);
                        case RandomizerDistribution.BaseTotalBase:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.GetBasicBases().Select(b => (int)b).Sum());
                            nds1.idx = 4;
                            return nds1;
                        case RandomizerDistribution.BaseTotalAdvanced:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.GetBasicBases().Select(b => (int)b).Sum());
                            nds2.idx = 4;
                            return nds2;
                        case RandomizerDistribution.LimitHpBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitHp);
                        case RandomizerDistribution.LimitStrBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitStr);
                        case RandomizerDistribution.LimitTechBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitTech);
                        case RandomizerDistribution.LimitQuickBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitQuick);
                        case RandomizerDistribution.LimitLuckBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitLuck);
                        case RandomizerDistribution.LimitDefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitDef);
                        case RandomizerDistribution.LimitMagicBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitMagic);
                        case RandomizerDistribution.LimitMdefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitMdef);
                        case RandomizerDistribution.LimitPhysBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitPhys);
                        case RandomizerDistribution.LimitSightBase:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitSight);
                            nds3.distributions[3] = new NormalConstant(10, 20, 5);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.LimitMoveBase:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.LimitMove);
                            nds4.distributions[3] = new NormalConstant(10, 20, 5);
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.LimitHpAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitHp);
                        case RandomizerDistribution.LimitStrAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitStr);
                        case RandomizerDistribution.LimitTechAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitTech);
                        case RandomizerDistribution.LimitQuickAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitQuick);
                        case RandomizerDistribution.LimitLuckAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitLuck);
                        case RandomizerDistribution.LimitDefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitDef);
                        case RandomizerDistribution.LimitMagicAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitMagic);
                        case RandomizerDistribution.LimitMdefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitMdef);
                        case RandomizerDistribution.LimitPhysAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitPhys);
                        case RandomizerDistribution.LimitSightAdvanced:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitSight);
                            nds5.distributions[3] = new NormalConstant(10, 20, 5);
                            nds5.idx = 3;
                            return nds5;
                        case RandomizerDistribution.LimitMoveAdvanced:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.LimitMove);
                            nds6.distributions[3] = new NormalConstant(10, 20, 5);
                            nds6.idx = 3;
                            return nds6;
                        case RandomizerDistribution.LimitTotalBase:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.GetBasicLimits().Select(b => (int)b).Sum());
                            nds7.idx = 4;
                            return nds7;
                        case RandomizerDistribution.LimitTotalAdvanced:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.GetBasicLimits().Select(b => (int)b).Sum());
                            nds8.idx = 4;
                            return nds8;
                        case RandomizerDistribution.BaseGrowHpBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowHp);
                        case RandomizerDistribution.BaseGrowStrBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowStr);
                        case RandomizerDistribution.BaseGrowTechBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowTech);
                        case RandomizerDistribution.BaseGrowQuickBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowQuick);
                        case RandomizerDistribution.BaseGrowLuckBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowLuck);
                        case RandomizerDistribution.BaseGrowDefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowDef);
                        case RandomizerDistribution.BaseGrowMagicBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowMagic);
                        case RandomizerDistribution.BaseGrowMdefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowMdef);
                        case RandomizerDistribution.BaseGrowPhysBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowPhys);
                        case RandomizerDistribution.BaseGrowSightBase:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowSight);
                            nds9.distributions[0] = new UniformConstant(10, 0, 10);
                            nds9.idx = 0;
                            return nds9;
                        case RandomizerDistribution.BaseGrowMoveBase:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowMove);
                            nds10.distributions[0] = new UniformConstant(10, 0, 10);
                            nds10.idx = 0;
                            return nds10;
                        case RandomizerDistribution.BaseGrowHpAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowHp);
                        case RandomizerDistribution.BaseGrowStrAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowStr);
                        case RandomizerDistribution.BaseGrowTechAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowTech);
                        case RandomizerDistribution.BaseGrowQuickAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowQuick);
                        case RandomizerDistribution.BaseGrowLuckAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowLuck);
                        case RandomizerDistribution.BaseGrowDefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowDef);
                        case RandomizerDistribution.BaseGrowMagicAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowMagic);
                        case RandomizerDistribution.BaseGrowMdefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowMdef);
                        case RandomizerDistribution.BaseGrowPhysAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowPhys);
                        case RandomizerDistribution.BaseGrowSightAdvanced:
                            NumericDistributionSetup nds11 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowSight);
                            nds11.distributions[0] = new UniformConstant(10, 0, 10);
                            nds11.idx = 0;
                            return nds11;
                        case RandomizerDistribution.BaseGrowMoveAdvanced:
                            NumericDistributionSetup nds12 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.BaseGrowMove);
                            nds12.distributions[0] = new UniformConstant(10, 0, 10);
                            nds12.idx = 0;
                            return nds12;
                        case RandomizerDistribution.BaseGrowTotalBase:
                            NumericDistributionSetup nds13 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()), tos => tos.GetBasicEnemyGrowths().Select(b => (int)b).Sum());
                            nds13.idx = 4;
                            return nds13;
                        case RandomizerDistribution.BaseGrowTotalAdvanced:
                            NumericDistributionSetup nds14 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial()), tos => tos.GetBasicEnemyGrowths().Select(b => (int)b).Sum());
                            nds14.idx = 4;
                            return nds14;
                        case RandomizerDistribution.DiffGrowHpBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowHp);
                        case RandomizerDistribution.DiffGrowStrBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowStr);
                        case RandomizerDistribution.DiffGrowTechBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowTech);
                        case RandomizerDistribution.DiffGrowQuickBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowQuick);
                        case RandomizerDistribution.DiffGrowLuckBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowLuck);
                        case RandomizerDistribution.DiffGrowDefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowDef);
                        case RandomizerDistribution.DiffGrowMagicBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowMagic);
                        case RandomizerDistribution.DiffGrowMdefBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowMdef);
                        case RandomizerDistribution.DiffGrowPhysBase:
                            return GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowPhys);
                        case RandomizerDistribution.DiffGrowSightBase:
                            NumericDistributionSetup nds15 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowSight);
                            nds15.distributions[3] = new NormalConstant(10, 0, 5);
                            nds15.idx = 3;
                            return nds15;
                        case RandomizerDistribution.DiffGrowMoveBase:
                            NumericDistributionSetup nds16 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowMove);
                            nds16.distributions[3] = new NormalConstant(10, 0, 5);
                            nds16.idx = 3;
                            return nds16;
                        case RandomizerDistribution.DiffGrowHpAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowHp);
                        case RandomizerDistribution.DiffGrowStrAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowStr);
                        case RandomizerDistribution.DiffGrowTechAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowTech);
                        case RandomizerDistribution.DiffGrowQuickAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowQuick);
                        case RandomizerDistribution.DiffGrowLuckAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowLuck);
                        case RandomizerDistribution.DiffGrowDefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowDef);
                        case RandomizerDistribution.DiffGrowMagicAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowMagic);
                        case RandomizerDistribution.DiffGrowMdefAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowMdef);
                        case RandomizerDistribution.DiffGrowPhysAdvanced:
                            return GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowPhys);
                        case RandomizerDistribution.DiffGrowSightAdvanced:
                            NumericDistributionSetup nds17 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowSight);
                            nds17.distributions[3] = new NormalConstant(10, 0, 5);
                            nds17.idx = 3;
                            return nds17;
                        case RandomizerDistribution.DiffGrowMoveAdvanced:
                            NumericDistributionSetup nds18 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.DiffGrowMove);
                            nds18.distributions[3] = new NormalConstant(10, 0, 5);
                            nds18.idx = 3;
                            return nds18;
                        case RandomizerDistribution.DiffGrowTotalBase:
                            NumericDistributionSetup nds19 = GetNumericDistributionSetup(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.GetBasicGrowthModifiers().Select(b => (int)b).Sum());
                            nds19.idx = 4;
                            return nds19;
                        case RandomizerDistribution.DiffGrowTotalAdvanced:
                            NumericDistributionSetup nds20 = GetNumericDistributionSetup(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()), tos => tos.GetBasicGrowthModifiers().Select(b => (int)b).Sum());
                            nds20.idx = 4;
                            return nds20;
                        case RandomizerDistribution.Attrs:
                            return GetNumericDistributionSetup(allClasses, i => i.GetAttributes().Count);
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                case DataSetEnum.Individual:
                    List<Individual> individuals = dataSet.Params.Cast<Individual>().ToList();
                    List<Individual> playableCharacters = individuals.FilterData(i => i.Pid, GD.PlayableCharacters);
                    List<Individual> allyCharacters = individuals.FilterData(i => i.Pid, GD.AllyCharacters);
                    List<Individual> enemyCharacters = individuals.FilterData(i => i.Pid, GD.EnemyCharacters);
                    List<Individual> npcCharacters = individuals.FilterData(i => i.Pid, GD.NPCCharacters);
                    List<TypeOfSoldier> toss1 = GD.Get(DataSetEnum.TypeOfSoldier).Params.Cast<TypeOfSoldier>().ToList();
                    switch (dfe)
                    {
                        case RandomizerDistribution.Age:
                            return GetNumericDistributionSetup(playableCharacters.Where(i => i.Age != -1).ToList(), i => i.Age);
                        case RandomizerDistribution.LevelAlly:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(allyCharacters, i => i.Level);
                            nds0.distributions[4] = new NormalRelative(100, 1);
                            nds0.idx = 4;
                            return nds0;
                        case RandomizerDistribution.LevelEnemy:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(enemyCharacters, i => i.Level);
                            nds1.distributions[4] = new NormalRelative(100, 1);
                            nds1.idx = 4;
                            return nds1;
                        case RandomizerDistribution.InternalLevel:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(playableCharacters, i => i.GetInternalLevel(toss1));
                            nds2.distributions[4] = new NormalRelative(100, 1);
                            nds2.idx = 4;
                            return nds2;
                        case RandomizerDistribution.SkillPoint:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(playableCharacters, i => i.SkillPoint);
                            nds3.idx = 5;
                            return nds3;
                        case RandomizerDistribution.IndividualAptitude:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(playableCharacters, i => i.GetAptitudes().Count);
                            nds4.distributions[0] = new UniformConstant(10, 0, 2);
                            nds4.idx = 0;
                            return nds4;
                        case RandomizerDistribution.SubAptitude:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(playableCharacters, i => i.GetSubAptitudes().Count);
                            nds5.idx = 3;
                            return nds5;
                        case RandomizerDistribution.OffsetNHpAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNHp);
                        case RandomizerDistribution.OffsetNStrAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNStr);
                        case RandomizerDistribution.OffsetNTechAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNTech);
                        case RandomizerDistribution.OffsetNQuickAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNQuick);
                        case RandomizerDistribution.OffsetNLuckAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNLuck);
                        case RandomizerDistribution.OffsetNDefAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNDef);
                        case RandomizerDistribution.OffsetNMagicAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNMagic);
                        case RandomizerDistribution.OffsetNMdefAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNMdef);
                        case RandomizerDistribution.OffsetNPhysAlly:
                            return GetNumericDistributionSetup(allyCharacters, i => i.OffsetNPhys);
                        case RandomizerDistribution.OffsetNSightAlly:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(allyCharacters, i => i.OffsetNSight);
                            nds6.distributions[0] = new UniformConstant(10, -1, 1);
                            nds6.idx = 0;
                            return nds6;
                        case RandomizerDistribution.OffsetNMoveAlly:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(allyCharacters, i => i.OffsetNMove);
                            nds7.distributions[0] = new UniformConstant(10, -1, 1);
                            nds7.idx = 0;
                            return nds7;
                        case RandomizerDistribution.OffsetNTotalAlly:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(allyCharacters, i => i.GetBasicOffsetN().Select(s => (int)s).Sum());
                            nds8.idx = 4;
                            return nds8;
                        case RandomizerDistribution.OffsetNHpEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNHp);
                        case RandomizerDistribution.OffsetNStrEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNStr);
                        case RandomizerDistribution.OffsetNTechEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNTech);
                        case RandomizerDistribution.OffsetNQuickEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNQuick);
                        case RandomizerDistribution.OffsetNLuckEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNLuck);
                        case RandomizerDistribution.OffsetNDefEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNDef);
                        case RandomizerDistribution.OffsetNMagicEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNMagic);
                        case RandomizerDistribution.OffsetNMdefEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNMdef);
                        case RandomizerDistribution.OffsetNPhysEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNPhys);
                        case RandomizerDistribution.OffsetNSightEnemy:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNSight);
                            nds9.distributions[0] = new UniformConstant(10, -1, 1);
                            nds9.idx = 0;
                            return nds9;
                        case RandomizerDistribution.OffsetNMoveEnemy:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(enemyCharacters, i => i.OffsetNMove);
                            nds10.distributions[0] = new UniformConstant(10, -1, 1);
                            nds10.idx = 0;
                            return nds10;
                        case RandomizerDistribution.OffsetNTotalEnemy:
                            NumericDistributionSetup nds11 = GetNumericDistributionSetup(enemyCharacters, i => i.GetBasicOffsetN().Select(s => (int)s).Sum());
                            nds11.idx = 4;
                            return nds11;
                        case RandomizerDistribution.OffsetHHpEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHHp);
                        case RandomizerDistribution.OffsetHStrEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHStr);
                        case RandomizerDistribution.OffsetHTechEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHTech);
                        case RandomizerDistribution.OffsetHQuickEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHQuick);
                        case RandomizerDistribution.OffsetHLuckEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHLuck);
                        case RandomizerDistribution.OffsetHDefEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHDef);
                        case RandomizerDistribution.OffsetHMagicEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHMagic);
                        case RandomizerDistribution.OffsetHMdefEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHMdef);
                        case RandomizerDistribution.OffsetHPhysEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHPhys);
                        case RandomizerDistribution.OffsetHSightEnemy:
                            NumericDistributionSetup nds12 = GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHSight);
                            nds12.distributions[0] = new UniformConstant(10, -1, 1);
                            nds12.idx = 0;
                            return nds12;
                        case RandomizerDistribution.OffsetHMoveEnemy:
                            NumericDistributionSetup nds13 = GetNumericDistributionSetup(enemyCharacters, i => i.OffsetHMove);
                            nds13.distributions[0] = new UniformConstant(10, -1, 1);
                            nds13.idx = 0;
                            return nds13;
                        case RandomizerDistribution.OffsetHTotalEnemy:
                            NumericDistributionSetup nds14 = GetNumericDistributionSetup(enemyCharacters, i => i.GetBasicOffsetH().Select(s => (int)s).Sum());
                            nds14.idx = 4;
                            return nds14;
                        case RandomizerDistribution.OffsetLHpEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLHp);
                        case RandomizerDistribution.OffsetLStrEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLStr);
                        case RandomizerDistribution.OffsetLTechEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLTech);
                        case RandomizerDistribution.OffsetLQuickEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLQuick);
                        case RandomizerDistribution.OffsetLLuckEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLLuck);
                        case RandomizerDistribution.OffsetLDefEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLDef);
                        case RandomizerDistribution.OffsetLMagicEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLMagic);
                        case RandomizerDistribution.OffsetLMdefEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLMdef);
                        case RandomizerDistribution.OffsetLPhysEnemy:
                            return GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLPhys);
                        case RandomizerDistribution.OffsetLSightEnemy:
                            NumericDistributionSetup nds15 = GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLSight);
                            nds15.distributions[0] = new UniformConstant(10, -1, 1);
                            nds15.idx = 0;
                            return nds15;
                        case RandomizerDistribution.OffsetLMoveEnemy:
                            NumericDistributionSetup nds16 = GetNumericDistributionSetup(enemyCharacters, i => i.OffsetLMove);
                            nds16.distributions[0] = new UniformConstant(10, -1, 1);
                            nds16.idx = 0;
                            return nds16;
                        case RandomizerDistribution.OffsetLTotalEnemy:
                            NumericDistributionSetup nds17 = GetNumericDistributionSetup(enemyCharacters, i => i.GetBasicOffsetL().Select(s => (int)s).Sum());
                            nds17.idx = 4;
                            return nds17;
                        case RandomizerDistribution.LimitHp:
                            NumericDistributionSetup nds18 = GetNumericDistributionSetup(playableCharacters, i => i.LimitHp);
                            nds18.distributions[0] = new UniformConstant(10, -1, 1);
                            nds18.idx = 0;
                            return nds18;
                        case RandomizerDistribution.LimitStr:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitStr);
                        case RandomizerDistribution.LimitTech:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitTech);
                        case RandomizerDistribution.LimitQuick:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitQuick);
                        case RandomizerDistribution.LimitLuck:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitLuck);
                        case RandomizerDistribution.LimitDef:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitDef);
                        case RandomizerDistribution.LimitMagic:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitMagic);
                        case RandomizerDistribution.LimitMdef:
                            return GetNumericDistributionSetup(playableCharacters, i => i.LimitMdef);
                        case RandomizerDistribution.LimitPhys:
                            NumericDistributionSetup nds19 = GetNumericDistributionSetup(playableCharacters, i => i.LimitPhys);
                            nds19.distributions[0] = new UniformConstant(10, -1, 1);
                            nds19.idx = 0;
                            return nds19;
                        case RandomizerDistribution.LimitSight:
                            NumericDistributionSetup nds20 = GetNumericDistributionSetup(playableCharacters, i => i.LimitSight);
                            nds20.distributions[0] = new UniformConstant(10, -1, 1);
                            nds20.idx = 0;
                            return nds20;
                        case RandomizerDistribution.LimitMove:
                            NumericDistributionSetup nds21 = GetNumericDistributionSetup(playableCharacters, i => i.LimitMove);
                            nds21.distributions[0] = new UniformConstant(10, -1, 1);
                            nds21.idx = 0;
                            return nds21;
                        case RandomizerDistribution.GrowHp:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowHp);
                        case RandomizerDistribution.GrowStr:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowStr);
                        case RandomizerDistribution.GrowTech:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowTech);
                        case RandomizerDistribution.GrowQuick:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowQuick);
                        case RandomizerDistribution.GrowLuck:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowLuck);
                        case RandomizerDistribution.GrowDef:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowDef);
                        case RandomizerDistribution.GrowMagic:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowMagic);
                        case RandomizerDistribution.GrowMdef:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowMdef);
                        case RandomizerDistribution.GrowPhys:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GrowPhys);
                        case RandomizerDistribution.GrowSight:
                            NumericDistributionSetup nds22 = GetNumericDistributionSetup(playableCharacters, i => i.GrowSight);
                            nds22.distributions[0] = new UniformConstant(10, 0, 5);
                            nds22.idx = 0;
                            return nds22;
                        case RandomizerDistribution.GrowMove:
                            NumericDistributionSetup nds23 = GetNumericDistributionSetup(playableCharacters, i => i.GrowMove);
                            nds23.distributions[0] = new UniformConstant(10, 0, 5);
                            nds23.idx = 0;
                            return nds23;
                        case RandomizerDistribution.GrowTotal:
                            NumericDistributionSetup nds24 = GetNumericDistributionSetup(playableCharacters, i =>
                                i.GetBasicGrowths().Select(b => (int)b).Sum());
                            nds24.idx = 4;
                            return nds24;
                        case RandomizerDistribution.ItemsWeapons:
                            List<string> weaponIDs = GD.NormalWeapons.GetIDs();
                            NumericDistributionSetup nds25 = GetNumericDistributionSetup(playableCharacters, i =>
                                i.Items.Where(weaponIDs.Contains).Count());
                            nds25.distributions[4] = new NormalRelative(100, 0.4);
                            nds25.idx = 4;
                            return nds25;
                        case RandomizerDistribution.ItemsItems:
                            List<string> itemIDs = GD.BattleItems.GetIDs();
                            NumericDistributionSetup nds26 = GetNumericDistributionSetup(playableCharacters, i =>
                                i.Items.Where(itemIDs.Contains).Count());
                            nds26.distributions[4] = new NormalRelative(100, 0.4);
                            nds26.idx = 4;
                            return nds26;
                        case RandomizerDistribution.AttrsAlly:
                            return GetNumericDistributionSetup(playableCharacters, i => i.GetAttributes().Count);
                        case RandomizerDistribution.AttrsEnemy:
                            return GetNumericDistributionSetup(npcCharacters, i => i.GetAttributes().Count);
                        case RandomizerDistribution.CommonSids:
                            List<string> generalSkillIDs1 = GD.GeneralSkills.GetIDs();
                            NumericDistributionSetup nds27 = GetNumericDistributionSetup(playableCharacters, i => i.CommonSids.Where(generalSkillIDs1.Contains).Count());
                            nds27.distributions[1] = new UniformRelative(10, -1, 1);
                            nds27.idx = 1;
                            return nds27;
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                default:
                    throw new ArgumentException("Unsupported data set: " + dse);
            }
        }

        internal SelectionDistributionSetup GetSelectionDistributionSetup(RandomizerDistribution dfe)
        {
            DataSetEnum dse = DistributionToDataSet[dfe];
            DataSet dataSet = GD.Get(dse);
            switch (dse)
            {
                case DataSetEnum.GodGeneral:
                    List<GodGeneral> ggs = dataSet.Params.Cast<GodGeneral>().ToList();
                    List<GodGeneral> allyEngageableEmblems = ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems);
                    switch (dfe)
                    {
                        case RandomizerDistribution.Link:
                            SelectionDistributionSetup sds0 = GetSelectionDistributionSetup(
                                allyEngageableEmblems, gg => gg.Link, GD.PlayableCharacters);
                            sds0.idx = 1;
                            return sds0;
                        case RandomizerDistribution.EngageAttackAlly:
                            return GetSelectionDistributionSetup(
                                allyEngageableEmblems, gg => gg.EngageAttack, GD.CompatibleAsEngageAttacks);
                        case RandomizerDistribution.EngageAttackEnemy:
                            SelectionDistributionSetup sds3 = GetSelectionDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.EnemyEngageableEmblems),
                                gg => gg.EngageAttack, GD.CompatibleAsEngageAttacks);
                            sds3.idx = 0;
                            return sds3;
                        case RandomizerDistribution.EngageAttackLink:
                            SelectionDistributionSetup sds1 = GetSelectionDistributionSetup(
                                allyEngageableEmblems, gg => gg.EngageAttackLink, GD.CompatibleAsEngageAttacks);
                            sds1.idx = 0;
                            return sds1;
                        case RandomizerDistribution.LinkGid:
                            SelectionDistributionSetup sds2 = GetSelectionDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.LinkableEmblems), gg => gg.LinkGid, GD.LinkableEmblems);
                            sds2.idx = 1;
                            return sds2;
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    };
                case DataSetEnum.GrowthTable:
                    List<ParamGroup> pgs = dataSet.Params.Cast<ParamGroup>().ToList();
                    List<string> syncStatSkillIDs = GD.SyncStatSkills.GetIDs();
                    List<string> generalSkillIDs0 = GD.GeneralSkills.GetIDs();
                    switch (dfe)
                    {
                        case RandomizerDistribution.InheritanceSkills:
                            return GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.InheritanceSkills), s => s, GD.GeneralSkills);
                        case RandomizerDistribution.SynchroStatSkillsAlly:
                            return GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => syncStatSkillIDs.Contains(s))), s => s, GD.SyncStatSkills);
                        case RandomizerDistribution.SynchroStatSkillsEnemy:
                            SelectionDistributionSetup sds0 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => syncStatSkillIDs.Contains(s))), s => s, GD.SyncStatSkills);
                            sds0.idx = 1;
                            return sds0;
                        case RandomizerDistribution.SynchroGeneralSkillsAlly:
                            return GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => generalSkillIDs0.Contains(s))), s => s, GD.GeneralSkills);
                        case RandomizerDistribution.SynchroGeneralSkillsEnemy:
                            SelectionDistributionSetup sds1 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => generalSkillIDs0.Contains(s))), s => s, GD.GeneralSkills);
                            sds1.idx = 1;
                            return sds1;
                        case RandomizerDistribution.EngageSkills:
                            SelectionDistributionSetup sds2 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.BondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.EngageSkills.Where(s => generalSkillIDs0.Contains(s))), s => s, GD.GeneralSkills);
                            sds2.distributions[1] = new UniformSelection(100, ((Empirical)sds2.distributions[0]).weights.Select(i => i > 0).ToList());
                            sds2.idx = 1;
                            return sds2;
                        case RandomizerDistribution.EngageItems:
                            SelectionDistributionSetup sds3 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.BondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                {
                                    List<string> itemIDs = gt.EngageItems.ToList();
                                    itemIDs.AddRange(gt.EngageCooperations);
                                    itemIDs.AddRange(gt.EngageHorses);
                                    itemIDs.AddRange(gt.EngageCoverts);
                                    itemIDs.AddRange(gt.EngageHeavys);
                                    itemIDs.AddRange(gt.EngageFlys);
                                    itemIDs.AddRange(gt.EngageMagics);
                                    itemIDs.AddRange(gt.EngagePranas);
                                    itemIDs.AddRange(gt.EngageDragons);
                                    return itemIDs;
                                }), s => s, GD.EngageWeapons);
                            sds3.idx = 1;
                            return sds3;
                        case RandomizerDistribution.GrowthTableAptitude:
                            return GetSelectionDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables).SelectMany(pg =>
                                pg.Group.Cast<GrowthTable>()).SelectMany(gt => gt.GetAptitudes()), i => i, GD.Proficiencies);
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                case DataSetEnum.TypeOfSoldier:
                    List<TypeOfSoldier> toss = dataSet.Params.Cast<TypeOfSoldier>().ToList();
                    List<TypeOfSoldier> allClasses = toss.FilterData(tos => tos.Jid, GD.AllClasses);
                    List<TypeOfSoldier> generalClasses = toss.FilterData(tos => tos.Jid, GD.GeneralClasses);
                    switch (dfe)
                    {
                        case RandomizerDistribution.StyleName:
                            return GetSelectionDistributionSetup(allClasses, tos => tos.StyleName, GD.UnitTypes);
                        case RandomizerDistribution.MoveType:
                            return GetSelectionDistributionSetup(generalClasses, tos => tos.MoveType, GD.MovementTypes);
                        case RandomizerDistribution.Weapon:
                            SelectionDistributionSetup sds0 = GetSelectionDistributionSetup(generalClasses.SelectMany(tos => tos.GetBasicWeaponRequirements()),
                                i => i, GD.BasicProficiencies);
                            sds0.idx = 1;
                            return sds0;
                        case RandomizerDistribution.LearningSkill:
                            return GetSelectionDistributionSetup(allClasses.Where(tos => tos.LearningSkill != ""), tos => tos.LearningSkill, GD.GeneralSkills);
                        case RandomizerDistribution.LunaticSkill:
                            return GetSelectionDistributionSetup(allClasses.Where(tos => tos.LunaticSkill != ""), tos => tos.LunaticSkill, GD.GeneralSkills);
                        case RandomizerDistribution.Attrs:
                            return GetSelectionDistributionSetup(allClasses.SelectMany(tos => tos.GetAttributes()), i => i, GD.Attributes);
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                case DataSetEnum.Individual:
                    List<Individual> individuals = dataSet.Params.Cast<Individual>().ToList();
                    List<Individual> playableCharacters = individuals.FilterData(i => i.Pid, GD.PlayableCharacters);
                    List<Individual> npcCharacters = individuals.FilterData(i => i.Pid, GD.NPCCharacters);
                    List<string> generalSkillIDs1 = GD.GeneralSkills.GetIDs();
                    switch (dfe)
                    {
                        case RandomizerDistribution.JidAlly:
                            return GetSelectionDistributionSetup(playableCharacters, i => i.Jid, GD.PlayableClasses);
                        case RandomizerDistribution.JidEnemy:
                            List<string> generalClassIDs = GD.GeneralClasses.GetIDs();
                            return GetSelectionDistributionSetup(individuals.FilterData(i => i.Pid, GD.NPCCharacters).Where(i =>
                                generalClassIDs.Contains(i.Jid)), i => i.Jid, GD.GeneralClasses);
                        case RandomizerDistribution.SupportCategory:
                            return GetSelectionDistributionSetup(playableCharacters, i => i.SupportCategory, GD.SupportCategories);
                        case RandomizerDistribution.IndividualAptitude:
                            return GetSelectionDistributionSetup(playableCharacters.SelectMany(i => i.GetAptitudes()), i => i,
                                GD.Proficiencies);
                        case RandomizerDistribution.SubAptitude:
                            return GetSelectionDistributionSetup(playableCharacters.SelectMany(i => i.GetSubAptitudes()), i => i,
                                GD.Proficiencies);
                        case RandomizerDistribution.ItemsWeapons:
                            List<string> weaponIDs = GD.NormalWeapons.GetIDs();
                            SelectionDistributionSetup sds0 = GetSelectionDistributionSetup(playableCharacters.SelectMany(i =>
                                i.Items.Where(weaponIDs.Contains)), i => i, GD.NormalWeapons);
                            sds0.idx = 1;
                            return sds0;
                        case RandomizerDistribution.ItemsItems:
                            List<string> itemIDs = GD.BattleItems.GetIDs();
                            SelectionDistributionSetup sds1 = GetSelectionDistributionSetup(playableCharacters.SelectMany(i =>
                                i.Items.Where(itemIDs.Contains)), i => i, GD.BattleItems);
                            sds1.idx = 1;
                            return sds1;
                        case RandomizerDistribution.AttrsAlly:
                            return GetSelectionDistributionSetup(playableCharacters.SelectMany(i => i.GetAttributes()), i => i,
                                GD.Attributes);
                        case RandomizerDistribution.AttrsEnemy:
                            return GetSelectionDistributionSetup(npcCharacters.SelectMany(i => i.GetAttributes()), i => i,
                                GD.Attributes);
                        case RandomizerDistribution.CommonSids:
                            return GetSelectionDistributionSetup(npcCharacters.SelectMany(i =>
                                i.CommonSids.Where(generalSkillIDs1.Contains)), i => i, GD.GeneralSkills);
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    }
                default:
                    throw new ArgumentException("Unsupported data set: " + dse);
            }
        }

        /// <summary>
        ///  Generates numeric distribution objects.
        /// </summary>
        private static NumericDistributionSetup GetNumericDistributionSetup<T>(IEnumerable<T> objects, Func<T, double> f)
        {
            List<double> observations = new();
            for (int item = 0; item < objects.Count(); item++)
                observations.Add(f(objects.ElementAt(item)));
            return GetNumericDistributionSetup(observations);
        }

        /// <summary>
        ///  Generates numeric distribution objects from a list of observations.
        /// </summary>
        private static NumericDistributionSetup GetNumericDistributionSetup(IList<double> observations)
        {
            double min = observations.Count > 0 ? observations.Min() : 0;
            double max = observations.Count > 0 ? observations.Max() : 0;
            double uniAvg = (min + max) / 2.0;
            double avg = observations.Count > 0 ? observations.Average() : 0;
            double std = observations.StandardDeviation();

            IDistribution[] distributions = new IDistribution[]
            {
                new UniformConstant(100, min, max),
                new UniformRelative(100, (min-uniAvg)/2, (max-uniAvg)/2),
                new UniformProportional(100, (min+uniAvg)/(2*uniAvg == 0 ? 1 : 2*uniAvg), (max+uniAvg)/(2*uniAvg == 0 ? 1 : 2*uniAvg)),
                new NormalConstant(100, avg, std),
                new NormalRelative(100, std/2),
                new NormalProportional(100, std/(2*avg == 0 ? 1 : 2*avg)),
                new Redistribution(100)
            };
            return new NumericDistributionSetup()
            {
                distributions = distributions,
                idx = 6
            };
        }

        /// <summary>
        ///  Generates selection distribution objects.
        /// </summary>
        private static SelectionDistributionSetup GetSelectionDistributionSetup<T>(IEnumerable<T> objects,
            Func<T, string> getID, IEnumerable<(string id, string name)> entities)
        {
            int[] instances = new int[entities.Count()];
            List<string> ids = entities.Select(t => t.id).ToList();
            for (int i = 0; i < objects.Count(); i++)
            {
                string id = getID(objects.ElementAt(i));
                if (id != "")
                    instances[ids.IndexOf(id)]++;
            }
            return GetSelectionDistributionSetup(instances, entities.Select(t => t.name));
        }

        /// <summary>
        ///  Generates selection distribution objects.
        /// </summary>
        private static SelectionDistributionSetup GetSelectionDistributionSetup<T>(IEnumerable<T> objects,
            Func<T, int> getID, IEnumerable<(int id, string name)> entities)
        {
            int[] instances = new int[entities.Count()];
            List<int> ids = entities.Select(t => t.id).ToList();
            for (int i = 0; i < objects.Count(); i++)
            {
                int id = getID(objects.ElementAt(i));
                instances[ids.IndexOf(id)]++;
            }
            return GetSelectionDistributionSetup(instances, entities.Select(t => t.name));
        }

        /// <summary>
        ///  Generates selection distribution objects from an array of instances.
        /// </summary>
        private static SelectionDistributionSetup GetSelectionDistributionSetup(int[] instances,
            IEnumerable<string> names)
        {
            IDistribution[] distributions = new IDistribution[]
            {
                new Empirical(100, instances.ToList()),
                new UniformSelection(100, names.Select(_ => true).ToList()),
                new Redistribution(100)
            };
            return new()
            {
                distributions = distributions,
                names = names.ToList(),
                idx = 2
            };
        }
    }

    internal struct NumericDistributionSetup
    {
        internal IDistribution[] distributions;
        internal int idx;
    }

    internal struct SelectionDistributionSetup
    {
        internal IDistribution[] distributions;
        internal List<string> names;
        internal int idx;
    }
}
