using LanguageSchoolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LanguageSchoolApi.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Matriculate> Matriculates { get; set; }
    }
}
