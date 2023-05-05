using AssetsTools.NET;
using AssetsTools.NET.Extra;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ALittleSecretIngredient
{
    internal class AssetBundleParser
    {
        private FileManager FM { get; set; }
        private Dictionary<FileEnum, BundleFileInstance> Bundles { get; }

        private const int TextAsset = 49;

        internal AssetBundleParser(FileManager fm)
        {
            FM = fm;
            Bundles = new();
        }

        internal byte[] GetBytes(FileEnum fe)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = GetAssetBundle(fe);
            AssetsFileInstance afi = am.LoadAssetsFileFromBundle(bfi, 0);
            return am.GetTypeInstance(afi,  afi.table.GetAssetsOfType(TextAsset).Single()).GetBaseField()["m_Script"].GetValue().AsStringBytes();
        }

        private BundleFileInstance GetAssetBundle(FileEnum fe)
        {
            if (!Bundles.ContainsKey(fe))
            {
                AssetsManager am = new();
                using FileStream fs = FM.ReadFile(fe);
                Bundles[fe] = am.LoadBundleFile(fs, false);
                DecompressBundle(Bundles[fe]);
            }
            return Bundles[fe];
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

        internal void ExportBytes(FileEnum fe, byte[] bytes)
        {
            AssetsManager am = new();
            BundleFileInstance bfi = GetAssetBundle(fe);
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
            FileStream fs0 = FM.CreateTempFile(fe);
            afw = new(fs0);
            bfi.file.Write(afw, new List<BundleReplacer> { brfm });
            afw.Dispose();
            am = new();
            bfi = am.LoadBundleFile(fs0.Name, false);
            fs0.Dispose();
            using FileStream fs1 = FM.CreateOutputFile(fe);
            afw = new(fs1);
            bfi.file.Pack(bfi.file.reader, afw, AssetBundleCompressionType.LZ4);
            afw.Dispose();
            bfi.file.Close();
        }
    }
}
