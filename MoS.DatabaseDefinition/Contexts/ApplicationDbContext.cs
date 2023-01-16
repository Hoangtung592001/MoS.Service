﻿using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Models;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageType;
using static MoS.Models.Constants.Enums.Role;

namespace MoS.DatabaseDefinition.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = (int) RoleIDs.Admin, Name = "Admin" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = (int) RoleIDs.User, Name = "User" });
            modelBuilder.Entity<Role>().HasData(new BookImageType { Id = (int) BookImageTypeTDs.Main, Name = "Main" });
            modelBuilder.Entity<Role>().HasData(new BookImageType { Id = (int)BookImageTypeTDs.Sub, Name = "Sub" });
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCondition> BookConditions { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookImageType> BookImageTypes { get; set; }
        public DbSet<BookInformation> BookInformation { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
