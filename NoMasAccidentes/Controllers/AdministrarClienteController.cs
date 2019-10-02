using System;
using System.Collections.Generic;
using System.Linq;
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
            modelo.TotalDeRegistros = totalRegistros;


            return View(modelo);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CrearCliente(ClienteViewModel cliente)
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

            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var password = int.Parse(justNumbers.Substring(4, 4));
            clienteR.PASSWORD_CLIENTE = password.ToString();


            bd.CLIENTE.Add(clienteR);
            bd.SaveChanges();
            return Json("d");



        }
    }
}