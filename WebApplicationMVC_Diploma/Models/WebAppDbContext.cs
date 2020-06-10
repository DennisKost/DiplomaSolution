using Microsoft.EntityFrameworkCore;

namespace WebApplicationMVC_Diploma.Models
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options) { }

        public DbSet<Entities.Dictionary> Dictionary { get; set; }
    }
}
