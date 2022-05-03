using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace negocios2022.Models
{
    public class Categoria
    {
        [Required]
        [Display(Name = "Id de la Categoría")]
        public int idCategoria { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre de la Categoría")]
        public string nombreCategoria { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
    }
}