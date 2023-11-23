using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Models;

namespace Nazar1988.Areas.Identity.Data
{
    public class Nazar1988Context : IdentityDbContext<Nazar1988User,  ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public Nazar1988Context(DbContextOptions<Nazar1988Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
              base.OnModelCreating(builder);
            builder.Entity<ApplicationRole>().ToTable("AspNetRoles").ToTable("AppRoles");

            builder.Entity<ApplicationUserRole>().ToTable("AppUserRole");

            builder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users).HasForeignKey(r => r.RoleId);

            builder.Entity<Nazar1988User>().ToTable("AppUsers");

            builder.Entity<ApplicationUserRole>()
               .HasOne(userRole => userRole.User)
               .WithMany(role => role.Roles).HasForeignKey(r => r.UserId);

            //builder.Entity<Order>()
            //    .HasOne(n => n.ApplicationUser)
            //    .WithMany(a => a.Orders)
            //    .HasForeignKey(n => n.UserId);
                
        }
        #region Wallet
        public DbSet<Wallet> wallets { set; get; }
        public DbSet<WalleTType> walleTTypes { set; get; }
        #endregion 
    }
}
