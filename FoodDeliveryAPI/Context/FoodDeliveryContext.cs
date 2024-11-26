using FoodDeliveryAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Context
{
    public class FoodDeliveryContext : DbContext
    {
        private readonly DbContextOptions _options;

        public FoodDeliveryContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<User> Users { get; set; }
    }
}
