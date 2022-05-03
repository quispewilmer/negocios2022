using negocios2022.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using negocios2022.ViewModel;

namespace negocios2022.Controllers
{
    public class ProductoCategoriaController : Controller
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        // GET: ProductoCategoria
        public ActionResult Listar()
        {
            ProductoCategoria productoCategoria = new ProductoCategoria()
            {
                listaCategorias = listarCategorias(),
                listaProductos = listarProductos()
            };

            return View(productoCategoria);
        }

        private List<Producto> listarProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

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
                            Producto producto = new Producto()
                            {
                                idProducto = dataReader.GetInt32(0),
                                nombreProducto = dataReader.GetString(1),
                                idProveedor = dataReader.GetInt32(2),
                                idCategoria = dataReader.GetInt32(3),
                                unidadMedida = dataReader.GetString(4),
                                precioUnidad = dataReader.GetDecimal(5),
                                unidadesExistencia = dataReader.GetInt16(6)
                            };

                            listaProductos.Add(producto);
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

            return listaProductos;
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

            return listaCategorias;
        }
    }
}