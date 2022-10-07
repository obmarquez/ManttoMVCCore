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
    public class EquiposController : Controller
    {
        private DBOperaciones repo;

        public EquiposController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            IEnumerable<EquipoViewModel> datos = repo.Getdosparam<EquipoViewModel>("sp_obtener_inventario", 1, 0).ToList();

            return View(datos.ToList());
        }
    }
}
