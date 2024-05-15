using ALittleSecretIngredient.Structs;

namespace ALittleSecretIngredient
{
    internal static class GameDataLookup
    {
        internal static List<T> FilterData<T>(this IEnumerable<T> data, Func<T, string> getID, List<(string id, string name)> entities)
        {
            HashSet<string> entityIDs = entities.Select(t => t.id).ToHashSet();
            return data.Where(o => entityIDs.Contains(getID(o))).ToList();
        }
        internal static List<T> GetIDs<T>(this List<(T id, string name)> entities) => entities.Select(t => t.id).ToList();
        internal static string IDToName<T>(this List<(T id, string name)> entities, T id) => entities.First(t => t.id!.Equals(id)).name;
        internal static sbyte GetInternalLevel(this Individual i, List<TypeOfSoldier> toss) =>
            i.InternalLevel != 0 ? i.InternalLevel : i.GetTOS(toss).InternalLevel;
        internal static TypeOfSoldier GetTOS(this Individual i, IEnumerable<TypeOfSoldier> toss) => toss.First(tos => tos.Jid == i.Jid);
        internal static Individual? GetIndividual(this Arrangement a, IEnumerable<Individual> ins) => ins.FirstOrDefault(i => i.Pid == a.Pid);
        internal static TypeOfSoldier? GetTOS(this Arrangement a, Dictionary<string, Individual> ins,
            Dictionary<string, TypeOfSoldier> toss)
        {
            string jid = a.Jid;
            ins.TryGetValue(a.Pid, out Individual? i);
            if (jid == "" && i != null)
                jid = i.Jid;
            if (jid == "")
                return null;
            return toss[jid];
        }
        internal static Gender GetGender(this Individual i)
        {
            if (i.Name == "MPID_Lueur" || i.Name == "MPID_PastLueur")
                return Gender.Both;
            if (i.GetFlag(5))
                return Gender.Rosado;
            if (i.Gender == 2)
                return Gender.Female;
            return Gender.Male;
        }

        internal static ParamGroup GetDeploymentGroup(this DataSet ds) => ds.Params.Cast<ParamGroup>().First(pg =>
                        PlayerArrangementGroups.Contains(((Arrangement)pg.GroupMarker).Group));
        internal static IEnumerable<Arrangement> GetGenericDeployments(this DataSet ds) =>
            ds.GetDeployments().Where(a => a.GetFlag(7) && !a.GetFlag(8));

        internal static IEnumerable<Arrangement> GetDeployments(this DataSet ds) => ds.GetDeploymentGroup().Group.Cast<Arrangement>();

        internal static int GetDeploymentCount(this DataSet ds) => ds.GetDeployments().Count(a => a.GetFlag(7));

        internal static IEnumerable<Arrangement> GetForcedDeployments(this DataSet ds) => ds.GetDeployments().Where(a => a.GetFlag(8) && !a.GetFlag(10));

        internal static IEnumerable<Arrangement> GetEnemies(this (string id, DataSet ds) t) => t.ds.Params.Cast<ParamGroup>()
            .SelectMany(pg => pg.Group.Cast<Arrangement>()).Where(a => a.Force == 1 && a.Pid != "PID_リンデン" || EnemyNPCMaps.Contains(t.id) && a.Force == 2);

        internal static IEnumerable<Arrangement> GetEnemies(this IEnumerable<(string id, DataSet ds)> mapArrangements) =>
            mapArrangements.SelectMany(t => t.GetEnemies());

        internal static IEnumerable<Arrangement> GetGenericEnemies(this (string id, DataSet ds) t) => t.GetEnemies().Where(a => !a.GetFlag(4));

        internal static IEnumerable<Arrangement> GetGenericEnemies(this IEnumerable<(string id, DataSet ds)> mapArrangements) =>
            mapArrangements.SelectMany(t => t.GetGenericEnemies());

        internal static IEnumerable<Arrangement> GetBosses(this (string id, DataSet ds) t) => t.GetEnemies().Where(a => a.GetFlag(4));

        internal static IEnumerable<Arrangement> GetBosses(this IEnumerable<(string id, DataSet ds)> mapArrangements) =>
            mapArrangements.SelectMany(t => t.GetBosses());

        // Assuming maddening and high level where relevant
        internal static int GetEnemyCount(this (string id, DataSet ds) t) => t.GetEnemies()
            .Count(a => a.GetFlag(2) && (a is not GArrangement ga || !ga.LevelMax.Between<byte>(0, 255)));

        internal static IEnumerable<Arrangement> GetNewRecruits(this IEnumerable<(string id, DataSet ds)> mapArrangements,
            HashSet<string> playableCharacterIDs) => mapArrangements.SelectMany(t => t.GetNewRecruits(playableCharacterIDs));

        internal static IEnumerable<Arrangement> GetNewRecruits(this (string id, DataSet ds) t, HashSet<string> playableCharacterIDs) =>
            t.ds.Params.Cast<ParamGroup>().SelectMany(pg => pg.Group.Cast<Arrangement>().Where(a => playableCharacterIDs.Contains(a.Pid) &&
                (a.Force == 2 || a.GetFlag(3) || a.Force == 0 && t.id == "M001")));

        internal static IEnumerable<Arrangement> GetAllies(this (string id, DataSet ds) t, HashSet<string> playableCharacterIDs) =>
            t.GetNewRecruits(playableCharacterIDs).Concat(t.ds.GetDeployments()).Distinct();

        internal static IEnumerable<Arrangement> GetNPCs(this IEnumerable<(string id, DataSet ds)> mapArrangements,
            HashSet<string> playableCharacterIDs) => mapArrangements.SelectMany(t => t.GetNPCs(playableCharacterIDs));

        internal static IEnumerable<Arrangement> GetNPCs(this (string id, DataSet ds) t, HashSet<string> playableCharacterIDs) =>
            t.ds.Params.Cast<ParamGroup>().SelectMany(pg => pg.Group.Cast<Arrangement>()).Where(a => !playableCharacterIDs.Contains(a.Pid) &&
            a.Force == 2 && !EnemyNPCMaps.Contains(t.id) || a.GetFlag(10));

        internal static bool IsOnTargetTerrain(this Individual i, List<(string id, DataSet ds)> arrangements, List<(string id, DataSet ds)> mapTerrains,
            HashSet<string> targetTerrain)
        {
            foreach ((string disposID, DataSet ds) in arrangements)
                foreach (ParamGroup pg in ds.Params.Cast<ParamGroup>())
                    foreach (Arrangement a in pg.Group.Cast<Arrangement>())
                        if (a.Pid == i.Pid)
                            return a.IsOnTargetTerrain(i.BmapSize, (MapTerrain)mapTerrains.First(t => t.id == disposID).ds.Single, targetTerrain);
            return false;
        }

        internal static bool IsOnTargetTerrain(this Arrangement a, byte size, MapTerrain mt, HashSet<string> targetTerrain)
        {
            for (sbyte x = a.DisposX; x < a.DisposX + size; x++)
                for (sbyte y = a.DisposY; y < a.DisposY + size; y++)
                    if ((a.DisposX, a.DisposY) != (0, 0) && mt
                        .GetTerrain(x, y).Any(targetTerrain.Contains))
                        return true;
            return false;
        }

        internal static bool IsLegalTerrain(this MapTerrain mt, byte size, sbyte x, sbyte y, HashSet<string> legalTerrain)
        {
            for (sbyte x0 = x; x0 < x + size; x0++)
                for (sbyte y0 = y; y0 < y + size; y0++)
                    if (!mt.GetTerrain(x0, y0).All(legalTerrain.Contains))
                        return false;
            return true;
        }

        internal static bool IsValidPosition(this MapTerrain mt, Individual? i, sbyte x, sbyte y, HashSet<string> legalTerrain,
            bool[,] occupation)
        {
            byte size = i is null ? (byte)1 : i.BmapSize;
            if (!mt.IsLegalTerrain(size, x, y, legalTerrain))
                return false;
            for (sbyte x0 = x; x0 < x + size; x0++)
                for (sbyte y0 = y; y0 < y + size; y0++)
                    if (occupation[x0, y0])
                        return false;
            return true;
        }

        internal static List<(string id, string name)> ToMapTerrains(this List<(string id, string name)> maps) =>
            maps.Select(t => (t.id.TrimEnd('E'), t.name)).ToList();

        internal static bool Between<T>(this IComparable<T> middle, T lower, T higher) =>
            middle.CompareTo(lower) > 0 && middle.CompareTo(higher) < 0;

        internal static string IdToFileName(this string id, FileGroupEnum fge) => fge switch
        {
            FileGroupEnum.Dispos => id.ToLower() + ".xml.bundle",
            FileGroupEnum.Terrains => "mapterrain_" + id.ToLower() + ".bundle",
            _ => throw new NotImplementedException()
        };

        internal static string FileNameToId(this string fileName, FileGroupEnum fge) => fge switch
        {
            FileGroupEnum.Dispos => fileName.Replace(".xml.bundle", "").ToUpper(),
            FileGroupEnum.Terrains => fileName.Replace("mapterrain_", "").Replace(".bundle", "").ToUpper(),
            _ => throw new NotImplementedException()
        };

        internal static ProficiencyLevel ToProficiencyLevel(this string s) => s switch
        {
            "N" => ProficiencyLevel.N,
            "N+" => ProficiencyLevel.Np,
            "D" => ProficiencyLevel.D,
            "D+" => ProficiencyLevel.Dp,
            "C" => ProficiencyLevel.C,
            "C+" => ProficiencyLevel.Cp,
            "B" => ProficiencyLevel.B,
            "B+" => ProficiencyLevel.Bp,
            "A" => ProficiencyLevel.A,
            "A+" => ProficiencyLevel.Ap,
            "S" => ProficiencyLevel.S,
            _ => throw new ArgumentException("Unsupported proficiency level: " + s)
        };

        internal static Dictionary<RandomizerDistribution, DataSetEnum> DistributionToDataSet { get; } = new()
        {
            { RandomizerDistribution.ScaleAll, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleHead, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleNeck, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleTorso, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleShoulders, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleArms, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleHands, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleLegs, DataSetEnum.Asset },
            { RandomizerDistribution.ScaleFeet, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeArms, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeLegs, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeBust, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeAbdomen, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeTorso, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeScaleArms, DataSetEnum.Asset },
            { RandomizerDistribution.VolumeScaleLegs, DataSetEnum.Asset },
            { RandomizerDistribution.MapScaleAll, DataSetEnum.Asset },
            { RandomizerDistribution.MapScaleHead, DataSetEnum.Asset },
            { RandomizerDistribution.MapScaleWing, DataSetEnum.Asset },
            { RandomizerDistribution.Link, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageCount, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageAttackAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageAttackEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageAttackLink, DataSetEnum.GodGeneral },
            { RandomizerDistribution.LinkGid, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngravePower, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveWeight, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveHit, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveCritical, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveAvoid, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveSecure, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceHpAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceStrAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceTechAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceQuickAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceLuckAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceDefAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMagicAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMdefAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhancePhysAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMoveAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceHpEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceStrEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceTechEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceQuickEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceLuckEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceDefEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMagicEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMdefEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhancePhysEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMoveEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.InheritanceSkills, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroStatSkillsAlly, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroStatSkillsEnemy, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroGeneralSkillsAlly, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroGeneralSkillsEnemy, DataSetEnum.GrowthTable },
            { RandomizerDistribution.EngageSkills, DataSetEnum.GrowthTable },
            { RandomizerDistribution.EngageItems, DataSetEnum.GrowthTable },
            { RandomizerDistribution.GrowthTableAptitude, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SkillInheritanceLevel, DataSetEnum.GrowthTable },
            { RandomizerDistribution.StrongBondLevel, DataSetEnum.GrowthTable },
            { RandomizerDistribution.DeepSynergyLevel, DataSetEnum.GrowthTable },
            { RandomizerDistribution.Exp, DataSetEnum.BondLevel },
            { RandomizerDistribution.Cost, DataSetEnum.BondLevel },
            { RandomizerDistribution.StyleName, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.MoveType, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.Weapon, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.WeaponBaseCount, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.WeaponAdvancedCount, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.MaxWeaponLevelBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.MaxWeaponLevelAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseHpBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseStrBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseTechBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseQuickBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseLuckBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseDefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseMagicBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseMdefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BasePhysBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseSightBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseMoveBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseHpAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseStrAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseTechAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseQuickAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseLuckAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseDefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseMagicAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseMdefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BasePhysAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseSightAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseMoveAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseTotalBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseTotalAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitHpBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitStrBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitTechBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitQuickBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitLuckBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitDefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitMagicBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitMdefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitPhysBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitSightBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitMoveBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitHpAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitStrAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitTechAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitQuickAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitLuckAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitDefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitMagicAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitMdefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitPhysAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitSightAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitMoveAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitTotalBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LimitTotalAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowHpBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowStrBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowTechBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowQuickBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowLuckBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowDefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowMagicBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowMdefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowPhysBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowSightBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowMoveBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowHpAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowStrAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowTechAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowQuickAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowLuckAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowDefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowMagicAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowMdefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowPhysAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowSightAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowMoveAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowTotalBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.BaseGrowTotalAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowHpBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowStrBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowTechBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowQuickBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowLuckBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowDefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowMagicBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowMdefBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowPhysBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowSightBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowMoveBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowHpAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowStrAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowTechAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowQuickAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowLuckAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowDefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowMagicAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowMdefAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowPhysAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowSightAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowMoveAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowTotalBase, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.DiffGrowTotalAdvanced, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LearningSkill, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.LunaticSkill, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.Attrs, DataSetEnum.TypeOfSoldier },
            { RandomizerDistribution.JidAlly, DataSetEnum.Individual },
            { RandomizerDistribution.JidEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.Age, DataSetEnum.Individual },
            { RandomizerDistribution.LevelAlly, DataSetEnum.Individual },
            { RandomizerDistribution.LevelEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.InternalLevel, DataSetEnum.Individual },
            { RandomizerDistribution.SupportCategory, DataSetEnum.Individual },
            { RandomizerDistribution.SkillPoint, DataSetEnum.Individual },
            { RandomizerDistribution.IndividualAptitude, DataSetEnum.Individual },
            { RandomizerDistribution.SubAptitude, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNHpAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNStrAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNTechAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNQuickAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNLuckAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNDefAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNMagicAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNMdefAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNPhysAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNSightAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNMoveAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNTotalAlly, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNHpEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNStrEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNTechEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNQuickEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNLuckEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNDefEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNMagicEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNMdefEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNPhysEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNSightEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNMoveEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetNTotalEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHHpEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHStrEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHTechEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHQuickEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHLuckEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHDefEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHMagicEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHMdefEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHPhysEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHSightEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHMoveEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetHTotalEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLHpEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLStrEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLTechEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLQuickEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLLuckEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLDefEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLMagicEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLMdefEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLPhysEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLSightEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLMoveEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.OffsetLTotalEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.LimitHp, DataSetEnum.Individual },
            { RandomizerDistribution.LimitStr, DataSetEnum.Individual },
            { RandomizerDistribution.LimitTech, DataSetEnum.Individual },
            { RandomizerDistribution.LimitQuick, DataSetEnum.Individual },
            { RandomizerDistribution.LimitLuck, DataSetEnum.Individual },
            { RandomizerDistribution.LimitDef, DataSetEnum.Individual },
            { RandomizerDistribution.LimitMagic, DataSetEnum.Individual },
            { RandomizerDistribution.LimitMdef, DataSetEnum.Individual },
            { RandomizerDistribution.LimitPhys, DataSetEnum.Individual },
            { RandomizerDistribution.LimitSight, DataSetEnum.Individual },
            { RandomizerDistribution.LimitMove, DataSetEnum.Individual },
            { RandomizerDistribution.GrowHp, DataSetEnum.Individual },
            { RandomizerDistribution.GrowStr, DataSetEnum.Individual },
            { RandomizerDistribution.GrowTech, DataSetEnum.Individual },
            { RandomizerDistribution.GrowQuick, DataSetEnum.Individual },
            { RandomizerDistribution.GrowLuck, DataSetEnum.Individual },
            { RandomizerDistribution.GrowDef, DataSetEnum.Individual },
            { RandomizerDistribution.GrowMagic, DataSetEnum.Individual },
            { RandomizerDistribution.GrowMdef, DataSetEnum.Individual },
            { RandomizerDistribution.GrowPhys, DataSetEnum.Individual },
            { RandomizerDistribution.GrowSight, DataSetEnum.Individual },
            { RandomizerDistribution.GrowMove, DataSetEnum.Individual },
            { RandomizerDistribution.GrowTotal, DataSetEnum.Individual },
            { RandomizerDistribution.ItemsWeapons, DataSetEnum.Individual },
            { RandomizerDistribution.ItemsItems, DataSetEnum.Individual },
            { RandomizerDistribution.AttrsAlly, DataSetEnum.Individual },
            { RandomizerDistribution.AttrsEnemy, DataSetEnum.Individual },
            { RandomizerDistribution.CommonSids, DataSetEnum.Individual },
            { RandomizerDistribution.DeploymentSlots, DataSetEnum.Arrangement },
            { RandomizerDistribution.EnemyCount, DataSetEnum.Arrangement },
            { RandomizerDistribution.ForcedDeployment, DataSetEnum.Arrangement },
            { RandomizerDistribution.UnitPosition, DataSetEnum.Arrangement },
            { RandomizerDistribution.ItemsWeaponsAlly, DataSetEnum.Arrangement },
            { RandomizerDistribution.ItemsItemsAlly, DataSetEnum.Arrangement },
            { RandomizerDistribution.ItemsWeaponsEnemy, DataSetEnum.Arrangement },
            { RandomizerDistribution.ItemsItemsEnemy, DataSetEnum.Arrangement },
            { RandomizerDistribution.Sid, DataSetEnum.Arrangement },
            { RandomizerDistribution.Gid, DataSetEnum.Arrangement },
            { RandomizerDistribution.HpStockCount, DataSetEnum.Arrangement },
        };

        internal static Dictionary<(FileEnum fe, string sheetName), Type> DataParamTypes { get; } = new();
        internal static Dictionary<(FileGroupEnum fge, string sheetName), Type> GroupDataParamTypes { get; } = new();
        internal static Dictionary<DataSetEnum, (FileEnum fe, string sheetName)> DataSetToSheetName { get; } = new();
        internal static Dictionary<DataSetEnum, (FileGroupEnum fge, string sheetName)> GroupDataSetToSheetName { get; } = new();

        private static void Bind(FileEnum fe, DataSetEnum dse, Type dataParam, string sheetName)
        {
            DataParamTypes.Add((fe, sheetName), dataParam);
            DataSetToSheetName.Add(dse, (fe, sheetName));
        }
        private static void Bind(FileGroupEnum fge, DataSetEnum dse, Type dataParam, string sheetName)
        {
            GroupDataParamTypes.Add((fge, sheetName), dataParam);
            GroupDataSetToSheetName.Add(dse, (fge, sheetName));
        }

        #region Bond Level IDs
        internal static List<(string id, string name)> BondLevelsFromExp { get; } = new()
        {
            ("2", "Level 2"), ("3", "Level 3"), ("4", "Level 4"), ("5", "Level 5"),
            ("6", "Level 6"), ("7", "Level 7"), ("8", "Level 8"), ("9", "Level 9"),
            ("10", "Level 10"), ("11", "Level 11"), ("12", "Level 12"), ("13", "Level 13"),
            ("14", "Level 14"), ("15", "Level 15"), ("16", "Level 16"), ("17", "Level 17"),
            ("18", "Level 18"), ("19", "Level 19"), ("20", "Level 20")
        };
        internal static List<(string id, string name)> BondLevels { get; } = new() // BondLevelsFromExp +
        {
            ("0", "Level 0"), ("1", "Level 1")
        }; // For consistency's sake, but that doesn't stop me from feeling like a dumbass for doing it this way.
        #endregion
        #region Bond Level Table IDs
        internal static List<(string id, string name)> InheritableBondLevelTables { get; } = new()
        {
            ("GGID_マルス", "Marth"), ("GGID_シグルド", "Sigurd"), ("GGID_セリカ", "Celica"), ("GGID_ミカヤ", "Micaiah"),
            ("GGID_ロイ", "Roy"), ("GGID_リーフ", "Leif"), ("GGID_ルキナ", "Lucina"), ("GGID_リン", "Lyn"),
            ("GGID_アイク", "Ike"), ("GGID_ベレト", "Byleth"), ("GGID_カムイ", "Corrin"), ("GGID_エイリーク", "Eirika"),
            ("GGID_エーデルガルト", "Edelgard"), ("GGID_チキ", "Tiki"), ("GGID_ヘクトル", "Hector"), ("GGID_ヴェロニカ", "Veronica"),
            ("GGID_セネリオ", "Soren"), ("GGID_カミラ", "Camilla"), ("GGID_クロム", "Chrom")
        };

        internal static List<(string id, string name)> AllyBondLevelTables { get; } = new() // InheritableBondLevelTables +
        {
            ("GGID_リュール", "Emblem Alear")
        };

        internal static List<(string id, string name)> EnemyBondLevelTables { get; } = new()
        {
            ("GGID_M002_シグルド", "Sigurd (Chapter 2)"), ("GGID_M007_敵ルキナ", "Corrupted Lucina"),
            ("GGID_M008_敵リーフ", "Corrupted Leif (Chapter 8)"),
            ("GGID_M010_敵ベレト", "Corrupted Byleth (Chapter 10)"), ("GGID_M010_敵リン", "Corrupted Lyn"),
            ("GGID_M011_敵マルス", "Corrupted Marth (Chapter 11)"), ("GGID_M011_敵シグルド", "Corrupted Sigurd (Chapter 11)"),
            ("GGID_M011_敵セリカ", "Corrupted Celica (Chapter 11)"), ("GGID_M011_敵ミカヤ", "Corrupted Micaiah (Chapter 11)"),
            ("GGID_M011_敵ロイ", "Corrupted Roy (Chapter 11)"), ("GGID_M011_敵リーフ", "Corrupted Leif (Chapter 11)"),
            ("GGID_M014_敵ベレト", "Corrupted Byleth (Chapter 14)"), ("GGID_M017_敵マルス", "Corrupted Marth (Chapter 17)"),
            ("GGID_M017_敵シグルド", "Corrupted Sigurd (Chapter 17)"), ("GGID_M017_敵セリカ", "Corrupted Celica (Chapter 17)"),
            ("GGID_M017_敵ミカヤ", "Corrupted Micaiah (Chapter 17)"), ("GGID_M017_敵ロイ", "Corrupted Roy (Chapter 17)"),
            ("GGID_M017_敵リーフ", "Corrupter Leif (Chapter 17)"), ("GGID_M019_敵ミカヤ", "Corrupted Micaiah (Chapter 19)"),
            ("GGID_M019_敵ロイ", "Corrupted Roy (Chapter 19)"), ("GGID_M020_敵セリカ", "Corrupted Celica (Chapter 20)"),
            ("GGID_M021_敵マルス", "Corrupted Marth (Chapter 21)"), ("GGID_M024_敵マルス", "Corrupted Marth (Chapter 24)"),
            ("GGID_E001_敵チキ", "Corrupted Tiki (Xenologue 1)"), ("GGID_E002_敵ヘクトル", "Corrupted Hector (Xenologue 2)"),
            ("GGID_E003_敵ヴェロニカ", "Corrupted Veronica (Xenologue 3)"), ("GGID_E004_敵セネリオ", "Corrupted Soren (Xenologue 4)"),
            ("GGID_E004_敵カミラ", "Corrupted Camilla (Xenologue 4)"), ("GGID_E005_敵クロム", "Corrupted Chrom (Xenologue 5)"),
            ("GGID_E005_敵ヘクトル", "Corrupted Hector (Xenologue 5)"), ("GGID_E005_敵ヴェロニカ", "Corrupted Veronica (Xenologue 5)"),
            ("GGID_E006_敵チキ", "Corrupted Tiki (Xenologue 6)"), ("GGID_E006_敵ヘクトル", "Corrupted Hector (Xenologue 6)"),
            ("GGID_E006_敵ヴェロニカ", "Corrupted Veronica (Xenologue 6)"), ("GGID_E006_敵セネリオ", "Corrupted Soren (Xenologue 6)"),
            ("GGID_E006_敵カミラ", "Corrupted Camilla (Xenologue 6)"), ("GGID_E006_敵クロム", "Corrupted Chrom (Xenologue 6)"),
            ("GGID_E006_敵エーデルガルト", "Corrupted Edelgard")
        };

        internal static List<(string id, string name)> BondLevelTables { get; } = new(); // AllyBondLevelTables + EnemyBondLevelTables
        #endregion
        #region Character IDs
        internal static List<(string id, string name)> ProtagonistCharacters { get; } = new()
        {
            ("PID_リュール", "Alear")
        };

        internal static List<(string id, string name)> PlayableCharacters { get; } = new() // ProtagonistCharacters +
        {
            ("PID_ヴァンドレ", "Vander"),
            ("PID_クラン", "Clanne"), ("PID_フラン", "Framme"),
            ("PID_アルフレッド", "Alfred"), ("PID_エーティエ", "Etie"), ("PID_ブシュロン", "Boucheron"),
            ("PID_セリーヌ", "Céline"), ("PID_クロエ", "Chloé"), ("PID_ルイ", "Louis"),
            
            ("PID_ユナカ", "Yunaka"),
            ("PID_スタルーク", "Alcryst"), ("PID_シトリニカ", "Citrinne"), ("PID_ラピス", "Lapis"),
            ("PID_ディアマンド", "Diamant"), ("PID_アンバー", "Amber"),
            ("PID_ジェーデ", "Jade"),
            
            ("PID_アイビー", "Ivy"), ("PID_カゲツ", "Kagetsu"), ("PID_ゼルコバ", "Zelkov"),
            ("PID_フォガート", "Fogado"), ("PID_パンドロ", "Pandreo"), ("PID_ボネ", "Bunet"),
            ("PID_ミスティラ", "Timerra"), ("PID_パネトネ", "Panette"), ("PID_メリン", "Merrin"),
            ("PID_オルテンシア", "Hortensia"),
            ("PID_セアダス", "Seadall"),
            ("PID_ロサード", "Rosado"), ("PID_ゴルドマリー", "Goldmary"),
            
            ("PID_リンデン", "Lindon"),
            ("PID_ザフィーア", "Saphir"),

            ("PID_モーヴ", "Mauvier"),
            ("PID_ヴェイル", "Veyle"),

            ("PID_ジャン", "Jean"),
            ("PID_アンナ", "Anna"),

            ("PID_エル", "Nel"), ("PID_ラファール", "Rafal"), ("PID_セレスティア", "Zelestia"), ("PID_グレゴリー", "Gregory"), ("PID_マデリーン", "Madeline")
        };

        internal static List<(string id, string name)> FixedLevelAllyNPCCharacters { get; } = new()
        {
            ("PID_S001_ジャン_父親", "Sean"),
            ("PID_S001_村人_青年男", "Jean Paralogue Villager 1"), ("PID_S001_村人_青年女", "Jean Paralogue Villager 2"),
            ("PID_M009_ブロディア兵_ソードアーマー", "Chapter 9 Brodian 1"), ("PID_M009_ブロディア兵_ソードナイト", "Chapter 9 Brodian 2"),
            ("PID_M009_ブロディア兵_モンク", "Chapter 9 Brodian 3"), ("PID_M012_村人Ａ", "Chapter 12 Villager 1"),
            ("PID_M012_村人Ｂ", "Chapter 12 Villager 2"), ("PID_M012_村人Ｃ", "Chapter 12 Villager 3"),
            ("PID_E001_エル", "Xenologue 1 Nel"),
            ("PID_E001_エル_竜化", "Xenologue 1 Transformed Nel"), ("PID_E001_イル", "Xenologue 1 Nil"),
            ("PID_E002_エル", "Xenologue 2 Nel"),
            ("PID_E002_エル_竜化", "Xenologue 2 Transformed Nel"), ("PID_E002_イル", "Xenologue 2 Nil"),
            ("PID_E002_セレスティア", "Xenologue 2 Zelestia"), ("PID_E003_エル", "Xenologue 3 Nel"),
            ("PID_E003_エル_竜化", "Xenologue 3 Transformed Nel"), ("PID_E003_イル", "Xenologue 3 Nil"),
            ("PID_E003_セレスティア", "Xenologue 3 Zelestia"), ("PID_E003_グレゴリー", "Xenologue 3 Gregory"),
            ("PID_E004_エル", "Xenologue 4 Nel"),
            ("PID_E004_エル_竜化", "Xenologue 4 Transformed Nel"), ("PID_E004_イル", "Xenologue 4 Nil"),
            ("PID_E004_セレスティア", "Xenologue 4 Zelestia"), ("PID_E004_グレゴリー", "Xenologue 4 Gregory"),
            ("PID_E004_マデリーン", "Xenologue 4 Madeline"), ("PID_E005_エル", "Xenologue 5 Nel"),
            ("PID_E005_エル_竜化", "Xenologue 5 Transformed Nel"), ("PID_E005_Boss", "Xenologue 5 Nil"),
            ("PID_E006_エル", "Xenologue 6 Nel"), ("PID_E006_エル_竜化", "Xenologue 6 Transformed Nel"),
            ("PID_E006_セレスティア", "Xenologue 6 Zelestia"), ("PID_E006_グレゴリー", "Xenologue 6 Gregory"),
            ("PID_E006_マデリーン", "Xenologue 6 Madeline"),
        };

        internal static List<(string id, string name)> AllyNPCCharacters { get; } = new() // FixedLevelAllyNPCCharacters +
        {
            ("PID_召喚_ソードファイター", "Sword Fighter Summon 1"), ("PID_召喚_ソードファイター_重装特効", "Sword Fighter Summon 2"),
            ("PID_召喚_ソードファイター_竜特効", "Sword Fighter Summon 3"), ("PID_召喚_ランスファイター", "Lance Fighter Summon 1"),
            ("PID_召喚_ランスファイター_大槍", "Lance Fighter Summon 2"), ("PID_召喚_ランスファイター_手槍", "Lance Fighter Summon 3"),
            ("PID_召喚_アクスファイター", "Axe Fighter Summon 1"), ("PID_召喚_アクスファイター_大斧", "Axe Fighter Summon 2"),
            ("PID_召喚_アクスファイター_手斧", "Axe Fighter Summon 3"), ("PID_召喚_ソードペガサス", "Sword Flier Summon 1"),
            ("PID_召喚_ソードペガサス_重装特効", "Sword Flier Summon 2"), ("PID_召喚_ソードペガサス_竜特効", "Sword Flier Summon 3"),
            ("PID_召喚_ランスペガサス", "Lance Flier Summon 1"), ("PID_召喚_ランスペガサス_手槍", "Lance Flier Summon 2"),
            ("PID_召喚_アクスペガサス", "Axe Flier Summon 1"), ("PID_召喚_アクスペガサス_手斧", "Axe Flier Summon 2"),
            ("PID_召喚_ソードナイト", "Sword Cavalier Summon 1"), ("PID_召喚_ソードナイト_大剣", "Sword Cavalier Summon 2"),
            ("PID_召喚_ソードナイト_重装特効", "Sword Cavalier Summon 3"), ("PID_召喚_ソードナイト_竜特効", "Sword Cavalier Summon 4"),
            ("PID_召喚_ランスナイト", "Lance Cavalier Summon 1"), ("PID_召喚_ランスナイト_大槍", "Lance Cavalier Summon 2"),
            ("PID_召喚_ランスナイト_手槍", "Lance Cavalier Summon 3"), ("PID_召喚_アクスナイト", "Axe Cavalier Summon 1"),
            ("PID_召喚_アクスナイト_大斧", "Axe Cavalier Summon 2"), ("PID_召喚_アクスナイト_手斧", "Axe Cavalier Summon 3"),
            ("PID_召喚_ソードアーマー_大剣", "Sword Armor Summon 1"), ("PID_召喚_ソードアーマー_重装特効", "Sword Armor Summon 2"),
            ("PID_召喚_ソードアーマー_竜特効", "Sword Armor Summon 3"), ("PID_召喚_ランスアーマー_大槍", "Lance Armor Summon 1"),
            ("PID_召喚_ランスアーマー_手槍", "Lance Armor Summon 2"), ("PID_召喚_アクスアーマー_大斧", "Axe Armor Summon 1"),
            ("PID_召喚_アクスアーマー_手斧", "Axe Armor Summon 2"), ("PID_召喚_アーチャー", "Archer Summon 1"),
            ("PID_召喚_アーチャー_長弓", "Archer Summon 2"), ("PID_召喚_マージ_ファイアー", "Mage Summon 1"),
            ("PID_召喚_マージ_ウィンド", "Mage Summon 2"), ("PID_召喚_マージ_サンダー", "Mage Summon 3"),
            ("PID_召喚_モンク", "Martial Monk Summon 1"), ("PID_召喚_モンク_特殊", "Martial Monk Summon 2"),
            ("PID_召喚_モンク_攻撃", "Martial Monk Summon 3"), ("PID_召喚_シーフ_ランク３", "Thief Summon 1"),
            ("PID_召喚_シーフ_ランク３_近距離", "Thief Summon 2"), ("PID_召喚_ソードマスター", "Swordmaster Summon 1"),
            ("PID_召喚_ソードマスター_重装特効", "Swordmaster Summon 2"), ("PID_召喚_ソードマスター_竜特効", "Swordmaster Summon 3"),
            ("PID_召喚_ブレイブヒーロー", "Hero Summon 1"), ("PID_召喚_ブレイブヒーロー重装特効", "Hero Summon 2"),
            ("PID_召喚_ブレイブヒーロー_竜特効", "Hero Summon 3"), ("PID_召喚_ハルバーディア", "Halberdier Summon 1"),
            ("PID_召喚_ハルバーディア_大槍", "Halberdier Summon 2"), ("PID_召喚_ハルバーディア_手槍", "Halberdier Summon 3"),
            ("PID_召喚_ロイヤルナイト", "Royal Knight Summon 1"), ("PID_召喚_ロイヤルナイト_手槍", "Royal Knight Summon 2"),
            ("PID_召喚_ロイヤルナイト_回復", "Royal Knight Summon 3"), ("PID_召喚_ベルセルク", "Berserker Summon 1"),
            ("PID_召喚_ベルセルク_大斧", "Berserker Summon 2"), ("PID_召喚_ベルセルク_手斧", "Berserker Summon 3"),
            ("PID_召喚_ウォーリアー", "Warrior Summon 1"), ("PID_召喚_ウォーリアー_大斧", "Warrior Summon 2"),
            ("PID_召喚_ウォーリアー_手斧", "Warrior Summon 3"), ("PID_召喚_スナイパー", "Sniper Summon 1"),
            ("PID_召喚_スナイパー_長弓", "Sniper Summon 2"), ("PID_召喚_ボウナイト", "Bow Knight Summon 1"),
            ("PID_召喚_ボウナイト_長弓", "Bow Knight Summon 2"), ("PID_召喚_ジェネラル_剣_大剣", "General Summon 1"),
            ("PID_召喚_ジェネラル_剣_重装特効", "General Summon 2"), ("PID_召喚_ジェネラル_剣_竜特効", "General Summon 3"),
            ("PID_召喚_ジェネラル_槍_大槍", "General Summon 4"), ("PID_召喚_ジェネラル_槍_手槍", "General Summon 5"),
            ("PID_召喚_ジェネラル_斧_大斧", "General Summon 6"), ("PID_召喚_ジェネラル_斧_手斧", "General Summon 7"),
            ("PID_召喚_グレートナイト_剣", "Great Knight Summon 1"), ("PID_召喚_グレートナイト_剣_大剣", "Great Knight Summon 2"),
            ("PID_召喚_グレートナイト_剣_重装特効", "Great Knight Summon 3"), ("PID_召喚_グレートナイト_剣_竜特効", "Great Knight Summon 4"),
            ("PID_召喚_グレートナイト_槍", "Great Knight Summon 5"), ("PID_召喚_グレートナイト_槍_大槍", "Great Knight Summon 6"),
            ("PID_召喚_グレートナイト_槍_手槍", "Great Knight Summon 7"), ("PID_召喚_グレートナイト_斧", "Great Knight Summon 8"),
            ("PID_召喚_グレートナイト_斧_大斧", "Great Knight Summon 9"), ("PID_召喚_グレートナイト_斧_手斧", "Great Knight Summon 10"),
            ("PID_召喚_パラディン_剣", "Paladin Summon 1"), ("PID_召喚_パラディン_剣_大剣", "Paladin Summon 2"),
            ("PID_召喚_パラディン_剣_重装特効", "Paladin Summon 3"), ("PID_召喚_パラディン_剣_竜特効", "Paladin Summon 4"),
            ("PID_召喚_パラディン_槍", "Paladin Summon 5"), ("PID_召喚_パラディン_槍_大槍", "Paladin Summon 6"),
            ("PID_召喚_パラディン_槍_手槍", "Paladin Summon 7"), ("PID_召喚_パラディン_斧", "Paladin Summon 8"),
            ("PID_召喚_パラディン_斧_大斧", "Paladin Summon 9"), ("PID_召喚_パラディン_斧_手斧", "Paladin Summon 10"),
            ("PID_召喚_ウルフナイト", "Wolf Knight Summon 1"), ("PID_召喚_ウルフナイト_近距離", "Wolf Knight Summon 2"),
            ("PID_召喚_セイジ_ファイアー", "Sage Summon 1"), ("PID_召喚_セイジ_ウィンド", "Sage Summon 2"),
            ("PID_召喚_セイジ_サンダー", "Sage Summon 3"), ("PID_召喚_マージナイト_ファイアー", "Mage Knight Summon 1"),
            ("PID_召喚_マージナイト_ウィンド", "Mage Knight Summon 2"), ("PID_召喚_マージナイト_サンダー", "Mage Knight Summon 3"),
            ("PID_召喚_マスターモンク", "Martial Master Summon"), ("PID_召喚_ハイプリースト", "High Priest Summon 1"),
            ("PID_召喚_ハイプリースト_特殊", "High Priest Summon 2"), ("PID_召喚_グリフォンナイト_剣", "Griffin Knight Summon 1"),
            ("PID_召喚_グリフォンナイト_剣_重装特効", "Griffin Knight Summon 2"), ("PID_召喚_グリフォンナイト_剣_竜特効", "Griffin Knight Summon 3"),
            ("PID_召喚_グリフォンナイト_槍", "Griffin Knight Summon 4"), ("PID_召喚_グリフォンナイト_槍_手槍", "Griffin Knight Summon 5"),
            ("PID_召喚_グリフォンナイト_斧", "Griffin Knight Summon 6"), ("PID_召喚_グリフォンナイト_斧_手斧", "Griffin Knight Summon 7"),
            ("PID_召喚_ドラゴンナイト_剣", "Wyvern Knight Summon 1"), ("PID_召喚_ドラゴンナイト_剣_大剣", "Wyvern Knight Summon 2"),
            ("PID_召喚_ドラゴンナイト_剣_重装特効", "Wyvern Knight Summon 3"), ("PID_召喚_ドラゴンナイト_剣_竜特効", "Wyvern Knight Summon 4"),
            ("PID_召喚_ドラゴンナイト_槍", "Wyvern Knight Summon 5"), ("PID_召喚_ドラゴンナイト_槍_大槍", "Wyvern Knight Summon 6"),
            ("PID_召喚_ドラゴンナイト_槍_手槍", "Wyvern Knight Summon 7"), ("PID_召喚_ドラゴンナイト_斧", "Wyvern Knight Summon 8"),
            ("PID_召喚_ドラゴンナイト_斧_大斧", "Wyvern Knight Summon 9"), ("PID_召喚_ドラゴンナイト_斧_手斧", "Wyvern Knight Summon 10"),
            ("PID_召喚_シーフ_ランク４", "Thief Summon 3"), ("PID_召喚_シーフ_ランク４_近距離", "Thief Summon 4"),
            ("PID_召喚_マルス", "Marth Summon"), ("PID_召喚_シグルド_赤", "Sigurd Red Summon"),
            ("PID_召喚_ロイ", "Roy Summon"), ("PID_召喚_リーフ_赤", "Leif Red Summon"),
            ("PID_召喚_ルキナ_赤", "Lucina Red Summon"), ("PID_召喚_リン_赤", "Lyn Red Summon"),
            ("PID_召喚_アイク_赤", "Ike Red Summon"), ("PID_召喚_ベレト_赤", "Byleth Red Summon"),
            ("PID_召喚_カムイ", "Corrin Summon"), ("PID_召喚_エイリーク", "Eirika Summon"),
            ("PID_召喚_ヘクトル_赤", "Hector Red Summon"), ("PID_召喚_クロム", "Chrom Summon"),
            ("PID_召喚_シグルド_青", "Sigurd Blue Summon"), ("PID_召喚_リーフ_青", "Leif Blue Summon"),
            ("PID_召喚_ルキナ_青", "Lucina Blue Summon"), ("PID_召喚_ディミトリ", "Dimitri Summon"),
            ("PID_召喚_ヘクトル_青", "Hector Blue Summon"), ("PID_召喚_リーフ_緑", "Leif Green Summon"),
            ("PID_召喚_アイク_緑", "Ike Green Summon"), ("PID_召喚_エーデルガルト", "Edelgard Summon"),
            ("PID_召喚_ヘクトル_緑", "Hector Green Summon"), ("PID_召喚_カミラ_緑", "Camilla Green Summon"),
            ("PID_召喚_セリカ", "Celica Summon"), ("PID_召喚_ミカヤ", "Micaiah Summon"),
            ("PID_召喚_ルキナ_白", "Lucina White Summon"), ("PID_召喚_リン_白", "Lyn White Summon"),
            ("PID_召喚_ベレト_白", "Byleth White Summon"), ("PID_召喚_クロード", "Claude Summon"),
            ("PID_召喚_チキ", "Tiki Summon"), ("PID_召喚_ヴェロニカ", "Veronica Summon"),
            ("PID_召喚_セネリオ", "Soren Summon"), ("PID_召喚_カミラ_白", "Camilla White Summon"),
            ("PID_召喚_ルフレ", "Robin Summon"),
        };

        internal static List<(string id, string name)> AllyCharacters { get; } = new(); // PlayableCharacters + AllyNPCCharacters

        internal static List<(string id, string name)> FixedLevelEmblemCharacters { get; } = new()
        {
            ("PID_S003_ルキナ", "Lucina Paralogue Lucina"), ("PID_S004_リン", "Lyn Paralogue Lyn"),
            ("PID_S005_アイク", "Ike Paralogue Ike"), ("PID_S006_ベレト", "Byleth Paralogue Byleth"),
            ("PID_S007_カムイ", "Corrin Paralogue Corrin"), ("PID_S008_エイリーク", "Eirika Paralogue Eirika"),
            ("PID_S009_シグルド", "Sigurd Paralogue Sigurd"), ("PID_S010_リーフ", "Leif Paralogue Leif"),
            ("PID_S011_ミカヤ", "Micaiah Paralogue Micaiah"), ("PID_S012_ロイ", "Roy Paralogue Roy"),
            ("PID_S013_セリカ", "Celica Paralogue Celica"), ("PID_S014_マルス", "Marth Paralogue Marth"),
        };

        internal static List<(string id, string name)> FixedLevelEnemyCharacters { get; } = new() // FixedLevelEmblemCharacters +
        {
            ("PID_M001_異形兵_蛮族_ボス", "Chapter 1 Boss"), ("PID_M001_異形兵_蛮族_雑魚A", "Chapter 1 Corrupted 1"),
            ("PID_M001_異形兵_蛮族_雑魚B", "Chapter 1 Corrupted 2"), ("PID_M001_異形兵_蛮族_雑魚C", "Chapter 1 Corrupted 3"),
            ("PID_M001_異形兵_蛮族_微強", "Chapter 1 Corrupted 4"), ("PID_M002_ルミエル", "Chapter 2 Lumera"),
            ("PID_M002_幻影兵_アクスファイター_イベント", "Chapter 2 Fabrication 1"), ("PID_M002_幻影兵_アーチャー_イベント", "Chapter 2 Fabrication 2"),
            ("PID_M002_幻影兵_アクスファイター", "Chapter 2 Fabrication 3"), ("PID_M002_幻影兵_ランスファイター", "Chapter 2 Fabrication 4"),
            ("PID_M002_幻影兵_ソードナイト", "Chapter 2 Fabrication 5"), ("PID_M002_幻影兵_アーチャー", "Chapter 2 Fabrication 6"),
            ("PID_M003_イルシオン兵_ボス", "Chapter 3 Abyme"), ("PID_M003_イルシオン兵_ランスファイター", "Chapter 3 Soldier 1"),
            ("PID_M003_イルシオン兵_ソードファイター", "Chapter 3 Soldier 2"), ("PID_M003_イルシオン兵_アーチャー", "Chapter 3 Soldier 3"),
            ("PID_M003_イルシオン兵_ランスペガサス", "Chapter 3 Soldier 4"), ("PID_M003_イルシオン兵_ランスペガサス_イベント", "Chapter 3 Soldier 5"),
            ("PID_M004_イルシオン兵_ボス", "Rodine"), ("PID_M004_蛮族", "Chapter 4 Ruffian"),
            ("PID_M004_異形兵_ランスアーマー", "Chapter 4 Corrupted 1"), ("PID_M004_異形兵_アクスアーマー", "Chapter 4 Corrupted 2"),
            ("PID_M004_異形兵_ランスファイター", "Chapter 4 Corrupted 3"), ("PID_M004_異形兵_アーチャー", "Chapter 4 Corrupted 4"),
            ("PID_M004_異形兵_アクスナイト", "Chapter 4 Corrupted 5"), ("PID_M004_異形兵_マージ", "Chapter 4 Corrupted 6"),
            ("PID_M004_異形兵_ソードペガサス_雑魚", "Chapter 4 Corrupted 7"), ("PID_M004_異形兵_ソードペガサス", "Chapter 4 Corrupted 8"),
            ("PID_M004_異形兵_ボス側近", "Chapter 4 Corrupted 9"), ("PID_S001_異形兵_ボス", "Jean Paralogue Boss"),
            ("PID_S001_異形兵_アクスファイター_弱", "Jean Paralogue Corrupted 1"), ("PID_S001_異形兵_アーチャー_弱", "Jean Paralogue Corrupted 2"),
            ("PID_S001_異形兵_ソードナイト_弱", "Jean Paralogue Corrupted 3"), ("PID_S001_異形兵_ランスペガサス", "Jean Paralogue Corrupted 4"),
            ("PID_S001_異形兵_アクスファイター", "Jean Paralogue Corrupted 5"), ("PID_S001_異形兵_アーチャー", "Jean Paralogue Corrupted 6"),
            ("PID_S001_異形兵_ソードナイト", "Jean Paralogue Corrupted 7"), ("PID_S001_異形兵_マージ", "Jean Paralogue Corrupted 8"),
            ("PID_S001_異形兵_蛮族", "Jean Paralogue Corrupted 9"),
            ("PID_M005_Irc_ボス", "Nelucce"), ("PID_M005_Irc_ソードファイター", "Chapter 5 Soldier 1"),
            ("PID_M005_Irc_アクスファイター", "Chapter 5 Soldier 2"), ("PID_M005_Irc_ランスアーマー", "Chapter 5 Soldier 3"),
            ("PID_M005_Irc_アーチャー", "Chapter 5 Soldier 4"), ("PID_M005_Irc_pltn1_アクスファイター", "Chapter 5 Soldier 5"),
            ("PID_M005_Irc_pltn1_モンク", "Chapter 5 Soldier 6"), ("PID_M005_Irc_pltn1_ソードファイター", "Chapter 5 Soldier 7"),
            ("PID_M005_Irc_pltn4_ランスアーマー", "Chapter 5 Soldier 8"), ("PID_M005_Irc_pltn4_アクスファイター", "Chapter 5 Soldier 9"),
            ("PID_M005_Irc_pltn4_アーチャー", "Chapter 5 Soldier 10"), ("PID_M005_Irc_pltn4_マージ", "Chapter 5 Soldier 11"),
            ("PID_M005_Irc_trsr_ソードファイター", "Chapter 5 Soldier 12"), ("PID_M005_Irc_trsr_アクスファイター", "Chapter 5 Soldier 13"),
            ("PID_M005_Irc_trsr_ランスアーマー", "Chapter 5 Soldier 14"), ("PID_M005_Irc_trsr_アーチャー", "Chapter 5 Soldier 15"),
            ("PID_M005_Irc_ランスナイト", "Chapter 5 Soldier 16"), ("PID_M005_Irc_マージ", "Chapter 5 Soldier 17"),
            ("PID_M005_シーフ", "Chapter 5 Thief"), ("PID_M006_ボス", "Teronda"),
            ("PID_M006_蛮族_指輪持ち", "Chapter 6 Thief 1"), ("PID_M006_蛮族M", "Chapter 6 Thief 2"),
            ("PID_M006_蛮族F", "Chapter 6 Thief 3"), ("PID_M006_マージ", "Chapter 6 Thief 4"),
            ("PID_M006_アーチャー", "Chapter 6 Thief 5"), ("PID_M006_ソードファイター", "Chapter 6 Thief 6"),
            ("PID_M006_シーフ", "Chapter 6 Thief 7"), ("PID_S002_蛮族_お頭", "Mitan"),
            ("PID_S002_蛮族_ソードファイター", "Anna Paralogue Thief 1"), ("PID_S002_蛮族_アクスファイター", "Anna Paralogue Thief 2"),
            ("PID_S002_蛮族_アクスアーマー", "Anna Paralogue Thief 3"), ("PID_S002_蛮族_アクスアーマー_中央", "Anna Paralogue Thief 4"),
            ("PID_S002_蛮族_アーチャー", "Anna Paralogue Thief 5"), ("PID_S002_蛮族_マージ", "Anna Paralogue Thief 6"),
            ("PID_S002_蛮族_モンク_中央", "Anna Paralogue Thief 7"), ("PID_S002_蛮族_シーフ", "Anna Paralogue Thief 8"),
            ("PID_S002_蛮族_シーフ_2", "Anna Paralogue Thief 9"), ("PID_S002_蛮族_アクスアーマー_宝物庫西", "Anna Paralogue Thief 10"),
            ("PID_S002_蛮族_アクスファイター_宝物庫西", "Anna Paralogue Thief 11"), ("PID_S002_蛮族_ソードファイター_宝物庫西2", "Anna Paralogue Thief 12"),
            ("PID_S002_蛮族_アクスアーマー_宝物庫東", "Anna Paralogue Thief 13"), ("PID_S002_蛮族_ソードファイター_宝物庫東", "Anna Paralogue Thief 14"),
            ("PID_M007_オルテンシア", "Chapter 7 Hortensia"), ("PID_M007_ロサード", "Chapter 7 Rosado"),
            ("PID_M007_ゴルドマリー", "Chapter 7 Goldmary"), ("PID_M007_イルシオン兵_ランスファイター", "Chapter 7 Soldier 1"),
            ("PID_M007_イルシオン兵_ランスファイター_イベント", "Chapter 7 Soldier 2"), ("PID_M007_イルシオン兵_ソードペガサス_雑魚", "Chapter 7 Soldier 3"),
            ("PID_M007_イルシオン兵_ソードペガサス", "Chapter 7 Soldier 4"), ("PID_M007_イルシオン兵_ソードアーマー", "Chapter 7 Soldier 5"),
            ("PID_M007_イルシオン兵_ランスアーマー", "Chapter 7 Soldier 6"), ("PID_M007_イルシオン兵_アクスナイト", "Chapter 7 Soldier 7"),
            ("PID_M007_イルシオン兵_マージ", "Chapter 7 Soldier 8"), ("PID_M007_イルシオン兵_モンク", "Chapter 7 Soldier 9"),
            ("PID_M007_イルシオン兵_アーチャー", "Chapter 7 Soldier 10"), ("PID_M008_アイビー", "Chapter 8 Ivy"),
            ("PID_M008_カゲツ", "Chapter 8 Kagetsu"), ("PID_M008_ゼルコバ", "Chapter 8 Zelkov"),
            ("PID_M008_イルシオン兵_ランスファイター", "Chapter 8 Soldier 1"), ("PID_M008_イルシオン兵_ソードファイター", "Chapter 8 Soldier 2"),
            ("PID_M008_イルシオン兵_アクスファイター", "Chapter 8 Soldier 3"), ("PID_M008_イルシオン兵_マージ", "Chapter 8 Soldier 4"),
            ("PID_M008_イルシオン兵_アーチャー", "Chapter 8 Soldier 5"), ("PID_M008_イルシオン兵_シーフ", "Chapter 8 Soldier 6"),
            ("PID_M008_イルシオン兵_モンク", "Chapter 8 Soldier 7"), ("PID_M008_イルシオン兵_アクスアーマー", "Chapter 8 Soldier 8"),
            ("PID_M008_イルシオン兵_ソードペガサス", "Chapter 8 Soldier 9"), ("PID_M008_イルシオン兵_ランスペガサス", "Chapter 8 Soldier 10"),
            ("PID_M008_イルシオン兵_アクスペガサス", "Chapter 8 Soldier 11"), ("PID_M008_イルシオン兵_ソードペガサス_増援上向き", "Chapter 8 Soldier 12"),
            ("PID_M008_イルシオン兵_ランスペガサス_増援上向き", "Chapter 8 Soldier 13"), ("PID_M008_イルシオン兵_アクスペガサス_増援上向き", "Chapter 8 Soldier 14"),
            ("PID_M008_イルシオン兵_ソードペガサス_増援上向き2", "Chapter 8 Soldier 15"), ("PID_M008_イルシオン兵_ランスペガサス_増援上向き2", "Chapter 8 Soldier 16"),
            ("PID_M008_イルシオン兵_アクスペガサス_増援上向き2", "Chapter 8 Soldier 17"), ("PID_M008_異形兵_ソードファイター", "Chapter 8 Corrupted 1"),
            ("PID_M008_異形兵_ランスファイター", "Chapter 8 Corrupted 2"), ("PID_M008_異形兵_アクスファイター", "Chapter 8 Corrupted 3"),
            ("PID_M008_異形兵_ソードアーマー", "Chapter 8 Corrupted 4"), ("PID_M008_異形兵_アクスアーマー", "Chapter 8 Corrupted 5"),
            ("PID_M009_アイビー", "Chapter 9 Ivy"), ("PID_M009_カゲツ", "Chapter 9 Kagetsu"),
            ("PID_M009_ゼルコバ", "Chapter 9 Zelkov"), ("PID_M009_イルシオン兵_ランスアーマー", "Chapter 9 Soldier 1"),
            ("PID_M009_イルシオン兵_アクスアーマー", "Chapter 9 Soldier 2"), ("PID_M009_イルシオン兵_ランスアーマー隊", "Chapter 9 Soldier 3"),
            ("PID_M009_イルシオン兵_アクスアーマー隊", "Chapter 9 Soldier 4"), ("PID_M009_イルシオン兵_ランスナイト", "Chapter 9 Soldier 5"),
            ("PID_M009_イルシオン兵_アクスナイト", "Chapter 9 Soldier 6"), ("PID_M009_異形兵_モンク", "Chapter 9 Corrupted 1"),
            ("PID_M009_異形兵_アーチャー", "Chapter 9 Corrupted 2"), ("PID_M009_イルシオン兵_マージ", "Chapter 9 Soldier 7"),
            ("PID_M009_異形兵_ソードファイター_1", "Chapter 9 Corrupted 3"), ("PID_M009_異形兵_ソードファイター_2", "Chapter 9 Corrupted 4"),
            ("PID_M009_異形兵_ランスファイター", "Chapter 9 Corrupted 5"), ("PID_M009_異形兵_アクスファイター", "Chapter 9 Corrupted 6"),
            ("PID_M009_異形兵_シーフ_1", "Chapter 9 Corrupted 7"), ("PID_M009_異形兵_シーフ_2", "Chapter 9 Corrupted 8"),
            ("PID_M009_イルシオン兵_ソードペガサス", "Chapter 9 Soldier 8"), ("PID_M009_イルシオン兵_ランスペガサス", "Chapter 9 Soldier 9"),
            ("PID_M009_イルシオン兵_アクスペガサス", "Chapter 9 Soldier 10"), ("PID_M009_イルシオン兵_ソードペガサス_ジェーデ標的", "Chapter 9 Soldier 11"),
            ("PID_M009_イルシオン兵_ランスペガサス_ジェーデ標的", "Chapter 9 Soldier 12"), ("PID_M009_イルシオン兵_ランスアーマー_ジェーデ標的", "Chapter 9 Soldier 13"),
            ("PID_M009_イルシオン兵_アクスアーマー_ジェーデ標的", "Chapter 9 Soldier 14"), ("PID_M009_イルシオン兵_モンク", "Chapter 9 Soldier 15"),
            ("PID_M010_ハイアシンス", "Chapter 10 Hyacinth"),
            ("PID_M010_オルテンシア", "Chapter 10 Hortensia"), ("PID_M010_ロサード", "Chapter 10 Rosado"),
            ("PID_M010_ゴルドマリー", "Chapter 10 Goldmary"), ("PID_M010_異形兵_モリオン", "Corrupted Morion"),
            ("PID_M010_異形兵_ソードファイター", "Chapter 10 Corrupted 1"), ("PID_M010_異形兵_モンク", "Chapter 10 Corrupted 2"),
            ("PID_M010_イルシオン兵_アクスアーマー", "Chapter 10 Soldier 1"), ("PID_M010_イルシオン兵_ランスアーマー", "Chapter 10 Soldier 2"),
            ("PID_M010_イルシオン兵_アーチャー", "Chapter 10 Soldier 3"), ("PID_M010_イルシオン兵_マージ", "Chapter 10 Soldier 4"),
            ("PID_M010_イルシオン兵_マージ_魔砲台", "Chapter 10 Soldier 5"), ("PID_M010_イルシオン兵_モンク", "Chapter 10 Soldier 6"),
            ("PID_M010_イルシオン兵_ソードファイター", "Chapter 10 Soldier 7"), ("PID_M010_イルシオン兵_アクスペガサス", "Chapter 10 Soldier 8"),
            ("PID_M010_イルシオン兵_ソードペガサス", "Chapter 10 Soldier 9"), ("PID_M010_イルシオン兵_ランスナイト", "Chapter 10 Soldier 10"),
            ("PID_M010_盗賊_シーフ", "Chapter 10 Thief"), ("PID_M011_ヴェイル", "Chapter 11 Veyle"),
            ("PID_M011_セピア", "Chapter 11 Zephia"), ("PID_M011_グリ", "Chapter 11 Griss"),
            ("PID_M011_モーヴ", "Chapter 11 Mauvier"), ("PID_M011_マロン", "Chapter 11 Marni"),
            ("PID_M011_異形兵_アーチャー_離脱", "Chapter 11 Corrupted 1"), ("PID_M011_異形兵_ランスナイト_離脱", "Chapter 11 Corrupted 2"),
            ("PID_M011_異形兵_ソードファイター", "Chapter 11 Corrupted 3"), ("PID_M011_異形兵_アクスファイター", "Chapter 11 Corrupted 4"),
            ("PID_M011_異形兵_アクスファイター_トマホーク", "Chapter 11 Corrupted 5"), ("PID_M011_異形兵_ランスファイター", "Chapter 11 Corrupted 6"),
            ("PID_M011_異形兵_マージ", "Chapter 11 Corrupted 7"), ("PID_M011_異形兵_アーチャー", "Chapter 11 Corrupted 8"),
            ("PID_M011_異形兵_モンク", "Chapter 11 Corrupted 9"), ("PID_M011_異形兵_シーフ", "Chapter 11 Corrupted 10"),
            ("PID_M011_異形兵_アクスナイト", "Chapter 11 Corrupted 11"), ("PID_M011_異形兵_アクスアーマー", "Chapter 11 Corrupted 12"),
            ("PID_M011_異形兵_ソードペガサス", "Chapter 11 Corrupted 13"), ("PID_M011_異形竜", "Chapter 11 Corrupted Wyrm"),
            ("PID_M012_異形兵_ソードファイター", "Chapter 12 Corrupted 1"), ("PID_M012_異形兵_ソードマスター", "Chapter 12 Corrupted 2"),
            ("PID_M012_異形兵_アクスファイター", "Chapter 12 Corrupted 3"), ("PID_M012_異形兵_アクスファイター弱", "Chapter 12 Corrupted 4"),
            ("PID_M012_異形兵_ウォーリアー", "Chapter 12 Corrupted 5"), ("PID_M012_異形兵_ウルフナイト", "Chapter 12 Corrupted 6"),
            ("PID_M012_異形兵_マージ", "Chapter 12 Corrupted 7"), ("PID_M012_異形兵_モンク", "Chapter 12 Corrupted 8"),
            ("PID_M012_異形兵_ランスペガサス", "Chapter 12 Corrupted 9"),
            ("PID_M013_蛮族_お頭Ａ", "Tetchie"), ("PID_M013_蛮族_お頭Ｂ", "Totchie"),
            ("PID_M013_蛮族", "Chapter 13 Ruffian"), ("PID_M013_異形兵_ドラゴンナイト", "Chapter 13 Corrupted 1"),
            ("PID_M013_異形兵_アクスペガサス", "Chapter 13 Corrupted 2"), ("PID_M013_異形兵_ソードペガサス", "Chapter 13 Corrupted 3"),
            ("PID_M013_異形兵_アクスファイター", "Chapter 13 Corrupted 4"), ("PID_M013_異形兵_ウォーリアー", "Chapter 13 Corrupted 5"),
            ("PID_M013_異形兵_ソードファイター", "Chapter 13 Corrupted 6"), ("PID_M013_異形兵_ソードマスター", "Chapter 13 Corrupted 7"),
            ("PID_M013_異形兵_マージ", "Chapter 13 Corrupted 8"), ("PID_M013_異形兵_ウルフナイト", "Chapter 13 Corrupted 9"),
            ("PID_M013_異形兵_スナイパー", "Chapter 13 Corrupted 10"), ("PID_M013_異形兵_アーチャー", "Chapter 13 Corrupted 11"),
            ("PID_M014_オルテンシア", "Chapter 14 Hortensia"),
            ("PID_M014_セピア", "Chapter 14 Zephia"), ("PID_M014_モーヴ", "Chapter 14 Mauvier"),
            ("PID_M014_マロン", "Chapter 14 Marni"), ("PID_M014_イルシオン兵_ハイプリースト", "Chapter 14 Soldier 1"),
            ("PID_M014_イルシオン兵_モンク", "Chapter 14 Soldier 2"), ("PID_M014_イルシオン兵_ドラゴンナイト", "Chapter 14 Soldier 3"),
            ("PID_M014_イルシオン兵_アクスペガサス", "Chapter 14 Soldier 4"), ("PID_M014_イルシオン兵_ソードペガサス", "Chapter 14 Soldier 5"),
            ("PID_M014_イルシオン兵_アーチャー", "Chapter 14 Soldier 6"), ("PID_M014_イルシオン兵_マージ", "Chapter 14 Soldier 7"),
            ("PID_M014_イルシオン兵_ソードファイター", "Chapter 14 Soldier 8"), ("PID_M014_イルシオン兵_ブレイブヒーロー", "Chapter 14 Soldier 9"),
            ("PID_M014_イルシオン兵_ソードマスター", "Chapter 14 Soldier 10"), ("PID_M014_イルシオン兵_シーフ", "Chapter 14 Soldier 11"),
            ("PID_M014_イルシオン兵_アクスファイター", "Chapter 14 Soldier 12"), ("PID_M014_イルシオン兵_アクスアーマー", "Chapter 14 Soldier 13"),
            ("PID_M014_イルシオン兵_ベルセルク", "Chapter 14 Soldier 14"), ("PID_M014_イルシオン兵_ランスアーマー", "Chapter 14 Soldier 15"),
            ("PID_M014_イルシオン兵_ランスナイト", "Chapter 14 Soldier 16"), ("PID_M014_イルシオン兵_パラディン", "Chapter 14 Soldier 17"),
            ("PID_M014_イルシオン兵_ウルフナイト", "Chapter 14 Soldier 18"), ("PID_M014_イルシオン兵_ランスファイター", "Chapter 14 Soldier 19"),
            ("PID_M014_イルシオン兵_ハルバーディア", "Chapter 14 Soldier 20"),
            ("PID_S003_幻影兵_アクスファイター", "Lucina Paralogue Fabrication 1"), ("PID_S003_幻影兵_ベルセルク", "Lucina Paralogue Fabrication 2"),
            ("PID_S003_幻影兵_ランスアーマー", "Lucina Paralogue Fabrication 3"), ("PID_S003_幻影兵_ジェネラル", "Lucina Paralogue Fabrication 4"),
            ("PID_S003_幻影兵_マージ", "Lucina Paralogue Fabrication 5"), ("PID_S003_幻影兵_セイジ", "Lucina Paralogue Fabrication 6"),
            ("PID_S003_幻影兵_アーチャー", "Lucina Paralogue Fabrication 7"), ("PID_S003_幻影兵_スナイパー", "Lucina Paralogue Fabrication 8"),
            ("PID_S003_幻影兵_ウルフナイト", "Lucina Paralogue Fabrication 9"), ("PID_S003_幻影兵_ソードファイター", "Lucina Paralogue Fabrication 10"),
            ("PID_S003_幻影兵_ソードマスター", "Lucina Paralogue Fabrication 11"),
            ("PID_S004_幻影兵_トリオル", "Lyn Paralogue Toril"), ("PID_S004_幻影兵_クドカ", "Lyn Paralogue Kudoka"),
            ("PID_S004_幻影兵_カブル", "Lyn Paralogue Kabul"), ("PID_S004_幻影兵_ブラクル", "Lyn Paralogue Brakul"),
            ("PID_S004_幻影兵_マラル", "Lyn Paralogue Maral"), ("PID_S004_幻影兵_チャン", "Lyn Paralogue Chan"),
            ("PID_S004_幻影兵_ボウナイト", "Lyn Paralogue Fabrication 1"), ("PID_S004_幻影兵_マージ", "Lyn Paralogue Fabrication 2"),
            ("PID_S004_幻影兵_セイジ", "Lyn Paralogue Fabrication 3"), ("PID_S004_幻影兵_アーチャー", "Lyn Paralogue Fabrication 4"),
            ("PID_S004_幻影兵_スナイパー", "Lyn Paralogue Fabrication 5"), ("PID_S004_幻影兵_ソードファイター", "Lyn Paralogue Fabrication 6"),
            ("PID_S004_幻影兵_ソードマスター", "Lyn Paralogue Fabrication 7"), ("PID_S004_幻影兵_ドラゴンナイト", "Lyn Paralogue Fabrication 8"),
            ("PID_M015_異形兵_ボス", "Chapter 15 Boss"), ("PID_M015_異形兵_ソードファイター", "Chapter 15 Corrupted 1"),
            ("PID_M015_異形兵_ランスファイター", "Chapter 15 Corrupted 2"), ("PID_M015_異形兵_ハルバーディア", "Chapter 15 Corrupted 3"),
            ("PID_M015_異形兵_アクスファイター", "Chapter 15 Corrupted 4"), ("PID_M015_異形兵_ソードマスター", "Chapter 15 Corrupted 5"),
            ("PID_M015_異形兵_マージ", "Chapter 15 Corrupted 6"), ("PID_M015_異形兵_アーチャー", "Chapter 15 Corrupted 7"),
            ("PID_M015_異形兵_スナイパー", "Chapter 15 Corrupted 8"), ("PID_M015_異形兵_ウォーリアー", "Chapter 15 Corrupted 9"),
            ("PID_M015_異形兵_マスターモンク", "Chapter 15 Corrupted 10"), ("PID_M015_異形兵_ウォーリアー_増援", "Chapter 15 Corrupted 11"),
            ("PID_M015_異形兵_アクスファイター_増援フラグ", "Chapter 15 Corrupted 12"), ("PID_M015_異形兵_マージ_増援フラグ", "Chapter 15 Corrupted 13"),
            ("PID_M015_異形兵_セアダス周辺_アクスファイター", "Chapter 15 Corrupted 14"), ("PID_M015_異形兵_セアダス周辺_ランスファイター", "Chapter 15 Corrupted 15"),
            ("PID_M016_モーヴ", "Chapter 16 Mauvier"),
            ("PID_M016_マロン", "Chapter 16 Marni"), ("PID_M016_イルシオン兵_異形竜", "Chapter 16 Corrupted Wyrm"),
            ("PID_M016_蛮族", "Chapter 16 Ruffian"), ("PID_M016_イルシオン兵_ブレイブヒーロー", "Chapter 16 Soldier 1"),
            ("PID_M016_イルシオン兵_ランスファイター", "Chapter 16 Soldier 2"), ("PID_M016_イルシオン兵_ロイヤルナイト", "Chapter 16 Soldier 3"),
            ("PID_M016_イルシオン兵_アクスファイター", "Chapter 16 Soldier 4"), ("PID_M016_イルシオン兵_ボウナイト", "Chapter 16 Soldier 5"),
            ("PID_M016_イルシオン兵_ボウナイト_魔攻", "Chapter 16 Soldier 6"), ("PID_M016_イルシオン兵_グレートナイト", "Chapter 16 Soldier 7"),
            ("PID_M016_イルシオン兵_アクスナイト", "Chapter 16 Soldier 8"), ("PID_M016_イルシオン兵_パラディン", "Chapter 16 Soldier 9"),
            ("PID_M016_イルシオン兵_マージナイト", "Chapter 16 Soldier 10"), ("PID_M016_イルシオン兵_マスターモンク", "Chapter 16 Soldier 11"),
            ("PID_M016_イルシオン兵_ソードペガサス", "Chapter 16 Soldier 12"), ("PID_M016_イルシオン兵_ソードペガサス_魔攻", "Chapter 16 Soldier 13"),
            ("PID_M016_イルシオン兵_グリフォンナイト", "Chapter 16 Soldier 14"), ("PID_M016_イルシオン兵_シーフ", "Chapter 16 Soldier 15"),
            ("PID_S005_幻影兵_ハルバーディア", "Ike Paralogue Fabrication 1"), ("PID_S005_幻影兵_ジェネラル", "Ike Paralogue Fabrication 2"),
            ("PID_S005_幻影兵_ブレイブヒーロー", "Ike Paralogue Fabrication 3"), ("PID_S005_幻影兵_ソードマスター", "Ike Paralogue Fabrication 4"),
            ("PID_S005_幻影兵_ロイヤルナイト", "Ike Paralogue Fabrication 5"), ("PID_S005_幻影兵_パラディン", "Ike Paralogue Fabrication 6"),
            ("PID_S005_幻影兵_グレートナイト", "Ike Paralogue Fabrication 7"), ("PID_S005_幻影兵_ウルフナイト", "Ike Paralogue Fabrication 8"),
            ("PID_S005_幻影兵_ウォーリアー", "Ike Paralogue Fabrication 9"), ("PID_S005_幻影兵_ベルセルク", "Ike Paralogue Fabrication 10"),
            ("PID_S005_幻影兵_ボウナイト", "Ike Paralogue Fabrication 11"), ("PID_S005_幻影兵_スナイパー", "Ike Paralogue Fabrication 12"),
            ("PID_S005_幻影兵_マスターモンク", "Ike Paralogue Fabrication 13"), ("PID_S005_幻影兵_セイジ", "Ike Paralogue Fabrication 14"),
            ("PID_S005_幻影兵_イレース", "Ike Paralogue Ilyana"), ("PID_M017_ヴェイル", "Chapter 17 Veyle"),
            ("PID_M017_セピア", "Chapter 17 Zephia"), ("PID_M017_グリ", "Chapter 17 Griss"),
            ("PID_M017_モーヴ", "Chapter 17 Mauvier"), ("PID_M017_マロン", "Chapter 17 Marni"),
            ("PID_M017_異形兵_ハイアシンス", "Corrupted Hyacinth"), ("PID_M017_イルシオン兵_ハルバーディア", "Chapter 17 Soldier 1"),
            ("PID_M017_イルシオン兵_ジェネラル", "Chapter 17 Soldier 2"), ("PID_M017_イルシオン兵_パラディン", "Chapter 17 Soldier 3"),
            ("PID_M017_イルシオン兵_ロイヤルナイト", "Chapter 17 Soldier 4"), ("PID_M017_イルシオン兵_マージ", "Chapter 17 Soldier 5"),
            ("PID_M017_イルシオン兵_グリフォンナイト", "Chapter 17 Soldier 6"), ("PID_M017_イルシオン兵_ベルセルク", "Chapter 17 Soldier 7"),
            ("PID_M017_イルシオン兵_セイジ", "Chapter 17 Soldier 8"), ("PID_M017_イルシオン兵_ハイプリースト", "Chapter 17 Soldier 9"),
            ("PID_M017_イルシオン兵_ハルバーディア_増援", "Chapter 17 Soldier 10"), ("PID_M017_イルシオン兵_スナイパー", "Chapter 17 Soldier 11"),
            ("PID_M017_イルシオン兵_異形竜", "Chapter 17 Corrupted Wyrm"),
            ("PID_S006_幻影兵_ジェネラル", "Byleth Paralogue Fabrication 1"), ("PID_S006_幻影兵_セイジ", "Byleth Paralogue Fabrication 2"),
            ("PID_S006_幻影兵_ベルセルク", "Byleth Paralogue Fabrication 3"), ("PID_S006_幻影兵_スナイパー", "Byleth Paralogue Fabrication 4"),
            ("PID_S006_幻影兵_ソードマスター", "Byleth Paralogue Fabrication 5"), ("PID_S006_幻影兵_シーフ", "Byleth Paralogue Fabrication 6"),
            ("PID_S006_幻影兵_マスターモンク_ベレト側近", "Byleth Paralogue Fabrication 7"), ("PID_S006_幻影兵_スナイパー_ベレト側近", "Byleth Paralogue Fabrication 8"),
            ("PID_S006_幻影兵_エーデルガルト", "Byleth Paralogue Edelgard"), ("PID_S006_幻影兵_ディミトリ", "Byleth Paralogue Dimitri"),
            ("PID_S006_幻影兵_クロード", "Byleth Paralogue Claude"), ("PID_S006_幻影兵_グリフォンナイト", "Byleth Paralogue Fabrication 9"),
            ("PID_S006_幻影竜", "Byleth Paralogue Fabrication 10"),
            ("PID_S009_幻影兵_ユリウス", "Sigurd Paralogue Julius"), ("PID_S009_幻影兵_イシュタル", "Sigurd Paralogue Ishtar"),
            ("PID_S009_幻影兵_ザガム", "Sigurd Paralogue Zagam"), ("PID_S009_幻影兵_ジェネラル", "Sigurd Paralogue Fabrication 1"),
            ("PID_S009_幻影兵_マージナイト", "Sigurd Paralogue Fabrication 2"), ("PID_S009_幻影兵_グレートナイト", "Sigurd Paralogue Fabrication 3"),
            ("PID_S009_幻影兵_パラディン", "Sigurd Paralogue Fabrication 4"), ("PID_S009_幻影兵_ウルフナイト", "Sigurd Paralogue Fabrication 5"),
            ("PID_S009_幻影兵_ボウナイト", "Sigurd Paralogue Fabrication 6"), ("PID_S009_幻影兵_セイジ", "Sigurd Paralogue Fabrication 7"),
            ("PID_S009_幻影兵_マスターモンク", "Sigurd Paralogue Fabrication 8"), ("PID_S009_幻影兵_ハイプリースト", "Sigurd Paralogue Fabrication 9"),
            ("PID_S009_幻影兵_ブレイブヒーロー", "Sigurd Paralogue Fabrication 10"), ("PID_S009_幻影兵_ウォーリアー", "Sigurd Paralogue Fabrication 11"),
            ("PID_S009_幻影兵_ベルセルク", "Sigurd Paralogue Fabrication 12"), ("PID_S009_幻影兵_スナイパー", "Sigurd Paralogue Fabrication 13"),
            ("PID_M018_イルシオン兵_ボス", "Chapter 18 Abyme"), ("PID_M018_イルシオン兵_ブレイブヒーロー", "Chapter 18 Soldier 1"),
            ("PID_M018_イルシオン兵_ベルセルク", "Chapter 18 Soldier 2"), ("PID_M018_イルシオン兵_スナイパー", "Chapter 18 Soldier 3"),
            ("PID_M018_イルシオン兵_ジェネラル", "Chapter 18 Soldier 4"), ("PID_M018_イルシオン兵_セイジ", "Chapter 18 Soldier 5"),
            ("PID_M018_イルシオン兵_ドラゴンナイト", "Chapter 18 Soldier 6"), ("PID_M018_イルシオン兵_グリフォンナイト", "Chapter 18 Soldier 7"),
            ("PID_M018_シーフ", "Chapter 18 Thief"), ("PID_M018_イルシオン兵_スナイパー2", "Chapter 18 Soldier 8"),
            ("PID_S007_幻影兵_マークス", "Corrin Paralogue Xander"),
            ("PID_S007_幻影兵_カミラ", "Corrin Paralogue Camilla"), ("PID_S007_幻影兵_レオン", "Corrin Paralogue Leo"),
            ("PID_S007_幻影兵_エリーゼ", "Corrin Paralogue Elise"), ("PID_S007_幻影兵_リョウマ", "Corrin Paralogue Ryoma"),
            ("PID_S007_幻影兵_ヒノカ", "Corrin Paralogue Hinoka"), ("PID_S007_幻影兵_タクミ", "Corrin Paralogue Takumi"),
            ("PID_S007_幻影兵_サクラ", "Corrin Paralogue Sakura"), ("PID_S007_幻影兵_ソードマスター", "Corrin Paralogue Fabrication 1"),
            ("PID_S007_幻影兵_ハルバーディア", "Corrin Paralogue Fabrication 2"), ("PID_S007_幻影兵_ベルセルク", "Corrin Paralogue Fabrication 3"),
            ("PID_S007_幻影兵_スナイパー", "Corrin Paralogue Fabrication 4"), ("PID_S007_幻影兵_ドラゴンナイト", "Corrin Paralogue Fabrication 5"),
            ("PID_S007_幻影兵_パラディン", "Corrin Paralogue Fabrication 6"), ("PID_S007_幻影兵_マスターモンク", "Corrin Paralogue Fabrication 7"),
            ("PID_S007_幻影兵_セイジ", "Corrin Paralogue Fabrication 8"), ("PID_M019_モーヴ", "Chapter 19 Mauvier"),
            ("PID_M019_マロン", "Chapter 19 Marni"), ("PID_M019_異形兵_ブレイブヒーロー", "Chapter 19 Corrupted 1"),
            ("PID_M019_異形兵_ロイヤルナイト", "Chapter 19 Corrupted 2"), ("PID_M019_異形兵_グレートナイト", "Chapter 19 Corrupted 3"),
            ("PID_M019_異形兵_ウルフナイト", "Chapter 19 Corrupted 4"), ("PID_M019_異形兵_ウォーリアー", "Chapter 19 Corrupted 5"),
            ("PID_M019_異形兵_ベルセルク", "Chapter 19 Corrupted 6"), ("PID_M019_異形兵_スナイパー", "Chapter 19 Corrupted 7"),
            ("PID_M019_異形竜", "Chapter 19 Corrupted Wyrm"),
            ("PID_S008_ドラゴンゾンビ", "Eirika Paralogue Fabrication 1"), ("PID_S008_幻影兵_ゴーゴン魔法", "Eirika Paralogue Fabrication 2"),
            ("PID_S008_幻影兵_ゴーゴン杖", "Eirika Paralogue Fabrication 3"), ("PID_S008_幻影兵_マグダイル", "Eirika Paralogue Fabrication 4"),
            ("PID_S008_幻影兵_ケルベロス", "Eirika Paralogue Fabrication 5"), ("PID_S008_幻影兵_ヘルボーン剣槍", "Eirika Paralogue Fabrication 6"),
            ("PID_S008_幻影兵_ヘルボーン弓", "Eirika Paralogue Fabrication 7"), ("PID_S008_幻影兵_エルダバール", "Eirika Paralogue Fabrication 8"),
            ("PID_S008_幻影兵_デスガーゴイル", "Eirika Paralogue Fabrication 9"), ("PID_S008_幻影兵_アークビグル", "Eirika Paralogue Fabrication 10"),
            ("PID_S008_幻影兵_シーフ", "Eirika Paralogue Fabrication 11"),
            ("PID_S010_幻影兵_コーエン", "Leif Paralogue Coen"), ("PID_S010_幻影兵_サイアス", "Leif Paralogue Cyas"),
            ("PID_S010_幻影兵_ジェネラル", "Leif Paralogue Fabrication 1"), ("PID_S010_幻影兵_マージナイト", "Leif Paralogue Fabrication 2"),
            ("PID_S010_幻影兵_パラディン", "Leif Paralogue Fabrication 3"), ("PID_S010_幻影兵_ボウナイト", "Leif Paralogue Fabrication 4"),
            ("PID_S010_幻影兵_セイジ", "Leif Paralogue Fabrication 5"), ("PID_S010_幻影兵_ハイプリースト", "Leif Paralogue Fabrication 6"),
            ("PID_S010_幻影兵_スナイパー", "Leif Paralogue Fabrication 7"), ("PID_S010_幻影兵_ブレイブヒーロー", "Leif Paralogue Fabrication 8"),
            ("PID_S010_幻影兵_ランスナイト", "Leif Paralogue Fabrication 9"), ("PID_S010_幻影兵_アクスナイト", "Leif Paralogue Fabrication 10"),
            ("PID_S010_幻影兵_ランスファイター", "Leif Paralogue Fabrication 11"), ("PID_S010_幻影兵_ソードアーマー", "Leif Paralogue Fabrication 12"),
            ("PID_S010_幻影兵_アクスアーマー", "Leif Paralogue Fabrication 13"),
            ("PID_S011_幻影兵_サザ", "Micaiah Paralogue Sothe"), ("PID_S011_幻影兵_エディ", "Micaiah Paralogue Edward"),
            ("PID_S011_幻影兵_レオナルド", "Micaiah Paralogue Leonardo"), ("PID_S011_幻影兵_ノイス", "Micaiah Paralogue Nolan"),
            ("PID_S011_幻影兵_ジル", "Micaiah Paralogue Jill"), ("PID_S011_幻影兵_ツイハーク", "Micaiah Paralogue Zihark"),
            ("PID_S011_幻影兵_ローラ", "Micaiah Paralogue Laura"), ("PID_S011_幻影兵_ジェネラル", "Micaiah Paralogue Fabrication 1"),
            ("PID_S011_幻影兵_ハルバーディア", "Micaiah Paralogue Fabrication 2"), ("PID_S011_幻影兵_マージナイト", "Micaiah Paralogue Fabrication 3"),
            ("PID_S011_幻影兵_グレートナイト", "Micaiah Paralogue Fabrication 4"), ("PID_S011_幻影兵_パラディン", "Micaiah Paralogue Fabrication 5"),
            ("PID_S011_幻影兵_ウルフナイト", "Micaiah Paralogue Fabrication 6"), ("PID_S011_幻影兵_ボウナイト", "Micaiah Paralogue Fabrication 7"),
            ("PID_S011_幻影兵_セイジ", "Micaiah Paralogue Fabrication 8"), ("PID_S011_幻影兵_ハイプリースト", "Micaiah Paralogue Fabrication 9"),
            ("PID_S011_幻影兵_ウォーリアー", "Micaiah Paralogue Fabrication 10"), ("PID_S011_幻影兵_ベルセルク", "Micaiah Paralogue Fabrication 11"),
            ("PID_S011_幻影兵_スナイパー", "Micaiah Paralogue Fabrication 12"), ("PID_S011_幻影兵_グリフォンナイト", "Micaiah Paralogue Fabrication 13"),
            ("PID_S011_幻影兵_ドラゴンナイト", "Micaiah Paralogue Fabrication 14"), ("PID_S011_幻影兵_ソードマスター", "Micaiah Paralogue Fabrication 15"),
            ("PID_S011_幻影兵_ブレイブヒーロー", "Micaiah Paralogue Fabrication 16"), ("PID_M020_グリ", "Chapter 20 Griss"),
            ("PID_M020_異形兵_ソードマスター", "Chapter 20 Corrupted 1"), ("PID_M020_異形兵_ウォーリアー", "Chapter 20 Corrupted 2"),
            ("PID_M020_異形兵_グレートナイト", "Chapter 20 Corrupted 3"), ("PID_M020_異形兵_ウルフナイト", "Chapter 20 Corrupted 4"),
            ("PID_M020_イルシオン兵_ハルバーディア", "Chapter 20 Soldier 1"), ("PID_M020_イルシオン兵_パラディン", "Chapter 20 Soldier 2"),
            ("PID_M020_イルシオン兵_マージナイト", "Chapter 20 Soldier 3"), ("PID_M020_イルシオン兵_マスターモンク", "Chapter 20 Soldier 4"),
            ("PID_M020_イルシオン兵_ハイプリースト", "Chapter 20 Soldier 5"), ("PID_M020_シーフ", "Chapter 20 Thief"),
            ("PID_S012_幻影兵_リリーナ", "Roy Paralogue Lilina"),
            ("PID_S012_幻影兵_セイジ_ロイ周辺", "Roy Paralogue Fabrication 1"), ("PID_S012_幻影兵_セイジ_ロイ周辺2", "Roy Paralogue Fabrication 2"),
            ("PID_S012_幻影兵_ジェネラル_ロイ周辺", "Roy Paralogue Fabrication 3"), ("PID_S012_幻影兵_ジェネラル_ロイ周辺2", "Roy Paralogue Fabrication 4"),
            ("PID_S012_幻影兵_スナイパー_ロイ周辺", "Roy Paralogue Fabrication 5"), ("PID_S012_幻影兵_スナイパー_ロイ周辺2", "Roy Paralogue Fabrication 6"),
            ("PID_S012_幻影兵_ゲイル", "Roy Paralogue Galle"), ("PID_S012_幻影兵_セイジ", "Roy Paralogue Fabrication 7"),
            ("PID_S012_幻影兵_ジェネラル", "Roy Paralogue Fabrication 8"), ("PID_S012_幻影兵_ジェネラル_増援1", "Roy Paralogue Fabrication 9"),
            ("PID_S012_幻影兵_ドラゴンナイト", "Roy Paralogue Fabrication 10"), ("PID_S012_幻影兵_ドラゴンナイト_増援4", "Roy Paralogue Fabrication 11"),
            ("PID_S012_幻影兵_スナイパー", "Roy Paralogue Fabrication 12"), ("PID_S012_幻影兵_スナイパー_増援1", "Roy Paralogue Fabrication 13"),
            ("PID_S012_幻影兵_マスターモンク", "Roy Paralogue Fabrication 14"), ("PID_S012_幻影兵_ハルバーディア", "Roy Paralogue Fabrication 15"),
            ("PID_S012_幻影兵_パラディン", "Roy Paralogue Fabrication 16"), ("PID_S012_幻影兵_パラディン_増援4", "Roy Paralogue Fabrication 17"),
            ("PID_S012_幻影竜", "Roy Paralogue Fabrication 18"),
            ("PID_S013_幻影兵_召喚師１", "Celica Paralogue Fabrication 1"), ("PID_S013_幻影兵_召喚師２", "Celica Paralogue Fabrication 2"),
            ("PID_S013_幻影兵_召喚師３", "Celica Paralogue Fabrication 3"), ("PID_S013_幻影兵_ワープ１", "Celica Paralogue Fabrication 4"),
            ("PID_S013_幻影兵_ワープ２", "Celica Paralogue Fabrication 5"), ("PID_S013_幻影兵_ワープ３", "Celica Paralogue Fabrication 6"),
            ("PID_S013_幻影竜１", "Celica Paralogue Fabrication 7"), ("PID_S013_幻影竜２", "Celica Paralogue Fabrication 8"),
            ("PID_S013_幻影竜３", "Celica Paralogue Fabrication 9"), ("PID_S013_幻影兵_ハイプリースト", "Celica Paralogue Fabrication 10"),
            ("PID_S013_幻影兵_ブレイブヒーロー", "Celica Paralogue Fabrication 11"), ("PID_S013_幻影兵_ハルバーディア", "Celica Paralogue Fabrication 12"),
            ("PID_S013_幻影兵_スナイパー", "Celica Paralogue Fabrication 13"), ("PID_S013_幻影兵_パラディン", "Celica Paralogue Fabrication 14"),
            ("PID_S013_幻影兵_ボウナイト", "Celica Paralogue Fabrication 15"), ("PID_S013_幻影兵_ジェネラル", "Celica Paralogue Fabrication 16"),
            ("PID_S013_幻影兵_ドラゴンナイト", "Celica Paralogue Fabrication 17"), ("PID_S013_幻影兵_グリフォンナイト", "Celica Paralogue Fabrication 18"),
            ("PID_S013_幻影兵_セイジ", "Celica Paralogue Fabrication 19"), ("PID_S013_幻影兵_マージナイト", "Celica Paralogue Fabrication 20"),
            ("PID_S013_幻影兵_ランスファイター", "Celica Paralogue Fabrication 21"), ("PID_S013_幻影兵_アクスペガサス", "Celica Paralogue Fabrication 22"),
            ("PID_S013_幻影兵_ソードマスター", "Celica Paralogue Fabrication 23"), ("PID_M021_ヴェイル", "Chapter 21 Veyle"),
            ("PID_M021_セピア", "Chapter 21 Zephia"), ("PID_M021_グリ", "Chapter 21 Griss"),
            ("PID_M021_異形兵_ベルセルク", "Chapter 21 Corrupted 1"), ("PID_M021_異形兵_ウォーリアー", "Chapter 21 Corrupted 2"),
            ("PID_M021_異形兵_ソードマスター", "Chapter 21 Corrupted 3"), ("PID_M021_異形兵_ブレイブヒーロー", "Chapter 21 Corrupted 4"),
            ("PID_M021_異形兵_ハルバーディア", "Chapter 21 Corrupted 5"), ("PID_M021_異形兵_スナイパー", "Chapter 21 Corrupted 6"),
            ("PID_M021_異形兵_パラディン", "Chapter 21 Corrupted 7"), ("PID_M021_異形兵_ボウナイト", "Chapter 21 Corrupted 8"),
            ("PID_M021_異形兵_ジェネラル", "Chapter 21 Corrupted 9"), ("PID_M021_異形兵_ハイプリースト", "Chapter 21 Corrupted 10"),
            ("PID_M021_異形兵_ドラゴンナイト", "Chapter 21 Corrupted 11"), ("PID_M021_異形兵_セイジ", "Chapter 21 Corrupted 12"),
            ("PID_M021_異形兵_マージナイト", "Chapter 21 Corrupted 13"), ("PID_M021_異形兵_シーフ", "Chapter 21 Corrupted 14"),
            ("PID_M021_異形竜", "Chapter 21 Corrupted Wyrm"), ("PID_M022_ボス", "Chapter 22 Boss"),
            ("PID_M022_異形竜", "Chapter 22 Corrupted Wyrm"), ("PID_M022_異形兵_ソードマスター", "Chapter 22 Corrupted 1"),
            ("PID_M022_異形兵_ブレイブヒーロー", "Chapter 22 Corrupted 2"), ("PID_M022_異形兵_ハルバーディア", "Chapter 22 Corrupted 3"),
            ("PID_M022_異形兵_ロイヤルナイト", "Chapter 22 Corrupted 4"), ("PID_M022_異形兵_ベルセルク", "Chapter 22 Corrupted 5"),
            ("PID_M022_異形兵_ウォーリアー", "Chapter 22 Corrupted 6"), ("PID_M022_異形兵_スナイパー", "Chapter 22 Corrupted 7"),
            ("PID_M022_異形兵_スナイパー_魔", "Chapter 22 Corrupted 8"), ("PID_M022_異形兵_ボウナイト", "Chapter 22 Corrupted 9"),
            ("PID_M022_異形兵_ジェネラル", "Chapter 22 Corrupted 10"), ("PID_M022_異形兵_グレートナイト", "Chapter 22 Corrupted 11"),
            ("PID_M022_異形兵_パラディン", "Chapter 22 Corrupted 12"), ("PID_M022_異形兵_ウルフナイト", "Chapter 22 Corrupted 13"),
            ("PID_M022_異形兵_セイジ", "Chapter 22 Corrupted 14"), ("PID_M022_異形兵_マージナイト", "Chapter 22 Corrupted 15"),
            ("PID_M022_異形兵_マスターモンク", "Chapter 22 Corrupted 16"), ("PID_M022_異形兵_ハイプリースト", "Chapter 22 Corrupted 17"),
            ("PID_M022_異形兵_グリフォンナイト", "Chapter 22 Corrupted 18"), ("PID_M022_異形兵_ドラゴンナイト", "Chapter 22 Corrupted 19"),
            ("PID_M022_異形兵_シーフ", "Chapter 22 Corrupted 20"),
            ("PID_S014_幻影兵_アストリア", "Marth Paralogue Astram"),
            ("PID_S014_幻影兵_ジェネラル", "Marth Paralogue Fabrication 1"), ("PID_S014_幻影兵_ハルバーディア", "Marth Paralogue Fabrication 2"),
            ("PID_S014_幻影兵_セイジ", "Marth Paralogue Fabrication 3"), ("PID_S014_幻影兵_マスターモンク", "Marth Paralogue Fabrication 4"),
            ("PID_S014_幻影兵_ハイプリースト", "Marth Paralogue Fabrication 5"), ("PID_S014_幻影兵_ウォーリアー", "Marth Paralogue Fabrication 6"),
            ("PID_S014_幻影兵_スナイパー", "Marth Paralogue Fabrication 7"), ("PID_S014_幻影兵_ブレイブヒーロー", "Marth Paralogue Fabrication 8"),
            ("PID_S014_幻影兵_シーフ", "Marth Paralogue Fabrication 9"), ("PID_S014_幻影兵_ドラゴンナイト", "Marth Paralogue Fabrication 10"),
            ("PID_S014_幻影兵_マージナイト", "Marth Paralogue Fabrication 11"), ("PID_S014_幻影兵_グレートナイト", "Marth Paralogue Fabrication 12"),
            ("PID_S014_幻影兵_パラディン", "Marth Paralogue Fabrication 13"), ("PID_S014_幻影兵_ボウナイト", "Marth Paralogue Fabrication 14"),
            ("PID_S015_異形兵_ボス", "Alear Paralogue Boss"), ("PID_S015_異形兵_蛮族_指輪", "Alear Paralogue Corrupted 1"),
            ("PID_S015_異形兵_蛮族", "Alear Paralogue Corrupted 2"), ("PID_S015_異形兵_ブレイブヒーロー", "Alear Paralogue Corrupted 3"),
            ("PID_S015_異形兵_ウォーリアー", "Alear Paralogue Corrupted 4"), ("PID_S015_異形兵_ハルバーディア", "Alear Paralogue Corrupted 5"),
            ("PID_S015_異形兵_ジェネラル", "Alear Paralogue Corrupted 6"), ("PID_S015_異形兵_マージナイト", "Alear Paralogue Corrupted 7"),
            ("PID_S015_異形兵_ボウナイト", "Alear Paralogue Corrupted 8"), ("PID_S015_異形兵_ウルフナイト", "Alear Paralogue Corrupted 9"),
            ("PID_S015_異形兵_マスターモンク", "Alear Paralogue Corrupted 10"), ("PID_S015_異形兵_ハイプリースト", "Alear Paralogue Corrupted 11"),
            ("PID_S015_異形兵_グリフォンナイト", "Alear Paralogue Corrupted 12"), ("PID_S015_異形兵_シーフ", "Alear Paralogue Corrupted 13"),
            ("PID_M023_セピア", "Chapter 23 Zephia"), ("PID_M023_グリ", "Chapter 23 Griss"),
            ("PID_M023_異形兵_ソードマスター", "Chapter 23 Corrupted 1"), ("PID_M023_異形兵_ロイヤルナイト", "Chapter 23 Corrupted 2"),
            ("PID_M023_異形兵_ベルセルク", "Chapter 23 Corrupted 3"), ("PID_M023_異形兵_グレートナイト", "Chapter 23 Corrupted 4"),
            ("PID_M023_異形兵_パラディン", "Chapter 23 Corrupted 5"), ("PID_M023_異形兵_ウルフナイト", "Chapter 23 Corrupted 6"),
            ("PID_M023_異形兵_ボウナイト", "Chapter 23 Corrupted 7"), ("PID_M023_異形兵_マージナイト", "Chapter 23 Corrupted 8"),
            ("PID_M023_異形兵_マスターモンク", "Chapter 23 Corrupted 9"), ("PID_M023_異形兵_グリフォンナイト", "Chapter 23 Corrupted 10"),
            ("PID_M023_異形兵_グリフォンナイト_自壊持ち", "Chapter 23 Corrupted 11"), ("PID_M023_異形兵_ドラゴンナイト", "Chapter 23 Corrupted 12"),
            ("PID_M023_異形竜", "Chapter 23 Corrupted Wyrm"), ("PID_M024_リュール", "Past Alear"),
            ("PID_M024_異形兵_ブレイブヒーロー", "Chapter 24 Corrupted 1"), ("PID_M024_異形兵_ソードマスター", "Chapter 24 Corrupted 2"),
            ("PID_M024_異形兵_ベルセルク", "Chapter 24 Corrupted 3"), ("PID_M024_異形兵_ベルセルク_魔", "Chapter 24 Corrupted 4"),
            ("PID_M024_異形兵_ウォーリアー", "Chapter 24 Corrupted 5"), ("PID_M024_異形兵_スナイパー", "Chapter 24 Corrupted 6"),
            ("PID_M024_異形兵_スナイパー_ルナティック", "Chapter 24 Corrupted 7"), ("PID_M024_異形兵_スナイパー_魔", "Chapter 24 Corrupted 8"),
            ("PID_M024_異形兵_ジェネラル", "Chapter 24 Corrupted 9"), ("PID_M024_異形兵_グレートナイト", "Chapter 24 Corrupted 10"),
            ("PID_M024_異形兵_ウルフナイト", "Chapter 24 Corrupted 11"), ("PID_M024_異形兵_セイジ", "Chapter 24 Corrupted 12"),
            ("PID_M024_異形兵_マスターモンク", "Chapter 24 Corrupted 13"), ("PID_M024_異形兵_ハイプリースト", "Chapter 24 Corrupted 14"),
            ("PID_M024_異形兵_グリフォンナイト", "Chapter 24 Corrupted 15"), ("PID_M024_異形兵_ドラゴンナイト", "Chapter 24 Corrupted 16"),
            ("PID_M024_異形竜", "Chapter 24 Corrupted Wyrm"), ("PID_M025_ルミエル", "Corrupted Lumera"),
            ("PID_M025_異形兵_マスターモンク強", "Chapter 25 Corrupted 1"), ("PID_M025_異形兵_セイジ強", "Chapter 25 Corrupted 2"),
            ("PID_M025_異形兵_ハルバーディア", "Chapter 25 Corrupted 3"), ("PID_M025_異形兵_ブレイブヒーロー", "Chapter 25 Corrupted 4"),
            ("PID_M025_異形兵_ソードマスター", "Chapter 25 Corrupted 5"), ("PID_M025_異形兵_ブレイブヒーロー_増援", "Chapter 25 Corrupted 6"),
            ("PID_M025_異形兵_ウォーリアー", "Chapter 25 Corrupted 7"), ("PID_M025_異形兵_ウォーリアー_増援", "Chapter 25 Corrupted 8"),
            ("PID_M025_異形兵_ベルセルク", "Chapter 25 Corrupted 9"), ("PID_M025_異形兵_スナイパー", "Chapter 25 Corrupted 10"),
            ("PID_M025_異形兵_ジェネラル", "Chapter 25 Corrupted 11"), ("PID_M025_異形兵_ロイヤルナイト", "Chapter 25 Corrupted 12"),
            ("PID_M025_異形兵_グレートナイト", "Chapter 25 Corrupted 13"), ("PID_M025_異形兵_マージナイト", "Chapter 25 Corrupted 14"),
            ("PID_M025_異形兵_ボウナイト", "Chapter 25 Corrupted 15"), ("PID_M025_異形兵_パラディン", "Chapter 25 Corrupted 16"),
            ("PID_M025_異形兵_グリフォンナイト", "Chapter 25 Corrupted 17"), ("PID_M025_異形兵_ドラゴンナイト", "Chapter 25 Corrupted 18"),
            ("PID_M025_異形兵_ウルフナイト", "Chapter 25 Corrupted 19"), ("PID_M025_異形兵_シーフ", "Chapter 25 Corrupted 20"),
            ("PID_M025_異形兵_セイジ", "Chapter 25 Corrupted 21"), ("PID_M025_異形兵_セイジ_増援", "Chapter 25 Corrupted 22"),
            ("PID_M025_異形兵_マスターモンク", "Chapter 25 Corrupted 23"), ("PID_M025_異形兵_ハイプリースト", "Chapter 25 Corrupted 24"),
            ("PID_M025_異形竜", "Chapter 25 Corrupted Wyrm"), ("PID_M026_ソンブル_人型", "Chapter 26 Sombron"),
            ("PID_M026_ソンブル_竜型", "Chapter 25 Fell Sombron"), ("PID_M026_異形兵_マスター_メディウス", "Chapter 26 Corrupted 1"),
            ("PID_M026_異形兵_マスター_ドーマ", "Chapter 26 Corrupted 2"), ("PID_M026_異形兵_マスター_ロプトウス", "Chapter 26 Corrupted 3"),
            ("PID_M026_異形兵_マスター_ベルド", "Chapter 26 Corrupted 4"), ("PID_M026_異形兵_マスター_イドゥン", "Chapter 26 Corrupted 5"),
            ("PID_M026_異形兵_マスター_ネルガル", "Chapter 26 Corrupted 6"), ("PID_M026_異形兵_マスター_フォデス", "Chapter 26 Corrupted 7"),
            ("PID_M026_異形兵_マスター_アシュナード", "Chapter 26 Corrupted 8"), ("PID_M026_異形兵_マスター_アスタルテ", "Chapter 26 Corrupted 9"),
            ("PID_M026_異形兵_マスター_ギムレー", "Chapter 26 Corrupted 10"), ("PID_M026_異形兵_マスター_ハイドラ", "Chapter 26 Corrupted 11"),
            ("PID_M026_異形兵_マスター_ネメシス", "Chapter 26 Corrupted 12"), ("PID_M026_異形兵_ソードマスター_前半", "Chapter 26 Corrupted 13"),
            ("PID_M026_異形兵_ハルバーディア_前半", "Chapter 26 Corrupted 14"), ("PID_M026_異形兵_ベルセルク_前半", "Chapter 26 Corrupted 15"),
            ("PID_M026_異形兵_スナイパー_前半", "Chapter 26 Corrupted 16"), ("PID_M026_異形兵_セイジ_前半", "Chapter 26 Corrupted 17"),
            ("PID_M026_異形兵_マスターモンク_前半", "Chapter 26 Corrupted 18"), ("PID_M026_異形兵_ソードマスター", "Chapter 26 Corrupted 19"),
            ("PID_M026_異形兵_ブレイブヒーロー", "Chapter 26 Corrupted 20"), ("PID_M026_異形兵_ハルバーディア", "Chapter 26 Corrupted 21"),
            ("PID_M026_異形兵_ロイヤルナイト", "Chapter 26 Corrupted 22"), ("PID_M026_異形兵_ベルセルク", "Chapter 26 Corrupted 23"),
            ("PID_M026_異形兵_ウォーリアー", "Chapter 26 Corrupted 24"), ("PID_M026_異形兵_スナイパー", "Chapter 26 Corrupted 25"),
            ("PID_M026_異形兵_ボウナイト", "Chapter 26 Corrupted 26"), ("PID_M026_異形兵_セイジ", "Chapter 26 Corrupted 27"),
            ("PID_M026_異形兵_マージナイト", "Chapter 26 Corrupted 28"), ("PID_M026_異形兵_マスターモンク", "Chapter 26 Corrupted 29"),
            ("PID_M026_異形兵_ハイプリースト", "Chapter 26 Corrupted 30"), ("PID_M026_異形兵_パラディン", "Chapter 26 Corrupted 31"),
            ("PID_M026_異形兵_ウルフナイト", "Chapter 26 Corrupted 32"), ("PID_M026_異形兵_グリフォンナイト", "Chapter 26 Corrupted 33"),
            ("PID_M026_異形兵_ドラゴンナイト", "Chapter 26 Corrupted 34"), ("PID_M026_異形兵_ジェネラル", "Chapter 26 Corrupted 35"),
            ("PID_M026_異形兵_グレートナイト", "Chapter 26 Corrupted 36"), ("PID_M026_異形兵_シーフ", "Chapter 26 Corrupted 37"),
            ("PID_E001_Boss", "Xenologue 1 Fogado"), ("PID_E001_Boss_竜化", "Xenologue 1 Transformed Fogado"),
            ("PID_E001_異形兵_異形狼", "Xenologue 1 Corrupted Wolf 1"), ("PID_E001_異形兵強_異形狼", "Xenologue 1 Corrupted Wolf 2"),
            ("PID_E001_超強_異形狼", "Xenologue 1 Corrupted Wolf 3"), ("PID_E001_異形兵_アーチャー", "Xenologue 1 Corrupted 1"),
            ("PID_E001_異形兵_マージ", "Xenologue 1 Corrupted 2"), ("PID_E002_Boss", "Xenologue 2 Alfred"),
            ("PID_E002_Hide", "Xenologue 2 Céline"), ("PID_E002_異形兵_エンチャント", "Xenologue 2 Soldier 1"),
            ("PID_E002_異形兵強_エンチャント", "Xenologue 2 Soldier 2"), ("PID_E002_異形兵_ソードファイター", "Xenologue 2 Soldier 3"),
            ("PID_E002_異形兵_ランスファイター", "Xenologue 2 Soldier 4"), ("PID_E002_異形兵_アクスファイター", "Xenologue 2 Soldier 5"),
            ("PID_E002_異形兵_アーチャー", "Xenologue 2 Soldier 6"), ("PID_E002_異形兵_マージ", "Xenologue 2 Soldier 7"),
            ("PID_E002_異形兵_モンク", "Xenologue 2 Soldier 8"), ("PID_E002_異形兵_ソードアーマー", "Xenologue 2 Soldier 9"),
            ("PID_E002_異形兵_ランスアーマー", "Xenologue 2 Soldier 10"), ("PID_E002_異形兵_アクスアーマー", "Xenologue 2 Soldier 11"),
            ("PID_E002_異形兵_シーフ", "Xenologue 2 Soldier 12"),
            ("PID_E003_Boss", "Xenologue 3 Diamant"), ("PID_E003_Hide", "Xenologue 3 Alcryst"),
            ("PID_E003_召喚_マルス", "Xenologue 3 Marth Summon"), ("PID_E003_召喚_シグルド", "Xenologue 3 Sigurd Summon"),
            ("PID_E003_召喚_セリカ", "Xenologue 3 Celica Summon"), ("PID_E003_異形兵_マージカノン", "Xenologue 3 Soldier 1"),
            ("PID_E003_異形兵強_マージカノン", "Xenologue 3 Soldier 2"), ("PID_E003_異形兵_ソードマスター", "Xenologue 3 Soldier 3"),
            ("PID_E003_異形兵_ハルバーディア", "Xenologue 3 Soldier 4"), ("PID_E003_異形兵_ベルセルク", "Xenologue 3 Soldier 5"),
            ("PID_E003_異形兵_スナイパー", "Xenologue 3 Soldier 6"), ("PID_E003_異形兵_セイジ", "Xenologue 3 Soldier 7"),
            ("PID_E003_異形兵_マスターモンク", "Xenologue 3 Soldier 8"), ("PID_E003_異形兵_ジェネラル", "Xenologue 3 Soldier 9"),
            ("PID_E003_異形兵強_ジェネラル", "Xenologue 3 Soldier 10"), ("PID_E003_異形兵_エンチャント", "Xenologue 3 Soldier 11"),
            ("PID_E003_異形兵_グリフォンナイト", "Xenologue 3 Soldier 12"), ("PID_E003_異形兵_ドラゴンナイト", "Xenologue 3 Soldier 13"),
            ("PID_E003_異形兵_シーフ", "Xenologue 3 Soldier 14"), ("PID_E004_Boss", "Xenologue 4 Ivy"),
            ("PID_E004_異形兵_パラディン", "Xenologue 4 Elusian 1"), ("PID_E004_異形兵_ウルフナイト", "Xenologue 4 Elusian 2"),
            ("PID_E004_異形兵_ボウナイト", "Xenologue 4 Elusian 3"), ("PID_E004_異形兵_マージナイト", "Xenologue 4 Elusian 4"),
            ("PID_E004_異形兵_グレートナイト", "Xenologue 4 Elusian 5"), ("PID_E004_異形兵_ドラゴンナイト", "Xenologue 4 Elusian 6"),
            ("PID_E004_異形兵強_ドラゴンナイト", "Xenologue 4 Elusian 7"), ("PID_E004_異形兵_グリフォンナイト", "Xenologue 4 Elusian 8"),
            ("PID_E004_異形兵_異形飛竜", "Xenologue 4 Corrupted Wyvern 1"), ("PID_E004_異形兵強_異形飛竜", "Xenologue 4 Corrupted Wyvern 2"),
            ("PID_E004_Hide", "Xenologue 4 Timerra"), ("PID_E004_異形兵黄_ソードマスター", "Xenologue 4 Solmic 1"),
            ("PID_E004_異形兵黄_ハルバーディア", "Xenologue 4 Solmic 2"), ("PID_E004_異形兵黄_ベルセルク", "Xenologue 4 Solmic 3"),
            ("PID_E004_異形兵黄_スナイパー", "Xenologue 4 Solmic 4"), ("PID_E004_異形兵黄_セイジ", "Xenologue 4 Solmic 5"),
            ("PID_E004_異形兵黄_マスターモンク", "Xenologue 4 Solmic 6"), ("PID_E004_異形兵黄_ジェネラル", "Xenologue 4 Solmic 7"),
            ("PID_E004_異形兵黄強_ジェネラル", "Xenologue 4 Solmic 8"), ("PID_E004_異形兵黄_エンチャント", "Xenologue 4 Solmic 9"),
            ("PID_E004_異形兵黄_マージカノン", "Xenologue 4 Solmic 10"),
            ("PID_E005_Hide1", "Xenologue 5 Hortensia"), ("PID_E005_Hide2", "Xenologue 5 Fogado"),
            ("PID_E005_異形兵_ソードマスター", "Xenologue 5 Corrupted 1"), ("PID_E005_異形兵_ハルバーディア", "Xenologue 5 Corrupted 2"),
            ("PID_E005_異形兵_ベルセルク", "Xenologue 5 Corrupted 3"), ("PID_E005_異形兵強_ベルセルク", "Xenologue 5 Corrupted 4"),
            ("PID_E005_異形兵_スナイパー", "Xenologue 5 Corrupted 5"), ("PID_E005_異形兵_セイジ", "Xenologue 5 Corrupted 6"),
            ("PID_E005_異形兵強_セイジ", "Xenologue 5 Corrupted 7"), ("PID_E005_異形兵_マスターモンク", "Xenologue 5 Corrupted 8"),
            ("PID_E005_異形兵特殊_マスターモンク", "Xenologue 5 Corrupted 9"), ("PID_E005_異形兵_ジェネラル", "Xenologue 5 Corrupted 10"),
            ("PID_E005_異形兵強_ジェネラル", "Xenologue 5 Corrupted 11"), ("PID_E005_異形兵_エンチャント", "Xenologue 5 Corrupted 12"),
            ("PID_E005_異形兵_マージカノン", "Xenologue 5 Corrupted 13"), ("PID_E005_異形兵弱_マージカノン", "Xenologue 5 Corrupted 14"),
            ("PID_E005_異形兵_異形飛竜", "Xenologue 5 Corrupted 15"), ("PID_E005_異形兵弱_異形飛竜", "Xenologue 5 Corrupted 16"),
            ("PID_E005_異形兵_シーフ", "Xenologue 5 Corrupted 17"), ("PID_E005_異形兵_異形狼", "Xenologue 5 Corrupted 18"),
            ("PID_E005_異形兵強_異形狼", "Xenologue 5 Corrupted 19"), ("PID_E005_召喚_ドラゴンナイト", "Xenologue 5 Corrupted 20"),
            ("PID_E005_召喚_グレートナイト", "Xenologue 5 Corrupted 21"), ("PID_E005_召喚_マージナイト", "Xenologue 5 Corrupted 22"),
            ("PID_E006_Boss", "Xenologue 6 Fell Nil"),
            ("PID_E006_Hide1", "Xenologue 6 Alfred"), ("PID_E006_Hide2", "Xenologue 6 Céline"),
            ("PID_E006_Hide3", "Xenologue 6 Diamant"), ("PID_E006_Hide4", "Xenologue 6 Alcryst"),
            ("PID_E006_Hide5", "Xenologue 6 Ivy"), ("PID_E006_Hide6", "Xenologue 6 Hortensia"),
            ("PID_E006_Hide7", "Xenologue 6 Timerra"), ("PID_E006_Hide8", "Xenologue 6 Fogado"),
            ("PID_E006_Hide8_竜化", "Xenologue 6 Transformed Fogado"), ("PID_E006_異形兵_ソードマスター", "Xenologue 6 Corrupted 1"),
            ("PID_E006_異形兵_ブレイブヒーロー", "Xenologue 6 Corrupted 2"), ("PID_E006_異形兵_ハルバーディア", "Xenologue 6 Corrupted 3"),
            ("PID_E006_異形兵_ベルセルク", "Xenologue 6 Corrupted 4"), ("PID_E006_異形兵_スナイパー", "Xenologue 6 Corrupted 5"),
            ("PID_E006_異形兵_セイジ", "Xenologue 6 Corrupted 6"), ("PID_E006_異形兵_マスターモンク", "Xenologue 6 Corrupted 7"),
            ("PID_E006_異形兵_ジェネラル", "Xenologue 6 Corrupted 8"), ("PID_E006_異形兵強_ジェネラル", "Xenologue 6 Corrupted 9"),
            ("PID_E006_異形兵_エンチャント", "Xenologue 6 Corrupted 10"), ("PID_E006_異形兵_マージカノン", "Xenologue 6 Corrupted 11"),
            ("PID_E006_異形兵_異形飛竜", "Xenologue 6 Corrupted Wyvern"), ("PID_E006_異形兵_ウルフナイト", "Xenologue 6 Corrupted 12"),
            ("PID_E006_異形兵_ボウナイト", "Xenologue 6 Corrupted 13"), ("PID_E006_異形兵_マージナイト", "Xenologue 6 Corrupted 14"),
            ("PID_E006_異形兵_グレートナイト", "Xenologue 6 Corrupted 15"), ("PID_E006_異形兵_ドラゴンナイト", "Xenologue 6 Corrupted 16"),
            ("PID_E006_異形兵_グリフォンナイト", "Xenologue 6 Corrupted 17"), ("PID_E006_異形兵_異形狼", "Xenologue 6 Corrupted Wolf 1"),
            ("PID_E006_異形兵強_異形狼", "Xenologue 6 Corrupted Wolf 2"), ("PID_E006_召喚_マルス", "Xenologue 6 Marth Summon"),
            ("PID_E006_召喚_シグルド", "Xenologue 6 Sigurd Summon"), ("PID_E006_召喚_セリカ", "Xenologue 6 Celica Summon"),
            ("PID_E006_召喚_ロイ", "Xenologue 6 Roy Summon"), ("PID_E006_召喚_リーフ", "Xenologue 6 Leif Summon"),
            ("PID_E006_召喚異形兵_グレートナイト", "Xenologue 6 Corrupted Summon 1"), ("PID_E006_召喚異形兵_マスターモンク", "Xenologue 6 Corrupted Summon 2"),
            ("PID_E006_召喚異形兵_エンチャント", "Xenologue 6 Corrupted Summon 3"), ("PID_E006_召喚異形兵_ジェネラル", "Xenologue 6 Corrupted Summon 4"),
            ("PID_E006_召喚異形兵_ハルバーディア", "Xenologue 6 Corrupted Summon 5"), ("PID_E006_召喚異形兵_ベルセルク", "Xenologue 6 Corrupted Summon 6"),
            ("PID_E006_召喚異形兵_スナイパー", "Xenologue 6 Corrupted Summon 7"), ("PID_E006_召喚異形兵_ブレイブヒーロー", "Xenologue 6 Corrupted Summon 8"),
            ("PID_E006_召喚異形兵_ドラゴンナイト", "Xenologue 6 Corrupted Summon 9"), ("PID_E006_召喚異形兵_グリフォンナイト", "Xenologue 6 Corrupted Summon 10"),
            ("PID_E006_召喚異形兵_マージナイト", "Xenologue 6 Corrupted Summon 11"), ("PID_E006_召喚異形兵_ウルフナイト", "Xenologue 6 Corrupted Summon 12"),
            ("PID_E006_召喚異形兵_マージカノン", "Xenologue 6 Corrupted Summon 13"), ("PID_E006_召喚異形兵_セイジ", "Xenologue 6 Corrupted Summon 14"),
            ("PID_E006_召喚異形兵_異形狼", "Xenologue 6 Corrupted Wolf Summon 1"), ("PID_E006_召喚異形兵強_異形狼", "Xenologue 6 Corrupted Wolf Summon 2"),
        };

        internal static List<(string id, string name)> DivineParalogueEmblemCharacters { get; } = new()
        {
            ("PID_G001_チキ", "Tiki Paralogue Tiki"),
            ("PID_G001_チキ_特効無効", "Tiki Paralogue No Effectiveness Tiki"), ("PID_G001_チキ_竜化", "Tiki Paralogue Transformed Tiki"),
            ("PID_G001_チキ_竜化_特効無効", "Tiki Paralogue Transformed No Effectiveness Tiki"), ("PID_G002_ヘクトル", "Hector Paralogue Hector"),
            ("PID_G003_ヴェロニカ_ノーマル", "Veronica Paralogue Veronica Normal"),
            ("PID_G003_ヴェロニカ", "Veronica Paralogue Veronica"), ("PID_G004_セネリオ", "Soren Paralogue Soren"),
            ("PID_G004_セネリオ_移動－１", "Soren Paralogue Mov -1 Soren"), ("PID_G004_セネリオ_ルナ", "Soren Paralogue Soren Maddening"),
            ("PID_G004_セネリオ_ルナ_移動－１", "Soren Paralogue Mov -1 Soren Maddening"), ("PID_G005_カミラ", "Camilla Paralogue Camilla"),
            ("PID_G006_クロム", "Chrom Paralogue Chrom"),
            ("PID_G006_クロム_移動－１", "Chrom Paralogue Mov -1 Chrom"), ("PID_G006_ルフレ", "Chrom Paralogue Robin"),
            ("PID_G006_ルフレ_移動－１", "Chrom Paralogue Mov -1 Robin"),
        };

        internal static List<(string id, string name)> EmblemBossCharacters { get; } = new(); // FixedLevelEmblemCharacters + DivineParalogueEmblemCharacters

        internal static List<(string id, string name)> NonArenaEnemyCharacters { get; } = new() // FixedLevelEnemyCharacters + DivineParalogueEmblemCharacters +
        {
            ("PID_遭遇戦_異形兵_男", "Skirmish Corrupted Male"), ("PID_遭遇戦_異形兵_女", "Skirmish Corrupted Female"),
            ("PID_遭遇戦_異形兵_男_上級", "Skirmish Strong Corrupted Male"), ("PID_遭遇戦_異形兵_女_上級", "Skirmish Strong Corrupted Female"),
            ("PID_遭遇戦_ならずもの_男", "Skirmish Ruffian Male"), ("PID_遭遇戦_ならずもの_女", "Skirmish Ruffian Female"),
            ("PID_遭遇戦_フィレネ兵_男", "Skirmish Firenese Male"), ("PID_遭遇戦_フィレネ兵_女", "Skirmish Firenese Female"),
            ("PID_遭遇戦_ブロディア兵_男", "Skirmish Brodian Male"), ("PID_遭遇戦_ブロディア兵_女", "Skirmish Brodian Female"),
            ("PID_遭遇戦_ソルム兵_男", "Skirmish Solmic Male"), ("PID_遭遇戦_ソルム兵_女", "Skirmish Solmic Female"),
            ("PID_遭遇戦_イルシオン兵_男", "Skirmish Elusian Male"), ("PID_遭遇戦_イルシオン兵_女", "Skirmish Elusian Female"),
            ("PID_遭遇戦_レア経験値_男", "Skirmish Silver Corrupted Male"), ("PID_遭遇戦_レア経験値_女", "Skirmish Silver Corrupted Female"),
            ("PID_遭遇戦_レアお金_男", "Skirmish Gold Corrupted Male"), ("PID_遭遇戦_レアお金_女", "Skirmish Gold Corrupted Female"),
            ("PID_やり込み_幻影兵_男", "Tempest Trial Fabrication Male"), ("PID_やり込み_幻影兵_女", "Tempest Trial Fabrication Female"),
            ("PID_やり込み_幻影竜", "Tempest Trial Phantom Wyrm"), ("PID_G000_幻影兵_ソードファイター", "Divine Paralogue Fabrication 1"),
            ("PID_G000_幻影兵_ランスファイター", "Divine Paralogue Fabrication 2"), ("PID_G000_幻影兵_アクスファイター", "Divine Paralogue Fabrication 3"),
            ("PID_G000_幻影兵_アーチャー", "Divine Paralogue Fabrication 4"), ("PID_G000_幻影兵_ソードペガサス", "Divine Paralogue Fabrication 5"),
            ("PID_G000_幻影兵_ランスペガサス", "Divine Paralogue Fabrication 6"), ("PID_G000_幻影兵_アクスペガサス", "Divine Paralogue Fabrication 7"),
            ("PID_G000_幻影兵_ソードナイト", "Divine Paralogue Fabrication 8"), ("PID_G000_幻影兵_ランスナイト", "Divine Paralogue Fabrication 9"),
            ("PID_G000_幻影兵_アクスナイト", "Divine Paralogue Fabrication 10"), ("PID_G000_幻影兵_ソードアーマー", "Divine Paralogue Fabrication 11"),
            ("PID_G000_幻影兵_ランスアーマー", "Divine Paralogue Fabrication 12"), ("PID_G000_幻影兵_アクスアーマー", "Divine Paralogue Fabrication 13"),
            ("PID_G000_幻影兵_マージ", "Divine Paralogue Fabrication 14"), ("PID_G000_幻影兵_モンク", "Divine Paralogue Fabrication 15"),
            ("PID_G000_幻影兵_エンチャント", "Divine Paralogue Fabrication 16"), ("PID_G000_幻影兵_マージカノン", "Divine Paralogue Fabrication 17"),
            ("PID_G000_幻影兵_シーフ", "Divine Paralogue Fabrication 18"), ("PID_G000_幻影竜", "Divine Paralogue Fabrication 19"),
            ("PID_G000_幻影飛竜", "Divine Paralogue Fabrication 20"), ("PID_G000_幻影狼", "Divine Paralogue Fabrication 21"),
            ("PID_G000_幻影兵_ソードペガサス_経験値なし", "Divine Paralogue Fabrication 22"), ("PID_G000_幻影兵_ランスペガサス_経験値なし", "Divine Paralogue Fabrication 23"),
            ("PID_G000_幻影兵_アクスナイト_経験値なし", "Divine Paralogue Fabrication 24"), ("PID_G000_幻影飛竜_弱", "Divine Paralogue 0 Fabrication 25"),
            ("PID_G002_幻影兵_オズイン", "Hector Paralogue Oswin"),
            ("PID_G002_幻影兵_マシュー", "Hector Paralogue Matthew"), ("PID_G002_幻影兵_ウルフナイト", "Hector Paralogue Fabrication 1"),
            ("PID_G002_幻影兵_ソードペガサス_増援", "Hector Paralogue Fabrication 2"), ("PID_G002_幻影兵_ランスペガサス_増援", "Hector Paralogue Fabrication 3"),
            ("PID_G002_幻影兵_ソードナイト_増援", "Hector Paralogue Fabrication 4"), ("PID_G002_幻影兵_ランスナイト_増援", "Hector Paralogue Fabrication 5"),
            ("PID_G002_幻影兵_アクスペガサス_増援", "Hector Paralogue Fabrication 6"), ("PID_G002_幻影兵_アクスアーマー_増援", "Hector Paralogue Fabrication 7"),
            ("PID_G002_幻影兵_ランスファイター_増援", "Hector Paralogue Fabrication 8"), ("PID_G004_幻影兵_シーフ_移動－１", "Soren Paralogue Fabrication 1"),
            ("PID_G004_幻影兵_シーフ_移動－３", "Soren Paralogue Fabrication 2"), ("PID_G004_幻影兵_ソードペガサス_経験値なし", "Soren Paralogue Fabrication 3"),
            ("PID_G004_幻影兵_ランスペガサス_経験値なし", "Soren Paralogue Fabrication 4"), ("PID_G004_幻影兵_ソードナイト_経験値なし", "Soren Paralogue Fabrication 5"),
            ("PID_G004_幻影兵_アクスナイト_経験値なし", "Soren Paralogue Fabrication 6"),
            ("PID_G005_幻影兵_ベルカ", "Camilla Paralogue Beruka"), ("PID_G005_幻影兵_ベルカ_スキル持ち", "Camilla Paralogue Skilled Beruka"),
            ("PID_G005_幻影兵_ルーナ", "Camilla Paralogue Severa"), ("PID_G005_幻影兵_ルーナ_スキル持ち", "Camilla Paralogue Skilled Severa"),
            ("PID_G005_幻影兵_シーフ", "Camilla Paralogue Fabrication 1"), ("PID_G005_幻影兵_ドラゴンナイト", "Camilla Paralogue Fabrication 2"),
            ("PID_G005_幻影兵_グリフォンナイト", "Camilla Paralogue Fabrication 3"), ("PID_G006_幻影兵_セイジ", "Chrom Paralogue Fabrication 1"),
            ("PID_G006_幻影狼_移動－１", "Chrom Paralogue Fabrication 2"), ("PID_遭遇戦_異形狼", "Skirmish Corrupted Wolf"),
            ("PID_遭遇戦_異形飛竜", "Skirmish Corrupted Wyvern"), ("PID_遭遇戦_レア経験値_異形狼", "Skirmish Silver Corrupted Wolf"),
            ("PID_遭遇戦_レアお金_異形飛竜", "Skirmish Gold Corrupted Wyvern"),
        };

        internal static List<(string id, string name)> EnemyCharacters { get; } = new(); // FixedLevelEnemyCharacters + NonArenaEnemyCharacters + ArenaCharacters

        internal static List<(string id, string name)> ArenaCharacters { get; } = new()
        {
            ("PID_闘技場_マルス", "Arena Marth"), ("PID_闘技場_シグルド", "Arena Sigurd"),
            ("PID_闘技場_セリカ", "Arena Celica"), ("PID_闘技場_ミカヤ", "Arena Micaiah"),
            ("PID_闘技場_ロイ", "Arena Roy"), ("PID_闘技場_リーフ", "Arena Leif"),
            ("PID_闘技場_ルキナ", "Arena Lucina"), ("PID_闘技場_リン", "Arena Lyn"),
            ("PID_闘技場_アイク", "Arena Ike"), ("PID_闘技場_ベレト", "Arena Byleth"),
            ("PID_闘技場_カムイ", "Arena Corrin"), ("PID_闘技場_エイリーク", "Arena Eirika"),
            ("PID_闘技場_エーデルガルト", "Arena Edelgard"),
            ("PID_闘技場_ディミトリ", "Arena Dimitri"), ("PID_闘技場_クロード", "Arena Claude"),
            ("PID_闘技場_チキ", "Arena Tiki"), ("PID_闘技場_ヘクトル", "Arena Hector"),
            ("PID_闘技場_ヴェロニカ", "Arena Veronica"), ("PID_闘技場_セネリオ", "Arena Soren"),
            ("PID_闘技場_カミラ", "Arena Camilla"), ("PID_闘技場_クロム", "Arena Chrom"),
            ("PID_闘技場_ルフレ", "Arena Robin"),
        };

        internal static Dictionary<string, string> ArenaCharacterToGrowthTables { get; } = new()
        {
            { "PID_闘技場_マルス", "GGID_マルス" }, { "PID_闘技場_シグルド", "GGID_シグルド" },
            { "PID_闘技場_セリカ", "GGID_セリカ" }, { "PID_闘技場_ミカヤ", "GGID_ミカヤ" },
            { "PID_闘技場_ロイ", "GGID_ロイ" }, { "PID_闘技場_リーフ", "GGID_リーフ" },
            { "PID_闘技場_ルキナ", "GGID_ルキナ" }, { "PID_闘技場_リン", "GGID_リン" },
            { "PID_闘技場_アイク", "GGID_アイク" }, { "PID_闘技場_ベレト", "GGID_ベレト" },
            { "PID_闘技場_カムイ", "GGID_カムイ" }, { "PID_闘技場_エイリーク", "GGID_エイリーク" },
            { "PID_闘技場_エーデルガルト", "GGID_エーデルガルト" }, { "PID_闘技場_ディミトリ", "GGID_エーデルガルト" },
            { "PID_闘技場_クロード", "GGID_エーデルガルト" }, { "PID_闘技場_チキ", "GGID_チキ" },
            { "PID_闘技場_ヘクトル", "GGID_ヘクトル" }, { "PID_闘技場_ヴェロニカ", "GGID_ヴェロニカ" },
            { "PID_闘技場_セネリオ", "GGID_セネリオ" }, { "PID_闘技場_カミラ", "GGID_カミラ" },
            { "PID_闘技場_クロム", "GGID_クロム" }, { "PID_闘技場_ルフレ", "GGID_クロム" },
        };

        internal static List<(string id, string name)> OtherNPCCharacters { get; } = new()
        {
            ("PID_不明", "Null"), ("PID_ルミエル", "Lumera"),
            ("PID_ソンブル", "Sombron"), ("PID_イヴ", "Éve"),
            ("PID_モリオン", "Morion"), ("PID_ハイアシンス", "Hyacinth"),
            ("PID_スフォリア", "Seforia"),
            ("PID_ヴェイル_フード", "Hooded Veyle"), ("PID_ヴェイル_包帯", "Bandaged Veyle"),
            ("PID_ヴェイル_フード_顔出し", "Veyle Face Reveal"), ("PID_ヴェイル_白_悪", "Evil Veyle"),
            ("PID_ヴェイル_黒_悪", "Black Evil Veyle"), ("PID_ヴェイル_黒_善", "Black Veyle"),
            ("PID_ヴェイル_黒_善_角折れ", "Broken Helmet Veyle"), ("PID_セピア", "Zephia"),
            ("PID_グリ", "Griss"), ("PID_マロン", "Marni"),
            ("PID_ジェーデ_兜あり", "Helmeted Jade"), ("PID_残像", "Illusory Double"),
            ("PID_M000_リュール", "Prologue Alear"), ("PID_M000_ソンブル", "Prologue Sombron"),
            ("PID_M004_村人おじいさん", "Chapter 4 Villager 1"),
            ("PID_M004_村人お姉さん", "Chapter 4 Villager 2"),
            ("PID_S001_村人_訪問", "Jean Paralogue Villager 3"), ("PID_S001_ジャン_母親", "Anje"),
            ("PID_S001_村人_老人男", "Jean Paralogue Villager 4"), ("PID_S001_村人女の子", "Jean Paralogue Villager 5"),
            ("PID_M013_村人おじいさん", "Chapter 13 Villager 1"), ("PID_M013_村人女の子", "Chapter 13 Villager 2"),
            ("PID_M013_村人おばさん", "Chapter 13 Villager 3"),
            ("PID_M015_カムイ", "Chapter 15 Corrin"),
            ("PID_M016_村人おばさん", "Chapter 16 Villager"), ("PID_M022_紋章士_シグルド", "Chapter 22 Sigurd"),
            ("PID_M022_紋章士_セリカ", "Chapter 22 Celica"), ("PID_M022_紋章士_ミカヤ", "Chapter 22 Micaiah"),
            ("PID_M022_紋章士_ロイ", "Chapter 22 Roy"), ("PID_M022_紋章士_リーフ", "Chapter 22 Leif"),
            ("PID_M022_紋章士_ルキナ", "Chapter 22 Lucina"), ("PID_M022_紋章士_リン", "Chapter 22 Lyn"),
            ("PID_M022_紋章士_アイク", "Chapter 22 Ike"), ("PID_M022_紋章士_ベレト", "Chapter 22 Byleth"),
            ("PID_M022_紋章士_カムイ", "Chapter 22 Corrin"), ("PID_M022_紋章士_エイリーク", "Chapter 22 Eirika"),
            ("PID_武器屋", "Durthon"),
            ("PID_道具屋", "Anisse"), ("PID_アクセ屋", "Pinet"),
            ("PID_錬成屋", "Calney"), ("PID_ソラ", "Sommie"),
            ("PID_イル", "Nil"), ("PID_フィレネ村人_青年男1", "Firenese Male 1"),
            ("PID_フィレネ村人_青年男2", "Firenese Male 2"), ("PID_フィレネ村人_青年男3", "Firenese Male 3"),
            ("PID_フィレネ村人_青年男4", "Firenese Male 4"), ("PID_フィレネ村人_青年男5", "Firenese Male 5"),
            ("PID_フィレネ村人_青年男6", "Firenese Male 6"), ("PID_フィレネ村人_青年男7", "Firenese Male 7"),
            ("PID_フィレネ村人_青年男8", "Firenese Male 8"), ("PID_フィレネ村人_青年男9", "Firenese Male 9"),
            ("PID_フィレネ村人_青年女1", "Firenese Female 1"), ("PID_フィレネ村人_青年女2", "Firenese Female 2"),
            ("PID_フィレネ村人_青年女3", "Firenese Female 3"), ("PID_フィレネ村人_青年女4", "Firenese Female 4"),
            ("PID_フィレネ村人_青年女5", "Firenese Female 5"), ("PID_フィレネ村人_青年女6", "Firenese Female 6"),
            ("PID_フィレネ村人_青年女7", "Firenese Female 7"), ("PID_フィレネ村人_青年女8", "Firenese Female 8"),
            ("PID_フィレネ村人_青年女9", "Firenese Female 9"), ("PID_フィレネ村人_壮年男1", "Firenese Male 10"),
            ("PID_フィレネ村人_壮年男2", "Firenese Male 11"), ("PID_フィレネ村人_壮年男3", "Firenese Male 12"),
            ("PID_フィレネ村人_壮年男4", "Firenese Male 13"), ("PID_フィレネ村人_壮年男5", "Firenese Male 14"),
            ("PID_フィレネ村人_壮年男6", "Firenese Male 15"), ("PID_フィレネ村人_壮年女1", "Firenese Female 10"),
            ("PID_フィレネ村人_壮年女2", "Firenese Female 11"), ("PID_フィレネ村人_壮年女3", "Firenese Female 12"),
            ("PID_フィレネ村人_壮年女4", "Firenese Female 13"), ("PID_フィレネ村人_壮年女5", "Firenese Female 14"),
            ("PID_フィレネ村人_壮年女6", "Firenese Female 15"), ("PID_フィレネ村人_老人男1", "Firenese Male 16"),
            ("PID_フィレネ村人_老人男2", "Firenese Male 17"), ("PID_フィレネ村人_老人男3", "Firenese Male 18"),
            ("PID_フィレネ村人_老人男4", "Firenese Male 19"), ("PID_フィレネ村人_老人男5", "Firenese Male 20"),
            ("PID_フィレネ村人_老人男6", "Firenese Male 21"), ("PID_ソルム村人_青年男1", "Solmic Male 1"),
            ("PID_ソルム村人_青年男2", "Solmic Male 2"), ("PID_ソルム村人_青年男3", "Solmic Male 3"),
            ("PID_ソルム村人_青年男4", "Solmic Male 4"), ("PID_ソルム村人_青年男5", "Solmic Male 5"),
            ("PID_ソルム村人_青年男6", "Solmic Male 6"), ("PID_ソルム村人_青年男7", "Solmic Male 7"),
            ("PID_ソルム村人_青年男8", "Solmic Male 8"), ("PID_ソルム村人_青年男9", "Solmic Male 9"),
            ("PID_ソルム村人_青年女1", "Solmic Female 1"), ("PID_ソルム村人_青年女2", "Solmic Female 2"),
            ("PID_ソルム村人_青年女3", "Solmic Female 3"), ("PID_ソルム村人_青年女4", "Solmic Female 4"),
            ("PID_ソルム村人_青年女5", "Solmic Female 5"), ("PID_ソルム村人_青年女6", "Solmic Female 6"),
            ("PID_ソルム村人_青年女7", "Solmic Female 7"), ("PID_ソルム村人_青年女8", "Solmic Female 8"),
            ("PID_ソルム村人_青年女9", "Solmic Female 9"), ("PID_ソルム村人_壮年男1", "Solmic Male 10"),
            ("PID_ソルム村人_壮年男2", "Solmic Male 11"), ("PID_ソルム村人_壮年男3", "Solmic Male 12"),
            ("PID_ソルム村人_壮年男4", "Solmic Male 13"), ("PID_ソルム村人_壮年男5", "Solmic Male 14"),
            ("PID_ソルム村人_壮年男6", "Solmic Male 15"), ("PID_ソルム村人_壮年女1", "Solmic Female 10"),
            ("PID_ソルム村人_壮年女2", "Solmic Female 11"), ("PID_ソルム村人_壮年女3", "Solmic Female 12"),
            ("PID_ソルム村人_壮年女4", "Solmic Female 13"), ("PID_ソルム村人_壮年女5", "Solmic Female 14"),
            ("PID_ソルム村人_壮年女6", "Solmic Female 15"), ("PID_ソルム村人_老人男1", "Solmic Male 16"),
            ("PID_ソルム村人_老人男2", "Solmic Male 17"), ("PID_ソルム村人_老人男3", "Solmic Male 18"),
            ("PID_ソルム村人_老人男4", "Solmic Male 19"), ("PID_ソルム村人_老人男5", "Solmic Male 20"),
            ("PID_ソルム村人_老人男6", "Solmic Male 21"), ("PID_フィレネ兵士_ランスアーマー1", "Firenese Lance Armor 1"),
            ("PID_フィレネ兵士_ランスアーマー2", "Firenese Lance Armor 2"), ("PID_フィレネ兵士_ランスアーマー3", "Firenese Lance Armor 3"),
            ("PID_フィレネ兵士_ランスアーマー4", "Firenese Lance Armor 4"), ("PID_フィレネ兵士_ランスアーマー5", "Firenese Lance Armor 5"),
            ("PID_フィレネ兵士_ランスアーマー6", "Firenese Lance Armor 6"), ("PID_フィレネ兵士_ランスアーマー7", "Firenese Lance Armor 7"),
            ("PID_フィレネ兵士_ランスアーマー8", "Firenese Lance Armor 8"), ("PID_フィレネ兵士_ランスアーマー9", "Firenese Lance Armor 9"),
            ("PID_フィレネ兵士_ランスアーマー10", "Firenese Lance Armor 10"), ("PID_フィレネ兵士_ソードナイト1", "Firenese Sword Cavalier 1"),
            ("PID_フィレネ兵士_ソードナイト2", "Firenese Sword Cavalier 2"), ("PID_フィレネ兵士_ソードナイト3", "Firenese Sword Cavalier 3"),
            ("PID_フィレネ兵士_ソードナイト4", "Firenese Sword Cavalier 4"), ("PID_フィレネ兵士_モンク1", "Firenese Martial Monk 1"),
            ("PID_フィレネ兵士_モンク2", "Firenese Martial Monk 2"), ("PID_フィレネ兵士_モンク3", "Firenese Martial Monk 3"),
            ("PID_フィレネ兵士_モンク4", "Firenese Martial Monk 4"), ("PID_フィレネ兵士_マージ1", "Firenese Mage 1"),
            ("PID_フィレネ兵士_マージ2", "Firenese Mage 2"), ("PID_フィレネ兵士_マージ3", "Firenese Mage 3"),
            ("PID_フィレネ兵士_マージ4", "Firenese Mage 4"), ("PID_フィレネ兵士_アーチャー1", "Firenese Archer 1"),
            ("PID_フィレネ兵士_アーチャー2", "Firenese Archer 2"), ("PID_フィレネ兵士_アーチャー3", "Firenese Archer 3"),
            ("PID_フィレネ兵士_アーチャー4", "Firenese Archer 4"), ("PID_ブロディア兵士_ランスアーマー1", "Brodian Lance Armor 1"),
            ("PID_ブロディア兵士_ランスアーマー2", "Brodian Lance Armor 2"), ("PID_ブロディア兵士_ランスアーマー3", "Brodian Lance Armor 3"),
            ("PID_ブロディア兵士_ランスアーマー4", "Brodian Lance Armor 4"), ("PID_ブロディア兵士_ランスアーマー5", "Brodian Lance Armor 5"),
            ("PID_ブロディア兵士_ランスアーマー6", "Brodian Lance Armor 6"), ("PID_ブロディア兵士_ランスアーマー7", "Brodian Lance Armor 7"),
            ("PID_ブロディア兵士_ランスアーマー8", "Brodian Lance Armor 8"), ("PID_ブロディア兵士_ランスアーマー9", "Brodian Lance Armor 9"),
            ("PID_ブロディア兵士_ランスアーマー10", "Brodian Lance Armor 10"), ("PID_ブロディア兵士_ソードナイト1", "Brodian Sword Cavalier 1"),
            ("PID_ブロディア兵士_ソードナイト2", "Brodian Sword Cavalier 2"), ("PID_ブロディア兵士_ソードナイト3", "Brodian Sword Cavalier 3"),
            ("PID_ブロディア兵士_ソードナイト4", "Brodian Sword Cavalier 4"), ("PID_ブロディア兵士_モンク1", "Brodian Martial Monk 1"),
            ("PID_ブロディア兵士_モンク2", "Brodian Martial Monk 2"), ("PID_ブロディア兵士_モンク3", "Brodian Martial Monk 3"),
            ("PID_ブロディア兵士_モンク4", "Brodian Martial Monk 4"), ("PID_ブロディア兵士_マージ1", "Brodian Mage 1"),
            ("PID_ブロディア兵士_マージ2", "Brodian Mage 2"), ("PID_ブロディア兵士_マージ3", "Brodian Mage 3"),
            ("PID_ブロディア兵士_マージ4", "Brodian Mage 4"), ("PID_ブロディア兵士_アーチャー1", "Brodian Archer 1"),
            ("PID_ブロディア兵士_アーチャー2", "Brodian Archer 2"), ("PID_ブロディア兵士_アーチャー3", "Brodian Archer 3"),
            ("PID_ブロディア兵士_アーチャー4", "Brodian Archer 4"), ("PID_ソルム兵士_ランスアーマー1", "Solmic Lance Armor 1"),
            ("PID_ソルム兵士_ランスアーマー2", "Solmic Lance Armor 2"), ("PID_ソルム兵士_ランスアーマー3", "Solmic Lance Armor 3"),
            ("PID_ソルム兵士_ランスアーマー4", "Solmic Lance Armor 4"), ("PID_ソルム兵士_ランスアーマー5", "Solmic Lance Armor 5"),
            ("PID_ソルム兵士_ランスアーマー6", "Solmic Lance Armor 6"), ("PID_ソルム兵士_ランスアーマー7", "Solmic Lance Armor 7"),
            ("PID_ソルム兵士_ランスアーマー8", "Solmic Lance Armor 8"), ("PID_ソルム兵士_ランスアーマー9", "Solmic Lance Armor 9"),
            ("PID_ソルム兵士_ランスアーマー10", "Solmic Lance Armor 10"), ("PID_ソルム兵士_ソードナイト1", "Solmic Sword Cavalier 1"),
            ("PID_ソルム兵士_ソードナイト2", "Solmic Sword Cavalier 2"), ("PID_ソルム兵士_ソードナイト3", "Solmic Sword Cavalier 3"),
            ("PID_ソルム兵士_ソードナイト4", "Solmic Sword Cavalier 4"), ("PID_ソルム兵士_モンク1", "Solmic Martial Monk 1"),
            ("PID_ソルム兵士_モンク2", "Solmic Martial Monk 2"), ("PID_ソルム兵士_モンク3", "Solmic Martial Monk 3"),
            ("PID_ソルム兵士_モンク4", "Solmic Martial Monk 4"), ("PID_ソルム兵士_マージ1", "Solmic Mage 1"),
            ("PID_ソルム兵士_マージ2", "Solmic Mage 2"), ("PID_ソルム兵士_マージ3", "Solmic Mage 3"),
            ("PID_ソルム兵士_マージ4", "Solmic Mage 4"), ("PID_ソルム兵士_アーチャー1", "Solmic Archer 1"),
            ("PID_ソルム兵士_アーチャー2", "Solmic Archer 2"), ("PID_ソルム兵士_アーチャー3", "Solmic Archer 3"),
            ("PID_ソルム兵士_アーチャー4", "Solmic Archer 4"), ("PID_エル_竜化", "Transformed Nel"),
            ("PID_ラファール_竜化", "Transformed Rafal"), ("PID_ルフレ", "Robin (Character)"),
            ("PID_闇ルフレ", "Corrupted Robin"),
        };

        internal static List<(string id, string name)> Characters { get; } = new(); // PlayableCharacters + NPCCharacters
        internal static List<(string id, string name)> NPCCharacters { get; } = new(); // AllyNPCCharacters + EnemyCharacters
        internal static List<(string id, string name)> NonArenaNPCCharacters { get; } = new(); // AllyNPCCharacters + NonArenaEnemyCharacters
        internal static List<(string id, string name)> FixedLevelCharacters { get; } = new(); // PlayableCharacters + FixedLevelAllyNPCCharacters + FixedLevelEnemyCharacters
        #endregion
        #region Class IDs
        internal static List<(string id, string name)> UniversalClasses { get; } = new()
        {
            ("JID_神竜ノ子", "Dragon Child"), ("JID_神竜ノ王", "Divine Dragon (Alear)"), ("JID_ソードファイター", "Sword Fighter"), ("JID_ソードマスター", "Swordmaster"),
            ("JID_ブレイブヒーロー", "Hero"), ("JID_ランスファイター", "Lance Fighter"), ("JID_ハルバーディア", "Halberdier"), ("JID_ロイヤルナイト", "Royal Knight"),
            ("JID_アクスファイター", "Axe Fighter"), ("JID_ベルセルク", "Berserker"), ("JID_ウォーリアー", "Warrior"), ("JID_アーチャー", "Archer"),
            ("JID_スナイパー", "Sniper"), ("JID_ボウナイト", "Bow Knight"), ("JID_ソードアーマー", "Sword Armor"), ("JID_ランスアーマー", "Lance Armor"),
            ("JID_アクスアーマー", "Axe Armor"), ("JID_ジェネラル", "General"), ("JID_グレートナイト", "Great Knight"), ("JID_ソードナイト", "Sword Cavalier"),
            ("JID_ランスナイト", "Lance Cavalier"), ("JID_アクスナイト", "Axe Cavalier"), ("JID_パラディン", "Paladin"), ("JID_ウルフナイト", "Wolf Knight"),
            ("JID_マージ", "Mage"), ("JID_セイジ", "Sage"), ("JID_マージナイト", "Mage Knight"), ("JID_モンク", "Martial Monk"),
            ("JID_マスターモンク", "Martial Master"), ("JID_ハイプリースト", "High Priest"), ("JID_グリフォンナイト", "Griffin Knight"), ("JID_ドラゴンナイト", "Wyvern Knight"),
            ("JID_シーフ", "Thief"), ("JID_エンチャント", "Enchanter"), ("JID_マージカノン", "Mage Cannoneer"),
        };

        internal static List<(string id, string name)> MaleExclusiveClasses { get; } = new()
        {
            ("JID_アヴニール下級", "Noble (Alfred)"), ("JID_アヴニール", "Avenir"), ("JID_スュクセサール下級", "Lord (Diamant)"), ("JID_スュクセサール", "Successeur"),
            ("JID_ティラユール下級", "Lord (Alcryst)"), ("JID_ティラユール", "Tireur d'élite"), ("JID_クピードー下級", "Sentinel (Fogado)"), ("JID_クピードー", "Cupido"),
            ("JID_ダンサー", "Dancer"), ("JID_裏邪竜ノ子", "Fell Child (Rafal)"),
        };

        internal static List<(string id, string name)> FemaleExclusiveClasses { get; } = new()
        {
            ("JID_邪竜ノ娘", "Fell Child (Veyle)"), ("JID_フロラージュ下級", "Noble (Céline)"), ("JID_フロラージュ", "Vidame"),
            ("JID_リンドブルム下級", "Wing Tamer (Ivy)"), ("JID_リンドブルム", "Lindwurm"), ("JID_スレイプニル下級", "Wing Tamer (Hortensia)"), ("JID_スレイプニル", "Sleipnir Rider"),
            ("JID_ピッチフォーク下級", "Sentinel (Timerra)"), ("JID_ピッチフォーク", "Picket"), ("JID_ソードペガサス", "Sword Flier"), ("JID_ランスペガサス", "Lance Flier"),
            ("JID_アクスペガサス", "Axe Flier"), ("JID_裏邪竜ノ娘", "Fell Child (Nel)"), ("JID_メリュジーヌ_味方", "Melusine (Zelestia)"),
        };

        internal static List<(string id, string name)> MixedNPCExclusiveClasses { get; } = new()
        {
            ("JID_邪竜ノ子", "Fell Child (Past Alear)"), ("JID_蛮族", "Barbarian"), ("JID_村人", "Villager"),
        };

        internal static List<(string id, string name)> MaleEmblemClasses { get; } = new()
        {
            ("JID_紋章士_マルス", "Emblem (Marth)"), ("JID_紋章士_シグルド", "Emblem (Sigurd)"), ("JID_紋章士_ロイ", "Emblem (Roy)"),
            ("JID_紋章士_リーフ", "Emblem (Leif)"), ("JID_紋章士_アイク", "Emblem (Ike)"), ("JID_紋章士_ベレト", "Emblem (Byleth)"), ("JID_紋章士_ディミトリ", "Emblem (Dimitri)"),
            ("JID_紋章士_クロード", "Emblem (Claude)"), ("JID_紋章士_ヘクトル", "Emblem (Hector)"),
            ("JID_紋章士_セネリオ", "Emblem (Soren)"), ("JID_紋章士_クロム", "Emblem (Chrom)"), ("JID_紋章士_ルフレ", "Emblem (Robin)"),
        };

        internal static List<(string id, string name)> MaleNPCExclusiveClasses { get; } = new(); // MaleNonEmblemNPCExclusiveClasses + MaleEmblemClasses

        internal static List<(string id, string name)> MaleNonEmblemNPCExclusiveClasses { get; } = new()
        {
            ("JID_邪竜ノ王", "Fell Monarch"),  ("JID_裏邪竜ノ子_E1-4", "Fell Child (Xenologue 1-4 Nil)"),
            ("JID_裏邪竜ノ子_E5", "Fell Child (Xenologue 5 Nil)"), ("JID_アヴニール_E", "Royal (Alfred)"), ("JID_スュクセサール_E", "Warden (Diamant)"), ("JID_ティラユール_E", "Warden (Alcryst)"),
            ("JID_クピードー_E", "Watcher (Fogado)"), ("JID_紋章士_ヘクトル_召喚", "Emblem (Hector Summon)"),
        };

        internal static List<(string id, string name)> FemaleEmblemClasses { get; } = new()
        {
            ("JID_紋章士_セリカ", "Emblem (Celica)"),
            ("JID_紋章士_ミカヤ", "Emblem (Micaiah)"), ("JID_紋章士_ルキナ", "Emblem (Lucina)"), ("JID_紋章士_リン", "Emblem (Lyn)"), ("JID_紋章士_カムイ", "Emblem (Corrin)"),
            ("JID_紋章士_エイリーク", "Emblem (Eirika)"),
            ("JID_紋章士_エーデルガルト", "Emblem (Edelgard)"), ("JID_紋章士_チキ", "Emblem (Tiki)"), ("JID_紋章士_ヴェロニカ", "Emblem (Veronica)"),
            ("JID_紋章士_カミラ", "Emblem (Camilla)"),
        };

        internal static List<(string id, string name)> EmblemClasses { get; } = new(); // MaleEmblemClasses + FemaleEmblemClasses

        internal static List<(string id, string name)> FemaleNPCExclusiveClasses { get; } = new(); // FemaleNonEmblemNPCExclusiveClasses + FemaleEmblemClasses

        internal static List<(string id, string name)> FemaleNonEmblemNPCExclusiveClasses { get; } = new()
        {
            ("JID_M002_神竜ノ王", "Divine Dragon (Lumera)"), ("JID_邪竜ノ娘_敵", "Fell Child (Evil Veyle)"), ("JID_メリュジーヌ", "Melusine (Zephia)"),
            ("JID_フロラージュ_E", "Royal (Céline)"), ("JID_リンドブルム_E", "Trainer (Ivy)"), ("JID_スレイプニル_E", "Trainer (Hortensia)"),
            ("JID_ピッチフォーク_E", "Watcher (Timerra)"), ("JID_紋章士_ルキナ_召喚", "Emblem (Lucina Summon)"),
        };

        internal static List<(string id, string name)> NonEmblemGeneralClasses { get; } = new(); // PlayableClasses + MaleNonEmblemNPCExclusiveClasses + FemaleNonEmblemNPCExclusiveClasses

        internal static List<(string id, string name)> BeastClasses { get; } = new()
        {
            ("JID_邪竜", "Great Fell Dragon (Sombron)"), ("JID_異形竜", "Corrupted Wyrm"), ("JID_幻影竜", "Phantom Wyrm"), ("JID_E006ラスボス", "Great Fell Dragon (Nil)"),
            ("JID_異形狼", "Corrupted Wolf"), ("JID_幻影狼", "Phantom Wolf"), ("JID_異形飛竜", "Corrupted Wyvern"), ("JID_幻影飛竜", "Phantom Wyvern"),
        };

        internal static List<(string id, string name)> PlayableClasses { get; } = new(); // UniversalClasses + MaleExclusiveClasses + FemaleExclusiveClasses

        internal static List<(string id, string name)> GeneralClasses { get; } = new(); // PlayableClasses + MaleNPCExclusiveClasses + FemaleNPCExclusiveClasses

        internal static List<(string id, string name)> AllClasses { get; } = new(); // BeastClasses + GeneralClasses
        #endregion
        #region DemoAnim IDs
        internal static List<(string id, string name)> UniqueMaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_c001", "Male Alear"), ("AOC_Demo_c049", "Rafal A"),
            ("AOC_Demo_c049b", "Rafal B"), ("AOC_Demo_c100", "Alfred"),
            ("AOC_Demo_c101", "Boucheron"), ("AOC_Demo_c102", "Louis"),
            ("AOC_Demo_c103", "Jean"), ("AOC_Demo_c200", "Diamant"),
            ("AOC_Demo_c201", "Alcryst"), ("AOC_Demo_c202", "Morion"),
            ("AOC_Demo_c203", "Amber"), ("AOC_Demo_c300", "Hyacinth"),
            ("AOC_Demo_c301", "Zelkov"), ("AOC_Demo_c302", "Kagetsu"),
            ("AOC_Demo_c304", "Lindon"), ("AOC_Demo_c400", "Fogado"),
            ("AOC_Demo_c401", "Pandreo"), ("AOC_Demo_c402", "Bunet"),
            ("AOC_Demo_c403", "Seadall"), ("AOC_Demo_c500", "Vander"),
            ("AOC_Demo_c501", "Clanne"), ("AOC_Demo_c502", "Mauvier"),
            ("AOC_Demo_c503", "Griss"), ("AOC_Demo_c503b", "Gregory"),
            ("AOC_Demo_God0M", "Male Emblem"), ("AOC_Demo_c514", "Dimitri"),
            ("AOC_Demo_c515", "Claude"), ("AOC_Demo_c510", "Hector"),
            ("AOC_Demo_c511", "Soren"), ("AOC_Demo_c512", "Chrom"),
            ("AOC_Demo_c513", "Robin"),
        };

        internal static List<(string id, string name)> GenericMaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_Hum0M", "Male A"), ("AOC_Demo_Hum1M", "Male B"),
            ("AOC_Demo_Hum2M", "Male C"), ("AOC_Demo_c702", "Corrupted Male"),
            ("AOC_Demo_c809", "Old Man"),
        };

        internal static List<(string id, string name)> UniqueFemaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_c051", "Female Alear"), ("AOC_Demo_c099", "Nel"),
            ("AOC_Demo_c150", "Céline"), ("AOC_Demo_c151", "Éve"),
            ("AOC_Demo_c152", "Etie"), ("AOC_Demo_c153", "Chloé"),
            ("AOC_Demo_c250", "Jade"), ("AOC_Demo_c251", "Lapis"),
            ("AOC_Demo_c252", "Citrinne"), ("AOC_Demo_c253", "Yunaka"),
            ("AOC_Demo_c254", "Saphir"), ("AOC_Demo_c303", "Rosado"),
            ("AOC_Demo_c350", "Ivy"), ("AOC_Demo_c351", "Hortensia"),
            ("AOC_Demo_c352", "Goldmary"), ("AOC_Demo_c450", "Timerra"),
            ("AOC_Demo_c451", "Seforia"), ("AOC_Demo_c452", "Merrin"),
            ("AOC_Demo_c453", "Panette"), ("AOC_Demo_c550", "Framme"),
            ("AOC_Demo_c551", "Veyle"), ("AOC_Demo_c556", "Evil Veyle"),
            ("AOC_Demo_c552", "Anna"), ("AOC_Demo_c553", "Zephia"),
            ("AOC_Demo_c553b", "Zelestia"), ("AOC_Demo_c554", "Marni"),
            ("AOC_Demo_c554b", "Madeline"), ("AOC_Demo_c555", "Lumera"),
            ("AOC_Demo_God0F", "Female Emblem"), ("AOC_Demo_c560", "Tiki"),
            ("AOC_Demo_c563", "Edelgard"), ("AOC_Demo_c561", "Camilla"),
            ("AOC_Demo_c562", "Veronica"),
        };

        internal static List<(string id, string name)> GenericFemaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_Hum0F", "Female A"), ("AOC_Demo_Hum1F", "Female B"),
            ("AOC_Demo_Hum2F", "Female C"), ("AOC_Demo_Hum3F", "Female D"),
            ("AOC_Demo_Hum0FL", "Female E"), ("AOC_Demo_Hum1FL", "Female F"),
            ("AOC_Demo_Hum2FL", "Female G"), ("AOC_Demo_Hum3FL", "Female H"),
            ("AOC_Demo_c703", "Corrupted Female"),
        };
        #endregion
        #region Dress Model IDs
        internal static List<(string id, string name)> MaleClassDressModels { get; } = new()
        {
            ("uBody_Swd0AM_c000", "Male Sword Fighter"), ("uBody_Swd1AM_c699", "Male Swordmaster"),
            ("uBody_Swd1AM_c000", "Male Enemy Swordmaster"), ("uBody_Swd2AM_c000", "Male Hero"),
            ("uBody_Lnc0AM_c000", "Male Lance Fighter"), ("uBody_Lnc1AM_c000", "Male Halberdier"),
            ("uBody_Lnc2BM_c000", "Male Royal Knight"), ("uBody_Axe0AM_c699", "Male Axe Fighter"),
            ("uBody_Axe0AM_c000", "Male Enemy Axe Fighter"), ("uBody_Axe1AM_c699", "Male Berserker A"),
            ("uBody_Axe1AM_c699b", "Male Berserker B"), ("uBody_Axe1AM_c699c", "Male Berserker C"),
            ("uBody_Axe1AM_c699d", "Male Berserker D"), ("uBody_Axe1AM_c000", "Male Enemy Berserker"),
            ("uBody_Axe2AM_c000", "Male Warrior A"), ("uBody_Axe2AM_c000b", "Male Warrior B"),
            ("uBody_Axe2AM_c000c", "Male Warrior C"), ("uBody_Axe2AM_c000d", "Male Warrior D"),
            ("uBody_Amr0AM_c699", "Male Sword/Lance/Axe Armor"), ("uBody_Amr0AM_c000", "Male Enemy Sword/Lance/Axe Armor"),
            ("uBody_Amr1AM_c699", "Male General"), ("uBody_Amr1AM_c000", "Male Enemy General"),
            ("uBody_Amr2BM_c699", "Male Great Knight"), ("uBody_Amr2BM_c000", "Male Enemy Great Knight"),
            ("uBody_Bow0AM_c699", "Male Archer"), ("uBody_Bow0AM_c000", "Male Enemy Archer"),
            ("uBody_Bow1AM_c699", "Male Sniper"), ("uBody_Bow1AM_c000", "Male Enemy Sniper"),
            ("uBody_Bow2BM_c000", "Male Bow Knight"), ("uBody_Cav0BM_c000", "Male Sword/Lance/Axe Cavalier"),
            ("uBody_Cav1BM_c000", "Male Paladin"), ("uBody_Cav2CM_c000", "Male Wolf Knight"),
            ("uBody_Wng1FM_c000", "Male Griffin Knight"), ("uBody_Wng2DM_c000", "Male Wyvern Knight"),
            ("uBody_Dge0AM_c699", "Male Thief A"), ("uBody_Dge0AM_c699d", "Male Thief B"),
            ("uBody_Dge0AM_c000", "Male Enemy Thief"), ("uBody_Mag0AM_c000", "Male Mage"),
            ("uBody_Mag1AM_c000", "Male Sage A"), ("uBody_Mag1AM_c000b", "Male Sage B"),
            ("uBody_Mag1AM_c000l", "Male Sage C"), ("uBody_Mag1AM_c000c", "Male Sage D"),
            ("uBody_Mag1AM_c000d", "Male Sage E"), ("uBody_Mag1AM_c699", "Male Enemy Sage"),
            ("uBody_Mag2BM_c000", "Male Mage Knight"), ("uBody_Rod0AM_c000", "Male Martial Monk"),
            ("uBody_Rod1AM_c000", "Male Martial Master A"), ("uBody_Rod1AM_c000b", "Male Martial Master B"),
            ("uBody_Rod1AM_c000c", "Male Martial Master C"), ("uBody_Rod1AM_c000d", "Male Martial Master D"),
            ("uBody_Rod2AM_c000", "Male High Priest"), ("uBody_Bbr0AM_c000", "Male Barbarian"),
            ("uBody_Ect3AM_c000", "Male Enchanter"), ("uBody_Mcn3AM_c000", "Male Mage Cannoneer"),
            ("uBody_File4M_c809", "Male Firenese Villager"),
        };

        internal static List<(string id, string name)> MaleCorruptedClassDressModels { get; } = new()
        {
            ("uBody_Swd0AM_c702", "Male Corrupted Sword Fighter"), ("uBody_Swd1AM_c704", "Male Corrupted Swordmaster"),
            ("uBody_Swd2AM_c704", "Male Corrupted Hero"), ("uBody_Lnc0AM_c702", "Male Corrupted Lance Fighter"),
            ("uBody_Lnc1AM_c704", "Male Corrupted Halberdier"), ("uBody_Lnc2BM_c704", "Male Corrupted Royal Knight"),
            ("uBody_Axe0AM_c702", "Male Corrupted Axe Fighter"), ("uBody_Axe1AM_c704", "Male Corrupted Berserker"),
            ("uBody_Axe2AM_c704", "Male Corrupted Warrior"), ("uBody_Amr0AM_c702", "Male Corrupted Sword/Lance/Axe Armor"),
            ("uBody_Amr1AM_c704", "Male Corrupted General"), ("uBody_Amr2BM_c704", "Male Corrupted Great Knight"),
            ("uBody_Bow0AM_c702", "Male Corrupted Archer"), ("uBody_Bow1AM_c704", "Male Corrupted Sniper"),
            ("uBody_Bow2BM_c704", "Male Corrupted Bow Knight"), ("uBody_Cav0BM_c702", "Male Corrupted Sword/Lance/Axe Cavalier"),
            ("uBody_Cav1BM_c704", "Male Corrupted Paladin"), ("uBody_Cav2CM_c704", "Male Corrupted Wolf Knight"),
            ("uBody_Wng1FM_c704", "Male Corrupted Griffin Knight"), ("uBody_Wng2DM_c704", "Male Corrupted Wyvern Knight"),
            ("uBody_Dge0AM_c702", "Male Corrupted Thief"), ("uBody_Mag0AM_c702", "Male Corrupted Mage"),
            ("uBody_Mag1AM_c704", "Male Corrupted Sage"), ("uBody_Mag2BM_c704", "Male Corrupted Mage Knight"),
            ("uBody_Rod0AM_c702", "Male Corrupted Martial Monk"), ("uBody_Rod1AM_c704", "Male Corrupted Marial Master"),
            ("uBody_Rod2AM_c704", "Male Corrupted High Priest"), ("uBody_Bbr0AM_c702", "Male Corrupted Barbarian"),
            ("uBody_Ect3AM_c704", "Male Corrupted Enchanter"), ("uBody_Mcn3AM_c704", "Male Corrupted Mage Cannoneer"),
        };

        internal static List<(string id, string name)> FemaleClassDressModels { get; } = new()
        {
            ("uBody_Swd0AF_c699", "Female Sword Fighter"), ("uBody_Swd0AF_c000", "Female Enemy Sword Fighter"),
            ("uBody_Swd1AF_c699", "Female Swordmaster"), ("uBody_Swd1AF_c000", "Female Enemy Swordmaster"),
            ("uBody_Swd2AF_c000", "Female Hero"), ("uBody_Lnc0AF_c000", "Female Lance Fighter"),
            ("uBody_Lnc1AF_c000", "Female Halberdier"), ("uBody_Lnc2BF_c000", "Female Royal Knight"),
            ("uBody_Axe0AF_c699", "Female Axe Fighter"), ("uBody_Axe0AF_c000", "Female Enemy Axe Fighter"),
            ("uBody_Axe1AF_c699", "Female Berserker A"), ("uBody_Axe1AF_c699b", "Female Berserker B"),
            ("uBody_Axe1AF_c699c", "Female Berserker C"), ("uBody_Axe1AF_c699d", "Female Berserker D"),
            ("uBody_Axe1AF_c000", "Female Enemy Berserker"), ("uBody_Axe2AF_c000", "Female Warrior A"),
            ("uBody_Axe2AF_c000b", "Female Warrior B"), ("uBody_Axe2AF_c000c", "Female Warrior C"),
            ("uBody_Axe2AF_c000d", "Female Warrior D"), ("uBody_Amr0AF_c699", "Female Sword/Lance/Axe Armor"),
            ("uBody_Amr0AF_c000", "Female Enemy Sword/Lance/Axe Armor"), ("uBody_Amr1AF_c699", "Female General"),
            ("uBody_Amr1AF_c000", "Female Enemy General"), ("uBody_Amr2BF_c699", "Female Great Knight"),
            ("uBody_Amr2BF_c000", "Female Enemy Great Knight"), ("uBody_Bow0AF_c699", "Female Archer"),
            ("uBody_Bow0AF_c000", "Female Enemy Archer"), ("uBody_Bow1AF_c699", "Female Sniper"),
            ("uBody_Bow1AF_c000", "Female Enemy Sniper"), ("uBody_Bow2BF_c000", "Female Bow Knight"),
            ("uBody_Cav0BF_c000", "Female Sword/Lance/Axe Cavalier"), ("uBody_Cav1BF_c000", "Female Paladin"),
            ("uBody_Cav2CF_c000", "Female Wolf Knight"), ("uBody_Wng0EF_c000", "Female Sword/Lance/Axe Flier"),
            ("uBody_Wng1FF_c000", "Female Griffin Knight"), ("uBody_Wng2DF_c000", "Female Wyvern Knight"),
            ("uBody_Dge0AF_c699", "Female Thief A"), ("uBody_Dge0AF_c699d", "Female Thief B"),
            ("uBody_Dge0AF_c000", "Female Enemy Thief"), ("uBody_Mag0AF_c000", "Female Mage A"),
            ("uBody_Mag0AF_c000l", "Female Mage B"), ("uBody_Mag1AF_c000", "Female Sage A"),
            ("uBody_Mag1AF_c000l", "Female Sage B"), ("uBody_Mag1AF_c000b", "Female Sage C"),
            ("uBody_Mag1AF_c000c", "Female Sage D"), ("uBody_Mag1AF_c000d", "Female Sage E"),
            ("uBody_Mag1AF_c699", "Female Enemy Sage"), ("uBody_Mag2BF_c000", "Female Mage Knight A"),
            ("uBody_Mag2BF_c000l", "Female Mage Knight B"), ("uBody_Rod0AF_c000", "Female Martial Monk"),
            ("uBody_Rod1AF_c000", "Female Martial Master"), ("uBody_Rod2AF_c000", "Female High Priest"),
            ("uBody_Bbr0AF_c000", "Female Barbarian"), ("uBody_Ect3AF_c000", "Female Enchanter"),
            ("uBody_Mcn3AF_c000", "Female Mage Cannoneer"),
        };

        internal static List<(string id, string name)> FemaleCorruptedClassDressModels { get; } = new()
        {
            ("uBody_Swd0AF_c703", "Female Corrupted Sword Fighter"), ("uBody_Swd1AF_c705", "Female Corrupted Swordmaster"),
            ("uBody_Swd2AF_c705", "Female Corrupted Hero"), ("uBody_Lnc0AF_c703", "Female Corrupted Lance Fighter"),
            ("uBody_Lnc1AF_c705", "Female Corrupted Halberdier"), ("uBody_Lnc2BF_c705", "Female Corrupted Royal Knight"),
            ("uBody_Axe0AF_c703", "Female Corrupted Axe Fighter"), ("uBody_Axe1AF_c705", "Female Corrupted Berserker"),
            ("uBody_Axe2AF_c705", "Female Corrupted Warrior"), ("uBody_Amr0AF_c703", "Female Corrupted Sword/Lance/Axe Armor"),
            ("uBody_Amr1AF_c705", "Female Corrupted General"), ("uBody_Amr2BF_c705", "Female Corrupted Great Knight"),
            ("uBody_Bow0AF_c703", "Female Corrupted Archer"), ("uBody_Bow1AF_c705", "Female Corrupted Sniper"),
            ("uBody_Bow2BF_c705", "Female Corrupted Bow Knight"), ("uBody_Cav0BF_c703", "Female Corrupted Sword/Lance/Axe Cavalier"),
            ("uBody_Cav1BF_c705", "Female Corrupted Paladin"), ("uBody_Cav2CF_c705", "Female Corrupted Wolf Knight"),
            ("uBody_Wng0EF_c703", "Female Corrupted Sword/Lance/Axe Flier"), ("uBody_Wng1FF_c705", "Female Corrupted Griffin Knight"),
            ("uBody_Wng2DF_c705", "Female Corrupted Wyvern Knight"), ("uBody_Dge0AF_c703", "Female Corrupted Thief"),
            ("uBody_Mag0AF_c703", "Female Corrupted Mage"), ("uBody_Mag1AF_c705", "Female Corrupted Sage"),
            ("uBody_Mag2BF_c705", "Female Corrupted Mage Knight"), ("uBody_Rod0AF_c703", "Female Corrupted Marital Monk"),
            ("uBody_Rod1AF_c705", "Female Corrupted Martial Master"), ("uBody_Rod2AF_c705", "Female Corrupted High Priest"),
            ("uBody_Bbr0AF_c703", "Female Corrupted Barbarian"), ("uBody_Ect3AF_c705", "Female Corrupted Enchanter"),
            ("uBody_Mcn3AF_c705", "Female Corrupted Mage Cannoneer"),
        };

        internal static List<(string id, string name)> MalePersonalDressModels { get; } = new()
        {
            ("uBody_Drg0AM_c001", "Male Dragon Child"), ("uBody_Drg1AM_c001", "Male Divine Dragon (Alear)"),
            ("uBody_Drg0AM_c002", "Male Fell Child (Past Alear)"), ("uBody_Sds0AM_c049", "Fell Child (Rafal)"),
            ("uBody_Avn0BM_c100", "Noble (Alfred)"), ("uBody_Avn1BM_c100", "Avenir"),
            ("uBody_Axe0AM_c101", "Axe Fighter (Boucheron)"), ("uBody_Amr0AM_c102", "Sword/Lance/Axe Armor (Louis)"),
            ("uBody_Rod0AM_c103", "Martial Monk (Jean)"), ("uBody_Scs0AM_c200", "Lord (Diamant)"),
            ("uBody_Scs1AM_c200", "Successeur"), ("uBody_Trl0AM_c201", "Lord (Alcryst)"),
            ("uBody_Trl1AM_c201", "Tireur d'élite"), ("uBody_Swd2AM_c202", "Morion"),
            ("uBody_Cav0BM_c203", "Sword/Lance/Axe Cavalier (Amber)"), ("uBody_Rod1AM_c300", "Hyacinth"),
            ("uBody_Dge0AM_c301", "Thief (Zelkov)"), ("uBody_Swd1AM_c302", "Swordmaster (Kagetsu)"),
            ("uBody_Mag1AM_c304", "Sage (Lindon)"), ("uBody_Cpd0BM_c400", "Sentinel (Fogado)"),
            ("uBody_Cpd1BM_c400", "Cupido"), ("uBody_Mag0AM_c400", "Mage (Fogado)"),
            ("uBody_Dct0AM_c400", "Wolf Knight (Fogado)"), ("uBody_Rod2AM_c401", "High Priest (Pandreo)"),
            ("uBody_Amr2BM_c402", "Great Knight (Bunet)"), ("uBody_Dnc0AM_c403", "Dancer"),
            ("uBody_Cav1BM_c500", "Paladin (Vander)"), ("uBody_Mag0AM_c501", "Mage (Clanne)"),
            ("uBody_Lnc2BM_c502", "Royal Knight (Mauvier)"), ("uBody_Mag1AM_c503", "Sage (Griss)"),
            ("uBody_Mag1AM_c503b", "Sage (Gregory)"), ("uBody_Amr0AM_c811", "Rodine"),
            ("uBody_Axe0AM_c812", "Nelucce"),
            ("uBody_Bbr0AM_c813", "Tetchie"), ("uBody_Bbr0AM_c814", "Totchie"),
            ("uBody_Rod0AM_c819", "Sean"), ("uBody_File4M_c817", "Durthon"),
            ("uBody_Brod4M_c818", "Pinet"), ("uBody_WearM_c001", "Male Alear Casual"),
            ("uBody_WearM_c100", "Alfred Casual"), ("uBody_WearM_c101", "Boucheron Casual"),
            ("uBody_WearM_c102", "Louis Casual"), ("uBody_WearM_c103", "Jean Casual"),
            ("uBody_WearM_c200", "Diamant Casual"), ("uBody_WearM_c201", "Alcryst Casual"),
            ("uBody_WearM_c203", "Amber Casual"), ("uBody_WearM_c301", "Zelkov Casual"),
            ("uBody_WearM_c302", "Kagetsu Casual"), ("uBody_WearM_c304", "Lindon Casual"),
            ("uBody_WearM_c400", "Fogado Casual"), ("uBody_WearM_c401", "Pandreo Casual"),
            ("uBody_WearM_c402", "Bunet Casual"), ("uBody_WearM_c403", "Seadall Casual"),
            ("uBody_WearM_c500", "Vander Casual"), ("uBody_WearM_c501", "Clanne Casual"),
            ("uBody_WearM_c502", "Mauvier Casual"), ("uBody_WearM_c049", "Rafal Casual"),
            ("uBody_WearM_c503", "Gregory Casual"),
        };

        internal static List<(string id, string name)> FemalePersonalDressModels { get; } = new()
        {
            ("uBody_Drg0AF_c051", "Female Dragon Child"),
            ("uBody_Drg1AF_c051", "Female Divine Dragon (Alear)"), ("uBody_Drg0AF_c052", "Female Fell Child (Past Alear)"),
            ("uBody_Sds0AF_c099", "Fell Child (Nel)"), ("uBody_Flr0AF_c150", "Noble (Céline)"),
            ("uBody_Flr1AF_c150", "Vidame"), ("uBody_Rod2AF_c151", "Éve"),
            ("uBody_Bow0AF_c152", "Archer (Etie)"), ("uBody_Wng0EF_c153", "Sword/Lance/Axe Flier (Chloé)"),
            ("uBody_Amr0AF_c250", "Sword/Lance/Axe Armor (Jade)"), ("uBody_Swd0AF_c251", "Sword Fighter (Lapis)"),
            ("uBody_Mag0AF_c252", "Mage (Citrinne)"), ("uBody_Dge0AF_c253", "Thief (Yunaka)"),
            ("uBody_Axe2AF_c254", "Warrior (Saphir)"), ("uBody_Wng2DF_c303", "Wyvern Knight (Rosado)"),
            ("uBody_Lnd0DF_c350", "Wing Tamer (Ivy)"), ("uBody_Lnd1DF_c350", "Lindwurm"),
            ("uBody_Slp0EF_c351", "Wing Tamer (Hortensia)"), ("uBody_Slp1EF_c351", "Sleipnir Rider"),
            ("uBody_Swd2AF_c352", "Hero (Goldmary)"), ("uBody_Pcf0AF_c450", "Sentinel (Timerra)"),
            ("uBody_Pcf1AF_c450", "Picket"), ("uBody_Cav2CF_c450", "Wolf Knight (Timerra)"),
            ("uBody_Swd2AF_c450", "Hero (Timerra)"), ("uBody_Wng2DF_c451", "Seforia"),
            ("uBody_Cav2CF_c452", "Wolf Knight (Merrin)"), ("uBody_Axe1AF_c453", "Berserker (Panette)"),
            ("uBody_Rod0AF_c550", "Martial Monk (Framme)"), ("uBody_Sdp0AF_c551", "Fell Child (Veyle)"),
            ("uBody_Sdp0AF_c556", "Dark Veyle"), ("uBody_Sdp0AF_c557", "Hooded Veyle"),
            ("uBody_Axe0AF_c552", "Axe Fighter (Anna)"), ("uBody_Msn0DF_c553", "Melusine (Zephia)"),
            ("uBody_Msn0DF_c553b", "Melusine (Zelestia)"), ("uBody_Amr1AF_c554", "General (Marni)"),
            ("uBody_Amr1AF_c554b", "General (Madeline)"), ("uBody_Drg1AF_c555", "Divine Dragon (Lumera)"),
            ("uBody_Axe1AF_c855", "Abyme (Chapter 18)"), ("uBody_Bbr0AF_c859", "Mitan"),
            ("uBody_File4F_c858", "Anje"), ("uBody_File4F_c856", "Anisse"),
            ("uBody_Brod4F_c857", "Calney"), ("uBody_WearF_c303", "Rosado Casual"),
            ("uBody_WearF_c051", "Female Alear Casual"), ("uBody_WearF_c150", "Céline Casual"),
            ("uBody_WearF_c152", "Etie Casual"), ("uBody_WearF_c153", "Chloé Casual"),
            ("uBody_WearF_c250", "Jade Casual"), ("uBody_WearF_c251", "Lapis Casual"),
            ("uBody_WearF_c252", "Citrinne Casual"), ("uBody_WearF_c253", "Yunaka Casual"),
            ("uBody_WearF_c254", "Saphir Casual"), ("uBody_WearF_c350", "Ivy Casual"),
            ("uBody_WearF_c351", "Hortensia Casual"), ("uBody_WearF_c352", "Goldmary Casual"),
            ("uBody_WearF_c450", "Timerra Casual"), ("uBody_WearF_c452", "Merrin Casual"),
            ("uBody_WearF_c453", "Panette Casual"), ("uBody_WearF_c550", "Framme Casual"),
            ("uBody_WearF_c551", "Veyle Casual"), ("uBody_WearF_c552", "Anna Casual"),
            ("uBody_WearF_c099", "Nel Casual"), ("uBody_WearF_c553", "Zelestia Casual"),
            ("uBody_WearF_c554", "Madeline Casual"),
        };

        internal static List<(string id, string name)> MaleEmblemDressModels { get; } = new()
        {
            ("uBody_Mar0AM_c530", "Marth"), ("uBody_Mar0AM_c537", "Corrupted Marth"),
            ("uBody_Sig0BM_c531", "Sigurd"), ("uBody_Sig0BM_c538", "Corrupted Sigurd"),
            ("uBody_Lei0AM_c532", "Leif"), ("uBody_Lei0AM_c539", "Corrupted Leif"),
            ("uBody_Roy0AM_c533", "Roy"), ("uBody_Roy0AM_c540", "Corrupted Roy"),
            ("uBody_Ike0AM_c534", "Ike"), ("uBody_Ike0AM_c541", "Corrupted Ike"),
            ("uBody_Byl0AM_c535", "Byleth"), ("uBody_Byl0AM_c542", "Corrupted Byleth"),
            ("uBody_Eph0AM_c536", "Ephraim"), ("uBody_Eph0AM_c543", "Corrupted Ephraim"),
            ("uBody_Drg0AM_c003", "Male Emblem Alear"), ("uBody_Dim0AM_c514", "Dimitri"),
            ("uBody_Dim0AM_c521", "Corrupted Dimitri"), ("uBody_Cla0AM_c515", "Claude"),
            ("uBody_Cla0AM_c522", "Corrupted Claude"), ("uBody_Hec0AM_c510", "Hector"),
            ("uBody_Hec0AM_c517", "Corrupted Hector"), ("uBody_Sor0AM_c511", "Soren"),
            ("uBody_Sor0AM_c518", "Corrupted Soren"), ("uBody_Chr0AM_c512", "Chrom"),
            ("uBody_Chr0AM_c519", "Corrupted Chrom"), ("uBody_Rbi0AM_c513", "Robin"),
            ("uBody_Rbi0AM_c520", "Corrupted Robin"),
        };

        internal static List<(string id, string name)> FemaleEmblemDressModels { get; } = new()
        {
            ("uBody_Cel0AF_c580", "Celica"), ("uBody_Cel0AF_c587", "Corrupted Celica"),
            ("uBody_Lyn0AF_c581", "Lyn"), ("uBody_Lyn0AF_c588", "Corrupted Lyn"),
            ("uBody_Eir0AF_c582", "Eirika"), ("uBody_Eir0AF_c589", "Corrupted Eirika"),
            ("uBody_Mic0AF_c583", "Micaiah"), ("uBody_Mic0AF_c590", "Corrupted Micaiah"),
            ("uBody_Luc0AF_c584", "Lucina"), ("uBody_Luc0AF_c591", "Corrupted Lucina"),
            ("uBody_Cor0AF_c585", "Corrin"), ("uBody_Cor0AF_c592", "Corrupted Corrin"),
            ("uBody_Drg0AF_c053", "Female Emblem Alear"), ("uBody_Tik0AF_c560", "Tiki"),
            ("uBody_Tik0AF_c567", "Corrupted Tiki"), ("uBody_Ede0AF_c563", "Edelgard"),
            ("uBody_Ede0AF_c570", "Corrupted Edelgard"), ("uBody_Cmi0DF_c561", "Camilla"),
            ("uBody_Cmi0DF_c568", "Corrupted Camilla"), ("uBody_Ver0AF_c562", "Veronica"),
            ("uBody_Ver0AF_c569", "Corrupted Veronica"),
        };

        internal static List<(string id, string name)> MaleEngageDressModels { get; } = new()
        {
            ("uBody_Mar1AM_c000", "Male Engaged (Marth)"), ("uBody_Sig1AM_c000", "Male Engaged (Sigurd)"),
            ("uBody_Lei1AM_c000", "Male Engaged (Leif)"), ("uBody_Roy1AM_c000", "Male Engaged (Roy)"),
            ("uBody_Ike1AM_c000", "Male Engaged (Ike)"), ("uBody_Byl1AM_c000", "Male Engaged (Byleth)"),
            ("uBody_Cel1AM_c000", "Male Engaged (Celica)"), ("uBody_Lyn1AM_c000", "Male Engaged (Lyn)"),
            ("uBody_Eir1AM_c000", "Male Engaged (Eirika)"), ("uBody_Mic1AM_c000", "Male Engaged (Micaiah)"),
            ("uBody_Luc1AM_c000", "Male Engaged (Lucina)"), ("uBody_Cor1AM_c000", "Male Engaged (Corrin)"),
            ("uBody_Ler1AM_c000", "Male Engaged (Alear)"),
            ("uBody_Thr1AM_c000", "Male Engaged (Edelgard)"), ("uBody_Hec1AM_c000", "Male Engaged (Hector)"),
            ("uBody_Sor1AM_c000", "Male Engaged (Soren)"), ("uBody_Cmi1AM_c000", "Male Engaged (Camilla)"),
            ("uBody_Ver1AM_c000", "Male Engaged (Veronica)"), ("uBody_Chr1AM_c000", "Male Engaged (Chrom)"),
        };

        internal static List<(string id, string name)> FemaleEngageDressModels { get; } = new()
        {
            ("uBody_Mar1AF_c000", "Female Engaged (Marth)"), ("uBody_Sig1AF_c000", "Female Engaged (Sigurd)"),
            ("uBody_Lei1AF_c000", "Female Engaged (Leif)"), ("uBody_Roy1AF_c000", "Female Engaged (Roy)"),
            ("uBody_Ike1AF_c000", "Female Engaged (Ike)"), ("uBody_Byl1AF_c000", "Female Engaged (Byleth)"),
            ("uBody_Cel1AF_c000", "Female Engaged (Celica)"), ("uBody_Lyn1AF_c000", "Female Engaged (Lyn)"),
            ("uBody_Eir1AF_c000", "Female Engaged (Eirika)"), ("uBody_Mic1AF_c000", "Female Engaged (Micaiah)"),
            ("uBody_Luc1AF_c000", "Female Engaged (Lucina)"), ("uBody_Cor1AF_c000", "Female Engaged (Corrin)"),
            ("uBody_Ler1AF_c000", "Female Engaged (Alear)"),
            ("uBody_Thr1AF_c000", "Female Engaged (Edelgard)"), ("uBody_Hec1AF_c000", "Female Engaged (Hector)"),
            ("uBody_Sor1AF_c000", "Female Engaged (Soren)"), ("uBody_Cmi1AF_c000", "Female Engaged (Camilla)"),
            ("uBody_Ver1AF_c000", "Female Engaged (Veronica)"), ("uBody_Chr1AF_c000", "Female Engaged (Chrom)"),
        };

        internal static List<(string id, string name)> MaleCommonDressModels { get; } = new()
        {
            ("uBody_File1M_c000", "Male Firene Formal 1"), ("uBody_File2M_c000", "Male Firene Formal 2"),
            ("uBody_File3M_c000", "Male Firene Formal 3"), ("uBody_File4M_c000", "Male Firene Casual 1"),
            ("uBody_File5M_c000", "Male Firene Casual 2"), ("uBody_File6M_c000", "Male Firene Casual 3"),
            ("uBody_Brod1M_c000", "Male Brodia Formal 1"), ("uBody_Brod2M_c000", "Male Brodia Formal 2"),
            ("uBody_Brod3M_c000", "Male Brodia Formal 3"), ("uBody_Brod4M_c000", "Male Brodia Casual 1"),
            ("uBody_Brod5M_c000", "Male Brodia Casual 2"), ("uBody_Brod6M_c000", "Male Brodia Casual 3"),
            ("uBody_Irci1M_c000", "Male Elusia Formal 1"), ("uBody_Irci2M_c000", "Male Elusia Formal 2"),
            ("uBody_Irci3M_c000", "Male Elusia Formal 3"), ("uBody_Irci4M_c000", "Male Elusia Casual 1"),
            ("uBody_Irci5M_c000", "Male Elusia Casual 2"), ("uBody_Irci6M_c000", "Male Elusia Casual 3"),
            ("uBody_Solu1M_c000", "Male Solm Formal 1"), ("uBody_Solu2M_c000", "Male Solm Formal 2"),
            ("uBody_Solu3M_c000", "Male Solm Formal 3"), ("uBody_Solu4M_c000", "Male Solm Casual 1"),
            ("uBody_Solu5M_c000", "Male Solm Casual 2"), ("uBody_Solu6M_c000", "Male Solm Casual 3"),
            ("uBody_Lith1M_c000", "Male Lythos 1"), ("uBody_Lith2M_c000", "Male Lythos 2"),
            ("uBody_Lith3M_c000", "Male Lythos 3"), ("uBody_SwimM1_c000", "Male Swimwear 1A"),
            ("uBody_SwimM1_c000b", "Male Swimwear 1B"), ("uBody_SwimM1_c000c", "Male Swimwear 1C"),
            ("uBody_SwimM1_c000d", "Male Swimwear 1D"), ("uBody_SwimM2_c000", "Male Swimwear 2A"),
            ("uBody_SwimM2_c000b", "Male Swimwear 2B"), ("uBody_SwimM2_c000c", "Male Swimwear 2C"),
            ("uBody_SwimM2_c000d", "Male Swimwear 2D"), ("uBody_SwimM3_c000", "Male Swimwear 3A"),
            ("uBody_SwimM3_c000b", "Male Swimwear 3B"), ("uBody_SwimM3_c000c", "Male Swimwear 3C"),
            ("uBody_SwimM3_c000d", "Male Swimwear 3D"), ("uBody_ExerM1_c000", "Male Training Wear A"),
            ("uBody_ExerM1_c000c", "Male Training Wear B"), ("uBody_ExerM1_c000d", "Male Training Wear C"),
            ("uBody_KimnM_c000", "Male Kimono"), ("uBody_CstmM_c000", "Male Sommie Costume A"),
            ("uBody_CstmM_c699", "Male Sommie Costume B"), ("uBody_Mar0AM_c000", "Marth Costume"),
            ("uBody_Sig0BM_c000", "Sigurd Costume"), ("uBody_Roy0AM_c000", "Roy Costume"),
            ("uBody_Ike0AM_c000", "Ike Costume A"), ("uBody_Ike0AM_c000b", "Ike Costume B"),
            ("uBody_Lei0AM_c000", "Leif Costume"), ("uBody_Byl0AM_c000", "Byleth Costume"),
        };

        internal static List<(string id, string name)> FemaleCommonDressModels { get; } = new()
        {
            ("uBody_File1F_c000", "Female Firene Formal 1"), ("uBody_File2F_c000", "Female Firene Formal 2"),
            ("uBody_File3F_c000", "Female Firene Formal 3"), ("uBody_File4F_c000", "Female Firene Casual 1"),
            ("uBody_File5F_c000", "Female Firene Casual 2"), ("uBody_File6F_c000", "Female Firene Casual 3"),
            ("uBody_Brod1F_c000", "Female Brodia Formal 1"), ("uBody_Brod2F_c000", "Female Brodia Formal 2"),
            ("uBody_Brod3F_c000", "Female Brodia Formal 3"), ("uBody_Brod4F_c000", "Female Brodia Casual 1"),
            ("uBody_Brod5F_c000", "Female Brodia Casual 2"), ("uBody_Brod6F_c000", "Female Brodia Casual 3"),
            ("uBody_Irci1F_c000", "Female Elusia Formal 1"), ("uBody_Irci2F_c000", "Female Elusia Formal 2"),
            ("uBody_Irci3F_c000", "Female Elusia Formal 3"), ("uBody_Irci4F_c000", "Female Elusia Casual 1"),
            ("uBody_Irci5F_c000", "Female Elusia Casual 2"), ("uBody_Irci6F_c000", "Female Elusia Casual 3"),
            ("uBody_Solu1F_c000", "Female Solm Formal 1"), ("uBody_Solu2F_c000", "Female Solm Formal 2"),
            ("uBody_Solu3F_c000", "Female Solm Formal 3"), ("uBody_Solu4F_c000", "Female Solm Casual 1"),
            ("uBody_Solu5F_c000", "Female Solm Casual 2"), ("uBody_Solu6F_c000", "Female Solm Casual 3"),
            ("uBody_Lith1F_c000", "Female Lythos 1"), ("uBody_Lith2F_c000", "Female Lythos 2"),
            ("uBody_Lith3F_c000", "Female Lythos 3"), ("uBody_SwimF1_c000", "Female Swimwear 1A"),
            ("uBody_SwimF1_c000b", "Female Swimwear 1B"), ("uBody_SwimF1_c000c", "Female Swimwear 1C"),
            ("uBody_SwimF1_c000d", "Female Swimwear 1D"), ("uBody_SwimF2_c000", "Female Swimwear 2A"),
            ("uBody_SwimF2_c000b", "Female Swimwear 2B"), ("uBody_SwimF2_c000c", "Female Swimwear 2C"),
            ("uBody_SwimF2_c000d", "Female Swimwear 2D"), ("uBody_SwimF3_c000", "Female Swimwear 3A"),
            ("uBody_SwimF3_c000b", "Female Swimwear 3B"), ("uBody_SwimF3_c000c", "Female Swimwear 3C"),
            ("uBody_SwimF3_c000d", "Female Swimwear 3D"), ("uBody_ExerF1_c000", "Female Training Wear A"),
            ("uBody_ExerF1_c000c", "Female Training Wear B"), ("uBody_KimnF_c000", "Female Kimono"),
            ("uBody_CstmF_c000", "Female Sommie Costume A"), ("uBody_CstmF_c699", "Female Sommie Costume B"),
            ("uBody_Cel0AF_c000", "Celica Costume"), ("uBody_Mic0AF_c000", "Micaiah Costume"),
            ("uBody_Lyn0AF_c000", "Lyn Costume"), ("uBody_Luc0AF_c000", "Lucina Costume"),
            ("uBody_Cor0AF_c000", "Corrin Costume"), ("uBody_Eir0AF_c000", "Eirika Costume"),
        };

        internal static List<(string id, string name)> AllDressModels { get; } = new();
        #endregion
        #region Emblem IDs
        internal static List<(string id, string name)> AlearEmblems { get; } = new()
        {
            ("GID_リュール", "Emblem Alear")
        };

        internal static List<(string id, string name)> LinkableEmblems { get; } = new()
        {
            ("GID_マルス", "Marth"), ("GID_シグルド", "Sigurd"), ("GID_セリカ", "Celica"), ("GID_ミカヤ", "Micaiah"),
            ("GID_ロイ", "Roy"), ("GID_リーフ", "Leif"), ("GID_ルキナ", "Lucina"), ("GID_リン", "Lyn"),
            ("GID_アイク", "Ike"), ("GID_ベレト", "Byleth"), ("GID_カムイ", "Corrin"), ("GID_エイリーク", "Eirika"),
            ("GID_エーデルガルト", "Edelgard"), ("GID_チキ", "Tiki"), ("GID_ヘクトル", "Hector"), ("GID_ヴェロニカ", "Veronica"),
            ("GID_セネリオ", "Soren"), ("GID_カミラ", "Camilla"), ("GID_クロム", "Chrom")
        };

        internal static List<(string id, string name)> AllyEngageableEmblems { get; } = new(); // AlearEmblem + LinkableEmblems

        internal static List<(string id, string name)> AllySyncableEmblems { get; } = new() // AllyEngageableEmblems +
        {
            ("GID_エフラム", "Ephraim"), ("GID_ディミトリ", "Dimitri"), ("GID_クロード", "Claude")
        };

        internal static List<(string id, string name)> EnemyEngageableEmblems { get; } = new()
        {
            ("GID_M002_シグルド", "Sigurd (Chapter 2)"), ("GID_M007_敵ルキナ", "Corrupted Lucina"),
            ("GID_M008_敵リーフ", "Corrupted Leif (Chapter 8)"),
            ("GID_M010_敵ベレト", "Corrupted Byleth (Chapter 10)"), ("GID_M010_敵リン", "Corrupted Lyn"),
            ("GID_M011_敵マルス", "Corrupted Marth (Chapter 11)"), ("GID_M011_敵シグルド", "Corrupted Sigurd (Chapter 11)"),
            ("GID_M011_敵セリカ", "Corrupted Celica (Chapter 11)"), ("GID_M011_敵ミカヤ", "Corrupted Micaiah (Chapter 11)"),
            ("GID_M011_敵ロイ", "Corrupted Roy (Chapter 11)"), ("GID_M011_敵リーフ", "Corrupted Leif (Chapter 11)"),
            ("GID_M014_敵ベレト", "Corrupted Byleth (Chapter 14)"), ("GID_M017_敵マルス", "Corrupted Marth (Chapter 17)"),
            ("GID_M017_敵シグルド", "Corrupted Sigurd (Chapter 17)"), ("GID_M017_敵セリカ", "Corrupted Celica (Chapter 17)"),
            ("GID_M017_敵ミカヤ", "Corrupted Micaiah (Chapter 17)"), ("GID_M017_敵ロイ", "Corrupted Roy (Chapter 17)"),
            ("GID_M017_敵リーフ", "Corrupter Leif (Chapter 17)"), ("GID_M019_敵ミカヤ", "Corrupted Micaiah (Chapter 19)"),
            ("GID_M019_敵ロイ", "Corrupted Roy (Chapter 19)"), ("GID_M020_敵セリカ", "Corrupted Celica (Chapter 20)"),
            ("GID_M021_敵マルス", "Corrupted Marth (Chapter 21)"), ("GID_M024_敵マルス", "Corrupted Marth (Chapter 24)"),
            ("GID_M026_敵メディウス", "Medeus"), ("GID_M026_敵ロプトウス", "Loptous"),
            ("GID_M026_敵ドーマ", "Duma"), ("GID_M026_敵ベルド", "Veld"),
            ("GID_M026_敵イドゥン", "Idunn"), ("GID_M026_敵ネルガル", "Nergal"),
            ("GID_M026_敵フォデス", "Fomortiis"), ("GID_M026_敵アシュナード", "Ashnard"),
            ("GID_M026_敵アスタルテ", "Ashera"), ("GID_M026_敵ギムレー", "Grima"),
            ("GID_M026_敵ハイドラ", "Anankos"), ("GID_M026_敵ネメシス", "Nemesis"),
            ("GID_E001_敵チキ", "Corrupted Tiki (Xenologue 1)"), ("GID_E002_敵ヘクトル", "Corrupted Hector (Xenologue 2)"),
            ("GID_E003_敵ヴェロニカ", "Corrupted Veronica (Xenologue 3)"), ("GID_E004_敵セネリオ", "Corrupted Soren (Xenologue 4)"),
            ("GID_E004_敵カミラ", "Corrupted Camilla (Xenologue 4)"), ("GID_E005_敵クロム", "Corrupted Chrom (Xenologue 5)"),
            ("GID_E005_敵ヘクトル", "Corrupted Hector (Xenologue 5)"), ("GID_E005_敵ヴェロニカ", "Corrupted Veronica (Xenologue 5)"),
            ("GID_E006_敵チキ", "Corrupted Tiki (Xenologue 6)"), ("GID_E006_敵ヘクトル", "Corrupted Hector (Xenologue 6)"),
            ("GID_E006_敵ヴェロニカ", "Corrupted Veronica (Xenologue 6)"), ("GID_E006_敵セネリオ", "Corrupted Soren (Xenologue 6)"),
            ("GID_E006_敵カミラ", "Corrupted Camilla (Xenologue 6)"), ("GID_E006_敵クロム", "Corrupted Chrom (Xenologue 6)"),
            ("GID_E006_敵エーデルガルト", "Corrupted Edelgard")
        };

        internal static List<(string id, string name)> EnemySyncableEmblems { get; } = new() // EnemyEngageableEmblems + 
        {
            ("GID_E006_敵ディミトリ", "Corrupted Dimitri"), ("GID_E006_敵クロード", "Corrupted Claude")
        };

        internal static List<(string id, string name)> EngageableEmblems { get; } = new(); // AllyEngageableEmblems + EnemyEngageableEmblems

        internal static List<(string id, string name)> BaseArenaEmblems { get; } = new()
        {
            ("GID_相手マルス", "Marth (Arena)"), ("GID_相手シグルド", "Sigurd (Arena)"),
            ("GID_相手セリカ", "Celica (Arena)"), ("GID_相手ミカヤ", "Micaiah (Arena)"),
            ("GID_相手ロイ", "Roy (Arena)"), ("GID_相手リーフ", "Leif (Arena)"),
            ("GID_相手ルキナ", "Lucina (Arena)"), ("GID_相手リン", "Lyn (Arena)"),
            ("GID_相手アイク", "Ike (Arena)"), ("GID_相手ベレト", "Byleth (Arena)"),
            ("GID_相手カムイ", "Corrin (Arena)"), ("GID_相手エイリーク", "Eirika (Arena)"),
            ("GID_相手リュール", "Alear (Arena)"),
            ("GID_相手エーデルガルト", "Edelgard (Arena)"), ("GID_相手チキ", "Tiki (Arena)"),
            ("GID_相手ヘクトル", "Hector (Arena)"), ("GID_相手ヴェロニカ", "Veronica (Arena)"),
            ("GID_相手セネリオ", "Soren (Arena)"), ("GID_相手カミラ", "Camilla (Arena)"),
            ("GID_相手クロム", "Chrom (Arena)"),
        };

        internal static List<(string id, string name)> ArenaEmblems { get; } = new() // BaseArenaEmblems +
        {
            ("GID_相手エフラム", "Ephraim (Arena)"), ("GID_相手ディミトリ", "Dimitri (Arena)"),
            ("GID_相手クロード", "Claude (Arena)")
        };

        internal static List<(string id, string name)> AllyArenaSyncableEmblems { get; } = new(); // AllySyncableEmblems + ArenaEmblems

        internal static List<(string id, string name)> SyncableEmblems { get; } = new(); // AllyArenaSyncableEmblems + EnemySyncableEmblems

        internal static List<(string id, string name)> Emblems { get; } = new() // SyncableEmblems +
        {
            ("GID_M000_マルス", "Marth (Prologue)"), ("GID_ルフレ", "Robin")
        };
        #endregion
        #region HubAnim IDs
        internal static List<(string id, string name)> MaleHubAnims { get; } = new()
        {
            ("AOC_Hub_Hum0M", "Male A"), ("AOC_Hub_Hum1M", "Male B"),
            ("AOC_Hub_Hum2M", "Male C"), ("AOC_Hub_c001", "Male Alear"),
            ("AOC_Hub_c101", "Boucheron"), ("AOC_Hub_c102", "Louis"),
            ("AOC_Hub_c302", "Kagetsu"), ("AOC_Hub_c304", "Lindon"),
            ("AOC_Hub_c400", "Fogado"), ("AOC_Hub_c402", "Bunet"),
            ("AOC_Hub_c500", "Vander"), ("AOC_Hub_c502", "Mauvier"),
            ("AOC_Hub_God0M", "Male Emblem"), ("AOC_Hub_Shop1", "Durthon"),
            ("AOC_Hub_Shop3", "Pinet"), ("AOC_Hub_c809", "Old Man"),
        };

        internal static List<(string id, string name)> FemaleHubAnims { get; } = new()
        {
            ("AOC_Hub_Hum0F", "Female A"), ("AOC_Hub_Hum1F", "Female B"),
            ("AOC_Hub_Hum2F", "Female C"), ("AOC_Hub_Hum3F", "Female D"),
            ("AOC_Hub_Hum0FL", "Female E"), ("AOC_Hub_Hum1FL", "Female F"),
            ("AOC_Hub_Hum2FL", "Female G"), ("AOC_Hub_Hum3FL", "Female H"),
            ("AOC_Hub_Hum1FLyn", "Female I"), ("AOC_Hub_Hum2FLyn", "Female J"),
            ("AOC_Hub_Hum3FLyn", "Female K"),
            ("AOC_Hub_c051", "Female Alear"), ("AOC_Hub_c150", "Céline A"),
            ("AOC_Hub_c150S", "Céline B"), ("AOC_Hub_c150Lyn", "Céline C"),
            ("AOC_Hub_c250", "Jade"), ("AOC_Hub_c254", "Saphir"),
            ("AOC_Hub_c351", "Hortensia"), ("AOC_Hub_c352", "Goldmary"),
            ("AOC_Hub_c452", "Merrin"), ("AOC_Hub_c453", "Panette"),
            ("AOC_Hub_c551", "Veyle"), ("AOC_Hub_c555", "Lumera"),
            ("AOC_Hub_God0F", "Female Emblem"), ("AOC_Hub_Shop2", "Anisse"),
            ("AOC_Hub_Shop4", "Calney"),
        };
        #endregion
        #region InfoAnim IDs
        internal static List<(string id, string name)> UniqueMaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c000", "Default Male"),
            ("AOC_Info_c001", "Male Alear"), ("AOC_Info_c001_Eng", "Male Alear Engaged"),
            ("AOC_Info_c049", "Rafal A"), ("AOC_Info_c049_Eng", "Rafal Engaged"),
            ("AOC_Info_c049b", "Rafal B"), ("AOC_Info_c049c", "Rafal C"),
            ("AOC_Info_c100", "Alfred A"), ("AOC_Info_c100_Eng", "Alfred Engaged"),
            ("AOC_Info_c100b", "Alfred B"), ("AOC_Info_c101", "Boucheron A"),
            ("AOC_Info_c101b", "Boucheron B"), ("AOC_Info_c101_Eng", "Boucheron Engaged"),
            ("AOC_Info_c102", "Louis"), ("AOC_Info_c102_Eng", "Louis Engaged"),
            ("AOC_Info_c103", "Jean"), ("AOC_Info_c103_Eng", "Jean Engaged"),
            ("AOC_Info_c200", "Diamant A"),
            ("AOC_Info_c200_Eng", "Diamant Engaged"), ("AOC_Info_c200b", "Diamant B"),
            ("AOC_Info_c201", "Alcryst A"), ("AOC_Info_c201b", "Alcryst B"),
            ("AOC_Info_c201_Eng", "Alcryst Engaged"), ("AOC_Info_c201c", "Alcryst C"),
            ("AOC_Info_c202", "Corrupted Morion"), ("AOC_Info_c203", "Amber"),
            ("AOC_Info_c203_Eng", "Amber Engaged"),
            ("AOC_Info_c300", "Hyacinth"), ("AOC_Info_c301", "Zelkov"),
            ("AOC_Info_c301_Eng", "Zelkov Engaged"), ("AOC_Info_c302", "Kagetsu"),
            ("AOC_Info_c302_Eng", "Kagetsu Engaged"), ("AOC_Info_c304", "Lindon"),
            ("AOC_Info_c304_Eng", "Lindon Engaged"),
            ("AOC_Info_c400", "Fogado A"), ("AOC_Info_c400_Eng", "Fogado Engaged"),
            ("AOC_Info_c400b", "Fogado B"), ("AOC_Info_c401", "Pandreo"),
            ("AOC_Info_c401_Eng", "Pandreo"), ("AOC_Info_c402", "Bunet"),
            ("AOC_Info_c402_Eng", "Bunet Engaged"), ("AOC_Info_c403", "Seadall A"),
            ("AOC_Info_c403b", "Seadall B"), ("AOC_Info_c403_Eng", "Seadall Engaged"),
            ("AOC_Info_c500", "Vander"),
            ("AOC_Info_c500_Eng", "Vander Engaged"), ("AOC_Info_c501", "Clanne"),
            ("AOC_Info_c501_Eng", "Clanne Engaged"), ("AOC_Info_c502", "Mauvier"),
            ("AOC_Info_c502_Eng", "Mauvier Engaged"), ("AOC_Info_c503", "Griss"),
            ("AOC_Info_c503b", "Gregory"), ("AOC_Info_c503b_Eng", "Gregory Engaged"),
            ("AOC_Info_c811", "Rodine"),
            ("AOC_Info_c812", "Nelucce"), ("AOC_Info_c815", "Teronda"),
            ("AOC_Info_c813", "Tetchie"), ("AOC_Info_c814", "Totchie"),
            ("AOC_Info_c530", "Marth"),
            ("AOC_Info_c531", "Sigurd"), ("AOC_Info_c532", "Leif"),
            ("AOC_Info_c533", "Roy"), ("AOC_Info_c534", "Ike"),
            ("AOC_Info_c535", "Byleth"), ("AOC_Info_c536", "Ephraim"),
            ("AOC_Info_c514", "Dimitri"), ("AOC_Info_c515", "Claude"),
            ("AOC_Info_c510", "Hector"), ("AOC_Info_c511", "Soren"),
            ("AOC_Info_c512", "Chrom"), ("AOC_Info_c513", "Robin"),
        };

        internal static List<(string id, string name)> GenericMaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c702", "Default Corrupted Male"),
            ("AOC_Info_c604", "Male Sword Wielder"), ("AOC_Info_c720", "Corrupted Male Sword Wielder"),
            ("AOC_Info_c610", "Male Lance Wielder"), ("AOC_Info_c722", "Corrupted Male Lance Wielder"),
            ("AOC_Info_c617", "Male Axe Wielder"), ("AOC_Info_c724", "Corrupted Male Axe Wielder"),
            ("AOC_Info_c630", "Male Armored"), ("AOC_Info_c728", "Corrupted Male Armored"),
            ("AOC_Info_c623", "Male Bow Wielder"), ("AOC_Info_c726", "Corrupted Male Bow Wielder"),
            ("AOC_Info_c637", "Male Cavalry"), ("AOC_Info_c730", "Corrupted Male Cavalry"),
            ("AOC_Info_c652", "Male Griffin/Wyvern Unit"), ("AOC_Info_c733", "Corrupted Male Griffin/Wyvern Unit"),
            ("AOC_Info_c657", "Male Dagger Wielder"), ("AOC_Info_c735", "Corrupted Male Dagger Wielder"),
            ("AOC_Info_c659", "Male Tome Wielder"), ("AOC_Info_c737", "Corrupted Male Tome Wielder"),
            ("AOC_Info_c666", "Male Arts Wielder"), ("AOC_Info_c739", "Corrupted Male Arts Wielder"),
            ("AOC_Info_c681", "Male Barbarian"), ("AOC_Info_c697", "Male Enchanter"),
            ("AOC_Info_c695", "Male Mage Cannoneer"),
        };

        internal static List<(string id, string name)> UniqueFemaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c050", "Default Female"),
            ("AOC_Info_c051", "Female Alear"), ("AOC_Info_c051_Eng", "Female Alear Engaged"),
            ("AOC_Info_c099", "Nel"), ("AOC_Info_c099_Eng", "Nel Engaged"),
            ("AOC_Info_c150", "Céline A"), ("AOC_Info_c150_Eng", "Céline Engaged"),
            ("AOC_Info_c150b", "Céline B"), ("AOC_Info_c152", "Etie"),
            ("AOC_Info_c152_Eng", "Etie Engaged"), ("AOC_Info_c153", "Chloé"),
            ("AOC_Info_c153_Eng", "Chloé Engaged"), ("AOC_Info_c250", "Jade"),
            ("AOC_Info_c250_Eng", "Jade Engaged"), ("AOC_Info_c251", "Lapis A"),
            ("AOC_Info_c251b", "Lapis B"), ("AOC_Info_c251_Eng", "Lapis Engaged"),
            ("AOC_Info_c252", "Citrinne"), ("AOC_Info_c252_Eng", "Citrinne Engaged"),
            ("AOC_Info_c253", "Yunaka"), ("AOC_Info_c253_Eng", "Yunaka Engaged"),
            ("AOC_Info_c254", "Saphir"), ("AOC_Info_c254_Eng", "Saphir Engaged"),
            ("AOC_Info_c303", "Rosado"), ("AOC_Info_c303_Eng", "Rosado Engaged"),
            ("AOC_Info_c350", "Ivy A"),
            ("AOC_Info_c350b", "Ivy B"), ("AOC_Info_c350_Eng", "Ivy Engaged"),
            ("AOC_Info_c350c", "Ivy C"), ("AOC_Info_c351", "Hortensia A"),
            ("AOC_Info_c351_Eng", "Hortensia Engaged"), ("AOC_Info_c351b", "Hortensia B"),
            ("AOC_Info_c352", "Goldmary"), ("AOC_Info_c352_Eng", "Goldmary Engaged"),
            ("AOC_Info_c450", "Timerra A"), ("AOC_Info_c450_Eng", "Timerra Engaged"),
            ("AOC_Info_c450b", "Timerra B"), ("AOC_Info_c452", "Merrin"),
            ("AOC_Info_c452_Eng", "Merrin Engaged"), ("AOC_Info_c453", "Panette"),
            ("AOC_Info_c453_Eng", "Panette Engaged"),
            ("AOC_Info_c550", "Framme"), ("AOC_Info_c550_Eng", "Framme Engaged"),
            ("AOC_Info_c551", "Veyle A"), ("AOC_Info_c551b", "Veyle B"),
            ("AOC_Info_c551_Eng", "Veyle Engaged"), ("AOC_Info_c552", "Anna"),
            ("AOC_Info_c552_Eng", "Anna Engaged"), ("AOC_Info_c553", "Zephia"),
            ("AOC_Info_c553b", "Zelestia A"), ("AOC_Info_c553c", "Zelestia B"),
            ("AOC_Info_c553b_Eng", "Zelestia Engaged"), ("AOC_Info_c554", "Marni"),
            ("AOC_Info_c554b", "Madeline"), ("AOC_Info_c554b_Eng", "Madeline Engaged"),
            ("AOC_Info_c555", "Lumera"), ("AOC_Info_c558", "Corrupted Lumera"),
            ("AOC_Info_c855", "Abyme"), ("AOC_Info_c859", "Mitan"),
            ("AOC_Info_c580", "Celica"), ("AOC_Info_c581", "Lyn"),
            ("AOC_Info_c582", "Eirika"), ("AOC_Info_c583", "Micaiah"),
            ("AOC_Info_c584", "Lucina"), ("AOC_Info_c585", "Corrin"),
            ("AOC_Info_c560", "Tiki"), ("AOC_Info_c563", "Edelgard"),
            ("AOC_Info_c561", "Camilla"), ("AOC_Info_c562", "Veronica"),
        };

        internal static List<(string id, string name)> GenericFemaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c703", "Default Corrupted Female"),
            ("AOC_Info_c605", "Female Sword Wielder"), ("AOC_Info_c721", "Corrupted Female Sword Wielder"),
            ("AOC_Info_c611", "Female Lance Wielder"), ("AOC_Info_c723", "Corrupted Female Lance Wielder"),
            ("AOC_Info_c618", "Female Axe Wielder"), ("AOC_Info_c725", "Corrupted Female Axe Wielder"),
            ("AOC_Info_c631", "Female Armored"), ("AOC_Info_c729", "Corrupted Female Armored"),
            ("AOC_Info_c624", "Female Bow Wielder"), ("AOC_Info_c727", "Corrupted Female Bow Wielder"),
            ("AOC_Info_c638", "Female Cavalry"), ("AOC_Info_c731", "Corrupted Female Cavalry"),
            ("AOC_Info_c646", "Flier"), ("AOC_Info_c732", "Corrupted Flier"),
            ("AOC_Info_c653", "Female Griffin/Wyvern Unit"), ("AOC_Info_c734", "Corrupted Female Griffin/Wyvern Unit"),
            ("AOC_Info_c658", "Female Dagger Wielder"), ("AOC_Info_c736", "Corrupted Female Dagger Wielder"),
            ("AOC_Info_c660", "Female Tome Wielder"), ("AOC_Info_c738", "Corrupted Female Tome Wielder"),
            ("AOC_Info_c667", "Female Arts Wielder"), ("AOC_Info_c740", "Corrupted Female Arts Wielder"),
            ("AOC_Info_c682", "Female Barbarian"), ("AOC_Info_c698", "Female Enchanter"),
            ("AOC_Info_c696", "Female Mage Cannoneer"),
        };
        #endregion
        #region Item IDs
        internal static List<(string id, string name)> EngageWeapons { get; } = new()
        {
            ("IID_マルス_レイピア", "Rapier (Marth)"), ("IID_マルス_メリクルソード", "Mercurius"), ("IID_マルス_ファルシオン", "Falchion (Marth)"), ("IID_シグルド_ナイトキラー", "Ridersbane"),
            ("IID_シグルド_ゆうしゃのやり", "Brave Lance"), ("IID_シグルド_ティルフィング", "Tyrfing"), ("IID_セリカ_エンジェル", "Seraphim"), ("IID_セリカ_リカバー", "Recover"),
            ("IID_セリカ_ライナロック", "Ragnarok"), ("IID_ミカヤ_シャイン", "Shine"), ("IID_ミカヤ_リザイア", "Nosferatu"), ("IID_ミカヤ_セイニー", "Thani"),
            ("IID_ロイ_ランスバスター", "Lancereaver"), ("IID_ロイ_ドラゴンキラー", "Wyrmslayer"), ("IID_ロイ_封印の剣", "Binding Blade"), ("IID_リーフ_キラーアクス", "Killer Axe"),
            ("IID_リーフ_キラーアクス_M008", "Killer Axe (Chapter 8)"), ("IID_リーフ_キラーアクス_M011", "Killer Axe (Chapter 11)"), ("IID_リーフ_マスターランス", "Master Lance"), ("IID_リーフ_マスターランス_M008", "Master Lance (Chapter 8)"),
            ("IID_リーフ_ひかりの剣", "Light Brand"), ("IID_リーフ_マスターボウ", "Master Bow"), ("IID_ルキナ_ノーブルレイピア", "Noble Rapier"), ("IID_ルキナ_ノーブルレイピア_M007", "Noble Rapier (Chapter 7)"),
            ("IID_ルキナ_パルティア", "Parthia"), ("IID_ルキナ_裏剣ファルシオン", "Parallel Falchion"), ("IID_リン_キラーボウ", "Killer Bow"), ("IID_リン_キラーボウ_M010", "Killer Bow (Chapter 10)"),
            ("IID_リン_マーニ・カティ", "Mani Katti"), ("IID_リン_マーニ・カティ_M010", "Mani Katti (Chapter 10)"), ("IID_リン_ミュルグレ", "Mulagir"), ("IID_アイク_ハンマー", "Hammer"),
            ("IID_アイク_ウルヴァン", "Urvan"), ("IID_アイク_ラグネル", "Ragnell"), ("IID_ベレト_アイムール", "Aymr (Byleth)"), ("IID_ベレト_ブルトガング", "Blutgang"),
            ("IID_ベレト_ルーン", "Lúin"), ("IID_ベレト_ルーン_M010", "Lúin (Chapter 10)"), ("IID_ベレト_アラドヴァル", "Areadbhar (Byleth)"), ("IID_ベレト_アイギスの盾", "Aegis Shield"),
            ("IID_ベレト_フェイルノート", "Failnaught"), ("IID_ベレト_テュルソスの杖", "Thyrsus"), ("IID_ベレト_ラファイルの宝珠", "Rafail Gem"), ("IID_ベレト_ヴァジュラ", "Vajra-Mushti"),
            ("IID_ベレト_天帝の覇剣", "Sword of the Creator"), ("IID_ベレト_天帝の覇剣_M014", "Sword of the Creator (Chapter 14)"), ("IID_カムイ_逆刀", "Dual Katana"), ("IID_カムイ_飛刀", "Wakizashi"),
            ("IID_カムイ_夜刀神", "Yato"), ("IID_エイリーク_レイピア", "Rapier (Eirika)"), ("IID_エイリーク_かぜの剣", "Wind Sword"), ("IID_エイリーク_ジークリンデ", "Sieglinde"),
            ("IID_エフラム_ジークムント", "Siegmund"), ("IID_リュール_オリゴルディア", "Oligoludia"), ("IID_リュール_竜神の法", "Dragon's Fist"), ("IID_リュール_ライラシオン", "Lyrátion"),
            ("IID_三級長_アイムール", "Aymr (Edelgard)"), ("IID_三級長_アラドヴァル", "Areadbhar (Dimitri)"), ("IID_三級長_フェイルノート", "Failnaught"), ("IID_チキ_つめ", "Eternal Claw"),
            ("IID_チキ_つめ_E006", "Eternal Claw (Xenologue 6)"), ("IID_チキ_しっぽ", "Tail Smash"), ("IID_チキ_しっぽ_E006", "Tail Smash (Xenologue 6)"), ("IID_チキ_ブレス", "Fire Breath"),
            ("IID_チキ_ブレス_竜族", "Fog Breath"), ("IID_チキ_ブレス_重装", "Ice Breath"), ("IID_チキ_ブレス_飛行", "Flame Breath"), ("IID_チキ_ブレス_魔法", "Dark Breath"),
            ("IID_チキ_ブレス_E001", "Fire Breath (Xenologue 1)"), ("IID_チキ_ブレス_E006", "Fire Breath (Xenologue 6)"), ("IID_ヘクトル_ヴォルフバイル", "Wolf Beil"), ("IID_ヘクトル_ルーンソード", "Runesword"),
            ("IID_ヘクトル_ルーンソード_闇", "Corrupted Runesword"), ("IID_ヘクトル_アルマーズ", "Armads"), ("IID_ヴェロニカ_フリズスキャルヴ", "Hliðskjálf"), ("IID_ヴェロニカ_リザーブ＋", "Fortify+"),
            ("IID_ヴェロニカ_エリヴァーガル", "Élivágar"), ("IID_セネリオ_サンダーストーム", "Bolting"), ("IID_セネリオ_サンダーストーム_闇", "Corrupted Bolting"), ("IID_セネリオ_マジックシールド", "Reflect"),
            ("IID_セネリオ_レクスカリバー", "Rexcalibur"), ("IID_カミラ_ボルトアクス", "Bolt Axe"), ("IID_カミラ_ライトニング", "Lightning"), ("IID_カミラ_カミラの艶斧", "Camilla's Axe"),
            ("IID_クロム_サンダーソード", "Levin Sword"), ("IID_クロム_トロン", "Thoron"), ("IID_クロム_神剣ファルシオン", "Falchion (Chrom)")
        };

        internal static List<(string id, string name)> DSwordWeapons { get; } = new()
        {
            ("IID_ほそみの剣", "Slim Sword"), ("IID_鉄の剣", "Iron Sword"), ("IID_フォルクヴァング", "Fólkvangr"), ("IID_チョコレート剣", "Biting Blade"),
        };

        internal static List<(string id, string name)> CSwordWeapons { get; } = new()
        {
            ("IID_鋼の剣", "Steel Sword"), ("IID_アーマーキラー", "Armorslayer"), ("IID_キルソード", "Killing Edge"), ("IID_いかづちの剣", "Levin Sword"),
            ("IID_鉄の大剣", "Iron Blade"),
        };

        internal static List<(string id, string name)> BSwordWeapons { get; } = new()
        {
            ("IID_銀の剣", "Silver Sword"), ("IID_倭刀", "Wo Dao"), ("IID_ドラゴンキラー", "Wyrmslayer"), ("IID_鋼の大剣", "Steel Blade"),
        };

        internal static List<(string id, string name)> ASwordWeapons { get; } = new()
        {
            ("IID_勇者の剣", "Brave Sword"), ("IID_銀の大剣", "Silver Blade"),
        };

        internal static List<(string id, string name)> SSwordWeapons { get; } = new()
        {
            ("IID_クラドホルグ", "Caladbolg"), ("IID_ゲオルギオス", "Georgios"),
        };

        internal static List<(string id, string name)> DLanceWeapons { get; } = new()
        {
            ("IID_ほそみの槍", "Slim Lance"), ("IID_鉄の槍", "Iron Lance"), ("IID_手槍", "Javelin"), ("IID_ナイトキラー", "Ridersbane"),
            ("IID_フェンサリル", "Fensalir"), ("IID_ソフトクリーム槍", "Swirlance"),
        };


        internal static List<(string id, string name)> CLanceWeapons { get; } = new()
        {
            ("IID_鋼の槍", "Steel Lance"), ("IID_キラーランス", "Killer Lance"), ("IID_ほのおの槍", "Flame Lance"), ("IID_鉄の大槍", "Iron Greatlance"),
        };

        internal static List<(string id, string name)> BLanceWeapons { get; } = new()
        {
            ("IID_銀の槍", "Silver Lance"), ("IID_スレンドスピア", "Spear"), ("IID_鋼の大槍", "Steel Greatlance"),
        };

        internal static List<(string id, string name)> ALanceWeapons { get; } = new()
        {
            ("IID_勇者の槍", "Brave Lance"), ("IID_銀の大槍", "Silver Greatlance"), ("IID_トライゾン", "Représailles"),
        };

        internal static List<(string id, string name)> SLanceWeapons { get; } = new()
        {
            ("IID_ブリューナク", "Brionac"), ("IID_ヴェノマス", "Venomous"),
        };

        internal static List<(string id, string name)> DAxeWeapons { get; } = new()
        {
            ("IID_ショートアクス", "Compact Axe"), ("IID_鉄の斧", "Iron Axe"), ("IID_手斧", "Hand Axe"), ("IID_ハンマー", "Hammer"),
            ("IID_ポールアクス", "Poleaxe"), ("IID_ノーアトゥーン", "Nóatún"), ("IID_ロリポップ斧", "Lollichop"),
        };

        internal static List<(string id, string name)> CAxeWeapons { get; } = new()
        {
            ("IID_鋼の斧", "Steel Axe"), ("IID_キラーアクス", "Killer Axe"), ("IID_鉄の大斧", "Iron Greataxe"),
        };

        internal static List<(string id, string name)> BAxeWeapons { get; } = new()
        {
            ("IID_銀の斧", "Silver Axe"), ("IID_トマホーク", "Tomahawk"), ("IID_鋼の大斧", "Steel Greataxe"), ("IID_かぜの大斧", "Hurricane Axe"),
        };

        internal static List<(string id, string name)> AAxeWeapons { get; } = new()
        {
            ("IID_勇者の斧", "Brave Axe"), ("IID_銀の大斧", "Silver Greataxe"), ("IID_ルヴァンシュ", "Revanche"), ("IID_ルヴァンシュ_E005", "Revanche (Xenologue 5)"),
        };


        internal static List<(string id, string name)> SAxeWeapons { get; } = new()
        {
            ("IID_フラガラッハ", "Fragarach"), ("IID_ウコンバサラ", "Ukonvasara"),
        };

        internal static List<(string id, string name)> DBowWeapons { get; } = new()
        {
            ("IID_ショートボウ", "Mini Bow"), ("IID_鉄の弓", "Iron Bow"), ("IID_クロワッサン弓", "Croissbow"),
        };

        internal static List<(string id, string name)> CBowWeapons { get; } = new()
        {
            ("IID_鋼の弓", "Steel Bow"), ("IID_長弓", "Longbow"), ("IID_キラーボウ", "Killer Bow"), ("IID_光の弓", "Radiant Bow"),
        };

        internal static List<(string id, string name)> BBowWeapons { get; } = new()
        {
            ("IID_銀の弓", "Silver Bow"),
        };

        internal static List<(string id, string name)> ABowWeapons { get; } = new()
        {
            ("IID_勇者の弓", "Brave Bow"),
        };

        internal static List<(string id, string name)> SBowWeapons { get; } = new()
        {
            ("IID_レンダウィル", "Lendabair"),
        };

        internal static List<(string id, string name)> DDaggerWeapons { get; } = new()
        {
            ("IID_ショートナイフ", "Short Knife"), ("IID_鉄のナイフ", "Iron Dagger"), ("IID_おだんご短剣", "Confectioknife"),
        };

        internal static List<(string id, string name)> CDaggerWeapons { get; } = new()
        {
            ("IID_鋼のナイフ", "Steel Dagger"), ("IID_カルド", "Kard"),
        };

        internal static List<(string id, string name)> BDaggerWeapons { get; } = new()
        {
            ("IID_銀のナイフ", "Silver Dagger"), ("IID_スティレット", "Stiletto"),
        };

        internal static List<(string id, string name)> ADaggerWeapons { get; } = new()
        {
            ("IID_ペシュカド", "Peshkatz"),
        };

        internal static List<(string id, string name)> SDaggerWeapons { get; } = new()
        {
            ("IID_シンクエディア", "Cinquedea"), ("IID_カルンウェナン", "Carnwenhan"),
        };

        internal static List<(string id, string name)> DTomeWeapons { get; } = new()
        {
            ("IID_サージ", "Surge"), ("IID_ファイアー", "Fire"), ("IID_サンダー", "Thunder"), ("IID_ティラミス魔道書", "Tiramistorm"),
        };

        internal static List<(string id, string name)> CTomeWeapons { get; } = new()
        {
            ("IID_ウィンド", "Wind"), ("IID_エルサージ", "Elsurge"), ("IID_エルファイアー", "Elfire"),
        };

        internal static List<(string id, string name)> BTomeWeapons { get; } = new()
        {
            ("IID_エルサンダー", "Elthunder"), ("IID_エルウィンド", "Elwind"),
        };

        internal static List<(string id, string name)> CommonATomeWeapons { get; } = new()
        {
            ("IID_ボルガノン", "Bolganone"), ("IID_トロン", "Thoron"), ("IID_エクスカリバー", "Excalibur"),
        };

        internal static List<(string id, string name)> EnemyATomeWeapons { get; } = new()
        {
            ("IID_トロン_セピア専用", "Thoron (Zephia)"), ("IID_エクスカリバー_グリ専用", "Excalibur (Griss)"), ("IID_メティオ", "Meteor"), ("IID_メティオ_G004", "Meteor (Soren Paralogue)"),
        };

        internal static List<(string id, string name)> STomeWeapons { get; } = new()
        {
            ("IID_ノヴァ", "Nova"),
        };

        internal static List<(string id, string name)> CommonDStaves { get; } = new()
        {
            ("IID_ライブ", "Heal"), ("IID_リライブ", "Mend"),
            ("IID_カップケーキ杖", "Treat"),
        };

        internal static List<(string id, string name)> AllyDStaves { get; } = new()
        {
            ("IID_トーチ", "Illume"), ("IID_アイスロック", "Obstruct"),
        };

        internal static List<(string id, string name)> CommonCStaves { get; } = new()
        {
            ("IID_リブロー", "Physic"), ("IID_リブロー_G004", "Physic (Soren Paralogue)"), ("IID_リワープ", "Rewarp"),
            ("IID_フリーズ", "Freeze"), ("IID_サイレス", "Silence"),
        };

        internal static List<(string id, string name)> AllyCStaves { get; } = new()
        {
            ("IID_レスト", "Restore"),
        };

        internal static List<(string id, string name)> EnemyCStaves { get; } = new()
        {
            ("IID_フリーズ_S010", "Freeze (Leif Paralogue)"), ("IID_サイレス_S010", "Silence (Leif Paralogue)"),
        };

        internal static List<(string id, string name)> CommonBStaves { get; } = new()
        {
            ("IID_リカバー", "Recover"), ("IID_ワープ", "Warp"), ("IID_レスキュー", "Rescue"), ("IID_コラプス", "Fracture"),
        };

        internal static List<(string id, string name)> EnemyBStaves { get; } = new()
        {
            ("IID_レスキュー_M019", "Rescue (Chapter 19)"), ("IID_コラプス_M014", "Fracture (Chapter 14)"), ("IID_コラプス_S010", "Fracture (Leif Paralogue)")
        };

        internal static List<(string id, string name)> AStaves { get; } = new()
        {
            ("IID_リザーブ", "Fortify"), ("IID_ドロー", "Entrap"),
        };

        internal static List<(string id, string name)> AllySStaves { get; } = new()
        {
            ("IID_ノードゥス", "Nodus"),
        };

        internal static List<(string id, string name)> DArtWeapons { get; } = new()
        {
            ("IID_初心の法", "Initiate Art"), ("IID_鉄身の法", "Iron-Body Art"), ("IID_ロールケーキ体術", "Scrollcake"),
        };


        internal static List<(string id, string name)> CArtWeapons { get; } = new()
        {
            ("IID_鋼身の法", "Steel-Hand Art"), ("IID_護身の法", "Shielding Art"),
        };

        internal static List<(string id, string name)> BArtWeapons { get; } = new()
        {
            ("IID_銀身の法", "Silver-Spirit Art"),
        };

        internal static List<(string id, string name)> AArtWeapons { get; } = new()
        {
            ("IID_閃進の法", "Flashing Fist Art"),
        };

        internal static List<(string id, string name)> SArtWeapons { get; } = new()
        {
            ("IID_覇神の法", "Divine Fist Art"),
        };

        internal static List<(string id, string name)> DSpecialWeapons { get; } = new()
        {
            ("IID_火のブレス", "Fire Breath"), ("IID_炎塊", "Fireball"),
        };

        internal static List<(string id, string name)> CSpecialWeapons { get; } = new()
        {
            ("IID_氷のブレス", "Freezing Breath"), ("IID_氷塊", "Iceball"),
        };

        internal static List<(string id, string name)> BSpecialWeapons { get; } = new()
        {
            ("IID_牙", "Fangs"), ("IID_瘴気のブレス", "Miasma Breath"), ("IID_瘴気の塊", "Miasma Ball"),
        };

        internal static List<(string id, string name)> SSpecialWeapons { get; } = new()
        {
            ("IID_ソンブル_物理攻撃", "Fell Assault (Sombron)"), ("IID_ソンブル_魔法攻撃", "Fell Surge"), ("IID_ソンブル_回転アタック", "Whirling Death"), ("IID_ソンブル_ビーム", "Howling Beam"),
            ("IID_ソンブル_エンゲージブレイク", "Disengage"), ("IID_イル_反撃", "Fell Assault (Nil)"), ("IID_イル_薙払いビーム", "Fell Beam"), ("IID_イル_突進", "Devastation"),
            ("IID_イル_吸収", "Drain Essence"), ("IID_イル_召喚", "Summon Vortex"),
        };

        internal static List<(string id, string name)> LiberationWeapons { get; } = new()
        {
            ("IID_リベラシオン", "Libération"), ("IID_リベラシオン改", "Libération (Xenologue 6)"), ("IID_リベラシオン改_ノーマル", "Libération (Xenologue 6 Normal)"),
        };

        internal static List<(string id, string name)> WilleGlanzWeapons { get; } = new()
        {
            ("IID_ヴィレグランツ", "Wille Glanz"),
        };

        internal static List<(string id, string name)> MisericordeWeapons { get; } = new()
        {
            ("IID_ミセリコルデ", "Misericorde"),
        };

        internal static List<(string id, string name)> ObscuriteWeapons { get; } = new()
        {
            ("IID_オヴスキュリテ", "Obscurité"),
        };

        internal static List<(string id, string name)> DragonStones { get; } = new()
        {
            ("IID_邪竜石", "Fell Stone"), ("IID_真邪竜石", "Fell Ruinstone"), ("IID_邪竜石_魔法攻撃", "Fell Magicstone"), ("IID_邪竜石_騎馬特効", "Fell Slaystone"),
            ("IID_邪竜石_飛行特効", "Fell Weightstone"), ("IID_邪竜石_E", "Fell Spark"), ("IID_邪竜石_E005", "Fell Spark (Xenologue 5)"), ("IID_邪竜石_魔法攻撃_E", "Fell Arcana"),
        };

        internal static List<(string id, string name)> Cannonballs { get; } = new()
        {
            ("IID_弾_物理", "Standard Blast"), ("IID_弾_魔法", "Magic Blast"), ("IID_弾_フリーズ", "Freeze Blast"), ("IID_弾_サイレス", "Silence Blast"),
            ("IID_弾_ブレイク", "Break Blast"), ("IID_弾_毒", "Venom Blast"), ("IID_弾_飛行特効", "Tornado Blast"), ("IID_弾_重装特効", "Armor Blast"),
            ("IID_弾_騎馬特効", "Rider Blast"), ("IID_弾_竜特効", "Wyrm Blast"), ("IID_弾_異形特効", "Holy Blast"), ("IID_弾_物理_強", "Mighty Blast"),
            ("IID_弾_魔法_強", "Eldritch Blast"), ("IID_弾_防御無視", "Piercing Blast"),
        };

        internal static List<(string id, string name)> NormalEngageSwordWeapons { get; } = new()
        {
            ("IID_マルス_レイピア_通常", "Rapier (Marth)"), ("IID_マルス_メリクルソード_通常", "Mercurius"), ("IID_マルス_ファルシオン_通常", "Falchion (Marth)"), ("IID_シグルド_ティルフィング_通常", "Tyrfing"),
            ("IID_ロイ_ランスバスター_通常", "Lancereaver"), ("IID_ロイ_ドラゴンキラー_通常", "Wyrmslayer (Roy)"), ("IID_ロイ_封印の剣_通常", "Binding Blade"), ("IID_リーフ_ひかりの剣_通常", "Light Brand"),
            ("IID_ルキナ_ノーブルレイピア_通常", "Noble Rapier"), ("IID_ルキナ_裏剣ファルシオン_通常", "Parallel Falchion"), ("IID_リン_マーニ・カティ_通常", "Mani Katti"), ("IID_アイク_ラグネル_通常", "Ragnell"),
            ("IID_ベレト_ブルトガング_通常", "Blutgang"), ("IID_ベレト_天帝の覇剣_通常", "Sword of the Creator"), ("IID_カムイ_逆刀_通常", "Dual Katana"), ("IID_カムイ_飛刀_通常", "Wakizashi"),
            ("IID_カムイ_夜刀神_通常", "Yato"), ("IID_エイリーク_レイピア_通常", "Rapier (Eirika)"), ("IID_エイリーク_かぜの剣_通常", "Wind Sword"), ("IID_エイリーク_ジークリンデ_通常", "Sieglinde"),
            ("IID_リュール_オリゴルディア_通常", "Oligoludia"), ("IID_リュール_ライラシオン_通常", "Lyrátion"), ("IID_ヘクトル_ルーンソード_通常", "Runesword"), ("IID_ヘクトル_ルーンソード_通常_G002_最弱", "Runesword A (Hector Paralogue)"),
            ("IID_ヘクトル_ルーンソード_通常_G002_弱", "Runesword B (Hector Paralogue)"), ("IID_ヘクトル_ルーンソード_通常_G002_強", "Runesword C (Hector Paralogue)"), ("IID_ヘクトル_ルーンソード_通常_G002_最強", "Runesword D (Hector Paralogue)"), ("IID_クロム_サンダーソード_通常", "Levin Sword (Chrom)"),
            ("IID_クロム_サンダーソード_通常_G005_弱", "Levin Sword A (Chrom Paralogue)"), ("IID_クロム_サンダーソード_通常_G005_最弱", "Levin Sword B (Chrom Paralogue)"), ("IID_クロム_神剣ファルシオン_通常", "Falchion (Chrom)"), ("IID_クロム_神剣ファルシオン_通常_G006_微弱", "Falchion A (Chrom Paralogue)"),
            ("IID_クロム_神剣ファルシオン_通常_G006_弱", "Falchion B (Chrom Paralogue)"), ("IID_クロム_神剣ファルシオン_通常_G006_最弱", "Falchion C (Chrom Paralogue)"), ("IID_リーフ_ひかりの剣＋_通常", "Light Brand+"), ("IID_リーフ_ひかりの剣＋＋_通常", "Light Brand++"),
            ("IID_ルキナ_裏剣ファルシオン＋_通常", "Parallel Falchion+"), ("IID_リン_マーニ・カティ＋_通常", "Mani Katti+"), ("IID_リン_マーニ・カティ＋＋_通常", "Mani Katti++"), ("IID_ベレト_ブルトガング＋_通常", "Blutgang+"),
            ("IID_ヘクトル_ルーンソード＋_通常", "Runesword+"), ("IID_クロム_サンダーソード＋_通常", "Levin Sword+ (Chrom)"),
        };

        internal static List<(string id, string name)> NormalEngageLanceWeapons { get; } = new()
        {
            ("IID_シグルド_ナイトキラー_通常", "Ridersbane (Sigurd)"), ("IID_シグルド_ゆうしゃのやり_通常", "Brave Lance (Sigurd)"), ("IID_リーフ_マスターランス_通常", "Master Lance"), ("IID_三級長_アラドヴァル_通常", "Areadbhar"),
            ("IID_シグルド_ゆうしゃのやり＋_通常", "Brave Lance+ (Sigurd)"), ("IID_リーフ_マスターランス＋_通常", "Master Lance+"), ("IID_リーフ_マスターランス＋＋_通常", "Master Lance++"),
        };

        internal static List<(string id, string name)> NormalEngageAxeWeapons { get; } = new()
        {
            ("IID_リーフ_キラーアクス_通常", "Killer Axe (Leif)"), ("IID_アイク_ハンマー_通常", "Hammer (Ike)"), ("IID_アイク_ウルヴァン_通常", "Urvan"), ("IID_三級長_アイムール_通常", "Aymr"),
            ("IID_ヘクトル_ヴォルフバイル_通常", "Wolf Beil"), ("IID_ヘクトル_ヴォルフバイル_通常_G002_低命中", "Wolf Beil A (Hector Paralogue)"), ("IID_ヘクトル_ヴォルフバイル_通常_G002_最弱_低命中", "Wolf Beil B (Hector Paralogue)"), ("IID_ヘクトル_ヴォルフバイル_通常_G002_最弱", "Wolf Beil C (Hector Paralogue)"),
            ("IID_ヘクトル_ヴォルフバイル_通常_G002_弱_低命中", "Wolf Beil D (Hector Paralogue)"), ("IID_ヘクトル_ヴォルフバイル_通常_G002_弱", "Wolf Beil E (Hector Paralogue)"), ("IID_ヘクトル_ヴォルフバイル_通常_G002_強", "Wolf Beil F (Hector Paralogue)"), ("IID_ヘクトル_ヴォルフバイル_通常_G002_最強", "Wolf Beil G (Hector Paralogue)"),
            ("IID_ヘクトル_アルマーズ_通常", "Armads"), ("IID_ヘクトル_アルマーズ_通常_G002_最弱_低命中", "Armads A (Hector Paralogue)"), ("IID_ヘクトル_アルマーズ_通常_G002_最弱", "Armads B (Hector Paralogue)"), ("IID_ヘクトル_アルマーズ_通常_G002_弱_低命中", "Armads C (Hector Paralogue)"),
            ("IID_ヘクトル_アルマーズ_通常_G002_弱", "Armads D (Hector Paralogue)"), ("IID_ヘクトル_アルマーズ_通常_G002_微弱_低命中", "Armads E (Hector Paralogue)"), ("IID_ヘクトル_アルマーズ_通常_G002_微弱", "Armads F (Hector Paralogue)"), ("IID_カミラ_ボルトアクス_通常", "Bolt Axe"),
            ("IID_カミラ_ボルトアクス_通常_G005", "Bolt Axe A (Camilla Paralogue)"), ("IID_カミラ_ボルトアクス_通常_G005_弱", "Bolt Axe B (Camilla Paralogue)"), ("IID_カミラ_カミラの艶斧_通常", "Camilla's Axe"),
            ("IID_カミラ_カミラの艶斧_通常_G005_微弱", "Camilla's Axe A (Camilla Paralogue)"), ("IID_カミラ_カミラの艶斧_通常_G005_弱", "Camilla's Axe B (Camilla Paralogue)"), ("IID_カミラ_カミラの艶斧_通常_G005_最弱", "Camilla's Axe C (Camilla Paralogue)"), ("IID_リーフ_キラーアクス＋_通常", "Killer Axe+ (Leif)"),
            ("IID_リーフ_キラーアクス＋＋_通常", "Killer Axe++ (Leif)"), ("IID_ヘクトル_ヴォルフバイル＋_通常", "Wolf Beil+"),
        };

        internal static List<(string id, string name)> NormalEngageBowWeapons { get; } = new()
        {
            ("IID_ルキナ_パルティア_通常", "Parthia"), ("IID_リン_キラーボウ_通常", "Killer Bow (Lyn)"), ("IID_リン_ミュルグレ_通常", "Mulagir"), ("IID_三級長_フェイルノート_通常", "Failnaught"),
            ("IID_リン_キラーボウ＋_通常", "Killer Bow+ (Lyn)"), ("IID_リン_キラーボウ＋＋_通常", "Killer Bow++ (Lyn)"),
        };

        internal static List<(string id, string name)> NormalEngageDaggerWeapons { get; } = new()
        {

        };

        internal static List<(string id, string name)> NormalEngageTomeWeapons { get; } = new()
        {
            ("IID_セリカ_エンジェル_通常", "Seraphim"), ("IID_セリカ_ライナロック_通常", "Ragnarok"), ("IID_ミカヤ_シャイン_通常", "Shine"), ("IID_ミカヤ_リザイア_通常", "Nosferatu"),
            ("IID_ミカヤ_セイニー_通常", "Thani"), ("IID_ヴェロニカ_フリズスキャルヴ_通常", "Hliðskjálf"), ("IID_ヴェロニカ_フリズスキャルヴ_通常_G003_最弱", "Hliðskjálf A (Veronica Paralogue)"), ("IID_ヴェロニカ_フリズスキャルヴ_通常_G003_弱", "Hliðskjálf B (Veronica Paralogue)"),
            ("IID_ヴェロニカ_フリズスキャルヴ_通常_G003_微弱", "Hliðskjálf C (Veronica Paralogue)"), ("IID_ヴェロニカ_フリズスキャルヴ_通常_G003_強", "Hliðskjálf D (Veronica Paralogue)"), ("IID_ヴェロニカ_エリヴァーガル_通常", "Élivágar"), ("IID_ヴェロニカ_エリヴァーガル_通常_G003_最弱", "Élivágar A (Veronica Paralogue)"),
            ("IID_ヴェロニカ_エリヴァーガル_通常_G003_弱", "Élivágar B (Veronica Paralogue)"), ("IID_ヴェロニカ_エリヴァーガル_通常_G003_微弱", "Élivágar C (Veronica Paralogue)"), ("IID_ヴェロニカ_エリヴァーガル_通常_G003_微強", "Élivágar D (Veronica Paralogue)"), ("IID_ヴェロニカ_エリヴァーガル_通常_G003_強", "Élivágar E (Veronica Paralogue)"),
            ("IID_セネリオ_サンダーストーム_通常", "Bolting"), ("IID_セネリオ_サンダーストーム_通常_G004_弱", "Bolting A (Soren Paralogue)"), ("IID_セネリオ_サンダーストーム_通常_G004_最弱", "Bolting B (Soren Paralogue)"), ("IID_セネリオ_レクスカリバー_通常", "Rexcalibur"),
            ("IID_セネリオ_レクスカリバー_通常_G004_微弱", "Rexcalibur A (Soren Paralogue)"), ("IID_セネリオ_レクスカリバー_通常_G004_弱", "Rexcalibur B (Soren Paralogue)"), ("IID_セネリオ_レクスカリバー_通常_G004_最弱", "Rexcalibur C (Soren Paralogue)"), ("IID_カミラ_ライトニング_通常", "Lightning"),
            ("IID_カミラ_ライトニング_通常_G005_弱", "Lightning (Camilla Paralogue)"), ("IID_クロム_トロン_通常", "Thoron (Chrom)"), ("IID_クロム_トロン_通常_G006_微弱", "Thoron A (Chrom Paralogue)"), ("IID_クロム_トロン_通常_G006_弱", "Thoron B (Chrom Paralogue)"),
            ("IID_クロム_トロン_通常_G006_最弱", "Thoron C (Chrom Paralogue)"), ("IID_カミラ_ライトニング＋_通常", "Lightning+"), ("IID_クロム_トロン＋_通常", "Thoron+ (Chrom)"),
        };

        internal static List<(string id, string name)> NormalEngageStaves { get; } = new()
        {
            ("IID_セリカ_リカバー_通常", "Recover (Celica)"), ("IID_ヴェロニカ_リザーブ＋_通常", "Fortify+"), ("IID_セネリオ_マジックシールド_通常", "Reflect"),
        };

        internal static List<(string id, string name)> NormalEngageArtWeapons { get; } = new()
        {
            ("IID_ベレト_ヴァジュラ_通常", "Vajra-Mushti"), ("IID_リュール_竜神の法_通常", "Dragon's Fist"), ("IID_ベレト_ヴァジュラ＋_通常", "Vajra-Mushti+"),
        };

        internal static List<(string id, string name)> NormalEngageSpecialWeapons { get; } = new()
        {
            ("IID_チキ_つめ_通常", "Eternal Claw"), ("IID_チキ_つめ_通常_G001_最弱", "Eternal Claw A (Tiki Paralogue)"), ("IID_チキ_つめ_通常_G001_弱", "Eternal Claw B (Tiki Paralogue)"), ("IID_チキ_つめ_通常_G001_微弱", "Eternal Claw C (Tiki Paralogue)"),
            ("IID_チキ_つめ_通常_G001_強", "Eternal Claw D (Tiki Paralogue)"), ("IID_チキ_つめ_通常_G001_最強", "Eternal Claw E (Tiki Paralogue)"), ("IID_チキ_しっぽ_通常", "Tail Smash"), ("IID_チキ_しっぽ_通常_G001_最弱", "Tail Smash A (Tiki Paralogue)"),
            ("IID_チキ_しっぽ_通常_G001_弱", "Tail Smash B (Tiki Paralogue)"), ("IID_チキ_しっぽ_通常_G001_微強", "Tail Smash C (Tiki Paralogue)"), ("IID_チキ_しっぽ_通常_G001_強", "Tail Smash D (Tiki Paralogue)"), ("IID_チキ_しっぽ_通常_G001_最強", "Tail Smash E (Tiki Paralogue)"),
            ("IID_チキ_ブレス_通常", "Fog Breath"), ("IID_チキ_ブレス_通常_G001_最弱", "Fog Breath A (Tiki Paralogue)"), ("IID_チキ_ブレス_通常_G001_弱", "Fog Breath B (Tiki Paralogue)"), ("IID_チキ_ブレス_通常_G001_強", "Fog Breath C (Tiki Paralogue)"),
            ("IID_チキ_ブレス_通常_G001_最強", "Fog Breath D (Tiki Paralogue)"),
        };

        internal static List<(string id, string name)> NormalCommonWeapons { get; } = new();

        internal static List<(string id, string name)> NormalAllyWeapons { get; } = new();

        internal static List<(string id, string name)> NormalEnemyWeapons { get; } = new();

        internal static List<(string id, string name)> NormalWeapons { get; } = new();

        internal static List<(string id, string name)> HealItems { get; } = new()
        {
            ("IID_傷薬", "Vulnerary"), ("IID_毒消し", "Antitoxin"), ("IID_特効薬", "Elixir"),
        };

        internal static List<(string id, string name)> EnemyUsableItems { get; } = new(); // HealItems

        internal static List<(string id, string name)> NonHealEnchantItems { get; } = new()
        {
            ("IID_聖水", "Pure Water"),
            ("IID_たいまつ", "Torch"), ("IID_HPの薬", "HP Tonic"), ("IID_力の薬", "Strength Tonic"), ("IID_技の薬", "Dexterity Tonic"),
            ("IID_速さの薬", "Speed Tonic"), ("IID_幸運の薬", "Luck Tonic"), ("IID_守備の薬", "Defense Tonic"), ("IID_魔力の薬", "Magic Tonic"),
            ("IID_魔防の薬", "Resistance Tonic"),
        };

        internal static List<(string id, string name)> EnchantItems { get; } = new(); // HealItems + NonHealEnchantItems

        internal static List<(string id, string name)> EnemyNonUsableItems { get; } = new() // NonHealEnchantItems +
        {
            ("IID_天使の衣", "Seraph Robe"), ("IID_力のしずく", "Energy Drop"), ("IID_精霊の粉", "Spirit Dust"),
            ("IID_秘伝の書", "Secret Book"), ("IID_はやての羽", "Speedwing"), ("IID_女神の像", "Goddess Icon"), ("IID_竜の盾", "Dracoshield"),
            ("IID_魔よけ", "Talisman"), ("IID_ブーツ", "Boots"), ("IID_スキルの書・守", "Novice Book"), ("IID_スキルの書・破", "Adept Book"),
            ("IID_スキルの書・離", "Expert Book"), ("IID_マスタープルフ", "Master Seal"), ("IID_チェンジプルフ", "Second Seal"), ("IID_エンチャント専用プルフ", "Mystic Satchel"),
            ("IID_マージカノン専用プルフ", "Mage Cannon"),
        };

        internal static List<(string id, string name)> AllyItems { get; } = new(); // EnemyUsableItems + EnemyNonUsableItems

        internal static List<(string id, string name)> GoldItems { get; } = new()
        {
            ("IID_100G", "100 Gold"), ("IID_200G", "200 Gold"), ("IID_300G", "300 Gold"), ("IID_400G", "400 Gold"),
            ("IID_500G", "500 Gold"), ("IID_600G", "600 Gold"), ("IID_700G", "700 Gold"), ("IID_800G", "800 Gold"),
            ("IID_900G", "900 Gold"), ("IID_1000G", "1000 Gold"), ("IID_1100G", "1100 Gold"), ("IID_1200G", "1200 Gold"),
            ("IID_1300G", "1300 Gold"), ("IID_1400G", "1400 Gold"), ("IID_1500G", "1500 Gold"), ("IID_1600G", "1600 Gold"),
            ("IID_1700G", "1700 Gold"), ("IID_1800G", "1800 Gold"), ("IID_1900G", "1900 Gold"), ("IID_2000G", "2000 Gold"),
            ("IID_3000G", "3000 Gold"), ("IID_5000G", "5000 Gold"), ("IID_7000G", "7000 Gold"), ("IID_10000G", "10000 Gold"),
            ("IID_20000G", "20000 Gold"), ("IID_50000G", "50000 Gold"),
        };

        internal static List<(string id, string name)> EnemyDropItems { get; } = new(); // EnemyNonUsableItems + GoldItems

        internal static List<(string id, string name)> EnemyItems { get; } = new(); // EnemyUsableItems + EnemyDropItems

        internal static List<(string id, string name)> AllItems { get; } = new();

        internal enum Proficiency
        {
            None, Sword, Lance, Axe, Bow, Dagger, Tome, Staff, Arts, Special
        }

        internal enum ProficiencyLevel
        {
            N, Np, D, Dp, C, Cp, B, Bp, A, Ap, S
        }

        internal static Dictionary<Proficiency, List<List<(string id, string name)>>> AllyWeaponLookup = new();
        internal static Dictionary<Proficiency, List<List<(string id, string name)>>> EnemyWeaponLookup = new();
        #endregion
        #region Map IDs
        internal static List<(string id, string name)> TempestTrialsMaps { get; } = new() // Okay, this might not be accurate. Let's just pretend these don't exist.
        {
            ("C001_1", "Tempest Trials Verdant Plain 1"), ("C001_2", "Tempest Trials Verdant Plain 2"), ("C001_3", "Tempest Trials Verdant Plain 3"),
            ("C002_1", "Tempest Trials Floral Field 1"), ("C002_2", "Tempest Trials Floral Field 2"), ("C002_3", "Tempest Trials Floral Field 3"),
            ("C003_1", "Tempest Trials Mountain Peak 1"), ("C003_2", "Tempest Trials Mountain Peak 2"), ("C003_3", "Tempest Trials Mountain Peak 3"),
            ("C004_1", "Tempest Trials Winter Forest 1"), ("C004_2", "Tempest Trials Winter Forest 2"), ("C004_3", "Tempest Trials Winter Forest 3"),
            ("C005_1", "Tempest Trials Desert Dunes 1"), ("C005_2", "Tempest Trials Desert Dunes 2"), ("C005_3", "Tempest Trials Desert Dunes 3"),
            ("C006_1", "Tempest Trials Vicious Volcano 1"), ("C006_2", "Tempest Trials Vicious Volcano 2"), ("C006_3", "Tempest Trials Vicious Volcano 3"),
        };

        internal static List<(string id, string name)> XenologueMaps { get; } = new()
        {
            ("E001", "Xenologue 1"), ("E002", "Xenologue 2"), ("E003", "Xenologue 3"), ("E004", "Xenologue 4"),
            ("E005", "Xenologue 5"), ("E006", "Xenologue 6"),
        };

        internal static List<(string id, string name)> DivineParalogueMaps { get; } = new()
        {
            ("G001", "Tiki Paralogue"), ("G002", "Hector Paralogue"), ("G003", "Veronica Paralogue"), ("G004", "Soren Paralogue"),
            ("G005", "Camilla Paralogue"), ("G006", "Chrom Paralogue"),
        };

        internal static List<(string id, string name)> ChapterMaps { get; } = new()
        {
            ("M001","Chapter 1"), ("M002","Chapter 2"), ("M003","Chapter 3"), ("M004","Chapter 4"),
            ("M005","Chapter 5"), ("M006","Chapter 6"), ("M007","Chapter 7"), ("M008","Chapter 8"),
            ("M009","Chapter 9"), ("M010","Chapter 10"), ("M011","Chapter 11"), ("M012","Chapter 12"),
            ("M013","Chapter 13"), ("M014","Chapter 14"), ("M015","Chapter 15"), ("M016","Chapter 16"),
            ("M017","Chapter 17"), ("M018","Chapter 18"), ("M019","Chapter 19"), ("M020","Chapter 20"),
            ("M021","Chapter 21"), ("M022","Chapter 22"), ("M023","Chapter 23"), ("M024","Chapter 24"),
            ("M025","Chapter 25"), ("M026","Chapter 26"),
        };

        internal static List<(string id, string name)> ParalogueMaps { get; } = new()
        {
            ("S001","Jean Paralogue"), ("S002","Anna Paralogue"), ("S003","Lucina Paralogue"), ("S004","Lyn Paralogue"),
            ("S005","Ike Paralogue"), ("S006","Byleth Paralogue"), ("S007","Corrin Paralogue"), ("S008","Eirika Paralogue"),
            ("S009","Sigurd Paralogue"), ("S010","Leif Paralogue"), ("S011","Micaiah Paralogue"), ("S012","Roy Paralogue"),
            ("S013","Celica Paralogue"), ("S014","Marth Paralogue"), ("S015","Alear Paralogue"),
        };

        internal static List<(string id, string name)> SkirmishMaps { get; } = new()
        {
            ("E001E", "Skirmish Xenologue 1"), ("E002E", "Skirmish Xenologue 2"), ("E003E", "Skirmish Xenologue 3"), ("E004E", "Skirmish Xenologue 4"),
            ("E005E", "Skirmish Xenologue 5"),
            ("G001E", "Skirmish Tiki Paralogue"), ("G002E", "Skirmish Hector Paralogue"), ("G003E", "Skirmish Veronica Paralogue"), ("G004E", "Skirmish Soren Paralogue"),
            ("G005E", "Skirmish Camilla Paralogue"), ("G006E", "Skirmish Chrom Paralogue"),
            ("M004E","Skirmish Chapter 4"),
            ("M005E","Skirmish Chapter 5"), ("M006E","Skirmish Chapter 6"), ("M007E","Skirmish Chapter 7"), ("M008E","Skirmish Chapter 8"),
            ("M009E","Skirmish Chapter 9"), ("M010E","Skirmish Chapter 10"), ("M011E","Skirmish Chapter 11"), ("M012E","Skirmish Chapter 12"),
            ("M013E","Skirmish Chapter 13"), ("M014E","Skirmish Chapter 14"), ("M015E","Skirmish Chapter 15"), ("M016E","Skirmish Chapter 16"),
            ("M017E","Skirmish Chapter 17"), ("M018E","Skirmish Chapter 18"), ("M019E","Skirmish Chapter 19"), ("M020E","Skirmish Chapter 20"),
            ("M022E","Skirmish Chapter 22"), ("M023E","Skirmish Chapter 23"),
            ("M025E","Skirmish Chapter 25"),
            ("S001E","Skirmish Jean Paralogue"), ("S002E","Skirmish Anna Paralogue"), ("S003E","Skirmish Lucina Paralogue"), ("S004E","Skirmish Lyn Paralogue"),
            ("S005E","Skirmish Ike Paralogue"), ("S006E","Skirmish Byleth Paralogue"), ("S007E","Skirmish Corrin Paralogue"), ("S008E","Skirmish Eirika Paralogue"),
            ("S009E","Skirmish Sigurd Paralogue"), ("S010E","Skirmish Leif Paralogue"), ("S011E","Skirmish Micaiah Paralogue"), ("S012E","Skirmish Roy Paralogue"),
            ("S013E","Skirmish Celica Paralogue"), ("S014E","Skirmish Marth Paralogue"), ("S015E","Skirmish Alear Paralogue"),
        };

        internal static List<(string id, string name)> StaticUnitMaps { get; } = new(); // FellXenologueMaps + DivineParalogueMaps + ChapterMaps + ParalogueMaps

        internal static List<(string id, string name)> AllMaps { get; } = new();
        // XenologueMaps + DivineParalogueMaps + ChapterMaps + ParalogueMaps + SkirmishMaps
        #endregion
        #region Ride Dress Model IDs
        internal static List<(string id, string name)> HorseRideDressModels { get; } = new()
        {
            ("uBody_Lnc2BR_c000", "Royal Knight Horse"), ("uBody_Lnc2BR_c707", "Corrupted Royal Knight Horse"),
            ("uBody_Amr2BR_c000", "Great Knight Horse"), ("uBody_Amr2BR_c707", "Corrupted Great Knight Horse"),
            ("uBody_Bow2BR_c000", "Bow Knight Horse"), ("uBody_Bow2BR_c707", "Corrupted Bow Knight Horse"),
            ("uBody_Cav0BR_c000", "Sword/Lance/Axe Cavalier Horse"), ("uBody_Cav0BR_c707", "Corrupted Sword/Lance/Axe Cavalier Horse"),
            ("uBody_Cav1BR_c000", "Paladin Horse"), ("uBody_Cav1BR_c707", "Corrupted Paladin Horse"),
            ("uBody_Mag2BR_c000", "Mage Knight Horse"), ("uBody_Mag2BR_c707", "Corrupted Mage Knight Horse"),
            ("uBody_Avn0BR_c100", "Avenir Horse"), ("uBody_Cpd0BR_c400", "Cupido Horse"),
            ("uBody_Sig0BR_c531", "Sigurd's Horse"), ("uBody_Sig0BR_c538", "Corrupted Sigurd's Horse"),
        };
        internal static List<(string id, string name)> PegasusRideDressModels { get; } = new()
        {
            ("uBody_Wng0ER_c000", "Sword/Lance/Axe Flier Pegasus"), ("uBody_Wng0ER_c707", "Corrupted Sword/Lance/Axe Flier Pegasus"),
            ("uBody_Slp0ER_c351", "Sleipnir Rider Pegasus"),
        };
        internal static List<(string id, string name)> WolfRideDressModels { get; } = new()
        {
            ("uBody_Cav2CR_c000", "Wolf Knight Wolf"), ("uBody_Cav2CR_c707", "Corrupted Wolf Knight Wolf"),
            ("uBody_Wlf0CT_c707", "Corrupted Wolf"), ("uBody_Wlf0CT_c715", "Phantom Wolf"),
            ("uBody_Wlf0CT_c751", "Rare Corrupted Wolf"), ("uBody_Cav2CR_c452", "Wolf Knight (Merrin) Wolf"),
        };
        internal static List<(string id, string name)> WyvernRideDressModels { get; } = new()
        {
            ("uBody_Wng2DR_c000", "Wyvern Knight Wyvern"), ("uBody_Wng2DR_c707", "Corrupted Wyvern Knight Wyvern"),
            ("uBody_Wng2DR_c303", "Wyvern Knight (Rosado) Wyvern"), ("uBody_Lnd0DR_c350", "Lindwurm Wyvern"),
            ("uBody_Msn0DR_c553", "Melusine (Zephia) Wyvern"), ("uBody_Msn0DR_c553b", "Melusine (Zelestia) Wyvern"),
            ("uBody_Cmi0DR_c561", "Camilla's Wyvern"), ("uBody_Cmi0DR_c568", "Corrupted Camilla's Wyvern"),
        };
        #endregion
        #region Skill IDs

        internal static List<(string id, string name)> TriggerAttackSkills { get; } = new()
        {
            ("SID_マルスエンゲージ技", "Lodestar Rush"), ("SID_マルスエンゲージ技_竜族", "Lodestar Rush [Dragon]"),
            ("SID_マルスエンゲージ技_連携", "Lodestar Rush [Backup]"), ("SID_マルスエンゲージ技_魔法", "Lodestar Rush [Mystical]"),
            ("SID_踏み込み", "Advance"),
            ("SID_ロイエンゲージ技", "Blazing Lion"), ("SID_ロイエンゲージ技_竜族", "Blazing Lion [Dragon]"),
            ("SID_ロイエンゲージ技_魔法", "Blazing Lion [Mystical]"), ("SID_リーフエンゲージ技", "Quadruple Hit"),
            ("SID_リーフエンゲージ技_竜族", "Quadruple Hit [Dragon]"), ("SID_リーフエンゲージ技_隠密", "Quadruple Hit [Covert]"),
            ("SID_リーフエンゲージ技_気功", "Quadruple Hit [Qi Adept]"),
            ("SID_リンエンゲージ技", "Astra Storm"), ("SID_リンエンゲージ技_竜族", "Astra Storm [Dragon]"),
            ("SID_リンエンゲージ技_隠密", "Astra Storm [Covert]"), ("SID_リンエンゲージ技_気功", "Astra Storm [Qi Adept]"),
            ("SID_リンエンゲージ技_威力減", "Weak Astra Storm"), ("SID_リンエンゲージ技_闇_気功", "Weak Astra Storm [Qi Adept]"),
            ("SID_カムイエンゲージ技", "Torrential Roar"), ("SID_カムイエンゲージ技_竜族", "Torrential Roar [Dragon]"),
            ("SID_切り抜け", "Run Through"),
            ("SID_幻月", "Paraselene"), ("SID_計略_引込の計", "Assembly Gambit"),
            ("SID_計略_猛火計", "Flame Gambit"), ("SID_計略_聖盾の備え", "Shield Gambit"),
            ("SID_計略_毒矢", "Poison Gambit"), ("SID_戦技_狂嵐", "Raging Storm"),
            ("SID_戦技_狂嵐_竜族", "Raging Storm [Dragon]"), ("SID_戦技_狂嵐_隠密", "Raging Storm [Covert]"),
            ("SID_戦技_無残", "Atrocity"), ("SID_戦技_無残_竜族", "Atrocity [Dragon]"),
            ("SID_戦技_無残_隠密", "Atrocity [Covert]"), ("SID_戦技_落星", "Fallen Star"),
            ("SID_戦技_落星_竜族", "Fallen Star [Dragon]"), ("SID_戦技_落星_隠密", "Fallen Star [Covert]"),
            ("SID_三級長エンゲージ技", "Houses Unite"), ("SID_三級長エンゲージ技_竜族", "Houses Unite [Dragon]"),
            ("SID_三級長エンゲージ技_騎馬", "Houses Unite [Cavalry]"), ("SID_三級長エンゲージ技_隠密", "Houses Unite [Covert]"),
            ("SID_三級長エンゲージ技_重装", "Houses Unite [Armored]"), ("SID_三級長エンゲージ技_気功", "Houses Unite [Qi Adept]"),
            ("SID_三級長エンゲージ技＋", "Houses Unite+"), ("SID_三級長エンゲージ技＋_竜族", "Houses Unite+ [Dragon]"),
            ("SID_三級長エンゲージ技＋_騎馬", "Houses Unite+ [Cavalry]"), ("SID_三級長エンゲージ技＋_隠密", "Houses Unite+ [Covert]"),
            ("SID_三級長エンゲージ技＋_重装", "Houses Unite+ [Armored]"), ("SID_三級長エンゲージ技＋_気功", "Houses Unite+ [Qi Adept]"),
            ("SID_セネリオエンゲージ技", "Cataclysm"), ("SID_セネリオエンゲージ技_竜族", "Cataclysm [Dragon]"),
            ("SID_セネリオエンゲージ技_魔法", "Cataclysm [Mystical]"), ("SID_セネリオエンゲージ技_気功", "Cataclysm [Qi Adept]"),
            ("SID_セネリオエンゲージ技＋", "Cataclysm+"), ("SID_セネリオエンゲージ技＋_竜族", "Cataclysm+ [Dragon]"),
            ("SID_セネリオエンゲージ技＋_魔法", "Cataclysm+ [Mystical]"), ("SID_セネリオエンゲージ技＋_気功", "Cataclysm+ [Qi Adept]"),
            ("SID_セネリオエンゲージ技_G004", "Cataclysm (Divine Paralogue)"), ("SID_セネリオエンゲージ技_闇", "Corrupted Cataclysm"),
            ("SID_全弾発射", "Let Fly")
        };

        internal static List<(string id, string name)> CompatibleAsEngageAttacks { get; } = new() // TriggerAttackSkills +
        {
            ("SID_シグルドエンゲージ技", "Override"), ("SID_シグルドエンゲージ技_竜族", "Override [Dragon]"),
            ("SID_シグルドエンゲージ技_重装", "Override [Armored]"), ("SID_シグルドエンゲージ技_魔法", "Override [Mystical]"),
            ("SID_シグルドエンゲージ技_気功", "Override [Qi Adept]"),
            ("SID_セリカエンゲージ技", "Warp Ragnarok"),
            ("SID_セリカエンゲージ技_竜族", "Warp Ragnarok [Dragon]"), ("SID_セリカエンゲージ技_騎馬", "Warp Ragnarok [Cavalry]"),
            ("SID_セリカエンゲージ技_飛行", "Warp Ragnarok [Flying]"), ("SID_セリカエンゲージ技_魔法", "Warp Ragnarok [Mystical]"),
            ("SID_セリカエンゲージ技_闇", "Dark Warp"), ("SID_セリカエンゲージ技_闇_M020", "Ragnarok Warp"),
            ("SID_ミカヤエンゲージ技", "Great Sacrifice"),
            ("SID_ミカヤエンゲージ技_竜族", "Great Sacrifice [Dragon]"), ("SID_ミカヤエンゲージ技_重装", "Great Sacrifice [Armored]"),
            ("SID_ミカヤエンゲージ技_気功", "Great Sacrifice [Qi Adept]"),
            ("SID_ルキナエンゲージ技", "All for One"), ("SID_ルキナエンゲージ技_竜族", "All for One [Dragon]"),
            ("SID_ルキナエンゲージ技_連携", "All for One [Backup]"),
            ("SID_アイクエンゲージ技", "Great Aether"), ("SID_アイクエンゲージ技_竜族", "Great Aether [Dragon]"),
            ("SID_アイクエンゲージ技_重装", "Great Aether [Armored]"), ("SID_アイクエンゲージ技_飛行", "Great Aether [Flying]"),
            ("SID_ベレトエンゲージ技", "Goddess Dance"), ("SID_ベレトエンゲージ技_竜族", "Goddess Dance [Dragon]"),
            ("SID_ベレトエンゲージ技_連携", "Goddess Dance [Backup]"), ("SID_ベレトエンゲージ技_騎馬", "Goddess Dance [Cavalry]"),
            ("SID_ベレトエンゲージ技_隠密", "Goddess Dance [Covert]"), ("SID_ベレトエンゲージ技_重装", "Goddess Dance [Armored]"),
            ("SID_ベレトエンゲージ技_飛行", "Goddess Dance [Flying]"), ("SID_ベレトエンゲージ技_魔法", "Goddess Dance [Mystical]"),
            ("SID_ベレトエンゲージ技_気功", "Goddess Dance [Qi Adept]"), ("SID_ベレトエンゲージ技_闇", "Diabolical Dance"),
            ("SID_エイリークエンゲージ技", "Twin Strike"), ("SID_エイリークエンゲージ技_竜族", "Twin Strike [Dragon]"),
            ("SID_エイリークエンゲージ技_騎馬", "Twin Strike [Cavalry]"),
            ("SID_リュールエンゲージ技", "Dragon Blast"),
            ("SID_リュールエンゲージ技_竜族", "Dragon Blast [Dragon]"), ("SID_リュールエンゲージ技_連携", "Dragon Blast [Backup]"),
            ("SID_リュールエンゲージ技_魔法", "Dragon Blast [Mystical]"), ("SID_リュールエンゲージ技_気功", "Dragon Blast [Qi Adept]"),
            ("SID_リュールエンゲージ技共同", "Bond Blast"), ("SID_リュールエンゲージ技共同_竜族", "Bond Blast [Dragon]"),
            ("SID_リュールエンゲージ技共同_連携", "Bond Blast [Backup]"), ("SID_リュールエンゲージ技共同_魔法", "Bond Blast [Mystical]"),
            ("SID_リュールエンゲージ技共同_気功", "Bond Blast [Qi Adept]"),
            ("SID_チキエンゲージ技", "Divine Blessing"), ("SID_チキエンゲージ技_竜族", "Divine Blessing [Dragon]"),
            ("SID_チキエンゲージ技_気功", "Divine Blessing [Qi Adept]"), ("SID_チキエンゲージ技_E001", "Divine Blessing (Xenologue 1)"),
            ("SID_チキエンゲージ技＋", "Divine Blessing+"),
            ("SID_チキエンゲージ技＋_竜族", "Divine Blessing+ [Dragon]"), ("SID_チキエンゲージ技＋_気功", "Divine Blessing+ [Qi Adept]"),
            ("SID_ヘクトルエンゲージ技", "Storm's Eye"), ("SID_ヘクトルエンゲージ技_竜族", "Storm's Eye [Dragon]"),
            ("SID_ヘクトルエンゲージ技_連携", "Storm's Eye [Backup]"), ("SID_ヘクトルエンゲージ技_隠密", "Storm's Eye [Covert]"),
            ("SID_ヘクトルエンゲージ技＋", "Storm's Eye+"), ("SID_ヘクトルエンゲージ技＋_竜族", "Storm's Eye+ [Dragon]"),
            ("SID_ヘクトルエンゲージ技＋_連携", "Storm's Eye+ [Backup]"), ("SID_ヘクトルエンゲージ技＋_隠密", "Storm's Eye+ [Covert]"),
            ("SID_ヴェロニカエンゲージ技", "Summon Hero"), ("SID_ヴェロニカエンゲージ技_竜族", "Summon Hero [Dragon]"),
            ("SID_ヴェロニカエンゲージ技_連携", "Summon Hero [Backup]"), ("SID_ヴェロニカエンゲージ技_騎馬", "Summon Hero [Cavalry]"),
            ("SID_カミラエンゲージ技", "Dark Inferno"), ("SID_カミラエンゲージ技_竜族", "Dark Inferno [Dragon]"),
            ("SID_カミラエンゲージ技_魔法", "Dark Inferno [Mystical]"), ("SID_カミラエンゲージ技_気功", "Dark Inferno [Qi Adept]"),
            ("SID_カミラエンゲージ技＋", "Dark Inferno+"), ("SID_カミラエンゲージ技＋_竜族", "Dark Inferno+ [Dragon]"),
            ("SID_カミラエンゲージ技＋_魔法", "Dark Inferno+ [Mystical]"), ("SID_カミラエンゲージ技＋_気功", "Dark Inferno+ [Qi Adept]"),
            ("SID_クロムエンゲージ技", "Giga Levin Sword"), ("SID_クロムエンゲージ技_竜族", "Giga Levin Sword [Dragon]"),
            ("SID_クロムエンゲージ技_飛行", "Giga Levin Sword [Flying]"), ("SID_クロムエンゲージ技_魔法", "Giga Levin Sword [Mystical]"),
            ("SID_クロムエンゲージ技＋", "Giga Levin Sword+"), ("SID_クロムエンゲージ技＋_竜族", "Giga Levin Sword+ [Dragon]"),
            ("SID_クロムエンゲージ技＋_飛行", "Giga Levin Sword+ [Flying]"), ("SID_クロムエンゲージ技＋_魔法", "Giga Levin Sword+ [Mystical]")
        };

        internal static List<(string id, string name)> BossSkills { get; } = new()
        {
            ("SID_ブレイク無効", "Unbreakable"), ("SID_特効耐性", "Stalwart"), ("SID_特効無効", "Unwavering"),
            ("SID_不動", "Anchor"), ("SID_熟練者", "Veteran"), ("SID_熟練者＋", "Veteran+"),
            ("SID_チェインアタック威力軽減", "Bond Breaker"),
            ("SID_チェインアタック威力軽減＋", "Bond Breaker+"),
        };

        internal static List<(string id, string name)> GeneralSkills { get; } = new() // TriggerAttackSkills + BossSkills +
        {
            ("SID_ＨＰ＋５_継承用", "HP +5"), ("SID_ＨＰ＋７_継承用", "HP +7"), ("SID_ＨＰ＋１０_継承用", "HP +10"), ("SID_ＨＰ＋１２_継承用", "HP +12"),
            ("SID_ＨＰ＋１５_継承用", "HP +15"), ("SID_力＋１_継承用", "Strength +1"), ("SID_力＋２_継承用", "Strength +2"), ("SID_力＋３_継承用", "Strength +3"),
            ("SID_力＋４_継承用", "Strength +4"), ("SID_力＋５_継承用", "Strength +5"), ("SID_力＋６_継承用", "Strength +6"), ("SID_技＋１_継承用", "Dexterity +1"),
            ("SID_技＋２_継承用", "Dexterity +2"), ("SID_技＋３_継承用", "Dexterity +3"), ("SID_技＋４_継承用", "Dexterity +4"), ("SID_技＋５_継承用", "Dexterity +5"),
            ("SID_速さ＋１_継承用", "Speed +1"), ("SID_速さ＋２_継承用", "Speed +2"), ("SID_速さ＋３_継承用", "Speed +3"), ("SID_速さ＋４_継承用", "Speed +4"),
            ("SID_速さ＋５_継承用", "Speed +5"), ("SID_幸運＋２_継承用", "Luck +2"), ("SID_幸運＋４_継承用", "Luck +4"), ("SID_幸運＋６_継承用", "Luck +6"),
            ("SID_幸運＋８_継承用", "Luck +8"), ("SID_幸運＋１０_継承用", "Luck +10"), ("SID_幸運＋１２_継承用", "Luck +12"), ("SID_守備＋１_継承用", "Defense +1"),
            ("SID_守備＋２_継承用", "Defense +2"), ("SID_守備＋３_継承用", "Defense +3"), ("SID_守備＋４_継承用", "Defense +4"), ("SID_守備＋５_継承用", "Defense +5"),
            ("SID_魔力＋２_継承用", "Magic +2"), ("SID_魔力＋３_継承用", "Magic +3"), ("SID_魔力＋４_継承用", "Magic +4"), ("SID_魔力＋５_継承用", "Magic +5"),
            ("SID_魔防＋２_継承用", "Resistance +2"), ("SID_魔防＋３_継承用", "Resistance +3"), ("SID_魔防＋４_継承用", "Resistance +4"), ("SID_魔防＋５_継承用", "Resistance +5"),
            ("SID_体格＋３_継承用", "Build +3"), ("SID_体格＋４_継承用", "Build +4"), ("SID_体格＋５_継承用", "Build +5"), ("SID_移動＋１_継承用", "Movement +1"),
            ("SID_蛇毒", "Poison Strike"), ("SID_死の吐息", "Savage Blow"), ("SID_剣殺し", "Swordbreaker"), ("SID_槍殺し", "Lancebreaker"),
            ("SID_斧殺し", "Axebreaker"), ("SID_魔殺し", "Tomebreaker"), ("SID_弓殺し", "Bowbreaker"), ("SID_短刀殺し", "Knifebreaker"),
            ("SID_気功殺し", "Artbreaker"), ("SID_力封じ", "Seal Strength"), ("SID_魔力封じ", "Seal Magic"), ("SID_守備封じ", "Seal Defense"),
            ("SID_速さ封じ", "Seal Speed"), ("SID_魔防封じ", "Seal Resistance"), ("SID_鬼神の構え", "Fierce Stance"), ("SID_金剛の構え", "Steady Stance"),
            ("SID_飛燕の構え", "Darting Stance"), ("SID_明鏡の構え", "Warding Stance"), ("SID_死線", "Life and Death"), ("SID_相性激化", "Triangle Adept"),
            ("SID_噛描", "Cornered Beast"), ("SID_自壊", "Self-Destruct"), ("SID_清流の一撃", "Duelist's Blow"), ("SID_飛燕の一撃", "Darting Blow"),
            ("SID_鬼神の一撃", "Death Blow"), ("SID_凶鳥の一撃", "Certain Blow"), ("SID_金剛の一撃", "Armored Blow"), ("SID_明鏡の一撃", "Warding Blow"),
            ("SID_狂乱の一撃", "Spirit Strike"),
            ("SID_虚無の呪い", "Void Curse"),
            ("SID_回避＋１０", "Avoid +10"), ("SID_回避＋１５", "Avoid +15"), ("SID_回避＋２０", "Avoid +20"), ("SID_回避＋２５", "Avoid +25"),
            ("SID_回避＋３０", "Avoid +30"), ("SID_命中＋１０", "Hit +10"), ("SID_命中＋１５", "Hit +15"), ("SID_命中＋２０", "Hit +20"),
            ("SID_命中＋２５", "Hit +25"), ("SID_命中＋３０", "Hit +30"), ("SID_必殺回避＋１０", "Dodge +10"), ("SID_必殺回避＋１５", "Dodge +15"),
            ("SID_必殺回避＋２０", "Dodge +20"), ("SID_必殺回避＋２５", "Dodge +25"), ("SID_必殺回避＋３０", "Dodge +30"), ("SID_剣術・柔１", "Sword Agility 1"),
            ("SID_剣術・柔２", "Sword Agility 2"), ("SID_剣術・柔３", "Sword Agility 3"), ("SID_剣術・柔４", "Sword Agility 4"), ("SID_剣術・柔５", "Sword Agility 5"),
            ("SID_剣術・剛１", "Sword Power 1"), ("SID_剣術・剛２", "Sword Power 2"), ("SID_剣術・剛３", "Sword Power 3"), ("SID_剣術・剛４", "Sword Power 4"),
            ("SID_剣術・剛５", "Sword Power 5"), ("SID_剣術・心１", "Sword Focus 1"), ("SID_剣術・心２", "Sword Focus 2"), ("SID_剣術・心３", "Sword Focus 3"),
            ("SID_剣術・心４", "Sword Focus 4"), ("SID_剣術・心５", "Sword Focus 5"), ("SID_槍術・柔１", "Lance Agility 1"), ("SID_槍術・柔２", "Lance Agility 2"),
            ("SID_槍術・柔３", "Lance Agility 3"), ("SID_槍術・柔４", "Lance Agility 4"), ("SID_槍術・柔５", "Lance Agility 5"), ("SID_槍術・剛１", "Lance Power 1"),
            ("SID_槍術・剛２", "Lance Power 2"), ("SID_槍術・剛３", "Lance Power 3"), ("SID_槍術・剛４", "Lance Power 4"), ("SID_槍術・剛５", "Lance Power 5"),
            ("SID_斧術・剛１", "Axe Power 1"), ("SID_斧術・剛２", "Axe Power 2"), ("SID_斧術・剛３", "Axe Power 3"), ("SID_斧術・剛４", "Axe Power 4"),
            ("SID_斧術・剛５", "Axe Power 5"), ("SID_弓術・柔１", "Bow Agility 1"), ("SID_弓術・柔２", "Bow Agility 2"), ("SID_弓術・柔３", "Bow Agility 3"),
            ("SID_弓術・柔４", "Bow Agility 4"), ("SID_弓術・柔５", "Bow Agility 5"), ("SID_弓術・心１", "Bow Focus 1"), ("SID_弓術・心２", "Bow Focus 2"),
            ("SID_弓術・心３", "Bow Focus 3"), ("SID_弓術・心４", "Bow Focus 4"), ("SID_弓術・心５", "Bow Focus 5"), ("SID_体術・心１", "Art Focus 1"),
            ("SID_体術・心２", "Art Focus 2"), ("SID_体術・心３", "Art Focus 3"), ("SID_体術・心４", "Art Focus 4"), ("SID_体術・心５", "Art Focus 5"),
            ("SID_短剣術１", "Knife Precision 1"), ("SID_短剣術２", "Knife Precision 2"), ("SID_短剣術３", "Knife Precision 3"), ("SID_短剣術４", "Knife Precision 4"),
            ("SID_短剣術５", "Knife Precision 5"), ("SID_魔道１", "Tome Precision 1"), ("SID_魔道２", "Tome Precision 2"), ("SID_魔道３", "Tome Precision 3"),
            ("SID_魔道４", "Tome Precision 4"), ("SID_魔道５", "Tome Precision 5"), ("SID_信仰１", "Staff Mastery 1"), ("SID_信仰２", "Staff Mastery 2"),
            ("SID_信仰３", "Staff Mastery 3"), ("SID_信仰４", "Staff Mastery 4"), ("SID_信仰５", "Staff Mastery 5"), ("SID_見切り", "Perceptive"),
            ("SID_見切り＋", "Perceptive+"), ("SID_ブレイク時追撃", "Break Defenses"), ("SID_不屈", "Unyielding"), ("SID_不屈＋", "Unyielding+"),
            ("SID_不屈＋＋", "Unyielding++"), ("SID_カウンター", "Divine Speed"), ("SID_カウンター_隠密", "Divine Speed [Covert]"), ("SID_カウンター_竜族", "Divine Speed [Dragon]"),
            ("SID_再移動", "Canter"), ("SID_再移動＋", "Canter+"), ("SID_助走", "Momentum"), ("SID_助走＋", "Momentum+"),
            ("SID_猛進", "Headlong Rush"), ("SID_迅走", "Gallop"), ("SID_迅走_竜族", "Gallop [Dragon]"), ("SID_迅走_騎馬", "Gallop [Cavalry]"),
            ("SID_迅走_隠密", "Gallop [Covert]"), ("SID_迅走_闇", "Dark Gallop"), ("SID_異形リベンジ", "Holy Stance"), ("SID_異形リベンジ＋", "Holy Stance+"),
            ("SID_異形リベンジ＋＋", "Holy Stance++"), ("SID_異形リベンジ＋＋_闇", "Unholy Stance"), ("SID_共鳴の黒魔法", "Resonance"), ("SID_共鳴の黒魔法＋", "Resonance+"),
            ("SID_大好物", "Favorite Food"), ("SID_重唱", "Echo"), ("SID_重唱_竜族", "Echo [Dragon]"), ("SID_重唱_魔法", "Echo [Mystical]"),
            ("SID_杖使い", "Cleric"), ("SID_杖使い＋", "Cleric+"), ("SID_杖使い＋＋", "Cleric++"), ("SID_サイレス無効", "Silence Ward"),
            ("SID_癒しの響き", "Healing Light"), ("SID_増幅", "Augment"), ("SID_増幅_竜族", "Augment [Dragon]"), ("SID_増幅_気功", "Augment [Qi Adept]"),
            ("SID_増幅_闇", "Dark Augment"), ("SID_踏ん張り", "Hold Out"), ("SID_踏ん張り＋", "Hold Out+"), ("SID_踏ん張り＋＋", "Hold Out++"),
            ("SID_踏ん張り＋＋＋", "Hold Out+++ "), ("SID_超越", "Rise Above"), ("SID_超越_竜族", "Rise Above [Dragon]"), ("SID_超越_騎馬", "Rise Above [Cavalry]"),
            ("SID_超越_重装", "Rise Above [Armored]"), ("SID_超越_闇", "Sink Below"), ("SID_武器相性激化", "Arms Shield"), ("SID_武器相性激化＋", "Arms Shield+"),
            ("SID_武器相性激化＋＋", "Arms Shield++"), ("SID_待ち伏せ", "Vantage"), ("SID_待ち伏せ＋", "Vantage+"), ("SID_待ち伏せ＋＋", "Vantage++"),
            ("SID_順応", "Adaptable"), ("SID_順応_竜族", "Adaptable [Dragon]"), ("SID_順応_連携", "Adaptable [Backup]"), ("SID_順応_隠密", "Adaptable [Covert]"),
            ("SID_順応_重装", "Adaptable [Armored]"), ("SID_順応_飛行", "Adaptable [Flying]"), ("SID_絆の力", "Dual Strike"), ("SID_デュアルアシスト", "Dual Assist"),
            ("SID_デュアルアシスト＋", "Dual Assist+"), ("SID_デュアルサポート", "Dual Support"), ("SID_絆盾", "Bonded Shield"), ("SID_絆盾_竜族", "Bonded Shield [Dragon]"),
            ("SID_絆盾_騎馬", "Bonded Shield [Cavalry]"), ("SID_絆盾_重装", "Bonded Shield [Armored]"), ("SID_絆盾_飛行", "Bonded Shield [Flying]"), ("SID_絆盾_気功", "Bonded Shield [Qi Adept]"),
            ("SID_攻め立て", "Alacrity"), ("SID_攻め立て＋", "Alacrity+"), ("SID_攻め立て＋＋", "Alacrity++"), ("SID_速さの吸収", "Speedtaker"),
            ("SID_残像", "Call Doubles"), ("SID_残像_竜族", "Call Doubles [Dragon]"), ("SID_残像_飛行", "Call Doubles [Flying]"), ("SID_破壊", "Demolish"),
            ("SID_引き戻し", "Reposition"), ("SID_怒り", "Wrath"), ("SID_勇将", "Resolve"), ("SID_勇将＋", "Resolve+"),
            ("SID_アイクエンゲージスキル", "Laguz Friend"), ("SID_アイクエンゲージスキル_竜族", "Laguz Friend [Dragon]"), ("SID_天刻の拍動", "Divine Pulse"), ("SID_天刻の拍動＋", "Divine Pulse+"),
            ("SID_師の導き", "Mentorship"), ("SID_拾得", "Lost & Found "), ("SID_先生", "Instruct"), ("SID_先生_竜族", "Instruct [Dragon]"),
            ("SID_先生_連携", "Instruct [Backup]"), ("SID_先生_騎馬", "Instruct [Cavalry]"), ("SID_先生_隠密", "Instruct [Covert]"), ("SID_先生_重装", "Instruct [Armored]"),
            ("SID_先生_飛行", "Instruct [Flying]"), ("SID_先生_魔法", "Instruct [Mystical]"), ("SID_先生_気功", "Instruct [Qi Adept]"), ("SID_竜脈", "Dragon Vein (Corrin)"),
            ("SID_竜脈_竜族", "Dragon Vein (Corrin) [Dragon]"), ("SID_竜脈_連携", "Dragon Vein (Corrin) [Backup]"), ("SID_竜脈_騎馬", "Dragon Vein (Corrin) [Cavalry]"), ("SID_竜脈_隠密", "Dragon Vein (Corrin) [Covert]"),
            ("SID_竜脈_重装", "Dragon Vein (Corrin) [Armored]"), ("SID_竜脈_飛行", "Dragon Vein (Corrin) [Flying]"), ("SID_竜脈_魔法", "Dragon Vein (Corrin) [Mystical]"), ("SID_竜脈_気功", "Dragon Vein (Corrin) [Qi Adept]"),
            ("SID_竜呪", "Draconic Hex"), ("SID_防陣", "Pair Up"), ("SID_スキンシップ", "Quality Time"), ("SID_スキンシップ＋", "Quality Time+"),
            ("SID_呪縛", "Dreadful Aura"), ("SID_呪縛_隠密", "Dreadful Aura [Covert]"),
            ("SID_月の腕輪", "Lunar Brace"), ("SID_太陽の腕輪", "Solar Brace"), ("SID_日月の腕輪", "Eclipse Brace"), ("SID_月の腕輪＋", "Lunar Brace+"),
            ("SID_太陽の腕輪＋", "Solar Brace+"), ("SID_日月の腕輪＋", "Eclipse Brace+"), ("SID_優風", "Gentility"), ("SID_勇空", "Bravery"),
            ("SID_蒼穹", "Blue Skies"), ("SID_優風＋", "Gentility+"), ("SID_勇空＋", "Bravery+"), ("SID_蒼穹＋", "Blue Skies+"),
            ("SID_リュール邪竜特効", "Holy Aura"), ("SID_神竜の加護", "Holy Shield"), ("SID_絆を繋薙くもの", "Bond Forger"), ("SID_絆を繋薙くもの＋", "Bond Forger+"),
            ("SID_エレオスの祝福", "Boon of Elyos"), ("SID_神竜の結束", "Divinely Inspiring"), ("SID_白の忠誠", "Alabaster Duty"),
            ("SID_碧の信仰", "Verdant Faith"), ("SID_緋い声援", "Crimson Cheer"), ("SID_自己研鑽", "Self-Improver"), ("SID_筋肉増強剤", "Energized"),
            ("SID_涙腺崩壊", "Moved to Tears"), ("SID_平和の花", "Gentle Flower"), ("SID_絵になる二人", "Fairy-Tale Folk"), ("SID_花園の門番", "Admiration"),
            ("SID_真っ向勝負", "Fair Fight"), ("SID_名乗り上げ", "Aspiring Hero"), ("SID_瞑想", "Meditation"), ("SID_僕が守ります！", "Get Behind Me!"),
            ("SID_戦果委譲", "Share Spoils"), ("SID_大盤振る舞い", "Generosity"), ("SID_執着", "Single-Minded"), ("SID_『次』はない", "Not *Quite*"),
            ("SID_光彩奪目！", "Blinding Flash"), ("SID_煌めく理力", "Big Personality"), ("SID_微笑み", "Stunning Smile"), ("SID_溜め息", "Disarming Sigh"),
            ("SID_ソルムの騒音", "Racket of Solm"), ("SID_エスコート", "Knightly Escort"), ("SID_戦の血", "Blood Fury"), ("SID_人たらし", "Charmer"),
            ("SID_大集会", "Party Animal"), ("SID_料理再現", "Seconds?"), ("SID_一攫千金", "Make a Killing"), ("SID_努力の才", "Expertise"),
            ("SID_殺しの技術", "Trained to Kill"), ("SID_神秘の踊り", "Curious Dance"), ("SID_歴戦の勘", "Weapon Insight"), ("SID_勝利への意思", "Will to Win"),
            ("SID_邪竜の救済", "Fell Protection"), ("SID_次への備え", "Contemplative"), ("SID_助太刀", "Brave Assist"), ("SID_挟撃", "Pincer Attack"),
            ("SID_手助け", "Reforge"), ("SID_スマッシュ＋", "Smash+"), ("SID_無慈悲", "Merciless"), ("SID_集中", "No Distractions"),
            ("SID_狙撃", "Careful Aim"), ("SID_入れ替え", "Swap"), ("SID_護衛", "Allied Defense"), ("SID_回り込み", "Pivot"),
            ("SID_足狙い", "Hobble"), ("SID_移動補助", "Clear the Way"), ("SID_急襲", "Air Raid"), ("SID_すり抜け", "Pass"),
            ("SID_魔力増幅", "Spell Harmony"), ("SID_背理の法", "Chaos Style"), ("SID_気の拡散", "Diffuse Healer"), ("SID_自己回復", "Self-Healing"),
            ("SID_神竜気", "Divine Spirit"), ("SID_邪竜気", "Fell Spirit"), ("SID_邪竜気・闇", "Dark Spirit"), ("SID_金蓮", "Golden Lotus"),
            ("SID_華炎", "Ignis"), ("SID_太陽", "Sol"), ("SID_月光", "Luna"), ("SID_虚空", "Grasping Void"),
            ("SID_大樹", "World Tree"), ("SID_砂陣", "Sandstorm"), ("SID_水鏡", "Back at You"), ("SID_魔法剣", "Soulblade"),
            ("SID_特別な踊り", "Special Dance"), ("SID_伝道師", "Sympathetic"), ("SID_必殺剣", "Deadly Blade"), ("SID_百戦練磨", "Battlewise"),
            ("SID_回復", "Renewal"), ("SID_風薙ぎ", "Windsweep"), ("SID_轟雷", "Great Thunder"), ("SID_瞬殺", "Bane"),
            ("SID_慈悲", "Mercy"), ("SID_業火", "Raging Fire"), ("SID_剛腕", "Strong Arm"), ("SID_祈り", "Miracle"),
            ("SID_ダイムサンダ", "Dire Thunder"), ("SID_王の器", "Rightful Ruler"), ("SID_いやしの心", "Healtouch"), ("SID_引き寄せ", "Draw Back"),
            ("SID_ギガスカリバー", "Giga Excalibur"), ("SID_旋風", "Wind Adept"), ("SID_体当たり", "Shove"), ("SID_閃花", "Flickering Flower"),
            ("SID_風神", "Wind God"), ("SID_騎士道", "Chivalry"), ("SID_武士道", "Bushido"), ("SID_滅殺", "Lethality"),
            ("SID_必的", "Sure Strike"), ("SID_絆の指輪_アルフォンス", "Spur Attack"), ("SID_絆の指輪_シャロン", "Fortify Def"), ("SID_絆の指輪_アンナ", "Spur Res"),
            ("SID_武器シンクロ", "Weapon Sync"), ("SID_武器シンクロ＋", "Weapon Sync+"),
            ("SID_血統", "Lineage"), ("SID_戦技", "Combat Arts"), ("SID_戦技_竜族", "Combat Arts [Dragon]"), ("SID_戦技_隠密", "Combat Arts [Covert]"),
            ("SID_光玉の加護", "Lightsphere"), ("SID_星玉の加護", "Starsphere"), ("SID_命玉の加護", "Lifesphere"), ("SID_命玉の加護＋", "Lifesphere+"),
            ("SID_命玉の加護＋＋", "Lifesphere++"), ("SID_地玉の加護", "Geosphere"), ("SID_地玉の加護＋", "Geosphere+"), ("SID_竜化", "Draconic Form"),
            ("SID_竜化_重装", "Draconic Form [Armored]"), ("SID_竜化_魔法", "Draconic Form [Mystical]"), ("SID_切り返し", "Quick Riposte"), ("SID_切り返し＋", "Quick Riposte+"),
            ("SID_重撃", "Heavy Attack"), ("SID_角の睨み", "Piercing Glare"), ("SID_適応能力", "Adaptability"), ("SID_適応能力＋", "Adaptability+"),
            ("SID_鉄壁", "Impenetrable"), ("SID_鉄壁_竜族", "Impenetrable [Dragon]"), ("SID_鉄壁_騎馬", "Impenetrable [Cavalry]"), ("SID_鉄壁_重装", "Impenetrable [Armored]"),
            ("SID_鉄壁_飛行", "Impenetrable [Flying]"), ("SID_SPコンバート", "SP Conversion"), ("SID_血讐", "Reprisal"), ("SID_血讐＋", "Reprisal+"),
            ("SID_限界突破", "Level Boost"), ("SID_異界の力", "Book of Worlds"), ("SID_契約", "Contract"), ("SID_契約_竜族", "Contract [Dragon]"),
            ("SID_契約_連携", "Contract [Backup]"), ("SID_契約_隠密", "Contract [Covert]"), ("SID_理魔法＋", "Anima Focus"), ("SID_慧眼", "Keen Insight"),
            ("SID_慧眼＋", "Keen Insight+"), ("SID_囮指名", "Assign Decoy"), ("SID_復帰阻止", "Block Recovery"), ("SID_陽光", "Flare"),
            ("SID_陽光_竜族", "Flare [Dragon]"), ("SID_陽光_魔法", "Flare [Mystical]"), ("SID_陽光_気功", "Flare [Qi Adept]"), ("SID_陽光_闇", "Corrupted Flare"),
            ("SID_竜脈・異", "Dragon Vein (Camilla)"), ("SID_竜脈・異_竜族", "Dragon Vein (Camilla) [Dragon]"), ("SID_竜脈・異_連携", "Dragon Vein (Camilla) [Backup]"), ("SID_竜脈・異_騎馬", "Dragon Vein (Camilla) [Cavalry]"),
            ("SID_竜脈・異_隠密", "Dragon Vein (Camilla) [Covert]"), ("SID_竜脈・異_重装", "Dragon Vein (Camilla) [Armored]"), ("SID_竜脈・異_飛行", "Dragon Vein (Camilla) [Flying]"), ("SID_竜脈・異_魔法", "Dragon Vein (Camilla) [Mystical]"),
            ("SID_竜脈・異_気功", "Dragon Vein (Camilla) [Qi Adept]"), ("SID_地脈吸収", "Groundswell"), ("SID_デトックス", "Detoxify"), ("SID_後始末", "Decisive Strike"),
            ("SID_後始末＋", "Decisive Strike+"), ("SID_天駆", "Soar"), ("SID_天駆_竜族", "Soar [Dragon]"), ("SID_天駆_騎馬", "Soar [Cavalry]"),
            ("SID_天駆_飛行", "Soar [Flying]"), ("SID_力まかせ", "Brute Force"), ("SID_カリスマ", "Charm"), ("SID_不意打ち", "Surprise Attack"),
            ("SID_七色の叫び", "Rally Spectrum"), ("SID_七色の叫び＋", "Rally Spectrum+"), ("SID_半身_単身用", "Other Half (Arena)"),
            ("SID_力・技＋１", "Str/Dex +1"), ("SID_力・技＋２", "Str/Dex +2"), ("SID_力・技＋３", "Str/Dex +3"), ("SID_力・技＋４", "Str/Dex +4"),
            ("SID_力・技＋５", "Str/Dex +5"), ("SID_ＨＰ・幸運＋２", "HP/Lck +2"), ("SID_ＨＰ・幸運＋４", "HP/Lck +4"), ("SID_ＨＰ・幸運＋６", "HP/Lck +6"),
            ("SID_ＨＰ・幸運＋８", "HP/Lck +8"), ("SID_ＨＰ・幸運＋１０", "HP/Lck +10"), ("SID_力・守備＋１", "Str/Def +1"), ("SID_力・守備＋２", "Str/Def +2"),
            ("SID_力・守備＋３", "Str/Def +3"), ("SID_力・守備＋４", "Str/Def +4"), ("SID_力・守備＋５", "Str/Def +5"), ("SID_魔力・技＋１", "Mag/Dex +1"),
            ("SID_魔力・技＋２", "Mag/Dex +2"), ("SID_魔力・技＋３", "Mag/Dex +3"), ("SID_魔力・技＋４", "Mag/Dex +4"), ("SID_魔力・技＋５", "Mag/Dex +5"),
            ("SID_魔力・魔防＋１", "Mag/Res +1"), ("SID_魔力・魔防＋２", "Mag/Res +2"), ("SID_魔力・魔防＋３", "Mag/Res +3"), ("SID_魔力・魔防＋４", "Mag/Res +4"),
            ("SID_魔力・魔防＋５", "Mag/Res +5"), ("SID_速さ・魔防＋１", "Spd/Res +1"), ("SID_速さ・魔防＋２", "Spd/Res +2"), ("SID_速さ・魔防＋３", "Spd/Res +3"),
            ("SID_速さ・魔防＋４", "Spd/Res +4"), ("SID_速さ・魔防＋５", "Spd/Res +5"), ("SID_技・速さ＋１", "Spd/Dex +1"), ("SID_技・速さ＋２", "Spd/Dex +2"),
            ("SID_技・速さ＋３", "Spd/Dex +3"), ("SID_技・速さ＋４", "Spd/Dex +4"), ("SID_技・速さ＋５", "Spd/Dex +5"), ("SID_対弓術１", "Bow Guard 1"),
            ("SID_対弓術２", "Bow Guard 2"), ("SID_対弓術３", "Bow Guard 3"), ("SID_対弓術４", "Bow Guard 4"), ("SID_対弓術５", "Bow Guard 5"),
            ("SID_対特殊１", "Special Guard 1"), ("SID_対特殊２", "Special Guard 2"), ("SID_対特殊３", "Special Guard 3"), ("SID_対特殊４", "Special Guard 4"),
            ("SID_対特殊５", "Special Guard 5"), ("SID_対斧術１", "Axe Guard 1"), ("SID_対斧術２", "Axe Guard 2"), ("SID_対斧術３", "Axe Guard 3"),
            ("SID_対斧術４", "Axe Guard 4"), ("SID_対斧術５", "Axe Guard 5"), ("SID_対短剣術１", "Knife Guard 1"), ("SID_対短剣術２", "Knife Guard 2"),
            ("SID_対短剣術３", "Knife Guard 3"), ("SID_対短剣術４", "Knife Guard 4"), ("SID_対短剣術５", "Knife Guard 5"), ("SID_対魔道１", "Magic Guard 1"),
            ("SID_対魔道２", "Magic Guard 2"), ("SID_対魔道３", "Magic Guard 3"), ("SID_対魔道４", "Magic Guard 4"), ("SID_対魔道５", "Magic Guard 5"),
            ("SID_対槍術１", "Lance Guard 1"), ("SID_対槍術２", "Lance Guard 2"), ("SID_対槍術３", "Lance Guard 3"), ("SID_対槍術４", "Lance Guard 4"),
            ("SID_対槍術５", "Lance Guard 5"), ("SID_対剣術１", "Sword Guard 1"), ("SID_対剣術２", "Sword Guard 2"), ("SID_対剣術３", "Sword Guard 3"),
            ("SID_対剣術４", "Sword Guard 4"), ("SID_対剣術５", "Sword Guard 5"), ("SID_守護者", "Protective"), ("SID_負けず嫌い", "Rivalry"),
            ("SID_ムードメーカー", "Friendly Boost"), ("SID_生存戦略", "Survival Plan"), ("SID_理想の騎士像", "Knightly Code"),
            ("SID_保身", "Self-Defense"), ("SID_戦場の花", "Fierce Bloom"), ("SID_促す決着", "This Ends Here"), ("SID_能力誇示", "Show-Off"),
            ("SID_傷をつけたわね", "Final Say"), ("SID_密かな支援", "Stealth Assist"), ("SID_王の尊厳", "Dignity of Solm"), ("SID_殺戮者", "Wear Down"),
            ("SID_輸送隊", "Convoy"), ("SID_瘴気の領域", "Miasma Domain "), ("SID_氷の領域", "Frost Domain"), ("SID_裏邪竜ノ娘_兵種スキル", "Resist Emblems"), ("SID_裏邪竜ノ子_兵種スキル", "Spur Emblems"),
            ("SID_異形狼連携", "Pack Hunter (Corrupted)"),
            ("SID_幻影狼連携", "Pack Hunter (Phantom)"),
        };

        internal static List<(string id, string name)> VisibleSkills { get; } = new(); // GeneralSkills + RestrictedSkills

        internal static List<(string id, string name)> RestrictedSkills { get; } = new()
        {
            ("SID_バリア１", "Fell Barrier"), ("SID_バリア２", "Fell Barrier+"), ("SID_バリア３", "Fell Barrier++"), ("SID_バリア４", "Fell Barrier+++"), ("SID_バリア１_ノーマル用", "Dark Barrier"),
            ("SID_バリア２_ノーマル用", "Dark Barrier+"), ("SID_バリア３_ノーマル用", "Dark Barrier++"), ("SID_バリア４_ノーマル用", "Dark Barrier+++"),
            ("SID_双聖", "Sacred Twins"), ("SID_オルタネイト", "Night and Day"),
            ("SID_以心", "Attuned"), ("SID_以心_竜族", "Attuned [Dragon]"), ("SID_以心_連携", "Attuned [Backup]"),
            ("SID_以心_騎馬", "Attuned [Cavalry]"), ("SID_以心_隠密", "Attuned [Covert]"), ("SID_以心_重装", "Attuned [Armored]"), ("SID_以心_飛行", "Attuned [Flying]"),
            ("SID_以心_魔法", "Attuned [Mystical]"), ("SID_以心_気功", "Attuned [Qi Adept]"),
            ("SID_切磋琢磨", "Friendly Rivalry"), ("SID_計略", "Gambit"), ("SID_半身", "Other Half"), ("SID_半身_竜族", "Other Half [Dragon]"),
            ("SID_半身_連携", "Other Half [Backup]"), ("SID_半身_隠密", "Other Half [Covert]"), ("SID_半身_連携_G006_クロム", "Corrupted Other Half"),
            ("SID_守護者_E001", "Protective (Xenologue 1)"),
            ("SID_守護者_E002", "Protective (Xenologue 2)"), ("SID_守護者_E003", "Protective (Xenologue 3)"),
            ("SID_守護者_E004", "Protective (Xenologue 4)"),
            ("SID_役に立ちたい_E001", "Wounded Pride (Xenologue 1)"), ("SID_役に立ちたい_E002", "Wounded Pride (Xenologue 2)"), ("SID_役に立ちたい_E003", "Wounded Pride (Xenologue 3)"), ("SID_役に立ちたい_E004", "Wounded Pride (Xenologue 4)"),
            ("SID_負けず嫌い_E005", "Rivalry (Xenologue 5)"),
        };

        internal static List<(string id, string name)> SyncHPSkills { get; } = new()
        {
            ("SID_ＨＰ＋３", "HP +3"), ("SID_ＨＰ＋５", "HP +5"), ("SID_ＨＰ＋７", "HP +7"), ("SID_ＨＰ＋１０", "HP +10"),
            ("SID_ＨＰ＋１２", "HP +12"), ("SID_ＨＰ＋１５", "HP +15")
        };

        internal static List<(string id, string name)> SyncStrSkills { get; } = new()
        {
            ("SID_力＋１", "Strength +1"), ("SID_力＋２", "Strength +2"), ("SID_力＋３", "Strength +3"), ("SID_力＋４", "Strength +4"),
            ("SID_力＋５", "Strength +5"), ("SID_力＋６", "Strength +6")
        };

        internal static List<(string id, string name)> SyncDexSkills { get; } = new()
        {
            ("SID_技＋１", "Dexterity +1"), ("SID_技＋２", "Dexterity +2"), ("SID_技＋３", "Dexterity +3"), ("SID_技＋４", "Dexterity +4 "),
            ("SID_技＋５", "Dexterity +5")
        };

        internal static List<(string id, string name)> SyncSpdSkills { get; } = new()
        {
            ("SID_速さ＋１", "Speed +1"), ("SID_速さ＋２", "Speed +2"), ("SID_速さ＋３", "Speed +3"), ("SID_速さ＋４", "Speed +4"),
            ("SID_速さ＋５", "Speed +5")
        };

        internal static List<(string id, string name)> SyncLckSkills { get; } = new()
        {
            ("SID_幸運＋２", "Luck +2"), ("SID_幸運＋４", "Luck +4"), ("SID_幸運＋６", "Luck +6"), ("SID_幸運＋８", "Luck +8"),
            ("SID_幸運＋１０", "Luck +10"), ("SID_幸運＋１２", "Luck +12")
        };

        internal static List<(string id, string name)> SyncDefSkills { get; } = new()
        {
            ("SID_守備＋１", "Defense +1"), ("SID_守備＋２", "Defense +2"), ("SID_守備＋３", "Defense +3"), ("SID_守備＋４", "Defense +4"),
            ("SID_守備＋５", "Defense +5")
        };

        internal static List<(string id, string name)> SyncMagSkills { get; } = new()
        {
            ("SID_魔力＋１", "Magic +1"), ("SID_魔力＋２", "Magic +2"), ("SID_魔力＋３", "Magic +3"), ("SID_魔力＋４", "Magic +4"),
            ("SID_魔力＋５", "Magic +5")
        };

        internal static List<(string id, string name)> SyncResSkills { get; } = new()
        {
            ("SID_魔防＋１", "Resistance +1"), ("SID_魔防＋２", "Resistance +2"), ("SID_魔防＋３", "Resistance +3"), ("SID_魔防＋４", "Resistance +4"),
            ("SID_魔防＋５", "Resistance +5")
        };

        internal static List<(string id, string name)> SyncBldSkills { get; } = new()
        {
            ("SID_体格＋１", "Build +1"), ("SID_体格＋２", "Build +2"), ("SID_体格＋３", "Build +3"), ("SID_体格＋４", "Build +4"),
            ("SID_体格＋５", "Build +5 ")
        };

        internal static List<(string id, string name)> SyncMovSkills { get; } = new()
        {
            ("SID_移動＋１", "Movement +1")
        };

        internal static List<(string id, string name)> SyncStatSkills { get; } = new();

        internal static Dictionary<string, ushort> DefaultSPCost { get; } = new()
        {
            { "Movement +1", 1000 }, { "Poison Strike", 300 }, { "Savage Blow", 300 }, { "Swordbreaker", 300 },
            { "Lancebreaker", 300 }, { "Axebreaker", 300 }, { "Tomebreaker", 300 }, { "Bowbreaker", 300 },
            { "Knifebreaker", 300 }, { "Artbreaker", 300 }, { "Seal Strength", 300 }, { "Seal Magic", 300 },
            { "Seal Defense", 300 }, { "Seal Speed", 300 }, { "Seal Resistance", 300 }, { "Fierce Stance", 300 },
            { "Steady Stance", 300 }, { "Darting Stance", 300 }, { "Warding Stance", 300 }, { "Life and Death", 300 },
            { "Triangle Adept", 300 }, { "Cornered Beast", 300 }, { "Self-Destruct", 100 }, { "Duelist's Blow", 1000 },
            { "Darting Blow", 1000 }, { "Death Blow", 1000 }, { "Certain Blow", 1000 }, { "Armored Blow", 1000 },
            { "Warding Blow", 1000 }, { "Spirit Strike", 100 }, { "Unbreakable", 100 }, { "Stalwart", 100 },
            { "Unwavering", 300 }, { "Anchor", 100 }, { "Veteran", 300 }, { "Veteran+", 500 },
            { "Void Curse", 100 }, { "Fell Barrier", 6000 }, { "Fell Barrier+", 8000 }, { "Fell Barrier++", 10000 },
            { "Fell Barrier+++", 12000 }, { "Dark Barrier", 2000 }, { "Dark Barrier+", 4000 }, { "Dark Barrier++", 6000 },
            { "Dark Barrier+++", 8000 }, { "Bond Breaker", 1000 }, { "Divine Speed", 3000 }, { "Divine Speed [Covert]", 3000 },
            { "Divine Speed [Dragon]", 3000 }, { "Gallop", 5000 }, { "Gallop [Dragon]", 5000 }, { "Gallop [Cavalry]", 5000 },
            { "Gallop [Covert]", 5000 }, { "Dark Gallop", 5000 }, { "Unholy Stance", 1000 }, { "Echo", 300 },
            { "Echo [Dragon]", 300 }, { "Echo [Mystical]", 300 }, { "Cleric", 300 }, { "Cleric+", 400 },
            { "Cleric++", 500 }, { "Augment", 3000 }, { "Augment [Dragon]", 3000 }, { "Augment [Qi Adept]", 3000 },
            { "Dark Augment", 3000 }, { "Rise Above", 3000 }, { "Rise Above [Dragon]", 3000 }, { "Rise Above [Cavalry]", 3000 },
            { "Rise Above [Armored]", 3000 }, { "Sink Below", 3000 }, { "Adaptable", 300 }, { "Adaptable [Dragon]", 300 },
            { "Adaptable [Backup]", 300 }, { "Adaptable [Covert]", 300 }, { "Adaptable [Armored]", 300 }, { "Adaptable [Flying]", 300 },
            { "Dual Strike", 1000 }, { "Bonded Shield", 5000 }, { "Bonded Shield [Dragon]", 5000 }, { "Bonded Shield [Cavalry]", 5000 },
            { "Bonded Shield [Armored]", 5000 }, { "Bonded Shield [Flying]", 5000 }, { "Bonded Shield [Qi Adept]", 5000 }, { "Call Doubles", 3000 },
            { "Call Doubles [Dragon]", 3000 }, { "Call Doubles [Flying]", 3000 }, { "Laguz Friend", 5000 }, { "Laguz Friend [Dragon]", 5000 },
            { "Instruct", 1000 }, { "Instruct [Dragon]", 1000 }, { "Instruct [Backup]", 1000 }, { "Instruct [Cavalry]", 1000 },
            { "Instruct [Covert]", 1000 }, { "Instruct [Armored]", 1000 }, { "Instruct [Flying]", 1000 }, { "Instruct [Mystical]", 1000 },
            { "Instruct [Qi Adept]", 1000 }, { "Dragon Vein (Corrin)", 1000 }, { "Dragon Vein (Corrin) [Dragon]", 1000 }, { "Dragon Vein (Corrin) [Backup]", 1000 },
            { "Dragon Vein (Corrin) [Cavalry]", 1000 }, { "Dragon Vein (Corrin) [Covert]", 1000 }, { "Dragon Vein (Corrin) [Armored]", 1000 }, { "Dragon Vein (Corrin) [Flying]", 1000 },
            { "Dragon Vein (Corrin) [Mystical]", 1000 }, { "Dragon Vein (Corrin) [Qi Adept]", 1000 }, { "Dreadful Aura", 5000 }, { "Dreadful Aura [Covert]", 5000 },
            { "Sacred Twins", 100 }, { "Night and Day", 100 }, { "Solar Brace", 3000 }, { "Eclipse Brace", 3000 },
            { "Solar Brace+", 5000 }, { "Eclipse Brace+", 5000 }, { "Bravery", 2000 }, { "Blue Skies", 2000 },
            { "Bravery+", 3000 }, { "Blue Skies+", 3000 }, { "Holy Aura", 100 }, { "Holy Shield", 300 },
            { "Bond Forger", 2000 }, { "Bond Forger+", 3000 }, { "Boon of Elyos", 3000 }, { "Attuned", 3000 },
            { "Attuned [Dragon]", 3000 }, { "Attuned [Backup]", 3000 }, { "Attuned [Cavalry]", 3000 }, { "Attuned [Covert]", 3000 },
            { "Attuned [Armored]", 3000 }, { "Attuned [Flying]", 3000 }, { "Attuned [Mystical]", 3000 }, { "Attuned [Qi Adept]", 3000 },
            { "Divinely Inspiring", 500 }, { "Alabaster Duty", 100 }, { "Verdant Faith", 100 }, { "Crimson Cheer", 100 },
            { "Self-Improver", 100 }, { "Energized", 100 }, { "Moved to Tears", 100 }, { "Gentle Flower", 100 },
            { "Fairy-Tale Folk", 100 }, { "Admiration", 100 }, { "Fair Fight", 300 }, { "Aspiring Hero", 100 },
            { "Meditation", 100 }, { "Get Behind Me!", 300 }, { "Share Spoils", 300 }, { "Generosity", 300 },
            { "Single-Minded", 100 }, { "Not *Quite*", 500 }, { "Blinding Flash", 300 }, { "Big Personality", 500 },
            { "Stunning Smile", 300 }, { "Disarming Sigh", 300 }, { "Racket of Solm", 100 }, { "Knightly Escort", 300 },
            { "Blood Fury", 500 }, { "Charmer", 100 }, { "Party Animal", 300 }, { "Seconds?", 100 },
            { "Make a Killing", 300 }, { "Expertise", 1000 }, { "Trained to Kill", 500 }, { "Curious Dance", 300 },
            { "Weapon Insight", 500 }, { "Will to Win", 100 }, { "Fell Protection", 500 }, { "Contemplative", 100 },
            { "Brave Assist", 1000 }, { "Pincer Attack", 500 }, { "Reforge", 100 }, { "Smash+", 100 },
            { "Merciless", 500 }, { "No Distractions", 500 }, { "Careful Aim", 300 }, { "Swap", 200 },
            { "Allied Defense", 500 }, { "Pivot", 200 }, { "Hobble", 500 }, { "Clear the Way", 500 },
            { "Air Raid", 300 }, { "Pass", 500 }, { "Spell Harmony", 300 }, { "Chaos Style", 500 },
            { "Diffuse Healer", 500 }, { "Self-Healing", 300 }, { "Divine Spirit", 300 }, { "Fell Spirit", 300 },
            { "Dark Spirit", 100 }, { "Golden Lotus", 300 }, { "Ignis", 500 }, { "Sol", 500 },
            { "Luna", 500 }, { "Grasping Void", 300 }, { "World Tree", 500 }, { "Sandstorm", 500 },
            { "Back at You", 100 }, { "Soulblade", 500 }, { "Special Dance", 500 }, { "Sympathetic", 500 },
            { "Deadly Blade", 500 }, { "Battlewise", 300 }, { "Renewal", 500 }, { "Windsweep", 100 },
            { "Great Thunder", 500 }, { "Bane", 100 }, { "Mercy", 100 }, { "Raging Fire", 500 },
            { "Strong Arm", 500 }, { "Miracle", 300 }, { "Dire Thunder", 1000 }, { "Rightful Ruler", 300 },
            { "Healtouch", 100 }, { "Draw Back", 200 }, { "Giga Excalibur", 300 },  { "Wind Adept", 300 },
            { "Shove", 100 }, { "Flickering Flower", 100 }, { "Wind God", 1000 }, { "Chivalry", 300 },
            { "Bushido", 500 }, { "Lethality", 300 }, { "Sure Strike", 300 }, { "Spur Attack", 500 },
            { "Fortify Def", 500 }, { "Spur Res", 500 }, { "Friendly Rivalry", 100 }, { "Gambit", 1500 },
            { "Combat Arts", 1000 }, { "Combat Arts [Dragon]", 1000 }, { "Combat Arts [Covert]", 1000 }, { "Draconic Form", 3000 },
            { "Draconic Form [Armored]", 3000 }, { "Draconic Form [Mystical]", 3000 }, { "Piercing Glare", 1000 }, { "Impenetrable", 3000 },
            { "Impenetrable [Dragon]", 3000 }, { "Impenetrable [Cavalry]", 3000 }, { "Impenetrable [Armored]", 3000 }, { "Impenetrable [Flying]", 3000 },
            { "Contract", 1000 }, { "Contract [Dragon]", 1000 }, { "Contract [Backup]", 1000 }, { "Contract [Covert]", 1000 },
            { "Flare", 3000 }, { "Flare [Dragon]", 3000 }, { "Flare [Mystical]", 3000 }, { "Flare [Qi Adept]", 3000 },
            { "Corrupted Flare", 3000 }, { "Dragon Vein (Camilla)", 1000 }, { "Dragon Vein (Camilla) [Dragon]", 1000 }, { "Dragon Vein (Camilla) [Backup]", 1000 },
            { "Dragon Vein (Camilla) [Cavalry]", 1000 }, { "Dragon Vein (Camilla) [Covert]", 1000 }, { "Dragon Vein (Camilla) [Armored]", 1000 }, { "Dragon Vein (Camilla) [Flying]", 1000 },
            { "Dragon Vein (Camilla) [Mystical]", 1000 }, { "Dragon Vein (Camilla) [Qi Adept]", 1000 }, { "Soar", 3000 }, { "Soar [Dragon]", 3000 },
            { "Soar [Cavalry]", 3000 }, { "Soar [Flying]", 3000 }, { "Other Half", 3000 }, { "Other Half [Dragon]", 3000 },
            { "Other Half [Backup]", 3000 }, { "Other Half [Covert]", 3000 }, { "Corrupted Other Half", 3000 }, { "Other Half (Arena)", 3000 },
            { "Protective", 100 }, { "Protective (Xenologue 1)", 100 }, { "Protective (Xenologue 2)", 100 }, { "Protective (Xenologue 3)", 100 },
            { "Protective (Xenologue 4)", 100 }, { "Rivalry", 100 }, { "Wounded Pride (Xenologue 1)", 100 }, { "Wounded Pride (Xenologue 2)", 100 },
            { "Wounded Pride (Xenologue 3)", 100 }, { "Wounded Pride (Xenologue 4)", 100 }, { "Rivalry (Xenologue 5)", 100 }, { "Friendly Boost", 300 },
            { "Survival Plan", 300 }, { "Knightly Code", 300 }, { "Self-Defense", 100 }, { "Fierce Bloom", 100 },
            { "This Ends Here", 300 }, { "Show-Off", 300 }, { "Final Say", 100 }, { "Stealth Assist", 500 },
            { "Dignity of Solm", 100 }, { "Wear Down", 100 }, { "Convoy", 500 }, { "Miasma Domain ", 100 },
            { "Frost Domain", 100 }, { "Pack Hunter (Corrupted)", 100 }, { "Pack Hunter (Phantom)", 100 }, { "Resist Emblems", 300 },
            { "Spur Emblems", 300 }, { "Sigil Protection", 5000 }, { "Bond Breaker+", 1500 }, { "Lodestar Rush", 5000 },
            { "Lodestar Rush [Dragon]", 5000 }, { "Lodestar Rush [Backup]", 5000 }, { "Lodestar Rush [Mystical]", 5000 }, { "Override", 5000 },
            { "Override [Dragon]", 5000 }, { "Override [Armored]", 5000 }, { "Override [Mystical]", 5000 }, { "Override [Qi Adept]", 5000 },
            { "Warp Ragnarok", 5000 }, { "Warp Ragnarok [Dragon]", 5000 }, { "Warp Ragnarok [Cavalry]", 5000 }, { "Warp Ragnarok [Flying]", 5000 },
            { "Warp Ragnarok [Mystical]", 5000 }, { "Dark Warp", 5000 }, { "Ragnarok Warp", 5000 }, { "Great Sacrifice", 5000 },
            { "Great Sacrifice [Dragon]", 5000 }, { "Great Sacrifice [Armored]", 5000 }, { "Great Sacrifice [Qi Adept]", 5000 }, { "Blazing Lion", 5000 },
            { "Blazing Lion [Dragon]", 5000 }, { "Blazing Lion [Mystical]", 5000 }, { "Quadruple Hit", 5000 }, { "Quadruple Hit [Dragon]", 5000 },
            { "Quadruple Hit [Covert]", 5000 }, { "Quadruple Hit [Qi Adept]", 5000 }, { "All for One", 5000 }, { "All for One [Dragon]", 5000 },
            { "All for One [Backup]", 5000 }, { "Astra Storm", 5000 }, { "Astra Storm [Dragon]", 5000 }, { "Astra Storm [Covert]", 5000 },
            { "Astra Storm [Qi Adept]", 5000 }, { "Weak Astra Storm", 5000 }, { "Weak Astra Storm [Qi Adept]", 5000 }, { "Great Aether", 5000 },
            { "Great Aether [Dragon]", 5000 }, { "Great Aether [Armored]", 5000 }, { "Great Aether [Flying]", 5000 }, { "Goddess Dance", 5000 },
            { "Goddess Dance [Dragon]", 5000 }, { "Goddess Dance [Backup]", 5000 }, { "Goddess Dance [Cavalry]", 5000 }, { "Goddess Dance [Covert]", 5000 },
            { "Goddess Dance [Armored]", 5000 }, { "Goddess Dance [Flying]", 5000 }, { "Goddess Dance [Mystical]", 5000 }, { "Goddess Dance [Qi Adept]", 5000 },
            { "Diabolical Dance", 5000 }, { "Torrential Roar", 5000 }, { "Torrential Roar [Dragon]", 5000 }, { "Twin Strike", 5000 },
            { "Twin Strike [Dragon]", 5000 }, { "Twin Strike [Cavalry]", 5000 }, { "Dragon Blast", 5000 }, { "Dragon Blast [Dragon]", 5000 },
            { "Dragon Blast [Backup]", 5000 }, { "Dragon Blast [Mystical]", 5000 }, { "Dragon Blast [Qi Adept]", 5000 }, { "Bond Blast", 8000 },
            { "Bond Blast [Dragon]", 8000 }, { "Bond Blast [Backup]", 8000 }, { "Bond Blast [Mystical]", 8000 }, { "Bond Blast [Qi Adept]", 8000 },
            { "Run Through", 300 }, { "Paraselene", 300 }, { "Flame Gambit", 1500 }, { "Shield Gambit", 1500 },
            { "Poison Gambit", 1500 }, { "Raging Storm", 1000 }, { "Raging Storm [Dragon]", 1000 }, { "Raging Storm [Covert]", 1000 },
            { "Atrocity", 1000 }, { "Atrocity [Dragon]", 1000 }, { "Atrocity [Covert]", 1000 }, { "Fallen Star", 1000 },
            { "Fallen Star [Dragon]", 1000 }, { "Fallen Star [Covert]", 1000 }, { "Houses Unite", 5000 }, { "Houses Unite [Dragon]", 5000 },
            { "Houses Unite [Cavalry]", 5000 }, { "Houses Unite [Covert]", 5000 }, { "Houses Unite [Armored]", 5000 }, { "Houses Unite [Qi Adept]", 5000 },
            { "Houses Unite+", 8000 }, { "Houses Unite+ [Dragon]", 8000 }, { "Houses Unite+ [Cavalry]", 8000 }, { "Houses Unite+ [Covert]", 8000 },
            { "Houses Unite+ [Armored]", 8000 }, { "Houses Unite+ [Qi Adept]", 8000 }, { "Divine Blessing", 5000 }, { "Divine Blessing [Dragon]", 5000 },
            { "Divine Blessing [Qi Adept]", 5000 }, { "Divine Blessing (Xenologue 1)", 5000 }, { "Divine Blessing+", 8000 }, { "Divine Blessing+ [Dragon]", 8000 },
            { "Divine Blessing+ [Qi Adept]", 8000 }, { "Storm's Eye", 5000 }, { "Storm's Eye [Dragon]", 5000 }, { "Storm's Eye [Backup]", 5000 },
            { "Storm's Eye [Covert]", 5000 }, { "Storm's Eye+", 8000 }, { "Storm's Eye+ [Dragon]", 8000 }, { "Storm's Eye+ [Backup]", 8000 },
            { "Storm's Eye+ [Covert]", 8000 }, { "Summon Hero", 5000 }, { "Summon Hero [Dragon]", 5000 }, { "Summon Hero [Backup]", 5000 },
            { "Summon Hero [Cavalry]", 5000 }, { "Cataclysm", 5000 }, { "Cataclysm [Dragon]", 5000 }, { "Cataclysm [Mystical]", 5000 },
            { "Cataclysm [Qi Adept]", 5000 }, { "Cataclysm+", 8000 }, { "Cataclysm+ [Dragon]", 8000 }, { "Cataclysm+ [Mystical]", 8000 },
            { "Cataclysm+ [Qi Adept]", 8000 }, { "Cataclysm (Divine Paralogue)", 5000 }, { "Corrupted Cataclysm", 5000 }, { "Dark Inferno", 5000 },
            { "Dark Inferno [Dragon]", 5000 }, { "Dark Inferno [Mystical]", 5000 }, { "Dark Inferno [Qi Adept]", 5000 }, { "Dark Inferno+", 8000 },
            { "Dark Inferno+ [Dragon]", 8000 }, { "Dark Inferno+ [Mystical]", 8000 }, { "Dark Inferno+ [Qi Adept]", 8000 }, { "Giga Levin Sword", 5000 },
            { "Giga Levin Sword [Dragon]", 5000 }, { "Giga Levin Sword [Flying]", 5000 }, { "Giga Levin Sword [Mystical]", 5000 }, { "Giga Levin Sword+", 8000 },
            { "Giga Levin Sword+ [Dragon]", 8000 }, { "Giga Levin Sword+ [Flying]", 8000 }, { "Giga Levin Sword+ [Mystical]", 8000 }, { "Let Fly", 300 }
        };

        internal static Dictionary<string, string> EngageAttackToBondLinkSkill { get; } = new()
        {
            { "SID_リュールエンゲージ技", "SID_リュールエンゲージ技共同" }, // Dragon Blast
            { "SID_三級長エンゲージ技", "SID_三級長エンゲージ技＋" }, // Houses Unite
            { "SID_チキエンゲージ技", "SID_チキエンゲージ技＋" }, // Divine Blessing
            { "SID_ヘクトルエンゲージ技", "SID_ヘクトルエンゲージ技＋" }, // Storm's Eye
            { "SID_セネリオエンゲージ技", "SID_セネリオエンゲージ技＋" }, // Cataclysm
            { "SID_カミラエンゲージ技", "SID_カミラエンゲージ技＋" }, // Dark Inferno
            { "SID_クロムエンゲージ技", "SID_クロムエンゲージ技＋" } // Giga Levin Sword
        };

        internal static Dictionary<string, sbyte> EngageAttackToAIEngageAttackType { get; } = new()
        {
            { "SID_マルスエンゲージ技", 1 }, // Lodestar Rush
            { "SID_マルスエンゲージ技_竜族", 1 },
            { "SID_マルスエンゲージ技_連携", 1 },
            { "SID_マルスエンゲージ技_魔法", 1 },
            { "SID_シグルドエンゲージ技", 2 }, // Override
            { "SID_シグルドエンゲージ技_竜族", 2 },
            { "SID_シグルドエンゲージ技_重装", 2 },
            { "SID_シグルドエンゲージ技_魔法", 2 },
            { "SID_シグルドエンゲージ技_気功", 2 },
            { "SID_セリカエンゲージ技", 1 }, // Warp Ragnarok
            { "SID_セリカエンゲージ技_竜族", 1 },
            { "SID_セリカエンゲージ技_騎馬", 1 },
            { "SID_セリカエンゲージ技_飛行", 1 },
            { "SID_セリカエンゲージ技_魔法", 1 },
            { "SID_セリカエンゲージ技_闇", 1 },
            { "SID_セリカエンゲージ技_闇_M020", 1 },
            { "SID_ミカヤエンゲージ技", 4 }, // Great Sacrifice
            { "SID_ミカヤエンゲージ技_竜族", 4 },
            { "SID_ミカヤエンゲージ技_重装", 4 },
            { "SID_ミカヤエンゲージ技_気功", 4 },
            { "SID_ロイエンゲージ技", 1 }, // Blazing Lion
            { "SID_ロイエンゲージ技_竜族", 1 },
            { "SID_ロイエンゲージ技_魔法", 1 },
            { "SID_リーフエンゲージ技", 1 }, // Quadruple Hit
            { "SID_リーフエンゲージ技_竜族", 1 },
            { "SID_リーフエンゲージ技_隠密", 1 },
            { "SID_リーフエンゲージ技_気功", 1 },
            { "SID_ルキナエンゲージ技", 1 }, // All for One
            { "SID_ルキナエンゲージ技_竜族", 1 },
            { "SID_ルキナエンゲージ技_連携", 1 },
            { "SID_リンエンゲージ技", 1 }, // Astra Storm
            { "SID_リンエンゲージ技_竜族", 1 },
            { "SID_リンエンゲージ技_隠密", 1 },
            { "SID_リンエンゲージ技_気功", 1 },
            { "SID_リンエンゲージ技_威力減", 1 },
            { "SID_リンエンゲージ技_闇_気功", 1 },
            { "SID_アイクエンゲージ技", 3 }, // Great Aether
            { "SID_アイクエンゲージ技_竜族", 3 },
            { "SID_アイクエンゲージ技_重装", 3 },
            { "SID_アイクエンゲージ技_飛行", 3 },
            { "SID_ベレトエンゲージ技", 5 }, // Goddess Dance
            { "SID_ベレトエンゲージ技_竜族", 5 },
            { "SID_ベレトエンゲージ技_連携", 5 },
            { "SID_ベレトエンゲージ技_騎馬", 5 },
            { "SID_ベレトエンゲージ技_隠密", 5 },
            { "SID_ベレトエンゲージ技_重装", 5 },
            { "SID_ベレトエンゲージ技_飛行", 5 },
            { "SID_ベレトエンゲージ技_魔法", 5 },
            { "SID_ベレトエンゲージ技_気功", 5 },
            { "SID_ベレトエンゲージ技_闇", 5 },
            { "SID_カムイエンゲージ技", 1 }, // Torrential Roar
            { "SID_カムイエンゲージ技_竜族", 1 },
            { "SID_エイリークエンゲージ技", 1 }, // Twin Strike
            { "SID_エイリークエンゲージ技_竜族", 1 },
            { "SID_エイリークエンゲージ技_騎馬", 1 },
            { "SID_リュールエンゲージ技", 1 }, // Dragon Blast
            { "SID_リュールエンゲージ技_竜族", 1 },
            { "SID_リュールエンゲージ技_連携", 1 },
            { "SID_リュールエンゲージ技_魔法", 1 },
            { "SID_リュールエンゲージ技_気功", 1 },
            { "SID_リュールエンゲージ技共同", 1 }, // Bond Blast
            { "SID_リュールエンゲージ技共同_竜族", 1 },
            { "SID_リュールエンゲージ技共同_連携", 1 },
            { "SID_リュールエンゲージ技共同_魔法", 1 },
            { "SID_リュールエンゲージ技共同_気功", 1 },
            { "SID_三級長エンゲージ技", 1 }, // Houses Unite
            { "SID_三級長エンゲージ技_竜族", 1 },
            { "SID_三級長エンゲージ技_騎馬", 1 },
            { "SID_三級長エンゲージ技_隠密", 1 },
            { "SID_三級長エンゲージ技_重装", 1 },
            { "SID_三級長エンゲージ技_気功", 1 },
            { "SID_三級長エンゲージ技＋", 1 }, // Houses Unite+
            { "SID_三級長エンゲージ技＋_竜族", 1 },
            { "SID_三級長エンゲージ技＋_騎馬", 1 },
            { "SID_三級長エンゲージ技＋_隠密", 1 },
            { "SID_三級長エンゲージ技＋_重装", 1 },
            { "SID_三級長エンゲージ技＋_気功", 1 },
            { "SID_チキエンゲージ技", 6 }, // Divine Blessing
            { "SID_チキエンゲージ技_竜族", 6 },
            { "SID_チキエンゲージ技_気功", 6 },
            { "SID_チキエンゲージ技_E001", 6 },
            { "SID_チキエンゲージ技＋", 6 }, // Divine Blessing+
            { "SID_チキエンゲージ技＋_竜族", 6 },
            { "SID_チキエンゲージ技＋_気功", 6 },
            { "SID_ヘクトルエンゲージ技", 7 }, // Storm's Eye
            { "SID_ヘクトルエンゲージ技_竜族", 7 },
            { "SID_ヘクトルエンゲージ技_連携", 7 },
            { "SID_ヘクトルエンゲージ技_隠密", 7 },
            { "SID_ヘクトルエンゲージ技＋", 7 }, // Storm's Eye+
            { "SID_ヘクトルエンゲージ技＋_竜族", 7 },
            { "SID_ヘクトルエンゲージ技＋_連携", 7 },
            { "SID_ヘクトルエンゲージ技＋_隠密", 7 },
            { "SID_ヴェロニカエンゲージ技", 9 }, // Summon Hero
            { "SID_ヴェロニカエンゲージ技_竜族", 9 },
            { "SID_ヴェロニカエンゲージ技_連携", 9 },
            { "SID_ヴェロニカエンゲージ技_騎馬", 9 },
            { "SID_セネリオエンゲージ技", 1 }, // Cataclysm
            { "SID_セネリオエンゲージ技_竜族", 1 },
            { "SID_セネリオエンゲージ技_魔法", 1 },
            { "SID_セネリオエンゲージ技_気功", 1 },
            { "SID_セネリオエンゲージ技_G004", 1 },
            { "SID_セネリオエンゲージ技_闇", 1 },
            { "SID_セネリオエンゲージ技＋", 1 }, // Cataclysm+
            { "SID_セネリオエンゲージ技＋_竜族", 1 },
            { "SID_セネリオエンゲージ技＋_魔法", 1 },
            { "SID_セネリオエンゲージ技＋_気功", 1 },
            { "SID_カミラエンゲージ技", 8 }, // Dark Inferno
            { "SID_カミラエンゲージ技_竜族", 8 },
            { "SID_カミラエンゲージ技_魔法", 8 },
            { "SID_カミラエンゲージ技_気功", 8 },
            { "SID_カミラエンゲージ技＋", 8 }, // Dark Inferno+
            { "SID_カミラエンゲージ技＋_竜族", 8 },
            { "SID_カミラエンゲージ技＋_魔法", 8 },
            { "SID_カミラエンゲージ技＋_気功", 8 },
            { "SID_クロムエンゲージ技", 1 }, // Giga Levin Sword
            { "SID_クロムエンゲージ技_竜族", 1 },
            { "SID_クロムエンゲージ技_飛行", 1 },
            { "SID_クロムエンゲージ技_魔法", 1 },
            { "SID_クロムエンゲージ技＋", 1 }, // Giga Levin Sword+
            { "SID_クロムエンゲージ技＋_竜族", 1 },
            { "SID_クロムエンゲージ技＋_飛行", 1 },
            { "SID_クロムエンゲージ技＋_魔法", 1 },
        };

        internal enum SyncStat { HP, Str, Dex, Spd, Lck, Def, Mag, Res, Bld, Mov, None }

        internal static List<List<string>> SyncStatLookup = new();
        #endregion
        #region Support Category IDs
        internal static List<(string id, string name)> SupportCategories { get; } = new()
        {
            ("デフォルト", "Default"), ("バランス", "Balanced"), ("回避", "Avoid"), ("必殺", "Critical"),
            ("命中", "Hit"), ("必殺回避", "Dodge"),
        };
        #endregion
        #region TalkAnim IDs
        internal static List<(string id, string name)> MaleTalkAnims { get; } = new()
        {
            ("AOC_Talk_c000", "Default Male"), ("AOC_Talk_c001", "Male Alear"),
            ("AOC_Talk_c049", "Rafal"), ("AOC_Talk_c100", "Alfred A"),
            ("AOC_Talk_c100b", "Alfred B"), ("AOC_Talk_c101", "Boucheron"),
            ("AOC_Talk_c102", "Louis"), ("AOC_Talk_c103", "Jean"),
            ("AOC_Talk_c200", "Diamant A"), ("AOC_Talk_c200b", "Diamant B"),
            ("AOC_Talk_c201", "Alcryst A"), ("AOC_Talk_c201b", "Alcryst B"),
            ("AOC_Talk_c202", "Morion"), ("AOC_Talk_c203", "Amber"),
            ("AOC_Talk_c300", "Hyacinth"), ("AOC_Talk_c301", "Zelkov"),
            ("AOC_Talk_c302", "Kagetsu"), ("AOC_Talk_c304", "Lindon"),
            ("AOC_Talk_c400", "Fogado A"), ("AOC_Talk_c400b", "Fogado B"),
            ("AOC_Talk_c401", "Pandreo"), ("AOC_Talk_c402", "Bunet"),
            ("AOC_Talk_c403", "Seadall"), ("AOC_Talk_c500", "Vander"),
            ("AOC_Talk_c501", "Clanne"), ("AOC_Talk_c502", "Mauvier"),
            ("AOC_Talk_c503", "Griss"), ("AOC_Talk_c503b", "Gregory"),
            ("AOC_Talk_c530", "Marth"), ("AOC_Talk_c531", "Sigurd"),
            ("AOC_Talk_c532", "Leif"), ("AOC_Talk_c533", "Roy"),
            ("AOC_Talk_c534", "Ike"), ("AOC_Talk_c535", "Byleth"),
            ("AOC_Talk_c536", "Ephraim"), ("AOC_Talk_c514", "Dimitri"),
            ("AOC_Talk_c515", "Claude"), ("AOC_Talk_c510", "Hector"),
            ("AOC_Talk_c511", "Soren"), ("AOC_Talk_c512", "Chrom"),
            ("AOC_Talk_c513", "Robin"),
        };

        internal static List<(string id, string name)> FemaleTalkAnims { get; } = new()
        {
            ("AOC_Talk_c050", "Default Female"), ("AOC_Talk_c051", "Female Alear"),
            ("AOC_Talk_c099", "Nel"), ("AOC_Talk_c150", "Céline A"),
            ("AOC_Talk_c150b", "Céline B"), ("AOC_Talk_c151", "Éve"),
            ("AOC_Talk_c152", "Etie"), ("AOC_Talk_c153", "Chloé"),
            ("AOC_Talk_c250", "Jade"), ("AOC_Talk_c251", "Lapis"),
            ("AOC_Talk_c252", "Citrinne"), ("AOC_Talk_c253", "Yunaka"),
            ("AOC_Talk_c254", "Saphir"), ("AOC_Talk_c303", "Rosado"),
            ("AOC_Talk_c350", "Ivy A"), ("AOC_Talk_c350b", "Ivy B"),
            ("AOC_Talk_c351", "Hortensia A"), ("AOC_Talk_c351b", "Hortensia B"),
            ("AOC_Talk_c352", "Goldmary"), ("AOC_Talk_c450", "Timerra A"),
            ("AOC_Talk_c450b", "Timerra B"), ("AOC_Talk_c451", "Seforia"),
            ("AOC_Talk_c452", "Merrin"), ("AOC_Talk_c453", "Panette"),
            ("AOC_Talk_c550", "Framme"), ("AOC_Talk_c551", "Veyle"),
            ("AOC_Talk_c556", "Evil Veyle"), ("AOC_Talk_c552", "Anna"),
            ("AOC_Talk_c553", "Zephia"), ("AOC_Talk_c553b", "Zelestia"),
            ("AOC_Talk_c554", "Marni"), ("AOC_Talk_c554b", "Madeline"),
            ("AOC_Talk_c555", "Lumera"), ("AOC_Talk_c580", "Celica"),
            ("AOC_Talk_c581", "Lyn"), ("AOC_Talk_c582", "Eirika"),
            ("AOC_Talk_c583", "Micaiah"), ("AOC_Talk_c584", "Lucina"),
            ("AOC_Talk_c585", "Corrin"), ("AOC_Talk_c560", "Tiki"),
            ("AOC_Talk_c563", "Edelgard"), ("AOC_Talk_c561", "Camilla"),
            ("AOC_Talk_c562", "Veronica"),
        };
        #endregion
        #region Unit Type IDs
        internal static List<(string id, string name)> UnitTypes { get; } = new()
        {
            ("スタイル無し", "None"), ("連携スタイル", "Backup"), ("騎馬スタイル", "Cavalry"), ("隠密スタイル", "Covert"),
            ("重装スタイル", "Armor"), ("飛行スタイル", "Flier"), ("魔法スタイル", "Mystical"), ("気功スタイル", "Qi Adept"),
            ("竜族スタイル", "Dragon"),
        };
        #endregion
        #region Other
        internal static List<(int id, string name)> Proficiencies { get; } = new()
        {
            (0, "None"), (1, "Sword"), (2, "Lance"), (3, "Axe"), (4, "Bow"), (5, "Dagger"), (6, "Tome"), (7, "Staff"),
            (8, "Arts"), (9, "Special")
        };
        internal static List<(int id, string name)> BasicProficiencies { get; } = new()
        {
            (1, "Sword"), (2, "Lance"), (3, "Axe"), (4, "Bow"), (5, "Dagger"), (6, "Tome"), (7, "Staff"), (8, "Arts"),
        };

        internal enum Gender // Ah yes, the four genders:
        {
            Male, Female, Both, Rosado
        }

        internal struct AssetShuffleEntity
        {
            internal string name;
            internal string id;
            internal List<string> alternates;
            internal Gender gender;
            internal string iconID;
            internal bool enemyEmblem;
            internal string nameID;
            internal string faceIconID;
            internal string? thumbnail;
            internal Color hair;
            internal string? eid;

            internal AssetShuffleEntity(string name, string id, List<string> alternates, Gender gender,
                string iconID, bool enemyEmblem, string nameID, string faceIconID, string? thumbnail, Color hair, string? eid)
            {
                this.name = name;
                this.id = id;
                this.alternates = alternates;
                this.gender = gender;
                this.iconID = iconID;
                this.enemyEmblem = enemyEmblem;
                this.nameID = nameID;
                this.faceIconID = faceIconID;
                this.thumbnail = thumbnail;
                this.hair = hair;
                this.eid = eid;
            }
        }

        internal static List<AssetShuffleEntity> ProtagonistAssetShuffleData { get; } = new()
        {
            new("Alear", "PID_リュール", new() { "MPID_Lueur_M000", "PID_M000_リュール", "JID_M000_神竜ノ子", "MPID_MorphLueur",
                "PID_デモ用_神竜王リュール"}, Gender.Both,
                "001Lueur", false, "MPID_Lueur", "Face_Lueur", "Lueur", Color.FromArgb(97, 184, 231), null)
        };

        internal static List<AssetShuffleEntity> PlayableAssetShuffleData { get; } = new()
        {
            new("Vander", "PID_ヴァンドレ", new() { }, Gender.Male,
                "500Vandre", false, "MPID_Vandre", "Face_DarkEmblem", "Vandre", Color.FromArgb(241, 227, 217), null),
            new("Clanne", "PID_クラン", new() { }, Gender.Male,
                "501Clan", false, "MPID_Clan", "Face_DarkEmblem", "Clan", Color.FromArgb(224, 196, 189), null),
            new("Framme", "PID_フラン", new() { }, Gender.Female,
                "550Fram", false, "MPID_Fram", "Face_DarkEmblem", "Fram", Color.FromArgb(217, 212, 201), null),
            new("Alfred", "PID_アルフレッド", new() { "PID_E002_Boss", "PID_E006_Hide1" }, Gender.Male,
                "100Alfred", false, "MPID_Alfred", "Face_DarkEmblem", "Alfred", Color.FromArgb(241, 238, 199), null),
            new("Etie", "PID_エーティエ", new() { }, Gender.Female,
                "152Etie", false, "MPID_Etie", "Face_DarkEmblem", "Etie", Color.FromArgb(250, 170, 104), null),
            new("Boucheron", "PID_ブシュロン", new() { }, Gender.Male,
                "101Boucheron", false, "MPID_Boucheron", "Face_DarkEmblem", "Boucheron", Color.FromArgb(184, 134, 75), null),
            new("Céline", "PID_セリーヌ", new() { "PID_E002_Hide", "PID_E006_Hide2" }, Gender.Female,
                "150Celine", false, "MPID_Celine", "Face_DarkEmblem", "Celine", Color.FromArgb(236, 220, 181), null),
            new("Chloé", "PID_クロエ", new() { }, Gender.Female,
                "153Chloe", false, "MPID_Chloe", "Face_DarkEmblem", "Chloe", Color.FromArgb(90, 180, 180), null),
            new("Louis", "PID_ルイ", new() { }, Gender.Male,
                "102Louis", false, "MPID_Louis", "Face_DarkEmblem", "Louis", Color.FromArgb(66, 50, 35), null),
            new("Yunaka", "PID_ユナカ", new() { }, Gender.Female,
                "253Yunaka", false, "MPID_Yunaka", "Face_DarkEmblem", "Yunaka", Color.FromArgb(170, 22, 71), null),
            new("Alcryst", "PID_スタルーク", new() { "PID_E003_Hide", "PID_E006_Hide4" }, Gender.Male,
                "201Staluke", false, "MPID_Staluke", "Face_DarkEmblem", "Staluke", Color.FromArgb(15, 44, 86), null),
            new("Citrinne", "PID_シトリニカ", new() { }, Gender.Female,
                "252Citrinica", false, "MPID_Citrinica", "Face_DarkEmblem", "Citrinica", Color.FromArgb(226, 216, 161), null),
            new("Lapis", "PID_ラピス", new() { }, Gender.Female,
                "251Lapis", false, "MPID_Lapis", "Face_DarkEmblem", "Lapis", Color.FromArgb(255, 202, 196), null),
            new("Diamant", "PID_ディアマンド", new() { "PID_E003_Boss", "PID_E006_Hide3" }, Gender.Male,
                "200Diamand", false, "MPID_Diamand", "Face_DarkEmblem", "Diamand", Color.FromArgb(124, 42, 42), null),
            new("Amber", "PID_アンバー", new() { }, Gender.Male,
                "203Umber", false, "MPID_Umber", "Face_DarkEmblem", "Umber", Color.FromArgb(246, 198, 77), null),
            new("Jade", "PID_ジェーデ", new() { "PID_ジェーデ_兜あり" }, Gender.Female,
                "250Jade", false, "MPID_Jade", "Face_DarkEmblem", "Jade", Color.FromArgb(255, 229, 160), null),
            new("Ivy", "PID_アイビー", new() { "PID_M008_アイビー", "PID_M009_アイビー", "PID_E004_Boss", "PID_E006_Hide5" }, Gender.Female,
                "350Ivy", false, "MPID_Ivy", "Face_DarkEmblem", "Ivy", Color.FromArgb(132, 37, 108), null),
            new("Kagetsu", "PID_カゲツ", new() { "PID_M008_カゲツ", "PID_M009_カゲツ" }, Gender.Male,
                "302Kagetsu", false, "MPID_Kagetsu", "Face_DarkEmblem", "Kagetsu", Color.FromArgb(56, 65, 98), null),
            new("Zelkov", "PID_ゼルコバ", new() { "PID_M008_ゼルコバ", "PID_M009_ゼルコバ" }, Gender.Male,
                "301Zelkova", false, "MPID_Zelkova", "Face_DarkEmblem", "Zelkova", Color.FromArgb(62, 61, 72), null),
            new("Fogado", "PID_フォガート", new() { "PID_E005_Hide2", "PID_E006_Hide8" }, Gender.Male,
                "400Fogato", false, "MPID_Fogato", "Face_DarkEmblem", "Fogato", Color.FromArgb(60, 33, 19), null),
            new("Pandreo", "PID_パンドロ", new() { }, Gender.Male,
                "401Pandoro", false, "MPID_Pandoro", "Face_DarkEmblem", "Pandoro", Color.FromArgb(246, 127, 83), null),
            new("Bunet", "PID_ボネ", new() { }, Gender.Male,
                "402Bonet", false, "MPID_Bonet", "Face_DarkEmblem", "Bonet", Color.FromArgb(236, 220, 181), null),
            new("Timerra", "PID_ミスティラ", new() { "PID_E004_Hide", "PID_E006_Hide7" }, Gender.Female,
                "450Misutira", false, "MPID_Misutira", "Face_DarkEmblem", "Misutira", Color.FromArgb(60, 33, 19), null),
            new("Panette", "PID_パネトネ", new() { }, Gender.Female,
                "453Panetone", false, "MPID_Panetone", "Face_DarkEmblem", "Panetone", Color.FromArgb(245, 90, 51), null),
            new("Merrin", "PID_メリン", new() { }, Gender.Female,
                "452Merin", false, "MPID_Merin", "Face_DarkEmblem", "Merin", Color.FromArgb(236, 220, 181), null),
            new("Hortensia", "PID_オルテンシア", new() { "PID_M014_オルテンシア", "PID_M007_オルテンシア", "PID_M010_オルテンシア",
                "PID_E005_Hide1", "PID_E006_Hide6" }, Gender.Female,
                "351Hortensia", false, "MPID_Hortensia", "Face_DarkEmblem", "Hortensia", Color.FromArgb(255, 96, 192), null),
            new("Seadall", "PID_セアダス", new() { }, Gender.Male,
                "403Seadas", false, "MPID_Seadas", "Face_DarkEmblem", "Seadas", Color.FromArgb(128, 128, 96), null),
            new("Rosado", "PID_ロサード", new() { "PID_M007_ロサード", "PID_M010_ロサード" }, Gender.Rosado,
                "303Rosado", false, "MPID_Rosado", "Face_DarkEmblem", "Rosado", Color.FromArgb(224, 224, 255), null),
            new("Goldmary", "PID_ゴルドマリー", new() { "PID_M007_ゴルドマリー", "PID_M010_ゴルドマリー" }, Gender.Female,
                "352Goldmary", false, "MPID_Goldmary", "Face_DarkEmblem", "Goldmary", Color.FromArgb(184, 144, 105), null),
            new("Lindon", "PID_リンデン", new() { }, Gender.Male,
                "304Linden", false, "MPID_Linden", "Face_DarkEmblem", "Linden", Color.FromArgb(239, 227, 211), null),
            new("Saphir", "PID_ザフィーア", new() { }, Gender.Female,
                "254Saphir", false, "MPID_Saphir", "Face_DarkEmblem", "Saphir", Color.FromArgb(191, 191, 191), null),
            new("Veyle", "PID_ヴェイル", new() { "PID_ヴェイル_包帯", "PID_ヴェイル_黒_善", "PID_ヴェイル_黒_善_角折れ", "PID_ヴェイル_白_悪",
                "PID_ヴェイル_黒_悪", "PID_M011_ヴェイル", "PID_M017_ヴェイル", "PID_M021_ヴェイル" }, Gender.Female,
                "551Veyre", false, "MPID_Veyre", "Face_DarkEmblem", "Veyre", Color.FromArgb(224, 192, 255), null),
            new("Mauvier", "PID_モーヴ", new() { "PID_M011_モーヴ", "PID_M014_モーヴ", "PID_M017_モーヴ", "PID_M019_モーヴ" }, Gender.Male,
                "502Mauve", false, "MPID_Mauve", "Face_DarkEmblem", "Mauve", Color.FromArgb(88, 91, 102), null),
            new("Anna", "PID_アンナ", new() { }, Gender.Female,
                "552Anna", false, "MPID_Anna", "Face_DarkEmblem", "Anna", Color.FromArgb(196, 85, 81), null),
            new("Jean", "PID_ジャン", new() { }, Gender.Male,
                "103Jean", false, "MPID_Jean", "Face_DarkEmblem", "Jean", Color.FromArgb(92, 95, 109), null),
            new("Rafal", "PID_ラファール", new() { "PID_デモ用_竜石なし_ラファール", "MPID_Il", "PID_E005_Boss", "PID_イル", "PID_E001_イル",
                "PID_E002_イル", "PID_E003_イル", "PID_E004_イル" }, Gender.Male,
                "049Il", false, "MPID_Rafale", "Face_DarkEmblem", "Rafale", Color.FromArgb(255, 255, 255), null),
            new("Nel", "PID_エル", new() { "PID_E001_エル", "PID_E002_エル", "PID_E003_エル", "PID_E004_エル", "PID_E005_エル", "PID_E006_エル" }, Gender.Female,
                "099El", false, "MPID_El", "Face_DarkEmblem", "El", Color.FromArgb(160, 160, 160), null),
            new("Zelestia", "PID_セレスティア", new() { "PID_E002_セレスティア", "PID_E003_セレスティア", "PID_E004_セレスティア",
                "PID_E006_セレスティア" }, Gender.Female,
                "553Selestia", false, "MPID_Selestia", "Face_DarkEmblem", "Selestia", Color.FromArgb(233, 226, 215), null),
            new("Gregory", "PID_グレゴリー", new() { "PID_E003_グレゴリー", "PID_E004_グレゴリー", "PID_E006_グレゴリー" }, Gender.Male,
                "503Gregory", false, "MPID_Gregory", "Face_DarkEmblem", "Gregory", Color.FromArgb(50, 83, 69), null),
            new("Madeline", "PID_マデリーン", new() { "PID_E004_マデリーン", "PID_E006_マデリーン" }, Gender.Female,
                "554Madeline", false, "MPID_Madeline", "Face_DarkEmblem", "Madeline", Color.FromArgb(246, 228, 166), null)
        };

        internal static List<AssetShuffleEntity> NamedNPCAssetShuffleData { get; } = new()
        {
            new("Lumera", "PID_ルミエル", new() { "PID_M025_ルミエル", "MPID_MorphLumiere", "PID_M002_ルミエル" }, Gender.Female,
                "555Lumiere", false, "MPID_Lumiere", "Face_DarkEmblem", null, Color.FromArgb(125, 175, 255), null),
            new("Sombron", "PID_ソンブル", new() { "PID_M000_ソンブル", "PID_M026_ソンブル_人型" }, Gender.Male,
                "504Sombre", false, "MPID_Sombre", "Face_DarkEmblem", null, Color.FromArgb(96, 32, 96), null),
            new("Éve", "PID_イヴ", new() { }, Gender.Female,
                "151Eve", false, "MPID_Eve", "Face_DarkEmblem", null, Color.FromArgb(255, 235, 188), null),
            new("Morion", "PID_モリオン", new() { }, Gender.Male,
                "202Morion", false, "MPID_Morion", "Face_DarkEmblem", null, Color.FromArgb(149, 43, 47), null),
            new("Hyacinth", "PID_ハイアシンス", new() { "MPID_MorphHyacinth", "PID_M010_ハイアシンス", "PID_M017_異形兵_ハイアシンス" }, Gender.Male,
                "300Hyacinth", false, "MPID_Hyacinth", "Face_DarkEmblem", null, Color.FromArgb(214, 214, 214), null),
            new("Seforia", "PID_スフォリア", new() { }, Gender.Female,
                "451Sfoglia", false, "MPID_Sfoglia", "Face_DarkEmblem", null, Color.FromArgb(60, 33, 19), null),
            new("Zephia", "PID_セピア", new() { "PID_M011_セピア", "PID_M014_セピア", "PID_M017_セピア", "PID_M021_セピア", "PID_M023_セピア" }, Gender.Female,
                "553Sepia", false, "MPID_Sepia", "Face_DarkEmblem", null, Color.FromArgb(233, 226, 215), null),
            new("Griss", "PID_グリ", new() { "PID_M011_グリ", "PID_M017_グリ", "PID_M020_グリ", "PID_M021_グリ", "PID_M023_グリ" }, Gender.Male,
                "503Gris", false, "MPID_Gris", "Face_DarkEmblem", null, Color.FromArgb(50, 83, 69), null),
            new("Marni", "PID_マロン", new() { "PID_M011_マロン", "PID_M014_マロン", "PID_M017_マロン", "PID_M019_マロン" }, Gender.Female,
                "554Marron", false, "MPID_Marron", "Face_DarkEmblem", null, Color.FromArgb(246, 228, 166), null),
            new("Abyme", "PID_M003_イルシオン兵_ボス", new() { "PID_M018_イルシオン兵_ボス" }, Gender.Female,
                "855Boss", false, "MPID_M003_Boss", "Face_DarkEmblem", null, Color.FromArgb(192, 196, 173), null),
            new("Rodine", "PID_M004_イルシオン兵_ボス", new() { }, Gender.Male,
                "811Boss", false, "MPID_M004_Boss", "Face_DarkEmblem", null, Color.FromArgb(201, 183, 159), null),
            new("Sean", "PID_S001_ジャン_父親", new() { }, Gender.Male,
                "", false, "MPID_JeanFather", "Face_DarkEmblem", null, Color.FromArgb(92, 95, 109), null),
            new("Anje", "PID_S001_ジャン_母親", new() { }, Gender.Female,
                "", false, "MPID_JeanMother", "Face_DarkEmblem", null, Color.FromArgb(79, 68, 100), null),
            new("Nelucce", "PID_M005_Irc_ボス", new() { }, Gender.Male,
                "812Boss", false, "MPID_M005_Boss", "Face_DarkEmblem", null, Color.FromArgb(255, 255, 255), null),
            new("Teronda", "PID_M006_ボス", new() { }, Gender.Male,
                "809Boss", false, "MPID_M006_Boss", "Face_DarkEmblem", null, Color.FromArgb(142, 115, 97), null),
            new("Mitan", "PID_S002_蛮族_お頭", new() { }, Gender.Female,
                "810Boss", false, "MPID_S002_Boss", "Face_DarkEmblem", null, Color.FromArgb(73, 73, 73), null),
            new("Corrupted Morion", "PID_M010_異形兵_モリオン", new() { }, Gender.Male,
                "202Morion", false, "MPID_MorphMorion", "Face_DarkEmblem", null, Color.FromArgb(149, 43, 47), null),
            new("Tetchie", "PID_M013_蛮族_お頭Ａ", new() { }, Gender.Male,
                "807Boss", false, "MPID_M013_BossA", "Face_DarkEmblem", null, Color.FromArgb(25, 25, 25), null),
            new("Totchie", "PID_M013_蛮族_お頭Ｂ", new() { }, Gender.Male,
                "808Boss", false, "MPID_M013_BossB", "Face_DarkEmblem", null, Color.FromArgb(25, 25, 25), null),
            new("Past Alear", "JID_邪竜ノ子", new() { "PID_M024_リュール" }, Gender.Both,
                "002Lueur", false, "MPID_PastLueur", "Face_Lueur", "Lueur", Color.FromArgb(97, 184, 231), null),
            new("Durthon", "PID_武器屋", new() { }, Gender.Male,
                "", false, "MPID_WeaponShop", "Face_DarkEmblem", null, Color.FromArgb(0, 0, 0), null),
            new("Anisse", "PID_道具屋", new() { }, Gender.Female,
                "", false, "MPID_ItemShop", "Face_DarkEmblem", null, Color.FromArgb(35, 42, 63), null),
            new("Pinet", "PID_アクセ屋", new() { }, Gender.Male,
                "", false, "MPID_AccessoriesShop", "Face_DarkEmblem", null, Color.FromArgb(192, 117, 114), null),
            new("Calney", "PID_錬成屋", new() { }, Gender.Female,
                "", false, "MPID_BlackSmith", "Face_DarkEmblem", null, Color.FromArgb(197, 179, 141), null),
        };

        internal static List<AssetShuffleEntity> AllyEmblemAssetShuffleData { get; } = new()
        {
            new("Marth", "GID_マルス", new() { "PID_S014_マルス", "PID_闘技場_マルス", "GID_M000_マルス", "GID_相手マルス" }, Gender.Male,
                "530Marth", false, "MGID_Marth", "Face_Marth", "Marth", Color.FromArgb(129, 198, 255), "EID_マルス"),
            new("Sigurd", "GID_シグルド", new() { "PID_S009_シグルド", "PID_闘技場_シグルド", "GID_M002_シグルド", "GID_相手シグルド" }, Gender.Male,
                "531Siglud", false, "MGID_Siglud", "Face_Siglud", "Siglud", Color.FromArgb(201, 216, 255), "EID_シグルド"),
            new("Celica", "GID_セリカ", new() { "PID_S013_セリカ", "PID_闘技場_セリカ", "GID_相手セリカ" }, Gender.Female,
                "580Celica", false, "MGID_Celica", "Face_Celica", "Celica", Color.FromArgb(255, 212, 245), "EID_セリカ"),
            new("Micaiah", "GID_ミカヤ", new() { "PID_S011_ミカヤ", "PID_闘技場_ミカヤ", "GID_相手ミカヤ" }, Gender.Female,
                "583Micaiah", false, "MGID_Micaiah", "Face_Micaiah", "Micaiah", Color.FromArgb(255, 240, 197), "EID_ミカヤ"),
            new("Roy", "GID_ロイ", new() { "PID_S012_ロイ", "PID_闘技場_ロイ", "GID_相手ロイ" }, Gender.Male,
                "533Roy", false, "MGID_Roy", "Face_Roy", "Roy", Color.FromArgb(254, 200, 140), "EID_ロイ"),
            new("Leif", "GID_リーフ", new() { "PID_S010_リーフ", "PID_闘技場_リーフ", "GID_相手リーフ" }, Gender.Male,
                "532Leaf", false, "MGID_Leaf", "Face_Leaf", "Leaf", Color.FromArgb(251, 254, 209), "EID_リーフ"),
            new("Lucina", "GID_ルキナ", new() { "PID_S003_ルキナ", "PID_闘技場_ルキナ", "GID_相手ルキナ" }, Gender.Female,
                "584Lucina", false, "MGID_Lucina", "Face_Lucina", "Lucina", Color.FromArgb(182, 234, 255), "EID_ルキナ"),
            new("Lyn", "GID_リン", new() { "PID_S004_リン", "PID_闘技場_リン", "GID_相手リン" }, Gender.Female,
                "581Lin", false, "MGID_Lin", "Face_Lin", "Lin", Color.FromArgb(216, 241, 178), "EID_リン"),
            new("Ike", "GID_アイク", new() { "PID_S005_アイク", "PID_闘技場_アイク", "GID_相手アイク" }, Gender.Male,
                "534Ike", false, "MGID_Ike", "Face_Ike", "Ike", Color.FromArgb(88, 90, 229), "EID_アイク"),
            new("Byleth", "GID_ベレト", new() { "PID_S006_ベレト", "PID_闘技場_ベレト", "GID_相手ベレト" }, Gender.Male,
                "535Byleth", false, "MGID_Byleth", "Face_Byleth", "Byleth", Color.FromArgb(236, 141, 255), "EID_ベレト"),
            new("Corrin", "GID_カムイ", new() { "PID_S007_カムイ", "PID_M015_カムイ", "PID_闘技場_カムイ", "GID_相手カムイ" }, Gender.Female,
                "585Kamui", false, "MGID_Kamui", "Face_Kamui", "Kamui", Color.FromArgb(188, 188, 188), "EID_カムイ"),
            new("Eirika", "GID_エイリーク", new() { "PID_S008_エイリーク", "PID_闘技場_エイリーク", "GID_相手エイリーク" }, Gender.Female,
                "582Eirik", false, "MGID_Eirik", "Face_Eirik", "Eirik", Color.FromArgb(175, 246, 230), "EID_エイリーク"),
            new("Ephraim", "GID_エフラム", new() { "GID_相手エフラム" }, Gender.Male,
                "536Ephraim", false, "MGID_Ephraim", "Face_Ephraim", "Ephraim", Color.FromArgb(175, 246, 230), null),
            new("Emblem Alear", "GID_リュール", new() { "PID_青リュール", "GID_相手リュール" }, Gender.Both,
                "001Lueur", false, "MGID_Lueur", "Face_Lueur", "Lueur", Color.FromArgb(97, 184, 231), "EID_リュール"),
            new("Edelgard", "GID_エーデルガルト", new() { "GID_相手エーデルガルト" }, Gender.Female,
                "563Edelgard", false, "MGID_Edelgard", "Face_Edelgard", "Edelgard", Color.FromArgb(78, 74, 107), "EID_エーデルガルト"),
            new("Dimitri", "GID_ディミトリ", new() { "GID_相手ディミトリ" }, Gender.Male,
                "514Dimitri", false, "MGID_Dimitri", "Face_Dimitri", "Dimitri", Color.FromArgb(78, 74, 107), null),
            new("Claude", "GID_クロード", new() { "GID_相手クロード" }, Gender.Male,
                "515Claude", false, "MGID_Claude", "Face_Claude", "Claude", Color.FromArgb(78, 74, 107), null),
            new("Tiki", "GID_チキ", new() { "PID_G001_チキ", "PID_G001_チキ_特効無効", "GID_相手チキ" }, Gender.Female,
                "560Tiki", false, "MGID_Tiki", "Face_Tiki", "Tiki", Color.FromArgb(160, 224, 160), "EID_チキ"),
            new("Hector", "GID_ヘクトル", new() { "GID_相手ヘクトル" }, Gender.Male,
                "510Hector", false, "MGID_Hector", "Face_Hector", "Hector", Color.FromArgb(46, 51, 143), "EID_ヘクトル"),
            new("Veronica", "GID_ヴェロニカ", new() { "GID_相手ヴェロニカ" }, Gender.Female,
                "562Veronica", false, "MGID_Veronica", "Face_Veronica", "Veronica", Color.FromArgb(214, 207, 197), "EID_ヴェロニカ"),
            new("Soren", "GID_セネリオ", new() { "GID_相手セネリオ" }, Gender.Male,
                "511Senerio", false, "MGID_Senerio", "Face_Senerio", "Senerio", Color.FromArgb(85, 134, 134), "EID_セネリオ"),
            new("Camilla", "GID_カミラ", new() { "GID_相手カミラ" }, Gender.Female,
                "561Camilla", false, "MGID_Camilla", "Face_Camilla", "Camilla", Color.FromArgb(191, 183, 224), "EID_カミラ"),
            new("Chrom", "GID_クロム", new() { "GID_相手クロム" }, Gender.Male,
                "512Chrom", false, "MGID_Chrom", "Face_Chrom", "Chrom", Color.FromArgb(48, 92, 129), "EID_クロム"),
            new("Robin", "PID_ルフレ", new() { }, Gender.Male,
                "513Robin", false, "MGID_Reflet", "Face_Reflet", "Reflet", Color.FromArgb(48, 92, 129), null),
        };

        internal static List<AssetShuffleEntity> EnemyEmblemAssetShuffleData { get; } = new()
        {
            new("Corrupted Marth", "GID_M011_敵マルス", new() { "PID_E003_召喚_マルス", "PID_E006_召喚_マルス", "GID_M017_敵マルス",
                "GID_M021_敵マルス", "GID_M024_敵マルス" }, Gender.Male,
                "530Marth", true, "MGID_Marth", "Face_MarthDarkness", "Marth", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Sigurd", "GID_M011_敵シグルド", new() { "PID_E003_召喚_シグルド", "PID_E006_召喚_シグルド", "GID_M017_敵シグルド",
                "PID_M022_紋章士_シグルド" }, Gender.Male,
                "531Siglud", true, "MGID_Siglud", "Face_SigludDarkness", "Siglud", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Celica", "GID_M011_敵セリカ", new() { "PID_E003_召喚_セリカ", "PID_E006_召喚_セリカ", "GID_M017_敵セリカ",
                "GID_M020_敵セリカ", "PID_M022_紋章士_セリカ" }, Gender.Female,
                "580Celica", true, "MGID_Celica", "Face_CelicaDarkness", "Celica", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Micaiah", "GID_M011_敵ミカヤ", new() { "GID_M017_敵ミカヤ", "GID_M019_敵ミカヤ", "PID_M022_紋章士_ミカヤ" }, Gender.Female,
                "583Micaiah", true, "MGID_Micaiah", "Face_MicaiahDarkness", "Micaiah", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Roy", "GID_M011_敵ロイ", new() { "PID_E006_召喚_ロイ", "GID_M017_敵ロイ", "GID_M019_敵ロイ", "PID_M022_紋章士_ロイ" }, Gender.Male,
                "533Roy", true, "MGID_Roy", "Face_RoyDarkness", "Roy", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Leif", "GID_M008_敵リーフ", new() { "PID_E006_召喚_リーフ", "GID_M011_敵リーフ", "GID_M017_敵リーフ",
                "PID_M022_紋章士_リーフ" }, Gender.Male,
                "532Leaf", true, "MGID_Leaf", "Face_LeafDarkness", "Leaf", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Lucina", "GID_M007_敵ルキナ", new() { "PID_M022_紋章士_ルキナ" }, Gender.Female,
                "584Lucina", true, "MGID_Lucina", "Face_LucinaDarkness", "Lucina", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Lyn", "GID_M010_敵リン", new() { "PID_M022_紋章士_リン" }, Gender.Female,
                "581Lin", true, "MGID_Lin", "Face_LinDarkness", "Lin", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Ike", "PID_M022_紋章士_アイク", new() { }, Gender.Male,
                "534Ike", true, "MGID_Ike", "Face_IkeDarkness", "Ike", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Byleth", "GID_M010_敵ベレト", new() { "GID_M014_敵ベレト", "PID_M022_紋章士_ベレト" }, Gender.Male,
                "535Byleth", true, "MGID_Byleth", "Face_BylethDarkness", "Byleth", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Corrin", "PID_M022_紋章士_カムイ", new() { }, Gender.Female,
                "585Kamui", true, "MGID_Kamui", "Face_KamuiDarkness", "Kamui", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Eirika", "PID_M022_紋章士_エイリーク", new() { }, Gender.Female,
                "582Eirik", true, "MGID_Eirik", "Face_EirikDarkness", "Eirik", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Edelgard", "GID_E006_敵エーデルガルト", new() { }, Gender.Female,
                "563Edelgard", true, "MGID_Edelgard", "Face_EdelgardDarkness", "Edelgard", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Dimitri", "GID_E006_敵ディミトリ", new() { }, Gender.Male,
                "514Dimitri", true, "MGID_Dimitri", "Face_DimitriDarkness", "Dimitri", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Claude", "GID_E006_敵クロード", new() { }, Gender.Male,
                "515Claude", true, "MGID_Claude", "Face_ClaudeDarkness", "Claude", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Tiki", "GID_E001_敵チキ", new() { "GID_E006_敵チキ" }, Gender.Female,
                "560Tiki", true, "MGID_Tiki", "Face_TikiDarkness", "Tiki", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Hector", "GID_E002_敵ヘクトル", new() { "GID_E005_敵ヘクトル", "GID_E006_敵ヘクトル" }, Gender.Male,
                "510Hector", true, "MGID_Hector", "Face_HectorDarkness", "Hector", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Veronica", "GID_E003_敵ヴェロニカ", new() { "GID_E005_敵ヴェロニカ", "GID_E006_敵ヴェロニカ" }, Gender.Female,
                "562Veronica", true, "MGID_Veronica", "Face_VeronicaDarkness", "Veronica", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Soren", "GID_E004_敵セネリオ", new() { "GID_E006_敵セネリオ" }, Gender.Male,
                "511Senerio", true, "MGID_Senerio", "Face_SenerioDarkness", "Senerio", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Camilla", "GID_E004_敵カミラ", new() { "GID_E006_敵カミラ" }, Gender.Female,
                "561Camilla", true, "MGID_Camilla", "Face_CamillaDarkness", "Camilla", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Chrom", "GID_E005_敵クロム", new() { "GID_E006_敵クロム" }, Gender.Male,
                "512Chrom", true, "MGID_Chrom", "Face_ChromDarkness", "Chrom", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Robin", "PID_闇ルフレ", new() { }, Gender.Male,
                "513Robin", true, "MGID_Reflet", "Face_RefletDarkness", "Reflet", Color.FromArgb(255, 0, 0), null),
        };

        internal static List<string> ExclusiveClassesList { get; } = new()
        {
            "JID_神竜ノ子", "JID_神竜ノ王", "JID_邪竜ノ子", "JID_M002_神竜ノ王",
            "JID_邪竜ノ娘", "JID_邪竜ノ娘_敵", "JID_邪竜ノ王", "JID_アヴニール下級",
            "JID_アヴニール", "JID_フロラージュ下級", "JID_フロラージュ", "JID_スュクセサール下級",
            "JID_スュクセサール", "JID_ティラユール下級", "JID_ティラユール", "JID_リンドブルム下級",
            "JID_リンドブルム", "JID_スレイプニル下級", "JID_スレイプニル", "JID_ピッチフォーク下級",
            "JID_ピッチフォーク", "JID_クピードー下級", "JID_クピードー", "JID_メリュジーヌ",
            "JID_ダンサー", "JID_紋章士_マルス", "JID_紋章士_シグルド", "JID_紋章士_セリカ",
            "JID_紋章士_ミカヤ", "JID_紋章士_ロイ", "JID_紋章士_リーフ", "JID_紋章士_ルキナ",
            "JID_紋章士_リン", "JID_紋章士_アイク", "JID_紋章士_ベレト", "JID_紋章士_カムイ",
            "JID_紋章士_エイリーク", "JID_裏邪竜ノ娘", "JID_裏邪竜ノ子", "JID_裏邪竜ノ子_E1-4",
            "JID_裏邪竜ノ子_E5", "JID_メリュジーヌ_味方", "JID_アヴニール_E", "JID_フロラージュ_E",
            "JID_スュクセサール_E", "JID_ティラユール_E", "JID_リンドブルム_E", "JID_スレイプニル_E",
            "JID_ピッチフォーク_E", "JID_クピードー_E", "JID_紋章士_エーデルガルト", "JID_紋章士_ディミトリ",
            "JID_紋章士_クロード", "JID_紋章士_チキ", "JID_紋章士_ヘクトル", "JID_紋章士_ヴェロニカ",
            "JID_紋章士_セネリオ", "JID_紋章士_カミラ", "JID_紋章士_クロム", "JID_紋章士_ルフレ",
            "JID_紋章士_ルキナ_召喚", "JID_紋章士_ヘクトル_召喚"
        };

        internal static List<string> RemoveAccList { get; } = new()
        {
            "uAcc_spine2_Hair051", "uAcc_spine2_Hair052", "uAcc_spine2_Hair150", "uAcc_spine2_Hair150k",
            "uAcc_spine2_Hair201", "uAcc_spine2_Hair201k", "uAcc_spine2_Hair350", "uAcc_spine2_Hair350k",
            "uAcc_spine2_Hair865"
        };

        internal static List<(int id, string name)> Attributes { get; } = new()
        {
            (0, "Infantry"), (1, "Cavalry"), (2, "Armored"), (3, "Flying"), (4, "Dragon"), (5, "Fell Dragon"), (6, "Corrupted"), (7, "Medeus"),
            (8, "Duma"), (9, "Loptous"), (10, "Veld"), (11, "Idunn"), (12, "Nergal"), (13, "Fomortiis"), (14, "Ashnard"), (15, "Ashera"),
            (16, "Grima"), (17, "Anankos"), (18, "Nemesis"),
        };

        internal static List<(int id, string name)> MovementTypes { get; } = new()
        {
            (1, "Infantry"), (2, "Cavalry"), (3, "Flier"),
        };

        internal static List<string> PlayerArrangementGroups { get; } = new()
        {
            "Player", "OwnArmy", "Player0"
        };

        internal static Dictionary<string, int> MaxDeployment { get; } = new()
        {
            { "E001", 35 }, { "E002", 35 }, { "E003", 36 }, { "E004", 36 }, { "E005", 36 }, { "E006", 36 },
            { "G001", 41 }, { "G002", 41 }, { "G003", 41 }, { "G004", 41 }, { "G005", 41 }, { "G006", 41 },
            { "M001", 0 }, { "M002", 0 }, { "M003", 0 }, { "M004", 6 }, { "M005", 9 }, { "M006", 10 }, { "M007", 17 }, { "M008", 20 },
            { "M009", 22 }, { "M010", 23 }, { "M011", 23 }, { "M012", 20 }, { "M013", 29 }, { "M014", 32 }, { "M015", 33 }, { "M016", 34 },
            { "M017", 36 }, { "M018", 36 }, { "M019", 37 }, { "M020", 38 }, { "M021", 38 }, { "M022", 39 }, { "M023", 40 }, { "M024", 40 },
            { "M025", 40 }, { "M026", 40 },
            { "S001", 39 }, { "S002", 39 }, { "S003", 40 }, { "S004", 40 }, { "S005", 40 }, { "S006", 40 }, { "S007", 40 }, { "S008", 40 },
            { "S009", 40 }, { "S010", 40 }, { "S011", 40 }, { "S012", 40 }, { "S013", 40 }, { "S014", 40 }, { "S015", 40 },
            { "E001E", 41 }, { "E002E", 41 }, { "E003E", 41 }, { "E004E", 41 }, { "E005E", 41 },
            { "G001E", 41 }, { "G002E", 41 }, { "G003E", 41 }, { "G004E", 41 }, { "G005E", 41 }, { "G006E", 41 },
            { "M004E", 41 }, { "M005E", 41 }, { "M006E", 41 }, { "M007E", 41 }, { "M008E", 41 },
            { "M009E", 41 }, { "M010E", 41 }, { "M011E", 41 }, { "M012E", 41 }, { "M013E", 41 }, { "M014E", 41 }, { "M015E", 41 }, { "M016E", 41 },
            { "M017E", 41 }, { "M018E", 41 }, { "M019E", 41 }, { "M020E", 41 }, { "M022E", 41 }, { "M023E", 41 },
            { "M025E", 41 },
            { "S001E", 41 }, { "S002E", 41 }, { "S003E", 41 }, { "S004E", 41 }, { "S005E", 41 }, { "S006E", 41 }, { "S007E", 41 }, { "S008E", 41 },
            { "S009E", 41 }, { "S010E", 41 }, { "S011E", 41 }, { "S012E", 41 }, { "S013E", 41 }, { "S014E", 41 }, { "S015E", 41 },
        };

        internal static HashSet<string> NoPrepMaps { get; } = new()
        {
            "M001", "M002", "M003"
        };

        internal static HashSet<string> EnemyNPCMaps { get; } = new()
        {
            "E004", "M002"
        };

        internal struct MapNode
        {
            internal string? prevMap;
            internal List<string> newUnits;

            internal MapNode(string? prevMap, List<string> newUnits)
            {
                this.prevMap = prevMap;
                this.newUnits = newUnits;
            }

            internal readonly IEnumerable<string> GetPreMapMustUnits()
            {
                if (prevMap is null)
                    return Array.Empty<string>();
                MapNode parent = MapNodes[prevMap];
                return parent.GetPreMapMustUnits().Concat(parent.newUnits);
            }
        }

        internal static Dictionary<string, MapNode> MapNodes { get; } = new()
        {
            { "E001", new("M006", new()) },
            { "E002", new("E001", new()) },
            { "E003", new("E002", new()) },
            { "E004", new("E003", new()) },
            { "E005", new("E004", new()) },
            { "E006", new("E005", new() { "PID_エル", "PID_ラファール", "PID_セレスティア", "PID_グレゴリー", "PID_マデリーン" }) },
            { "G001", new("M006", new()) },
            { "G002", new("G001", new()) },
            { "G003", new("G001", new()) },
            { "G004", new("G001", new()) },
            { "G005", new("G001", new()) },
            { "G006", new("G001", new()) },
            { "M001", new(null, new() { "PID_リュール", "PID_ヴァンドレ" }) },
            { "M002", new("M001", new() { "PID_クラン", "PID_フラン" }) },
            { "M003", new("M002", new() { "PID_アルフレッド", "PID_エーティエ", "PID_ブシュロン" }) },
            { "M004", new("M003", new() { "PID_セリーヌ", "PID_クロエ", "PID_ルイ" }) },
            { "M005", new("M004", new()) },
            { "M006", new("M005", new() { "PID_ユナカ" }) },
            { "M007", new("M006", new() { "PID_スタルーク", "PID_シトリニカ", "PID_ラピス" }) },
            { "M008", new("M007", new() { "PID_ディアマンド", "PID_アンバー" }) },
            { "M009", new("M008", new() { "PID_ジェーデ" }) },
            { "M010", new("M009", new()) },
            { "M011", new("M010", new() { "PID_アイビー", "PID_カゲツ", "PID_ゼルコバ" }) },
            { "M012", new("M011", new() { "PID_フォガート", "PID_パンドロ", "PID_ボネ" }) },
            { "M013", new("M012", new() { "PID_ミスティラ", "PID_パネトネ", "PID_メリン" }) },
            { "M014", new("M013", new() { "PID_オルテンシア" }) },
            { "M015", new("M014", new() { "PID_セアダス" }) },
            { "M016", new("M015", new() { "PID_ロサード", "PID_ゴルドマリー" }) },
            { "M017", new("M016", new()) },
            { "M018", new("M017", new() { "PID_リンデン" }) },
            { "M019", new("M018", new() { "PID_ザフィーア" }) },
            { "M020", new("M019", new()) },
            { "M021", new("M020", new() { "PID_モーヴ" }) },
            { "M022", new("M021", new() { "PID_ヴェイル" }) },
            { "M023", new("M022", new()) },
            { "M024", new("M023", new()) },
            { "M025", new("M024", new()) },
            { "M026", new("M025", new()) },
            { "S001", new("M005", new() { "PID_ジャン" }) },
            { "S002", new("M006", new() { "PID_アンナ" }) },
            { "S003", new("M011", new()) },
            { "S004", new("M012", new()) },
            { "S005", new("M013", new()) },
            { "S006", new("M014", new()) },
            { "S007", new("M015", new()) },
            { "S008", new("M016", new()) },
            { "S009", new("M017", new()) },
            { "S010", new("M017", new()) },
            { "S011", new("M018", new()) },
            { "S012", new("M018", new()) },
            { "S013", new("M020", new()) },
            { "S014", new("M022", new()) },
            { "S015", new("M022", new()) },
            { "E001E", new("E001", new()) },
            { "E002E", new("E002", new()) },
            { "E003E", new("E003", new()) },
            { "E004E", new("E004", new()) },
            { "E005E", new("E005", new()) },
            { "G001E", new("G001", new()) },
            { "G002E", new("G002", new()) },
            { "G003E", new("G003", new()) },
            { "G004E", new("G004", new()) },
            { "G005E", new("G005", new()) },
            { "G006E", new("G006", new()) },
            { "M004E", new("M004", new()) },
            { "M005E", new("M005", new()) },
            { "M006E", new("M006", new()) },
            { "M007E", new("M007", new()) },
            { "M008E", new("M008", new()) },
            { "M009E", new("M009", new()) },
            { "M010E", new("M010", new()) },
            { "M011E", new("M011", new()) },
            { "M012E", new("M012", new()) },
            { "M013E", new("M013", new()) },
            { "M014E", new("M014", new()) },
            { "M015E", new("M015", new()) },
            { "M016E", new("M016", new()) },
            { "M017E", new("M017", new()) },
            { "M018E", new("M018", new()) },
            { "M019E", new("M019", new()) },
            { "M020E", new("M020", new()) },
            { "M022E", new("M022", new()) },
            { "M023E", new("M023", new()) },
            { "M025E", new("M025", new()) },
            { "S001E", new("S001", new()) },
            { "S002E", new("S002", new()) },
            { "S003E", new("S003", new()) },
            { "S004E", new("S004", new()) },
            { "S005E", new("S005", new()) },
            { "S006E", new("S006", new()) },
            { "S007E", new("S007", new()) },
            { "S008E", new("S008", new()) },
            { "S009E", new("S009", new()) },
            { "S010E", new("S010", new()) },
            { "S011E", new("S011", new()) },
            { "S012E", new("S012", new()) },
            { "S013E", new("S013", new()) },
            { "S014E", new("S014", new()) },
            { "S015E", new("S015", new()) },
        };

        internal static HashSet<string> SupportedEnemyAttackAIs { get; } = new()
        {
            "AI_AT_EngageBlessPerson", "AI_AT_Attack", "AI_AT_EngageWait", "AI_AT_Enchant",
            "AI_AT_EngageCamilla", "AI_AT_Heal", "AI_AT_EngageAttack", "AI_AT_HealToAttack",
            "AI_AT_EngageWaitGaze", "AI_AT_Interference", "AI_AT_EngageMagicShield",
            "AI_AT_AttackToHeal", "AI_AT_EngageCSYell", "AI_AT_AttackHealHigh", "AI_AT_EngageVision",
            "AI_AT_AttackToInterference", "AI_AT_EngagePierce",
            "AI_AT_EngageAttackNoGuard", "AI_AT_EngageCSBattle", "AI_AT_RodWarp", "AI_AT_EngageDance",
            "AI_AT_EngageOverlap"
        };
        #endregion

        static GameDataLookup()
        {
            Bind(FileEnum.AI, DataSetEnum.Command, typeof(Command), "コマンド");
            Bind(FileEnum.AssetTable, DataSetEnum.Asset, typeof(Asset), "アセット");
            Bind(FileEnum.God, DataSetEnum.GodGeneral, typeof(GodGeneral), "神将");
            Bind(FileEnum.God, DataSetEnum.GrowthTable, typeof(GrowthTable), "成長表");
            Bind(FileEnum.God, DataSetEnum.BondLevel, typeof(BondLevel), "絆レベル");
            Bind(FileEnum.Item, DataSetEnum.Item, typeof(Item), "アイテム");
            Bind(FileEnum.Item, DataSetEnum.ItemCategory, typeof(ItemCategory), "カテゴリ");
            Bind(FileEnum.Item, DataSetEnum.Alchemy, typeof(Alchemy), "錬成");
            Bind(FileEnum.Item, DataSetEnum.Evolution, typeof(Evolution), "進化");
            Bind(FileEnum.Item, DataSetEnum.RefiningMaterialExchange, typeof(RefiningMaterialExchange), "錬成素材交換");
            Bind(FileEnum.Item, DataSetEnum.WeaponLevel, typeof(WeaponLevel), "武器レベル");
            Bind(FileEnum.Item, DataSetEnum.Compatibility, typeof(Compatibility), "相性");
            Bind(FileEnum.Item, DataSetEnum.Accessory, typeof(Accessory), "アクセサリ");
            Bind(FileEnum.Item, DataSetEnum.Gift, typeof(Gift), "贈り物");
            Bind(FileEnum.Item, DataSetEnum.Reward, typeof(Reward), "報酬");
            Bind(FileEnum.Item, DataSetEnum.EngageWeaponEnhancement, typeof(EngageWeaponEnhancement), "エンゲージ武器強化");
            Bind(FileEnum.Item, DataSetEnum.BattleReward, typeof(BattleReward), "対戦報酬");
            Bind(FileEnum.Job, DataSetEnum.TypeOfSoldier, typeof(TypeOfSoldier), "兵種");
            Bind(FileEnum.Job, DataSetEnum.FightingStyle, typeof(FightingStyle), "戦闘スタイル");
            Bind(FileEnum.Person, DataSetEnum.Individual, typeof(Individual), "個人");
            Bind(FileEnum.Skill, DataSetEnum.Skill, typeof(Skill), "スキル");
            Bind(FileEnum.Terrain, DataSetEnum.Terrain, typeof(Terrain), "地形");
            Bind(FileEnum.Terrain, DataSetEnum.TerrainCost, typeof(TerrainCost), "地形コスト");
            Bind(FileGroupEnum.Dispos, DataSetEnum.Arrangement, typeof(Arrangement), "配置");
            Bind(FileGroupEnum.Terrains, DataSetEnum.MapTerrain, typeof(MapTerrain), "");

            BondLevels.AddRange(BondLevelsFromExp);
            AllyBondLevelTables.AddRange(InheritableBondLevelTables);
            BondLevelTables.AddRange(AllyBondLevelTables);
            BondLevelTables.AddRange(EnemyBondLevelTables);
            PlayableCharacters.AddRange(ProtagonistCharacters);
            AllyNPCCharacters.AddRange(FixedLevelAllyNPCCharacters);
            AllyCharacters.AddRange(PlayableCharacters);
            AllyCharacters.AddRange(AllyNPCCharacters);
            FixedLevelEnemyCharacters.AddRange(FixedLevelEmblemCharacters);
            EmblemBossCharacters.AddRange(FixedLevelEmblemCharacters);
            EmblemBossCharacters.AddRange(DivineParalogueEmblemCharacters);
            NonArenaEnemyCharacters.AddRange(DivineParalogueEmblemCharacters);
            NonArenaEnemyCharacters.AddRange(FixedLevelEnemyCharacters);
            EnemyCharacters.AddRange(NonArenaEnemyCharacters);
            EnemyCharacters.AddRange(ArenaCharacters);
            Characters.AddRange(AllyCharacters);
            Characters.AddRange(EnemyCharacters);
            Characters.AddRange(OtherNPCCharacters);
            NPCCharacters.AddRange(AllyNPCCharacters);
            NPCCharacters.AddRange(EnemyCharacters);
            NonArenaNPCCharacters.AddRange(AllyNPCCharacters);
            NonArenaNPCCharacters.AddRange(NonArenaEnemyCharacters);
            FixedLevelCharacters.AddRange(PlayableCharacters);
            FixedLevelCharacters.AddRange(FixedLevelAllyNPCCharacters);
            FixedLevelCharacters.AddRange(FixedLevelEnemyCharacters);
            MaleNPCExclusiveClasses.AddRange(MaleNonEmblemNPCExclusiveClasses);
            MaleNPCExclusiveClasses.AddRange(MaleEmblemClasses);
            FemaleNPCExclusiveClasses.AddRange(FemaleNonEmblemNPCExclusiveClasses);
            FemaleNPCExclusiveClasses.AddRange(FemaleEmblemClasses);
            EmblemClasses.AddRange(MaleEmblemClasses);
            EmblemClasses.AddRange(FemaleEmblemClasses);
            PlayableClasses.AddRange(UniversalClasses);
            PlayableClasses.AddRange(MaleExclusiveClasses);
            PlayableClasses.AddRange(FemaleExclusiveClasses);
            NonEmblemGeneralClasses.AddRange(PlayableClasses);
            NonEmblemGeneralClasses.AddRange(MaleNonEmblemNPCExclusiveClasses);
            NonEmblemGeneralClasses.AddRange(FemaleNonEmblemNPCExclusiveClasses);
            GeneralClasses.AddRange(PlayableClasses);
            GeneralClasses.AddRange(MixedNPCExclusiveClasses);
            GeneralClasses.AddRange(MaleNPCExclusiveClasses);
            GeneralClasses.AddRange(FemaleNPCExclusiveClasses);
            AllClasses.AddRange(BeastClasses);
            AllClasses.AddRange(GeneralClasses);
            AllDressModels.AddRange(MaleClassDressModels);
            AllDressModels.AddRange(FemaleClassDressModels);
            AllDressModels.AddRange(MaleCorruptedClassDressModels);
            AllDressModels.AddRange(FemaleCorruptedClassDressModels);
            AllDressModels.AddRange(MalePersonalDressModels);
            AllDressModels.AddRange(FemalePersonalDressModels);
            AllDressModels.AddRange(MaleEmblemDressModels);
            AllDressModels.AddRange(FemaleEmblemDressModels);
            AllDressModels.AddRange(MaleEngageDressModels);
            AllDressModels.AddRange(FemaleEngageDressModels);
            AllDressModels.AddRange(MaleCommonDressModels);
            AllDressModels.AddRange(FemaleCommonDressModels);
            AllyEngageableEmblems.AddRange(AlearEmblems);
            AllyEngageableEmblems.AddRange(LinkableEmblems);
            AllySyncableEmblems.AddRange(AllyEngageableEmblems);
            EnemySyncableEmblems.AddRange(EnemyEngageableEmblems);
            EngageableEmblems.AddRange(AllyEngageableEmblems);
            EngageableEmblems.AddRange(EnemyEngageableEmblems);
            ArenaEmblems.AddRange(BaseArenaEmblems);
            AllyArenaSyncableEmblems.AddRange(AllySyncableEmblems);
            AllyArenaSyncableEmblems.AddRange(ArenaEmblems);
            SyncableEmblems.AddRange(AllyArenaSyncableEmblems);
            SyncableEmblems.AddRange(EnemySyncableEmblems);
            Emblems.AddRange(SyncableEmblems);
            NormalCommonWeapons.AddRange(DSwordWeapons);
            NormalCommonWeapons.AddRange(CSwordWeapons);
            NormalCommonWeapons.AddRange(BSwordWeapons);
            NormalCommonWeapons.AddRange(ASwordWeapons);
            NormalCommonWeapons.AddRange(SSwordWeapons);
            NormalCommonWeapons.AddRange(DLanceWeapons);
            NormalCommonWeapons.AddRange(CLanceWeapons);
            NormalCommonWeapons.AddRange(BLanceWeapons);
            NormalCommonWeapons.AddRange(ALanceWeapons);
            NormalCommonWeapons.AddRange(SLanceWeapons);
            NormalCommonWeapons.AddRange(DAxeWeapons);
            NormalCommonWeapons.AddRange(CAxeWeapons);
            NormalCommonWeapons.AddRange(BAxeWeapons);
            NormalCommonWeapons.AddRange(AAxeWeapons);
            NormalCommonWeapons.AddRange(SAxeWeapons);
            NormalCommonWeapons.AddRange(DBowWeapons);
            NormalCommonWeapons.AddRange(CBowWeapons);
            NormalCommonWeapons.AddRange(BBowWeapons);
            NormalCommonWeapons.AddRange(ABowWeapons);
            NormalCommonWeapons.AddRange(SBowWeapons);
            NormalCommonWeapons.AddRange(DDaggerWeapons);
            NormalCommonWeapons.AddRange(CDaggerWeapons);
            NormalCommonWeapons.AddRange(BDaggerWeapons);
            NormalCommonWeapons.AddRange(ADaggerWeapons);
            NormalCommonWeapons.AddRange(SDaggerWeapons);
            NormalCommonWeapons.AddRange(DTomeWeapons);
            NormalCommonWeapons.AddRange(CTomeWeapons);
            NormalCommonWeapons.AddRange(BTomeWeapons);
            NormalCommonWeapons.AddRange(CommonATomeWeapons);
            NormalCommonWeapons.AddRange(STomeWeapons);
            NormalCommonWeapons.AddRange(CommonDStaves);
            NormalCommonWeapons.AddRange(CommonCStaves);
            NormalCommonWeapons.AddRange(CommonBStaves);
            NormalCommonWeapons.AddRange(AStaves);
            NormalCommonWeapons.AddRange(DArtWeapons);
            NormalCommonWeapons.AddRange(CArtWeapons);
            NormalCommonWeapons.AddRange(BArtWeapons);
            NormalCommonWeapons.AddRange(AArtWeapons);
            NormalCommonWeapons.AddRange(SArtWeapons);
            NormalCommonWeapons.AddRange(DSpecialWeapons);
            NormalCommonWeapons.AddRange(CSpecialWeapons);
            NormalCommonWeapons.AddRange(BSpecialWeapons);
            NormalCommonWeapons.AddRange(SSpecialWeapons);
            NormalCommonWeapons.AddRange(LiberationWeapons);
            NormalCommonWeapons.AddRange(WilleGlanzWeapons);
            NormalCommonWeapons.AddRange(MisericordeWeapons);
            NormalCommonWeapons.AddRange(ObscuriteWeapons);
            NormalCommonWeapons.AddRange(DragonStones);
            NormalCommonWeapons.AddRange(Cannonballs);
            NormalCommonWeapons.AddRange(NormalEngageSwordWeapons);
            NormalCommonWeapons.AddRange(NormalEngageLanceWeapons);
            NormalCommonWeapons.AddRange(NormalEngageAxeWeapons);
            NormalCommonWeapons.AddRange(NormalEngageBowWeapons);
            NormalCommonWeapons.AddRange(NormalEngageDaggerWeapons);
            NormalCommonWeapons.AddRange(NormalEngageTomeWeapons);
            NormalCommonWeapons.AddRange(NormalEngageStaves);
            NormalCommonWeapons.AddRange(NormalEngageArtWeapons);
            NormalCommonWeapons.AddRange(NormalEngageSpecialWeapons);
            NormalAllyWeapons.AddRange(NormalCommonWeapons);
            NormalAllyWeapons.AddRange(AllyDStaves);
            NormalAllyWeapons.AddRange(AllyCStaves);
            NormalAllyWeapons.AddRange(AllySStaves);
            NormalEnemyWeapons.AddRange(NormalCommonWeapons);
            NormalEnemyWeapons.AddRange(EnemyATomeWeapons);
            NormalEnemyWeapons.AddRange(EnemyCStaves);
            NormalEnemyWeapons.AddRange(EnemyBStaves);
            NormalWeapons.AddRange(NormalAllyWeapons);
            NormalWeapons.AddRange(EnemyATomeWeapons);
            NormalWeapons.AddRange(EnemyCStaves);
            NormalWeapons.AddRange(EnemyBStaves);
            EnemyUsableItems.AddRange(HealItems);
            EnchantItems.AddRange(HealItems);
            EnchantItems.AddRange(NonHealEnchantItems);
            EnemyNonUsableItems.AddRange(NonHealEnchantItems);
            AllyItems.AddRange(EnemyUsableItems);
            AllyItems.AddRange(EnemyNonUsableItems);
            EnemyDropItems.AddRange(EnemyNonUsableItems);
            EnemyDropItems.AddRange(GoldItems);
            EnemyItems.AddRange(EnemyUsableItems);
            EnemyItems.AddRange(EnemyDropItems);
            AllItems.AddRange(NormalWeapons);
            AllItems.AddRange(AllyItems);
            AllItems.AddRange(EngageWeapons);
            StaticUnitMaps.AddRange(XenologueMaps);
            StaticUnitMaps.AddRange(DivineParalogueMaps);
            StaticUnitMaps.AddRange(ChapterMaps);
            StaticUnitMaps.AddRange(ParalogueMaps);
            AllMaps.AddRange(XenologueMaps);
            AllMaps.AddRange(DivineParalogueMaps);
            AllMaps.AddRange(ChapterMaps);
            AllMaps.AddRange(ParalogueMaps);
            AllMaps.AddRange(SkirmishMaps);
            CompatibleAsEngageAttacks.AddRange(TriggerAttackSkills);
            GeneralSkills.AddRange(TriggerAttackSkills);
            GeneralSkills.AddRange(BossSkills);
            VisibleSkills.AddRange(GeneralSkills);
            VisibleSkills.AddRange(RestrictedSkills);
            SyncStatSkills.AddRange(SyncHPSkills);
            SyncStatSkills.AddRange(SyncStrSkills);
            SyncStatSkills.AddRange(SyncDexSkills);
            SyncStatSkills.AddRange(SyncSpdSkills);
            SyncStatSkills.AddRange(SyncLckSkills);
            SyncStatSkills.AddRange(SyncDefSkills);
            SyncStatSkills.AddRange(SyncMagSkills);
            SyncStatSkills.AddRange(SyncResSkills);
            SyncStatSkills.AddRange(SyncBldSkills);
            SyncStatSkills.AddRange(SyncMovSkills);
            AllyWeaponLookup.Add(Proficiency.None, new() { new(), new(), new(), new(), new(), new() });
            AllyWeaponLookup.Add(Proficiency.Sword, new() { DSwordWeapons, CSwordWeapons, BSwordWeapons, ASwordWeapons, SSwordWeapons, NormalEngageSwordWeapons });
            AllyWeaponLookup.Add(Proficiency.Lance, new() { DLanceWeapons, CLanceWeapons, BLanceWeapons, ALanceWeapons, SLanceWeapons, NormalEngageLanceWeapons });
            AllyWeaponLookup.Add(Proficiency.Axe, new() { DAxeWeapons, CAxeWeapons, BAxeWeapons, AAxeWeapons, SAxeWeapons, NormalEngageAxeWeapons });
            AllyWeaponLookup.Add(Proficiency.Bow, new() { DBowWeapons, CBowWeapons, BBowWeapons, ABowWeapons, SBowWeapons, NormalEngageBowWeapons });
            AllyWeaponLookup.Add(Proficiency.Dagger, new() { DDaggerWeapons, CDaggerWeapons, BDaggerWeapons, ADaggerWeapons, SDaggerWeapons, NormalEngageDaggerWeapons });
            AllyWeaponLookup.Add(Proficiency.Tome, new() { DTomeWeapons, CTomeWeapons, BTomeWeapons, CommonATomeWeapons, STomeWeapons, NormalEngageTomeWeapons });
            AllyWeaponLookup.Add(Proficiency.Staff, new() { CommonDStaves.Concat(AllyDStaves).ToList(), CommonCStaves.Concat(AllyCStaves).ToList(), CommonBStaves,
                AStaves, AllySStaves, NormalEngageStaves });
            AllyWeaponLookup.Add(Proficiency.Arts, new() { DArtWeapons, CArtWeapons, BArtWeapons, AArtWeapons, SArtWeapons, NormalEngageArtWeapons });
            AllyWeaponLookup.Add(Proficiency.Special, new() { DSpecialWeapons, CSpecialWeapons, BSpecialWeapons, new(), SSpecialWeapons, NormalEngageSpecialWeapons });
            EnemyWeaponLookup.Add(Proficiency.None, new() { new(), new(), new(), new(), new(), new() });
            EnemyWeaponLookup.Add(Proficiency.Sword, new() { DSwordWeapons, CSwordWeapons, BSwordWeapons, ASwordWeapons, SSwordWeapons, NormalEngageSwordWeapons });
            EnemyWeaponLookup.Add(Proficiency.Lance, new() { DLanceWeapons, CLanceWeapons, BLanceWeapons, ALanceWeapons, SLanceWeapons, NormalEngageLanceWeapons });
            EnemyWeaponLookup.Add(Proficiency.Axe, new() { DAxeWeapons, CAxeWeapons, BAxeWeapons, AAxeWeapons, SAxeWeapons, NormalEngageAxeWeapons });
            EnemyWeaponLookup.Add(Proficiency.Bow, new() { DBowWeapons, CBowWeapons, BBowWeapons, ABowWeapons, SBowWeapons, NormalEngageBowWeapons });
            EnemyWeaponLookup.Add(Proficiency.Dagger, new() { DDaggerWeapons, CDaggerWeapons, BDaggerWeapons, ADaggerWeapons, SDaggerWeapons, NormalEngageDaggerWeapons });
            EnemyWeaponLookup.Add(Proficiency.Tome, new() { DTomeWeapons, CTomeWeapons, BTomeWeapons,
                CommonATomeWeapons.Concat(EnemyATomeWeapons).ToList(), STomeWeapons, NormalEngageTomeWeapons });
            EnemyWeaponLookup.Add(Proficiency.Staff, new() { CommonDStaves, CommonCStaves.Concat(EnemyCStaves).ToList(), CommonBStaves.Concat(EnemyBStaves).ToList(),
                AStaves, new(), NormalEngageStaves });
            EnemyWeaponLookup.Add(Proficiency.Arts, new() { DArtWeapons, CArtWeapons, BArtWeapons, AArtWeapons, SArtWeapons, NormalEngageArtWeapons });
            EnemyWeaponLookup.Add(Proficiency.Special, new() { DSpecialWeapons, CSpecialWeapons, BSpecialWeapons, new(), SSpecialWeapons, NormalEngageSpecialWeapons });
            SyncStatLookup.Add(SyncHPSkills.GetIDs());
            SyncStatLookup.Add(SyncStrSkills.GetIDs());
            SyncStatLookup.Add(SyncDexSkills.GetIDs());
            SyncStatLookup.Add(SyncSpdSkills.GetIDs());
            SyncStatLookup.Add(SyncLckSkills.GetIDs());
            SyncStatLookup.Add(SyncDefSkills.GetIDs());
            SyncStatLookup.Add(SyncMagSkills.GetIDs());
            SyncStatLookup.Add(SyncResSkills.GetIDs());
            SyncStatLookup.Add(SyncBldSkills.GetIDs());
            SyncStatLookup.Add(SyncMovSkills.GetIDs());
        }
    }
}
