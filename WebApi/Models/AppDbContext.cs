using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CourseDetails> CourseDetails { get; set; }

    }
}
