using ManttoMVCCore.Data;
using ManttoMVCCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Controllers
{
    [Authorize]
    public class ServiciosController : Controller
    {
        private DBOperaciones repo;

        public ServiciosController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<Servicio> datos = repo.Getdosparam<Servicio>("sp_obtener_servicios", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Editar(int p_Id)
        {
            return View(repo.Getdosparam<Servicio>("sp_obtener_servicios", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(Servicio serv)
        {
            repo.AgregaActualizaServicios(serv);
            return RedirectToAction("Index");
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(Servicio serv)
        {
            repo.AgregaActualizaServicios(serv);
            return RedirectToAction("Index");
        }
    }
}
