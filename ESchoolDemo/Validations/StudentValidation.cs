using ESchoolDemo.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.Validations
{
    public class StudentValidation : AbstractValidator<StudentClass>
    {
        public StudentValidation()
        {
            RuleFor(x => x.StudentName).NotEmpty().WithMessage("Student Name not empty!");
            RuleFor(x => x.StudentSurname).NotEmpty().WithMessage("Student Surname not empty!");

            RuleFor(x => x.StudentName).MinimumLength(3).WithMessage("Student Name minimum 3 character");
            RuleFor(x => x.StudentSurname).MinimumLength(2).WithMessage("Student Surname minimum 2 character");

            RuleFor(x => x.SchoolNumber).NotNull().WithMessage("School Number not null");
        }
    }
}
