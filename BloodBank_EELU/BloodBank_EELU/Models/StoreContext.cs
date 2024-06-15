using Microsoft.EntityFrameworkCore;

namespace BloodBank_EELU.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options) { }

        DbSet<Hospital> hospitals { get; set; }
        DbSet<AppUser> appusers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configure relationships
        //    modelBuilder.Entity<Hospital>()
        //        .HasOne(h => h.AppUser)
        //        .WithMany()
        //        .HasForeignKey(h => h.NtionalID)
        //        .IsRequired();
        //}
    }
}
