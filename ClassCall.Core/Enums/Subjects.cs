using System.ComponentModel;

namespace ClassCall.Core.Enums
{
    public enum Subjects
    {
        [Description("未知")]
        None = 0,
        [Description("语文")]
        Chinese,
        [Description("数学")]
        Math,
        [Description("英语")]
        English,
        [Description("物理")]
        Physics,
        [Description("化学")]
        Chemistry,
        [Description("生物")]
        Biology,
        [Description("政治")]
        Politics,
        [Description("历史")]
        History,
        [Description("地理")]
        Geography,
        [Description("音乐")]
        Music,
        [Description("美术")]
        Art,
        [Description("体育")]
        PE,
        [Description("信息技术")]
        IT,
        [Description("通用技术")]
        GT,
        [Description("心理")]
        Psychology,
        [Description("其他")]
        Others
    }
}
