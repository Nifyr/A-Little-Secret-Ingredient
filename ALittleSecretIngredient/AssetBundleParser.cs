using ALittleSecretIngredient.Structs;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System.Globalization;
using System.Reflection;

namespace ALittleSecretIngredient
{
    internal class AssetBundleParser
    {
        private Dictionary<FileEnum, BundleFileInstance> Bundles { get; } = new();
        private Dictionary<(FileGroupEnum fge, string fileName), BundleFileInstance> GroupBundles { get; } = new();

        private const int TextAsset = 49;
        private const int MonoBehaviour = 114;

        internal string ParseToXmlString(FileEnum fe, FileStream fs)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = am.LoadBundleFile(fs, false);
            Bundles[fe] = bfi;
            DecompressBundle(bfi);
            return GetXmlString(bfi);
        }

        internal string? GetXmlString(FileEnum fe)
        {
            Bundles.TryGetValue(fe, out BundleFileInstance? bfi);
            if (bfi is null) return null;
            return GetXmlString(bfi);
        }

        internal string ParseToXmlString(FileGroupEnum fge, string fileName, FileStream fs)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = am.LoadBundleFile(fs, false);
            GroupBundles[(fge, fileName)] = bfi;
            DecompressBundle(bfi);
            return GetXmlString(bfi);
        }

        internal string? GetXmlString(FileGroupEnum fge, string fileName)
        {
            GroupBundles.TryGetValue((fge, fileName), out BundleFileInstance? bfi);
            if (bfi is null) return null;
            return GetXmlString(bfi);
        }

        private static string GetXmlString(BundleFileInstance bfi)
        {
            AssetsManager am = new();
            AssetsFileInstance afi = am.LoadAssetsFileFromBundle(bfi, 0);
            return am.GetTypeInstance(afi, afi.table.GetAssetsOfType(TextAsset).Single()).GetBaseField()["m_Script"].GetValue().AsString()[1..];
        }

        internal DataSet? GetDataSet(FileGroupEnum fge, string fileName)
        {
            GroupBundles.TryGetValue((fge, fileName), out BundleFileInstance? bfi);
            if (bfi is null) return null;
            return GetDataSet(bfi, GameDataLookup.GroupDataParamTypes[(fge, "")]);
        }

        internal DataSet ParseToDataSet(FileGroupEnum fge, string fileName, FileStream fs)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = am.LoadBundleFile(fs, false);
            GroupBundles[(fge, fileName)] = bfi;
            DecompressBundle(bfi);
            return GetDataSet(bfi, GameDataLookup.GroupDataParamTypes[(fge, "")]);
        }

        private static DataSet GetDataSet(BundleFileInstance? bfi, Type t)
        {
            AssetsManager am = new();
            AssetsFileInstance afi = am.LoadAssetsFileFromBundle(bfi, 0);
            return new(am.GetTypeInstance(afi, afi.table.GetAssetsOfType(MonoBehaviour).Single()).GetBaseField(), t);
        }

        private static void DecompressBundle(BundleFileInstance bfi)
        {
            AssetBundleFile abf = bfi.file;

            MemoryStream stream = new();
            abf.Unpack(abf.reader, new AssetsFileWriter(stream));

            stream.Position = 0;

            AssetBundleFile newAbf = new();
            newAbf.Read(new AssetsFileReader(stream), false);

            abf.reader.Close();
            bfi.file = newAbf;
        }

        internal void ExportXmlBytes(FileEnum fe, byte[] bytes, FileStream tempFile, FileStream outputFile)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = Bundles[fe];
            AssetsFileInstance afi = am.LoadAssetsFileFromBundle(bfi, 0);
            AssetTypeValueField atvf = am.GetTypeInstance(afi, afi.table.GetAssetsOfType(TextAsset).Single()).GetBaseField();
            atvf["m_Script"].GetValue().Set(bytes);
            byte[] b = atvf.WriteToByteArray();
            AssetFileInfoEx afie = afi.table.GetAssetInfo(atvf.Get("m_Name").GetValue().AsString(), TextAsset);
            AssetsReplacerFromMemory arfm = new(0, afie.index, (int)afie.curFileType, AssetHelper.GetScriptIndex(afi.file, afie), b);
            List<AssetsReplacer> ars = new() { arfm };
            MemoryStream ms = new();
            AssetsFileWriter afw = new(ms);
            afi.file.Write(afw, 0, ars, 0);
            BundleReplacerFromMemory brfm = new(bfi.file.bundleInf6.dirInf[0].name, null, true, ms.ToArray(), -1);
            afw = new(tempFile);
            bfi.file.Write(afw, new List<BundleReplacer> { brfm });
            afw.Dispose();
            am = new();
            bfi = am.LoadBundleFile(tempFile.Name, false);
            tempFile.Dispose();
            afw = new(outputFile);
            bfi.file.Pack(bfi.file.reader, afw, AssetBundleCompressionType.LZ4);
            afw.Dispose();
            bfi.file.Close();
        }
        internal void ExportXmlBytes(FileGroupEnum fge, string fileName, byte[] bytes, FileStream outputFile)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = GroupBundles[(fge, fileName)];
            AssetsFileInstance afi = am.LoadAssetsFileFromBundle(bfi, 0);
            AssetTypeValueField atvf = am.GetTypeInstance(afi, afi.table.GetAssetsOfType(TextAsset).Single()).GetBaseField();
            atvf["m_Script"].GetValue().Set(bytes);
            byte[] b = atvf.WriteToByteArray();
            AssetFileInfoEx afie = afi.table.GetAssetInfo(atvf.Get("m_Name").GetValue().AsString(), TextAsset);
            AssetsReplacerFromMemory arfm = new(0, afie.index, (int)afie.curFileType, AssetHelper.GetScriptIndex(afi.file, afie), b);
            List<AssetsReplacer> ars = new() { arfm };
            MemoryStream ms = new();
            AssetsFileWriter afw = new(ms);
            afi.file.Write(afw, 0, ars, 0);
            BundleReplacerFromMemory brfm = new(bfi.file.bundleInf6.dirInf[0].name, null, true, ms.ToArray(), -1);
            FileStream fs0 = FileManager.CreateTempFile(fileName);
            afw = new(fs0);
            bfi.file.Write(afw, new List<BundleReplacer> { brfm });
            afw.Dispose();
            am = new();
            bfi = am.LoadBundleFile(fs0.Name, false);
            fs0.Dispose();
            afw = new(outputFile);
            bfi.file.Pack(bfi.file.reader, afw, AssetBundleCompressionType.LZ4);
            afw.Dispose();
            bfi.file.Close();
        }

        internal void ExportDataSet(FileGroupEnum fge, string fileName, DataSet ds, FileStream outputFile)
        {
            BundleFileInstance bfi = GroupBundles[(fge, fileName)];
            AssetsManager am = new();
            AssetsFileInstance afi = am.LoadAssetsFileFromBundle(bfi, 0);
            AssetTypeValueField atvf = am.GetTypeInstance(afi, afi.table.GetAssetsOfType(MonoBehaviour).Single()).GetBaseField();
            ds.Write(atvf);
            byte[] b = atvf.WriteToByteArray();
            AssetFileInfoEx afie = afi.table.GetAssetInfo(atvf.Get("m_Name").GetValue().AsString(), MonoBehaviour);
            AssetsReplacerFromMemory arfm = new(0, afie.index, (int)afie.curFileType, AssetHelper.GetScriptIndex(afi.file, afie), b);
            List<AssetsReplacer> ars = new() { arfm };
            MemoryStream ms = new();
            AssetsFileWriter afw = new(ms);
            afi.file.Write(afw, 0, ars, 0);
            BundleReplacerFromMemory brfm = new(bfi.file.bundleInf6.dirInf[0].name, null, true, ms.ToArray(), -1);
            FileStream fs0 = FileManager.CreateTempFile(fileName);
            afw = new(fs0);
            bfi.file.Write(afw, new List<BundleReplacer> { brfm });
            afw.Dispose();
            am = new();
            bfi = am.LoadBundleFile(fs0.Name, false);
            fs0.Dispose();
            afw = new(outputFile);
            bfi.file.Pack(bfi.file.reader, afw, AssetBundleCompressionType.LZ4);
            afw.Dispose();
            bfi.file.Close();
        }
    }

    internal static class AssetBundleStructUtils
    {
        private static Dictionary<string, AssetTypeTemplateField> ChildATTFs { get; } = new();

        internal static object ParseTo(this AssetTypeValueField atvf, PropertyInfo p) => GetValue(p.PropertyType, atvf[p.Name]);

        private static object GetValue(Type t, AssetTypeValueField atvf)
        {
            if (t.BaseType != null && t.BaseType.Name == "Array")
            {
                Type childType = t.GetElementType()!;
                object[] array = (object[])Array.CreateInstance(childType, atvf["Array"].childrenCount);
                if (array.Length > 0)
                {
                    ChildATTFs[atvf.GetTemplateField().type] = atvf["Array"][0].GetTemplateField();
                    for (int i = 0; i < array.Length; i++)
                        array[i] = GetValue(childType, atvf["Array"][i]);
                }
                return array;
            }
            switch (t.Name)
            {
                case "Byte":
                    return atvf.GetValue().value.asUInt8;
                case "Int32":
                    return atvf.GetValue().AsInt();
                case "Int64":
                    return atvf.GetValue().AsInt64();
                case "String":
                    return atvf.GetValue().AsString();
                default:
                    object o = t.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
                    foreach (PropertyInfo p in t.GetRuntimeProperties())
                        p.SetValue(o, atvf.ParseTo(p));
                    return o;
            }
        }
        internal static void ParseFrom(this AssetTypeValueField atvf, object o)
        {
            Type t = o.GetType();
            if (t.BaseType != null && t.BaseType.Name == "Array")
            {
                object[] array = (object[])o;
                List<AssetTypeValueField> newChildren = new();
                for (int i = 0; i < array.Length; i++)
                {
                    AssetTypeValueField child = ValueBuilder.DefaultValueFieldFromTemplate(ChildATTFs[atvf.GetTemplateField().type]);
                    child.ParseFrom(array[i]);
                    newChildren.Add(child);
                }
                atvf["Array"].SetChildrenList(newChildren.ToArray());
                return;
            }
            switch (t.Name)
            {
                case "Byte":
                case "Int32":
                case "Int64":
                case "String":
                    atvf.GetValue().Set(o);
                    return;
                default:
                    foreach (PropertyInfo p in t.GetRuntimeProperties())
                        atvf[p.Name].ParseFrom(p.GetValue(o)!);
                    return;
            }
        }
    }
}
