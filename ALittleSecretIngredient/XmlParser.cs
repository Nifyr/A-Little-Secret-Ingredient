using ALittleSecretIngredient.Structs;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace ALittleSecretIngredient
{

    internal class XmlParser
    {
        private Dictionary<FileEnum, Book> Books { get; }
        private Dictionary<(FileEnum, string), Type> DataParamTypes { get; }
        internal Dictionary<DataSetEnum, (FileEnum fe, string name)> DataSetToSheetName { get; }
        private AssetBundleParser ABP { get; }

        internal XmlParser(AssetBundleParser abp)
        {
            ABP = abp;
            Books = new();
            DataParamTypes = new();
            DataSetToSheetName = new();
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
        }

        private void Bind(FileEnum fe, DataSetEnum dse, Type dataParam, string sheetName)
        {
            DataParamTypes.Add((fe, sheetName), dataParam);
            DataSetToSheetName.Add(dse, (fe, sheetName));
        }

        internal DataSet GetData(DataSetEnum dse)
        {
            (FileEnum fe, string sheetName) = DataSetToSheetName[dse];
            return GetBook(fe).Sheets.First(s => s.Name == sheetName).Data;
        }

        private Book GetBook(FileEnum fe)
        {
            if (!Books.ContainsKey(fe))
            {
                XmlDocument xml = new();
                xml.Load(new StringReader(Encoding.UTF8.GetString(ABP.GetBytes(fe))[1..]));
                Books[fe] = new Book(this, xml.ChildNodes[1]!, fe);
            }
            return Books[fe];
        }

        internal void Export(List<FileEnum> changedFiles)
        {
            foreach (FileEnum fe in changedFiles)
                Export(fe);
        }

        internal static void WriteRandomizerSettings(RandomizerSettings rs)
        {
            using FileStream fs = FileManager.CreateRandomizerSettings();
            DataContractSerializer dcs = new(typeof(RandomizerSettings));
            dcs.WriteObject(fs, rs);
            fs.Close();
        }

        internal static RandomizerSettings? ReadRandomizerSettings()
        {
            FileStream? fs = FileManager.ReadRandomizerSettings();
            if (fs == null)
                return null;
            XmlDictionaryReader xmldr = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer dcs = new(typeof(RandomizerSettings));
            RandomizerSettings? rs = (RandomizerSettings?)dcs.ReadObject(xmldr);
            return rs;
        }

        internal void Export(FileEnum fe)
        {
            XmlDocument xml = new();
            xml.Load(new StringReader(Encoding.UTF8.GetString(ABP.GetBytes(fe))[1..]));
            Books[fe].Write(xml.ChildNodes[1]!);
            MemoryStream ms = new();
            xml.Save(ms);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes);
            ABP.ExportBytes(fe, bytes);
        }

        private class Book
        {
            internal Book(XmlParser xp, XmlNode n, FileEnum fe)
            {
                Count = n.Attributes!["Count"]!.Value;
                Sheets = n.SelectChildren().Select(c => new Sheet(xp, c, fe)).ToArray();
            }
            internal string Count { get; set; }
            internal Sheet[] Sheets { get; set; }

            internal void Write(XmlNode n)
            {
                n.Attributes!["Count"]!.Value = Count;
                for (int i = 0; i < Sheets.Length; i++)
                    Sheets[i].Write(n.ChildNodes[i]!);
            }
        }

        private class Sheet
        {
            internal Sheet(XmlParser xp, XmlNode n, FileEnum fe)
            {
                Name = n.Attributes!["Name"]!.Value;
                Count = n.Attributes["Count"]!.Value;
                Header = new Header(n["Header"]!);
                Data = new DataSet(n["Data"]!, xp.DataParamTypes[(fe, Name)]);
            }

            internal string Name { get; set; }
            internal string Count { get; set; }
            internal Header Header { get; set; }
            internal DataSet Data { get; set; }

            internal void Write(XmlNode n)
            {
                n.Attributes!["Name"]!.Value = Name;
                n.Attributes["Count"]!.Value = Count;
                Header.Write(n["Header"]!);
                Data.Write(n["Data"]!);
            }
        }

        private class Header
        {
            internal Header(XmlNode n)
            {
                Params = n.SelectChildren().Select(c => new HeaderParam(c)).ToArray();
            }
            internal HeaderParam[] Params { get; set; }

            internal void Write(XmlNode n)
            {
                for (int i = 0; i < Params.Length; i++)
                    Params[i].Write(n.ChildNodes[i]!);
            }
        }

        private class HeaderParam
        {
            internal HeaderParam(XmlNode n)
            {
                Name = n.Attributes!["Name"]!.Value;
                Ident = n.Attributes["Ident"]!.Value;
                Type = n.Attributes["Type"]!.Value;
                Min = n.Attributes["Min"]!.Value;
                Max = n.Attributes["Max"]!.Value;
                Chg = n.Attributes["Chg"]!.Value;
            }

            internal string Name { get; set; }
            internal string Ident { get; set; }
            internal string Type { get; set; }
            internal string Min { get; set; }
            internal string Max { get; set; }
            internal string Chg { get; set; }

            internal void Write(XmlNode n)
            {
                n.Attributes!["Name"]!.Value = Name;
                n.Attributes["Ident"]!.Value = Ident;
                n.Attributes["Type"]!.Value = Type;
                n.Attributes["Min"]!.Value = Min;
                n.Attributes["Max"]!.Value = Max;
                n.Attributes["Chg"]!.Value = Chg;
            }
        }
    }

    internal class DataSet
    {
        internal DataSet(XmlNode n, Type paramType)
        {
            Params = n.SelectChildren().Select(c => DataParam.Create(c, paramType)).ToList();
            if (GroupedParams())
                Group();
        }
        internal List<DataParam> Params { get; set; }

        private bool GroupedParams() => Params.Count > 0 && (Params[0] is GroupedParam || Params[0] is ParamGroup);

        private void Group()
        {
            List<ParamGroup> groups = new();
            foreach (DataParam dp in Params)
            {
                GroupedParam gp = (GroupedParam)dp;
                string? name = gp.GetGroupName();
                if (name != null)
                    groups.Add(new(gp));
                else
                    groups.Last().Group.Add(gp);
            }
            Params = groups.Cast<DataParam>().ToList();
        }

        private void UnGroup()
        {
            List<DataParam> unGrouped = new();
            foreach (DataParam dp in Params)
            {
                ParamGroup pg = (ParamGroup)dp;
                unGrouped.Add(pg.GroupMarker);
                unGrouped.AddRange(pg.Group);
            }
            Params = unGrouped;
        }

        internal void Write(XmlNode n)
        {
            if (GroupedParams())
                UnGroup();
            for (int i = 0; i < Params.Count; i++)
                Params[i].Write(n.ChildNodes[i]!);
            if (GroupedParams())
                Group();
        }
    }

    internal class ParamGroup : DataParam
    {
        internal string Name { get; set; }
        internal List<GroupedParam> Group { get; set; }
        internal GroupedParam GroupMarker { get; set; }

        internal ParamGroup(GroupedParam gp)
        {
            GroupMarker = gp;
            Name = GroupMarker.GetGroupName()!;
            Group = new();
        }
    }

    internal abstract class DataParam
    {
        internal static DataParam Create(XmlNode n, Type paramType)
        {
            DataParam dp = (DataParam)paramType.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
            foreach (PropertyInfo p in paramType.GetRuntimeProperties())
                p.SetValue(dp, n.ParseTo(p));
            return dp;
        }

        internal void Write(XmlNode n)
        {
            foreach (PropertyInfo p in GetType().GetRuntimeProperties())
                n.SelectAttributes().First(a => a.Name.Replace(".", "") == p.Name).ParseFrom(p.GetValue(this)!);
        }
    }

    internal abstract class GroupedParam : DataParam
    {
        internal abstract string? GetGroupName();
    }

    internal static class XmlStructUtils
    {
        internal static IEnumerable<XmlAttribute> SelectAttributes(this XmlNode n)
        {
            return n.Attributes!.Cast<XmlAttribute>();
        }
        internal static IEnumerable<XmlNode> SelectChildren(this XmlNode n)
        {
            return n.ChildNodes!.Cast<XmlNode>();
        }

        internal static object ParseTo(this XmlNode n, PropertyInfo p)
        {
            string s = n.SelectAttributes().First(a => a.Name.Replace(".", "") == p.Name).Value;
            return p.PropertyType.Name switch
            {
                "Boolean" => s != "" && bool.Parse(s),
                "SByte" => s == "" ? default : sbyte.Parse(s),
                "Byte" => s == "" ? default : byte.Parse(s),
                "Int16" => s == "" ? default : short.Parse(s),
                "UInt16" => s == "" ? default : ushort.Parse(s),
                "Int32" => s == "" ? default : int.Parse(s),
                "Int32[]" => s.Split(';').SkipLast(1).Select(int.Parse).ToArray(),
                "UInt32" => s == "" ? default : uint.Parse(s),
                "UInt64" => s == "" ? default : ulong.Parse(s),
                "Single" => s == "" ? default : float.Parse(s),
                "Double" => s == "" ? default : float.Parse(s),
                "String" => s,
                "String[]" => s.Split(';').SkipLast(1).ToArray(),
                _ => throw new ArgumentException("Unsupported type: " + p.PropertyType.Name),
            };
        }

        internal static void ParseFrom(this XmlAttribute a, object o)
        {
            a.Value = o.GetType().Name switch
            {
                "Boolean" => ((bool)o).ToString(),
                "SByte" => ((sbyte)o).ToString(),
                "Byte" => ((byte)o).ToString(),
                "Int16" => ((short)o).ToString(),
                "UInt16" => ((ushort)o).ToString(),
                "Int32" => ((int)o).ToString(),
                "Int32[]" => string.Concat(((int[])o).Select(i => i + ";")),
                "UInt32" => ((uint)o).ToString(),
                "UInt64" => ((ulong)o).ToString(),
                "Single" => ((float)o).ToString(),
                "Double" => ((double)o).ToString(),
                "String" => (string)o,
                "String[]" => string.Concat(((string[])o).Select(s => s + ";")),
                _ => throw new ArgumentException("Unsupported type: " + o.GetType().Name),
            };
        }
    }
}
