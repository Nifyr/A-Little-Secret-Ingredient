#pragma warning disable CS8618

namespace ALittleSecretIngredient.Structs
{

    internal class Command : GroupedParam
    {
        public Command() { }
        internal Command(sbyte active, sbyte code, sbyte mind, string strValue0, string strValue1)
        {
            Group = ""; Active = active; Code = code; Mind = mind; StrValue0 = strValue0; StrValue1 = strValue1; Trans = -128;
        }
        internal string Group { get; set; }
        internal sbyte Active { get; set; }
        internal sbyte Code { get; set; }
        internal sbyte Mind { get; set; }
        internal string StrValue0 { get; set; }
        internal string StrValue1 { get; set; }
        internal sbyte Trans { get; set; }

        internal override string? GetGroupName()
        {
            return Group == "" ? null : Group;
        }
    }
}
