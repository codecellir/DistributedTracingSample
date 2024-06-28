using Carter;
using MediatR;
using Students.Reporting.Features.Get;

namespace Students.Reporting.Features
{
    public class StudentReportModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/report");

            app.MapGet("get/{studentId}", async (int studentId, ISender sender) =>
            {
                var result = await sender.Send(new GetStudentReportQuery(studentId));

                return TypedResults.Ok(result);
            });
        }
    }
}
