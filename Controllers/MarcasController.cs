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
    public class MarcasController : Controller
    {
        private DBOperaciones repo;

        public MarcasController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<MarcasViewModel> datos = repo.Getdosparam<MarcasViewModel>("sp_obtener_marcas", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Editar(int p_Id)
        {
            //ViewBag.p_Id = p_Id;
            return View(repo.Getdosparam<MarcasViewModel>("sp_obtener_marcas", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(MarcasViewModel marcaModel)
        {
            //marcaModel.opcion = 2;
            repo.AgregaActualizaMarca(marcaModel);
            return RedirectToAction("Index");
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(MarcasViewModel marcaModel)
        {
            repo.AgregaActualizaMarca(marcaModel);
            return RedirectToAction("Index");
        }
    }
}
