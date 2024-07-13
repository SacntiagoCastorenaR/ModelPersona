using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnicaPersona.Controllers
{
    public class CuentasController : Controller
    {

        readonly ModelPersona.CuentasClases.CuentasClase cuentasClase = new ModelPersona.CuentasClases.CuentasClase();
        // GET: Cuentas
        public ActionResult GetcuentasById(int id)
        {
            var cuenta = cuentasClase.getCuentasById(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return Json(cuenta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrudCuentas(int id, int idPersona, string Banco, string Cuenta)
        {
            cuentasClase.idCuentas = id;
            cuentasClase.idPersona = idPersona;
            cuentasClase.Banco = Banco;
            cuentasClase.Cuenta = Cuenta;
            cuentasClase.CrudCuentas(id);
            if (id > 0)
            {
                var message = "El banco " + cuentasClase.Banco + " ha sido editado.";
                TempData["MessageModal"] = message.ToString();
            }
            else
            {
                var message = "El banco " + cuentasClase.Banco + " ha sido agregado.";
                TempData["MessageModal"] = message.ToString();
            }
            return Json(new { success = true });
        }

        public ActionResult Delete(int id)
        {
            cuentasClase.DeleteCuenta(id);
            return Json(new { success = true });
        }
    }
}