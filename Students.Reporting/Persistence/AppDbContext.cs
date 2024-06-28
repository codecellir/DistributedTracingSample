using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Students.Reporting.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("codecell");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<StudentReport> Reports => Set<StudentReport>();
    }
}
