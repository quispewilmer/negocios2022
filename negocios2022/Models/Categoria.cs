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
        public int idCategoria { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string nombreCategoria { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }
    }
}