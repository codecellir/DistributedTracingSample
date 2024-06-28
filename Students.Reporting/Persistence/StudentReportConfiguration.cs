using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Students.Reporting.Persistence
{
    public class StudentReportConfiguration : IEntityTypeConfiguration<StudentReport>
    {
        public void Configure(EntityTypeBuilder<StudentReport> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(nameof(StudentReport));

            builder.Property(d => d.Type)
                .IsRequired(true)
                .HasMaxLength(10);
        }
    }
}
