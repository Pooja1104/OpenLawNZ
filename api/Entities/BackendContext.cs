using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class BackendContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<LegalCase> Cases { get; set; }

        public BackendContext(DbContextOptions<BackendContext> options)
    : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>()
             .HasMany(c => c.Cases)
             .WithOne(e => e.Folder);
        }

    }
}
