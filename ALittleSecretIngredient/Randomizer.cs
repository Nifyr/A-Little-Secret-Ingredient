using ALittleSecretIngredient.Structs;
using System.Text;
using static ALittleSecretIngredient.Probability;
using static ALittleSecretIngredient.ColorGenerator;

namespace ALittleSecretIngredient
{
    internal class Randomizer
    {
        private GameData GD { get; }

        private StringBuilder? _changelog;
        private StringBuilder Changelog
        {
            set
            {
                _changelog = value;
            }
        }
        internal StringBuilder? PopChangelog()
        {
            StringBuilder? changelog = _changelog;
            _changelog = null;
            return changelog;
        }

        internal Randomizer(GameData gd)
        {
            GD = gd;
        }

        internal void Randomize(RandomizerSettings settings)
        {
            StringBuilder innerChangelog = new();

            AddTable(innerChangelog, RandomizeAssetTable(settings.AssetTable));
            AddTable(innerChangelog, RandomizeGodGeneral(settings.GodGeneral));
            AddTable(innerChangelog, RandomizeGrowthTable(settings.GrowthTable));
            AddTable(innerChangelog, RandomizeBondLevel(settings.BondLevel));

            if (settings.SaveChangelog && innerChangelog.Length > 0)
            {
                StringBuilder changelog = new($"\t---- A Little *Secret* Ingredient Changelog ----\nMade with artistic passion!\n\n" +
                    $"By Nifyr.\nGenerated {DateTime.Now}\n\n\n");
                changelog.AppendLine(innerChangelog.ToString());
                Changelog = changelog;
            }
        }

        private StringBuilder RandomizeAssetTable(RandomizerSettings.AssetTableSettings settings)
        {
            List<Asset> assets = GD.Get(DataSetEnum.Asset).Params.Cast<Asset>().ToList();
            List<GodGeneral> ggs = GD.Get(DataSetEnum.GodGeneral).Params.Cast<GodGeneral>().ToList();
            List<Individual> individuals = GD.Get(DataSetEnum.Individual).Params.Cast<Individual>().ToList();

            StringBuilder assetShuffleInnertable = new();
            List<List<GameData.AssetShuffleEntity>> modelSwapLists = GetModelSwapLists(settings.ModelSwap);
            foreach (List<GameData.AssetShuffleEntity> list in modelSwapLists)
                ModelSwap(assets, ggs, individuals, assetShuffleInnertable, list);

            StringBuilder assetShuffleTable = new();
            if (assetShuffleInnertable.Length > 0)
            {
                assetShuffleTable.AppendLine("---- Model Swaps ----\n");
                assetShuffleTable.AppendLine(assetShuffleInnertable.ToString());
            }

            StringBuilder outfitShuffleInnertable = new();
            GetOutfitSwapLists(settings.OutfitSwap, out List<List<string>> maleOutfitSwapLists, out List<List<string>> femaleOutfitSwapLists);
            foreach (List<string> list in maleOutfitSwapLists)
                OutfitSwap(assets, outfitShuffleInnertable, list);
            foreach (List<string> list in femaleOutfitSwapLists)
                OutfitSwap(assets, outfitShuffleInnertable, list);

            StringBuilder outfitShuffleTable = new();
            if (outfitShuffleInnertable.Length > 0)
            {
                outfitShuffleTable.AppendLine("---- Outfit Swaps ----\n");
                outfitShuffleTable.AppendLine(outfitShuffleInnertable.ToString());
            }

            StringBuilder tables = new();
            tables.AppendLine(assetShuffleTable.ToString());
            tables.AppendLine(outfitShuffleTable.ToString());

            return tables;
        }

        private void GetOutfitSwapLists(RandomizerFieldSettings settings, out List<List<string>> maleOutfitSwapLists, out List<List<string>> femaleOutfitSwapLists)
        {
            maleOutfitSwapLists = new();
            femaleOutfitSwapLists = new();
            if (settings.GetArg<bool>(0))
            {
                IEnumerable<string> maleList = GD.MaleClassDressModels.GetIDs();
                IEnumerable<string> femaleList = GD.FemaleClassDressModels.GetIDs();
                if (settings.GetArg<bool>(1))
                {
                    maleList = maleList.Concat(GD.MaleCorruptedClassDressModels.GetIDs());
                    femaleList = femaleList.Concat(GD.FemaleCorruptedClassDressModels.GetIDs());
                }
                maleOutfitSwapLists.Add(maleList.ToList());
                femaleOutfitSwapLists.Add(femaleList.ToList());
            }
            if (settings.GetArg<bool>(2))
            {
                maleOutfitSwapLists.Add(GD.MalePersonalDressModels.GetIDs());
                femaleOutfitSwapLists.Add(GD.FemalePersonalDressModels.GetIDs());
            }
            if (settings.GetArg<bool>(3))
            {
                maleOutfitSwapLists.Add(GD.MaleEmblemDressModels.GetIDs());
                femaleOutfitSwapLists.Add(GD.FemaleEmblemDressModels.GetIDs());
            }
            if (settings.GetArg<bool>(4))
            {
                maleOutfitSwapLists.Add(GD.MaleEngageDressModels.GetIDs());
                femaleOutfitSwapLists.Add(GD.FemaleEngageDressModels.GetIDs());
            }
            if (settings.GetArg<bool>(5))
            {
                maleOutfitSwapLists.Add(GD.MaleCommonDressModels.GetIDs());
                femaleOutfitSwapLists.Add(GD.FemaleCommonDressModels.GetIDs());
            }
            if (settings.GetArg<bool>(6))
            {
                maleOutfitSwapLists = new() { maleOutfitSwapLists.SelectMany(l => l).ToList() };
                femaleOutfitSwapLists = new() { femaleOutfitSwapLists.SelectMany(l => l).ToList() };
            }
        }

        private void OutfitSwap(List<Asset> assets, StringBuilder outfitShuffleInnertable, List<string> list)
        {
            List<string> shuffle = new(list);
            new Redistribution(100).Randomize(shuffle);
            Dictionary<string, string> mapping = new();
            for (int i = 0; i < list.Count; i++)
            {
                mapping.Add(list[i], shuffle[i]);
                outfitShuffleInnertable.AppendLine($"{GD.AllDressModels.IDToName(list[i])} → {GD.AllDressModels.IDToName(shuffle[i])}");
            }
            foreach (Asset a in assets)
                if (mapping.TryGetValue(a.DressModel, out string? newDressModel))
                    a.DressModel = newDressModel;
            GD.SetDirty(DataSetEnum.Asset);
        }

        private List<List<GameData.AssetShuffleEntity>> GetModelSwapLists(RandomizerFieldSettings settings)
        {
            List<List<GameData.AssetShuffleEntity>> modelSwapLists = new();

            if (settings.GetArg<bool>(0))
            {
                IEnumerable<GameData.AssetShuffleEntity> list = GD.PlayableAssetShuffleData;
                if (settings.GetArg<bool>(1))
                    list = list.Concat(GD.ProtagonistAssetShuffleData);
                modelSwapLists.Add(list.ToList());
            }
            if (settings.GetArg<bool>(2))
                modelSwapLists.Add(GD.NamedNPCAssetShuffleData);
            if (settings.GetArg<bool>(3))
            {
                IEnumerable<GameData.AssetShuffleEntity> list = GD.AllyEmblemAssetShuffleData;
                if (settings.GetArg<bool>(4))
                    list = list.Concat(GD.EnemyEmblemAssetShuffleData);
                modelSwapLists.Add(list.ToList());
            }
            if (settings.GetArg<bool>(5))
                modelSwapLists = new() { modelSwapLists.SelectMany(l => l).ToList() };
            if (settings.GetArg<bool>(6))
            {
                List<List<GameData.AssetShuffleEntity>> separatedLists = new();
                foreach (List<GameData.AssetShuffleEntity> list in modelSwapLists)
                {
                    List<GameData.AssetShuffleEntity> male = new();
                    List<GameData.AssetShuffleEntity> female = new();
                    List<GameData.AssetShuffleEntity> both = new();
                    male.AddRange(list.Where(ase => ase.gender == GameData.Gender.Male || ase.gender == GameData.Gender.Rosado));
                    female.AddRange(list.Where(ase => ase.gender == GameData.Gender.Female));
                    both.AddRange(list.Where(ase => ase.gender == GameData.Gender.Both));
                    foreach (GameData.AssetShuffleEntity ase in both)
                        if (50.0.Occur())
                            male.Insert(0, ase);
                        else
                            female.Insert(0, ase);
                    separatedLists.Add(male);
                    separatedLists.Add(female);
                }
                modelSwapLists = separatedLists;
            }

            return modelSwapLists;
        }

