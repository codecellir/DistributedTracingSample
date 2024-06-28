using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Students.Api.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("codecell");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<Student> Students => Set<Student>();
    }
}
