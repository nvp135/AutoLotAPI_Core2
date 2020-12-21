using System;
using AutoLotDAL_Core2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AutoLotDAL_Core2.EF
{
    public class AutoLotContext : DbContext
    {
        public DbSet<CreditRisk> CreditRisks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }

        internal AutoLotContext()
        { }

        public  AutoLotContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditRisk>(entity => 
            {
                entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            });

            modelBuilder.Entity<Order>()
                .HasOne(e => e.Car)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public string GetTableName(Type type)
        {
            return Model.FindEntityType(type).GetTableName();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
