using Microsoft.EntityFrameworkCore;
using Danfoss.Entities;

namespace Danfoss.DataLayer
{
    public class DanfossDbContext : DbContext
    {
        public DanfossDbContext() : base() { }
        public DanfossDbContext(DbContextOptions<DanfossDbContext> options) : base(options) { }
        public DbSet<Counter> Counter { get; set; }
        public DbSet<House> House { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>()
                .HasOne(c => c.Counter)
                .WithOne(h => h.House)
                .HasForeignKey<Counter>(c => c.HouseId);
            modelBuilder.Entity<Counter>();
        }
    }
}
