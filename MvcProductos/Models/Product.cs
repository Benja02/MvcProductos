using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProductos.Models
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Categoría")]
        public int IdCategoria { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        [Display(Name = "Costo")]
        public double Costo { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public double precioVenta { get; set; }
        [Required]
        [Display(Name = "Existencias")]
        public int existencia { get; set; }
        [Required]
        [Display(Name = "Cantidad de pedidos")]
        public int numPedidos { get; set; }
    }
}