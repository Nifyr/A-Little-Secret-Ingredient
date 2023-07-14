using ALittleSecretIngredient.Structs;
using System.Collections.Generic;
using System.Text;
using static ALittleSecretIngredient.ColorGenerator;
using static ALittleSecretIngredient.Probability;

namespace ALittleSecretIngredient
{
    internal class Randomizer
    {
        private GameData GD { get; }

        private StringBuilder? _changelog;

        private Dictionary<string, string> CharacterNameMapping { get; set; }
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
            CharacterNameMapping = GD.Characters.Concat(GD.Emblems).Select(t => t.name).ToDictionary(s => s);
        }

        internal void Randomize(RandomizerSettings settings)
        {
            // This calls a function called with a function that returns a function called with a function called with a function that returns a
            // function called with a function called with a function that does nothing as argument as argument as argument as argument as argument.
            // Use in case of emergency.
            ((Action<Func<Action<Action<Func<Action<Action<Action>>>>>>>)(f => f()(f => f()(f => f()))))(() => f => f(() => f => f(() => { })));

            StringBuilder innerChangelog = new();

            DataPreAdjustment(settings);

            if (settings.AssetTable.Any())
                AddTable(innerChangelog, RandomizeAssetTable(settings.AssetTable));
            if (settings.GodGeneral.Any())
                AddTable(innerChangelog, RandomizeGodGeneral(settings.GodGeneral));
            if (settings.GrowthTable.Any())
                AddTable(innerChangelog, RandomizeGrowthTable(settings.GrowthTable));
            if (settings.BondLevel.Any())
                AddTable(innerChangelog, RandomizeBondLevel(settings.BondLevel));
            if (settings.TypeOfSoldier.Any())
                AddTable(innerChangelog, RandomizeTypeOfSoldier(settings.TypeOfSoldier));
            if (settings.Individual.Any())
                AddTable(innerChangelog, RandomizeIndividual(settings.Individual));

            DataPostAdjustment(settings);

            if (settings.SaveChangelog && innerChangelog.Length > 0)
            {
                StringBuilder changelog = new($"\t---- A Little *Secret* Ingredient Changelog ----\nMade with artistic passion!\n\n" +
                    $"By Nifyr.\nGenerated {DateTime.Now}\n\n\n");
                changelog.AppendLine(innerChangelog.ToString());
                Changelog = changelog;
            }
        }

        private void DataPreAdjustment(RandomizerSettings settings)
        {
            List<Asset>? assets = null;
            if (settings.Individual.JidAlly.Enabled)
                assets = GD.Get(DataSetEnum.Asset).Params.Cast<Asset>().ToList();

            // Set up comment modify events for Cobalt - OBSOLETE AS OF COBALT 0.9.0
            //for (int i = 0; i < assets.Count; i++)
            //{
            //    int index = i;
            //    assets[i].OnModified += a => a.Comment = "!Modify " + index;
            //}

            // Unlock dragon stuff for fell child classes
            if (assets != null)
                foreach (Asset a in assets)
                    switch(string.Join(';', a.Conditions))
                    {
                        case "竜化;MPID_El":
                            a.Conditions = new string[] { "竜化", "JID_裏邪竜ノ娘" };
                            continue;
                        case "エンゲージ中;MPID_El;JID_裏邪竜ノ娘;竜石":
                            a.Conditions = new string[] { "エンゲージ中", "JID_裏邪竜ノ娘", "竜石" };
                            continue; 
                        case "竜化;MPID_Il|MPID_Rafale":
                            a.Conditions = new string[] { "竜化", "JID_E006ラスボス|JID_裏邪竜ノ子" };
                            continue;
                        case "エンゲージ中;MPID_Rafale;JID_裏邪竜ノ子;竜石":
                            a.Conditions = new string[] { "エンゲージ中", "JID_裏邪竜ノ子", "竜石" };
                            continue;
                    }
        }

