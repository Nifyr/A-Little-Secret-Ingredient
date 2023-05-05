#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{

    internal class GodGeneral : DataParam
    {
        internal string Out { get; set; }
        internal string Gid { get; set; }
        internal string Mid { get; set; }
        internal string Nickname { get; set; }
        internal string Help { get; set; }
        internal string AsciiName { get; set; }
        internal string SoundID { get; set; }
        internal string AssetID { get; set; }
        internal string FaceIconName { get; set; }
        internal string FaceIconNameDarkness { get; set; }
        internal string Ringname { get; set; }
        internal string Ringhelp { get; set; }
        internal string UnitIconID { get; set; }
        internal string[] Change { get; set; }
        internal string Link { get; set; }
        internal string EngageHaunt { get; set; }
        internal byte Level { get; set; }
        internal sbyte ForceType { get; set; }
        internal sbyte Female { get; set; }
        internal sbyte GoodWeapon { get; set; }
        internal short Sort { get; set; }
        internal byte EngageCount { get; set; }
        internal string EngageAttack { get; set; }
        internal string EngageAttackRampage { get; set; }
        internal string EngageAttackLink { get; set; }
        internal string LinkGid { get; set; }
        internal string Gbid { get; set; }
        internal string GrowTable { get; set; }
        internal byte LevelCap { get; set; }
        internal string UnlockLevelCapVarName { get; set; }
        internal string EngraveWord { get; set; }
        internal sbyte EngravePower { get; set; }
        internal sbyte EngraveWeight { get; set; }
        internal sbyte EngraveHit { get; set; }
        internal sbyte EngraveCritical { get; set; }
        internal sbyte EngraveAvoid { get; set; }
        internal sbyte EngraveSecure { get; set; }
        internal sbyte SynchroEnhanceHp { get; set; }
        internal sbyte SynchroEnhanceStr { get; set; }
        internal sbyte SynchroEnhanceTech { get; set; }
        internal sbyte SynchroEnhanceQuick { get; set; }
        internal sbyte SynchroEnhanceLuck { get; set; }
        internal sbyte SynchroEnhanceDef { get; set; }
        internal sbyte SynchroEnhanceMagic { get; set; }
        internal sbyte SynchroEnhanceMdef { get; set; }
        internal sbyte SynchroEnhancePhys { get; set; }
        internal sbyte SynchroEnhanceMove { get; set; }
        internal uint Flag { get; set; } // ?, ?, corrupted?, corrupted?, Restricts weapons, DLC?
        internal bool GetWeaponRestricted() => (Flag & (1 << 4)) > 0;
        internal void SetWeaponRestricted(bool value)
        {
            if (value ^ GetWeaponRestricted())
                Flag ^= 1 << 4;
        }
        internal byte NetRankingIndex { get; set; }
        internal sbyte AIEngageAttackType { get; set; }
    }

    internal class GrowthTable : GroupedParam
    {
        internal string Ggid { get; set; }
        internal byte Level { get; set; }
        internal string[] InheritanceSkills { get; set; }
        internal string[] SynchroSkills { get; set; }
        internal string[] EngageSkills { get; set; }
        internal string[] EngageItems { get; set; }
        internal string[] EngageCooperations { get; set; }
        internal string[] EngageHorses { get; set; }
        internal string[] EngageCoverts { get; set; }
        internal string[] EngageHeavys { get; set; }
        internal string[] EngageFlys { get; set; }
        internal string[] EngageMagics { get; set; }
        internal string[] EngagePranas { get; set; }
        internal string[] EngageDragons { get; set; }
        internal uint Aptitude { get; set; }
        internal ushort AptitudeCostNone { get; set; }
        internal ushort AptitudeCostSword { get; set; }
        internal ushort AptitudeCostLance { get; set; }
        internal ushort AptitudeCostAxe { get; set; }
        internal ushort AptitudeCostBow { get; set; }
        internal ushort AptitudeCostDagger { get; set; }
        internal ushort AptitudeCostMagic { get; set; }
        internal ushort AptitudeCostRod { get; set; }
        internal ushort AptitudeCostFist { get; set; }
        internal ushort AptitudeCostSpecial { get; set; }
        internal uint Flag { get; set; }

        internal override string? GetGroupName()
        {
            return Ggid == "" ? null : Ggid;
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
        internal bool GetFlag(int index) => (Flag & (1 << index)) > 0;
        internal void SetFlag(int index, bool value)
        {
            if (value ^ GetFlag(index))
                Flag ^= (uint)(1 << index);
        }
    }

    internal class BondLevel : DataParam
    {
        internal string Out { get; set; }
        internal string Level { get; set; }
        internal int Exp { get; set; }
        internal string RelianceLevel { get; set; }
        internal int Cost { get; set; }
    }
}
