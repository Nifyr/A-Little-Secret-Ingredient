#pragma warning disable CS8618
namespace ALittleSecretIngredient.Structs
{
    internal class Individual : DataParam
    {
        internal string Out { get; set; }
        internal string Pid { get; set; }
        internal string Fid { get; set; }
        internal string Name { get; set; }
        internal string Jid { get; set; }
        internal string Aid { get; set; }
        internal string Help { get; set; }
        internal string Die { get; set; }
        internal string Belong { get; set; }
        internal string UnitIconID { get; set; }
        internal short Age { get; set; }
        internal sbyte Gender { get; set; }
        internal byte BirthMonth { get; set; }
        internal byte BirthDay { get; set; }
        internal byte Level { get; set; }
        internal sbyte InternalLevel { get; set; }
        internal sbyte AutoGrowOffsetN { get; set; }
        internal sbyte AutoGrowOffsetH { get; set; }
        internal sbyte AutoGrowOffsetL { get; set; }
        internal sbyte AssetForce { get; set; }
        internal string SupportCategory { get; set; }
        internal int SkillPoint { get; set; }
        internal byte BmapSize { get; set; }
        internal byte Flag { get; set; }
        internal uint Aptitude { get; set; }
        internal uint SubAptitude { get; set; }
        internal sbyte OffsetNHp { get; set; }
        internal sbyte OffsetNStr { get; set; }
        internal sbyte OffsetNTech { get; set; }
        internal sbyte OffsetNQuick { get; set; }
        internal sbyte OffsetNLuck { get; set; }
        internal sbyte OffsetNDef { get; set; }
        internal sbyte OffsetNMagic { get; set; }
        internal sbyte OffsetNMdef { get; set; }
        internal sbyte OffsetNPhys { get; set; }
        internal sbyte OffsetNSight { get; set; }
        internal sbyte OffsetNMove { get; set; }
        internal sbyte OffsetHHp { get; set; }
        internal sbyte OffsetHStr { get; set; }
        internal sbyte OffsetHTech { get; set; }
        internal sbyte OffsetHQuick { get; set; }
        internal sbyte OffsetHLuck { get; set; }
        internal sbyte OffsetHDef { get; set; }
        internal sbyte OffsetHMagic { get; set; }
        internal sbyte OffsetHMdef { get; set; }
        internal sbyte OffsetHPhys { get; set; }
        internal sbyte OffsetHSight { get; set; }
        internal sbyte OffsetHMove { get; set; }
        internal sbyte OffsetLHp { get; set; }
        internal sbyte OffsetLStr { get; set; }
        internal sbyte OffsetLTech { get; set; }
        internal sbyte OffsetLQuick { get; set; }
        internal sbyte OffsetLLuck { get; set; }
        internal sbyte OffsetLDef { get; set; }
        internal sbyte OffsetLMagic { get; set; }
        internal sbyte OffsetLMdef { get; set; }
        internal sbyte OffsetLPhys { get; set; }
        internal sbyte OffsetLSight { get; set; }
        internal sbyte OffsetLMove { get; set; }
        internal sbyte LimitHp { get; set; }
        internal sbyte LimitStr { get; set; }
        internal sbyte LimitTech { get; set; }
        internal sbyte LimitQuick { get; set; }
        internal sbyte LimitLuck { get; set; }
        internal sbyte LimitDef { get; set; }
        internal sbyte LimitMagic { get; set; }
        internal sbyte LimitMdef { get; set; }
        internal sbyte LimitPhys { get; set; }
        internal sbyte LimitSight { get; set; }
        internal sbyte LimitMove { get; set; }
        internal byte GrowHp { get; set; }
        internal byte GrowStr { get; set; }
        internal byte GrowTech { get; set; }
        internal byte GrowQuick { get; set; }
        internal byte GrowLuck { get; set; }
        internal byte GrowDef { get; set; }
        internal byte GrowMagic { get; set; }
        internal byte GrowMdef { get; set; }
        internal byte GrowPhys { get; set; }
        internal byte GrowSight { get; set; }
        internal byte GrowMove { get; set; }
        internal string[] Items { get; set; }
        internal string DropItem { get; set; }
        internal float DropRatio { get; set; }
        internal uint Attrs { get; set; }
        internal string[] CommonSids { get; set; }
        internal string[] NormalSids { get; set; }
        internal string[] HardSids { get; set; }
        internal string[] LunaticSids { get; set; }
        internal string EngageSid { get; set; }
        internal float TalkPauseDelayMin { get; set; }
        internal float TalkPauseDelayMax { get; set; }
        internal float TalkPauseSpeed { get; set; }
        internal string CombatBgm { get; set; }
        internal string ExistDieCid { get; set; }
        internal sbyte ExistDieTiming { get; set; }
        internal sbyte Hometown { get; set; }
        internal byte NetRankingIndex { get; set; }
        internal string[] NotLvUpTalkPids { get; set; }
        internal sbyte SummonColor { get; set; }
        internal sbyte SummonRank { get; set; }
        internal int SummonRate { get; set; }
        internal string SummonGod { get; set; }
        internal bool GetFlag(int index) => (Flag & (1 << index)) > 0;
        internal void SetFlag(int index, bool value)
        {
            if (value ^ GetFlag(index))
                Flag ^= (byte)(1 << index);
        }

