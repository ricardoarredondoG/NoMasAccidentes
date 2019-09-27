using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarPersonalController : Controller
    {
        // GET: AdministrarPersonal
        [Authorize]
        public ActionResult Index(int pagina = 1, string nombre = "", string apellidoP = "", string usuario = "", string correo = "")
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var personal = bd.PERSONAL.ToList();

            //Busqueda por Nombre
            if (nombre != "")
            {
                personal = personal.FindAll(x => x.NOMBRE_PERSO.ToLower().Contains(nombre.ToLower()));
            }
            //Busqueda por Apellido Paterno

            if (apellidoP != "")
            {
                personal = personal.FindAll(x => x.APELLIDOP_PERSO.ToLower().Contains(apellidoP.ToLower()));
            }

            //Busqueda por Usuario

            if (usuario != "")
            {
                personal = personal.FindAll(x => x.USERNAME_PERSO.ToLower().Contains(usuario.ToLower()));
            }

            //Busqueda por Correo Electronico

            if (correo != "")
            {
                personal = personal.FindAll(x => x.CORREO_PERSO.ToLower().Contains(correo.ToLower()));
            }


            var totalRegistros = personal.Count();
            personal = personal.OrderBy(x => x.ID_PERSONAL).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();


            var modelo = new IndexViewModel();

            modelo.personal = personal;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;


            return View(modelo);
        }

        [HttpPost]
        [Authorize]
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