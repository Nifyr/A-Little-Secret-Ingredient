﻿#pragma warning disable CS8618
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
    }
}
