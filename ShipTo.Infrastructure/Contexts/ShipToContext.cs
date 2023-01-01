using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShipTo.Core.Entities;
using ShipTo.Infrastructure.Extentions;

namespace ShipTo.Infrastructure.Contexts
{
    public class ShipToContext : IdentityDbContext
    {
        
        public ShipToContext(DbContextOptions<ShipToContext> options) : base(options)
        {
           
        }

        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<ShippingOrderLog> ShippingOrderLogs { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<ShippingOrderColumnInfo> ShippingOrderColumnInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.Seed();
            
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            //builder.Entity<ShippingOrder>()
            //    .HasOne(e => e.Shipper)
            //    .WithOne(e => e.OwnedBlog)
            //    .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ShippingOrder>().HasOne(p => p.Shipper).WithMany(b => b.ShippingOrders)
                .HasForeignKey(p => p.ShipperId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShippingOrder>().HasOne(p => p.Carrier).WithMany(b => b.ShippingOrders)
                .HasForeignKey(p => p.CarrierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShippingOrder>().HasOne(p => p.Carrier).WithMany(b => b.ShippingOrders)
               .HasForeignKey(p => p.CarrierId)
               .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<ShippingOrderLog>().HasOne(p => p.ShippingOrder).WithMany(b => b.ShippingOrderLogs)
            //   .HasForeignKey(p => p.ShippingOrderID)
            //   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShippingOrder>().HasOne(p => p.DeliveryStatus).WithMany(b => b.ShippingOrders)
             .HasForeignKey(p => p.DeliveryStatusId)
             .OnDelete(DeleteBehavior.NoAction);


        }


    }


}
