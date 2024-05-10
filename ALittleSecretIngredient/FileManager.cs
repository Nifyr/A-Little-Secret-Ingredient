using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ALittleSecretIngredient
{

    internal class FileManager
    {
        private Dictionary<FileEnum, FileData> Files { get; } = new();
        private Dictionary<(FileGroupEnum fge, string fileName), FileData> FileGroups { get; } = new();
        private readonly List<(FileEnum fe, string localPath)> targetFiles = new()
        {
            ( FileEnum.AssetTable, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\assettable.xml.bundle" ),
            ( FileEnum.God, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\god.xml.bundle" ),
            ( FileEnum.Item, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\item.xml.bundle" ),
            ( FileEnum.Job, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\job.xml.bundle" ),
            ( FileEnum.Person, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\person.xml.bundle" ),
            ( FileEnum.Skill, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\skill.xml.bundle" ),
            ( FileEnum.Terrain, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\terrain.xml.bundle" ),
        };
        private readonly List<(FileGroupEnum fge, string localPath)> targetFileGroups = new()
        {
            ( FileGroupEnum.Dispos, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\dispos\" ),
            ( FileGroupEnum.Terrains, @"\StreamingAssets\aa\Switch\fe_assets_gamedata\terrains\" ),
        };
        private Dictionary<FileEnum, string> CobaltFilePatches { get; } = new()
        {
            { FileEnum.AssetTable, "AssetTable.xml" },
            { FileEnum.God, "God.xml" },
            { FileEnum.Item, "Item.xml" },
            { FileEnum.Job, "Job.xml" },
            { FileEnum.Person, "Person.xml" },
            { FileEnum.Skill, "Skill.xml" },
            { FileEnum.Terrain, "Terrain.xml" },
        };
        private Dictionary<FileGroupEnum, string> CobaltFileGroupDirs { get; } = new()
        {
            { FileGroupEnum.Dispos, "Dispos\\" },
        };

        private const string OutputFolderName = "Output";
        private const string CobaltModName = "Cobalt Randomizer Mod";
        private const string LayeredFSModName = "LayeredFS Randomizer Mod";
        private const string TempFolderName = "Temp";
        private const string RandomizerSettingsFileName = "RandomizerSettings.xml";
        private const string ChangelogFileName = "Changelog.txt";
        private const string IpsPatchFileName = "8C08B9719E085F91847B5E0F935D9488.ips";

        private class FileData
        {
            internal FileData(string outGamePath, string sourcePath)
            {
                OutGamePath = outGamePath;
                SourcePath = sourcePath;
                Name = Path.GetFileName(sourcePath);
            }
            internal string OutGamePath { get; }
            internal string SourcePath { get; }
            internal string Name { get; }
            internal bool Dirty { get; set; }

            internal FileStream FileStream
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

            if (!targetFiles.Any(t => File.Exists(dataDir + t.localPath)))
            {
                error = $"It appears that the files in question are presently *unlocatable*. My " +
                    $"attempts to *locate* said files have been concentrated within the following " +
                    $"directory:\n{dataDir}\\StreamingAssets\\aa\\Switch\\fe_assets_gamedata";
                return false;
            }

            foreach ((FileEnum fe, string localPath) in targetFiles)
                Files.Add(fe, new(@"Data\" + localPath, dataDir + localPath));

            foreach ((FileGroupEnum fge, string localPath) in targetFileGroups)
                foreach (string filePath in Directory.GetFiles(dataDir + localPath))
                {
                    string fileName = Path.GetFileName(filePath);
                    if (Path.GetExtension(fileName) == ".bundle")
                        FileGroups.Add((fge, fileName), new FileData(@"Data\" + localPath + fileName, filePath));
                }

            return true;
        }

        internal FileStream ReadFile(FileEnum fe) => Files[fe].FileStream;
        internal FileStream ReadFile(FileGroupEnum fge, string fileName) => FileGroups[(fge, fileName)].FileStream;
        internal List<string> GetFileNames(FileGroupEnum fge) => FileGroups.Where(kvp => kvp.Key.fge == fge)
            .Select(kvp => kvp.Key.fileName).ToList();

        internal static FileStream? ReadRandomizerSettings()
        {
            string randomizerSettingsPath = Directory.GetCurrentDirectory() + @"\" + RandomizerSettingsFileName;
            if (!File.Exists(randomizerSettingsPath))
                return null;
            return File.OpenRead(randomizerSettingsPath);
        }

        internal void SetDirty(FileEnum fe) => Files[fe].Dirty = true;

        internal void SetDirty(FileGroupEnum fge, string fileName) => FileGroups[(fge, fileName)].Dirty = true;

        internal List<FileEnum> DirtyFiles()
        {
            List<FileEnum> dirtyFiles = new();
            foreach (FileEnum fe in Enum.GetValues<FileEnum>())
                if (Files[fe].Dirty)
                    dirtyFiles.Add(fe);
            return dirtyFiles;
        }

        internal List<(FileGroupEnum, string)> DirtyGroupFiles()
        {
            List<(FileGroupEnum, string)> dirtyFiles = new();
            foreach (FileGroupEnum fge in Enum.GetValues<FileGroupEnum>())
                dirtyFiles.AddRange(FileGroups.Where(kvp => kvp.Key.fge == fge && kvp.Value.Dirty).Select(kvp => (kvp.Key.fge, kvp.Key.fileName)));
            return dirtyFiles;
        }

        private static string GetOutputDir() => $"{Directory.GetCurrentDirectory()}\\" +
            $"{OutputFolderName}\\";
        private static string GetOutputModDir(ExportFormat ef) => $"{GetOutputDir()}" +
            $"{(ef == ExportFormat.Cobalt ? CobaltModName : LayeredFSModName)}\\";

        private string GetOutputFilePath(FileEnum fe, ExportFormat ef)
        {
            string path = $"{GetOutputModDir(ef)}";
            path += ef switch
            {
                ExportFormat.Cobalt => CobaltFilePatches.TryGetValue(fe, out string? fileName) ? $"patches\\xml\\{fileName}" : Files[fe].OutGamePath,
                ExportFormat.LayeredFS => $"romfs\\{Files[fe].OutGamePath}",
                _ => throw new NotImplementedException(),
            };
            return path;
        }
        private string GetOutputFilePath(FileGroupEnum fge, string fileName, ExportFormat ef)
        {
            string path = $"{GetOutputModDir(ef)}";
            path += ef switch
            {
                ExportFormat.Cobalt => CobaltFileGroupDirs.TryGetValue(fge, out string? dir) ?
                    $"patches\\xml\\{dir}{fileName.Replace(".xml.bundle", "").ToUpper()}.xml" : FileGroups[(fge, fileName)].OutGamePath,
                ExportFormat.LayeredFS => $"romfs\\{FileGroups[(fge, fileName)].OutGamePath}",
                _ => throw new NotImplementedException(),
            };
            return path;
        }

        internal FileStream CreateOutputFile(FileEnum fe, ExportFormat ef)
        {
            string path = GetOutputFilePath(fe, ef);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            return File.Create(path);
        }
        internal FileStream CreateOutputFile(FileGroupEnum fge, string fileName, ExportFormat ef)
        {
            string path = GetOutputFilePath(fge, fileName, ef);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            return File.Create(path);
        }

        internal static FileStream CreateRandomizerSettings() => File.Create(Directory.GetCurrentDirectory() + @"\" +
            RandomizerSettingsFileName);

        internal static void SaveChangelog(StringBuilder sb)
        {
            Directory.CreateDirectory(GetOutputDir());
            File.WriteAllText($"{GetOutputDir()}{ChangelogFileName}", sb.ToString());
        }

        internal static void CleanOutputDir()
        {
            if (Directory.Exists(GetOutputDir()))
                Directory.Delete(GetOutputDir(), true);
        }

        internal static void CleanTempDir()
        {
            if (Directory.Exists(GetTempDir()))
                Directory.Delete(GetTempDir(), true);
        }

        internal static void DeleteRandomizerSettings()
        {
            string randomizerSettingsPath = Directory.GetCurrentDirectory() + @"\" + RandomizerSettingsFileName;
            if (File.Exists(randomizerSettingsPath))
                File.Delete(randomizerSettingsPath);
        }

        private static string GetTempDir() => $"{Directory.GetCurrentDirectory()}\\" +
            $"{TempFolderName}\\";

        private string GetTempFilePath(FileEnum fe) => $"{GetTempDir()}{Files[fe].Name}";
        private static string GetTempFilePath(string fileName) => $"{GetTempDir()}{fileName}";
        internal FileStream CreateTempFile(FileEnum fe)
        {
            Directory.CreateDirectory(GetTempDir());
            return File.Create(GetTempFilePath(fe));
        }
        internal static FileStream CreateTempFile(string fileName)
        {
            Directory.CreateDirectory(GetTempDir());
            return File.Create(GetTempFilePath(fileName));
        }

        private static string GetIpsPatchOutputPath(ExportFormat ef)
        {
            string outputPath = $"{GetOutputModDir(ef)}";
            outputPath += ef switch
            {
                ExportFormat.Cobalt => $"",
                ExportFormat.LayeredFS => $"exefs\\",
                _ => throw new NotImplementedException(),
            };
            outputPath += IpsPatchFileName;
            return outputPath;
        }

        internal static void CopyIpsPatch(IEnumerable<ExportFormat> targets)
        {
            foreach (ExportFormat ef in targets)
            {
                string outputPath = GetIpsPatchOutputPath(ef);
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
                File.Copy($"{Directory.GetCurrentDirectory()}\\{IpsPatchFileName}", outputPath);
            }
        }
    }
}
