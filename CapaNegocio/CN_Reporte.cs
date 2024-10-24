
using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporte = CapaEntidad.Reporte;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDato = new CD_Reporte();

        public Dashboard VerDashBoard()
        {
            return objCapaDato.VerDashBoard();
        }

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDato.Ventas(fechainicio, fechafin, idtransaccion);

        }

        public List<Reporte> AllVentas()
        {
            return objCapaDato.AllVentas();
        }



    }
}
