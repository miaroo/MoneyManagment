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
        public DbSet<Saldo> Saldo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity("API.Entities.Category", b =>
                {
                    b.HasOne("API.Entities.AppUser", "AppUser")
                        .WithMany("Categories")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.OperationType", "OperationType")
                        .WithMany("Category")
                        .HasForeignKey("OperationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    });

                 modelBuilder.Entity("API.Entities.Operation", b =>
                {
                    b.HasOne("API.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.Saldo", "Saldo")
                        .WithMany("Operations")
                        .HasForeignKey("SaldoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                    });
    }



    }
}
