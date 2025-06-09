using _00016168_BACKEND.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace _00016168_BACKEND.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Movie> movies { get; set; }
        public DbSet<Review> reviews { get; set; }
    }
}