        private void ModelSwap(List<Asset> assets, List<GodGeneral> ggs, List<Individual> individuals,
            StringBuilder assetShuffleInnertable, IEnumerable<GameData.AssetShuffleEntity> targets)
        {
            Dictionary<string, GameData.AssetShuffleEntity> entities = new();
            foreach (GameData.AssetShuffleEntity e in targets)
                entities.Add(e.id, e);

            List<string> distinct = entities.Keys.ToList();
            List<string> shuffle = new(distinct);
            new Redistribution(100).Randomize(shuffle);
            Dictionary<string, string> conditionsMapping = new();
            for (int i = 0; i < distinct.Count; i++)
            {
                GameData.AssetShuffleEntity target = entities[distinct[i]];
                GameData.AssetShuffleEntity source = entities[shuffle[i]];
                conditionsMapping.Add(source.id, target.id);
                for (int j = 0; j < source.alternates.Count; j++)
                    conditionsMapping.Add(source.alternates[j], target.alternates.Count > 0 ? target.alternates.GetRandom() : target.id);

                List<string> targetIDs = new(target.alternates) { target.id };
                foreach (string id in targetIDs)
                    if (id.StartsWith("PID"))
                        CorrectIndividual(source, individuals.First(i0 => i0.Pid == id));
                    else if (id.StartsWith("MPID"))
                        foreach (Individual ind in individuals.Where(i0 => i0.Name == id))
                            CorrectIndividual(source, ind);
                    else if (id.StartsWith("GID"))
                        CorrectGodGeneral(source, ggs.First(gg => gg.Gid == id));

                if (GD.ProtagonistAssetShuffleData.Contains(target))
                {
                    Asset? remove = null;
                    switch (source.gender)
                    {
                        case GameData.Gender.Male:
                        case GameData.Gender.Rosado:
                            remove = assets.First(a => a.Conditions[0] == "PID_タイトル用_リュール女");
                            break;
                        case GameData.Gender.Female:
                            remove = assets.First(a => a.Conditions[0] == "PID_タイトル用_リュール男");
                            break;
                    }
                    if (remove != null)
                    {
                        remove.BodyModel = "uRig_HumnM1Invisible";
                        remove.DressModel = "uBody_null";
                        remove.HeadModel = "uHead_null";
                        remove.HairModel = "uHair_null";
                    }
                }

                if (target.eid != null)
                    foreach (Asset a in assets.Where(a0 => a0.Conditions.Any(s => s.Contains(target.eid))))
                    {
                        a.HairR = source.hair.R;
                        a.HairG = source.hair.G;
                        a.HairB = source.hair.B;
                        NormalRelative nr = new(100, 16);
                        a.GradR = (byte)Math.Clamp(nr.Next(source.hair.R), 0, 255);
                        a.GradG = (byte)Math.Clamp(nr.Next(source.hair.G), 0, 255);
                        a.GradB = (byte)Math.Clamp(nr.Next(source.hair.B), 0, 255);
                    }

                assetShuffleInnertable.AppendLine($"{target.name} → {source.name}");
            }
            foreach (Asset a in assets)
                for (int i = 0; i < a.Conditions.Length; i++)
                    foreach (string id in a.Conditions[i].Split('|').Select(s => s.Trim(' ', '!')))
                        if (conditionsMapping.TryGetValue(id, out string? newID))
                            a.Conditions[i] = a.Conditions[i].Replace(id, newID);
            GD.SetDirty(DataSetEnum.Asset);
            GD.SetDirty(DataSetEnum.GodGeneral);
            GD.SetDirty(DataSetEnum.Individual);
        }

        private static void CorrectIndividual(GameData.AssetShuffleEntity source, Individual ind)
        {
            ind.SetFlag(5, false);
            switch (source.gender)
            {
                case GameData.Gender.Male:
                    ind.Gender = 1;
                    break;
                case GameData.Gender.Female:
                    ind.Gender = 2;
                    break;
                case GameData.Gender.Rosado:
                    ind.Gender = 1;
                    ind.SetFlag(5, true);
                    break;
            }
            ind.UnitIconID = source.iconID;
            ind.Name = source.nameID;
        }

        private static void CorrectGodGeneral(GameData.AssetShuffleEntity source, GodGeneral gg)
        {
            switch (source.gender)
            {
                case GameData.Gender.Male:
                case GameData.Gender.Rosado:
                    gg.Female = 0;
                    break;
                case GameData.Gender.Female:
                    gg.Female = 1;
                    break;
            }
            gg.UnitIconID = source.iconID;
            gg.SetCorrupted(source.enemyEmblem);
            gg.Mid = source.nameID;
            if (source.thumbnail != null)
                gg.AsciiName = source.thumbnail;
            gg.FaceIconName = source.faceIconID;
            gg.FaceIconNameDarkness = source.faceIconID;
        }

