using Common.Model;
using ManagerServer.Database.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Database
{
    public class ManagerDbContext: IdentityDbContext<AppUser>
    {
        public ManagerDbContext(DbContextOptions<ManagerDbContext> options): base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppUser>().HasOne(x => x.SmallHolding).WithMany(x => x.AppUsersList).HasForeignKey(x => x.SmallholdingId);
        //    modelBuilder.Entity<FarmEntity>().HasOne(x => x.Owner).WithMany(x => x.Farms).HasForeignKey(x => x.OwnerId);
        //    modelBuilder.Entity<SmallHoldingEntity>().HasOne(x => x.Farm).WithMany(x => x.SmallHoldings).HasForeignKey(x => x.FarmId);

        //}
        public DbSet<DataProcessingEntity> DataProcessingEntites { get; set; }
        public DbSet<FarmEntity> FarmEntities { get; set; }
        public DbSet<SmallHoldingEntity> SmallHoldingEntities { get; set; }
    }
}
