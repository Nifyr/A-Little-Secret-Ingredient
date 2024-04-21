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
        internal string AI_ActionName { get; set; }
        internal string AI_ActionVal { get; set; }
        internal string AI_MindName { get; set; }
        internal string AI_MindVal { get; set; }
        internal string AI_AttackName { get; set; }
        internal string AI_AttackVal { get; set; }
        internal string AI_MoveName { get; set; }
        internal string AI_MoveVal { get; set; }
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
                Flag ^= (byte)(1 << index);
        }


        internal override string? GetGroupName()
        {
            return Group == "" ? null : Group;
        }
    }
}
