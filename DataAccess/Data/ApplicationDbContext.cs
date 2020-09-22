using Microsoft.EntityFrameworkCore;
using FoodDelivery.Models;
using FoodDelivery.Models.Models;

namespace FoodDelivery.Data {
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<FoodType> FoodType {get; set;}
        public DbSet<MenuItem> MenuItem { get; set; }
    }
}
