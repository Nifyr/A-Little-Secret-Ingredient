using ALittleSecretIngredient.Structs;
using System.Text;

namespace ALittleSecretIngredient
{
    internal class GlobalData
    {
        internal FileManager FM { get; }
        internal AssetBundleParser ABP { get; }
        internal XmlParser XP { get; }
        internal GameData GD { get; }
        internal Randomizer R { get; }
        internal DefaultDistributionSetup DDS { get; }

        internal GlobalData()
        {
            FM = new();
            ABP = new(FM);
            XP = new(ABP);
            GD = new(XP, FM);
            R = new(GD);
            DDS = new(GD);
        }

        internal void Export()
        {
            FileManager.CleanOutputDir();
            List<FileEnum> changedFiles = FM.DirtyFiles();
            XP.Export(changedFiles);
            FileManager.CleanTempDir();
            StringBuilder? changelog = R.PopChangelog();
            if (changelog != null)
                FileManager.SaveChangelog(changelog);
        }
    }
}
