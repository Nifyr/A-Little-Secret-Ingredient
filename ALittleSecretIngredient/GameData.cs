using ALittleSecretIngredient.Structs;
using System.Data.SqlTypes;

namespace ALittleSecretIngredient
{
    internal class GameData
    {
        private FileManager FM { get; }
        private AssetBundleParser ABP { get; }
        private XmlParser XP { get; }
        private MsbtParser MP { get; }

        internal event Action<string>? OnStatusUpdate;

        private Dictionary<DataSetEnum, DataSet> DataSets { get; } = new();
        internal DataSet Get(DataSetEnum dse)
        {
            if (!DataSets.TryGetValue(dse, out DataSet? ds))
            {
                ds = XP.GetDataSet(dse);
                if (ds is null)
                {
                    FileEnum fe = GameDataLookup.DataSetToSheetName[dse].fe;
                    string? xmlString = ABP.GetText(fe);
                    if (xmlString is null)
                    {
                        OnStatusUpdate?.Invoke($"Parsing {fe} AssetBundle...");
                        using FileStream fs = FM.ReadFile(fe);
                        xmlString = ABP.ParseToText(fe, fs);
                    }
                    OnStatusUpdate?.Invoke($"Parsing {fe} XML...");
                    ds = XP.Parse(dse, xmlString);
                    OnStatusUpdate?.Invoke($"{fe} Loaded");
                }
                DataSets.Add(dse, ds);
            }
            return ds;
        }

        internal void Export(IEnumerable<FileEnum> changedFiles, IEnumerable<ExportFormat> targets)
        {
            foreach (FileEnum fe in changedFiles)
                Export(fe, targets);
            OnStatusUpdate?.Invoke($"Saving IPS Patch...");
            FileManager.CopyIpsPatch(targets);
            OnStatusUpdate?.Invoke($"*Adequate* results.");
        }

        private void Export(FileEnum fe, IEnumerable<ExportFormat> targets)
        {
            OnStatusUpdate?.Invoke($"Generating {fe} XML...");
            string xmlString = ABP.GetText(fe)!;
            byte[] xmlBytes = XP.ExportXml(fe, xmlString);
            foreach (ExportFormat ef in targets)
            {
                using FileStream outputFile = FM.CreateOutputFile(fe, ef);
                switch (ef)
                {
                    case ExportFormat.Cobalt:
                        {
                            OnStatusUpdate?.Invoke($"Saving {fe} XML...");
                            outputFile.Write(xmlBytes);
                            break;
                        }
                    case ExportFormat.LayeredFS:
                        {
                            OnStatusUpdate?.Invoke($"Saving {fe} AssetBundle...");
                            using FileStream tempFile = FM.CreateTempFile(fe);
                            ABP.ExportTextBytes(fe, xmlBytes, tempFile, outputFile);
                            break;
                        }
                }
            }
        }

        private Dictionary<DataSetEnum, DataSetGroup> DataSetGroups { get; } = new();
        internal List<(string id, DataSet ds)> GetGroup(DataSetEnum dse, List<(string id, string name)> entities)
        {
            if (!DataSetGroups.ContainsKey(dse))
                DataSetGroups.Add(dse, new(this, dse));
            return DataSetGroups[dse].Get(entities);
        }

        private class DataSetGroup
        {
            private GameData GD { get; }
            internal Dictionary<string, DataSet> DataSets { get; }
            private DataSetEnum DSE { get; }
            private FileGroupEnum FGE { get; }

            internal DataSetGroup(GameData gd, DataSetEnum dse)
            {
                GD = gd;
                DSE = dse;
                FGE = GameDataLookup.GroupDataSetToSheetName[dse].fge;
                DataSets = new();
            }

            internal List<(string fileName, DataSet ds)> Get(List<(string id, string name)> entities)
            {
                List<(string id, DataSet)> dataSets = new();
                bool parse = false;
                foreach ((string id, string _) in entities)
                {
                    if (!DataSets.TryGetValue(id, out DataSet? ds))
                    {
                        ds = Parse(id);
                        GD.OnStatusUpdate?.Invoke($"{id} Loaded");
                        DataSets.Add(id, ds);
                        parse = true;
                    }
                    dataSets.Add((id, ds));
                }
                if (parse)
                    GD.OnStatusUpdate?.Invoke($"{FGE} Loaded");
                return dataSets;
            }

