using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace CapaDatos
{
    public class CD_Uusarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {

                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdUsuario,Nombres,Apellidos,Correo,Clave,Restablecer,Activo from USUARIO";


                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Restablecer"]),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }

                    }
                }
            }
           catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
       

            }
            return lista;
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", cnx);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
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
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
           
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", cnx);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
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
                    SqlCommand cmd = new SqlCommand("delete top(1) from usuario where IdUsuario = @id", cnx);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
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

        public bool CambiarClave(int idususario, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @nuevaclave, restablecer = 0 where idusuario = @id", cnx);
                    cmd.Parameters.AddWithValue("@id", idususario);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
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
        public bool ReestablecerClave(int idususario, string clave, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @nuevaclave, restablecer = 1 where IdUsuario = @id", cnx);
                    cmd.Parameters.AddWithValue("@id", idususario);
                    cmd.Parameters.AddWithValue("@nuevaclave", clave);
                    cmd.CommandType = CommandType.Text;
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
