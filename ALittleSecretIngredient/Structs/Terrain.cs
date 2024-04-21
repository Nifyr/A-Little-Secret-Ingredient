#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{
    internal class Terrain : DataParam
    {
        internal string Out { get; set; }
        internal string Tid { get; set; }
        internal string Name { get; set; }
        internal string CostName { get; set; }
        internal sbyte Layer { get; set; }
        internal sbyte Prohibition { get; set; }
        internal byte Sight { get; set; }
        internal sbyte Destroyer { get; set; }
        internal byte Hp_N { get; set; }
        internal byte Hp_H { get; set; }
        internal byte Hp_L { get; set; }
        internal sbyte Defense { get; set; }
        internal sbyte Avoid { get; set; }
        internal sbyte PlayerDefense { get; set; }
        internal sbyte EnemyDefense { get; set; }
        internal sbyte PlayerAvoid { get; set; }
        internal sbyte EnemyAvoid { get; set; }
        internal sbyte Heal { get; set; }
        internal byte Life { get; set; }
        internal byte MoveCost { get; set; }
        internal byte FlyCost { get; set; }
        internal sbyte MoveFirst { get; set; }
        internal float Offset { get; set; }
        internal string PutEffect { get; set; }
        internal string Minimap { get; set; }
        internal string CannonSkill { get; set; }
        internal byte CannonShellsN { get; set; }
        internal byte CannonShellsH { get; set; }
        internal byte CannonShellsL { get; set; }
        internal string ChangeTid { get; set; }
        internal string ChangeEncount { get; set; }
        internal sbyte Command { get; set; }
        internal uint Flag { get; set; }
        internal byte PutAllow { get; set; }
        internal float Height { get; set; }
        internal byte ColorR { get; set; }
        internal byte ColorG { get; set; }
        internal byte ColorB { get; set; }
    }

    internal class TerrainCost : DataParam
    {
        internal string Out { get; set; }
        internal string Name { get; set; }
        internal byte None { get; set; }
        internal byte Foot { get; set; }
        internal byte Horse { get; set; }
        internal byte Fly { get; set; }
        internal byte Dragon { get; set; }
        internal byte Pad { get; set; }
        internal byte ColorR { get; set; }
        internal byte ColorG { get; set; }
        internal byte ColorB { get; set; }
        internal byte ColorA { get; set; }
    }
}
