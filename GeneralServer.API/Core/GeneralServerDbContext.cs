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

        public GeneralServerDbContext(DbContextOptions<GeneralServerDbContext> options) : base(options)
        {

        }
        

        public virtual DbSet<GSUser> GSUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GSUser>()
                .Property(x => x.Deleted)
                .HasDefaultValue(false);
            modelBuilder.Entity<GSUser>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<GSUser>()
                .Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

        }
    }
}
