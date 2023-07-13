#pragma warning disable CS8618

using System.Linq;

namespace ALittleSecretIngredient.Structs
{
    internal class TypeOfSoldier : DataParam
    {
        internal string Out { get; set; }
        internal string Jid { get; set; }
        internal string Aid { get; set; }
        internal string Name { get; set; }
        internal string Help { get; set; }
        internal string UnitIconID_M { get; set; }
        internal string UnitIconID_F { get; set; }
        internal string UnitIconWeaponID { get; set; }
        internal sbyte Rank { get; set; }
        internal string StyleName { get; set; }
        internal sbyte MoveType { get; set; }
        internal byte StepFrame { get; set; }
        internal byte MaxLevel { get; set; }
        internal sbyte InternalLevel { get; set; }
        internal ushort Sort { get; set; }
        internal byte Flag { get; set; }
        // 1→Can reclass into, 2→Available for everyone, 4→Female exclusive, 8→?
        internal sbyte WeaponNone { get; set; }
        internal sbyte WeaponSword { get; set; }
        internal sbyte WeaponLance { get; set; }
        internal sbyte WeaponAxe { get; set; }
        internal sbyte WeaponBow { get; set; }
        internal sbyte WeaponDagger { get; set; }
        internal sbyte WeaponMagic { get; set; }
        internal sbyte WeaponRod { get; set; }
        internal sbyte WeaponFist { get; set; }
        internal sbyte WeaponSpecial { get; set; }
        internal sbyte WeaponTool { get; set; }
        internal string MaxWeaponLevelNone { get; set; }
        internal string MaxWeaponLevelSword { get; set; }
        internal string MaxWeaponLevelLance { get; set; }
        internal string MaxWeaponLevelAxe { get; set; }
        internal string MaxWeaponLevelBow { get; set; }
        internal string MaxWeaponLevelDagger { get; set; }
        internal string MaxWeaponLevelMagic { get; set; }
        internal string MaxWeaponLevelRod { get; set; }
        internal string MaxWeaponLevelFist { get; set; }
        internal string MaxWeaponLevelSpecial { get; set; }
        internal byte BaseHp { get; set; }
        internal byte BaseStr { get; set; }
        internal byte BaseTech { get; set; }
        internal byte BaseQuick { get; set; }
        internal byte BaseLuck { get; set; }
        internal byte BaseDef { get; set; }
        internal byte BaseMagic { get; set; }
        internal byte BaseMdef { get; set; }
        internal byte BasePhys { get; set; }
        internal byte BaseSight { get; set; }
        internal byte BaseMove { get; set; }
        internal byte LimitHp { get; set; }
        internal byte LimitStr { get; set; }
        internal byte LimitTech { get; set; }
        internal byte LimitQuick { get; set; }
        internal byte LimitLuck { get; set; }
        internal byte LimitDef { get; set; }
        internal byte LimitMagic { get; set; }
        internal byte LimitMdef { get; set; }
        internal byte LimitPhys { get; set; }
        internal byte LimitSight { get; set; }
        internal byte LimitMove { get; set; }
        internal byte BaseGrowHp { get; set; }
        internal byte BaseGrowStr { get; set; }
        internal byte BaseGrowTech { get; set; }
        internal byte BaseGrowQuick { get; set; }
        internal byte BaseGrowLuck { get; set; }
        internal byte BaseGrowDef { get; set; }
        internal byte BaseGrowMagic { get; set; }
        internal byte BaseGrowMdef { get; set; }
        internal byte BaseGrowPhys { get; set; }
        internal byte BaseGrowSight { get; set; }
        internal byte BaseGrowMove { get; set; }
        internal sbyte DiffGrowHp { get; set; }
        internal sbyte DiffGrowStr { get; set; }
        internal sbyte DiffGrowTech { get; set; }
        internal sbyte DiffGrowQuick { get; set; }
        internal sbyte DiffGrowLuck { get; set; }
        internal sbyte DiffGrowDef { get; set; }
        internal sbyte DiffGrowMagic { get; set; }
        internal sbyte DiffGrowMdef { get; set; }
        internal sbyte DiffGrowPhys { get; set; }
        internal sbyte DiffGrowSight { get; set; }
        internal sbyte DiffGrowMove { get; set; }
        internal sbyte DiffGrowNormalHp { get; set; }
        internal sbyte DiffGrowNormalStr { get; set; }
        internal sbyte DiffGrowNormalTech { get; set; }
        internal sbyte DiffGrowNormalQuick { get; set; }
        internal sbyte DiffGrowNormalLuck { get; set; }
        internal sbyte DiffGrowNormalDef { get; set; }
        internal sbyte DiffGrowNormalMagic { get; set; }
        internal sbyte DiffGrowNormalMdef { get; set; }
        internal sbyte DiffGrowNormalPhys { get; set; }
        internal sbyte DiffGrowNormalSight { get; set; }
        internal sbyte DiffGrowNormalMove { get; set; }
        internal sbyte DiffGrowHardHp { get; set; }
        internal sbyte DiffGrowHardStr { get; set; }
        internal sbyte DiffGrowHardTech { get; set; }
        internal sbyte DiffGrowHardQuick { get; set; }
        internal sbyte DiffGrowHardLuck { get; set; }
        internal sbyte DiffGrowHardDef { get; set; }
        internal sbyte DiffGrowHardMagic { get; set; }
        internal sbyte DiffGrowHardMdef { get; set; }
        internal sbyte DiffGrowHardPhys { get; set; }
        internal sbyte DiffGrowHardSight { get; set; }
        internal sbyte DiffGrowHardMove { get; set; }
        internal sbyte DiffGrowLunaticHp { get; set; }
        internal sbyte DiffGrowLunaticStr { get; set; }
        internal sbyte DiffGrowLunaticTech { get; set; }
        internal sbyte DiffGrowLunaticQuick { get; set; }
        internal sbyte DiffGrowLunaticLuck { get; set; }
        internal sbyte DiffGrowLunaticDef { get; set; }
        internal sbyte DiffGrowLunaticMagic { get; set; }
        internal sbyte DiffGrowLunaticMdef { get; set; }
        internal sbyte DiffGrowLunaticPhys { get; set; }
        internal sbyte DiffGrowLunaticSight { get; set; }
        internal sbyte DiffGrowLunaticMove { get; set; }
        internal string HighJob1 { get; set; }
        internal string HighJob2 { get; set; }
        internal string LowJob { get; set; }
        internal string[] CCItems { get; set; }
        internal string ShortName { get; set; }
        internal string[] UniqueItems { get; set; }
        internal string[] Skills { get; set; }
        internal string LearningSkill { get; set; }
        internal string LunaticSkill { get; set; }
        internal uint Attrs { get; set; }

