using GeneralServer.API.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralServer.API.Core
{
    public class GeneralServerDbContext : DbContext
    {

        public GeneralServerDbContext(DbContextOptions<GeneralServerDbContext> contextOptions) : base(contextOptions)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .Property(x => x.Deleted)
                .HasDefaultValue(false);
            modelBuilder.Entity<User>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>()
                .Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
