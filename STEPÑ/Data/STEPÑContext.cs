using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using STEPÑ.Models;

namespace STEPÑ.Data
{
    public class STEPÑContext : DbContext
    {
        public STEPÑContext (DbContextOptions<STEPÑContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
