using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarAsistenteController : Controller
    {

        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public ActionResult Index(int pagina = 1, string nombre = "", string apellidoP = "", string apellidoM = "")
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var modelo = new IndexViewModel();
            return View(modelo);
        }
        // GET: AdministrarAsistente
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public ActionResult ListarCliente(int? pagina = 1, string nombre = "", string apellidoP = "", string apellidoM = "")
        {

            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var asistente = bd.ASISTENTE.ToList();

            var cliente = bd.CLIENTE.Where(x => x.ACTIVO_CLIENTE == "S" && !bd.ASISTENTE.Any(sp => sp.CLIENTE_ID_CLIENTE == x.ID_CLIENTE && sp.ACTIVO_ASISTENTE == "S")).ToList();


            if (nombre != "")
            {
                asistente = asistente.FindAll(x => x.NOMBRE_ASISTENTE.ToLower().Contains(nombre.ToLower()));
            }

            if (apellidoP != "")
            {
                asistente = asistente.FindAll(x => x.APELLIDOP_ASISTENTE.ToLower().Contains(apellidoP.ToLower()));
            }

            if (apellidoM != "")
            {
                asistente = asistente.FindAll(x => x.APELLIDOM_ASISTENTE.ToLower().Contains(apellidoM.ToLower()));
            }

            int pageSize = 4;
            int pageNumber = pagina ?? 1;
            return PartialView(asistente.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]

        public JsonResult CrearA(AsistenteViewModel asistente)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(asistente);
            if (resultado.ok == true)
            {


                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.ASISTENTE asis = new ASISTENTE();

                asis.NOMBRE_ASISTENTE = asistente.nombre_asistente;
                asis.APELLIDOP_ASISTENTE = asistente.apellidop_asistente;
                asis.APELLIDOM_ASISTENTE = asistente.apellidom_asistente;
                asis.CLIENTE_ID_CLIENTE = asistente.cliente_id_cliente;
                asis.ACTIVO_ASISTENTE = "S";


                //Eliminar espacios en Blanco
                var nombre = asistente.nombre_asistente.Replace(" ", "");
                var apellido = asistente.apellidop_asistente.Replace(" ", "");
                var username = "";
                var username_encontrado = false;
                var cantidad_caracter = 3;

                //Buscar usuario
                while (!username_encontrado)
                {
                    username = (nombre.Substring(0, cantidad_caracter) + "." + apellido).ToLower();

                    //Consulta

                    if (bd.ASISTENTE.ToList().FindAll(x => x.NOMBRE_ASISTENTE.Contains(username)).Count() == 0)
                    {
                        username_encontrado = true;
                    }
                    else
                    {
                        cantidad_caracter++;
                    }



                    bd.ASISTENTE.Add(asis);
                    try
                    {
                        bd.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>El asistente no se ha registrado.</br>";
                        throw raise;
                    }

                    resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Asistente registrado correctamente.</br>";
                }
            }
            else
            {
                resultado.mensaje = "<b>Error</b></br>" + resultado.mensaje;
            }
            return Json(resultado);

        }

        [HttpPost]
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public JsonResult EliminarA(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resul = new baseRespuesta();
            var asistente = bd.ASISTENTE.Find(id);
            asistente.ACTIVO_ASISTENTE = "N";
            bd.Entry(asistente).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resul.mensaje = "Asistente eliminado correctamente";
            return Json(resul);
        }

        [HttpPost]
        [Authorize]

        public JsonResult EditarA(AsistenteViewModel asistente)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(asistente);
            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.ASISTENTE asistentes = new ASISTENTE();
                var asistenteId = bd.ASISTENTE.Find(asistente.id_asistente);
                asistenteId.NOMBRE_ASISTENTE = asistente.nombre_asistente;
                asistenteId.APELLIDOP_ASISTENTE = asistente.apellidop_asistente;
                asistenteId.APELLIDOM_ASISTENTE = asistente.apellidom_asistente;
                bd.Entry(asistenteId).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Asistente Modificado Correctamente";
            }
            return Json(resultado);
        }


        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }


        }

        public baseRespuesta validaciones(AsistenteViewModel asistente)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            if (asistente.nombre_asistente == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Nombre</br>";
                resultado.ok = false;
            }
            if (asistente.apellidop_asistente == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese apellido paterno</br>";
                resultado.ok = false;
            }
            if (asistente.apellidom_asistente == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese apellido materno</br>";
                resultado.ok = false;
            }
            if (asistente.cliente_id_cliente == 0)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese id del cliente</br>";
                resultado.ok = false;
            }
            return resultado;
        }
    }
}

