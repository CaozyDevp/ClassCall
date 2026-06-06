using ClassCall.Core.Enums;
namespace ClassCall.Core
{
    public struct ClassInfo
    {
        public SchoolGrades Grade { get; set; }
        public int ClassNumber { get; set; }

        public ClassInfo(SchoolGrades grade, int classNumber)
        {
            Grade = grade;
            ClassNumber = classNumber;
        }
    }
}
