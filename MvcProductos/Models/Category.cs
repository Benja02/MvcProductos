using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProductos.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}