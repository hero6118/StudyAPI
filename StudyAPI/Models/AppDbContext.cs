using Microsoft.EntityFrameworkCore;

namespace StudyAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
    }
}
