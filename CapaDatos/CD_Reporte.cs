using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<CapaEntidad.Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<CapaEntidad.Reporte> lista = new List<CapaEntidad.Reporte>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
                  
                    SqlCommand cmd = new SqlCommand("SP_REPORTEVENTAS", cnx);
                   
                    cmd.Parameters.AddWithValue("@fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("@fechafin", fechafin);
                    cmd.Parameters.AddWithValue("@idtransaccion", idtransaccion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CapaEntidad.Reporte()
                            {
                                FechaVenta = dr["FechaVenta"].ToString(),
                                Cliente = dr["Cliente"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                Precio = dr["Precio"].ToString(),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                IdTransaccion = dr["IdTransaccion"].ToString(),

                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<CapaEntidad.Reporte>();

            }
            return lista;
        }
        public List<CapaEntidad.Reporte> AllVentas()
        {
            List<CapaEntidad.Reporte> lista = new List<CapaEntidad.Reporte>();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {


                    SqlCommand cmd = new SqlCommand("SP_REPORTEVENTAS_ALL", cnx);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CapaEntidad.Reporte()
                            {
                                FechaVenta = dr["FechaVenta"].ToString(),
                                Cliente = dr["Cliente"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                Precio = dr["Precio"].ToString(),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                IdTransaccion = dr["IdTransaccion"].ToString(),

                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<CapaEntidad.Reporte>();

            }
            return lista;
        }
        public Dashboard VerDashBoard()
        {
            Dashboard objeto = new Dashboard();
            try
            {
                using (SqlConnection cnx = new SqlConnection(Conexion.cn))
                {
             
                    SqlCommand cmd = new SqlCommand("SP_REPORTEDASHBOARD", cnx);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cnx.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Dashboard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"])

                            };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objeto = new Dashboard();

            }
            return objeto;
        }


    }
}