        private StringBuilder RandomizeGodGeneral(RandomizerSettings.GodGeneralSettings settings)
        {
            List<GodGeneral> ggs = GD.Get(DataSetEnum.GodGeneral).Params.Cast<GodGeneral>().ToList();
            List<GodGeneral> allyEngageableEmblems = ggs.FilterData(gg => gg.Gid, GD.AllyEngageableEmblems);
            List<GodGeneral> enemyEngageableEmblems = ggs.FilterData(gg => gg.Gid, GD.EnemyEngageableEmblems);
            List<GodGeneral> allySynchableEmblems = ggs.FilterData(gg => gg.Gid, GD.AllySynchableEmblems);
            List<GodGeneral> enemySynchableEmblems = ggs.FilterData(gg => gg.Gid, GD.EnemySynchableEmblems);
            List<GodGeneral> linkableEmblems = ggs.FilterData(gg => gg.Gid, GD.LinkableEmblems);
            List<GodGeneral> baseArenaEmblems = ggs.FilterData(gg => gg.Gid, GD.BaseArenaEmblems);
            List<GodGeneral> arenaEmblems = ggs.FilterData(gg => gg.Gid, GD.ArenaEmblems);
            List<GodGeneral> allyArenaSynchableEmblems = ggs.FilterData(gg => gg.Gid, GD.AllyArenaSynchableEmblems);
            List<GodGeneral> engageableEmblems = ggs.FilterData(gg => gg.Gid, GD.EngageableEmblems);
            List<string> playableCharacterIDs = GD.PlayableCharacters.GetIDs();
            List<string> compatibleAsEngageAttackIDs = GD.CompatibleAsEngageAttacks.GetIDs();
            List<string> linkableEmblemIDs = GD.LinkableEmblems.GetIDs();
            Dictionary<GodGeneral, StringBuilder> entries = CreateStringBuilderDictionary(ggs);

            // Pair bond links
            allyEngageableEmblems.ForEach(gg => { if (gg.LinkGid != "") ggs.First(gg0 => gg0.Gid == gg.LinkGid).LinkGid = gg.Gid; });

            if (settings.Link.GetArg<bool>(0))
            {
                // Filter down selection based on "Engage+ Link % per emblem" field.
                List<GodGeneral> randomizeSelection = linkableEmblems.Where(_ => settings.Link.GetArg<double>(1).Occur()).
                    ToList();
                // Randomize
                randomizeSelection.Randomize(gg => gg.Link, (gg, s) => gg.Link = s, settings.Link.Distribution,
                    playableCharacterIDs);
                // Remove links of filtered emblems
                foreach (GodGeneral gg in linkableEmblems)
                    if (!randomizeSelection.Contains(gg))
                        gg.Link = "";
                WriteToChangelog(entries, linkableEmblems, gg => gg.Link, "Engage+ Link", GD.PlayableCharacters);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.Link.Enabled)
            {
                // Get Alear emblems
                List<GodGeneral> alearEmblems = ggs.Where(gg => GD.AlearEmblems.Select(t => t.id).Contains(gg.Gid)).ToList();
                // Randomize
                alearEmblems.Randomize(gg => gg.Link, (gg, s) => gg.Link = s, settings.Link.Distribution, playableCharacterIDs);
                WriteToChangelog(entries, alearEmblems, gg => gg.Link, "Engage+ Link", GD.PlayableCharacters);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.EngageCount.Enabled)
            {
                allySynchableEmblems.Randomize(gg => gg.EngageCount, (gg, b) => gg.EngageCount = b, settings.EngageCount.
                    Distribution, 1, byte.MaxValue);
                WriteToChangelog(entries, allySynchableEmblems, gg => gg.EngageCount, "Engage Meter Size");
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.EngageAttackAlly.Enabled)
            {
                // Randomize
                allyEngageableEmblems.Randomize(gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s, settings.
                    EngageAttackAlly.Distribution, compatibleAsEngageAttackIDs);
                // Correct Bond Link Skills
                allyEngageableEmblems.ForEach(gg => gg.EngageAttackLink = GD.EngageAttackToBondLinkSkill.TryGetValue(gg.
                    EngageAttack, out string? value) ? value : "");
                // Propagate to assosiated emblems
                Propagate(allyEngageableEmblems, allySynchableEmblems, gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s);
                Propagate(allyEngageableEmblems, allySynchableEmblems, gg => gg.EngageAttackLink, (gg, s) => gg.EngageAttackLink = s);
                // Correct AIEngageAttackType field if possible
                allyEngageableEmblems.ForEach(gg => gg.AIEngageAttackType = GD.EngageAttackToAIEngageAttackType.TryGetValue(gg.
                    EngageAttack, out sbyte value) ? value : (sbyte)0);
                WriteToChangelog(entries, allySynchableEmblems, gg => gg.EngageAttack, "Engage Attack", GD.CompatibleAsEngageAttacks);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.EngageAttackEnemy.Enabled)
            {
                // Randomize
                enemyEngageableEmblems.Randomize(gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s, settings.
                    EngageAttackEnemy.Distribution, compatibleAsEngageAttackIDs);
                // Propagate to assosiated emblems
                Propagate(enemyEngageableEmblems, enemySynchableEmblems, gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s);
                WriteToChangelog(entries, enemySynchableEmblems, gg => gg.EngageAttack, "Engage Attack", GD.CompatibleAsEngageAttacks);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.EngageAttackLink.Enabled)
            {
                // Randomize
                allyEngageableEmblems.Randomize(gg => gg.EngageAttackLink, (gg, s) => gg.EngageAttackLink = s, settings.
                    EngageAttackLink.Distribution, compatibleAsEngageAttackIDs);
                // Propagate to assosiated emblems
                Propagate(allyEngageableEmblems, allySynchableEmblems, gg => gg.EngageAttackLink, (gg, s) => gg.
                    EngageAttackLink = s);
                WriteToChangelog(entries, allySynchableEmblems, gg => gg.EngageAttackLink, "Bond Link Skill", GD.CompatibleAsEngageAttacks);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.LinkGid.Enabled)
            {
                // Randomize
                linkableEmblems.Randomize(gg => gg.LinkGid, (gg, s) => gg.LinkGid = s, settings.LinkGid.Distribution,
                    linkableEmblemIDs);
                // Force pairs if toggled
                if (settings.LinkGid.GetArg<bool>(0))
                    PairLinkGids(linkableEmblems);
                // Propagate to assosiated emblems
                Propagate(allyEngageableEmblems, allySynchableEmblems, gg => gg.LinkGid, (gg, s) => gg.LinkGid = s);
                WriteToChangelog(entries, allySynchableEmblems, gg => gg.LinkGid, "Bond Link Emblem", GD.LinkableEmblems);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.ShuffleGrowTableAlly)
            {
                // Shuffle ally emblems
                allyEngageableEmblems.Randomize(gg => gg.GrowTable, (gg, s) => gg.GrowTable = s, new Redistribution(100), null!);
                // Shuffle arena emblems
                baseArenaEmblems.Randomize(gg => gg.GrowTable, (gg, s) => gg.GrowTable = s, new Redistribution(100), null!);
                // Propagate to assosiated emblems
                Propagate(allyEngageableEmblems, allySynchableEmblems, gg => gg.GrowTable, (gg, s) => gg.GrowTable = s);
                Propagate(baseArenaEmblems, arenaEmblems, gg => gg.GrowTable, (gg, s) => gg.GrowTable = s);
                WriteToChangelog(entries, allySynchableEmblems, gg => gg.GrowTable, "Bond Level Table", GD.AllyBondLevelTables);
                WriteToChangelog(entries, arenaEmblems, gg => gg.GrowTable, "Bond Level Table", GD.AllyBondLevelTables);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.ShuffleGrowTableEnemy)
            {
                // Shuffle
                enemyEngageableEmblems.Randomize(gg => gg.GrowTable, (gg, s) => gg.GrowTable = s, new Redistribution(100), null!);
                // Propagate to assosiated emblems
                Propagate(enemyEngageableEmblems, enemySynchableEmblems, gg => gg.GrowTable, (gg, s) => gg.GrowTable = s);
                WriteToChangelog(entries, enemySynchableEmblems, gg => gg.GrowTable, "Bond Level Table", GD.EnemyBondLevelTables);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.RandomizeEngravingStats)
            {
                Dictionary<GodGeneral, StringBuilder> engravingEntries = new();
                ggs.ForEach(gg => engravingEntries.Add(gg, new()));
                void HandleEngraving(RandomizerFieldSettings rfs, Func<GodGeneral, sbyte> get, Action<GodGeneral, sbyte> set, string name)
                {
                    allyEngageableEmblems.Randomize(get, set, rfs.Distribution, sbyte.MinValue, sbyte.MaxValue);
                    Propagate(allyEngageableEmblems, allySynchableEmblems, get, set);
                    allySynchableEmblems.ForEach(o => engravingEntries[o].Append(get(o) + name + ",\t"));
                }
                HandleEngraving(settings.EngravePower, gg => gg.EngravePower, (gg, i) => gg.EngravePower = i, "Mt");
                HandleEngraving(settings.EngraveWeight, gg => gg.EngraveWeight, (gg, i) => gg.EngraveWeight = i, "Wt");
                HandleEngraving(settings.EngraveHit, gg => gg.EngraveHit, (gg, i) => gg.EngraveHit = i, "Hit");
                HandleEngraving(settings.EngraveCritical, gg => gg.EngraveCritical, (gg, i) => gg.EngraveCritical = i, "Crit");
                HandleEngraving(settings.EngraveAvoid, gg => gg.EngraveAvoid, (gg, i) => gg.EngraveAvoid = i, "Avo");
                HandleEngraving(settings.EngraveSecure, gg => gg.EngraveSecure, (gg, i) => gg.EngraveSecure = i, "Ddg");
                ggs.ForEach(gg =>
                {
                    if (engravingEntries[gg].Length > 0)
                        entries[gg].AppendLine("Engraving Bonus:\t" + engravingEntries[gg].ToString()[..^2]);
                });
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.RandomizeAllyStaticSynchStats)
            {
                Dictionary<GodGeneral, StringBuilder> statEntries = new();
                ggs.ForEach(gg => statEntries.Add(gg, new()));
                void HandleStat(RandomizerFieldSettings rfs, Func<GodGeneral, sbyte> get, Action<GodGeneral, sbyte> set, string name)
                {
                    allyArenaSynchableEmblems.Randomize(get, set, rfs.Distribution, sbyte.MinValue, sbyte.MaxValue);
                    allyArenaSynchableEmblems.ForEach(o => statEntries[o].Append(get(o) + name + ",\t"));
                }
                HandleStat(settings.SynchroEnhanceHpAlly, gg => gg.SynchroEnhanceHp, (gg, i) => gg.SynchroEnhanceHp = i, "HP");
                HandleStat(settings.SynchroEnhanceStrAlly, gg => gg.SynchroEnhanceStr, (gg, i) => gg.SynchroEnhanceStr = i, "Str");
                HandleStat(settings.SynchroEnhanceTechAlly, gg => gg.SynchroEnhanceTech, (gg, i) => gg.SynchroEnhanceTech = i, "Dex");
                HandleStat(settings.SynchroEnhanceQuickAlly, gg => gg.SynchroEnhanceQuick, (gg, i) => gg.SynchroEnhanceQuick = i, "Spd");
                HandleStat(settings.SynchroEnhanceLuckAlly, gg => gg.SynchroEnhanceLuck, (gg, i) => gg.SynchroEnhanceLuck = i, "Lck");
                HandleStat(settings.SynchroEnhanceDefAlly, gg => gg.SynchroEnhanceDef, (gg, i) => gg.SynchroEnhanceDef = i, "Def");
                HandleStat(settings.SynchroEnhanceMagicAlly, gg => gg.SynchroEnhanceMagic, (gg, i) => gg.SynchroEnhanceMagic = i, "Mag");
                HandleStat(settings.SynchroEnhanceMdefAlly, gg => gg.SynchroEnhanceMdef, (gg, i) => gg.SynchroEnhanceMdef = i, "Res");
                HandleStat(settings.SynchroEnhancePhysAlly, gg => gg.SynchroEnhancePhys, (gg, i) => gg.SynchroEnhancePhys = i, "Bld");
                HandleStat(settings.SynchroEnhanceMoveAlly, gg => gg.SynchroEnhanceMove, (gg, i) => gg.SynchroEnhanceMove = i, "Mov");
                ggs.ForEach(gg =>
                {
                    if (statEntries[gg].Length > 0)
                        entries[gg].AppendLine("Static Synch Bonus:\t" + statEntries[gg].ToString()[..^2]);
                });
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.RandomizeEnemyStaticSynchStats)
            {
                Dictionary<GodGeneral, StringBuilder> statEntries = new();
                ggs.ForEach(gg => statEntries.Add(gg, new()));
                void HandleStat(RandomizerFieldSettings rfs, Func<GodGeneral, sbyte> get, Action<GodGeneral, sbyte> set, string name)
                {
                    enemySynchableEmblems.Randomize(get, set, rfs.Distribution, sbyte.MinValue, sbyte.MaxValue);
                    enemySynchableEmblems.ForEach(o => statEntries[o].Append(get(o) + name + ",\t"));
                }
                HandleStat(settings.SynchroEnhanceHpEnemy, gg => gg.SynchroEnhanceHp, (gg, i) => gg.SynchroEnhanceHp = i, "HP");
                HandleStat(settings.SynchroEnhanceStrEnemy, gg => gg.SynchroEnhanceStr, (gg, i) => gg.SynchroEnhanceStr = i, "Str");
                HandleStat(settings.SynchroEnhanceTechEnemy, gg => gg.SynchroEnhanceTech, (gg, i) => gg.SynchroEnhanceTech = i, "Dex");
                HandleStat(settings.SynchroEnhanceQuickEnemy, gg => gg.SynchroEnhanceQuick, (gg, i) => gg.SynchroEnhanceQuick = i, "Spd");
                HandleStat(settings.SynchroEnhanceLuckEnemy, gg => gg.SynchroEnhanceLuck, (gg, i) => gg.SynchroEnhanceLuck = i, "Lck");
                HandleStat(settings.SynchroEnhanceDefEnemy, gg => gg.SynchroEnhanceDef, (gg, i) => gg.SynchroEnhanceDef = i, "Def");
                HandleStat(settings.SynchroEnhanceMagicEnemy, gg => gg.SynchroEnhanceMagic, (gg, i) => gg.SynchroEnhanceMagic = i, "Mag");
                HandleStat(settings.SynchroEnhanceMdefEnemy, gg => gg.SynchroEnhanceMdef, (gg, i) => gg.SynchroEnhanceMdef = i, "Res");
                HandleStat(settings.SynchroEnhancePhysEnemy, gg => gg.SynchroEnhancePhys, (gg, i) => gg.SynchroEnhancePhys = i, "Bld");
                HandleStat(settings.SynchroEnhanceMoveEnemy, gg => gg.SynchroEnhanceMove, (gg, i) => gg.SynchroEnhanceMove = i, "Mov");
                ggs.ForEach(gg =>
                {
                    if (statEntries[gg].Length > 0)
                        entries[gg].AppendLine("Static Synch Bonus:\t" + statEntries[gg].ToString()[..^2]);
                });
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.WeaponRestriction.Enabled)
            {
                engageableEmblems.ForEach(gg => gg.SetWeaponRestricted(settings.WeaponRestriction.GetArg<double>(0).Occur()));
                WriteToChangelog(entries, engageableEmblems, gg => gg.GetWeaponRestricted(), "Weapon Restricted");
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            StringBuilder innertable = new();
            foreach (GodGeneral gg in ggs)
                if (entries[gg].Length > 0)
                {
                    innertable.AppendLine($"\t{GD.Emblems.IDToName(gg.Gid)}:");
                    innertable.AppendLine(entries[gg].ToString());
                }

            StringBuilder table = new();
            if (innertable.Length > 0)
            {
                table.AppendLine("---- Emblems ----\n");
                table.AppendLine(innertable.ToString());
            }

            return table;
        }

        private static Dictionary<T, StringBuilder> CreateStringBuilderDictionary<T>(List<T> objects) where T : notnull
        {
            Dictionary<T, StringBuilder> entries = new();
            objects.ForEach(o => entries.Add(o, new()));
            return entries;
        }

        private StringBuilder RandomizeGrowthTable(RandomizerSettings.GrowthTableSettings settings)
        {
            List<ParamGroup> pgs = GD.Get(DataSetEnum.GrowthTable).Params.Cast<ParamGroup>().ToList();
            List<Skill> generalSkills = GD.Get(DataSetEnum.Skill).Params.Cast<Skill>().ToList().FilterData(s => s.Sid, GD.GeneralSkills);
            List<ParamGroup> inheritableBondLevelTables = pgs.FilterData(pg => pg.Name, GD.InheritableBondLevelTables);
            List<ParamGroup> allyBondLevelTables = pgs.FilterData(pg => pg.Name, GD.AllyBondLevelTables);
            List<ParamGroup> enemyBondLevelTables = pgs.FilterData(pg => pg.Name, GD.EnemyBondLevelTables);
            List<ParamGroup> bondLevelTables = pgs.FilterData(pg => pg.Name, GD.BondLevelTables);
            List<string> generalSkillIDs = GD.GeneralSkills.GetIDs();
            List<string> synchStatSkillIDs = GD.SynchStatSkills.GetIDs();
            List<string> synchMovSkillIDs = GD.SynchMovSkills.GetIDs();
            List<string> engageWeaponIDs = GD.EngageWeapons.GetIDs();
            List<int> proficiencyIDs = GD.Proficiencies.GetIDs();
            foreach (Skill s in generalSkills)
                if (s.InheritanceCost == 0 && GD.DefaultSPCost.TryGetValue(GD.GeneralSkills.First(t => t.id == s.Sid).name, out ushort value))
                    s.InheritanceCost = value;

            Dictionary<(ParamGroup pg, int gtIdx), StringBuilder> levelEntries = new();
            pgs.ForEach(pg =>
            {
                for (int i = 0; i < pg.Group.Count; i++)
                    levelEntries.Add((pg, i), new());
            });

            if (settings.InheritanceSkills.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.InheritanceSkillsCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.InheritanceSkillsCount.Distribution, inheritableBondLevelTables,
                        gt => gt.InheritanceSkills, (gt, sa) => gt.InheritanceSkills = sa, generalSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.InheritanceSkills.Distribution, inheritableBondLevelTables, generalSkillIDs,
                    gt => gt.InheritanceSkills, _ => true);
                // Write to changelog
                WriteGrowthTableArraysToChangelog(inheritableBondLevelTables, gt => gt.InheritanceSkills, levelEntries,
                    GD.GeneralSkills, "Inheritable");
                GD.SetDirty(DataSetEnum.GrowthTable);
                GD.SetDirty(DataSetEnum.Skill);
            }

            if (settings.SynchroStatSkillsAlly.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.SynchroStatSkillsAllyCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.SynchroStatSkillsAllyCount.Distribution, allyBondLevelTables,
                        gt => gt.SynchroSkills.Where(synchStatSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.SynchroSkills.Where(s => !synchStatSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.SynchroSkills = skills.ToArray();
                        }, synchStatSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.SynchroStatSkillsAlly.Distribution, allyBondLevelTables, synchStatSkillIDs,
                    gt => gt.SynchroSkills, synchStatSkillIDs.Contains);
                // Limit to 3 stats if toggled
                if (settings.SynchroStatSkillsAlly.GetArg<bool>(0))
                    LimitSynchStatsTo3(allyBondLevelTables, synchStatSkillIDs, synchMovSkillIDs);
                // Sort same stats in increasing order
                SortSynchStats(allyBondLevelTables);
                WriteGrowthTableArraysToChangelog(allyBondLevelTables, gt => gt.SynchroSkills.Where(synchStatSkillIDs.Contains),
                    levelEntries, GD.SynchStatSkills, "While Synched");
                GD.SetDirty(DataSetEnum.GrowthTable);
            }
            if (settings.SynchroStatSkillsEnemy.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.SynchroStatSkillsEnemyCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.SynchroStatSkillsEnemyCount.Distribution, enemyBondLevelTables,
                        gt => gt.SynchroSkills.Where(synchStatSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.SynchroSkills.Where(s => !synchStatSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.SynchroSkills = skills.ToArray();
                        }, synchStatSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.SynchroStatSkillsEnemy.Distribution, enemyBondLevelTables, synchStatSkillIDs,
                    gt => gt.SynchroSkills, synchStatSkillIDs.Contains);
                // Limit to 3 stats if toggled
                if (settings.SynchroStatSkillsAlly.GetArg<bool>(0))
                    LimitSynchStatsTo3(enemyBondLevelTables, synchStatSkillIDs, synchMovSkillIDs);
                // Sort same stats in increasing order
                SortSynchStats(enemyBondLevelTables);
                WriteGrowthTableArraysToChangelog(enemyBondLevelTables, gt => gt.SynchroSkills.Where(synchStatSkillIDs.Contains),
                    levelEntries, GD.SynchStatSkills, "While Synched");
                GD.SetDirty(DataSetEnum.GrowthTable);
            }
            if (settings.SynchroGeneralSkillsAlly.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.SynchroGeneralSkillsAllyCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.SynchroGeneralSkillsAllyCount.Distribution, allyBondLevelTables,
                        gt => gt.SynchroSkills.Where(generalSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.SynchroSkills.Where(s => !generalSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.SynchroSkills = skills.ToArray();
                        }, generalSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.SynchroGeneralSkillsAlly.Distribution, allyBondLevelTables, generalSkillIDs,
                    gt => gt.SynchroSkills, generalSkillIDs.Contains);
                WriteGrowthTableArraysToChangelog(allyBondLevelTables, gt => gt.SynchroSkills.Where(generalSkillIDs.Contains),
                    levelEntries, GD.GeneralSkills, "While Synched");
                GD.SetDirty(DataSetEnum.GrowthTable);
            }
            if (settings.SynchroGeneralSkillsEnemy.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.SynchroGeneralSkillsEnemyCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.SynchroGeneralSkillsEnemyCount.Distribution, enemyBondLevelTables,
                        gt => gt.SynchroSkills.Where(generalSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.SynchroSkills.Where(s => !generalSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.SynchroSkills = skills.ToArray();
                        }, generalSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.SynchroGeneralSkillsEnemy.Distribution, enemyBondLevelTables, generalSkillIDs,
                    gt => gt.SynchroSkills, generalSkillIDs.Contains);
                WriteGrowthTableArraysToChangelog(enemyBondLevelTables, gt => gt.SynchroSkills.Where(generalSkillIDs.Contains),
                    levelEntries, GD.GeneralSkills, "While Synched");
                GD.SetDirty(DataSetEnum.GrowthTable);
            }

            if (settings.EngageSkills.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.EngageSkillsCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.EngageSkillsCount.Distribution, bondLevelTables,
                        gt => gt.EngageSkills.Where(generalSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.EngageSkills.Where(s => !generalSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.EngageSkills = skills.ToArray();
                        }, generalSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.EngageSkills.Distribution, bondLevelTables, generalSkillIDs, gt => gt.EngageSkills,
                    generalSkillIDs.Contains);
                WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageSkills.Where(generalSkillIDs.Contains), levelEntries,
                    GD.GeneralSkills, "While Engaged");
                GD.SetDirty(DataSetEnum.GrowthTable);
            }

