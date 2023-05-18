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
                                ggs.FilterData(gg => gg.Gid, GD.AllySynchableEmblems),
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
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceHp);
                            nds1.distributions[3] = new NormalConstant(10, 0, 8);
                            nds1.idx = 3;
                            return nds1;
                        case RandomizerDistribution.SynchroEnhanceStrAlly:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceStr);
                            nds2.distributions[3] = new NormalConstant(10, 0, 5);
                            nds2.idx = 3;
                            return nds2;
                        case RandomizerDistribution.SynchroEnhanceTechAlly:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceTech);
                            nds3.distributions[3] = new NormalConstant(10, 0, 5);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.SynchroEnhanceQuickAlly:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceQuick);
                            nds4.distributions[3] = new NormalConstant(10, 0, 5);
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.SynchroEnhanceLuckAlly:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceLuck);
                            nds5.distributions[3] = new NormalConstant(10, 0, 5);
                            nds5.idx = 3;
                            return nds5;
                        case RandomizerDistribution.SynchroEnhanceDefAlly:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceDef);
                            nds6.distributions[3] = new NormalConstant(10, 0, 5);
                            nds6.idx = 3;
                            return nds6;
                        case RandomizerDistribution.SynchroEnhanceMagicAlly:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceMagic);
                            nds7.distributions[3] = new NormalConstant(10, 0, 5);
                            nds7.idx = 3;
                            return nds7;
                        case RandomizerDistribution.SynchroEnhanceMdefAlly:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceMdef);
                            nds8.distributions[3] = new NormalConstant(10, 0, 5);
                            nds8.idx = 3;
                            return nds8;
                        case RandomizerDistribution.SynchroEnhancePhysAlly:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhancePhys);
                            nds9.distributions[3] = new NormalConstant(10, 0, 5);
                            nds9.idx = 3;
                            return nds9;
                        case RandomizerDistribution.SynchroEnhanceMoveAlly:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(
                                ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems),
                                gg => gg.SynchroEnhanceMove);
                            nds10.distributions[3] = new NormalConstant(10, 0, 0.5);
                            nds10.idx = 3;
                            return nds10;
                        case RandomizerDistribution.SynchroEnhanceHpEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceHp);
                        case RandomizerDistribution.SynchroEnhanceStrEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceStr);
                        case RandomizerDistribution.SynchroEnhanceTechEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceTech);
                        case RandomizerDistribution.SynchroEnhanceQuickEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceQuick);
                        case RandomizerDistribution.SynchroEnhanceLuckEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceLuck);
                        case RandomizerDistribution.SynchroEnhanceDefEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceDef);
                        case RandomizerDistribution.SynchroEnhanceMagicEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceMagic);
                        case RandomizerDistribution.SynchroEnhanceMdefEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceMdef);
                        case RandomizerDistribution.SynchroEnhancePhysEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhancePhys);
                        case RandomizerDistribution.SynchroEnhanceMoveEnemy:
                            return GetNumericDistributionSetup(ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems), gg =>
                                gg.SynchroEnhanceMove);
                        default:
                            throw new ArgumentException("Unsupported data field: " + dfe);
                    };
                case DataSetEnum.GrowthTable:
                    List<ParamGroup> pgs = dataSet.Params.Cast<ParamGroup>().ToList();
                    List<string> synchStatSkillIDs = GD.SynchStatSkills.GetIDs();
                    List<string> generalSkillIDs = GD.GeneralSkills.GetIDs();
                    switch (dfe)
                    {
                        case RandomizerDistribution.InheritanceSkills:
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables),
                                pg => pg.Group.Cast<GrowthTable>().Select(gt => gt.InheritanceSkills.Length).Sum());
                            nds0.idx = 3;
                            return nds0;
                        case RandomizerDistribution.SynchroStatSkillsAlly:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(synchStatSkillIDs.Contains)).Sum());
                            nds1.idx = 3;
                            return nds1;
                        case RandomizerDistribution.SynchroStatSkillsEnemy:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(synchStatSkillIDs.Contains)).Sum());
                            nds2.distributions[3] = new NormalConstant(10, 10, 1);
                            nds2.idx = 3;
                            return nds2;
                        case RandomizerDistribution.SynchroGeneralSkillsAlly:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(generalSkillIDs.Contains)).Sum());
                            nds3.distributions[3] = new NormalConstant(100, 3, 1);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.SynchroGeneralSkillsEnemy:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.SynchroSkills.Count(generalSkillIDs.Contains)).Sum());
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.EngageSkills:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.BondLevelTables), pg =>
                                pg.Group.Cast<GrowthTable>().Select(gt => gt.EngageSkills.Count(generalSkillIDs.Contains)).Sum());
                            nds5.distributions[0] = new UniformConstant(10, 0, 2);
                            nds5.idx = 0;
                            return nds5;
                        case RandomizerDistribution.EngageItems:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(pgs.FilterData(pg => pg.Name, GD.BondLevelTables), pg =>
                            pg.Group.Cast<GrowthTable>().Select(gt => gt.EngageSkills.Length + gt.EngageDragons.Length).Sum());
                            nds6.distributions[0] = new UniformConstant(10, 2, 4);
                            nds6.idx = 0;
                            return nds6;
                        case RandomizerDistribution.Aptitude:
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
                            NumericDistributionSetup nds0 = GetNumericDistributionSetup(assets.Where(a => a.ScaleAll != 0).ToList(), a => a.ScaleAll);
                            nds0.idx = 3;
                            return nds0;
                        case RandomizerDistribution.ScaleHead:
                            NumericDistributionSetup nds1 = GetNumericDistributionSetup(assets.Where(a => a.ScaleHead != 0).ToList(), a => a.ScaleHead);
                            nds1.idx = 3;
                            return nds1;
                        case RandomizerDistribution.ScaleNeck:
                            NumericDistributionSetup nds2 = GetNumericDistributionSetup(assets.Where(a => a.ScaleNeck != 0).ToList(), a => a.ScaleNeck);
                            nds2.idx = 3;
                            return nds2;
                        case RandomizerDistribution.ScaleTorso:
                            NumericDistributionSetup nds3 = GetNumericDistributionSetup(assets.Where(a => a.ScaleTorso != 0).ToList(), a => a.ScaleTorso);
                            nds3.idx = 3;
                            return nds3;
                        case RandomizerDistribution.ScaleShoulders:
                            NumericDistributionSetup nds4 = GetNumericDistributionSetup(assets.Where(a => a.ScaleShoulders != 0).ToList(), a => a.ScaleShoulders);
                            nds4.idx = 3;
                            return nds4;
                        case RandomizerDistribution.ScaleArms:
                            NumericDistributionSetup nds5 = GetNumericDistributionSetup(assets.Where(a => a.ScaleArms != 0).ToList(), a => a.ScaleArms);
                            nds5.idx = 3;
                            return nds5;
                        case RandomizerDistribution.ScaleHands:
                            NumericDistributionSetup nds6 = GetNumericDistributionSetup(assets.Where(a => a.ScaleHands != 0).ToList(), a => a.ScaleHands);
                            nds6.idx = 3;
                            return nds6;
                        case RandomizerDistribution.ScaleLegs:
                            NumericDistributionSetup nds7 = GetNumericDistributionSetup(assets.Where(a => a.ScaleLegs != 0).ToList(), a => a.ScaleLegs);
                            nds7.idx = 3;
                            return nds7;
                        case RandomizerDistribution.ScaleFeet:
                            NumericDistributionSetup nds8 = GetNumericDistributionSetup(assets.Where(a => a.ScaleFeet != 0).ToList(), a => a.ScaleFeet);
                            nds8.idx = 3;
                            return nds8;
                        case RandomizerDistribution.VolumeArms:
                            NumericDistributionSetup nds9 = GetNumericDistributionSetup(assets.Where(a => a.VolumeArms != 0).ToList(), a => a.VolumeArms);
                            nds9.idx = 3;
                            return nds9;
                        case RandomizerDistribution.VolumeLegs:
                            NumericDistributionSetup nds10 = GetNumericDistributionSetup(assets.Where(a => a.VolumeLegs != 0).ToList(), a => a.VolumeLegs);
                            nds10.idx = 3;
                            return nds10;
                        case RandomizerDistribution.VolumeBust:
                            NumericDistributionSetup nds11 = GetNumericDistributionSetup(assets.Where(a => a.VolumeBust != 0).ToList(), a => a.VolumeBust);
                            nds11.idx = 3;
                            return nds11;
                        case RandomizerDistribution.VolumeAbdomen:
                            NumericDistributionSetup nds12 = GetNumericDistributionSetup(assets.Where(a => a.VolumeAbdomen != 0).ToList(), a => a.VolumeAbdomen);
                            nds12.idx = 3;
                            return nds12;
                        case RandomizerDistribution.VolumeTorso:
                            NumericDistributionSetup nds13 = GetNumericDistributionSetup(assets.Where(a => a.VolumeTorso != 0).ToList(), a => a.VolumeTorso);
                            nds13.idx = 3;
                            return nds13;
                        case RandomizerDistribution.VolumeScaleArms:
                            NumericDistributionSetup nds14 = GetNumericDistributionSetup(assets.Where(a => a.VolumeScaleArms != 0).ToList(), a => a.VolumeScaleArms);
                            nds14.idx = 3;
                            return nds14;
                        case RandomizerDistribution.VolumeScaleLegs:
                            NumericDistributionSetup nds15 = GetNumericDistributionSetup(assets.Where(a => a.VolumeScaleLegs != 0).ToList(), a => a.VolumeScaleLegs);
                            nds15.idx = 3;
                            return nds15;
                        case RandomizerDistribution.MapScaleAll:
                            NumericDistributionSetup nds16 = GetNumericDistributionSetup(assets.Where(a => a.MapScaleAll != 0).ToList(), a => a.MapScaleAll);
                            nds16.idx = 3;
                            return nds16;
                        case RandomizerDistribution.MapScaleHead:
                            NumericDistributionSetup nds17 = GetNumericDistributionSetup(assets.Where(a => a.MapScaleHead != 0).ToList(), a => a.MapScaleHead);
                            nds17.idx = 3;
                            return nds17;
                        case RandomizerDistribution.MapScaleWing:
                            NumericDistributionSetup nds18 = GetNumericDistributionSetup(assets.Where(a => a.MapScaleWing != 0).ToList(), a => a.MapScaleWing);
                            nds18.idx = 3;
                            return nds18;
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
                    List<string> synchStatSkillIDs = GD.SynchStatSkills.GetIDs();
                    List<string> generalSkillIDs = GD.GeneralSkills.GetIDs();
                    switch (dfe)
                    {
                        case RandomizerDistribution.InheritanceSkills:
                            return GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.InheritanceSkills).ToList(), s => s, GD.GeneralSkills);
                        case RandomizerDistribution.SynchroStatSkillsAlly:
                            return GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => synchStatSkillIDs.Contains(s))).ToList(), s => s, GD.SynchStatSkills);
                        case RandomizerDistribution.SynchroStatSkillsEnemy:
                            SelectionDistributionSetup sds0 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => synchStatSkillIDs.Contains(s))).ToList(), s => s, GD.SynchStatSkills);
                            sds0.idx = 1;
                            return sds0;
                        case RandomizerDistribution.SynchroGeneralSkillsAlly:
                            return GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => generalSkillIDs.Contains(s))).ToList(), s => s, GD.GeneralSkills);
                        case RandomizerDistribution.SynchroGeneralSkillsEnemy:
                            SelectionDistributionSetup sds1 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.SynchroSkills.Where(s => generalSkillIDs.Contains(s))).ToList(), s => s, GD.GeneralSkills);
                            sds1.idx = 1;
                            return sds1;
                        case RandomizerDistribution.EngageSkills:
                            SelectionDistributionSetup sds2 = GetSelectionDistributionSetup(
                                pgs.FilterData(pg => pg.Name, GD.BondLevelTables).SelectMany(pg => pg.Group.Cast<GrowthTable>()).SelectMany(gt =>
                                gt.EngageSkills.Where(s => generalSkillIDs.Contains(s))).ToList(), s => s, GD.GeneralSkills);
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
                                }).ToList(), s => s, GD.EngageWeapons);
                            sds3.idx = 1;
                            return sds3;
                        case RandomizerDistribution.Aptitude:
                            return GetSelectionDistributionSetup(pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables).SelectMany(pg =>
                            pg.Group.Cast<GrowthTable>()).SelectMany(gt => gt.GetAptitudes()).ToList(), i => i, GD.Proficiencies);
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
        private static NumericDistributionSetup GetNumericDistributionSetup<T>(IList<T> objects, Func<T, double> f)
        {
            List<double> observations = new();
            for (int item = 0; item < objects.Count; item++)
                observations.Add(f(objects[item]));
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
        private static SelectionDistributionSetup GetSelectionDistributionSetup<T>(IList<T> objects,
            Func<T, string> getID, IEnumerable<(string id, string name)> entities)
        {
            int[] instances = new int[entities.Count()];
            List<string> ids = entities.Select(t => t.id).ToList();
            for (int i = 0; i < objects.Count; i++)
            {
                string id = getID(objects[i]);
                if (id != "")
                    instances[ids.IndexOf(id)]++;
            }
            return GetSelectionDistributionSetup(instances, entities.Select(t => t.name));
        }

        /// <summary>
        ///  Generates selection distribution objects.
        /// </summary>
        private static SelectionDistributionSetup GetSelectionDistributionSetup<T>(IList<T> objects,
            Func<T, int> getID, IEnumerable<(int id, string name)> entities)
        {
            int[] instances = new int[entities.Count()];
            List<int> ids = entities.Select(t => t.id).ToList();
            for (int i = 0; i < objects.Count; i++)
            {
                int id = getID(objects[i]);
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
