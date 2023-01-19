using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Models;
using System;
using System.Threading.Tasks;

namespace MoS.DatabaseDefinition.Contexts
{
    public interface IApplicationDbContext : IDisposable
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCondition> BookConditions { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookImageType> BookImageTypes { get; set; }
        public DbSet<BookInformation> BookInformation { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Task<int> SaveChangesAsync();
        public int SaveChanges();
    }
}