        internal sbyte[] GetWeaponRequirementValues() => new sbyte[]
        {
            WeaponNone, WeaponSword, WeaponLance, WeaponAxe,
            WeaponBow, WeaponDagger, WeaponMagic, WeaponRod,
            WeaponFist, WeaponSpecial,
        };

        internal sbyte[] GetBasicWeaponRequirementValues() => new sbyte[]
        {
            WeaponSword, WeaponLance, WeaponAxe,
            WeaponBow, WeaponDagger, WeaponMagic, WeaponRod,
            WeaponFist,
        };

        internal void SetBasicWeaponRequirementValues(sbyte[] values)
        {
            WeaponSword = values[0];
            WeaponLance = values[1];
            WeaponAxe = values[2];
            WeaponBow = values[3];
            WeaponDagger = values[4];
            WeaponMagic = values[5];
            WeaponRod = values[6];
            WeaponFist = values[7];
        }

        internal List<int> GetBasicWeaponRequirements()
        {
            List<int> output = new();
            sbyte[] wrvs = GetWeaponRequirementValues();
            for (int i = 1; i < 9; i++)
                if (wrvs[i] > 0)
                    output.Add(i);
            return output;
        }

        internal int GetBasicWeaponRequirementCount()
        {
            int output = 0;
            sbyte[] wrvs = GetWeaponRequirementValues();
            for (int i = 1; i < 9; i++)
                if (wrvs[i] == 1)
                    output++;
            if (wrvs.Any(i8 => i8 == 2 || i8 == 3))
                output++;
            return output;
        }

