using AutoMapper;
using MediatR;
using Students.BuildingBlock;
using Students.Reporting.Application.Models;
using Students.Reporting.Persistence;

namespace Students.Reporting.Features.Create;

public record CreateStudentReportCommand(StudentReportModel Model) : IRequest<Result<int>>;

internal sealed class CreateStudentReportCommandHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateStudentReportCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateStudentReportCommand request, CancellationToken cancellationToken)
    {
        var student = mapper.Map<StudentReport>(request.Model);

        dbContext.Reports.Add(student);

        await dbContext.SaveChangesAsync();

        return Result<int>.Success(student.Id);
    }
}
