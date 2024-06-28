using MassTransit;
using MediatR;
using Students.BuildingBlock.Evetns;
using Students.Reporting.Application.Models;
using Students.Reporting.Features.Create;

namespace Students.Reporting.Consumers
{
    public class StudentCreateConsumer(ISender sender) : IConsumer<StudentCreateEvent>
    {
        public async Task Consume(ConsumeContext<StudentCreateEvent> context)
        {
            await sender.Send(new CreateStudentReportCommand(new StudentReportModel
            {
                DateOnUtc = context.Message.CreatedOnUtc,
                StudentId = context.Message.StudentId,
                Type = "Create"
            }));
        }
    }
}
