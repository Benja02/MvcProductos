using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcProductos.Models
{
    public class TallerContext : DbContext
    {
        public TallerContext() : base("TallerContext")
        {
        }

        public DbSet<Product> Productos { get; set; }
        public DbSet<Category> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}