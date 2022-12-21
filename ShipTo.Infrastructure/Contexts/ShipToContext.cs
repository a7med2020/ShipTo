using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.Contexts
{
    public class ShipToContext : IdentityDbContext
    {
        public ShipToContext(DbContextOptions<ShipToContext> options) : base(options)
        {

        }

        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<ShippingOrderColumnInfo> ShippingOrderColumnInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            builder.Entity<Shipper>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Shipper>().HasIndex(x => x.Phone).IsUnique();
            builder.Entity<Shipper>().HasIndex(x => x.Email).IsUnique();
        }


    }


}
