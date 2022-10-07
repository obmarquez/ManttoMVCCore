using ManttoMVCCore.Data;
using ManttoMVCCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        private DBOperaciones repo;

        public MantenimientoController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<Consultas> datos = repo.Getdosparam<Consultas>("sp_obtener_mantenimientos", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Nuevo()
        {
            ViewBag.resguardante = repo.Get<ResguardanteConsultaViewmodel>("sp_obter_resguardantes_mantenimiento").ToList();
            ViewBag.equipo = repo.Get<EquipoViewModel>("sp_obtener_equipo_mantenimiento").ToList();
            ViewBag.recibe = repo.Getdosparam<TestigoViewModel>("sp_obtener_testigo", 1, 0).ToList();
            ViewBag.soporte = repo.Getdosparam<TestigoViewModel>("sp_obtener_testigo", 1, 0).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(MantenimientoViewModel mantenimiento)
        {
            repo.AgregaActualizaMantenimiento(mantenimiento);
            return RedirectToAction("Index");
        }
    }
}