            if (settings.EngageItems.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.EngageItemsCount.Enabled)
                {
                    RandomizeGrowthTableArraySizes(settings.EngageItemsCount.Distribution, bondLevelTables,
                        gt => gt.EngageItems, (gt, sa) => gt.EngageItems = sa, engageWeaponIDs);
                    foreach (ParamGroup pg in bondLevelTables)
                        foreach (GrowthTable gt in pg.Group.Cast<GrowthTable>())
                            if (gt.EngageDragons.Length > 0)
                                gt.EngageItems = gt.EngageItems.SkipLast(gt.EngageDragons.Length).ToArray();
                }
                // Change the set of growthtables with unit specific weapons if toggled
                if (settings.EngageItems.GetArg<bool>(0))
                    SetUnitTypeSpecificItems(settings.EngageItems.GetArg<double>(1), bondLevelTables);
                // Randomize engage weapons
                RandomizeEngageItems(settings.EngageItems.Distribution, bondLevelTables, engageWeaponIDs);
                WriteEngageItemsToChangelog(bondLevelTables, levelEntries);
                GD.SetDirty(DataSetEnum.GrowthTable);
            }

            if (settings.Aptitude.Enabled)
            {
                // Randomize counts if toggled
                if (settings.AptitudeCount.Enabled)
                    RandomizeAptitudeCounts(settings.AptitudeCount.Distribution, inheritableBondLevelTables, proficiencyIDs);
                // Randomize proficiencies
                RandomizeAptitude(settings.Aptitude.Distribution, inheritableBondLevelTables, proficiencyIDs);
                WriteAptitudeToChangelog(inheritableBondLevelTables, levelEntries);
                GD.SetDirty(DataSetEnum.GrowthTable);
            }

