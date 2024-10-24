using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { set; get; }
        public int IdCliente { set; get; }
        public int TotalProducto { set; get; }
        public decimal MontoTotal { set; get; }
        public string Contacto { set; get; }
        public string IdDistrito { set; get; }
        public string Telefono { set; get; }
        public string Direccion { set; get; }
        public string FechaTexto { set; get; }
        public string IdTransaccion { set; get; }

    }
}
