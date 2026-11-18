using Student_Book_Managment.DataHandlers;
using Student_Book_Managment.Exceptions;
using Student_Managment.Models;

namespace Student_Book_Managment.Services.StudentsService
{
    static class StudentManager
    {
        private static readonly List<Student> studentsList = new List<Student>();
        private static readonly SortedList<char, List<Student>> BestGradeToStudentsMap = new SortedList<char, List<Student>>();
        private static readonly Dictionary<int, List<Student>> RollToStudentsMap = new Dictionary<int, List<Student>>();
        private static readonly Dictionary<int, Student> IdToStudentMap = new Dictionary<int, Student>();
        private static readonly string FilePath = @"C:\Users\user\Desktop\Backend\Projects\Student&BookManagment\Student_Book_Managment\JsonFolders\Students.json";
        public static async Task InitializeAsync()
        {
            StudentStorageData data = await JsonFIleStorage.LoadAsync<StudentStorageData>(FilePath);

            Student.Count = data.IdCount;

            foreach (Student s in data.Students)
            {
                AddToCollections(s);
            }
        }

        private async static void SaveAllAsync() => await JsonFIleStorage.SaveAsync(new StudentStorageData(studentsList, Student.Count), FilePath);

        private static void AddToCollections(Student s)
        {
            if (!BestGradeToStudentsMap.TryGetValue(s.Grade, out List<Student>? gradeStudents))
            {
                gradeStudents = new List<Student>();
                BestGradeToStudentsMap[s.Grade] = gradeStudents;
            }

            if (!RollToStudentsMap.TryGetValue(s.RollNum, out List<Student>? rollStudents))
            {
                rollStudents = new List<Student>();
                RollToStudentsMap[s.RollNum] = rollStudents;
            }

            gradeStudents.Add(s);
            rollStudents.Add(s);
            studentsList.Add(s);
            IdToStudentMap.Add(s.Id, s);
        }

        public static void ModifyGrade(Student student, char grade)
        {
            if (Student.IsValidGrade(grade))
            {
                student.Grade = grade;
            }
            else
            {
                throw new GradeInvalidException();
            }
        }

        public static void DisplayAllStudents()
        {
            if (studentsList.Count == 0)
            {
                Console.WriteLine("\nThere are no students.");
            }
            else
            {
                Console.WriteLine("\n[STUDENTS]\n");

                foreach (Student student in studentsList)
                {
                    Console.WriteLine(student);
                }
            }
        }

        public static void DisplayStudentsByGrade()
        {
            if (BestGradeToStudentsMap.Count == 0)
            {
                Console.WriteLine("\nThere are no students.");
            }
            else
            {
                Console.WriteLine("\n[STUDENTS BY GRADE]\n");

                foreach ((char grade, List<Student> students) in BestGradeToStudentsMap)
                {
                    foreach (Student student in students)
                    {
                        Console.WriteLine($"Grade: [{grade}], Student: {student}");
                    }
                }
            }
        }

        public static bool FindStudentsByRollNum(int rollNum, out List<Student>? foundStudents)
        {
            bool rollFound = RollToStudentsMap.TryGetValue(rollNum, out List<Student>? foundList);
            foundStudents = null;

            if (rollFound)
            {
                if (foundList?.Count == 0)
                {
                    return false;
                }
                else
                {
                    foundStudents = foundList;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IdIsValid(int id) => IdToStudentMap.ContainsKey(id);

        public static bool FindStudentById(int id, out Student? foundStudent)
        {
            return IdToStudentMap.TryGetValue(id, out foundStudent);
        }

        private static bool StudentExists(Student student) => IdToStudentMap.ContainsKey(student.Id);

        public static bool AddStudent(Student student)
        {
            if (StudentExists(student))
            {
                return false;
            }
            else
            {
                AddToCollections(student);
                SaveAllAsync();
                return true;
            }
        }

        public static bool RemoveStudent(Student student)
        {
            if (StudentExists(student))
            {
                BestGradeToStudentsMap.TryGetValue(student.Grade, out List<Student>? students);
                students?.Remove(student);
                studentsList.Remove(student);
                IdToStudentMap.Remove(student.Id);
                RollToStudentsMap[student.RollNum].Remove(student);
                SaveAllAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveAllStudents()
        {
            if (studentsList.Count > 0)
            {
                studentsList.Clear();
                IdToStudentMap.Clear();
                BestGradeToStudentsMap.Clear();
                RollToStudentsMap.Clear();
                SaveAllAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
