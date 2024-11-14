using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.Models
{
    public class CourseClass
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int ClassHour { get; set; }

        public string CourseContent
        {
            get
            {
                return $"{CourseCode} : {CourseName}";
            }
        }

        public override string ToString()
        {
            return $"{CourseCode} : {CourseName}";
        }
    }
}
