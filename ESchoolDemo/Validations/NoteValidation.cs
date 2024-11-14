using ESchoolDemo.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.Validations
{
    public class NoteValidation : AbstractValidator<NoteClass>
    {
        public NoteValidation()
        {
            RuleFor(x => x.Exam1).NotNull().WithMessage("1. Exam cannot be left blank and must consist of numbers.");
            RuleFor(x => x.Exam1).ExclusiveBetween(0, 100).WithMessage("1. The exam must be between 0-100");
            RuleFor(x => x.Exam2).NotNull().WithMessage("2. Exam cannot be left blank and must consist of numbers.");
            RuleFor(x => x.Exam2).ExclusiveBetween(0, 100).WithMessage("2. The exam must be between 0-100");
            RuleFor(x => x.Performance1).NotNull().WithMessage("1. Performance cannot be left blank and must consist of numbers.");
            RuleFor(x => x.Performance1).ExclusiveBetween(0, 100).WithMessage("1. The performance must be between 0-100");
            RuleFor(x => x.Performance2).NotNull().WithMessage("2. Performance cannot be left blank and must consist of numbers.");
            RuleFor(x => x.Performance2).ExclusiveBetween(0, 100).WithMessage("1. The performance must be between 0-100");
        }
    }
}
