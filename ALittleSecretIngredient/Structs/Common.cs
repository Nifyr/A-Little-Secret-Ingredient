﻿using AssetsTools.NET;
using MsbtLib;
using System.Reflection;
using System.Xml;

namespace ALittleSecretIngredient.Structs
{
    internal class DataSet
    {
        internal DataSet(XmlNode n, Type paramType)
        {
            Params = n.SelectChildren().Select(c => DataParam.Create(c, paramType)).ToList();
            if (GroupedParams())
                Group();
        }
        internal DataSet(AssetTypeValueField atvf, Type paramType)
        {
            Params = new() { DataParam.Create(atvf, paramType) };
            if (GroupedParams())
                Group();
        }
        internal DataSet(MSBT msbt)
        {
            Params = msbt.GetTexts().Select(kvp => DataParam.Create(kvp.Key, kvp.Value)).ToList();
            if (GroupedParams())
                Group();
        }
        internal List<DataParam> Params { get; set; }
        internal DataParam Single { get => Params[0]; set => Params[0] = value; }

        private bool GroupedParams() => Params.Count > 0 && (Params[0] is GroupedParam || Params[0] is ParamGroup);

        internal int ParamCount() => GroupedParams() ? Params.Select(dp => ((ParamGroup)dp).Group.Count + 1).Sum() : Params.Count;

        private void Group()
        {
            List<ParamGroup> groups = new();
            foreach (DataParam dp in Params)
            {
                GroupedParam gp = (GroupedParam)dp;
                string? name = gp.GetGroupName();
                if (name != null)
                    groups.Add(new(gp));
                else
                    groups.Last().Group.Add(gp);
            }
            Params = groups.Cast<DataParam>().ToList();
        }

        private void UnGroup()
        {
            List<DataParam> unGrouped = new();
            foreach (DataParam dp in Params)
            {
                ParamGroup pg = (ParamGroup)dp;
                unGrouped.Add(pg.GroupMarker);
                unGrouped.AddRange(pg.Group);
            }
            Params = unGrouped;
        }

        internal void Write(XmlNode n)
        {
            if (GroupedParams())
                UnGroup();
            for (int i = 0; i < Params.Count; i++)
            {
                if (i >= n.ChildNodes.Count)
                    n.AppendChild(n.FirstChild!.Clone());
                Params[i].Write(n.ChildNodes[i]!);
            }
            while (n.ChildNodes.Count > Params.Count)
                n.RemoveChild(n.ChildNodes[^1]!);
            if (GroupedParams())
                Group();
        }

        internal void Write(AssetTypeValueField atvf)
        {
            if (GroupedParams())
                UnGroup();
            Single.Write(atvf);
            if (GroupedParams())
                Group();
        }
    }

    internal class ParamGroup : DataParam
    {
        internal string Name { get; set; }
        internal List<GroupedParam> Group { get; set; }
        internal GroupedParam GroupMarker { get; set; }

        internal ParamGroup(GroupedParam gp)
        {
            GroupMarker = gp;
            Name = GroupMarker.GetGroupName()!;
            Group = new();
        }
    }

    internal abstract class DataParam
    {
        internal static DataParam Create(XmlNode n, Type paramType)
        {
            if (paramType.Name == "Arrangement" && n.SelectAttributes().Any(xa => xa.Name == "LevelMin"))
                paramType = typeof(GArrangement);
            DataParam dp = (DataParam)paramType.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
            foreach (PropertyInfo p in paramType.GetRuntimeProperties())
                p.SetValue(dp, n.ParseTo(p));
            return dp;
        }

        internal void Write(XmlNode n)
        {
            foreach (PropertyInfo p in GetType().GetRuntimeProperties())
                n.SelectAttributes().First(a => a.Name.Replace(".", "") == p.Name).ParseFrom(p.GetValue(this)!);
        }

        internal static DataParam Create(AssetTypeValueField atvf, Type paramType)
        {
            DataParam dp = (DataParam)paramType.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
            foreach (PropertyInfo p in paramType.GetRuntimeProperties())
                p.SetValue(dp, atvf.ParseTo(p));
            return dp;
        }

        internal void Write(AssetTypeValueField atvf)
        {
            foreach (PropertyInfo p in GetType().GetRuntimeProperties())
                atvf[p.Name].ParseFrom(p.GetValue(this)!);
        }

        internal static DataParam Create(string label, MsbtEntry entry) => new MsbtMessage()
        {
            Label = label,
            Attribute = entry.Attribute,
            Value = entry.Value
        };
    }

    internal abstract class GroupedParam : DataParam
    {
        internal abstract string? GetGroupName();
    }
}
