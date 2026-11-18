using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Managment.Models
{
    internal class StudentStorageData // same thing here, a wrapper class which is a blueprint of how data should be saved in the json files.
    {
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public int IdCount { get; set; }

        public StudentStorageData() { }

        public StudentStorageData(IEnumerable<Student> students, int idCount)
        {
            Students = students;
            IdCount = idCount;
        }
    }
}
