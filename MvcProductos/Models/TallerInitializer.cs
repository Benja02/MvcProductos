using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcProductos.Models;

namespace MvcProductos.Models
{
    public class TallerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TallerContext>
    {
        protected override void Seed(TallerContext context)
        {
            var categ = new List<Category>
            {
            new Category{IdCategory=001,Descripcion="Frutas"},
            new Category{IdCategory=0025,Descripcion="Carnes"}
            };
            categ.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();

            var prod = new List<Product>
            {
            new Product{IdProduct=01,Descripcion="Manzanas",IdCategoria=001, Costo=50, precioVenta = 60, existencia = 100, numPedidos=1},
            new Product{IdProduct=02,Descripcion="Res",IdCategoria=0025, Costo=80, precioVenta = 100, existencia = 200, numPedidos=2} 
            };

            prod.ForEach(s => context.Productos.Add(s));
            context.SaveChanges();
            
        }
    }
}