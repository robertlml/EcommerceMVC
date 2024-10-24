using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Ubicacion
    {
        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lista = new List<Departamento>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM DEPATARMENTO";

                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Departamento()
                            {
                                IdDepartamento = dr["IdDepartamento"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Departamento>();

            }
            return lista;
        }

        public List<Provincia> ObtenerProvincia(string iddepartamento)
        {
            List<Provincia> lista = new List<Provincia>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from provincia where IdDepartamento = @iddepartamento";


                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Provincia()
                            {
                                IdProvincia = dr["IdProvincia"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Provincia>();

            }
            return lista;
        }

        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            List<Distrito> lista = new List<Distrito>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from DISTRITO where IdProvincia = @idprovincia and IdDepartamento = @iddepartamento";


                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Distrito()
                            {
                                IdDistrito = dr["IdDistrito"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Distrito>();

            }
            return lista;
        }
    }
}
