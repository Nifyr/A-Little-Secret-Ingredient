﻿using System.Text;

namespace ALittleSecretIngredient
{
    internal class GlobalData
    {
        internal FileManager FM { get; }
        internal AssetBundleParser ABP { get; }
        internal XmlParser XP { get; }
        internal MsbtParser MP { get; }
        internal GameData GD { get; }
        internal Randomizer R { get; }
        internal DefaultDistributionSetup DDS { get; }

        internal GlobalData()
        {
            FM = new();
            ABP = new();
            XP = new();
            MP = new();
            GD = new(FM, ABP, XP, MP);
            R = new(GD);
            DDS = new(GD);
        }

        internal void SubscribeToStatusUpdate(Action<string> update)
        {
            GD.OnStatusUpdate += update;
            R.OnStatusUpdate += update;
        }

        internal ExportResult Export(IEnumerable<ExportFormat> targets)
        {
            if (!targets.Any()) return ExportResult.NoExportTargets;
            List<FileEnum> changedFiles = FM.DirtyFiles();
            List<(FileGroupEnum, string)> changedGroupFiles = FM.DirtyGroupFiles();
            if (changedFiles.Count == 0 && changedGroupFiles.Count == 0) return ExportResult.NoChanges;
            FileManager.CleanOutputDir();
            GD.Export(changedFiles, targets);
            GD.Export(changedGroupFiles, targets);
            FileManager.CleanTempDir();
            StringBuilder? changelog = R.PopChangelog();
            if (changelog != null)
                FileManager.SaveChangelog(changelog);
            return ExportResult.Success;
        }
    }
}
