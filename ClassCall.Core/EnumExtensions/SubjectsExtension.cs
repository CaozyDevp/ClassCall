using ClassCall.Core.Enums;

namespace ClassCall.Core.EnumExtensions
{
    public static class SubjectsExtension
    {
        public static string GetName(Subjects subject)
        {
            switch (subject)
            {
                case Subjects.None:
                    return "未知";
                case Subjects.Chinese:
                    return "语文";
                case Subjects.Math:
                    return "数学";
                case Subjects.English:
                    return "英语";
                case Subjects.Physics:
                    return "物理";
                case Subjects.Chemistry:
                    return "化学";
                case Subjects.Biology:
                    return "生物";
                case Subjects.Politics:
                    return "政治";
                case Subjects.History:
                    return "历史";
                case Subjects.Geography:
                    return "地理";
                case Subjects.Music:
                    return "音乐";
                case Subjects.Art:
                    return "美术";
                case Subjects.PE:
                    return "体育";
                case Subjects.IT:
                    return "信息技术";
                case Subjects.GT:
                    return "通用技术";
                case Subjects.Psychology:
                    return "心理";
                case Subjects.Others:
                    return "其他";
                default:
                    return "未知";
            }
        }

        public static Subjects? GetSubject(string subject)
        {
            switch (subject)
            {
                case "未知":
                    return Subjects.None;
                case "语文":
                    return Subjects.Chinese;
                case "数学":
                    return Subjects.Math;
                case "英语":
                    return Subjects.English;
                case "物理":
                    return Subjects.Physics;
                case "化学":
                    return Subjects.Chemistry;
                case "生物":
                    return Subjects.Biology;
                case "政治":
                    return Subjects.Politics;
                case "历史":
                    return Subjects.History;
                case "地理":
                    return Subjects.Geography;
                case "音乐":
                    return Subjects.Music;
                case "美术":
                    return Subjects.Art;
                case "体育":
                    return Subjects.PE;
                case "信息技术":
                    return Subjects.IT;
                case "通用技术":
                    return Subjects.GT;
                case "心理":
                    return Subjects.Psychology;
                case "其他":
                    return Subjects.Others;
                default:
                    return null;
            }
        }
    }
}