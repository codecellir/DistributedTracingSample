using FluentValidation;
using Students.Api.Application.Models;

namespace Students.Api.Features.Create
{
    public class CreateStudentValidator:AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.Model)
                .SetValidator(new StudentValidator());
        }
    }
    public class StudentValidator:AbstractValidator<StudentModel>
    {
        public StudentValidator()
        {

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.FirstName)
                   .NotEmpty()
                   .MaximumLength(50);

            RuleFor(d => d.LastName)
                   .NotEmpty()
                   .MaximumLength(50);
        }
    }
}