            void HandleFlag(RandomizerFieldSettings settings, int index, string fieldName)
            {
                if (!settings.Enabled) return;
                List<Node<byte>> unlockLevels = inheritableBondLevelTables.Select(pg => new Node<byte>(pg.Group.Cast<GrowthTable>().First(gt =>
                    gt.GetFlag(index)).Level)).ToList();
                unlockLevels.Randomize(n => n.value, (n, b) => n.value = b, settings.Distribution, 1, 20);
                for (int pgIdx = 0; pgIdx < inheritableBondLevelTables.Count; pgIdx++)
                    for (int gtIdx = 0; gtIdx < inheritableBondLevelTables[pgIdx].Group.Count; gtIdx++)
                    {
                        GrowthTable gt = inheritableBondLevelTables[pgIdx].Group.Cast<GrowthTable>().ElementAt(gtIdx);
                        gt.SetFlag(index, gt.Level == unlockLevels[pgIdx].value);
                        if (gt.GetFlag(index))
                            levelEntries[(inheritableBondLevelTables[pgIdx], gtIdx)].Append(fieldName + ",\t");
                    }
                GD.SetDirty(DataSetEnum.GrowthTable);
            }

            HandleFlag(settings.SkillInheritanceLevel, 0, "Skill Inheritance Unlocked");
            HandleFlag(settings.StrongBondLevel, 1, "Strong Bond Unlocked");
            HandleFlag(settings.DeepSynergyLevel, 2, "Deep Synergy Unlocked");

