using ALittleSecretIngredient.Structs;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace ALittleSecretIngredient
{
    internal class XmlParser
    {
        private Dictionary<FileEnum, Book> Books { get; }
        private Dictionary<(FileGroupEnum fge, string fileName), Book> GroupBooks { get; }

        internal XmlParser()
        {
            Books = new();
            GroupBooks = new();
        }

        internal DataSet? GetDataSet(DataSetEnum dse)
        {
            (FileEnum fe, string sheetName) = GameDataLookup.DataSetToSheetName[dse];
            Books.TryGetValue(fe, out Book? b);
            return b?.Sheets.First(s => s.Name == sheetName).Data;
        }

        internal DataSet Parse(DataSetEnum dse, string xmlString)
        {
            (FileEnum fe, string sheetName) = GameDataLookup.DataSetToSheetName[dse];
            XmlDocument xml = new();
            xml.Load(new StringReader(xmlString));
            Book b = new(xml.ChildNodes[1]!, fe);
            Books[fe] = b;
            return b.Sheets.First(s => s.Name == sheetName).Data;
        }

        internal DataSet? GetDataSet(DataSetEnum dse, string fileName)
        {
            (FileGroupEnum fge, string sheetName) = GameDataLookup.GroupDataSetToSheetName[dse];
            GroupBooks.TryGetValue((fge, fileName), out Book? b);
            return b?.Sheets.First(s => s.Name == sheetName).Data;
        }

        internal DataSet Parse(DataSetEnum dse, string fileName, string xmlString)
        {
            (FileGroupEnum fge, string sheetName) = GameDataLookup.GroupDataSetToSheetName[dse];
            XmlDocument xml = new();
            xml.Load(new StringReader(xmlString));
            Book b = new(xml.ChildNodes[1]!, fge);
            GroupBooks[(fge, fileName)] = b;
            return b.Sheets.First(s => s.Name == sheetName).Data;
        }

        internal byte[] ExportXml(FileEnum fe, string oldXmlString)
        {
            XmlDocument xml = new();
            xml.Load(new StringReader(oldXmlString));
            Books[fe].Write(xml.ChildNodes[1]!);
            MemoryStream ms = new();
            xml.Save(ms);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes);
            return bytes;
        }

        internal byte[] GetNewXml(FileGroupEnum fge, string fileName, string oldXmlString)
        {
            XmlDocument xml = new();
            xml.Load(new StringReader(oldXmlString));
            GroupBooks[(fge, fileName)].Write(xml.ChildNodes[1]!);
            MemoryStream ms = new();
            xml.Save(ms);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes);
            return bytes;
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
            using FileStream? fs = FileManager.ReadRandomizerSettings();
            if (fs == null)
                return null;
            XmlDictionaryReader xmldr = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer dcs = new(typeof(RandomizerSettings));
            RandomizerSettings? rs = (RandomizerSettings?)dcs.ReadObject(xmldr);
            return rs;
        }

        private class Book
        {
            internal Book(XmlNode n, FileEnum fe)
            {
                Count = n.Attributes!["Count"]!.Value;
                Sheets = n.SelectChildren().Select(c => new Sheet(c, fe)).ToArray();
            }
            internal Book(XmlNode n, FileGroupEnum fge)
            {
                Count = n.Attributes!["Count"]!.Value;
                Sheets = n.SelectChildren().Select(c => new Sheet(c, fge)).ToArray();
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
            internal Sheet(XmlNode n, FileEnum fe)
            {
                Name = n.Attributes!["Name"]!.Value;
                Count = n.Attributes["Count"]!.Value;
                Header = new Header(n["Header"]!);
                Data = new DataSet(n["Data"]!, GameDataLookup.DataParamTypes[(fe, Name)]);
            }
            internal Sheet(XmlNode n, FileGroupEnum fge)
            {
                Name = n.Attributes!["Name"]!.Value;
                Count = n.Attributes["Count"]!.Value;
                Header = new Header(n["Header"]!);
                Data = new DataSet(n["Data"]!, GameDataLookup.GroupDataParamTypes[(fge, Name)]);
            }

            internal string Name { get; set; }
            internal string Count { get; set; }
            internal Header Header { get; set; }
            internal DataSet Data { get; set; }

            internal void Write(XmlNode n)
            {
                n.Attributes!["Name"]!.Value = Name;
                n.Attributes["Count"]!.Value = Data.ParamCount().ToString();
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
                "SByte" => s == "" ? default : sbyte.Parse(s, CultureInfo.InvariantCulture),
                "Byte" => s == "" ? default : byte.Parse(s, CultureInfo.InvariantCulture),
                "Int16" => s == "" ? default : short.Parse(s, CultureInfo.InvariantCulture),
                "UInt16" => s == "" ? default : ushort.Parse(s, CultureInfo.InvariantCulture),
                "Int32" => s == "" ? default : int.Parse(s, CultureInfo.InvariantCulture),
                "Int32[]" => s.Split(';').SkipLast(1).Select(i => int.Parse(i, CultureInfo.InvariantCulture)).ToArray(),
                "UInt32" => s == "" ? default : uint.Parse(s, CultureInfo.InvariantCulture),
                "UInt64" => s == "" ? default : ulong.Parse(s, CultureInfo.InvariantCulture),
                "Single" => s == "" ? default : float.Parse(s, CultureInfo.InvariantCulture),
                "Double" => s == "" ? default : double.Parse(s, CultureInfo.InvariantCulture),
                "String" => s,
                "String[]" => s.Split(';').SkipLast(1).ToArray(),
                _ => throw new ArgumentException("Unsupported type: " + p.PropertyType.Name),
            };
        }

        internal static void ParseFrom(this XmlAttribute a, object o)
        {
            a.Value = o.GetType().Name switch
            {
                "Boolean" => ((bool)o).ToString(CultureInfo.InvariantCulture),
                "SByte" => ((sbyte)o).ToString(CultureInfo.InvariantCulture),
                "Byte" => ((byte)o).ToString(CultureInfo.InvariantCulture),
                "Int16" => ((short)o).ToString(CultureInfo.InvariantCulture),
                "UInt16" => ((ushort)o).ToString(CultureInfo.InvariantCulture),
                "Int32" => ((int)o).ToString(CultureInfo.InvariantCulture),
                "Int32[]" => string.Concat(((int[])o).Select(i => i.ToString(CultureInfo.InvariantCulture) + ";")),
                "UInt32" => ((uint)o).ToString(CultureInfo.InvariantCulture),
                "UInt64" => ((ulong)o).ToString(CultureInfo.InvariantCulture),
                "Single" => ((float)o).ToString(CultureInfo.InvariantCulture),
                "Double" => ((double)o).ToString(CultureInfo.InvariantCulture),
                "String" => (string)o,
                "String[]" => string.Concat(((string[])o).Select(s => s + ";")),
                _ => throw new ArgumentException("Unsupported type: " + o.GetType().Name),
            };
        }
    }
}
