using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarPersonalController : Controller
    {
        // GET: AdministrarPersonal
        
        [AccessDeniedAuthorize(Roles = "Administrador")]
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
        public JsonResult Crear(PersonalViewModel persona)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(persona);
 
            if (resultado.ok == true)
            {
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
                    username = (nombre.Substring(0, cantidad_caracter) + "." + apellido).ToLower();

                    //Consulta

                    if (bd.PERSONAL.ToList().FindAll(x => x.USERNAME_PERSO.Contains(username)).Count() == 0)
                    {
                        username_encontrado = true;
                    }
                    else
                    {
                        cantidad_caracter++;
                    }

                }
                personal.USERNAME_PERSO = username;
                personal.ACTIVO = "S";
                //GenerarPassword
                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var password = int.Parse(justNumbers.Substring(4, 4));
                personal.PASSWORD_PERSO = password.ToString();

                bd.PERSONAL.Add(personal);
                bd.SaveChanges();
                resultado.mensaje= "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Personal Registrado Correctamente</br>";
                resultado.mensaje = resultado.mensaje+ "<i class='zmdi zmdi-account-add zmdi-hc-fw'></i>Usuario: " + username+"<br>";
                resultado.mensaje = resultado.mensaje+ "<i class='zmdi zmdi-lock zmdi-hc-fw'></i>Contraseña: " + password;

            }
            else
            {
                resultado.mensaje = "<b>Error</b></br>"+ resultado.mensaje;
            }
            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Eliminar(int id )
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resultado = new baseRespuesta();

            var personal = bd.PERSONAL.Find(id);
            personal.ACTIVO = "N";
            bd.Entry(personal).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "Personal Eliminado Correctamente";
            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Editar(PersonalViewModel persona)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(persona);
            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.PERSONAL personal = new PERSONAL();


                var personaId = bd.PERSONAL.Find(persona.id_personal);
                personaId.APELLIDOM_PERSO = persona.apellidom_perso;
                personaId.APELLIDOP_PERSO = persona.apellidop_perso;
                personaId.CORREO_PERSO = persona.correo_pero;
                personaId.DIRECCION_PERSO = persona.direccion_perso;
                personaId.TIPO_PERSONAL_ID_TIPOPERSONAL = persona.tipo_personal;
                personaId.TELEFONO_PERSO = persona.telefono_perso;
                personaId.NOMBRE_PERSO = persona.nombre_perso;
                bd.Entry(personaId).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Personal Modificado Correctamente";

            }

            return Json(resultado);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }


        public baseRespuesta validaciones(PersonalViewModel persona)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            //Validaciones
            if (persona.nombre_perso == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Nombre</br>";
                resultado.ok = false;
            }
            if (persona.apellidop_perso == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Apellido Paterno</br>";
                resultado.ok = false;
            }
            if (persona.apellidom_perso == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Apellido Materno</br>";
                resultado.ok = false;
            }
            if (persona.telefono_perso == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Telefono</br>";
                resultado.ok = false;
            }
            else if (Int64.Parse(persona.telefono_perso) < 0 || Int64.Parse(persona.telefono_perso) > 999999999 || persona.telefono_perso.Length != 9)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> El Telefono Ingresado no es Valido</br>";
                resultado.ok = false;
            }
            if (persona.direccion_perso == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Direccion</br>";
                resultado.ok = false;
            }
            Regex oRegExp = new Regex(@"^[A-Za-z0-9_.\-]+@[A-Za-z0-9_\-]+\.([A-Za-z0-9_\-]+\.)*[A-Za-z][A-Za-z]+$", RegexOptions.IgnoreCase);

            if (persona.correo_pero == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Correo Electronico</br>";
                resultado.ok = false;
            }
            else if (!oRegExp.Match(persona.correo_pero).Success)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Correo Electronico no Valido</br>";
                resultado.ok = false;
            }
            if (persona.tipo_personal != 1 && persona.tipo_personal != 2)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Seleccione un Tipo de Personal</br>";
                resultado.ok = false;
            }
            return resultado;
        }

        
    }
}