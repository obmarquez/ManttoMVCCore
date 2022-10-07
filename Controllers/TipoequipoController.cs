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
    public class TipoequipoController : Controller
    {
        private DBOperaciones repo;

        public TipoequipoController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<TipoequipoViewModel> datos = repo.Getdosparam<TipoequipoViewModel>("sp_obtener_tipoEquipo", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Editar(int p_Id)
        {
            return View(repo.Getdosparam<TipoequipoViewModel>("sp_obtener_tipoEquipo", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(TipoequipoViewModel tipoModel)
        {

            repo.AgregaActualizaTipoEquipo(tipoModel);
            return RedirectToAction("Index");
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(TipoequipoViewModel tipoModel)
        {
            repo.AgregaActualizaTipoEquipo(tipoModel);
            return RedirectToAction("Index");
        }
    }
}
