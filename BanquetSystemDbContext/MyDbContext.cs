using Microsoft.EntityFrameworkCore;
using System;
using Banquet_Hall_App.Models;
using System.IO;

namespace Banquet_Hall_App.BanquetSystemDbContext
{
    public class MyDbContext : DbContext
    {
        private readonly string _connectionString;

        public MyDbContext() 
        {
            _connectionString = File.ReadAllText($"{Directory
                .GetCurrentDirectory()
                .Split("\\bin", StringSplitOptions.None)[0]}"+"\\BanquetSystemDbContext\\connectionString.txt");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString,
                    new MySqlServerVersion(new Version(8, 0, 33)));
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<OrderDish> OrderDish { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDish>()
                .HasKey(od => new { od.OrderId, od.DishId });
        }
    }
}