            private DataSet Parse(string id)
            {
                string fileName = id.IdToFileName(FGE);
                switch (FGE)
                {
                    case FileGroupEnum.Dispos:
                        {
                            DataSet? ds = GD.XP.GetDataSet(DSE, fileName);
                            if (ds is null)
                            {
                                string? xmlString = GD.ABP.GetText(FGE, fileName);
                                if (xmlString is null)
                                {
                                    GD.OnStatusUpdate?.Invoke($"Parsing {id} AssetBundle...");
                                    using FileStream fs = GD.FM.ReadFile(FGE, fileName);
                                    xmlString = GD.ABP.ParseToText(FGE, fileName, fs);
                                }
                                GD.OnStatusUpdate?.Invoke($"Parsing {id} XML...");
                                ds = GD.XP.Parse(DSE, fileName, xmlString);
                            }
                            return ds;
                        }
                    case FileGroupEnum.Message:
                        {
                            DataSet? ds = GD.MP.GetDataSet(FGE, fileName);
                            if (ds is null)
                            {
                                byte[]? bytes = GD.ABP.GetTextBytes(FGE, fileName);
                                if (bytes is null)
                                {
                                    GD.OnStatusUpdate?.Invoke($"Parsing {id} AssetBundle...");
                                    using FileStream fs = GD.FM.ReadFile(FGE, fileName);
                                    bytes = GD.ABP.ParseToTextBytes(FGE, fileName, fs);
                                }
                                GD.OnStatusUpdate?.Invoke($"Parsing {id} MSBT...");
                                ds = GD.MP.Parse(FGE, fileName, bytes);
                            }
                            return ds;
                        }
                    case FileGroupEnum.Terrains:
                        {
                            DataSet? ds = GD.ABP.GetDataSet(FGE, fileName);
                            if (ds is null)
                            {
                                using FileStream fs = GD.FM.ReadFile(FGE, fileName);
                                GD.OnStatusUpdate?.Invoke($"Parsing {id} AssetBundle...");
                                ds = GD.ABP.ParseToDataSet(FGE, fileName, fs);
                            }
                            return ds;
                        }
                    default: throw new NotImplementedException();
                }
            }
        }

        internal void Export(IEnumerable<(FileGroupEnum fge, string fileName)> changedFiles, IEnumerable<ExportFormat> targets)
        {
            foreach ((FileGroupEnum fge, string fileName) in changedFiles)
                Export(fge, fileName, targets);
            OnStatusUpdate?.Invoke($"A *worthwhile* pursuit.");
        }

        private void Export(FileGroupEnum fge, string fileName, IEnumerable<ExportFormat> targets)
        {
            switch (fge)
            {
                case FileGroupEnum.Dispos:
                    {
                        OnStatusUpdate?.Invoke($"Generating {fileName} XML...");
                        string xmlString = ABP.GetText(fge, fileName)!;
                        byte[] xmlBytes = XP.ExportXml(fge, fileName, xmlString);
                        foreach (ExportFormat ef in targets)
                        {
                            using FileStream outputFile = FM.CreateOutputFile(fge, fileName, ef);
                            switch (ef)
                            {
                                case ExportFormat.Cobalt:
                                    {
                                        OnStatusUpdate?.Invoke($"Saving {fileName} XML...");
                                        outputFile.Write(xmlBytes);
                                        break;
                                    }
                                case ExportFormat.LayeredFS:
                                    {
                                        OnStatusUpdate?.Invoke($"Saving {fileName} AssetBundle...");
                                        ABP.ExportTextBytes(fge, fileName, xmlBytes, outputFile);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case FileGroupEnum.Message:
                    {
                        foreach (ExportFormat ef in targets)
                        {
                            using FileStream outputFile = FM.CreateOutputFile(fge, fileName, ef);
                            DataSet ds = DataSetGroups[DataSetEnum.MsbtMessage].DataSets[fileName.FileNameToId(fge)];
                            switch (ef)
                            {
                                case ExportFormat.Cobalt:
                                    {
                                        OnStatusUpdate?.Invoke($"Generating {fileName} MSBT patch...");
                                        byte[] patch = MP.ExportPatch(fge, fileName, ds);
                                        OnStatusUpdate?.Invoke($"Saving {fileName} MSBT patch...");
                                        outputFile.Write(patch);
                                        break;
                                    }
                                case ExportFormat.LayeredFS:
                                    {
                                        OnStatusUpdate?.Invoke($"Generating {fileName} MSBT...");
                                        byte[] bytes = MP.ExportMsbt(fge, fileName, ds);
                                        OnStatusUpdate?.Invoke($"Saving {fileName} AssetBundle...");
                                        ABP.ExportTextBytes(fge, fileName, bytes, outputFile);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case FileGroupEnum.Terrains:
                    {
                        foreach (ExportFormat ef in targets)
                        {
                            OnStatusUpdate?.Invoke($"Saving {fileName} AssetBundle...");
                            using FileStream outputFile = FM.CreateOutputFile(fge, fileName, ef);
                            ABP.ExportDataSet(fge, fileName,
                                DataSetGroups[DataSetEnum.MapTerrain].DataSets[fileName.FileNameToId(fge)], outputFile);
                        }
                        break;
                    }
            }
        }

        internal void SetDirty(DataSetEnum dse) => FM.SetDirty(GameDataLookup.DataSetToSheetName[dse].fe);
        internal void SetDirty(DataSetEnum dse, (string id, DataSet) t)
        {
            FileGroupEnum fge = GameDataLookup.GroupDataSetToSheetName[dse].fge;
            FM.SetDirty(fge, t.id.IdToFileName(fge));
        }
        internal void SetDirty(DataSetEnum dse, List<(string id, DataSet)> group)
        {
            FileGroupEnum fge = GameDataLookup.GroupDataSetToSheetName[dse].fge;
            group.ForEach(t => FM.SetDirty(fge, t.id.IdToFileName(fge)));
        }

        internal GameData(FileManager fm, AssetBundleParser abp, XmlParser xp, MsbtParser mp)
        {
            FM = fm;
            ABP = abp;
            XP = xp;
            MP = mp;
        }
    }
}
