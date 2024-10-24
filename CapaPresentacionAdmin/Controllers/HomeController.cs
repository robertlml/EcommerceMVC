using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CapaPresentacionAdmin.Controllers
{
 
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();
            oLista = new CN_Usuarios().Listar();

            return Json( new {data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuarios( Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if(objeto.IdUsuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
             }
            else
            {
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
            }


            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VistaDashBoard()
        {
            Dashboard objeto = new CN_Reporte().VerDashBoard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListaReporte(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListaAllReporte()
        {
            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reporte().AllVentas();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public FileResult ExportarVentas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(string)); 
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("IdTransaccion", typeof(string));


            foreach (Reporte r in oLista)
            {
                dt.Rows.Add(new object[] { 
                        r.FechaVenta,
                        r.Cliente,
                        r.Producto,
                        r.Precio,
                        r.Cantidad,
                        r.Total,
                        r.IdTransaccion
                });
            }

            dt.TableName = "Datos";

            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using(MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");      

                }
            }
        }
    }

}