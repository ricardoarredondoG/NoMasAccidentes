using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarAsistenteController : Controller
    {
        // GET: AdministrarAsistente
        public ActionResult Index(int pagina = 1, string nombre = "", string apellidoP = "", string apellidoM = "")
        {
                var cantidadRegistroPorPagina = 4;
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                var asistente = bd.ASISTENTE.ToList();

                
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
                

                var totalRegistros = asistente.Count();
                asistente = asistente.OrderBy(x => x.ID_ASISTENTE).Skip((pagina - 1) * cantidadRegistroPorPagina).Take(cantidadRegistroPorPagina).ToList();

                var modelo = new IndexViewModel();
                modelo.asistente = asistente;
                modelo.PaginaActual = pagina;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;
                modelo.TotalDeRegistros = totalRegistros;


                return View(modelo);

            }
        [HttpPost]
        [Authorize]

        public JsonResult CrearA(AsistenteViewModel asistente)
        {
            var resul = new baseRespuesta();
            resul.ok = true;
            //Validaciones.
            if(asistente.nombre_asistente == null)
            {
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese nombre del asistente</br>";
            }
            if (asistente.apellidop_asistente == null)
            {
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese apellido paterno</br>";
            }
            if (asistente.apellidom_asistente == null)
            {
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese apellido materno.</br>";
            }
            if (asistente.cliente_id_cliente != 0) 
            {
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese id del cliente.</br>";
                resul.ok = false;
            }
            if (resul.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.ASISTENTE asis = new ASISTENTE();

                asis.NOMBRE_ASISTENTE = asistente.nombre_asistente;
                asis.APELLIDOP_ASISTENTE = asistente.apellidop_asistente;
                asis.APELLIDOM_ASISTENTE = asistente.apellidom_asistente;
                asis.CLIENTE_ID_CLIENTE = asistente.cliente_id_cliente;
                asis.ACTIVO_ASISTENTE = "S";


                //Eliminar espacios en Blanco
                /* var nombre = asistente.nombre_asistente.Replace(" ", "");
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

                 }
                 */
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
                    resul.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>El asistente no se ha registrado.</br>";
                    throw raise;
                }
                
                resul.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Asistente registrado correctamente.</br>";
            }
            else
            {
                resul.mensaje = "<b>Error</b></br>" + resul.mensaje;
            }
            return Json(resul);

        }


        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }
    }
}