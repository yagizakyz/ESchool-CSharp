using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.Models
{
    public class NoteClass
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? Exam1 { get; set; }
        public int? Exam2 { get; set; }
        public int? Performance1 { get; set; }
        public int? Performance2 { get; set; }
        public double? Average { get; set; }
        public bool Situation { get; set; }

        public void CalculateAverage()
        {
            Average = (Exam1 + Exam2 + Performance1 + Performance2) / 4.0;
            Situation = Average > 50 ? true : false;
        }
    }
}
