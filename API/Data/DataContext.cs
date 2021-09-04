using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<OperationType> OperationType { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(a => a.AppUser)
            .WithMany(c => c.Categories)
            .HasForeignKey(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .HasOne(a => a.OperationType)
            .WithMany(c => c.Categories)
            .HasForeignKey(s => s.OperationTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Operations)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .HasOne(a => a.BankAccount)
            .WithMany(c => c.Operations)
            .HasForeignKey(s => s.BankAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();    
    }
  }
}