        internal List<string> GetHighJobs()
        {
            List<string> highJobs = new();
            if (HighJob1 != "")
                highJobs.Add(HighJob1);
            if (HighJob2 != "")
                highJobs.Add(HighJob2);
            return highJobs;
        }

        internal string[] GetMaxWeaponLevels() => new string[]
        {
            MaxWeaponLevelNone, MaxWeaponLevelSword, MaxWeaponLevelLance, MaxWeaponLevelAxe,
            MaxWeaponLevelBow, MaxWeaponLevelDagger, MaxWeaponLevelMagic, MaxWeaponLevelRod,
            MaxWeaponLevelFist, MaxWeaponLevelSpecial,
        };

        internal void SetMaxWeaponLevels(string[] values)
        {
            MaxWeaponLevelNone = values[0];
            MaxWeaponLevelSword = values[1];
            MaxWeaponLevelLance = values[2];
            MaxWeaponLevelAxe = values[3];
            MaxWeaponLevelBow = values[4];
            MaxWeaponLevelDagger = values[5];
            MaxWeaponLevelMagic = values[6];
            MaxWeaponLevelRod = values[7];
            MaxWeaponLevelFist = values[8];
            MaxWeaponLevelSpecial = values[9];
        }

        internal bool IsAdvancedOrSpecial() => Rank > 0 || MaxLevel > 20;

        internal byte[] GetBases() => new byte[]
        {
            BaseHp, BaseStr, BaseTech, BaseQuick, BaseLuck, BaseDef, BaseMagic, BaseMdef,
            BasePhys, BaseSight, BaseMove,
        };

        internal byte[] GetBasicBases() => new byte[]
        {
            BaseHp, BaseStr, BaseTech, BaseQuick, BaseLuck, BaseDef, BaseMagic, BaseMdef,
        };

        internal void SetBases(byte[] values)
        {
            BaseHp = values[0];
            BaseStr = values[1];
            BaseTech = values[2];
            BaseQuick = values[3];
            BaseLuck = values[4];
            BaseDef = values[5];
            BaseMagic = values[6];
            BaseMdef = values[7];
            BasePhys = values[8];
            BaseSight = values[9];
            BaseMove = values[10];
        }

        internal void SetBasicBases(byte[] values)
        {
            BaseHp = values[0];
            BaseStr = values[1];
            BaseTech = values[2];
            BaseQuick = values[3];
            BaseLuck = values[4];
            BaseDef = values[5];
            BaseMagic = values[6];
            BaseMdef = values[7];
        }

        internal byte[] GetLimits() => new byte[]
        {
            LimitHp, LimitStr, LimitTech, LimitQuick, LimitLuck, LimitDef, LimitMagic, LimitMdef,
            LimitPhys, LimitSight, LimitMove,
        };

        internal byte[] GetBasicLimits() => new byte[]
        {
            LimitHp, LimitStr, LimitTech, LimitQuick, LimitLuck, LimitDef, LimitMagic, LimitMdef,
        };

        internal void SetLimits(byte[] values)
        {
            LimitHp = values[0];
            LimitStr = values[1];
            LimitTech = values[2];
            LimitQuick = values[3];
            LimitLuck = values[4];
            LimitDef = values[5];
            LimitMagic = values[6];
            LimitMdef = values[7];
            LimitPhys = values[8];
            LimitSight = values[9];
            LimitMove = values[10];
        }

        internal void SetBasicLimits(byte[] values)
        {
            LimitHp = values[0];
            LimitStr = values[1];
            LimitTech = values[2];
            LimitQuick = values[3];
            LimitLuck = values[4];
            LimitDef = values[5];
            LimitMagic = values[6];
            LimitMdef = values[7];
        }

