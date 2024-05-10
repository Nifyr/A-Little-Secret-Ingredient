using ALittleSecretIngredient.Structs;

namespace ALittleSecretIngredient
{
    internal class GameData
    {
        private XmlParser XP { get; }
        private AssetBundleParser ABP { get; }
        private FileManager FM { get; }

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
                    string? xmlString = ABP.GetXmlString(fe);
                    if (xmlString is null)
                    {
                        OnStatusUpdate?.Invoke($"Parsing {fe} AssetBundle...");
                        using FileStream fs = FM.ReadFile(fe);
                        xmlString = ABP.ParseToXmlString(fe, fs);
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
            string xmlString = ABP.GetXmlString(fe)!;
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
                            ABP.ExportXmlBytes(fe, xmlBytes, tempFile, outputFile);
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
                                string? xmlString = GD.ABP.GetXmlString(FGE, fileName);
                                if (xmlString is null)
                                {
                                    GD.OnStatusUpdate?.Invoke($"Parsing {id} AssetBundle...");
                                    using FileStream fs = GD.FM.ReadFile(FGE, fileName);
                                    xmlString = GD.ABP.ParseToXmlString(FGE, fileName, fs);
                                }
                                GD.OnStatusUpdate?.Invoke($"Parsing {id} XML...");
                                ds = GD.XP.Parse(DSE, fileName, xmlString);
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
                    OnStatusUpdate?.Invoke($"Generating {fileName} XML...");
                    string xmlString = ABP.GetXmlString(fge, fileName)!;
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
                                    ABP.ExportXmlBytes(fge, fileName, xmlBytes, outputFile);
                                    break;
                                }
                        }
                    }
                    break;
                case FileGroupEnum.Terrains:
                    foreach (ExportFormat ef in targets)
                    {
                        OnStatusUpdate?.Invoke($"Saving {fileName} AssetBundle...");
                        using FileStream outputFile = FM.CreateOutputFile(fge, fileName, ef);
                        ABP.ExportDataSet(fge, fileName, DataSetGroups[DataSetEnum.MapTerrain].DataSets[fileName.FileNameToId(fge)], outputFile);
                    }
                    break;
            }
        }

        internal void SetDirty(DataSetEnum dse) => FM.SetDirty(GameDataLookup.DataSetToSheetName[dse].fe);
        internal void SetDirty(DataSetEnum dse, List<(string id, DataSet)> group)
        {
            FileGroupEnum fge = GameDataLookup.GroupDataSetToSheetName[dse].fge;
            group.ForEach(t => FM.SetDirty(fge, t.id.IdToFileName(fge)));
        }

        internal GameData(XmlParser xp, AssetBundleParser abp, FileManager fm)
        {
            XP = xp;
            ABP = abp;
            FM = fm;
        }
    }
}
