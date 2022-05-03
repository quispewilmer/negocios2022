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
        [Display(Name = "Id del Producto")]
        public int idProducto { get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name = "Nombre del Producto")]
        public string nombreProducto { get; set; }
        [Required]
        [Display(Name = "ID del Proveedor")]
        public int idProveedor { get; set; }
        [Required]
        [Display(Name = "ID de la categoría")]
        public int idCategoria { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Unidad de Medida")]
        public string unidadMedida { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public decimal precioUnidad { get; set; }
        [Required]
        [Display(Name = "Unidades en Existencia")]
        public int unidadesExistencia { get; set; }
    }
}