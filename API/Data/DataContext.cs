using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserRole>()
                .HasKey(ur => new { ur.AppUserId, ur.AppRoleId });

            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.AppUser)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
              .HasMany(ur => ur.UserRoles)
              .WithOne(u => u.AppRole)
              .HasForeignKey(ur => ur.RoleId)
              .IsRequired();


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

            modelBuilder.Entity<Category>()
                .HasOne(a => a.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(s => s.ParentCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Operation>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Operations)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();

            modelBuilder.Entity<Operation>()
                .HasOne(a => a.BankAccount)
                .WithMany(c => c.Operations)
                .HasForeignKey(s => s.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<BankAccount>()
                .HasOne(a => a.AppUser)
                .WithMany(c => c.BankAccounts)
                .HasForeignKey(s => s.AppUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
