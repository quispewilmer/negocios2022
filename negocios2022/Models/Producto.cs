using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace negocios2022.Models
{
    public class Producto
    {
        [Required]
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int idProveedor { get; set; }
        public int idCategoria { get; set; }
        public string unidadMedida { get; set; }
        public decimal precioUnidad { get; set; }
        public int unidadesExistencia { get; set; }
    }
}