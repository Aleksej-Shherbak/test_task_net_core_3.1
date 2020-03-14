using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Job> Jobs { get; set; } 
        
        public ApplicationDbContext(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("OurDatabase");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // relations are here ... 
        }
    }
}