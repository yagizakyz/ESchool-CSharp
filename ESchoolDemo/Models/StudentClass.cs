using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.Models
{
    public class StudentClass
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public int? SchoolNumber { get; set; }
        public override string ToString()
        {
            return $"{SchoolNumber} : {StudentName} {StudentSurname}";
        }
    }
}