        private void DataPostAdjustment(RandomizerSettings settings)
        {
            // Create dragon individuals
            DataSet? individualDataSet = null; 
            if (settings.Individual.JidAlly.Enabled)
                individualDataSet = GD.Get(DataSetEnum.Individual);

            if (individualDataSet != null)
            {
                List<Individual> individuals = individualDataSet.Params.Cast<Individual>().ToList();
                for (int iIdx = 0; iIdx < individuals.Count; iIdx++)
                    if (!individuals[iIdx].Pid.EndsWith("_竜化") && !individuals.Any(i => i.Pid == individuals[iIdx].Pid + "_竜化"))
                        switch (individuals[iIdx].Jid)
                        {
                            case "JID_裏邪竜ノ娘":
                                Individual newI0 = (Individual)individuals[iIdx].Clone();
                                newI0.Pid = individuals[iIdx].Pid + "_竜化";
                                newI0.Fid = individuals[iIdx].Pid + "_竜化";
                                newI0.Aid = "AID_エル竜化";
                                newI0.Items = Array.Empty<string>();
                                individuals.Add(newI0);
                                continue;
                            case "JID_裏邪竜ノ子":
                                Individual newI1 = (Individual)individuals[iIdx].Clone();
                                newI1.Pid = individuals[iIdx].Pid + "_竜化";
                                newI1.Fid = individuals[iIdx].Pid + "_竜化";
                                newI1.Aid = "AID_ラファール竜化";
                                newI1.Items = Array.Empty<string>();
                                individuals.Add(newI1);
                                continue;
                        }
                individualDataSet.Params = individuals.Cast<DataParam>().ToList();
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
            StringBuilder assetShuffleTable = ApplyTableTitle(assetShuffleInnertable, "Model Swaps");

            StringBuilder outfitShuffleInnertable = new();
            GetOutfitSwapLists(settings.OutfitSwap, out List<List<string>> maleOutfitSwapLists,
                out List<List<string>> femaleOutfitSwapLists);
            foreach (List<string> list in maleOutfitSwapLists)
                OutfitSwap(assets, outfitShuffleInnertable, list);
            foreach (List<string> list in femaleOutfitSwapLists)
                OutfitSwap(assets, outfitShuffleInnertable, list);
            StringBuilder outfitShuffleTable = ApplyTableTitle(outfitShuffleInnertable, "Outfit Swaps");

            foreach (Asset a in assets)
                RandomizeColors(settings, a);

            StringBuilder mountModelShuffleInnertable = new();
            if (settings.ShuffleRideDressModel)
                ShuffleMountModels(assets, mountModelShuffleInnertable);
            StringBuilder mountModelShuffleTable = ApplyTableTitle(mountModelShuffleInnertable, "Mount Model Swaps");

            StringBuilder infoAnimShuffleInnertable = new();
            if (settings.InfoAnim.Enabled)
                ShuffleInfoAnims(assets, infoAnimShuffleInnertable, settings.InfoAnim.GetArg<bool>(0));
            StringBuilder infoAnimShuffleTable = ApplyTableTitle(infoAnimShuffleInnertable, "Menu Character Animation Swaps");

            StringBuilder talkAnimShuffleInnertable = new();
            if (settings.ShuffleTalkAnims)
                ShuffleTalkAnims(assets, talkAnimShuffleInnertable);
            StringBuilder talkAnimShuffleTable = ApplyTableTitle(talkAnimShuffleInnertable, "Text Box Character Animation Swaps");

            StringBuilder demoAnimShuffleInnertable = new();
            if (settings.DemoAnim.Enabled)
                ShuffleDemoAnims(assets, demoAnimShuffleInnertable, settings.DemoAnim.GetArg<bool>(0));
            StringBuilder demoAnimShuffleTable = ApplyTableTitle(demoAnimShuffleInnertable, "Cutscene Character Animation Swaps");

            StringBuilder hubAnimShuffleInnertable = new();
            if (settings.ShuffleHubAnims)
                ShuffleHubAnims(assets, hubAnimShuffleInnertable);
            StringBuilder hubAnimShuffleTable = ApplyTableTitle(hubAnimShuffleInnertable, "Hub Character Animation Swaps");

            //Exclude the assets that adjusts sizes for engage outfits, randomizing them often results in stick-figures
            if (settings.RandomizeModelParameters)
                RandomizeModelParameters(settings, assets.Where(a => a.Conditions.Length != 1 || a.Conditions.First() != "エンゲージ中"));

            StringBuilder tables = new();
            if (assetShuffleTable.Length > 0)
                tables.AppendLine(assetShuffleTable.ToString());
            if (outfitShuffleTable.Length > 0)
                tables.AppendLine(outfitShuffleTable.ToString());
            if (mountModelShuffleTable.Length > 0)
                tables.AppendLine(mountModelShuffleTable.ToString());
            if (infoAnimShuffleTable.Length > 0)
                tables.AppendLine(infoAnimShuffleTable.ToString());
            if (talkAnimShuffleTable.Length > 0)
                tables.AppendLine(talkAnimShuffleTable.ToString());
            if (demoAnimShuffleTable.Length > 0)
                tables.AppendLine(demoAnimShuffleTable.ToString());
            if (hubAnimShuffleTable.Length > 0)
                tables.AppendLine(hubAnimShuffleTable.ToString());

            return tables;
        }

        private void RandomizeModelParameters(RandomizerSettings.AssetTableSettings settings, IEnumerable<Asset> assets)
        {
            assets.Where(a => a.ScaleAll != 0).ToList().Randomize(a => a.ScaleAll, (a, f) => a.ScaleAll = f,
                settings.ScaleAll.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleHead != 0).ToList().Randomize(a => a.ScaleHead, (a, f) => a.ScaleHead = f,
                settings.ScaleHead.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleNeck != 0).ToList().Randomize(a => a.ScaleNeck, (a, f) => a.ScaleNeck = f,
                settings.ScaleNeck.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleTorso != 0).ToList().Randomize(a => a.ScaleTorso, (a, f) => a.ScaleTorso = f,
                settings.ScaleTorso.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleShoulders != 0).ToList().Randomize(a => a.ScaleShoulders, (a, f) => a.ScaleShoulders = f,
                settings.ScaleShoulders.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleArms != 0).ToList().Randomize(a => a.ScaleArms, (a, f) => a.ScaleArms = f,
                settings.ScaleArms.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleHands != 0).ToList().Randomize(a => a.ScaleHands, (a, f) => a.ScaleHands = f,
                settings.ScaleHands.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleLegs != 0).ToList().Randomize(a => a.ScaleLegs, (a, f) => a.ScaleLegs = f,
                settings.ScaleLegs.Distribution, 0, float.MaxValue);
            assets.Where(a => a.ScaleFeet != 0).ToList().Randomize(a => a.ScaleFeet, (a, f) => a.ScaleFeet = f,
                settings.ScaleFeet.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeArms != 0).ToList().Randomize(a => a.VolumeArms, (a, f) => a.VolumeArms = f,
                settings.VolumeArms.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeLegs != 0).ToList().Randomize(a => a.VolumeLegs, (a, f) => a.VolumeLegs = f,
                settings.VolumeLegs.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeBust != 0).ToList().Randomize(a => a.VolumeBust, (a, f) => a.VolumeBust = f,
                settings.VolumeBust.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeAbdomen != 0).ToList().Randomize(a => a.VolumeAbdomen, (a, f) => a.VolumeAbdomen = f,
                settings.VolumeAbdomen.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeTorso != 0).ToList().Randomize(a => a.VolumeTorso, (a, f) => a.VolumeTorso = f,
                settings.VolumeTorso.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeScaleArms != 0).ToList().Randomize(a => a.VolumeScaleArms, (a, f) => a.VolumeScaleArms = f,
                settings.VolumeScaleArms.Distribution, 0, float.MaxValue);
            assets.Where(a => a.VolumeScaleLegs != 0).ToList().Randomize(a => a.VolumeScaleLegs, (a, f) => a.VolumeScaleLegs = f,
                settings.VolumeScaleLegs.Distribution, 0, float.MaxValue);
            assets.Where(a => a.MapScaleAll != 0).ToList().Randomize(a => a.MapScaleAll, (a, f) => a.MapScaleAll = f,
                settings.MapScaleAll.Distribution, 0, float.MaxValue);
            assets.Where(a => a.MapScaleHead != 0).ToList().Randomize(a => a.MapScaleHead, (a, f) => a.MapScaleHead = f,
                settings.MapScaleHead.Distribution, 0, float.MaxValue);
            assets.Where(a => a.MapScaleWing != 0).ToList().Randomize(a => a.MapScaleWing, (a, f) => a.MapScaleWing = f,
                settings.MapScaleWing.Distribution, 0, float.MaxValue);
            GD.SetDirty(DataSetEnum.Asset);
        }

        private static StringBuilder ApplyTableTitle(StringBuilder innerTable, string title)
        {
            StringBuilder assetShuffleTable = new();
            if (innerTable.Length > 0)
            {
                assetShuffleTable.AppendLine($"---- {title} ----\n");
                assetShuffleTable.AppendLine(innerTable.ToString());
            }

            return assetShuffleTable;
        }

        private void ShuffleHubAnims(List<Asset> assets, StringBuilder hubAnimShuffleInnertable)
        {
            Dictionary<string, string> mapping = new();
            CreateRandomMapping(hubAnimShuffleInnertable, mapping, GD.MaleHubAnims);
            CreateRandomMapping(hubAnimShuffleInnertable, mapping, GD.FemaleHubAnims);
            foreach (Asset a in assets)
                if (mapping.TryGetValue(a.HubAnim, out string? newValue))
                    a.HubAnim = newValue;
            GD.SetDirty(DataSetEnum.Asset);
        }

        private void ShuffleDemoAnims(List<Asset> assets, StringBuilder demoAnimShuffleInnertable, bool includeGeneric)
        {
            Dictionary<string, string> mapping = new();
            List<(string id, string name)> maleDemoAnims = new(GD.UniqueMaleDemoAnims);
            List<(string id, string name)> femaleDemoAnims = new(GD.UniqueFemaleDemoAnims);
            if (includeGeneric)
            {
                maleDemoAnims.AddRange(GD.GenericMaleDemoAnims);
                femaleDemoAnims.AddRange(GD.GenericFemaleDemoAnims);
            }
            CreateRandomMapping(demoAnimShuffleInnertable, mapping, maleDemoAnims);
            CreateRandomMapping(demoAnimShuffleInnertable, mapping, femaleDemoAnims);
            foreach (Asset a in assets)
                if (mapping.TryGetValue(a.DemoAnim, out string? newValue))
                    a.DemoAnim = newValue;
            GD.SetDirty(DataSetEnum.Asset);
        }

        private void ShuffleTalkAnims(List<Asset> assets, StringBuilder talkAnimShuffleInnertable)
        {
            Dictionary<string, string> mapping = new();
            CreateRandomMapping(talkAnimShuffleInnertable, mapping, GD.MaleTalkAnims);
            CreateRandomMapping(talkAnimShuffleInnertable, mapping, GD.FemaleTalkAnims);
            foreach (Asset a in assets)
                if (mapping.TryGetValue(a.TalkAnim, out string? newTalkAnim))
                    a.TalkAnim = newTalkAnim;
            GD.SetDirty(DataSetEnum.Asset);
        }

        private void ShuffleInfoAnims(List<Asset> assets, StringBuilder infoAnimShuffleInnertable, bool includeGeneric)
        {
            Dictionary<string, string> mapping = new();
            List<(string id, string name)> maleInfoAnims = new(GD.UniqueMaleInfoAnims);
            List<(string id, string name)> femaleInfoAnims = new(GD.UniqueFemaleInfoAnims);
            if (includeGeneric)
            {
                maleInfoAnims.AddRange(GD.GenericMaleInfoAnims);
                femaleInfoAnims.AddRange(GD.GenericFemaleInfoAnims);
            }
            CreateRandomMapping(infoAnimShuffleInnertable, mapping, maleInfoAnims);
            CreateRandomMapping(infoAnimShuffleInnertable, mapping, femaleInfoAnims);
            foreach (Asset a in assets)
                if (mapping.TryGetValue(a.InfoAnim, out string? newInfoAnim))
                    a.InfoAnim = newInfoAnim;
            GD.SetDirty(DataSetEnum.Asset);
        }

        private void ShuffleMountModels(List<Asset> assets, StringBuilder mountModelShuffleInnertable)
        {
            Dictionary<string, string> mapping = new();
            CreateRandomMapping(mountModelShuffleInnertable, mapping, GD.HorseRideDressModels);
            CreateRandomMapping(mountModelShuffleInnertable, mapping, GD.PegasusRideDressModels);
            CreateRandomMapping(mountModelShuffleInnertable, mapping, GD.WolfRideDressModels);
            CreateRandomMapping(mountModelShuffleInnertable, mapping, GD.WyvernRideDressModels);
            foreach (Asset a in assets)
                if (mapping.TryGetValue(a.RideDressModel, out string? newRideDressModel))
                    a.RideDressModel = newRideDressModel;
            GD.SetDirty(DataSetEnum.Asset);
        }

        private static void CreateRandomMapping(StringBuilder innertable, Dictionary<string, string> mapping,
            List<(string id, string name)> entities)
        {
            Redistribution r = new(100);
            List<string> distinct = entities.GetIDs();
            List<string> shuffle = new(distinct);
            r.Randomize(shuffle);
            for (int i = 0; i < distinct.Count; i++)
            {
                mapping.Add(distinct[i], shuffle[i]);
                innertable.AppendLine($"{entities.IDToName(distinct[i])} → " +
                    $"{entities.IDToName(shuffle[i])}");
            }
        }

        private static void CreateRandomMapping(Dictionary<string, string> mapping, List<(string id, string name)> entities)
        {
            Redistribution r = new(100);
            List<string> distinct = entities.GetIDs();
            List<string> shuffle = new(distinct);
            r.Randomize(shuffle);
            for (int i = 0; i < distinct.Count; i++)
                mapping.Add(distinct[i], shuffle[i]);
        }

        private void RandomizeColors(RandomizerSettings.AssetTableSettings settings, Asset a)
        {
            List<Color> colors = settings.ColorPalette.GetArg<bool>(0) ? RandomPalette(4) : RandomColors(4);
            new Redistribution(100).Randomize(colors);
            if (settings.ColorPalette.Enabled && a.HasMaskColor())
            {
                a.MaskColor100R = colors[0].R;
                a.MaskColor100G = colors[0].G;
                a.MaskColor100B = colors[0].B;
                a.MaskColor075R = colors[1].R;
                a.MaskColor075G = colors[1].G;
                a.MaskColor075B = colors[1].B;
                a.MaskColor050R = colors[2].R;
                a.MaskColor050G = colors[2].G;
                a.MaskColor050B = colors[2].B;
                a.MaskColor025R = colors[3].R;
                a.MaskColor025G = colors[3].G;
                a.MaskColor025B = colors[3].B;
                GD.SetDirty(DataSetEnum.Asset);
            }
        }

        private void GetOutfitSwapLists(RandomizerFieldSettings settings, out List<List<string>> maleOutfitSwapLists,
            out List<List<string>> femaleOutfitSwapLists)
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
                mapping.Add(ToOBody(list[i]), ToOBody(shuffle[i]));
                outfitShuffleInnertable.AppendLine($"{GD.AllDressModels.IDToName(list[i])} → {GD.AllDressModels.IDToName(shuffle[i])}");
            }
            HashSet<string> bodyModels = assets.Select(a => a.BodyModel).Distinct().Order().ToHashSet();
            foreach (Asset a in assets)
            {
                if (mapping.TryGetValue(a.DressModel, out string? newDressModel))
                    a.DressModel = newDressModel;
                mapping.TryGetValue(a.BodyModel, out string? newBodyModel);
                if (newBodyModel != null && bodyModels.Contains(newBodyModel))
                    a.BodyModel = newBodyModel;
            }
            GD.SetDirty(DataSetEnum.Asset);
        }

        private static string ToOBody(string s) => s.Replace("uBody", "oBody");

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
                            remove = assets.First(a => a.Conditions[0] == "PID_タイトル用_リュール女");
                            break;
                        case GameData.Gender.Female:
                        case GameData.Gender.Rosado:
                            remove = assets.First(a => a.Conditions[0] == "PID_タイトル用_リュール男");
                            break;
                    }
                    if (remove != null)
                    {
                        remove.BodyModel = "uRig_HumnM1Invisible";
                        remove.DressModel = "uBody_null";
                        remove.HeadModel = "uHead_null";
                        remove.HairModel = "uHair_null";
                        remove.Acc1Locator = "";
                        remove.Acc1Model = "";
                        remove.Acc2Locator = "";
                        remove.Acc2Model = "";
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
                CharacterNameMapping[target.name] = source.name;
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
            List<GodGeneral> allySyncableEmblems = ggs.FilterData(gg => gg.Gid, GD.AllySyncableEmblems);
            List<GodGeneral> enemySyncableEmblems = ggs.FilterData(gg => gg.Gid, GD.EnemySyncableEmblems);
            List<GodGeneral> linkableEmblems = ggs.FilterData(gg => gg.Gid, GD.LinkableEmblems);
            List<GodGeneral> baseArenaEmblems = ggs.FilterData(gg => gg.Gid, GD.BaseArenaEmblems);
            List<GodGeneral> arenaEmblems = ggs.FilterData(gg => gg.Gid, GD.ArenaEmblems);
            List<GodGeneral> allyArenaSyncableEmblems = ggs.FilterData(gg => gg.Gid, GD.AllyArenaSyncableEmblems);
            List<GodGeneral> engageableEmblems = ggs.FilterData(gg => gg.Gid, GD.EngageableEmblems);
            List<string> playableCharacterIDs = GD.PlayableCharacters.GetIDs();
            List<string> compatibleAsEngageAttackIDs = GD.CompatibleAsEngageAttacks.GetIDs();
            List<string> linkableEmblemIDs = GD.LinkableEmblems.GetIDs();
            Dictionary<GodGeneral, StringBuilder> entries = CreateStringBuilderDictionary(ggs);

            // Pair bond links
            allyEngageableEmblems.ForEach(gg => { if (gg.LinkGid != "") ggs.First(gg0 => gg0.Gid == gg.LinkGid).LinkGid = gg.Gid; });

            if (settings.Link.Enabled)
            {
                // Get Alear emblems
                List<GodGeneral> alearEmblems = ggs.Where(gg => GD.AlearEmblems.Select(t => t.id).Contains(gg.Gid)).ToList();
                // Randomize
                alearEmblems.Randomize(gg => gg.Link, (gg, s) => gg.Link = s, settings.Link.Distribution, playableCharacterIDs);
                // Remove duplicate characters
                RemoveDuplicateLinks(alearEmblems);
                WriteToChangelog(entries, alearEmblems, gg => gg.Link, "Engage+ Link", MapNames(GD.PlayableCharacters));
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
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
                // Remove duplicate characters
                RemoveDuplicateLinks(allyEngageableEmblems);
                WriteToChangelog(entries, linkableEmblems, gg => gg.Link, "Engage+ Link", MapNames(GD.PlayableCharacters));
                GD.SetDirty(DataSetEnum.GodGeneral);
            }


            if (settings.EngageCount.Enabled)
            {
                allySyncableEmblems.Randomize(gg => gg.EngageCount, (gg, b) => gg.EngageCount = b, settings.EngageCount.
                    Distribution, 1, byte.MaxValue);
                WriteToChangelog(entries, allySyncableEmblems, gg => gg.EngageCount, "Engage Meter Size");
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
                Propagate(allyEngageableEmblems, allySyncableEmblems, gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s);
                Propagate(allyEngageableEmblems, allySyncableEmblems, gg => gg.EngageAttackLink, (gg, s) => gg.EngageAttackLink = s);
                // Correct AIEngageAttackType field if possible
                allyEngageableEmblems.ForEach(gg => gg.AIEngageAttackType = GD.EngageAttackToAIEngageAttackType.TryGetValue(gg.
                    EngageAttack, out sbyte value) ? value : (sbyte)0);
                WriteToChangelog(entries, allySyncableEmblems, gg => gg.EngageAttack, "Engage Attack", GD.CompatibleAsEngageAttacks);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.EngageAttackEnemy.Enabled)
            {
                // Randomize
                enemyEngageableEmblems.Randomize(gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s, settings.
                    EngageAttackEnemy.Distribution, compatibleAsEngageAttackIDs);
                // Propagate to assosiated emblems
                Propagate(enemyEngageableEmblems, enemySyncableEmblems, gg => gg.EngageAttack, (gg, s) => gg.EngageAttack = s);
                WriteToChangelog(entries, enemySyncableEmblems, gg => gg.EngageAttack, "Engage Attack", GD.CompatibleAsEngageAttacks);
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.EngageAttackLink.Enabled)
            {
                // Randomize
                allyEngageableEmblems.Randomize(gg => gg.EngageAttackLink, (gg, s) => gg.EngageAttackLink = s, settings.
                    EngageAttackLink.Distribution, compatibleAsEngageAttackIDs);
                // Propagate to assosiated emblems
                Propagate(allyEngageableEmblems, allySyncableEmblems, gg => gg.EngageAttackLink, (gg, s) => gg.
                    EngageAttackLink = s);
                WriteToChangelog(entries, allySyncableEmblems, gg => gg.EngageAttackLink, "Bond Link Skill", GD.CompatibleAsEngageAttacks);
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
                Propagate(allyEngageableEmblems, allySyncableEmblems, gg => gg.LinkGid, (gg, s) => gg.LinkGid = s);
                WriteToChangelog(entries, allySyncableEmblems, gg => gg.LinkGid, "Bond Link Emblem", MapNames(GD.LinkableEmblems));
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.ShuffleGrowTableAlly)
            {
                // Shuffle ally emblems
                allyEngageableEmblems.Randomize(gg => gg.GrowTable, (gg, s) => gg.GrowTable = s, new Redistribution(100), null!);
                // Shuffle arena emblems
                baseArenaEmblems.Randomize(gg => gg.GrowTable, (gg, s) => gg.GrowTable = s, new Redistribution(100), null!);
                // Propagate to assosiated emblems
                Propagate(allyEngageableEmblems, allySyncableEmblems, gg => gg.GrowTable, (gg, s) => gg.GrowTable = s);
                Propagate(baseArenaEmblems, arenaEmblems, gg => gg.GrowTable, (gg, s) => gg.GrowTable = s);
                WriteToChangelog(entries, allySyncableEmblems, gg => gg.GrowTable, "Bond Level Table", MapNames(GD.AllyBondLevelTables));
                WriteToChangelog(entries, arenaEmblems, gg => gg.GrowTable, "Bond Level Table", MapNames(GD.AllyBondLevelTables));
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.ShuffleGrowTableEnemy)
            {
                // Shuffle
                enemyEngageableEmblems.Randomize(gg => gg.GrowTable, (gg, s) => gg.GrowTable = s, new Redistribution(100), null!);
                // Propagate to assosiated emblems
                Propagate(enemyEngageableEmblems, enemySyncableEmblems, gg => gg.GrowTable, (gg, s) => gg.GrowTable = s);
                WriteToChangelog(entries, enemySyncableEmblems, gg => gg.GrowTable, "Bond Level Table", MapNames(GD.EnemyBondLevelTables));
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.RandomizeEngravingStats)
            {
                Dictionary<GodGeneral, StringBuilder> engravingEntries = new();
                ggs.ForEach(gg => engravingEntries.Add(gg, new()));
                void HandleEngraving(RandomizerFieldSettings rfs, Func<GodGeneral, sbyte> get, Action<GodGeneral, sbyte> set, string name)
                {
                    allyEngageableEmblems.Randomize(get, set, rfs.Distribution, sbyte.MinValue, sbyte.MaxValue);
                    Propagate(allyEngageableEmblems, allySyncableEmblems, get, set);
                    allySyncableEmblems.ForEach(o => engravingEntries[o].Append(get(o) + name + ",\t"));
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

            if (settings.RandomizeAllyStaticSyncStats)
            {
                Dictionary<GodGeneral, StringBuilder> statEntries = new();
                ggs.ForEach(gg => statEntries.Add(gg, new()));
                void HandleStat(RandomizerFieldSettings rfs, Func<GodGeneral, sbyte> get, Action<GodGeneral, sbyte> set, string name)
                {
                    allyArenaSyncableEmblems.Randomize(get, set, rfs.Distribution, sbyte.MinValue, sbyte.MaxValue);
                    allyArenaSyncableEmblems.ForEach(o => statEntries[o].Append(get(o) + name + ",\t"));
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
                        entries[gg].AppendLine("Static Sync Bonus:\t" + statEntries[gg].ToString()[..^2]);
                });
                GD.SetDirty(DataSetEnum.GodGeneral);
            }
            if (settings.RandomizeEnemyStaticSyncStats)
            {
                Dictionary<GodGeneral, StringBuilder> statEntries = new();
                ggs.ForEach(gg => statEntries.Add(gg, new()));
                void HandleStat(RandomizerFieldSettings rfs, Func<GodGeneral, sbyte> get, Action<GodGeneral, sbyte> set, string name)
                {
                    enemySyncableEmblems.Randomize(get, set, rfs.Distribution, sbyte.MinValue, sbyte.MaxValue);
                    enemySyncableEmblems.ForEach(o => statEntries[o].Append(get(o) + name + ",\t"));
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
                        entries[gg].AppendLine("Static Sync Bonus:\t" + statEntries[gg].ToString()[..^2]);
                });
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            if (settings.WeaponRestriction.Enabled)
            {
                engageableEmblems.ForEach(gg => gg.SetWeaponRestricted(settings.WeaponRestriction.GetArg<double>(0).Occur()));
                WriteToChangelog(entries, engageableEmblems, gg => gg.GetWeaponRestricted(), "Weapon Restricted");
                GD.SetDirty(DataSetEnum.GodGeneral);
            }

            StringBuilder innerTable = new();
            List<(string id, string name)> mappedNames = MapNames(GD.Emblems);
            foreach (GodGeneral gg in ggs)
                if (entries[gg].Length > 0)
                {
                    innerTable.AppendLine($"\t{mappedNames.IDToName(gg.Gid)}:");
                    innerTable.AppendLine(entries[gg].ToString());
                }
            return ApplyTableTitle(innerTable, "Emblems");
        }

        private static void RemoveDuplicateLinks(List<GodGeneral> allyEngageableEmblems)
        {
            HashSet<string> linkedCharacters = new();
            List<GodGeneral> shuffled = allyEngageableEmblems;
            new Redistribution(100).Randomize(shuffled);
            foreach (GodGeneral gg in shuffled)
                if (gg.Link != "")
                    if (linkedCharacters.Contains(gg.Link))
                        gg.Link = "";
                    else
                        linkedCharacters.Add(gg.Link);
        }

        private List<(string id, string name)> MapNames(IEnumerable<(string id, string name)> characters) =>
            characters.Select(t => (t.id, CharacterNameMapping[t.name])).ToList();

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
            List<string> syncStatSkillIDs = GD.SyncStatSkills.GetIDs();
            List<string> syncMovSkillIDs = GD.SyncMovSkills.GetIDs();
            List<string> engageWeaponIDs = GD.EngageWeapons.GetIDs();
            List<int> proficiencyIDs = GD.Proficiencies.GetIDs();

            foreach (Skill s in generalSkills)
                if (s.InheritanceCost == 0 && GD.DefaultSPCost.TryGetValue(GD.GeneralSkills.First(t => t.id == s.Sid).name,
                    out ushort value))
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
                        gt => gt.SynchroSkills.Where(syncStatSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.SynchroSkills.Where(s => !syncStatSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.SynchroSkills = skills.ToArray();
                        }, syncStatSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.SynchroStatSkillsAlly.Distribution, allyBondLevelTables, syncStatSkillIDs,
                    gt => gt.SynchroSkills, syncStatSkillIDs.Contains);
                // Limit to 3 stats if toggled
                if (settings.SynchroStatSkillsAlly.GetArg<bool>(0))
                    LimitSyncStatsTo3(allyBondLevelTables, syncStatSkillIDs, syncMovSkillIDs);
                // Sort same stats in increasing order
                SortSyncStats(allyBondLevelTables);
                WriteGrowthTableArraysToChangelog(allyBondLevelTables, gt => gt.SynchroSkills.Where(syncStatSkillIDs.Contains),
                    levelEntries, GD.SyncStatSkills, "While Synced");
                GD.SetDirty(DataSetEnum.GrowthTable);
            }
            if (settings.SynchroStatSkillsEnemy.Enabled)
            {
                // Randomize array sizes if toggled
                if (settings.SynchroStatSkillsEnemyCount.Enabled)
                    RandomizeGrowthTableArraySizes(settings.SynchroStatSkillsEnemyCount.Distribution, enemyBondLevelTables,
                        gt => gt.SynchroSkills.Where(syncStatSkillIDs.Contains).ToArray(), (gt, sa) =>
                        {
                            List<string> skills = gt.SynchroSkills.Where(s => !syncStatSkillIDs.Contains(s)).ToList();
                            skills.AddRange(sa);
                            gt.SynchroSkills = skills.ToArray();
                        }, syncStatSkillIDs);
                // Randomize skills
                RandomizeGrowthTableArrays(settings.SynchroStatSkillsEnemy.Distribution, enemyBondLevelTables, syncStatSkillIDs,
                    gt => gt.SynchroSkills, syncStatSkillIDs.Contains);
                // Limit to 3 stats if toggled
                if (settings.SynchroStatSkillsAlly.GetArg<bool>(0))
                    LimitSyncStatsTo3(enemyBondLevelTables, syncStatSkillIDs, syncMovSkillIDs);
                // Sort same stats in increasing order
                SortSyncStats(enemyBondLevelTables);
                WriteGrowthTableArraysToChangelog(enemyBondLevelTables, gt => gt.SynchroSkills.Where(syncStatSkillIDs.Contains),
                    levelEntries, GD.SyncStatSkills, "While Synced");
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
                    levelEntries, GD.GeneralSkills, "While Synced");
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
                    levelEntries, GD.GeneralSkills, "While Synced");
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
                    RandomizeGrowthTableAptitudeCounts(settings.AptitudeCount.Distribution, inheritableBondLevelTables, proficiencyIDs);
                // Randomize proficiencies
                RandomizeGrowthTableAptitude(settings.Aptitude.Distribution, inheritableBondLevelTables, proficiencyIDs);
                WriteGrowthTableAptitudeToChangelog(inheritableBondLevelTables, levelEntries);
                GD.SetDirty(DataSetEnum.GrowthTable);
            }

            void HandleFlag(RandomizerFieldSettings settings, int index, string fieldName)
            {
                if (!settings.Enabled) return;
                List<Node<byte>> unlockLevels = inheritableBondLevelTables.Select(pg => new Node<byte>(pg.Group.Cast<GrowthTable>().
                    First(gt => gt.GetFlag(index)).Level)).ToList();
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
            List<(string id, string name)> mappedNames = MapNames(GD.BondLevelTables);
            foreach (ParamGroup pg in pgs)
                if (entries[pg].Length > 0)
                {
                    innertable.AppendLine($"\t{mappedNames.IDToName(pg.Name)}:");
                    innertable.AppendLine(entries[pg].ToString());
                }

            return ApplyTableTitle(innertable, "Bond Level Tables");
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

            return ApplyTableTitle(innertable, "Bond Levels");
        }

        private StringBuilder RandomizeTypeOfSoldier(RandomizerSettings.TypeOfSoldierSettings settings)
        {
            List<TypeOfSoldier> toss = GD.Get(DataSetEnum.TypeOfSoldier).Params.Cast<TypeOfSoldier>().ToList();
            List<TypeOfSoldier> allClasses = toss.FilterData(tos => tos.Jid, GD.AllClasses);
            List<TypeOfSoldier> generalClasses = toss.FilterData(tos => tos.Jid, GD.GeneralClasses);
            Dictionary<TypeOfSoldier, StringBuilder> entries = CreateStringBuilderDictionary(toss);

            if (settings.StyleName.Enabled)
            {
                allClasses.Randomize(tos => tos.StyleName, (tos, s) => tos.StyleName = s, settings.StyleName.Distribution, GD.UnitTypes.GetIDs());
                WriteToChangelog(entries, allClasses, tos => tos.StyleName, "Unit Type", GD.UnitTypes);
                GD.SetDirty(DataSetEnum.TypeOfSoldier);
            }

            if (settings.MoveType.Enabled)
            {
                if (settings.MoveType.GetArg<bool>(0))
                    foreach (TypeOfSoldier tos in generalClasses)
                        tos.MoveType = tos.StyleName switch
                        {
                            "騎馬スタイル" => 2,
                            "飛行スタイル" => 3,
                            _ => 1
                        };
                else
                    generalClasses.Randomize(tos => tos.MoveType, (tos, i) => tos.MoveType = (sbyte)i, settings.MoveType.Distribution,
                        GD.MovementTypes.GetIDs());
                WriteToChangelog(entries, generalClasses, tos => tos.MoveType, "Movement Type", GD.MovementTypes);
                GD.SetDirty(DataSetEnum.TypeOfSoldier);
            }

            if (settings.Weapon.Enabled)
            {
                if (settings.RandomizeWeaponTypeCount)
                    RandomizeWeaponTypeCounts(settings, generalClasses);
                RandomizeWeaponTypes(settings, generalClasses);
                foreach (TypeOfSoldier tos in generalClasses)
                    AdjustRanksToWeaponTypes(tos);
            }
            if (settings.RandomizeWeaponRank.Enabled)
                RandomizeWeaponRanks(settings, generalClasses);
            if (settings.Weapon.Enabled || settings.RandomizeWeaponRank.Enabled)
            {
                WriteWeaponRanksToChangelog(generalClasses, entries);
                GD.SetDirty(DataSetEnum.TypeOfSoldier);
            }

            if (settings.RandomizeBaseStats.Enabled)
            {
                RandomizeStatSpread(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()).ToList(), entries, settings.GetBaseBaseStatSettings(),
                    tos => tos.GetBases(), (tos, a) => tos.SetBases(a), tos => tos.GetBasicBases(), (tos, a) => tos.SetBasicBases(a),
                    "Base Stats", DataSetEnum.TypeOfSoldier);
                RandomizeStatSpread(allClasses.Where(tos => tos.IsAdvancedOrSpecial()).ToList(), entries, settings.GetAdvancedBaseStatSettings(),
                    tos => tos.GetBases(), (tos, a) => tos.SetBases(a), tos => tos.GetBasicBases(), (tos, a) => tos.SetBasicBases(a),
                    "Base Stats", DataSetEnum.TypeOfSoldier);
            }
            if (settings.RandomizeStatLimits.Enabled)
            {
                RandomizeStatSpread(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()).ToList(), entries, settings.GetBaseStatLimitSettings(),
                    tos => tos.GetLimits(), (tos, a) => tos.SetLimits(a), tos => tos.GetBasicLimits(), (tos, a) => tos.SetBasicLimits(a),
                    "Stat Limits", DataSetEnum.TypeOfSoldier);
                RandomizeStatSpread(allClasses.Where(tos => tos.IsAdvancedOrSpecial()).ToList(), entries, settings.GetAdvancedStatLimitSettings(),
                    tos => tos.GetLimits(), (tos, a) => tos.SetLimits(a), tos => tos.GetBasicLimits(), (tos, a) => tos.SetBasicLimits(a),
                    "Stat Limits", DataSetEnum.TypeOfSoldier);
            }
            if (settings.RandomizeEnemyStatGrows.Enabled)
            {
                RandomizeStatSpread(allClasses.Where(tos => !tos.IsAdvancedOrSpecial()).ToList(), entries, settings.GetBaseEnemyGrowthSettings(),
                    tos => tos.GetEnemyGrowths(), (tos, a) => tos.SetEnemyGrowths(a), tos => tos.GetBasicEnemyGrowths(),
                    (tos, a) => tos.SetBasicEnemyGrowths(a), "Enemy Growths", DataSetEnum.TypeOfSoldier);
                RandomizeStatSpread(allClasses.Where(tos => tos.IsAdvancedOrSpecial()).ToList(), entries, settings.GetBaseEnemyGrowthSettings(),
                    tos => tos.GetEnemyGrowths(), (tos, a) => tos.SetEnemyGrowths(a), tos => tos.GetBasicEnemyGrowths(),
                    (tos, a) => tos.SetBasicEnemyGrowths(a), "Enemy Growths", DataSetEnum.TypeOfSoldier);
            }
            if (settings.RandomizeStatGrowthModifiers.Enabled)
            {
                RandomizeStatSpread(allClasses.Where(tos => !tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()).ToList(), entries,
                    settings.GetBaseGrowthModifierSettings(), tos => tos.GetGrowthModifiers(), (tos, a) => tos.SetGrowthModifiers(a),
                    tos => tos.GetBasicGrowthModifiers(), (tos, a) => tos.SetBasicGrowthModifiers(a), "Growth Modifiers",
                    DataSetEnum.TypeOfSoldier);
                RandomizeStatSpread(allClasses.Where(tos => tos.IsAdvancedOrSpecial() && tos.HasGrowthModifiers()).ToList(), entries,
                    settings.GetAdvancedGrowthModifierSettings(), tos => tos.GetGrowthModifiers(), (tos, a) => tos.SetGrowthModifiers(a),
                    tos => tos.GetBasicGrowthModifiers(), (tos, a) => tos.SetBasicGrowthModifiers(a), "Growth Modifiers",
                    DataSetEnum.TypeOfSoldier);
            }

            if (settings.LearningSkill.Enabled)
            {
                List<TypeOfSoldier> learningSkillTOSs = allClasses.Where(tos => tos.LearningSkill != "").ToList();
                learningSkillTOSs.Randomize(tos => tos.LearningSkill, (tos, s) => tos.LearningSkill = s,
                    settings.LearningSkill.Distribution, GD.GeneralSkills.GetIDs());
                WriteToChangelog(entries, learningSkillTOSs, tos => tos.LearningSkill, "Class Skill", GD.GeneralSkills);
                GD.SetDirty(DataSetEnum.TypeOfSoldier);
            }

            if (settings.LunaticSkill.Enabled)
            {
                List<TypeOfSoldier> lunaticSkillTOSs = allClasses.Where(tos => tos.LunaticSkill != "").ToList();
                lunaticSkillTOSs.Randomize(tos => tos.LunaticSkill, (tos, s) => tos.LunaticSkill = s,
                    settings.LunaticSkill.Distribution, GD.GeneralSkills.GetIDs());
                WriteToChangelog(entries, lunaticSkillTOSs, tos => tos.LunaticSkill, "Maddening Skill", GD.GeneralSkills);
                GD.SetDirty(DataSetEnum.TypeOfSoldier);
            }

            if (settings.Attrs.Enabled)
            {
                List<int> flagIDs = GD.Attributes.GetIDs();
                if (settings.AttrsCount.Enabled)
                    RandomizeFlagCounts(settings.AttrsCount.Distribution, allClasses, flagIDs, tos => tos.GetAttributes(), (tos, l) => tos.SetAttributes(l));
                RandomizeFlags(settings.Attrs.Distribution, allClasses, flagIDs, tos => tos.GetAttributes(), (tos, l) => tos.SetAttributes(l));
                if (settings.Attrs.GetArg<bool>(0))
                    MatchAttributesToUnitType(allClasses);
                WriteFlagsToChangelog(entries, allClasses, tos => tos.GetAttributes(), GD.Attributes, "Attributes", true);
                GD.SetDirty(DataSetEnum.TypeOfSoldier);
            }

            StringBuilder innerTable = new();
            foreach (TypeOfSoldier tos in toss)
                if (entries[tos].Length > 0)
                {
                    innerTable.AppendLine($"\t{GD.AllClasses.IDToName(tos.Jid)}:");
                    innerTable.AppendLine(entries[tos].ToString());
                }

            return ApplyTableTitle(innerTable, "Classes");
        }

        private static void MatchAttributesToUnitType(List<TypeOfSoldier> allClasses)
        {
            foreach (TypeOfSoldier tos in allClasses)
                switch (tos.StyleName)
                {
                    case "スタイル無し":
                    case "連携スタイル":
                    case "隠密スタイル":
                    case "魔法スタイル":
                    case "気功スタイル":
                        tos.SetAttribute(0, true);
                        tos.SetAttribute(1, false);
                        tos.SetAttribute(2, false);
                        tos.SetAttribute(3, false);
                        tos.SetAttribute(4, false);
                        tos.SetAttribute(5, false);
                        break;
                    case "騎馬スタイル":
                        tos.SetAttribute(0, false);
                        tos.SetAttribute(1, true);
                        tos.SetAttribute(3, false);
                        break;
                    case "重装スタイル":
                        tos.SetAttribute(0, false);
                        tos.SetAttribute(2, true);
                        break;
                    case "飛行スタイル":
                        tos.SetAttribute(0, false);
                        tos.SetAttribute(1, false);
                        tos.SetAttribute(3, true);
                        break;
                    case "竜族スタイル":
                        if (!tos.GetAttribute(4) && !tos.GetAttribute(4))
                            if (0.5.Occur())
                                tos.SetAttribute(4, true);
                            else
                                tos.SetAttribute(5, true);
                        break;
                }
        }

        private void WriteWeaponRanksToChangelog(List<TypeOfSoldier> generalClasses, Dictionary<TypeOfSoldier, StringBuilder> entries)
        {
            foreach (TypeOfSoldier tos in generalClasses)
            {
                IEnumerable<(string, int)> ranks = tos.GetMaxWeaponLevels().Select((s, i) => (s, i)).Where(t => t.s != "N");
                if (ranks.Any())
                    entries[tos].AppendLine("Weapons:\t" + string.Join(",\t", ranks.Select(t =>
                        $"{t.Item1} {GD.Proficiencies.IDToName(t.Item2)}")));
                if (!ranks.Any())
                    entries[tos].AppendLine("Weapons:\tNone");
            }
        }

        private static void RandomizeWeaponRanks(RandomizerSettings.TypeOfSoldierSettings settings, List<TypeOfSoldier> generalClasses)
        {
            AsNodeStructure(generalClasses, tos => GetRankPool(tos).Select(s => (int)s.ToProficiencyLevel()), out List<List<Node<int>>> rankStructure, out _);
            IEnumerable<(List<Node<int>> ranks, TypeOfSoldier tos)> tosRankStructure = rankStructure.Zip(generalClasses);
            IEnumerable<(List<Node<int>> ranks, TypeOfSoldier tos)> baseStructure = tosRankStructure.Where(t => !t.tos.IsAdvancedOrSpecial());
            IEnumerable<(List<Node<int>> ranks, TypeOfSoldier tos)> advancedStructure = tosRankStructure.Where(t => t.tos.IsAdvancedOrSpecial());
            baseStructure.SelectMany(t => t.ranks).ToList().Randomize(n => n.value, (n, i) => n.value = i, settings.MaxWeaponLevelBase.Distribution, 1, 10);
            advancedStructure.SelectMany(t => t.ranks).ToList().Randomize(n => n.value, (n, i) => n.value = i, settings.MaxWeaponLevelAdvanced.Distribution, 1, 10);
            IEnumerable<string> proficiencyLevels = Enum.GetNames(typeof(GameData.ProficiencyLevel)).Select(s => s.Replace('p', '+'));
            foreach ((List<Node<int>> newRanks, TypeOfSoldier tos) in tosRankStructure)
            {
                if (settings.RandomizeWeaponRank.GetArg<bool>(0) && tos.IsAdvancedOrSpecial())
                    if (newRanks.Count == 1)
                        newRanks[0].value = Math.Min(newRanks[0].value + 1, 10);
                    else
                        for (int i = 0; i < newRanks.Count * 2; i++)
                        {
                            Node<int> n = newRanks.GetRandom();
                            n.value = Math.Max(n.value - 1, 1);
                        }
                int index = 0;
                string[] ranks = tos.GetMaxWeaponLevels();
                while (newRanks.Any())
                {
                    if (ranks[index] != "N")
                    {
                        ranks[index] = proficiencyLevels.ElementAt(newRanks.First().value);
                        newRanks.RemoveAt(0);
                    }
                    index++;
                }
                tos.SetMaxWeaponLevels(ranks);
            }
        }

        private void RandomizeWeaponTypes(RandomizerSettings.TypeOfSoldierSettings settings, List<TypeOfSoldier> generalClasses)
        {
            AsNodeStructure(generalClasses, tos => tos.GetBasicWeaponRequirements(), out List<List<Node<int>>> structure, out List<Node<int>> flattened);
            flattened.Randomize(n => n.value, (n, i) => n.value = i, settings.Weapon.Distribution, GD.BasicProficiencies.GetIDs());
            for (int tosIdx = 0; tosIdx < generalClasses.Count; tosIdx++)
            {
                TypeOfSoldier tos = generalClasses[tosIdx];
                List<Node<int>> list = structure[tosIdx];
                List<int> reqs = list.Select(n => n.value).Distinct().ToList();
                sbyte[] reqValues = tos.GetBasicWeaponRequirementValues();
                while (reqs.Count < list.Count)
                    reqs.Add(GD.BasicProficiencies.GetIDs().Except(reqs).GetRandom());
                IEnumerable<sbyte> valuePool = reqValues.Distinct().Where(i => i != 0);
                if (!valuePool.Any())
                    valuePool = new sbyte[] { 1 };
                reqValues = new sbyte[reqValues.Length];
                foreach (int i in reqs)
                    reqValues[i - 1] = valuePool.GetRandom();
                tos.SetBasicWeaponRequirementValues(reqValues);
            }
            if (settings.Weapon.GetArg<bool>(0))
                foreach (TypeOfSoldier tos in generalClasses)
                {
                    TypeOfSoldier? lowTOS = generalClasses.FirstOrDefault(tos0 => tos0.Name == tos.LowJob);
                    if (lowTOS == null) continue;

                    IEnumerable<sbyte> valuePool = tos.GetBasicWeaponRequirementValues().Distinct().Where(i => i != 0);
                    if (!valuePool.Any())
                        valuePool = new sbyte[] { 1 };
                    while (GetWeaponRequirements(lowTOS).Except(GetWeaponRequirements(tos)).Any())
                    {
                        sbyte[] reqValues = tos.GetBasicWeaponRequirementValues();
                        List<int> reqs = GetWeaponRequirements(tos);
                        int index = GetWeaponRequirements(lowTOS).Except(reqs).GetRandom();
                        reqValues[reqs.GetRandom()] = 0;
                        reqValues[index] = valuePool.GetRandom();
                        tos.SetBasicWeaponRequirementValues(reqValues);
                    }
                }
        }

        private static void RandomizeWeaponTypeCounts(RandomizerSettings.TypeOfSoldierSettings settings, List<TypeOfSoldier> generalClasses)
        {
            List<Node<TypeOfSoldier, int>> weaponTypeCounts = generalClasses.Select(tos =>
                                    new Node<TypeOfSoldier, int>(tos, tos.GetBasicWeaponRequirementCount())).ToList();
            weaponTypeCounts.Where(n => !n.a.IsAdvancedOrSpecial()).ToList().Randomize(n => n.b, (n, i) => n.b = i,
                settings.WeaponBaseCount.Distribution, 0, 8);
            weaponTypeCounts.Where(n => n.a.IsAdvancedOrSpecial()).ToList().Randomize(n => n.b, (n, i) => n.b = i,
                settings.WeaponAdvancedCount.Distribution, 0, 8);
            foreach (Node<TypeOfSoldier, int> n in weaponTypeCounts)
                if (n.b == 0 && n.a.WeaponNone == 0 && n.a.WeaponSpecial == 0)
                    n.b = 1;
            if (settings.Weapon.GetArg<bool>(0))
                foreach (Node<TypeOfSoldier, int> n in weaponTypeCounts)
                {
                    Node<TypeOfSoldier, int>? lowNode = weaponTypeCounts.FirstOrDefault(n0 => n0.a.Name == n.a.LowJob);
                    if (lowNode == null) continue;
                    if (n.b < lowNode.b)
                        n.b = lowNode.b;
                }
            foreach (Node<TypeOfSoldier, int> n in weaponTypeCounts)
            {
                sbyte[] reqValues = n.a.GetBasicWeaponRequirementValues();
                if (reqValues.Any(i8 => i8 == 2))
                {
                    List<int> indices = reqValues.Select((i8, i32) => (i8, i32)).Where(t => t.i8 == 2).Select(t => t.i32).ToList();
                    for (int i = 0; i < reqValues.Length; i++)
                        if (reqValues[i] == 2)
                            reqValues[i] = 0;
                    reqValues[indices.GetRandom()] = 1;
                }
                if (reqValues.Any(i8 => i8 == 3))
                {
                    List<int> indices = reqValues.Select((i8, i32) => (i8, i32)).Where(t => t.i8 == 3).Select(t => t.i32).ToList();
                    new Redistribution(100).Randomize(indices);
                    for (int i = 0; i < reqValues.Length; i++)
                        if (reqValues[i] == 3)
                            reqValues[i] = 0;
                    reqValues[indices[0]] = 1;
                    reqValues[indices[1]] = 1;
                }
                List<int> reqs = n.a.GetBasicWeaponRequirementValues().Select((i8, i32) => (i8, i32)).Where(t => t.i8 == 1).Select(t => t.i32).ToList();
                while (reqs.Count < n.b)
                    reqs.Add(Enumerable.Range(0, reqValues.Length).Except(reqs).GetRandom());
                while (reqs.Count > n.b)
                    reqs.Remove(reqs.GetRandom());
                reqValues = new sbyte[reqValues.Length];
                foreach (int req in reqs)
                    reqValues[req] = 1;
                n.a.SetBasicWeaponRequirementValues(reqValues);
            }
        }

        private static List<int> GetWeaponRequirements(TypeOfSoldier tos) =>
            tos.GetBasicWeaponRequirementValues().Select((i8, i32) => (i8, i32)).Where(t => t.i8 > 0).Select(t => t.i32).ToList();

        private static void AdjustRanksToWeaponTypes(TypeOfSoldier tos)
        {
            sbyte[] reqValues = tos.GetWeaponRequirementValues();
            string[] ranks = tos.GetMaxWeaponLevels();
            List<string> rankPool = GetRankPool(tos);
            if (!rankPool.Any())
                rankPool.Add("S");
            for (int i = 0; i < reqValues.Length; i++)
                if (reqValues[i] > 0 && ranks[i] == "N")
                    ranks[i] = rankPool.GetRandom();
                else if (reqValues[i] == 0)
                    ranks[i] = "N";
            tos.SetMaxWeaponLevels(ranks);
        }

        private static List<string> GetRankPool(TypeOfSoldier tos) => tos.GetMaxWeaponLevels().Where(s => s != "N").ToList();

        private StringBuilder RandomizeIndividual(RandomizerSettings.IndividualSettings settings)
        {
            List<Individual> individuals = GD.Get(DataSetEnum.Individual).Params.Cast<Individual>().ToList();
            List<Individual> playableCharacters = individuals.FilterData(i => i.Pid, GD.PlayableCharacters);
            List<Individual> allyCharacters = individuals.FilterData(i => i.Pid, GD.AllyCharacters);
            List<Individual> enemyCharacters = individuals.FilterData(i => i.Pid, GD.EnemyCharacters);
            List<Individual> npcCharacters = individuals.FilterData(i => i.Pid, GD.NPCCharacters);
            List<TypeOfSoldier> toss = GD.Get(DataSetEnum.TypeOfSoldier).Params.Cast<TypeOfSoldier>().ToList();
            List<Asset> assets = GD.Get(DataSetEnum.Asset).Params.Cast<Asset>().ToList();
            List<string> generalClassIDs = GD.GeneralClasses.GetIDs();
            List<string> weaponIDs = GD.NormalWeapons.GetIDs();
            List<string> itemIDs = GD.BattleItems.GetIDs();
            Dictionary<Individual, StringBuilder> entries = CreateStringBuilderDictionary(individuals);

            UnlockExclusiveClassOutfits(assets);

            HandleFlagSet(settings.Aptitude, settings.AptitudeCount, playableCharacters, GD.Proficiencies, entries,
                i => i.GetAptitudes(), (i, l) => i.SetAptitudes(l), "Primary Proficiencies");
            HandleFlagSet(settings.SubAptitude, settings.SubAptitudeCount, playableCharacters, GD.Proficiencies, entries,
                i => i.GetSubAptitudes(), (i, l) => i.SetSubAptitudes(l), "Secondary Proficiencies");

            if (settings.JidAlly.Enabled)
                RandomizeStartingClasses(settings, playableCharacters, toss, weaponIDs, entries);

            if (settings.JidEnemy.Enabled)
                RandomizeEnemyClasses(settings, npcCharacters, toss, generalClassIDs, weaponIDs, entries);

            if (settings.Age.Enabled)
            {
                // Not all playables have age.
                List<Individual> aged = playableCharacters.Where(i => i.Age != -1).ToList();
                aged.Randomize(i => i.Age, (i, s) => i.Age = s, settings.Age.Distribution, 0, short.MaxValue);
                WriteToChangelog(entries, aged, i => i.Age, "Age");
                GD.SetDirty(DataSetEnum.Individual);
            }

            if (settings.RandomizeBirthday)
                RandomizeBirthdays(playableCharacters, entries);

            //CHANGING THE PROTAGONIST'S LEVEL CRASHES THE GAME
            HandleLevel(settings.LevelAlly, allyCharacters.Where(i => i.Pid != "PID_リュール").ToList(), toss, entries);
            HandleLevel(settings.LevelEnemy, enemyCharacters, toss, entries);

            if (settings.InternalLevel.Enabled)
            {
                playableCharacters.Randomize(i => i.GetInternalLevel(toss), (i, s) => i.InternalLevel = s,
                    settings.InternalLevel.Distribution, sbyte.MinValue, sbyte.MaxValue);
                WriteToChangelog(entries, playableCharacters, i => i.GetInternalLevel(toss), "Internal Level");
                GD.SetDirty(DataSetEnum.Individual);
            }

            if (settings.SupportCategory.Enabled)
            {
                playableCharacters.Randomize(i => i.SupportCategory, (i, s) => i.SupportCategory = s, settings.SupportCategory.Distribution,
                    GD.SupportCategories.GetIDs());
                WriteToChangelog(entries, playableCharacters, i => i.SupportCategory, "Support Category", GD.SupportCategories);
                GD.SetDirty(DataSetEnum.Individual);
            }

            if (settings.SkillPoint.Enabled)
            {
                playableCharacters.Randomize(i => i.SkillPoint, (i, i32) => i.SkillPoint = i32, settings.SkillPoint.Distribution,
                    0, int.MaxValue);
                WriteToChangelog(entries, playableCharacters, i => i.SkillPoint, "Starting SP");
                GD.SetDirty(DataSetEnum.Individual);
            }

            if (settings.RandomizeAllyBases)
                RandomizeAllyBaseStats(allyCharacters, entries, settings.GetOffsetNAllySettings(), "Base Stat Modifiers",
                    settings.StrongerProtagonist, settings.StrongerAllyNPCs);
            if (settings.RandomizeEnemyBasesNormal)
                RandomizeStatSpread(enemyCharacters, entries, settings.GetOffsetNEnemySettings(), i => i.GetOffsetN(),
                    (i, a) => i.SetOffsetN(a), i => i.GetBasicOffsetN(), (i, a) => i.SetBasicOffsetN(a), "Base Stats Normal",
                    DataSetEnum.Individual);
            if (settings.RandomizeEnemyBasesHard)
                RandomizeStatSpread(enemyCharacters, entries, settings.GetOffsetHEnemySettings(), i => i.GetOffsetH(),
                    (i, a) => i.SetOffsetH(a), i => i.GetBasicOffsetH(), (i, a) => i.SetBasicOffsetH(a), "Base Stats Hard",
                    DataSetEnum.Individual);
            if (settings.RandomizeEnemyBasesMaddening)
                RandomizeStatSpread(enemyCharacters, entries, settings.GetOffsetLEnemySettings(), i => i.GetOffsetL(),
                    (i, a) => i.SetOffsetL(a), i => i.GetBasicOffsetL(), (i, a) => i.SetBasicOffsetL(a), "Base Stats Maddening",
                    DataSetEnum.Individual);

            if (settings.RandomizeStatLimits)
                RandomizeStatLimits(settings, playableCharacters, entries);

            if (settings.RandomizeAllyStatGrowths)
                RandomizeStatSpread(playableCharacters.Where(i => i.HasGrowths()).ToList(), entries, settings.GetGrowthSettings(),
                    i => i.GetGrowths(), (i, a) => i.SetGrowths(a), i => i.GetBasicGrowths(), (i, a) => i.SetBasicGrowths(a),
                    "Stat Growths", DataSetEnum.Individual);

            if (settings.RandomizeEnemyStatGrowths)
                RandomizeStatSpread(npcCharacters.Where(i => i.HasGrowths()).ToList(), entries, settings.GetGrowthSettings(),
                    i => i.GetGrowths(), (i, a) => i.SetGrowths(a), i => i.GetBasicGrowths(), (i, a) => i.SetBasicGrowths(a),
                    "Stat Growths", DataSetEnum.Individual);

            HandleInventory(settings, playableCharacters, npcCharacters, toss, weaponIDs, itemIDs, entries);

            HandleFlagSet(settings.AttrsAlly, settings.AttrsAllyCount, playableCharacters, GD.Attributes, entries,
                i => i.GetAttributes(), (i, l) => i.SetAttributes(l), "Attributes");
            HandleFlagSet(settings.AttrsEnemy, settings.AttrsEnemyCount, npcCharacters, GD.Attributes, entries,
                i => i.GetAttributes(), (i, l) => i.SetAttributes(l), "Attributes");

            if (settings.CommonSids.Enabled)
            {
                RandomizePersonalSkills(settings, playableCharacters, entries);
                if (settings.CommonSids.GetArg<bool>(0))
                    RandomizePersonalSkills(settings, npcCharacters, entries);
            }

            StringBuilder innertable = new();
            List<(string id, string name)> mappedNames = MapNames(GD.Characters);
            foreach (Individual i in individuals)
                if (entries[i].Length > 0)
                {
                    innertable.AppendLine($"\t{mappedNames.IDToName(i.Pid)}:");
                    innertable.AppendLine(entries[i].ToString());
                }

            return ApplyTableTitle(innertable, "Characters");
        }

        private void RandomizePersonalSkills(RandomizerSettings.IndividualSettings settings, List<Individual> targets,
            Dictionary<Individual, StringBuilder> entries)
        {
            List<string> generalSkillIDs = GD.GeneralSkills.GetIDs();
            if (settings.CommonSids.GetArg<bool>(0))
            {
                targets.ForEach(i => i.CommonSids = i.CommonSids.
                    Concat(i.NormalSids.Where(generalSkillIDs.Contains)).
                    Concat(i.HardSids.Where(generalSkillIDs.Contains)).
                    Concat(i.LunaticSids.Where(generalSkillIDs.Contains)).Distinct().ToArray());
                targets.ForEach(i => i.NormalSids = i.NormalSids.Where(s => !generalSkillIDs.Contains(s)).ToArray());
                targets.ForEach(i => i.HardSids = i.HardSids.Where(s => !generalSkillIDs.Contains(s)).ToArray());
                targets.ForEach(i => i.LunaticSids = i.LunaticSids.Where(s => !generalSkillIDs.Contains(s)).ToArray());
            }
            if (settings.CommonSidsCount.Enabled)
                RandomizeArraySizes(targets, i => i.CommonSids, (i, a) => i.CommonSids = a, settings.CommonSidsCount.Distribution,
                   generalSkillIDs);
            RandomizeArrayContents(targets, i => i.CommonSids, (i, a) => i.CommonSids = a, settings.CommonSids.Distribution, generalSkillIDs);
            WriteToChangelog(entries, targets, i => i.CommonSids.Where(generalSkillIDs.Contains), "Personal Skills", GD.GeneralSkills, true);
            GD.SetDirty(DataSetEnum.Individual);
        }

        private void HandleInventory(RandomizerSettings.IndividualSettings settings, List<Individual> playableCharacters,
            List<Individual> npcCharacters, List<TypeOfSoldier> toss, List<string> weaponIDs, List<string> itemIDs,
            Dictionary<Individual, StringBuilder> entries)
        {
            List<Individual> inventoryTargets = settings.RandomizeEnemyInventories ? playableCharacters.Concat(npcCharacters).ToList() :
                            playableCharacters;
            if (settings.ItemsWeapons.Enabled)
            {
                if (settings.ItemsWeaponCount.Enabled)
                    RandomizeArraySizes(inventoryTargets, i => i.Items, (i, a) => i.Items = a, settings.ItemsWeaponCount.Distribution, weaponIDs);
                RandomizeArrayContents(inventoryTargets, i => i.Items, (i, a) => i.Items = a, settings.ItemsWeapons.Distribution, weaponIDs);
                if (settings.ItemsWeapons.GetArg<bool>(0))
                    foreach (Individual i in inventoryTargets)
                        EnsureUsableWeapons(toss, weaponIDs, i, settings.ForceUsableWeapon, settings.ItemsWeapons.GetArg<bool>(1));
            }
            if (settings.ItemsItems.Enabled)
            {
                if (settings.ItemsItemCount.Enabled)
                    RandomizeArraySizes(inventoryTargets, i => i.Items, (i, a) => i.Items = a, settings.ItemsItemCount.Distribution, itemIDs);
                RandomizeArrayContents(inventoryTargets, i => i.Items, (i, a) => i.Items = a, settings.ItemsItems.Distribution, itemIDs);
            }
            if (settings.ItemsWeapons.Enabled || settings.ItemsItems.Enabled)
            {
                HashSet<string> allItems = GD.AllItems.GetIDs().ToHashSet();
                WriteToChangelog(entries, inventoryTargets, i => i.Items.Where(allItems.Contains), "Static Inventory", GD.AllItems);
                GD.SetDirty(DataSetEnum.Individual);
            }
        }

        private static void RandomizeArrayContents<T>(List<T> targets, Func<T, string[]> get, Action<T, string[]> set, IDistribution distribution,
            List<string> pool)
        {
            AsNodeStructure(targets, t => get(t), out List<List<Node<string>>> structure, out List<Node<string>> flattened);
            flattened = flattened.Where(n => pool.Contains(n.value)).ToList();
            flattened.Randomize(n => n.value, (n, i) => n.value = i, distribution, pool);
            for (int iIdx = 0; iIdx < structure.Count; iIdx++)
                set(targets[iIdx], structure[iIdx].Select(n => n.value).ToArray());
        }

        private static void RandomizeArraySizes<T>(List<T> targets, Func<T, string[]> get, Action<T, string[]> set, IDistribution distribution,
            List<string> pool)
        {
            List<Node<int>> newCounts = targets.Select(t => new Node<int>(get(t).Where(pool.Contains).Count())).ToList();
            newCounts.Randomize(n => n.value, (n, i) => n.value = i, distribution, 0, int.MaxValue);
            IEnumerable<string> activePool = targets.SelectMany(t => get(t).Where(pool.Contains));
            for (int i = 0; i < targets.Count; i++)
            {
                T t = targets[i];
                int newCount = newCounts[i].value;
                while (get(t).Where(pool.Contains).Count() < newCount)
                {
                    IEnumerable<string> oldValues = get(t);
                    if (activePool.Any())
                        set(t, oldValues.Append(activePool.GetRandom()).ToArray());
                    else
                        set(t, oldValues.Append(pool.GetRandom()).ToArray());
                }
                while (get(t).Where(pool.Contains).Count() > newCount)
                {
                    List<string> oldValues = get(t).ToList();
                    IEnumerable<string> oldPoolValues = oldValues.Where(pool.Contains);
                    oldValues.Remove(oldPoolValues.GetRandom());
                    set(t, oldValues.ToArray());
                }
            }
        }

        private void RandomizeEnemyClasses(RandomizerSettings.IndividualSettings settings, List<Individual> individuals,
            List<TypeOfSoldier> toss, List<string> generalClassIDs, List<string> weaponIDs, Dictionary<Individual, StringBuilder> entries)
        {
            List<Individual> npcClassCharacters = individuals.Where(i => generalClassIDs.Contains(i.Jid)).ToList();
            List<int> totalLevels = npcClassCharacters.Select(i => i.Level + (i.GetTOS(toss).Rank == 1 ? 20 : 0)).ToList();
            if (settings.JidEnemy.GetArg<bool>(0))
            {
                Dictionary<string, string> classMapping = new();
                SplitClassesByRank(toss, GD.UniversalClasses.Concat(GD.MixedNPCExclusiveClasses).ToList(),
                    out List<(string id, string name)> lowGroup, out List<(string id, string name)> highGroup);
                CreateRandomMapping(classMapping, lowGroup);
                CreateRandomMapping(classMapping, highGroup);
                SplitClassesByRank(toss, GD.MaleExclusiveClasses.Concat(GD.MaleNPCExclusiveClasses).ToList(), out lowGroup, out highGroup);
                CreateRandomMapping(classMapping, lowGroup);
                CreateRandomMapping(classMapping, highGroup);
                SplitClassesByRank(toss, GD.FemaleExclusiveClasses.Concat(GD.FemaleNPCExclusiveClasses).ToList(),
                    out lowGroup, out highGroup);
                CreateRandomMapping(classMapping, lowGroup);
                CreateRandomMapping(classMapping, highGroup);
                foreach (Individual i in npcClassCharacters)
                    if (classMapping.TryGetValue(i.Jid, out string? newClass))
                        i.Jid = newClass;
            }
            else
                npcClassCharacters.Randomize(i => i.Jid, (i, s) => i.Jid = s, settings.JidEnemy.Distribution, generalClassIDs);

            for (int iIdx = 0; iIdx < npcClassCharacters.Count; iIdx++)
            {
                Individual i = npcClassCharacters[iIdx];
                TypeOfSoldier tos = i.GetTOS(toss);
                GameData.Gender g = i.GetGender();
                List<string> legalClassIDs = GD.UniversalClasses.GetIDs();
                legalClassIDs.AddRange(GD.MixedNPCExclusiveClasses.GetIDs());
                if (g == GameData.Gender.Male)
                {
                    legalClassIDs.AddRange(GD.MaleExclusiveClasses.GetIDs());
                    legalClassIDs.AddRange(GD.MaleNPCExclusiveClasses.GetIDs());
                }
                if (g == GameData.Gender.Female)
                {
                    legalClassIDs.AddRange(GD.FemaleExclusiveClasses.GetIDs());
                    legalClassIDs.AddRange(GD.FemaleNPCExclusiveClasses.GetIDs());
                }
                legalClassIDs = legalClassIDs.Select(s => toss.First(tos => tos.Jid == s)).Where(tos => tos.MaxLevel == 40 ||
                    (totalLevels[iIdx] > 20 ? tos.Rank == 1 : tos.Rank == 0)).Select(tos => tos.Jid).ToList();
                EnsureLegalClass(toss, totalLevels[iIdx], i, tos, legalClassIDs);
                EnsureUsableWeapons(toss, weaponIDs, i, settings.ForceUsableWeapon, false);
            }
            WriteToChangelog(entries, npcClassCharacters, i => i.Jid, "Class", GD.GeneralClasses);
            GD.SetDirty(DataSetEnum.Individual);
            GD.SetDirty(DataSetEnum.Asset);
        }

        private void EnsureUsableWeapons(List<TypeOfSoldier> toss, List<string> weaponIDs, Individual i, bool forceMin1, bool useLevel)
        {
            List<string> legalWeapons = GetLegalWeapons(i, toss, useLevel);
            if (legalWeapons.Count == 0)
                return;
            if (forceMin1 && i.Items.Length == 0)
                i.Items = new string[] { legalWeapons.GetRandom() };
            for (int itemIdx = 0; itemIdx < i.Items.Length; itemIdx++)
                if (weaponIDs.Contains(i.Items[itemIdx]) && !legalWeapons.Contains(i.Items[itemIdx]))
                    i.Items[itemIdx] = legalWeapons.GetRandom();
        }

        private List<string> GetLegalWeapons(Individual i, List<TypeOfSoldier> toss, bool useLevel)
        {
            List<string> legalWeapons = new();
            if (i.Jid == "")
                return legalWeapons;
            TypeOfSoldier tos = i.GetTOS(toss);
            string[] maxWeaponLevels = tos.GetMaxWeaponLevels();
            List<GameData.ProficiencyLevel> proficiencyLevels = maxWeaponLevels.Select(s => s.ToProficiencyLevel()).ToList();
            foreach (int pIdx in i.GetAptitudes())
                proficiencyLevels[pIdx] = proficiencyLevels[pIdx] == GameData.ProficiencyLevel.S ? GameData.ProficiencyLevel.S :
                    (GameData.ProficiencyLevel)(((int)proficiencyLevels[pIdx]) + 1);
            int weaponLevel = i.Level + (tos.Rank == 1 ? 20 : 0);
            if (!useLevel || !GD.FixedLevelCharacters.Any(t => t.id == i.Pid))
                weaponLevel = 40;
            for (int pIdx = 0; pIdx < proficiencyLevels.Count; pIdx++)
                legalWeapons.AddRange(GetLegalWeapons((GameData.Proficiency)pIdx, proficiencyLevels[pIdx], weaponLevel));

            if ((i.Pid == "PID_リュール" || i.Pid == "PID_M002_ルミエル") &&
                (int)proficiencyLevels[(int)GameData.Proficiency.Sword] >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(GD.LiberationWeapons.GetIDs());
            if ((i.Pid == "PID_リュール" || i.Pid == "PID_M025_ルミエル") &&
                (int)proficiencyLevels[(int)GameData.Proficiency.Sword] >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(GD.WilleGlanzWeapons.GetIDs());
            string[] misericordePids = new string[]
            {
                "PID_ヴェイル", "PID_ヴェイル_フード", "PID_ヴェイル_包帯", "PID_ヴェイル_フード_顔出し",
                "PID_ヴェイル_白_悪", "PID_ヴェイル_黒_悪", "PID_ヴェイル_黒_善", "PID_ヴェイル_黒_善_角折れ",
                "PID_M011_ヴェイル", "PID_M017_ヴェイル", "PID_M021_ヴェイル",
            };
            if (misericordePids.Contains(i.Pid) &&
                (int)proficiencyLevels[(int)GameData.Proficiency.Dagger] >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(GD.MisericordeWeapons.GetIDs());
            string[] obscuritePids = new string[]
            {
                "PID_ヴェイル", "PID_ヴェイル_フード", "PID_ヴェイル_包帯", "PID_ヴェイル_フード_顔出し",
                "PID_ヴェイル_白_悪", "PID_ヴェイル_黒_悪", "PID_ヴェイル_黒_善", "PID_ヴェイル_黒_善_角折れ",
                "PID_M011_ヴェイル", "PID_M017_ヴェイル", "PID_M021_ヴェイル", "PID_M026_ソンブル_人型",
                "PID_エル", "PID_ラファール"
            };
            if (obscuritePids.Contains(i.Pid) &&
                (int)proficiencyLevels[(int)GameData.Proficiency.Tome] >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(GD.ObscuriteWeapons.GetIDs());
            string[] dragonStonePids = new string[]
            {
                "PID_E001_Boss", "PID_E006_Hide8"
            };
            string[] dragonStoneJids = new string[]
            {
                "JID_裏邪竜ノ娘", "JID_裏邪竜ノ子", "JID_裏邪竜ノ子_E1-4", "JID_裏邪竜ノ子_E5",
            };
            if ((dragonStonePids.Contains(i.Pid) || dragonStoneJids.Contains(i.Jid)) &&
                (int)proficiencyLevels[(int)GameData.Proficiency.Special] >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(GD.DragonStones.GetIDs());
            if (i.Jid == "JID_マージカノン" &&
                (int)proficiencyLevels[(int)GameData.Proficiency.Special] >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(GD.Cannonballs.GetIDs());

            return legalWeapons;
        }

        private List<string> GetLegalWeapons(GameData.Proficiency p, GameData.ProficiencyLevel pl, int totalLevel)
        {
            List<string> legalWeapons = new();
            List<List<(string id, string name)>> weapons = GD.WeaponTypeLookup[p]; 
            if ((int)pl >= (int)GameData.ProficiencyLevel.D)
                legalWeapons.AddRange(weapons[0].GetIDs());
            if ((int)pl >= (int)GameData.ProficiencyLevel.C && totalLevel > 8)
                legalWeapons.AddRange(weapons[1].GetIDs());
            if ((int)pl >= (int)GameData.ProficiencyLevel.B && totalLevel > 16)
                legalWeapons.AddRange(weapons[2].GetIDs());
            if ((int)pl >= (int)GameData.ProficiencyLevel.A && totalLevel > 24)
                legalWeapons.AddRange(weapons[3].GetIDs());
            if ((int)pl >= (int)GameData.ProficiencyLevel.S && totalLevel > 32)
                legalWeapons.AddRange(weapons[4].GetIDs());
            if ((int)pl >= (int)GameData.ProficiencyLevel.S && totalLevel > 32)
                legalWeapons.AddRange(weapons[5].GetIDs());
            return legalWeapons;
        }

        private static void SplitClassesByRank(List<TypeOfSoldier> toss, List<(string id, string name)> classGroup,
            out List<(string id, string name)> lowGroup, out List<(string id, string name)> highGroup)
        {
            lowGroup = classGroup.Where(e =>
            {
                TypeOfSoldier tos = toss.First(tos => tos.Jid == e.id);
                return tos.Rank == 0 && tos.MaxLevel == 20;
            }).ToList();
            highGroup = classGroup.Where(e => toss.First(tos => tos.Jid == e.id).Rank == 1).ToList();
            foreach ((string id, string name) e in classGroup)
                if (toss.First(tos => tos.Jid == e.id).MaxLevel == 40)
                    if (50.0.Occur())
                        lowGroup.Add(e);
                    else
                        highGroup.Add(e);
        }

        private void RandomizeStartingClasses(RandomizerSettings.IndividualSettings settings, List<Individual> individuals,
            List<TypeOfSoldier> toss, List<string> weaponIDs, Dictionary<Individual, StringBuilder> entries)
        {
            List<int> totalLevels = individuals.Select(i => i.Level + (i.GetTOS(toss).Rank == 1 ? 20 : 0)).ToList();
            individuals.Randomize(i => i.Jid, (i, s) => i.Jid = s, settings.JidAlly.Distribution, GD.PlayableClasses.GetIDs());
            if (settings.JidEnemy.GetArg<bool>(0))
                foreach (Individual i in individuals)
                {
                    int maxScore = toss.Select(tos => CalcCompatibility(i, tos)).Max();
                    if (CalcCompatibility(i, i.GetTOS(toss)) < maxScore)
                        i.Jid = toss.Where(tos => CalcCompatibility(i, tos) == maxScore).GetRandom().Jid;
                }
            int retryCounter = 0;
            for (int iIdx = 0; iIdx < individuals.Count; iIdx++)
            {
                Individual i = individuals[iIdx];
                TypeOfSoldier tos = i.GetTOS(toss);
                GameData.Gender g = i.GetGender();
                List<string> legalClassIDs = GD.UniversalClasses.GetIDs();
                if (g == GameData.Gender.Male)
                    legalClassIDs.AddRange(GD.MaleExclusiveClasses.GetIDs());
                if (g == GameData.Gender.Female)
                    legalClassIDs.AddRange(GD.FemaleExclusiveClasses.GetIDs());
                legalClassIDs = legalClassIDs.Select(s => toss.First(tos => tos.Jid == s)).Where(tos => tos.MaxLevel == 40 ||
                    (totalLevels[iIdx] > 20 ? tos.Rank == 1 : tos.Rank == 0)).Select(tos => tos.Jid).ToList();
                EnsureLegalClass(toss, totalLevels[iIdx], i, tos, legalClassIDs);
                tos = i.GetTOS(toss);
                if (retryCounter < 32 && settings.JidEnemy.GetArg<bool>(0) && CalcCompatibility(i, tos) < ushort.MaxValue / 2)
                {
                    i.Jid = legalClassIDs.GetRandom();
                    iIdx--;
                    retryCounter++;
                    continue;
                }
                retryCounter = 0;
                EnsureUsableWeapons(toss, weaponIDs, i, settings.ForceUsableWeapon, false);
            }
            WriteToChangelog(entries, individuals, i => i.Jid, "Starting Class", GD.PlayableClasses);
            GD.SetDirty(DataSetEnum.Individual);
            GD.SetDirty(DataSetEnum.Asset);
        }

        private void UnlockExclusiveClassOutfits(List<Asset> assets)
        {
            foreach (Asset a in assets)
                if (a.Conditions.Any(GD.ExclusiveClassesList.Contains) &&
                    a.Conditions.Any(s => s.StartsWith("PID") || s.StartsWith("MPID")))
                {
                    a.Conditions = a.Conditions.Where(s => !s.StartsWith("PID") && !s.StartsWith("MPID")).ToArray();
                    if (GD.RemoveAccList.Contains(a.Acc1Model))
                    {
                        a.Acc1Locator = "";
                        a.Acc1Model = "";
                    }
                    if (GD.RemoveAccList.Contains(a.Acc2Model))
                    {
                        a.Acc2Locator = "";
                        a.Acc2Model = "";
                    }
                    if (GD.RemoveAccList.Contains(a.Acc3Model))
                    {
                        a.Acc3Locator = "";
                        a.Acc3Model = "";
                    }
                    a.ScaleAll = 0;
                    a.ScaleHead = 0;
                    a.ScaleNeck = 0;
                    a.ScaleTorso = 0;
                    a.ScaleShoulders = 0;
                    a.ScaleArms = 0;
                    a.ScaleHands = 0;
                    a.ScaleLegs = 0;
                    a.ScaleFeet = 0;
                    a.VolumeArms = 0;
                    a.VolumeLegs = 0;
                    a.VolumeBust = 0;
                    a.VolumeAbdomen = 0;
                    a.VolumeTorso = 0;
                    a.VolumeScaleArms = 0;
                    a.VolumeScaleLegs = 0;
                    a.Voice = "";
                }
        }

        private static void EnsureLegalClass(List<TypeOfSoldier> toss, int totalLevel, Individual i, TypeOfSoldier tos,
            List<string> legalClassIDs)
        {
            // This is specifically to avoid placing non-fliers on flier terrain in early rout maps.
            if (i.Pid == "PID_M003_イルシオン兵_ランスペガサス" || i.Pid == "PID_M003_イルシオン兵_ランスペガサス_イベント")
                legalClassIDs = legalClassIDs.Where(s => toss.First(tos => tos.Jid == s).MoveType == 3).ToList();
            if (!legalClassIDs.Contains(i.Jid))
            {
                if (tos.MaxLevel == 20 && totalLevel > 20 && tos.Rank == 0)
                {
                    List<string> highJobs = tos.GetHighJobs();
                    if (highJobs.Count > 0)
                        i.Jid = highJobs.GetRandom();
                    else
                        i.Jid = legalClassIDs.GetRandom();
                }
                else if (totalLevel <= 20 && tos.Rank == 1)
                {
                    if (tos.LowJob != "")
                    {
                        IEnumerable<TypeOfSoldier> lowJob = toss.Where(tos0 => tos0.Name == tos.LowJob);
                        i.Jid = lowJob.Any() ? lowJob.GetRandom().Jid : legalClassIDs.GetRandom();
                    }
                    else
                        i.Jid = legalClassIDs.GetRandom();
                }
                if (!legalClassIDs.Contains(i.Jid))
                    i.Jid = legalClassIDs.GetRandom();
            }
            i.Level = (byte)(totalLevel - (i.GetTOS(toss).Rank == 1 ? 20 : 0));
        }

        private static int CalcCompatibility(Individual i, TypeOfSoldier tos)
        {
            List<int> proficiencies = i.GetAptitudes();
            proficiencies.AddRange(i.GetSubAptitudes());
            proficiencies = proficiencies.Distinct().ToList();
            int primaryTotal = 0;
            int secondaryTotal = 0;
            int tertiaryTotal = 0;
            foreach (sbyte req in tos.GetWeaponRequirementValues())
                switch (req)
                {
                    case 1:
                        primaryTotal++;
                        break;
                    case 2:
                        secondaryTotal++;
                        break;
                    case 3:
                        tertiaryTotal++;
                        break;
                }
            if (primaryTotal == 0 && secondaryTotal == 0 && tertiaryTotal == 0)
                return 0;
            int primaryMatches = 0;
            int secondaryMatches = 0;
            int tertiaryMatches = 0;
            foreach (int i32 in proficiencies)
            {
                sbyte req = i32 switch
                {
                    0 => tos.WeaponNone,
                    1 => tos.WeaponSword,
                    2 => tos.WeaponLance,
                    3 => tos.WeaponAxe,
                    4 => tos.WeaponBow,
                    5 => tos.WeaponDagger,
                    6 => tos.WeaponMagic,
                    7 => tos.WeaponRod,
                    8 => tos.WeaponFist,
                    9 => tos.WeaponSpecial,
                    _ => throw new NotImplementedException("Illegal proficiency ID: " + i32),
                };
                if (req == 1)
                    primaryMatches++;
                if (req == 2)
                    secondaryMatches++;
                if (req == 3)
                    tertiaryMatches++;
            }

            return ushort.MaxValue * (primaryMatches + Math.Min(secondaryMatches, 1) + Math.Min(tertiaryMatches, 2)) /
                (primaryTotal + Math.Min(secondaryTotal, 1) + Math.Min(tertiaryTotal, 2)) +
                primaryMatches + Math.Min(secondaryMatches, 1) - secondaryTotal + Math.Min(tertiaryMatches, 2) - tertiaryTotal;
        }

        private void RandomizeStatLimits(RandomizerSettings.IndividualSettings settings, List<Individual> playableCharacters, Dictionary<Individual, StringBuilder> entries)
        {
            playableCharacters.Randomize(i => i.LimitHp, (i, s) => i.LimitHp = s, settings.LimitHp.Distribution,
                                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitStr, (i, s) => i.LimitStr = s, settings.LimitStr.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitTech, (i, s) => i.LimitTech = s, settings.LimitTech.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitQuick, (i, s) => i.LimitQuick = s, settings.LimitQuick.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitLuck, (i, s) => i.LimitLuck = s, settings.LimitLuck.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitDef, (i, s) => i.LimitDef = s, settings.LimitDef.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitMagic, (i, s) => i.LimitMagic = s, settings.LimitMagic.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitMdef, (i, s) => i.LimitMdef = s, settings.LimitMdef.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitPhys, (i, s) => i.LimitPhys = s, settings.LimitPhys.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitSight, (i, s) => i.LimitSight = s, settings.LimitSight.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            playableCharacters.Randomize(i => i.LimitMove, (i, s) => i.LimitMove = s, settings.LimitMove.Distribution,
                sbyte.MinValue, sbyte.MaxValue);
            WriteStatsToChangelog(playableCharacters, entries, i => i.GetLimits(), "Stat Limit Modifiers");
            GD.SetDirty(DataSetEnum.Individual);
        }

        private void RandomizeStatSpread<T>(List<T> individuals, Dictionary<T, StringBuilder> entries,
            RandomizerFieldSettings[] settingsFields, Func<T, sbyte[]> get, Action<T, sbyte[]> set,
            Func<T, sbyte[]> getBasic, Action<T, sbyte[]> setBasic, string fieldName, DataSetEnum dse) where T : notnull
        {
            List<Node<double>> oldStatTotals = individuals.Select(i => new Node<double>(getBasic(i).Select(s =>
                                (double)s).Sum())).ToList();
            RandomizeBaseStats(individuals, settingsFields, get, set);
            HandleStatTotal(individuals, settingsFields, getBasic, setBasic, oldStatTotals);
            WriteStatsToChangelog(individuals, entries, get, fieldName);
            GD.SetDirty(dse);
        }

        private void RandomizeStatSpread<T>(List<T> individuals, Dictionary<T, StringBuilder> entries,
            RandomizerFieldSettings[] settingsFields, Func<T, byte[]> get, Action<T, byte[]> set,
            Func<T, byte[]> getBasic, Action<T, byte[]> setBasic, string fieldName, DataSetEnum dse) where T : notnull
        {
            List<Node<double>> oldStatTotals = individuals.Select(i => new Node<double>(getBasic(i).Select(s =>
                                (double)s).Sum())).ToList();
            RandomizeBaseStats(individuals, settingsFields, get, set);
            HandleStatTotal(individuals, settingsFields, getBasic, setBasic, oldStatTotals);
            WriteStatsToChangelog(individuals, entries, get, fieldName);
            GD.SetDirty(dse);
        }

        private void RandomizeAllyBaseStats(List<Individual> individuals, Dictionary<Individual, StringBuilder> entries,
            RandomizerFieldSettings[] settingsFields, string fieldName, bool strongerProtagonist, bool strongerAllyNPCs)
        {
            List<Node<double>> oldBaseStatTotals = individuals.Select(i => new Node<double>(i.GetBasicOffsetN().Select(s =>
                                (double)s).Sum())).ToList();
            RandomizeBaseStats(individuals, settingsFields, i => i.GetOffsetN(), (i, s) => i.SetOffsetN(s));
            HandleStatTotal(individuals, settingsFields, i => i.GetBasicOffsetN(), (i, s) => i.SetBasicOffsetN(s), oldBaseStatTotals);
            if (strongerProtagonist)
                foreach (Individual i in individuals.FilterData(i0 => i0.Pid, GD.ProtagonistCharacters))
                    i.SetBasicOffsetN(i.GetBasicOffsetN().Select(s => (sbyte)(s + 1)).ToArray());
            if (strongerAllyNPCs)
                foreach (Individual i in individuals.FilterData(i0 => i0.Pid, GD.AllyNPCCharacters))
                    i.SetBasicOffsetN(i.GetBasicOffsetN().Select(s => (sbyte)(s + 5)).ToArray());
            foreach (Individual i in individuals)
            {
                i.SetOffsetH(i.GetOffsetN());
                i.SetOffsetL(i.GetOffsetN());
            }
            WriteStatsToChangelog(individuals, entries, i => i.GetOffsetN(), fieldName);
            GD.SetDirty(DataSetEnum.Individual);
        }

        private static void WriteStatsToChangelog<A, B>(List<B> individuals, Dictionary<B, StringBuilder> entries,
            Func<B, A[]> get, string fieldName) where B : notnull
        {
            foreach (B i in individuals)
            {
                A[] stats = get(i);
                entries[i].AppendLine($"{fieldName}:\t{stats[0]} HP,\t{stats[1]} Str,\t{stats[2]} Dex,\t{stats[3]} Spd,\t" +
                    $"{stats[4]} Lck,\t{stats[5]} Def,\t{stats[6]} Mag,\t{stats[7]} Res,\t" +
                    $"{stats[8]} Bld,\t{stats[9]} Sig,\t{stats[10]} Mov");
            }
        }

        private static void HandleStatTotal<T>(List<T> individuals, RandomizerFieldSettings[] settingsFields,
            Func<T, sbyte[]> getBasic, Action<T, sbyte[]> setBasic, List<Node<double>> baseStatTotals)
        {
            RandomizerFieldSettings totalSettings = settingsFields[11];
            if (totalSettings.Enabled)
            {
                baseStatTotals.Randomize(n => n.value, (n, i) => n.value = i, totalSettings.Distribution,
                    int.MinValue, int.MaxValue);
                for (int iIdx = 0; iIdx < individuals.Count; iIdx++)
                {
                    sbyte[] basicBaseStats = getBasic(individuals[iIdx]);
                    int toAdd = (int)Math.Round(baseStatTotals[iIdx].value - basicBaseStats.Select(s => (double)s).Sum());
                    for (int i = 0; i < basicBaseStats.Length; i++)
                        basicBaseStats[i] = (sbyte)Math.Clamp(basicBaseStats[i] + toAdd / basicBaseStats.Length, sbyte.MinValue, sbyte.MaxValue);
                    setBasic(individuals[iIdx], basicBaseStats);
                }
            }
        }

        private static void HandleStatTotal<T>(List<T> individuals, RandomizerFieldSettings[] settingsFields,
            Func<T, byte[]> getBasic, Action<T, byte[]> setBasic, List<Node<double>> baseStatTotals)
        {
            RandomizerFieldSettings totalSettings = settingsFields[11];
            if (totalSettings.Enabled)
            {
                baseStatTotals.Randomize(n => n.value, (n, i) => n.value = i, totalSettings.Distribution,
                    int.MinValue, int.MaxValue);
                for (int iIdx = 0; iIdx < individuals.Count; iIdx++)
                {
                    byte[] basicBaseStats = getBasic(individuals[iIdx]);
                    int toAdd = (int)Math.Round(baseStatTotals[iIdx].value - basicBaseStats.Select(s => (double)s).Sum());
                    for (int i = 0; i < basicBaseStats.Length; i++)
                        basicBaseStats[i] = (byte)Math.Clamp(basicBaseStats[i] + toAdd / basicBaseStats.Length, byte.MinValue, byte.MaxValue);
                    setBasic(individuals[iIdx], basicBaseStats);
                }
            }
        }

        private static void RandomizeBaseStats<T>(List<T> individuals, RandomizerFieldSettings[] settingsFields, Func<T, sbyte[]> get, Action<T, sbyte[]> set)
        {
            List<sbyte[]> baseStats = individuals.Select(get).ToList();
            for (int sIdx = 0; sIdx < baseStats[0].Length; sIdx++)
                baseStats.Randomize(a => a[sIdx], (a, s) => a[sIdx] = s, settingsFields[sIdx].Distribution,
                    sbyte.MinValue, sbyte.MaxValue);
            for (int iIdx = 0; iIdx < individuals.Count; iIdx++)
                set(individuals[iIdx], baseStats[iIdx]);
        }

        private static void RandomizeBaseStats<T>(List<T> individuals, RandomizerFieldSettings[] settingsFields, Func<T, byte[]> get, Action<T, byte[]> set)
        {
            List<byte[]> baseStats = individuals.Select(get).ToList();
            for (int sIdx = 0; sIdx < baseStats[0].Length; sIdx++)
                baseStats.Randomize(a => a[sIdx], (a, s) => a[sIdx] = s, settingsFields[sIdx].Distribution,
                    byte.MinValue, byte.MaxValue);
            for (int iIdx = 0; iIdx < individuals.Count; iIdx++)
                set(individuals[iIdx], baseStats[iIdx]);
        }

        private void HandleFlagSet<T>(RandomizerFieldSettings settings, RandomizerFieldSettings countSettings,
            List<T> targets, List<(int id, string name)> flags, Dictionary<T, StringBuilder> entries,
            Func<T, List<int>> get, Action<T, List<int>> set, string fieldName, bool writeEmpty = true) where T : notnull 
        {
            if (settings.Enabled)
            {
                List<int> flagIDs = flags.GetIDs();
                if (countSettings.Enabled)
                    RandomizeFlagCounts(countSettings.Distribution, targets, flagIDs, get, set);
                RandomizeFlags(settings.Distribution, targets, flagIDs, get, set);
                WriteFlagsToChangelog(entries, targets, get, flags, fieldName, writeEmpty);
                GD.SetDirty(DataSetEnum.Individual);
            }
        }

        private static void WriteFlagsToChangelog<T>(Dictionary<T, StringBuilder> entries, List<T> targets, Func<T, List<int>> get,
            List<(int id, string name)> entities, string fieldName, bool writeEmpty) where T : notnull
        {
            foreach (T t in targets)
            {
                StringBuilder entry = new($"{fieldName}:\t");
                List<int> flags = get(t);
                foreach (int proficiency in flags)
                    entry.Append(entities.IDToName(proficiency) + ",\t");
                if (flags.Count > 0)
                    entry = new(entry.ToString()[..^2]);
                if (writeEmpty && flags.Count == 0)
                    entry.Append("None");
                entries[t].AppendLine(entry.ToString());
            }
        }

        private static void RandomizeFlags<T>(IDistribution distribution, List<T> targets, List<int> flagIDs, Func<T, List<int>> get,
            Action<T, List<int>> set)
        {
            AsNodeStructure(targets, i => get(i), out List<List<Node<int>>> structure, out List<Node<int>> flattened);
            flattened.Randomize(n => n.value, (n, i) => n.value = i, distribution, flagIDs);
            for (int iIdx = 0; iIdx < structure.Count; iIdx++)
                set(targets[iIdx], structure[iIdx].Select(n => n.value).ToList());
        }

        private static void RandomizeFlagCounts<T>(IDistribution distribution, List<T> targets, List<int> flagIDs, Func<T, List<int>> get,
            Action<T, List<int>> set)
        {
            List<Node<int>> flagCounts = targets.Select(i => new Node<int>(get(i).Count)).ToList();
            flagCounts.Randomize(n => n.value, (n, i) => n.value = i, distribution, 0, flagIDs.Count);
            AsNodeStructure(targets, i => get(i).ToArray(),
                out List<List<Node<int>>> structure, out List<Node<int>> flattened);
            for (int tIdx = 0; tIdx < structure.Count; tIdx++)
            {
                List<Node<int>> tStructure = structure[tIdx];
                int newCount = flagCounts[tIdx].value;
                while (tStructure.Count < newCount)
                    tStructure.Add(flattened.GetRandom());
                while (tStructure.Count > newCount)
                    tStructure.Remove(tStructure.GetRandom());
                set(targets[tIdx], tStructure.Select(n => n.value).ToList());
            }
        }

        private void HandleLevel(RandomizerFieldSettings settings, List<Individual> individuals, List<TypeOfSoldier> toss,
            Dictionary<Individual, StringBuilder> entries)
        {
            if (settings.Enabled)
            {
                individuals.Randomize(i => i.Level, (i, b) => i.Level = b, settings.Distribution, 1, 40);
                // Ensure level is within their class' range
                foreach (Individual i in individuals)
                    if (i.Jid != "")
                        i.Level = Math.Min(i.Level, i.GetTOS(toss).MaxLevel);
                WriteToChangelog(entries, individuals, i => i.Level, "Level");
                GD.SetDirty(DataSetEnum.Individual);
            }
        }

        private void RandomizeBirthdays(List<Individual> playableCharacters, Dictionary<Individual, StringBuilder> entries)
        {
            foreach (Individual i in playableCharacters.Where(i => i.BirthMonth != 0))
            {
                i.BirthMonth = (byte)new UniformConstant(100, 1, 12).Next(0);
                UniformConstant dayDist = new();
                switch (i.BirthMonth)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        dayDist = new(100, 1, 31);
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        dayDist = new(100, 1, 30);
                        break;
                    case 2:
                        dayDist = new(100, 1, 28);
                        break;
                    default:
                        break;
                }
                i.BirthDay = (byte)dayDist.Next(0);
                entries[i].AppendLine($"Birthday:\tDay {i.BirthDay} of month {i.BirthMonth}");
            }
            GD.SetDirty(DataSetEnum.Individual);
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

        private void WriteGrowthTableAptitudeToChangelog(List<ParamGroup> allyBondLevelTables, Dictionary<(ParamGroup pg, int gtIdx),
            StringBuilder> levelEntries)
        {
            foreach (ParamGroup pg in allyBondLevelTables)
            {
                List<GrowthTable> gts = pg.Group.Cast<GrowthTable>().ToList();
                for (int gtIdx = 0; gtIdx < gts.Count; gtIdx++)
                    foreach (int proficiency in gts[gtIdx].GetAptitudes())
                        levelEntries[(pg, gtIdx)].Append(GD.Proficiencies.IDToName(proficiency) + " Proficiency,\t");
            }
        }

        private static void RandomizeGrowthTableAptitude(IDistribution distribution, List<ParamGroup> allyBondLevelTables,
            List<int> proficiencyIDs)
        {
            AsNodeStructure(allyBondLevelTables, gt => gt.GetAptitudes().ToArray(),
                                out List<List<List<Node<int>>>> structure, out List<Node<int>> flattened);
            flattened.Randomize(n => n.value, (n, i) => n.value = i, distribution, proficiencyIDs);
            for (int pgIdx = 0; pgIdx < structure.Count; pgIdx++)
                for (int gtIdx = 0; gtIdx < structure[pgIdx].Count; gtIdx++)
                    ((GrowthTable)allyBondLevelTables[pgIdx].Group[gtIdx]).SetAptitudes(structure[pgIdx][gtIdx].Select(n =>
                        n.value).ToList());
        }

        private static void RandomizeGrowthTableAptitudeCounts(IDistribution distribution, List<ParamGroup> allyBondLevelTables,
            List<int> proficiencyIDs)
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

        private void WriteEngageItemsToChangelog(List<ParamGroup> bondLevelTables,
            Dictionary<(ParamGroup pg, int gtIdx), StringBuilder> levelEntries)
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
            AsNodeStructure(bondLevelTables, gt => gt.EngageItems, out List<List<List<Node<string>>>> itemStructure,
                out List<Node<string>> flattened0);
            AsNodeStructure(bondLevelTables, gt => gt.EngageCooperations, out List<List<List<Node<string>>>> coopStructure,
                out List<Node<string>> flattened1);
            AsNodeStructure(bondLevelTables, gt => gt.EngageHorses, out List<List<List<Node<string>>>> horseStructure,
                out List<Node<string>> flattened2);
            AsNodeStructure(bondLevelTables, gt => gt.EngageCoverts, out List<List<List<Node<string>>>> covertStructure,
                out List<Node<string>> flattened3);
            AsNodeStructure(bondLevelTables, gt => gt.EngageHeavys, out List<List<List<Node<string>>>> heavyStructure,
                out List<Node<string>> flattened4);
            AsNodeStructure(bondLevelTables, gt => gt.EngageFlys, out List<List<List<Node<string>>>> flyStructure,
                out List<Node<string>> flattened5);
            AsNodeStructure(bondLevelTables, gt => gt.EngageMagics, out List<List<List<Node<string>>>> magicStructure,
                out List<Node<string>> flattened6);
            AsNodeStructure(bondLevelTables, gt => gt.EngagePranas, out List<List<List<Node<string>>>> pranaStructure,
                out List<Node<string>> flattened7);
            AsNodeStructure(bondLevelTables, gt => gt.EngageDragons, out List<List<List<Node<string>>>> dragonStructure,
                out List<Node<string>> flattened8);
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

        private void SortSyncStats(List<ParamGroup> pgs)
        {
            foreach (ParamGroup pg in pgs)
            {
                AsNodeStructure(pg, gt => gt.SynchroSkills, out List<List<Node<string>>> structure, out List<Node<string>> flattened);
                for (int a = 0; a < GD.SyncStatLookup.Count; a++)
                {
                    List<Node<string>> singleStat = flattened.Where(n => GD.SyncStatLookup[a].Contains(n.value)).ToList();
                    List<string> skills = singleStat.Select(n => n.value).ToList();
                    skills.Sort((s0, s1) => GD.SyncStatLookup[a].IndexOf(s0) - GD.SyncStatLookup[a].IndexOf(s1));
                    for (int b = 0; b < singleStat.Count; b++)
                        singleStat[b].value = skills[b];
                }
                WriteNodeStructure(pg, gt => gt.SynchroSkills, structure);
            }
        }

        private void LimitSyncStatsTo3(List<ParamGroup> pgs, List<string> syncStatSkillIDs, List<string> syncMovSkillIDs)
        {
            foreach (ParamGroup pg in pgs)
            {
                AsNodeStructure(pg, gt => gt.SynchroSkills,
                    out List<List<Node<string>>> structure, out List<Node<string>> flattened);
                flattened = flattened.Where(n => syncStatSkillIDs.Contains(n.value)).ToList();
                List<string> search = flattened.Select(n => n.value).ToList();
                new Redistribution(100).Randomize(search);
                List<GameData.SyncStat> stats = new();
                while (stats.Count < 3 && search.Count > 0)
                {
                    string s = search.Last();
                    search.RemoveAt(search.Count - 1);
                    GameData.SyncStat stat = GameData.SyncStat.None;
                    for (int i = 0; stat == GameData.SyncStat.None && i < GD.SyncStatLookup.Count; i++)
                        if (GD.SyncStatLookup[i].Contains(s))
                            stat = (GameData.SyncStat)i;
                    if (!stats.Contains(stat) && stat != GameData.SyncStat.Mov)
                        stats.Add(stat);
                }
                List<string> allowedSkills = stats.SelectMany(ss => GD.SyncStatLookup[(int)ss]).ToList();
                foreach (Node<string> n in flattened)
                    if (!allowedSkills.Contains(n.value) && !syncMovSkillIDs.Contains(n.value))
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

        private static void AsNodeStructure<T>(List<ParamGroup> pgs, Func<GrowthTable, T[]> getArray,
            out List<List<List<Node<T>>>> structure, out List<Node<T>> flattened)
        {
            structure = pgs.Select(pg => pg.Group.Select(gp =>
                                            getArray((GrowthTable)gp).Select(s => new Node<T>(s)).ToList()).ToList()).ToList();
            flattened = structure.SelectMany(l => l).SelectMany(l => l).ToList();
        }

        private static void AsNodeStructure<T>(ParamGroup pg, Func<GrowthTable, T[]> getArray, out List<List<Node<T>>> structure,
            out List<Node<T>> flattened)
        {
            structure = pg.Group.Select(gp => getArray((GrowthTable)gp).Select(s => new Node<T>(s)).ToList()).ToList();
            flattened = structure.SelectMany(l => l).ToList();
        }

        private static void AsNodeStructure<A, B>(IEnumerable<A> objects, Func<A, IEnumerable<B>> getIEnumerable,
            out List<List<Node<B>>> structure, out List<Node<B>> flattened)
        {
            structure = objects.Select(i => getIEnumerable(i).Select(s => new Node<B>(s)).ToList()).ToList();
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

        private static void RandomizeGrowthTableArraySizes(IDistribution distribution, List<ParamGroup> pgs,
            Func<GrowthTable, string[]> getArray, Action<GrowthTable, string[]> setArray, List<string> emergencyPool)
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

        private class Node<A, B>
        {
            internal A a;
            internal B b;
            internal Node(A valueA, B valueB) => (a, b) = (valueA, valueB);
        }

        private static void WriteToChangelog<A>(Dictionary<A, StringBuilder> changelogEntries, List<A> objects,
            Func<A, string> property, string propertyName, List<(string id, string name)> type) where A : notnull =>
            objects.ForEach(o =>
                {
                    string name = "None";
                    if (property(o) != "")
                        name = type.IDToName(property(o));
                    changelogEntries[o].AppendLine(propertyName + ":\t" + name);
                });
        private static void WriteToChangelog<A, B>(Dictionary<A, StringBuilder> changelogEntries, List<A> objects,
            Func<A, B> property, string propertyName, List<(B id, string name)> type) where A : notnull =>
            objects.ForEach(o => changelogEntries[o].AppendLine(propertyName + ":\t" + type.IDToName(property(o))));
        private static void WriteToChangelog<T>(Dictionary<T, StringBuilder> changelogEntries, List<T> objects,
            Func<T, IEnumerable<string>> property, string propertyName, List<(string id, string name)> type, bool writeEmpty = false) where T : notnull =>
            objects.ForEach(o =>
            {
                if (property(o).Any())
                    changelogEntries[o].AppendLine(propertyName + ":\t" + string.Join(",\t", property(o).Select(type.IDToName)));
                if (!property(o).Any() && writeEmpty)
                    changelogEntries[o].AppendLine(propertyName + ":\tNone");
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
