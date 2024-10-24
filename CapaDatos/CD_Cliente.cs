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
    public class CD_Cliente
    {


        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCLIENTE", cnx);
                    cmd.Parameters.AddWithValue("nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("correo", obj.Correo);
                    cmd.Parameters.AddWithValue("clave", obj.Clave);
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
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
            
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdCliente,Nombres,Apellidos,Correo,Clave,Restablecer from Cliente";


                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Restablecer = Convert.ToBoolean(dr["Restablecer"])
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                 lista = new List<Cliente>();

            }
            return lista;
        }
        public bool CambiarClave(int idcliente, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @nuevaclave, restablecer = 0 where idcliente = @id", cnx);
                    cmd.Parameters.AddWithValue("@id", idcliente);
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
        public bool ReestablecerClave(int idcliente, string clave, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @nuevaclave, restablecer = 1 where IdCliente = @id", cnx);
                    cmd.Parameters.AddWithValue("@id", idcliente);
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
