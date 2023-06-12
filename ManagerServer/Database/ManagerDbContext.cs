using ManagerServer.Database.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagerServer.Database
{
    public class ManagerDbContext : IdentityDbContext<AppUser>
    {
        public ManagerDbContext(DbContextOptions<ManagerDbContext> options) : base (options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppUser>().HasOne(x => x.SmallHolding).WithMany(x => x.AppUsersList).HasForeignKey(x => x.SmallholdingId);
        //    modelBuilder.Entity<FarmEntity>().HasOne(x => x.Owner).(x => x.Farms).HasForeignKey(x => x.OwnerId);
        //    modelBuilder.Entity<SmallHoldingEntity>().HasOne(x => x.Farm).WithMany(x => x.SmallHoldings).HasForeignKey(x => x.FarmId);

        //}
        public DbSet<DataEntity> DataEntities { get; set; }
        public DbSet<FarmEntity> FarmEntities { get; set; }
        public DbSet<ZoneEntity> ZoneEntities { get; set; }
        public DbSet<DeviceEntity> DeviceEntities { get; set; }
        public DbSet<DeviceActionEntity> DeviceActionEntities { get; set; }
    }
}
