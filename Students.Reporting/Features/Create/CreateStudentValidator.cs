using FluentValidation;
using Students.Reporting.Application.Models;

namespace Students.Reporting.Features.Create
{
    public class CreateStudentReportValidator : AbstractValidator<CreateStudentReportCommand>
    {
        public CreateStudentReportValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.Model)
                .SetValidator(new StudentReportValidator());
        }
    }
    public class StudentReportValidator : AbstractValidator<StudentReportModel>
    {
        public StudentReportValidator()
        {

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.Type)
                   .NotEmpty()
                   .MaximumLength(10);
        }
    }
}
