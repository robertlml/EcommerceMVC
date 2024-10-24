﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Reporte
    {
        public string FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }

        public string Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string IdTransaccion { get; set; }

    }
}
