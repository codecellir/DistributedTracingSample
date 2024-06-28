using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Students.BuildingBlock;
using Students.Reporting.Application.Models;
using Students.Reporting.Persistence;

namespace Students.Reporting.Features.Get;

public record GetStudentReportQuery(int StudentId) : IRequest<Result<IEnumerable<StudentReportModel>>>;

internal sealed class GetStudentQueryHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<GetStudentReportQuery, Result<IEnumerable<StudentReportModel>>>
{
    public async Task<Result<IEnumerable<StudentReportModel>>> Handle(GetStudentReportQuery request, CancellationToken cancellationToken)
    {
        var student = await dbContext.Reports.AsNoTracking().Where(d => d.StudentId == request.StudentId).ToListAsync();

        var model = mapper.Map<IEnumerable<StudentReportModel>>(student);

        return Result<IEnumerable<StudentReportModel>>.Success(model);
    }
}
