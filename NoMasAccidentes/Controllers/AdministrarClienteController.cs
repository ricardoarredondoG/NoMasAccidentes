﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarClienteController : Controller
    {
        // GET: AdministrarCliente
        public ActionResult Index(int pagina = 1, string nombre = "", string apellidoC = "", string usuario = "", string correoC = "")
        {
            var cantidadRegistroPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var cliente = bd.CLIENTE.ToList();

            if (nombre != "")
            {
                cliente = cliente.FindAll(x => x.NOMBRE_CLIENTE.ToLower().Contains(nombre.ToLower()));
            }

            if (apellidoC != "")
            {
                cliente = cliente.FindAll(x => x.APELLIDO_CLIENTE.ToLower().Contains(apellidoC.ToLower()));
            }

            if (usuario != "")
            {
                cliente = cliente.FindAll(x => x.USERNAME_CLIENTE.ToLower().Contains(usuario.ToLower()));
            }
            if (correoC != "")
            {
                cliente = cliente.FindAll(x => x.CORREO_CLIENTE.ToLower().Contains(correoC.ToLower()));
            }

            var totalRegistros = cliente.Count();
            cliente = cliente.OrderBy(x => x.ID_CLIENTE).Skip((pagina - 1) * cantidadRegistroPorPagina).Take(cantidadRegistroPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.cliente = cliente;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistroPorPagina;
            modelo.TotalDeRegistros = totalRegistros;


            //rubro
            modelo.rubro = bd.RUBRO.ToList();


            return View(modelo);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CrearCliente(ClienteViewModel cliente)
        {
            //Validaciones.
            var resul = new baseRespuesta();
            resul.ok = true;
            if (cliente.rut_cliente == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese rut</br>";
                resul.ok = false;
            }
            if (cliente.nombre_cliente == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese nombre</br>";
                resul.ok = false;
            }
            if (cliente.apellido_cliente == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese apellido</br>";
                resul.ok = false;
            }
            if (cliente.telefono_cliente == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese telefono</br>";
                resul.ok = false;
            }
            else if (Int64.Parse(cliente.telefono_cliente) < 0 || Int64.Parse(cliente.telefono_cliente) > 999999999 || cliente.telefono_cliente.Length != 9)
            {
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> El Telefono Ingresado no es Valido</br>";
                resul.ok = false;
            }
            if (cliente.direc_cliente == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese direccion</br>";
                resul.ok = false;
            }
            Regex oRegExp = new Regex(@"^[A-Za-z0-9_.\-]+@[A-Za-z0-9_\-]+\.([A-Za-z0-9_\-]+\.)*[A-Za-z][A-Za-z]+$", RegexOptions.IgnoreCase);

            if (cliente.correo_cliente == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese correo</br>";
                resul.ok = false;
            }
            else if (!oRegExp.Match(cliente.correo_cliente).Success)
            {
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Correo Electronico no Valido</br>";
                resul.ok = false;
            }
            if (cliente.rubro_id_rubro != 0)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Seleccione rubro</br>";
                resul.ok = false;
            }

            if (resul.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.CLIENTE clienteR = new CLIENTE();


                clienteR.RUT_CLIENTE = cliente.rut_cliente;
                clienteR.NOMBRE_CLIENTE = cliente.nombre_cliente;
                clienteR.APELLIDO_CLIENTE = cliente.apellido_cliente;
                clienteR.TELEFONO_CLIENTE = cliente.telefono_cliente;
                clienteR.DIREC_CLIENTE = cliente.direc_cliente;
                clienteR.CORREO_CLIENTE = cliente.correo_cliente;
                clienteR.RUBRO_ID_RUBRO = cliente.rubro_id_rubro;
                //Espacios en blanco

                var nombre = cliente.nombre_cliente.Replace(" ", "");
                var apellido = cliente.apellido_cliente.Replace(" ", "");
                var username = "";
                var user_encontrado = false;
                var cantidad_caracter = 3;


                //Buscar cliente

                while (!user_encontrado)
                {
                    username = (nombre.Substring(0, cantidad_caracter) + "." + apellido).ToLower();

                    if (bd.CLIENTE.ToList().FindAll(x => x.USERNAME_CLIENTE.Contains(username)).Count() == 0)
                    {
                        user_encontrado = true;

                    }
                    else
                    {
                        cantidad_caracter++;
                    }

                }

                clienteR.USERNAME_CLIENTE = username;
                clienteR.ACTIVO_CLIENTE = "S";


                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var password = int.Parse(justNumbers.Substring(4, 4));
                clienteR.PASSWORD_CLIENTE = password.ToString();


                bd.CLIENTE.Add(clienteR);
                bd.SaveChanges();
                resul.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Cliente Registrado Correctamente</br>";
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-account-add zmdi-hc-fw'></i>Usuario: " + username + "</br>";
                resul.mensaje = resul.mensaje + "<i class='zmdi zmdi-account-add zmdi-hc-fw'></i>Password: " + password;
            }
            else
            {
                resul.mensaje = "<b>Error</b></br>" + resul.mensaje;
            }
            return Json(resul);
        }

        [HttpPost]
        [Authorize]
        public JsonResult EliminarC(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resul = new baseRespuesta();
            var cliente = bd.CLIENTE.Find(id);
            cliente.ACTIVO_CLIENTE = "N";
            bd.Entry(cliente).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resul.mensaje = "Cliente eliminado correctamente";
            return Json(resul);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }
    }
}
