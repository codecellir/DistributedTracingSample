using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Students.Api.Persistence
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(nameof(Student));

            builder.Property(d => d.FirstName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(d => d.LastName)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}
