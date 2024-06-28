using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Students.Api.Application.Models;
using Students.Api.Persistence;
using Students.BuildingBlock;

namespace Students.Api.Features.List;

public record GetStudentListQuery : IRequest<Result<IEnumerable<StudentModel>>>;

internal sealed class GetStudentListQueryHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<GetStudentListQuery, Result<IEnumerable<StudentModel>>>
{
    public async Task<Result<IEnumerable<StudentModel>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var students = await dbContext.Students.AsNoTracking().ToListAsync();

        var models = mapper.Map<IEnumerable<StudentModel>>(students);

        return Result<IEnumerable<StudentModel>>.Success(models);
    }
}
