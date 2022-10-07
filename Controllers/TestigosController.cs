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
    public class TestigosController : Controller
    {
        private DBOperaciones repo;

        public TestigosController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult IndexConsulta()
        {
            IEnumerable<TestigoViewModel> datos = repo.Getdosparam<TestigoViewModel>("sp_obtener_testigo", 1, 0);
            return View(datos.ToList());
        }

        public IActionResult Editar(int p_Id)
        {
            return View(repo.Getdosparam<TestigoViewModel>("sp_obtener_testigo", 2, p_Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Editar(TestigoViewModel testigoModel)
        {
            repo.AgregaActualizaTestigos(testigoModel);
            return RedirectToAction("IndexConsulta");
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(TestigoViewModel testigoModel)
        {
            repo.AgregaActualizaTestigos(testigoModel);
            return RedirectToAction("IndexConsulta");
        }
    }
}