            Dictionary<ParamGroup, StringBuilder> entries = new();
            pgs.ForEach(pg => entries.Add(pg, new()));
            foreach (ParamGroup pg in pgs)
                for (int gtIdx = 0; gtIdx < pg.Group.Count; gtIdx++)
                    if (levelEntries[(pg, gtIdx)].Length > 0)
                    {
                        StringBuilder formatted = InsertLineBreaks(levelEntries[(pg, gtIdx)]);
                        entries[pg].AppendLine($"Level {((GrowthTable)pg.Group[gtIdx]).Level}:\t{formatted.ToString()}");
                    }

            StringBuilder innertable = new();
            foreach (ParamGroup pg in pgs)
                if (entries[pg].Length > 0)
                {
                    innertable.AppendLine($"\t{GD.BondLevelTables.IDToName(pg.Name)}:");
                    innertable.AppendLine(entries[pg].ToString());
                }

            StringBuilder table = new();
            if (innertable.Length > 0)
            {
                table.AppendLine("---- Bond Level Tables ----\n");
                table.AppendLine(innertable.ToString());
            }

            return table;
        }

        private StringBuilder RandomizeBondLevel(RandomizerSettings.BondLevelSettings settings)
        {
            List<BondLevel> bls = GD.Get(DataSetEnum.BondLevel).Params.Cast<BondLevel>().ToList();
            List<BondLevel> bondLevelsFromExp = bls.FilterData(bl => bl.Level, GD.BondLevelsFromExp);

            Dictionary<BondLevel, StringBuilder> entries = CreateStringBuilderDictionary(bls);

            if (settings.Exp.Enabled)
            {
                // Randomize
                bondLevelsFromExp.Randomize(bl => bl.Exp, (bl, i) => bl.Exp = i, settings.Exp.Distribution, 1, int.MaxValue);
                // Make sure exp requirement increases each level
                SortProperties(bondLevelsFromExp, bl => bl.Exp, (bl, i) => bl.Exp = i);
                WriteToChangelog(entries, bondLevelsFromExp, bl => bl.Exp, "Total Exp");
                GD.SetDirty(DataSetEnum.BondLevel);
            }

            if (settings.Cost.Enabled)
            {
                bondLevelsFromExp.Randomize(bl => bl.Cost, (bl, i) => bl.Cost = i, settings.Cost.Distribution, 1, int.MaxValue);
                WriteToChangelog(entries, bondLevelsFromExp, bl => bl.Cost, "Bond Fragment Cost");
                GD.SetDirty(DataSetEnum.BondLevel);
            }

            StringBuilder innertable = new();
            foreach (BondLevel bl in bls)
                if (entries[bl].Length > 0)
                {
                    innertable.AppendLine($"\t{GD.BondLevels.IDToName(bl.Level)}:");
                    innertable.AppendLine(entries[bl].ToString());
                }

            StringBuilder table = new();
            if (innertable.Length > 0)
            {
                table.AppendLine("---- Bond Levels ----\n");
                table.AppendLine(innertable.ToString());
            }

            return table;
        }

        private static void SortProperties<T>(List<T> list, Func<T, int> get, Action<T, int> set)
        {
            List<int> expValues = list.Select(get).ToList();
            expValues.Sort();
            for (int i = 0; i < list.Count; i++)
                set(list[i], expValues[i]);
        }

        private static StringBuilder InsertLineBreaks(StringBuilder sb)
        {
            string[] items = sb.ToString()[..^2].Split('\t');
            StringBuilder formatted = new();
            for (int itemIdx = 0; itemIdx < items.Length; itemIdx++)
            {
                formatted.Append(items[itemIdx]);
                if (itemIdx < items.Length - 1)
                {
                    if (itemIdx % 3 == 2)
                        formatted.Append('\n');
                    formatted.Append('\t');
                }
            }

            return formatted;
        }

        private void WriteAptitudeToChangelog(List<ParamGroup> allyBondLevelTables, Dictionary<(ParamGroup pg, int gtIdx), StringBuilder> levelEntries)
        {
            foreach (ParamGroup pg in allyBondLevelTables)
            {
                List<GrowthTable> gts = pg.Group.Cast<GrowthTable>().ToList();
                for (int gtIdx = 0; gtIdx < gts.Count; gtIdx++)
                    foreach (int proficiency in gts[gtIdx].GetAptitudes())
                        levelEntries[(pg, gtIdx)].Append(GD.Proficiencies.IDToName(proficiency) + " Proficiency,\t");
            }
        }

        private static void RandomizeAptitude(IDistribution distribution, List<ParamGroup> allyBondLevelTables, List<int> proficiencyIDs)
        {
            AsNodeStructure(allyBondLevelTables, gt => gt.GetAptitudes().ToArray(),
                                out List<List<List<Node<int>>>> structure, out List<Node<int>> flattened);
            flattened.Randomize(n => n.value, (n, i) => n.value = i, distribution, proficiencyIDs);
            for (int pgIdx = 0; pgIdx < structure.Count; pgIdx++)
                for (int gtIdx = 0; gtIdx < structure[pgIdx].Count; gtIdx++)
                    ((GrowthTable)allyBondLevelTables[pgIdx].Group[gtIdx]).SetAptitudes(structure[pgIdx][gtIdx].Select(n => n.value).ToList());
        }

        private static void RandomizeAptitudeCounts(IDistribution distribution, List<ParamGroup> allyBondLevelTables, List<int> proficiencyIDs)
        {
            List<Node<int>> proficiencyCounts = allyBondLevelTables.Select(pg => new Node<int>(pg.Group.Cast<GrowthTable>().Select(gt =>
                                    gt.GetAptitudes().Count).Sum())).ToList();
            proficiencyCounts.Randomize(n => n.value, (n, i) => n.value = i, distribution, 0, proficiencyIDs.Count);
            AsNodeStructure(allyBondLevelTables, gt => gt.GetAptitudes().ToArray(),
                out List<List<List<Node<int>>>> structure, out List<Node<int>> flattened);
            for (int pgIdx = 0; pgIdx < structure.Count; pgIdx++)
            {
                List<List<Node<int>>> pgStructure = structure[pgIdx];
                int newCount = proficiencyCounts[pgIdx].value;
                while (pgStructure.SelectMany(o => o).Count() < newCount)
                    pgStructure.GetRandom().Add(flattened.GetRandom());
                while (pgStructure.SelectMany(o => o).Count() > newCount)
                {
                    int gtIdx = new Empirical(100, pgStructure.Select(l => l.Count).ToList()).Next(0);
                    pgStructure[gtIdx].Remove(pgStructure[gtIdx].GetRandom());
                }
                for (int gtIdx = 0; gtIdx < pgStructure.Count; gtIdx++)
                    allyBondLevelTables[pgIdx].Group.Cast<GrowthTable>().ElementAt(gtIdx).SetAptitudes(
                        pgStructure[gtIdx].Select(n => n.value).ToList());
            }
        }

        private void WriteEngageItemsToChangelog(List<ParamGroup> bondLevelTables, Dictionary<(ParamGroup pg, int gtIdx), StringBuilder> levelEntries)
        {
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageItems, levelEntries, GD.EngageWeapons, "Available");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageCooperations, levelEntries, GD.EngageWeapons, "For Backups");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageHorses, levelEntries, GD.EngageWeapons, "For Cavalry");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageCoverts, levelEntries, GD.EngageWeapons, "For Coverts");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageHeavys, levelEntries, GD.EngageWeapons, "For Armored");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageFlys, levelEntries, GD.EngageWeapons, "For Fliers");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageMagics, levelEntries, GD.EngageWeapons, "For Mystics");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngagePranas, levelEntries, GD.EngageWeapons, "For Qi Adepts");
            WriteGrowthTableArraysToChangelog(bondLevelTables, gt => gt.EngageDragons, levelEntries, GD.EngageWeapons, "For Dragons");
        }

        private static void RandomizeEngageItems(IDistribution distribution, List<ParamGroup> bondLevelTables, List<string> engageWeaponIDs)
        {
            AsNodeStructure(bondLevelTables, gt => gt.EngageItems, out List<List<List<Node<string>>>> itemStructure, out List<Node<string>> flattened0);
            AsNodeStructure(bondLevelTables, gt => gt.EngageCooperations, out List<List<List<Node<string>>>> coopStructure, out List<Node<string>> flattened1);
            AsNodeStructure(bondLevelTables, gt => gt.EngageHorses, out List<List<List<Node<string>>>> horseStructure, out List<Node<string>> flattened2);
            AsNodeStructure(bondLevelTables, gt => gt.EngageCoverts, out List<List<List<Node<string>>>> covertStructure, out List<Node<string>> flattened3);
            AsNodeStructure(bondLevelTables, gt => gt.EngageHeavys, out List<List<List<Node<string>>>> heavyStructure, out List<Node<string>> flattened4);
            AsNodeStructure(bondLevelTables, gt => gt.EngageFlys, out List<List<List<Node<string>>>> flyStructure, out List<Node<string>> flattened5);
            AsNodeStructure(bondLevelTables, gt => gt.EngageMagics, out List<List<List<Node<string>>>> magicStructure, out List<Node<string>> flattened6);
            AsNodeStructure(bondLevelTables, gt => gt.EngagePranas, out List<List<List<Node<string>>>> pranaStructure, out List<Node<string>> flattened7);
            AsNodeStructure(bondLevelTables, gt => gt.EngageDragons, out List<List<List<Node<string>>>> dragonStructure, out List<Node<string>> flattened8);
            List<Node<string>> allItems = flattened0.Concat(flattened1).Concat(flattened2).Concat(flattened3).Concat(flattened4).
                Concat(flattened5).Concat(flattened6).Concat(flattened7).Concat(flattened8).ToList(); // Super flat. I like it.
            allItems.Randomize(n => n.value, (n, s) => n.value = s, distribution, engageWeaponIDs);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageItems, itemStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageCooperations, coopStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageHorses, horseStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageCoverts, covertStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageHeavys, heavyStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageFlys, flyStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageMagics, magicStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngagePranas, pranaStructure);
            WriteNodeStructure(bondLevelTables, gt => gt.EngageDragons, dragonStructure);
        }

        private static void SetUnitTypeSpecificItems(double p, List<ParamGroup> bondLevelTables)
        {
            foreach (ParamGroup pg in bondLevelTables)
                foreach (GrowthTable gt in pg.Group.Cast<GrowthTable>())
                {
                    gt.EngageItems = gt.EngageItems.Concat(gt.EngageDragons).ToArray();
                    gt.EngageCooperations = Array.Empty<string>();
                    gt.EngageHorses = Array.Empty<string>();
                    gt.EngageCoverts = Array.Empty<string>();
                    gt.EngageHeavys = Array.Empty<string>();
                    gt.EngageFlys = Array.Empty<string>();
                    gt.EngageMagics = Array.Empty<string>();
                    gt.EngagePranas = Array.Empty<string>();
                    gt.EngageDragons = Array.Empty<string>();

                    for (int i = 0; i < gt.EngageItems.Length; i++)
                        if (p.Occur())
                        {
                            gt.EngageCooperations = gt.EngageCooperations.Append(gt.EngageItems[i]).ToArray();
                            gt.EngageHorses = gt.EngageHorses.Append(gt.EngageItems[i]).ToArray();
                            gt.EngageCoverts = gt.EngageCoverts.Append(gt.EngageItems[i]).ToArray();
                            gt.EngageHeavys = gt.EngageHeavys.Append(gt.EngageItems[i]).ToArray();
                            gt.EngageFlys = gt.EngageFlys.Append(gt.EngageItems[i]).ToArray();
                            gt.EngageMagics = gt.EngageMagics.Append(gt.EngageItems[i]).ToArray();
                            gt.EngagePranas = gt.EngagePranas.Append(gt.EngageItems[i]).ToArray();
                            gt.EngageDragons = gt.EngageDragons.Append(gt.EngageItems[i]).ToArray();
                        }
                    gt.EngageItems = gt.EngageItems.Where(s => !gt.EngageDragons.Contains(s)).ToArray();
                }
        }

        private void SortSynchStats(List<ParamGroup> pgs)
        {
            foreach (ParamGroup pg in pgs)
            {
                AsNodeStructure(pg, gt => gt.SynchroSkills, out List<List<Node<string>>> structure, out List<Node<string>> flattened);
                for (int a = 0; a < GD.SynchStatLookup.Count; a++)
                {
                    List<Node<string>> singleStat = flattened.Where(n => GD.SynchStatLookup[a].Contains(n.value)).ToList();
                    List<string> skills = singleStat.Select(n => n.value).ToList();
                    skills.Sort((s0, s1) => GD.SynchStatLookup[a].IndexOf(s0) - GD.SynchStatLookup[a].IndexOf(s1));
                    for (int b = 0; b < singleStat.Count; b++)
                        singleStat[b].value = skills[b];
                }
                WriteNodeStructure(pg, gt => gt.SynchroSkills, structure);
            }
        }

        private void LimitSynchStatsTo3(List<ParamGroup> pgs, List<string> synchStatSkillIDs, List<string> synchMovSkillIDs)
        {
            foreach (ParamGroup pg in pgs)
            {
                AsNodeStructure(pg, gt => gt.SynchroSkills,
                    out List<List<Node<string>>> structure, out List<Node<string>> flattened);
                flattened = flattened.Where(n => synchStatSkillIDs.Contains(n.value)).ToList();
                List<string> search = flattened.Select(n => n.value).ToList();
                new Redistribution(100).Randomize(search);
                List<GameData.SynchStat> stats = new();
                while (stats.Count < 3 && search.Count > 0)
                {
                    string s = search.Last();
                    search.RemoveAt(search.Count - 1);
                    GameData.SynchStat stat = GameData.SynchStat.None;
                    for (int i = 0; stat == GameData.SynchStat.None && i < GD.SynchStatLookup.Count; i++)
                        if (GD.SynchStatLookup[i].Contains(s))
                            stat = (GameData.SynchStat)i;
                    if (!stats.Contains(stat) && stat != GameData.SynchStat.Mov)
                        stats.Add(stat);
                }
                List<string> allowedSkills = stats.SelectMany(ss => GD.SynchStatLookup[(int)ss]).ToList();
                foreach (Node<string> n in flattened)
                    if (!allowedSkills.Contains(n.value) && !synchMovSkillIDs.Contains(n.value))
                        n.value = allowedSkills.GetRandom();
                WriteNodeStructure(pg, gt => gt.SynchroSkills, structure);
            }
        }

        private static void WriteGrowthTableArraysToChangelog(List<ParamGroup> pgs, Func<GrowthTable, IEnumerable<string>> getArray,
            Dictionary<(ParamGroup pg, int gtIdx), StringBuilder> levelEntries, List<(string id, string name)> names, string fieldName)
        {
            foreach (ParamGroup pg in pgs)
                for (int gtIdx = 0; gtIdx < pg.Group.Count; gtIdx++)
                    foreach (string skillID in getArray((GrowthTable)pg.Group[gtIdx]))
                        levelEntries[(pg, gtIdx)].Append(
                            $"{names.IDToName(skillID)} {fieldName},\t");
        }

        private static void RandomizeGrowthTableArrays(IDistribution distribution, List<ParamGroup> pgs, List<string> pool,
            Func<GrowthTable, string[]> getArray, Func<string, bool> include)
        {
            AsNodeStructure(pgs, getArray, out List<List<List<Node<string>>>> structure, out List<Node<string>> flattened);
            flattened = flattened.Where(n => include(n.value)).ToList();
            flattened.Randomize(n => n.value, (n, s) => n.value = s, distribution, pool);
            WriteNodeStructure(pgs, getArray, structure);
        }

        private static void AsNodeStructure<T>(List<ParamGroup> pgs, Func<GrowthTable, T[]> getArray, out List<List<List<Node<T>>>> structure, out List<Node<T>> flattened)
        {
            structure = pgs.Select(pg => pg.Group.Select(gp =>
                                            getArray((GrowthTable)gp).Select(s => new Node<T>(s)).ToList()).ToList()).ToList();
            flattened = structure.SelectMany(l => l).SelectMany(l => l).ToList();
        }

        private static void AsNodeStructure(ParamGroup pg, Func<GrowthTable, string[]> getArray, out List<List<Node<string>>> structure, out List<Node<string>> flattened)
        {
            structure = pg.Group.Select(gp => getArray((GrowthTable)gp).Select(s => new Node<string>(s)).ToList()).ToList();
            flattened = structure.SelectMany(l => l).ToList();
        }

        private static void WriteNodeStructure<T>(List<ParamGroup> pgs, Func<GrowthTable, T[]> getArray,
            List<List<List<Node<T>>>> structure)
        {
            for (int pgIdx = 0; pgIdx < structure.Count; pgIdx++)
                for (int gtIdx = 0; gtIdx < structure[pgIdx].Count; gtIdx++)
                    for (int elemIdx = 0; elemIdx < structure[pgIdx][gtIdx].Count; elemIdx++)
                        getArray((GrowthTable)pgs[pgIdx].Group[gtIdx])[elemIdx] =
                            structure[pgIdx][gtIdx][elemIdx].value;
        }

        private static void WriteNodeStructure(ParamGroup pg, Func<GrowthTable, string[]> getArray, List<List<Node<string>>> structure)
        {
            for (int gtIdx = 0; gtIdx < structure.Count; gtIdx++)
                for (int skillIdx = 0; skillIdx < structure[gtIdx].Count; skillIdx++)
                    getArray((GrowthTable)pg.Group[gtIdx])[skillIdx] =
                        structure[gtIdx][skillIdx].value;
        }

        private static void RandomizeGrowthTableArraySizes(IDistribution distribution, List<ParamGroup> pgs, Func<GrowthTable, string[]> getArray,
            Action<GrowthTable, string[]> setArray, List<string> emergencyPool)
        {
            List<Node<int>> skillCounts = pgs.Select(pg => new Node<int>(
                                    pg.Group.Cast<GrowthTable>().Select(gt => getArray(gt).Length).Sum())).ToList();
            skillCounts.Randomize(n => n.value, (n, i) => n.value = i, distribution, 0, 255);
            for (int i = 0; i < pgs.Count; i++)
            {
                ParamGroup pg = pgs[i];
                List<GrowthTable> gts = pg.Group.Cast<GrowthTable>().ToList();
                int oldCount = gts.Select(gt => getArray(gt).Length).Sum();
                int newCount = skillCounts[i].value;
                while (oldCount < newCount)
                {
                    string newElem = gts.SelectMany(getArray).Any() ? gts.SelectMany(getArray).GetRandom() : emergencyPool.GetRandom();
                    int growthTableIdx = RNG.Next(gts.Count);
                    setArray(gts[growthTableIdx], getArray(gts[growthTableIdx]).Append(newElem).ToArray());
                    oldCount++;
                }
                while (oldCount > newCount)
                {
                    int growthTableIdx = new Empirical(100, gts.Select(gt => getArray(gt).Length).ToList()).Next(0);
                    string remove = getArray(gts[growthTableIdx]).GetRandom();
                    setArray(gts[growthTableIdx], getArray(gts[growthTableIdx]).Where(s => s != remove).ToArray());
                    oldCount--;
                }
            }
        }

        private static void Propagate<T>(List<GodGeneral> from, IEnumerable<GodGeneral> to, Func<GodGeneral, T> get,
            Action<GodGeneral, T> set) => from.ForEach(gg => to.Where(gg0 => gg.Change.Contains(gg0.Gid)).ToList().ForEach(gg0
                => set(gg0, get(gg))));

        private static void PairLinkGids(IEnumerable<GodGeneral> linkableEmblems)
        {
            // Attach paired bool
            List<PairNode<GodGeneral>> unpaired = linkableEmblems.Select(gg => new PairNode<GodGeneral>(gg)).ToList();
            // Set to paired if not linked or if in a loop of 2
            unpaired.Where(pn => pn.value.LinkGid == "" ||
            (pn.value.Gid == linkableEmblems.First(gg => gg.Gid == pn.value.LinkGid).LinkGid &&
            pn.value.LinkGid != pn.value.Gid)).ToList().ForEach(pn => pn.paired = true);
            // Get the ones who have yet to be paired
            unpaired = unpaired.Where(t => !t.paired).ToList();
            // If there's only one, leave it without a pair
            if (unpaired.Count == 1)
            {
                PairNode<GodGeneral> pn = unpaired.Single();
                pn.value.LinkGid = "";
                pn.paired = true;
            }

            while (unpaired.Any(pn => !pn.paired))
            {
                // Get the ones who have yet to be paired
                unpaired = unpaired.Where(t => !t.paired).ToList();
                // Get a random one
                PairNode<GodGeneral> pn0 = unpaired.GetRandom();
                // Get another one, preferably the one pn0 points at.
                PairNode<GodGeneral> pn1 = unpaired.FirstOrDefault(pn => pn.value.Gid == pn0.value.LinkGid) ??
                    unpaired.Where(pn => pn != pn0).GetRandom();
                // Pair them
                pn0.value.LinkGid = pn1.value.Gid;
                pn1.value.LinkGid = pn0.value.Gid;
                pn0.paired = true;
                pn1.paired = true;
                // Get the ones who have yet to be paired
                unpaired = unpaired.Where(t => !t.paired).ToList();
                // If there's only one, leave it without a pair
                if (unpaired.Count == 1)
                {
                    PairNode<GodGeneral> pn = unpaired.Single();
                    pn.value.LinkGid = "";
                    pn.paired = true;
                }
            }
        }

        private class PairNode<T>
        {
            internal bool paired;
            internal T value;
            internal PairNode(T t) => value = t;
        }

        private class Node<T>
        {
            internal T value;
            internal Node(T t) => value = t;
        }

        private static void WriteToChangelog<T>(Dictionary<T, StringBuilder> changelogEntries, List<T> objects,
            Func<T, string> property, string propertyName, List<(string id, string name)> type) where T : notnull =>
            objects.ForEach(o =>
                {
                    string name = "None";
                    if (property(o) != "")
                        name = type.IDToName(property(o));
                    changelogEntries[o].AppendLine(propertyName + ":\t" + name);
                });
        private static void WriteToChangelog<A, B>(Dictionary<A, StringBuilder> changelogEntries, List<A> objects,
            Func<A, B> property, string propertyName) where A : notnull =>
            objects.ForEach(o => changelogEntries[o].AppendLine(propertyName + ":\t" + property(o)));

        private static void AddTable(StringBuilder changelog, StringBuilder table)
        {
            if (table.Length > 0)
                changelog.AppendLine(table.ToString());
        }
    }
}
