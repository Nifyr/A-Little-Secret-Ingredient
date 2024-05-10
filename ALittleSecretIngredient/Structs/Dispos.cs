#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{

    internal class Arrangement : GroupedParam, ICloneable
    {
        internal string Group { get; set; }
        internal string Pid { get; set; }
        internal sbyte Force { get; set; }
        internal ushort Flag { get; set; }
        internal sbyte AppearX { get; set; }
        internal sbyte AppearY { get; set; }
        internal sbyte DisposX { get; set; }
        internal sbyte DisposY { get; set; }
        internal sbyte Direction { get; set; }
        internal byte LevelN { get; set; }
        internal byte LevelH { get; set; }
        internal byte LevelL { get; set; }
        internal string Jid { get; set; }
        internal string Item1Iid { get; set; }
        internal sbyte Item1Drop { get; set; }
        internal string Item2Iid { get; set; }
        internal sbyte Item2Drop { get; set; }
        internal string Item3Iid { get; set; }
        internal sbyte Item3Drop { get; set; }
        internal string Item4Iid { get; set; }
        internal sbyte Item4Drop { get; set; }
        internal string Item5Iid { get; set; }
        internal sbyte Item5Drop { get; set; }
        internal string Item6Iid { get; set; }
        internal sbyte Item6Drop { get; set; }
        internal string Sid { get; set; }
        internal string Bid { get; set; }
        internal string Gid { get; set; }
        internal byte HpStockCount { get; set; }
        internal sbyte State0 { get; set; }
        internal sbyte State1 { get; set; }
        internal sbyte State2 { get; set; }
        internal sbyte State3 { get; set; }
        internal sbyte State4 { get; set; }
        internal sbyte State5 { get; set; }

        // AI_AC_Everytime
        // AI_AC_TurnAttackRange
        // AI_AC_AttackRange
        // AI_AC_BandRangeExcludeSelf
        // AI_AC_Turn
        // AI_AC_Null
        // AI_AC_InterferenceRange
        // AI_AC_HealRange
        // AI_AC_FlagTrueBandRange
        // AI_AC_FlagTrueAttackRange
        // AI_AC_BandRange
        // AI_AC_FlagTrue
        // AI_AC_FlagTrueTurnAttackRange
        // AI_MI_GuardPerson
        // AI_AC_InterferenceRangeAttackRange
        // AI_AC_TurnInterferenceRange
        // AI_AC_TurnBandRange
        internal string AI_ActionName { get; set; }
        internal string AI_ActionVal { get; set; }

        // AI_MI_Null
        // AI_MI_Treasure
        // AI_AC_InterferenceRange
        // AI_MI_Village
        // AI_MI_TorchAlways
        // AI_MI_BreakDown
        // AI_MI_Torch
        // AI_MI_Escape
        // AI_MI_GuardPerson
        internal string AI_MindName { get; set; }
        internal string AI_MindVal { get; set; }

        // AI_AT_Attack
        // AI_AT_EngageBlessPerson
        // AI_AT_EngageWait
        // AI_AT_Enchant
        // AI_AT_Null
        // AI_AT_MageCannonFullBullet
        // AI_AT_EngageCamilla
        // AI_AT_Heal
        // AI_AT_ExcludePerson2
        // AI_AT_Hero
        // AI_AT_ExcludePerson
        // AI_AT_EngageAttack
        // AI_AT_HealToAttack
        // AI_AT_EngageWaitGaze
        // AI_AT_Interference
        // AI_AT_EngageMagicShield
        // AI_AT_AttackToHeal
        // AI_AT_EngageCSYell
        // AI_AT_ForceOnly_Hero
        // AI_AT_Person
        // AI_MV_Person
        // AI_AT_AttackHealHigh
        // AI_AT_EngageVision
        // AI_AT_AttackToInterference
        // AI_AT_ForceOnly
        // AI_AT_Force
        // AI_AT_Idle
        // AI_AT_EngagePierce
        // AI_AT_EngageAttackNoGuard
        // AI_AT_EngageCSBattle
        // AI_AT_RodWarp
        // AI_AT_AttackToHealForceOnly
        // AI_AT_HealNearingHero
        // AI_AT_EngageDance
        // AI_AT_EngageOverlap
        internal string AI_AttackName { get; set; }
        internal string AI_AttackVal { get; set; }

        // AI_MV_WeakEnemy
        // AI_MV_Person
        // AI_MV_TreasureToEscape
        // AI_MV_Position
        // AI_MV_Hero
        // AI_MV_NearestEnemyExcludePerson2
        // AI_MV_Null
        // AI_MV_NearestEnemyExcludePerson
        // AI_AT_Interference
        // AI_MV_Force
        // AI_MV_VillageToAttack
        // AI_MV_NearestEnemy
        // AI_MV_NearestHeal
        // AI_MV_TerrainDestroy
        // AI_MV_BreakDown
        // AI_MV_Retreat
        // AI_MV_NearestFriend
        // AI_MV_HeroOnly
        // AI_MV_Escape
        internal string AI_MoveName { get; set; }
        internal string AI_MoveVal { get; set; }

        // 突撃
        // 慎重
        // 攻撃
        internal string AI_BattleRate { get; set; }
        internal byte AI_Priority { get; set; }
        internal sbyte AI_HealRateA { get; set; }
        internal sbyte AI_HealRateB { get; set; }
        internal uint AI_BandNo { get; set; }
        internal string AI_MoveLimit { get; set; }
        internal uint AI_Flag { get; set; }

        public object Clone() => MemberwiseClone();

        // 0: Normal
        // 1: Hard
        // 2: Lunatic
        // 3: Create
        // 4: Leader
        // 5: NotMove
        // 6: Edge
        // 7: Pos
        // 8: Must
        // 9: Fix
        // 10: Guest
        internal bool GetFlag(int index) => (Flag & (1 << index)) > 0;
        internal void SetFlag(int index, bool value)
        {
            if (value ^ GetFlag(index))
                Flag ^= (ushort)(1 << index);
        }

        internal List<ArrangementItem> GetItems()
        {
            List<ArrangementItem> ais = new();
            if (Item1Iid.Length > 0)
                ais.Add(new(Item1Iid, Item1Drop != 0));
            if (Item2Iid.Length > 0)
                ais.Add(new(Item2Iid, Item2Drop != 0));
            if (Item3Iid.Length > 0)
                ais.Add(new(Item3Iid, Item3Drop != 0));
            if (Item4Iid.Length > 0)
                ais.Add(new(Item4Iid, Item4Drop != 0));
            if (Item5Iid.Length > 0)
                ais.Add(new(Item5Iid, Item5Drop != 0));
            if (Item6Iid.Length > 0)
                ais.Add(new(Item6Iid, Item6Drop != 0));
            return ais;
        }

        internal void SetItems(List<ArrangementItem> ais)
        {
            (Item1Iid, Item1Drop) = ais.Count > 0 ? (ais[0].iid, ais[0].drop ? (sbyte)1 : (sbyte)0) : ("", (sbyte)0);
            (Item2Iid, Item2Drop) = ais.Count > 1 ? (ais[1].iid, ais[1].drop ? (sbyte)1 : (sbyte)0) : ("", (sbyte)0);
            (Item3Iid, Item3Drop) = ais.Count > 2 ? (ais[2].iid, ais[2].drop ? (sbyte)1 : (sbyte)0) : ("", (sbyte)0);
            (Item4Iid, Item4Drop) = ais.Count > 3 ? (ais[3].iid, ais[3].drop ? (sbyte)1 : (sbyte)0) : ("", (sbyte)0);
            (Item5Iid, Item5Drop) = ais.Count > 4 ? (ais[4].iid, ais[4].drop ? (sbyte)1 : (sbyte)0) : ("", (sbyte)0);
            (Item6Iid, Item6Drop) = ais.Count > 5 ? (ais[5].iid, ais[5].drop ? (sbyte)1 : (sbyte)0) : ("", (sbyte)0);
        }

        // 0: NotActivateByAttacked
        // 1: Dummy
        // 2: ZeroAttack
        // 3: Heal
        // 4: Break
        // 5: Chain
        // 6: EquipShortAfterLongRange
        // 7: MoveBreak
        // 8: EngageAttackOnce

        internal bool GetAIFlag(int index) => (AI_Flag & (1 << index)) > 0;
        internal void SetAIFlag(int index, bool value)
        {
            if (value ^ GetFlag(index))
                AI_Flag ^= (uint)(1 << index);
        }

        internal override string? GetGroupName()
        {
            return Group == "" ? null : Group;
        }
    }

    internal class GArrangement : Arrangement
    {
        internal byte LevelMin { get; set; }
        internal byte LevelMax { get; set; }
    }

    internal class ArrangementItem
    {
        internal ArrangementItem(string iid, bool drop)
        {
            this.iid = iid;
            this.drop = drop;
        }

        internal string iid;
        internal bool drop;
    }
}
