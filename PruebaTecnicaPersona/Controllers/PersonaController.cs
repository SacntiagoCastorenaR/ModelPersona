using ModelPersona.PersonaClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PruebaTecnicaPersona.Controllers
{
    public class PersonaController : Controller
    {
        readonly ModelPersona.PersonaClases.PersonaClase getPersona = new ModelPersona.PersonaClases.PersonaClase();
        readonly ModelPersona.TipoRegimenClases.TipoRegimenClase getTipoRegimen = new ModelPersona.TipoRegimenClases.TipoRegimenClase();
        readonly ModelPersona.PaisClases.PaisClase getPais = new ModelPersona.PaisClases.PaisClase();
        readonly ModelPersona.SexoClases.SexoClase getSexo = new ModelPersona.SexoClases.SexoClase();
        // GET: Persona
        public ActionResult PersonaIndex()
        {
            var personList = getPersona.getPersona();
            ViewBag.Persona = personList;
            ViewBag.MessageModal = TempData["MessageModal"];
            return View();
        }

        public ActionResult CrudPersona(int id = 0)
        {
            DDLRegimen();
            DDLPais();
            DDLSexo();
            return View( id == 0 ? new PersonaClase() : getPersona.getPersonaById(id));
        }

        [HttpPost]
        public ActionResult CrudPersona(PersonaClase model, string submitButton = "", int id = 0)
        {

            DDLRegimen();
            DDLPais();
            DDLSexo();
            if (submitButton.Equals("Guardar")){
                getPersona.idPersona = model.idPersona;
                getPersona.Nombre = model.Nombre;
                getPersona.ApellidoPaterno = model.ApellidoPaterno;
                getPersona.ApellidoMaterno = model.ApellidoMaterno;
                getPersona.idTipoRegimen = model.idTipoRegimen;
                getPersona.FechaNacimiento = model.FechaNacimiento;
                getPersona.RFC = model.RFC;
                getPersona.CURP = model.CURP;
                getPersona.idPais = model.idPais;
                getPersona.idSexo = model.idSexo;
                getPersona.Estatus = model.Estatus;
                if (ModelState.IsValid)
                {
                    getPersona.CrudPersona(id);
                    if (id > 0)
                    {
                        var message = "El usuario " + getPersona.Nombre + " ha sido editado.";
                        TempData["MessageModal"] = message.ToString();
                    }
                    else
                    {
                        var message = "El usuario " + getPersona.Nombre + " ha sido agregado.";
                        TempData["MessageModal"] = message.ToString();
                    }
                }
                return RedirectToAction("PersonaIndex", "Persona");

            }
            return View(id == 0 ? new PersonaClase() : getPersona.getPersonaById(id));
        }

        public void DDLRegimen()
        {
            var tipoRegimen = getTipoRegimen.getTipoRegimen();
            var selectListItems = new List<SelectListItem>();
            foreach(var regimen  in tipoRegimen)
            {
                selectListItems.Add(new SelectListItem() {  Value= regimen.idTipoRegimen.ToString(), Text = regimen.Descripcion });
            }
             ViewBag.tipoRegimen = selectListItems;
        }
        public void DDLPais()
        {
            var Listpais = getPais.getPais();
            var selectListItem = new List<SelectListItem>();
            foreach (var pais in Listpais)
            {
                selectListItem.Add(new SelectListItem() { Value = pais.idPais.ToString(), Text = pais.Pais1 });
            }
            ViewBag.pais = selectListItem;
        }
        public void DDLSexo()
        {
            var ListSexo = getSexo.getSexo();
            var selectListItem = new List<SelectListItem>();
            foreach (var sexo in ListSexo)
            {
                selectListItem.Add(new SelectListItem() { Value = sexo.idSexo.ToString(), Text = sexo.Sexo1 });
            }
            ViewBag.sexo = selectListItem;
        }

        
        public ActionResult DeletePersona(int id)
        {
            getPersona.DeletePersona(id);
            
            
                var message = "El usuario " + getPersona.Nombre + " ha sido eliminado.";
                TempData["MessageModal"] = message.ToString();
                return RedirectToAction("PersonaIndex");
        }

        public JsonResult getCuentas(int id)
        {
            var resultado = getPersona.GetJson(id);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            object obj = serializer.Deserialize(resultado,typeof(object));
            return Json(obj,JsonRequestBehavior.AllowGet);
        }
    }
}