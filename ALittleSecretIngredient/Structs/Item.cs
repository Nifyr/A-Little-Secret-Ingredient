#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{
    internal class Item : DataParam
    {
        internal string Out { get; set; }
        internal string Iid { get; set; }
        internal string Name { get; set; }
        internal string Help { get; set; }
        internal string Tutorial { get; set; }
        internal string Aid { get; set; }
        internal sbyte Kind { get; set; }
        internal sbyte UseType { get; set; }
        internal sbyte WeaponAttr { get; set; }
        internal string Icon { get; set; }
        internal byte Endurance { get; set; }
        internal byte Power { get; set; }
        internal byte Weight { get; set; }
        internal byte RangeI { get; set; }
        internal byte RangeO { get; set; }
        internal byte Distance { get; set; }
        internal short Hit { get; set; }
        internal short Critical { get; set; }
        internal short Avoid { get; set; }
        internal short Secure { get; set; }
        internal sbyte EnhanceHp { get; set; }
        internal sbyte EnhanceStr { get; set; }
        internal sbyte EnhanceTech { get; set; }
        internal sbyte EnhanceQuick { get; set; }
        internal sbyte EnhanceLuck { get; set; }
        internal sbyte EnhanceDef { get; set; }
        internal sbyte EnhanceMagic { get; set; }
        internal sbyte EnhanceMdef { get; set; }
        internal sbyte EnhancePhys { get; set; }
        internal sbyte EnhanceMove { get; set; }
        internal sbyte GrowRatioHp { get; set; }
        internal sbyte GrowRatioStr { get; set; }
        internal sbyte GrowRatioTech { get; set; }
        internal sbyte GrowRatioQuick { get; set; }
        internal sbyte GrowRatioLuck { get; set; }
        internal sbyte GrowRatioDef { get; set; }
        internal sbyte GrowRatioMagic { get; set; }
        internal sbyte GrowRatioMdef { get; set; }
        internal sbyte GrowRatioPhys { get; set; }
        internal sbyte GrowRatioMove { get; set; }
        internal int Price { get; set; }
        internal string WeaponLevel { get; set; }
        internal sbyte RodType { get; set; }
        internal byte RodExp { get; set; }
        internal byte RateArena { get; set; }
        internal string ShootEffect { get; set; }
        internal string HitEffect { get; set; }
        internal string CannonEffect { get; set; }
        internal sbyte AttackMotion { get; set; }
        internal string OverlapTerrain { get; set; }
        internal string EquipCondition { get; set; }
        internal uint Flag { get; set; }
        internal string[] EquipSids { get; set; }
        internal string[] PassiveSids { get; set; }
        internal string[] GiveSids { get; set; }
        internal sbyte AddTarget { get; set; }
        internal byte AddRange { get; set; }
        internal sbyte AddType { get; set; }
        internal byte AddPower { get; set; }
        internal string[] AddSids { get; set; }
        internal string AddEffect { get; set; }
        internal string AddHelp { get; set; }
        internal string HighRankItem { get; set; }
    }

    internal class ItemCategory : DataParam
    {
        internal string Out { get; set; }
        internal string Category { get; set; }
        internal string Help { get; set; }
    }

    internal class Alchemy : GroupedParam
    {
        internal string Out { get; set; }
        internal string Rid { get; set; }
        internal ushort Iron { get; set; }
        internal ushort Steel { get; set; }
        internal ushort Silver { get; set; }
        internal ushort Price { get; set; }
        internal sbyte Power { get; set; }
        internal sbyte Weight { get; set; }
        internal sbyte Hit { get; set; }
        internal sbyte Critical { get; set; }

        internal override string? GetGroupName()
        {
            return Rid == "" ? null : Rid;
        }
    }

    internal class Evolution : GroupedParam
    {
        internal string Out { get; set; }
        internal string Eid { get; set; }
        internal string Iid { get; set; }
        internal ushort Iron { get; set; }
        internal ushort Steel { get; set; }
        internal ushort Silver { get; set; }
        internal ushort Price { get; set; }
        internal byte RefineLevel { get; set; }

        internal override string? GetGroupName()
        {
            return Eid == "" ? null : Eid;
        }
    }

    internal class RefiningMaterialExchange : DataParam
    {
        internal string Out { get; set; }
        internal string Operation { get; set; }
        internal string Name { get; set; }
        internal string Icon { get; set; }
        internal ushort ToIron { get; set; }
        internal ushort ToSteel { get; set; }
        internal ushort ToSilver { get; set; }
        internal ushort ForIron { get; set; }
        internal ushort ForSteel { get; set; }
        internal ushort ForSilver { get; set; }
    }

    internal class WeaponLevel : DataParam
    {
        internal string Level { get; set; }
        internal byte Exp { get; set; }
        internal byte Mastery { get; set; }
        internal byte Attack { get; set; }
        internal byte Hit { get; set; }
        internal byte Critical { get; set; }
        internal byte Recover { get; set; }
    }

    internal class Compatibility : DataParam
    {
        internal string Kind { get; set; }
        internal uint Flag { get; set; }
    }

    internal class Accessory : DataParam
    {
        internal string Out { get; set; }
        internal string Aid { get; set; }
        internal string Name { get; set; }
        internal string Help { get; set; }
        internal string NameM { get; set; }
        internal string HelpM { get; set; }
        internal string NameF { get; set; }
        internal string HelpF { get; set; }
        internal bool First { get; set; }
        internal bool Amiibo { get; set; }
        internal string Asset { get; set; }
        internal string CondtionCid { get; set; }
        internal string[] CondtionSkills { get; set; }
        internal sbyte CondtionGender { get; set; }
        internal string Gid { get; set; }
        internal int Price { get; set; }
        internal int Iron { get; set; }
        internal int Steel { get; set; }
        internal int Silver { get; set; }
        internal uint Mask { get; set; }
    }

    internal class Gift : DataParam
    {
        internal string Name { get; set; }
        internal sbyte V00 { get; set; }
        internal sbyte V01 { get; set; }
        internal sbyte V02 { get; set; }
        internal sbyte V03 { get; set; }
        internal sbyte V04 { get; set; }
        internal sbyte V05 { get; set; }
        internal sbyte V06 { get; set; }
        internal sbyte V07 { get; set; }
        internal sbyte V08 { get; set; }
        internal sbyte V09 { get; set; }
        internal sbyte V10 { get; set; }
        internal sbyte V11 { get; set; }
        internal sbyte V12 { get; set; }
        internal sbyte V13 { get; set; }
        internal sbyte V14 { get; set; }
        internal sbyte V15 { get; set; }
        internal sbyte V16 { get; set; }
        internal sbyte V17 { get; set; }
        internal sbyte V18 { get; set; }
        internal sbyte V19 { get; set; }
        internal sbyte V20 { get; set; }
        internal sbyte V21 { get; set; }
        internal sbyte V22 { get; set; }
        internal sbyte V23 { get; set; }
        internal sbyte V24 { get; set; }
        internal sbyte V25 { get; set; }
        internal sbyte V26 { get; set; }
        internal sbyte V27 { get; set; }
        internal sbyte V28 { get; set; }
        internal sbyte V29 { get; set; }
        internal sbyte V30 { get; set; }
        internal sbyte V31 { get; set; }
        internal sbyte V32 { get; set; }
        internal sbyte V33 { get; set; }
        internal sbyte V34 { get; set; }
        internal sbyte V35 { get; set; }
        internal sbyte V36 { get; set; }
        internal sbyte V37 { get; set; }
        internal sbyte V38 { get; set; }
        internal sbyte V39 { get; set; }
        internal sbyte V40 { get; set; }
        internal sbyte V41 { get; set; }
        internal sbyte V42 { get; set; }
        internal sbyte V43 { get; set; }
        internal sbyte V44 { get; set; }
        internal sbyte V45 { get; set; }
        internal sbyte V46 { get; set; }
        internal sbyte V47 { get; set; }
        internal sbyte V48 { get; set; }
        internal sbyte V49 { get; set; }
    }

    internal class Reward : GroupedParam
    {
        internal string Group { get; set; }
        internal string Iid { get; set; }
        internal float Ratio { get; set; }
        internal float Factor { get; set; }
        internal float Min { get; set; }
        internal float Max { get; set; }
        internal bool IsShow { get; set; }
        internal string Condition { get; set; }

        internal override string? GetGroupName()
        {
            return Group == "" ? null : Group;
        }
    }

    internal class EngageWeaponEnhancement : GroupedParam
    {
        internal string Out { get; set; }
        internal string Iid { get; set; }
        internal ushort PowerMat { get; set; }
        internal ushort HitMat { get; set; }
        internal ushort CriticalMat { get; set; }
        internal ushort AvoidMat { get; set; }
        internal ushort SecureMat { get; set; }
        internal ushort TechMat { get; set; }
        internal ushort QuickMat { get; set; }
        internal ushort DefMat { get; set; }
        internal ushort MdefMat { get; set; }
        internal ushort EfficacyHorseMat { get; set; }
        internal ushort EfficacyArmorMat { get; set; }
        internal ushort EfficacyFlyMat { get; set; }
        internal ushort EfficacyDragonMat { get; set; }
        internal ushort EfficacyMorphMat { get; set; }
        internal ushort PowerCapa { get; set; }
        internal ushort HitCapa { get; set; }
        internal ushort CriticalCapa { get; set; }
        internal ushort AvoidCapa { get; set; }
        internal ushort SecureCapa { get; set; }
        internal ushort TechCapa { get; set; }
        internal ushort QuickCapa { get; set; }
        internal ushort DefCapa { get; set; }
        internal ushort MdefCapa { get; set; }
        internal ushort EfficacyHorseCapa { get; set; }
        internal ushort EfficacyArmorCapa { get; set; }
        internal ushort EfficacyFlyCapa { get; set; }
        internal ushort EfficacyDragonCapa { get; set; }
        internal ushort EfficacyMorphCapa { get; set; }
        internal sbyte Power { get; set; }
        internal sbyte Hit { get; set; }
        internal sbyte Critical { get; set; }
        internal sbyte Avoid { get; set; }
        internal sbyte Secure { get; set; }
        internal sbyte Tech { get; set; }
        internal sbyte Quick { get; set; }
        internal sbyte Def { get; set; }
        internal sbyte Mdef { get; set; }

        internal override string? GetGroupName()
        {
            return Iid == "" ? null : Iid;
        }
    }

    internal class BattleReward : DataParam
    {
        internal string TypeID { get; set; }
        internal string[] Iids { get; set; }
        internal int[] Nums { get; set; }
        internal string[] Conditions { get; set; }
    }
}
