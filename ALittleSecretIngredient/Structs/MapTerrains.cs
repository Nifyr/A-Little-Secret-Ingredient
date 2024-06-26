﻿#pragma warning disable CS8618
#pragma warning disable IDE1006 // Naming Styles

namespace ALittleSecretIngredient.Structs
{
    internal class MapTerrain : DataParam
    {
        internal GameObject m_GameObject { get; set; }
        internal byte m_Enabled { get; set; }
        internal MonoScript m_Script { get; set; }
        internal string m_Name { get; set; }
        internal int m_X { get; set; }
        internal int m_Z { get; set; }
        internal int m_Width { get; set; }
        internal int m_Height { get; set; }
        internal LayerData[] m_Layers { get; set; }
        internal OverlapData[] m_Overlaps { get; set; }
        internal string[] m_Terrains { get; set; }

        internal List<string> GetTerrain(sbyte x, sbyte y)
        {
            List<string> terrains = new() {  m_Terrains[y * 32 + x] };
            foreach (LayerData ld in m_Layers)
                if (((int)x).Between(ld.X - 1, ld.X + ld.W) && ((int)y).Between(ld.Y - 1, ld.Y + ld.H))
                    terrains.Add(ld.Attr);
            return terrains;
        }
    }

    internal class GameObject
    {
        internal int m_FileID { get; set; }
        internal long m_PathID { get; set; }
    }

    internal class MonoScript
    {
        internal int m_FileID { get; set; }
        internal long m_PathID { get; set; }
    }

    internal class LayerData
    {
        internal byte X { get; set; }
        internal byte Y { get; set; }
        internal byte W { get; set; }
        internal byte H { get; set; }
        internal int Group { get; set; }
        internal string Attr { get; set; }
    }

    internal class OverlapData
    {
        internal byte X { get; set; }
        internal byte Y { get; set; }
        internal string Attr { get; set; }
    }
}