        internal byte[] GetEnemyGrowths() => new byte[]
        {
            BaseGrowHp, BaseGrowStr, BaseGrowTech, BaseGrowQuick, BaseGrowLuck, BaseGrowDef, BaseGrowMagic, BaseGrowMdef,
            BaseGrowPhys, BaseGrowSight, BaseGrowMove,
        };

        internal byte[] GetBasicEnemyGrowths() => new byte[]
        {
            BaseGrowHp, BaseGrowStr, BaseGrowTech, BaseGrowQuick, BaseGrowLuck, BaseGrowDef, BaseGrowMagic, BaseGrowMdef,
        };

        internal void SetEnemyGrowths(byte[] values)
        {
            BaseGrowHp = values[0];
            BaseGrowStr = values[1];
            BaseGrowTech = values[2];
            BaseGrowQuick = values[3];
            BaseGrowLuck = values[4];
            BaseGrowDef = values[5];
            BaseGrowMagic = values[6];
            BaseGrowMdef = values[7];
            BaseGrowPhys = values[8];
            BaseGrowSight = values[9];
            BaseGrowMove = values[10];
        }

        internal void SetBasicEnemyGrowths(byte[] values)
        {
            BaseGrowHp = values[0];
            BaseGrowStr = values[1];
            BaseGrowTech = values[2];
            BaseGrowQuick = values[3];
            BaseGrowLuck = values[4];
            BaseGrowDef = values[5];
            BaseGrowMagic = values[6];
            BaseGrowMdef = values[7];
        }

        internal sbyte[] GetGrowthModifiers() => new sbyte[]
        {
            DiffGrowHp, DiffGrowStr, DiffGrowTech, DiffGrowQuick, DiffGrowLuck, DiffGrowDef, DiffGrowMagic, DiffGrowMdef,
            DiffGrowPhys, DiffGrowSight, DiffGrowMove,
        };

        internal sbyte[] GetBasicGrowthModifiers() => new sbyte[]
        {
            DiffGrowHp, DiffGrowStr, DiffGrowTech, DiffGrowQuick, DiffGrowLuck, DiffGrowDef, DiffGrowMagic, DiffGrowMdef,
        };

        internal void SetGrowthModifiers(sbyte[] values)
        {
            DiffGrowHp = values[0];
            DiffGrowStr = values[1];
            DiffGrowTech = values[2];
            DiffGrowQuick = values[3];
            DiffGrowLuck = values[4];
            DiffGrowDef = values[5];
            DiffGrowMagic = values[6];
            DiffGrowMdef = values[7];
            DiffGrowPhys = values[8];
            DiffGrowSight = values[9];
            DiffGrowMove = values[10];
        }

        internal void SetBasicGrowthModifiers(sbyte[] values)
        {
            DiffGrowHp = values[0];
            DiffGrowStr = values[1];
            DiffGrowTech = values[2];
            DiffGrowQuick = values[3];
            DiffGrowLuck = values[4];
            DiffGrowDef = values[5];
            DiffGrowMagic = values[6];
            DiffGrowMdef = values[7];
        }

        internal bool HasGrowthModifiers() => GetGrowthModifiers().Any(i8 => i8 != 0);

        internal List<int> GetAttributes()
        {
            List<int> l = new();
            for (int i = 0; i < 32; i++)
                if ((Attrs & (1 << i)) > 0)
                    l.Add(i);
            return l;
        }

        internal void SetAttributes(List<int> l)
        {
            Attrs = 0;
            for (int i = 0; i < l.Count; i++)
                Attrs |= (uint)(1 << l[i]);
        }

        internal bool GetAttribute(int index) => (Attrs & (1 << index)) > 0;

        internal void SetAttribute(int index, bool value)
        {
            if (value ^ GetAttribute(index))
                Attrs ^= (byte)(1 << index);
        }
    }

    internal class FightingStyle : DataParam
    {
        internal string Out { get; set; }
        internal string Style { get; set; }
        internal string Name { get; set; }
        internal string Help { get; set; }
        internal string[] Skills { get; set; }
    }
}
