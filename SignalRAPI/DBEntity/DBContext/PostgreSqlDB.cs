using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DBEntity.Entities;
using DBEntity.EntityConfigs;

namespace DBEntity.DBContext
{
    public class PostgreSqlDB:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Address> Addresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=127.0.0.1;Port=5432;Database=postgres;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDB).Assembly);
        }
    }
}
