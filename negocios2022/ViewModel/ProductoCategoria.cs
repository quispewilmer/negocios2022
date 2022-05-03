using negocios2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace negocios2022.ViewModel
{
    public class ProductoCategoria
    {
        public List<Producto> listaProductos { get; set; }
        public List<Categoria> listaCategorias { get; set; }
    }
}