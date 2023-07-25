using CoreMVCWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMVCWebsite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Laptop", DisplayOrder = 1 },
                new Category { Id = 2, Name = "TV", DisplayOrder = 2 },
                new Category { Id = 3, Name = "AC", DisplayOrder = 3 }
                );
        }
    }
}



// update-database
// add-migration AddCategoryTableToDb
// update-database