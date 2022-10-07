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
    public class AreasController : Controller
    {
        private DBOperaciones repo;

        public AreasController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<AreaViewModel> datos = repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Editar(int p_Id)
        {
            return View(repo.Getdosparam<AreaViewModel>("sp_obtener_areas", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(AreaViewModel areaModel)
        {
            repo.AgregaActualizaArea(areaModel);
            return RedirectToAction("Index");
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(AreaViewModel areaModel)
        {
            repo.AgregaActualizaArea(areaModel);
            return RedirectToAction("Index");
        }
    }
}
