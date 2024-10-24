using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Marca
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdMarca, Descripcion, Activo from MARCA";


                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Marca>();

            }
            return lista;
        }
        public int Registrar(Marca obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARMARCA", cnx);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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
        public bool Editar(Marca obj, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARMARCA", cnx);
                    cmd.Parameters.AddWithValue("IdMarca", obj.IdMarca);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARMARCA", cnx);
                    cmd.Parameters.AddWithValue("IdMarca", id);
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

        public List<Marca> ListarMarcaxCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select distinct m.IdMarca, m.Descripcion from PRODUCTO p");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");
                    sb.AppendLine("inner join MARCA m on m.idmarca = p.idmarca and m.Activo = 1");
                    sb.AppendLine("WHERE c.IdCategoria = iif(@idcategoria = 0, c.IdCategoria, @idcategoria)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), cnx);
                    cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = dr["Descripcion"].ToString()
                               
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Marca>();

            }
            return lista;
        }

    }
}
