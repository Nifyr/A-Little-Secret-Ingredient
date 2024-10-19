using ALittleSecretIngredient.Structs;
using MsbtLib;
using System.Text;

namespace ALittleSecretIngredient
{
    internal class MsbtParser
    {
        private Dictionary<(FileGroupEnum fge, string fileName), MSBT> Msbts { get; }

        internal MsbtParser()
        {
            Msbts = new();
        }

        internal DataSet? GetDataSet(FileGroupEnum fge, string fileName)
        {
            Msbts.TryGetValue((fge, fileName), out MSBT? msbt);
            if (msbt is null) return null;
            return new(msbt);
        }

        internal DataSet Parse(FileGroupEnum fge, string fileName, byte[] bytes)
        {
            MSBT msbt = new(bytes);
            Msbts[(fge, fileName)] = msbt;
            return new(msbt);
        }

        internal byte[] ExportMsbt(FileGroupEnum fge, string fileName, DataSet dataSet)
        {
            MSBT msbt = Msbts[(fge, fileName)];
            Dictionary<string, MsbtEntry> entries = msbt.GetTexts();
            foreach (MsbtMessage mm in dataSet.Params.Cast<MsbtMessage>())
                if (entries.TryGetValue(mm.Label, out MsbtEntry? me))
                    me.Value = mm.Value;
                else
                    entries[mm.Label] = new MsbtEntry("", mm.Value);
            msbt.SetTexts(entries);
            return msbt.Write();
        }

        internal byte[] ExportPatch(FileGroupEnum fge, string fileName, DataSet dataSet)
        {
            List<MsbtMessage> targets = new();
            Dictionary<string, MsbtEntry> oldEntries = Msbts[(fge, fileName)].GetTexts();
            foreach (MsbtMessage mm in dataSet.Params.Cast<MsbtMessage>())
                if (!oldEntries.TryGetValue(mm.Label, out MsbtEntry? me) || me.Value != mm.Value)
                    targets.Add(mm);
            StringBuilder sb = new();
            foreach (MsbtMessage mm in targets)
            {
                sb.Append($"[{mm.Label}]" + '\n');
                sb.Append(ToAstraScript(mm.Value) + '\n');
                sb.Append('\n');
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private static string ToAstraScript(string value)
        {
            string formattedText = value;
            string[] parts = formattedText.Split("<raw group=");
            while (parts.Length > 1)
            {
                StringBuilder subSB = new();
                subSB.Append(parts[0]);
                string[] words = parts[1].Split(' ');
                int group = int.Parse(words[0]);
                int type = int.Parse(words[1][5..]);
                string parameters = parts[1].Split(" data=\"")[1].Split("\" />")[0];
                ushort[] data = Array.Empty<ushort>();
                if (parameters.Length > 0)
                    data = parameters.Split(' ').Select(ushort.Parse).ToArray();
                switch ((group, type))
                {
                    case (1, 0):
                    case (1, 1):
                    case (1, 2):
                    case (1, 3):
                    case (1, 4):
                    case (1, 7):
                    case (1, 8):
                    case (1, 9):
                    case (1, 10):
                    case (1, 11):
                        subSB.Append($"$Arg({type})");
                        break;
                    case (2, 0):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            subSB.Append($"$Type({type}, \"{s0}\")");
                            break;
                        }
                    case (2, 1):
                    case (2, 2):
                    case (2, 4):
                        subSB.Append($"$Type({type})");
                        break;
                    case (3, 0):
                    case (3, 3):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            int str1Offset = data[0] / 2 + 1;
                            string s1 = ReadUTF16Param(data, str1Offset);
                            subSB.Append($"$Window({type}, \"{s0}\", \"{s1}\")");
                            break;
                        }
                    case (3, 1):
                    case (3, 2):
                    case (3, 4):
                    case (3, 7):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            subSB.Append($"$Window({type}, \"{s0}\")");
                            break;
                        }
                    case (3, 9):
                        subSB.Append($"$Window2({type})");
                        break;
                    case (4, 0):
                    case (4, 1):
                    case (4, 2):
                        subSB.Append($"$Wait({type})");
                        break;
                    case (4, 3):
                        {
                            int i0 = data[0];
                            subSB.Append($"$Wait({type}, {i0})");
                            break;
                        }
                    case (5, 0):
                    case (5, 1):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            int str1Offset = data[0] / 2 + 1;
                            string s1 = ReadUTF16Param(data, str1Offset);
                            subSB.Append($"$Anim({type}, \"{s0}\", \"{s1}\")");
                            break;
                        }
                    case (6, 0):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            int str1Offset = data[0] / 2 + 1;
                            string s1 = ReadUTF16Param(data, str1Offset);
                            subSB.Append($"$Alias(\"{s0}\", \"{s1}\")");
                            break;
                        }
                    case (6, 3):
                        subSB.Append($"$P");
                        break;
                    case (6, 5):
                        subSB.Append($"$M");
                        break;
                    case (7, 0):
                        {
                            int i0 = data[0];
                            subSB.Append($"$Fade({type}, {i0})");
                            break;
                        }
                    case (7, 1):
                        {
                            int i0 = data[0];
                            int i1 = data[2];
                            subSB.Append($"$Fade({type}, {i0}, {i1})");
                            break;
                        }
                    case (8, 2):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            subSB.Append($"$Icon(\"{s0}\")");
                            break;
                        }
                    case (10, 0):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            int str1Offset = data[0] / 2 + 1;
                            string s1 = ReadUTF16Param(data, str1Offset);
                            subSB.Append($"$G({type}, \"{s0}\", \"{s1}\")");
                            break;
                        }
                    case (11, 0):
                        {
                            string s0 = ReadUTF16Param(data, 0);
                            int str1Offset = data[0] / 2 + 1;
                            string s1 = ReadUTF16Param(data, str1Offset);
                            subSB.Append($"$Show({type}, \"{s0}\", \"{s1}\")");
                            break;
                        }
                    case (11, 1):
                        {
                            int i0 = data[0];
                            string s0 = ReadUTF16Param(data, 2);
                            subSB.Append($"$Hide({i0}, \"{s0}\")");
                            break;
                        }

                    default:
                        throw new NotImplementedException();
                }
                subSB.Append(parts[1].Split("\" />")[1]);
                if (parts.Length > 2)
                    subSB.Append("<raw group=" + string.Join("<raw group=", parts.Skip(2)));
                formattedText = subSB.ToString();
                parts = formattedText.Split("<raw group=");
            }
            return formattedText;
        }

        private static string ReadUTF16Param(ushort[] data, int offset)
        {
            ushort[] str1Param = data.Skip(offset + 1).Take(data[offset] / 2).ToArray();
            return Encoding.Unicode.GetString(str1Param.SelectMany(BitConverter.GetBytes).ToArray());
        }

        private static int ReadInt32Param(ushort[] data, int offset)
        {
            ushort[] str1Param = data.Skip(offset).Take(2).ToArray();
            return BitConverter.ToInt32(str1Param.SelectMany(BitConverter.GetBytes).ToArray());
        }
    }
}
