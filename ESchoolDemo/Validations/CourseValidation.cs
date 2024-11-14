using ESchoolDemo.Models;
using FluentValidation;

namespace ESchoolDemo.Validations
{
    public class CourseValidation : AbstractValidator<CourseClass>
    {
        public CourseValidation()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CourseCode).NotEmpty().WithMessage("Course Code Not Empty");
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("Course Name Not Empty");

            RuleFor(x => x.CourseCode).Length(5).WithMessage("Course Code minimum and maximum 5 character");
            RuleFor(x => x.CourseName).MinimumLength(4).WithMessage("Course Name Not minimum 4 character");
        }
    }
}
