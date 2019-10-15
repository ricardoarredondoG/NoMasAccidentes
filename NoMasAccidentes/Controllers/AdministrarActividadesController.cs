using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarActividadesController : Controller
    {
        // GET: AdministrarActividades
        [Authorize]
        public ActionResult Index(int pagina = 1, string fecha="", string descripcion = "", string tipoActividad = "",string estado = "")
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var actividad = bd.ACTIVIDAD.ToList();

            //Busqueda por Nombre
            if (fecha != "")
            {
                actividad = actividad.FindAll(x => x.FECHA_ACTIVIDAD.ToShortDateString().Contains(fecha.ToString()));
                actividad = actividad.FindAll(x => x.DESCRIPCION_ACTIVIDAD.ToLower().Contains(descripcion.ToLower()));
                //actividad = actividad.FindAll(x => x.TIPO_ACTIVIDAD.Contains(descripcion.ToLower()));



            }
            //Busqueda por Apellido Paterno

            
            var totalRegistros = actividad.Count();
            actividad = actividad.OrderBy(x => x.ID_ACTIVIDAD).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();


            var modelo = new ActividadesViewModel();

           // modelo.actividad = actividad;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            

            return View(modelo);
        }

        [HttpPost]
        [Authorize]

        /*public JsonResult Crear(ActividadesViewModel actividades)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.ACTIVIDAD actividad = new ACTIVIDAD();

            //actividad.FECHA_ACTIVIDAD = actividades.fecha.ToString();
            //return 0;
        }*/

        public JsonResult Crear(PersonalViewModel persona) {


            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.PERSONAL personal = new PERSONAL();

            personal.DIRECCION_PERSO = persona.direccion_perso;
            personal.APELLIDOM_PERSO = persona.apellidom_perso;
            personal.APELLIDOP_PERSO = persona.apellidop_perso;
            personal.CORREO_PERSO = persona.correo_pero;
            personal.NOMBRE_PERSO = persona.nombre_perso;
            personal.TELEFONO_PERSO = persona.telefono_perso;
            personal.TIPO_PERSONAL_ID_TIPOPERSONAL = persona.tipo_personal;

            //Generar Usuario

                //Eliminar espacios en Blanco
                var nombre = persona.nombre_perso.Replace(" ", "");
                var apellido = persona.apellidop_perso.Replace(" ", "");
                var username = "";
                var username_encontrado = false;
                var cantidad_caracter = 3;

            //Buscar usuario
            while (!username_encontrado)
                {
                    username = (nombre.Substring(0, cantidad_caracter)+"."+apellido).ToLower();

                //Consulta

                    if(bd.PERSONAL.ToList().FindAll(x => x.USERNAME_PERSO.Contains(username)).Count() == 0)
                    {
                        username_encontrado = true;
                    }else
                    {
                        cantidad_caracter++;
                    }

                }
            personal.USERNAME_PERSO = username;

            //GenerarPassword
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var password = int.Parse(justNumbers.Substring(4, 4));
            personal.PASSWORD_PERSO = password.ToString();

            bd.PERSONAL.Add(personal);
            bd.SaveChanges();
            return Json("d");
        }
    }
}