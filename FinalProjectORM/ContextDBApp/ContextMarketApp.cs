using FinalProjectORM.Models.Admin_Panel.ModelsForAdmin;
using FinalProjectORM.Models.UserPanel.Models;
using FinalProjectORM.Models.UserPanel.ModelsForUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectORM.ContextDBApp
{
    internal class ContextMarketApp : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=STHQ0118-04;Initial Catalog=Market11;User ID=admin;Password=admin;Trust Server Certificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ShopCard> ShopCards { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopCard>().Property(p => p.Price).HasColumnType("money");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("money");
            modelBuilder.Entity<History>().Property(p => p.Price1).HasColumnType("money");

            modelBuilder.Entity<History>().HasOne(h => h.User).WithMany(u => u.Histories).HasForeignKey(h => h.UserId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
