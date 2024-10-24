using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select p.IdProducto,p.Nombre,p.Descripcion,");
                    sb.AppendLine("m.IdMarca,m.Descripcion[DesMarca],");
                    sb.AppendLine("c.IdCategoria,c.Descripcion[DesCategoria],");
                    sb.AppendLine("p.Precio,p.Stock,p.RutaImagen,p.NombreImage,p.Activo");
                    sb.AppendLine("from PRODUCTO p");
                    sb.AppendLine("inner join MARCA m on m.IdMarca = p.IdMarca");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), cnx);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImage"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();

            }
            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRODUCTO", cnx);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cnx.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTO", cnx);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cnx.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool GuardarDatosImagen(Producto obj , out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "update producto set RutaImagen = @rutaimagen, NombreImage = @nombreimage where IdProducto = @idproducto";

                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreimage", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@idproducto", obj.IdProducto);
                    cmd.CommandType = CommandType.Text;

                    cnx.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    } 
                    else
                    {
                        mensaje = "No se pudo actualizar imagen";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;


        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPRODUCTO", cnx);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cnx.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


    }
}
