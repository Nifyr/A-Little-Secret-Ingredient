using ALittleSecretIngredient.Structs;
using System.Data.SqlTypes;
using System.Text;
using System.Xml;

namespace ALittleSecretIngredient
{
    internal class GameData
    {
        private XmlParser XP { get; }
        private AssetBundleParser ABP { get; }
        private FileManager FM { get; }

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
                        using FileStream fs = FM.ReadFile(fe);
                        xmlString = ABP.ParseToXmlString(fe, fs);
                    }
                    ds = XP.Parse(dse, xmlString);
                }
                DataSets.Add(dse, ds);
            }
            return ds;
        }

        internal void Export(IEnumerable<FileEnum> changedFiles, IEnumerable<ExportFormat> targets)
        {
            foreach (FileEnum fe in changedFiles)
                Export(fe, targets);
        }

        private void Export(FileEnum fe, IEnumerable<ExportFormat> targets)
        {
            string xmlString = ABP.GetXmlString(fe)!;
            byte[] xmlBytes = XP.ExportXml(fe, xmlString);
            foreach (ExportFormat ef in targets)
            {
                using FileStream outputFile = FM.CreateOutputFile(fe, ef);
                switch (ef)
                {
                    case ExportFormat.Cobalt:
                        {
                            outputFile.Write(xmlBytes);
                            break;
                        }
                    case ExportFormat.LayeredFS:
                        {
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
                foreach ((string id, string _) in entities)
                {
                    if (!DataSets.TryGetValue(id, out DataSet? ds))
                    {
                        ds = Parse(id);
                        DataSets.Add(id, ds);
                    }
                    dataSets.Add((id, ds));
                }
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
                                    using FileStream fs = GD.FM.ReadFile(FGE, fileName);
                                    xmlString = GD.ABP.ParseToXmlString(FGE, fileName, fs);
                                }
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
        }

        private void Export(FileGroupEnum fge, string fileName, IEnumerable<ExportFormat> targets)
        {
            switch (fge)
            {
                case FileGroupEnum.Dispos:
                    string xmlString = ABP.GetXmlString(fge, fileName)!;
                    byte[] xmlBytes = XP.GetNewXml(fge, fileName, xmlString);
                    foreach (ExportFormat ef in targets)
                    {
                        using FileStream outputFile = FM.CreateOutputFile(fge, fileName, ef);
                        switch (ef)
                        {
                            case ExportFormat.Cobalt:
                                {
                                    outputFile.Write(xmlBytes);
                                    break;
                                }
                            case ExportFormat.LayeredFS:
                                {
                                    ABP.ExportXmlBytes(fge, fileName, xmlBytes, outputFile);
                                    break;
                                }
                        }
                    }
                    break;
                case FileGroupEnum.Terrains:
                    foreach (ExportFormat ef in targets)
                    {
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
