using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Students.Api.Application.Models;
using Students.Api.Persistence;
using Students.BuildingBlock;
using Students.BuildingBlock.Evetns;
using Students.BuildingBlock.Exceptions;

namespace Students.Api.Features.Get;

public record GetStudentQuery(int StudentId) : IRequest<Result<StudentModel>>;

internal sealed class GetStudentQueryHandler(AppDbContext dbContext, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<GetStudentQuery, Result<StudentModel>>
{
    public async Task<Result<StudentModel>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(d => d.Id == request.StudentId);

        if (student is null)
            throw new NotFoundException();

        var model = mapper.Map<StudentModel>(student);

        await publishEndpoint.Publish(new StudentViewEvent
        {
            StudentId = student.Id,
            ViewedOnUtc=DateTime.UtcNow
        });

        return Result<StudentModel>.Success(model);
    }
}
