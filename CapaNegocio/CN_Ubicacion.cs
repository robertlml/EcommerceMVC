using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapDato = new CD_Ubicacion();

        public List<Departamento> ObtenerDepartamento()
        {
            return objCapDato.ObtenerDepartamento();
        }
        public List<Provincia> ObtenerProvincia(string iddepartamento)
        {
            return objCapDato.ObtenerProvincia(iddepartamento);
        }
        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            return objCapDato.ObtenerDistrito(iddepartamento, idprovincia);
        }
    }
}
