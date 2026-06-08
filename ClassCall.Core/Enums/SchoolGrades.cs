using System.ComponentModel;

namespace ClassCall.Core.Enums
{
    public enum SchoolGrades : byte
    {
        [Description("初一")]
        Junior1 = 11,
        [Description("初二")]
        Junior2 = 12,
        [Description("初三")]
        Junior3 = 13,
        [Description("高一")]
        Senior1 = 14,
        [Description("高二")]
        Senior2 = 15,
        [Description("高三")]
        Senior3 = 16,
    }
}
