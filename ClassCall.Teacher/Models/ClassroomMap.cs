using ClassCall.Core;
using ClassCall.Core.Enums;
using System.Collections.Generic;

namespace ClassCall.Teacher.Models
{
    internal static class ClassroomMap
    {
        /// <summary>
        /// 门牌号与年级班级的对应关系
        /// </summary>
        public static readonly Dictionary<string, ClassInfo> Classrooms = new Dictionary<string, ClassInfo>()
        {
            { "C522", new ClassInfo(SchoolGrades.Senior2, 1) },
            { "C520", new ClassInfo(SchoolGrades.Senior2, 2) },
            { "C518", new ClassInfo(SchoolGrades.Senior2, 3) },
            { "C516", new ClassInfo(SchoolGrades.Senior2, 4) },
            { "C510", new ClassInfo(SchoolGrades.Senior2, 5) },
            { "C508", new ClassInfo(SchoolGrades.Senior2, 6) },
            { "B510", new ClassInfo(SchoolGrades.Senior2, 7) },
            { "C506", new ClassInfo(SchoolGrades.Senior2, 8) },
            { "C504", new ClassInfo(SchoolGrades.Senior2, 9) },
            { "B508", new ClassInfo(SchoolGrades.Senior2, 10) },
            { "B506", new ClassInfo(SchoolGrades.Senior2, 11) },
            { "B504", new ClassInfo(SchoolGrades.Senior2, 12) },
        };

        public static ClassInfo? GetClassInfo(string address)
        {
            if (Classrooms.TryGetValue(address, out ClassInfo classInfo))
                return classInfo;
            return null;
        }

        public static string GetAddress(ClassInfo classInfo)
        {
            foreach (var classroom in Classrooms)
            {
                if (classroom.Value.Grade == classInfo.Grade &&
                    classroom.Value.ClassNumber == classInfo.ClassNumber)
                    return classroom.Key;
            }
            return null;
        }
    }
}
