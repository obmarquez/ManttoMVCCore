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
    public class SoporteController : Controller
    {
        private DBOperaciones repo;

        public SoporteController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<ManttoMVCCore.Models.ViewModel.SoporteVM> datos = repo.Get<ManttoMVCCore.Models.ViewModel.SoporteVM>("sp_obtener_soporte_index").ToList();
            return View(datos);
        }

        public IActionResult Nuevo()
        {
            ViewBag.areas = repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 1, 0);
            ViewBag.resguardante = repo.Get<ResguardanteConsultaViewmodel>("sp_obter_resguardantes_mantenimiento").ToList();
            ViewBag.servicios = repo.Getdosparam<Servicio>("sp_obtener_servicios", 1, 0);
            ViewBag.atender = repo.Getdosparam<TestigoViewModel>("sp_obtener_testigo", 1, 0).ToList();
            //ViewBag.accion = 1;

            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(SoporteModel soporte)
        {
            repo.AgregaActualizaSoporte(soporte);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int p_Id)
        {
            ViewBag.areas = repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 1, 0);
            ViewBag.resguardante = repo.Get<ResguardanteConsultaViewmodel>("sp_obter_resguardantes_mantenimiento").ToList();
            ViewBag.servicios = repo.Getdosparam<Servicio>("sp_obtener_servicios", 1, 0);
            ViewBag.atender = repo.Getdosparam<TestigoViewModel>("sp_obtener_testigo", 1, 0).ToList();

            return View(repo.Getdosparam<SoporteModel>("sp_obtener_soportes", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(SoporteModel soporte)
        {
            repo.AgregaActualizaSoporte(soporte);
            return RedirectToAction("Index");
        }
    }
}
