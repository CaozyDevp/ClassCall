using System.Collections.Generic;

namespace ClassCall.Core.Constants
{
    public class SchoolConstants
    {
        public static IReadOnlyList<string> Subjects = new List<string>()
        {
            "语文", "数学", "英语", "物理", "化学", "生物", "历史", "地理", "政治",
            "音乐", "体育", "美术", "通用技术", "信息技术", "心理", "其他",
        };

        public static IReadOnlyList<string> Grades = new List<string>()
        {
            "初一", "初二", "初三",
            "高一", "高二", "高三",
        };

        public static IReadOnlyList<string> Classrooms = new List<string>()
        {
            "1班", "2班", "3班", "4班", "5班", "6班", "7班",
            "8班", "9班", "10班", "11班", "12班", "13班", "14班"
        };
    }
}
