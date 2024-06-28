using AutoMapper;
using MassTransit;
using MediatR;
using Students.Api.Application.Models;
using Students.Api.Persistence;
using Students.BuildingBlock;
using Students.BuildingBlock.Evetns;

namespace Students.Api.Features.Create;

public record CreateStudentCommand(StudentModel Model):IRequest<Result<int>>;

internal sealed class CreateStudentCommandHandler(AppDbContext dbContext,IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateStudentCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = mapper.Map<Student>(request.Model);

        dbContext.Students.Add(student);

        await dbContext.SaveChangesAsync();

        await publishEndpoint.Publish(new StudentCreateEvent
        {
            StudentId = student.Id,
            CreatedOnUtc = DateTime.UtcNow,
        });
        return Result<int>.Success(student.Id);
    }
}
