using Carter;
using MediatR;
using Students.Api.Application.Models;
using Students.Api.Features.Create;
using Students.Api.Features.Get;
using Students.Api.Features.List;

namespace Students.Api.Features
{
    public class StudentModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/students");

            app.MapPost("create", async (StudentModel model,ISender sender) =>
            {
                var result = await sender.Send(new CreateStudentCommand(model));

                return TypedResults.Ok(result);
            });

            app.MapGet("list", async (ISender sender) =>
            {
                var result = await sender.Send(new GetStudentListQuery());

                return TypedResults.Ok(result);
            });

            app.MapGet("get/{id}", async (int id,ISender sender) =>
            {
                var result = await sender.Send(new GetStudentQuery(id));

                return TypedResults.Ok(result);
            });
        }
    }
}