        internal List<int> GetAptitudes()
        {
            List<int> l = new();
            for (int i = 0; i < 32; i++)
                if ((Aptitude & (1 << i)) > 0)
                    l.Add(i);
            return l;
        }

        internal void SetAptitudes(List<int> l)
        {
            Aptitude = 0;
            for (int i = 0; i < l.Count; i++)
                Aptitude |= (uint)(1 << l[i]);
        }

        internal List<int> GetSubAptitudes()
        {
            List<int> l = new();
            for (int i = 0; i < 32; i++)
                if ((SubAptitude & (1 << i)) > 0)
                    l.Add(i);
            return l;
        }

        internal void SetSubAptitudes(List<int> l)
        {
            SubAptitude = 0;
            for (int i = 0; i < l.Count; i++)
                SubAptitude |= (uint)(1 << l[i]);
        }

        internal sbyte[] GetBasicOffsetN() => new sbyte[]
        {
            OffsetNHp, OffsetNStr, OffsetNTech, OffsetNQuick,
            OffsetNLuck, OffsetNDef, OffsetNMagic, OffsetNMdef,
        };

        internal sbyte[] GetBasicOffsetH() => new sbyte[]
        {
            OffsetHHp, OffsetHStr, OffsetHTech, OffsetHQuick,
            OffsetHLuck, OffsetHDef, OffsetHMagic, OffsetHMdef,
        };

        internal sbyte[] GetBasicOffsetL() => new sbyte[]
        {
            OffsetLHp, OffsetLStr, OffsetLTech, OffsetLQuick,
            OffsetLLuck, OffsetLDef, OffsetLMagic, OffsetLMdef,
        };

        internal void SetBasicOffsetN(sbyte[] value)
        {
            OffsetNHp = value[0];
            OffsetNStr = value[1];
            OffsetNTech = value[2];
            OffsetNQuick = value[3];
            OffsetNLuck = value[4];
            OffsetNDef = value[5];
            OffsetNMagic = value[6];
            OffsetNMdef = value[7];
        }

        internal void SetBasicOffsetH(sbyte[] value)
        {
            OffsetHHp = value[0];
            OffsetHStr = value[1];
            OffsetHTech = value[2];
            OffsetHQuick = value[3];
            OffsetHLuck = value[4];
            OffsetHDef = value[5];
            OffsetHMagic = value[6];
            OffsetHMdef = value[7];
        }

        internal void SetBasicOffsetL(sbyte[] value)
        {
            OffsetLHp = value[0];
            OffsetLStr = value[1];
            OffsetLTech = value[2];
            OffsetLQuick = value[3];
            OffsetLLuck = value[4];
            OffsetLDef = value[5];
            OffsetLMagic = value[6];
            OffsetLMdef = value[7];
        }

        internal sbyte[] GetOffsetN() => new sbyte[]
        {
            OffsetNHp, OffsetNStr, OffsetNTech, OffsetNQuick,
            OffsetNLuck, OffsetNDef, OffsetNMagic, OffsetNMdef,
            OffsetNPhys, OffsetNSight, OffsetNMove
        };

        internal sbyte[] GetOffsetH() => new sbyte[]
        {
            OffsetHHp, OffsetHStr, OffsetHTech, OffsetHQuick,
            OffsetHLuck, OffsetHDef, OffsetHMagic, OffsetHMdef,
            OffsetHPhys, OffsetHSight, OffsetHMove
        };

        internal sbyte[] GetOffsetL() => new sbyte[]
        {
            OffsetLHp, OffsetLStr, OffsetLTech, OffsetLQuick,
            OffsetLLuck, OffsetLDef, OffsetLMagic, OffsetLMdef,
            OffsetLPhys, OffsetLSight, OffsetLMove
        };

        internal void SetOffsetN(sbyte[] value)
        {
            OffsetNHp = value[0];
            OffsetNStr = value[1];
            OffsetNTech = value[2];
            OffsetNQuick = value[3];
            OffsetNLuck = value[4];
            OffsetNDef = value[5];
            OffsetNMagic = value[6];
            OffsetNMdef = value[7];
            OffsetNPhys = value[8];
            OffsetNSight = value[9];
            OffsetNMove = value[10];
        }

        internal void SetOffsetH(sbyte[] value)
        {
            OffsetHHp = value[0];
            OffsetHStr = value[1];
            OffsetHTech = value[2];
            OffsetHQuick = value[3];
            OffsetHLuck = value[4];
            OffsetHDef = value[5];
            OffsetHMagic = value[6];
            OffsetHMdef = value[7];
            OffsetHPhys = value[8];
            OffsetHSight = value[9];
            OffsetHMove = value[10];
        }

        internal void SetOffsetL(sbyte[] value)
        {
            OffsetLHp = value[0];
            OffsetLStr = value[1];
            OffsetLTech = value[2];
            OffsetLQuick = value[3];
            OffsetLLuck = value[4];
            OffsetLDef = value[5];
            OffsetLMagic = value[6];
            OffsetLMdef = value[7];
            OffsetLPhys = value[8];
            OffsetLSight = value[9];
            OffsetLMove = value[10];
        }

        internal sbyte[] GetLimits() => new sbyte[]
        {
            LimitHp, LimitStr, LimitTech, LimitQuick,
            LimitLuck, LimitDef, LimitMagic, LimitMdef,
            LimitPhys, LimitSight, LimitMove
        };
    }
}
