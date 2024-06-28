using MassTransit;
using MediatR;
using Students.BuildingBlock.Evetns;
using Students.Reporting.Application.Models;
using Students.Reporting.Features.Create;

namespace Students.Reporting.Consumers
{
    public class StudentViewConsumer(ISender sender) : IConsumer<StudentViewEvent>
    {
        public async Task Consume(ConsumeContext<StudentViewEvent> context)
        {
            await sender.Send(new CreateStudentReportCommand(new StudentReportModel
            {
                DateOnUtc = context.Message.ViewedOnUtc,
                StudentId = context.Message.StudentId,
                Type = "View"
            }));
        }
    }
}
