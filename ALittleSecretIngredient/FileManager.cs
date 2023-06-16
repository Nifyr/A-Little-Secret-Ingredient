using System.Text;

namespace ALittleSecretIngredient
{

    internal class FileManager
    {
        private Dictionary<FileEnum, FileData> Files { get; } = new();
        private readonly (FileEnum fe, string localPath)[] targetFiles = new (FileEnum fe, string localPath)[]
        {
            (FileEnum.AssetTable, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\assettable.xml.bundle"),
            (FileEnum.God, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\god.xml.bundle"),
            (FileEnum.Item, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\item.xml.bundle"),
            (FileEnum.Job, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\job.xml.bundle"),
            (FileEnum.Person, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\person.xml.bundle"),
            (FileEnum.Skill, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\skill.xml.bundle")
        };
        private Dictionary<FileEnum, string> CobaltPatches { get; } = new()
        {
            { FileEnum.AssetTable, "AssetTable.xml" },
            { FileEnum.God, "God.xml" },
            { FileEnum.Item, "Item.xml" },
            { FileEnum.Job, "Job.xml" },
            { FileEnum.Person, "Person.xml" },
            { FileEnum.Skill, "Skill.xml" },
        };

        private const string OutputFolderName = "Output";
        private const string CobaltModName = "Cobalt Randomizer Mod";
        private const string LayeredFSModName = "LayeredFS Randomizer Mod";
        private const string TempFolderName = "Temp";
        private const string RandomizerSettingsFileName = "RandomizerSettings.xml";
        private const string ChangelogFileName = "Changelog.txt";

        private struct FileData
        {
            internal string OutGamePath { get; set; }
            internal string SourcePath { get; set; }
            internal bool Dirty { get; set; }

            public FileStream FileStream
            {
                get => File.Open(SourcePath, FileMode.Open, FileAccess.Read);
            }
        }

        internal bool TryInitialize(string romfsDir, out string? error)
        {
            error = null;
            string dataDir = romfsDir + @"\Data";
            if (!Directory.Exists(dataDir))
            {
                if (Directory.Exists(romfsDir + @"\StreamingAssets"))
                    dataDir = romfsDir;
                else
                {
                    error = $"This folder structure *deviates* from the expected format, rendering " +
                        $"it impossible to locate the directory at the following path:" +
                        $"\n{romfsDir}\\Data.";
                    return false;
                }
            }

            if (!targetFiles.Any(s => File.Exists(dataDir + s.localPath)))
            {
                error = $"It appears that the files in question are presently *unlocatable*. My " +
                    $"attempts to *locate* said files have been concentrated within the following " +
                    $"directory:\n{dataDir}\\StreamingAssets\\aa\\Switch\\fe_assets_gamedata";
                return false;
            }

            foreach ((FileEnum fe, string localPath) in targetFiles)
                Files.Add(fe, new()
                {
                    OutGamePath = @"Data\" + localPath,
                    SourcePath = dataDir + localPath
                });
            return true;
        }

        internal FileStream ReadFile(FileEnum fe) => Files[fe].FileStream;

        internal static FileStream? ReadRandomizerSettings()
        {
            string randomizerSettingsPath = Directory.GetCurrentDirectory() + @"\" + RandomizerSettingsFileName;
            if (!File.Exists(randomizerSettingsPath))
                return null;
            return File.OpenRead(randomizerSettingsPath);
        }

        internal void SetDirty(FileEnum fe)
        {
            FileData fd = Files[fe];
            fd.Dirty = true;
            Files[fe] = fd;
        }

        internal List<FileEnum> DirtyFiles()
        {
            List<FileEnum> dirtyFiles = new();
            foreach (FileEnum fe in Enum.GetValues<FileEnum>())
                if (Files[fe].Dirty)
                    dirtyFiles.Add(fe);
            return dirtyFiles;
        }

        private static string GetOutputPath() => $"{Directory.GetCurrentDirectory()}\\" +
            $"{OutputFolderName}";
        private static string GetOutputModPath(ExportFormat ef) => $"{GetOutputPath()}\\" +
            $"{(ef == ExportFormat.Cobalt ? CobaltModName : LayeredFSModName)}";
        private string GetPath(FileEnum fe, ExportFormat ef)
        {
            string path = $"{GetOutputModPath(ef)}\\";
            path += ef switch
            {
                ExportFormat.Cobalt => CobaltPatches.TryGetValue(fe, out string? fileName) ? $"patches\\xml\\{fileName}" : Files[fe].OutGamePath,
                ExportFormat.LayeredFS => $"romfs\\{Files[fe].OutGamePath}",
                _ => throw new NotImplementedException(),
            };
            return path;
        }
        internal FileStream CreateOutputFile(FileEnum fe, ExportFormat ef)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GetPath(fe, ef))!);
            return File.Create(GetPath(fe, ef));
        }

        internal static FileStream CreateRandomizerSettings() => File.Create(Directory.GetCurrentDirectory() + @"\" +
            RandomizerSettingsFileName);

        internal static void SaveChangelog(StringBuilder sb)
        {
            Directory.CreateDirectory(GetOutputPath());
            File.WriteAllText($"{GetOutputPath()}\\{ChangelogFileName}", sb.ToString());
        }

        internal static void CleanOutputDir()
        {
            if (Directory.Exists(GetOutputPath()))
                Directory.Delete(GetOutputPath(), true);
        }

        internal static void CleanTempDir()
        {
            if (Directory.Exists(GetTempPath()))
                Directory.Delete(GetTempPath(), true);
        }

        internal static void DeleteRandomizerSettings()
        {
            string randomizerSettingsPath = Directory.GetCurrentDirectory() + @"\" + RandomizerSettingsFileName;
            if (File.Exists(randomizerSettingsPath))
                File.Delete(randomizerSettingsPath);
        }

        private static string GetTempPath() => $"{Directory.GetCurrentDirectory()}\\" +
            $"{TempFolderName}";

        private string GetTempPath(FileEnum fe) => $"{GetTempPath()}\\{Path.GetFileName(Files[fe].OutGamePath)}";
        internal FileStream CreateTempFile(FileEnum fe)
        {
            Directory.CreateDirectory(GetTempPath());
            return File.Create(GetTempPath(fe));
        }
    }
}
