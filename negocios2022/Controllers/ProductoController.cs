using negocios2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;

namespace negocios2022.Controllers
{
    public class ProductoController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        [HttpGet]
        public ActionResult Listar()
        {
            List<Producto> productos = listarProductos();

            return View(productos);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            ViewBag.ProductoCantidad = listarProductos().Count() + 1;

            return View();
        }

        [HttpGet]
        public ActionResult Actualizar(int idProducto)
        {
            Producto producto = listarProductos().Find(x => x.idProducto == idProducto);

            return View(producto);
        }

        [HttpGet]
        public ActionResult Eliminar(int idProducto)
        {
            Producto producto = listarProductos().Find(x => x.idProducto == idProducto);

            return View(producto);
        }

        [HttpGet]
        public ActionResult ListarPorCategoria()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Producto> listaProductos = listarProductos();
            List<Categoria> listaCategorias = listarCategorias();

            foreach (Categoria categoria in listaCategorias)
            {
                items.Add(new SelectListItem { Text = categoria.nombreCategoria, Value = categoria.idCategoria.ToString() });
            }

            ViewBag.ListaCategorias = items;

            return View(listaProductos);
        }

        [HttpPost]
        public ActionResult ListarPorCategoria(string ListaCategorias)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Producto> listaProductos = !ListaCategorias.Equals("")
                ? listarProductosPorCategoria(ListaCategorias)
                : listarProductos();

            foreach (Categoria categoria in listarCategorias())
            {
                items.Add(new SelectListItem { Text = categoria.nombreCategoria, Value = categoria.idCategoria.ToString(), Disabled = categoria.idCategoria.ToString().Equals(ListaCategorias) });
            }

            ViewBag.ListaCategorias = items;

            return View(listaProductos);
        }

        [HttpPost]
        public ActionResult Registrar(Producto producto)
        {
            int result = registrarProducto(producto);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Actualizar(Producto producto)
        {
            int result = actualizarProducto(producto);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Eliminar(Producto producto)
        {
            int result = eliminarProducto(producto.idProducto);

            return RedirectToAction("Listar");
        }
        private List<Categoria> listarCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM tb_categorias", connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            Categoria categoria = new Categoria()
                            {
                                idCategoria = dataReader.GetInt32(0),
                                nombreCategoria = dataReader.GetString(1),
                                descripcion = dataReader.GetString(2)
                            };

                            listaCategorias.Add(categoria);
                        }
                    }
                    catch (InvalidOperationException ioe)
                    {

                    }
                    catch (SqlException se)
                    {

                    }
                    catch (ConfigurationErrorsException cee)
                    {

                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return listaCategorias;
        }

        private List<Producto> listarProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM tb_productos", connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            Producto producto = new Producto
                            {
                                idProducto = dataReader.GetInt32(0),
                                nombreProducto = dataReader.GetString(1),
                                idProveedor = dataReader.GetInt32(2),
                                idCategoria = dataReader.GetInt32(3),
                                unidadMedida = dataReader.GetString(4),
                                precioUnidad = dataReader.GetDecimal(5),
                                unidadesExistencia = dataReader.GetInt16(6)
                            };

                            productos.Add(producto);
                        }

                        dataReader.Close();
                    }
                    catch (InvalidOperationException ioe)
                    {

                    }
                    catch (SqlException se)
                    {

                    }
                    catch (ConfigurationErrorsException cee)
                    {

                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return productos;
        }

        private List<Producto> listarProductosPorCategoria(string idCategoria)
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM tb_productos WHERE idCategoria = @IdCategoria", connection))
                {
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    try
                    {
                        connection.Open();
                        SqlDataReader dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            Producto producto = new Producto
                            {
                                idProducto = dataReader.GetInt32(0),
                                nombreProducto = dataReader.GetString(1),
                                idProveedor = dataReader.GetInt32(2),
                                idCategoria = dataReader.GetInt32(3),
                                unidadMedida = dataReader.GetString(4),
                                precioUnidad = dataReader.GetDecimal(5),
                                unidadesExistencia = dataReader.GetInt16(6)
                            };

                            productos.Add(producto);
                        }

                        dataReader.Close();
                    }
                    catch (InvalidOperationException ioe)
                    {

                    }
                    catch (SqlException se)
                    {

                    }
                    catch (ConfigurationErrorsException cee)
                    {

                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return productos;
        }

        private int registrarProducto(Producto producto)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO tb_productos(IdProducto, NombreProducto, IdProveedor, IdCategoria, umedida, PrecioUnidad, UnidadesEnExistencia) " +
                    "VALUES (@Id, @Nombre, @IdProveedor, @IdCategoria, @UMedida, @PrecioUnidad, @UnidadesExistencia)", connection))
                {
                    command.Parameters.AddWithValue("@Id", producto.idProducto);
                    command.Parameters.AddWithValue("@Nombre", producto.nombreProducto);
                    command.Parameters.AddWithValue("@IdProveedor", producto.idProveedor);
                    command.Parameters.AddWithValue("@IdCategoria", producto.idCategoria);
                    command.Parameters.AddWithValue("@UMedida", producto.unidadMedida);
                    command.Parameters.AddWithValue("@PrecioUnidad", producto.precioUnidad);
                    command.Parameters.AddWithValue("@UnidadesExistencia", producto.unidadesExistencia);

                    try
                    {
                        connection.Open();

                        result = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return result;
        }

        private int actualizarProducto(Producto producto)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE tb_productos " +
                    "SET NombreProducto = @Nombre, IdProveedor = @IdProveedor, IdCategoria = @IdCategoria, umedida = @UMedida, PrecioUnidad = @Precio, UnidadesEnExistencia = @UExistencia " +
                    "WHERE IdProducto = @IdProducto", connection))
                {
                    command.Parameters.AddWithValue("@IdProducto", producto.idProducto);
                    command.Parameters.AddWithValue("@Nombre", producto.nombreProducto);
                    command.Parameters.AddWithValue("@IdProveedor", producto.idProveedor);
                    command.Parameters.AddWithValue("@IdCategoria", producto.idCategoria);
                    command.Parameters.AddWithValue("@UMedida", producto.unidadMedida);
                    command.Parameters.AddWithValue("@Precio", producto.precioUnidad);
                    command.Parameters.AddWithValue("@UExistencia", producto.unidadesExistencia);

                    try
                    {
                        connection.Open();

                        result = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return result;
        }

        private int eliminarProducto(int idProducto)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM tb_productos WHERE IdProducto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", idProducto);

                    try
                    {
                        connection.Open();

                        result = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

                return result;
        }
    }
}