using ClassCall.Core.Enums;

namespace ClassCall.Core.Configs
{
    public class TeacherConfig
    {
        /// <summary>
        /// 教师姓名
        /// </summary>
        public string TeacherName { get; set; } = string.Empty;

        /// <summary>
        /// 教师的任课科目
        /// </summary>
        public Subjects Subject { get; set; }
    }
}
