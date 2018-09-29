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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
