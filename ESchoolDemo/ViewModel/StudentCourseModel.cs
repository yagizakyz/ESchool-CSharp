using ESchoolDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.ViewModel
{
    public class StudentCourseModel : NoteClass
    {
        public int SchoolNumber { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string NameSurname
        {
            get
            {
                return $"{StudentName} {StudentSurname}";
            }
        }
        public string CourseName { get; set; }
        public int ClassHour { get; set; }
    }
}
