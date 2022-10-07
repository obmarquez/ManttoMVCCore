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
    public class ResguardantesController : Controller
    {
        private DBOperaciones repo;

        public ResguardantesController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<ResguardanteConsultaViewmodel> datos = repo.Getdosparam<ResguardanteConsultaViewmodel>("sp_obtener_resguardantes", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Editar(int p_Id)
        {
            //IEnumerable<AreaViewModel> datosArea = repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 1, 0);
            //ViewData["lasAreas"] = datosArea.ToList();

            var lasAreas = repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 1, 0);
            var lasAreaslista = new SelectList(lasAreas, "id", "area");
            ViewData["lasAreas"] = lasAreaslista;

            return View(repo.Getdosparam<ResguardanteViewModel>("sp_obtener_resguardantes", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(ResguardanteViewModel resguardanteModel)
        {
            repo.AgregaActualizaResguardante(resguardanteModel);
            return RedirectToAction("Index");
        }

        public IActionResult Nuevo()
        {
            var lasAreas = repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 1, 0);
            var lasAreaslista = new SelectList(lasAreas, "id", "area");
            ViewData["lasAreas"] = lasAreaslista;

            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(ResguardanteViewModel resguardanteModel)
        {
            repo.AgregaActualizaResguardante(resguardanteModel);
            return RedirectToAction("Index");
        }
    }
}
