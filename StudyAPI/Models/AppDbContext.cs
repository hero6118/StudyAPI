using Microsoft.EntityFrameworkCore;
using StudyAPI.Models.data;

namespace StudyAPI.Models
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
