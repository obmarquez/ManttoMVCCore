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
    public class RolController : Controller
    {
        private DBOperaciones repo;

        public RolController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            IEnumerable<Roles> datos = repo.Get<Roles>("sp_obtener_roles").ToList();
            return View(datos); 
        }
    }
}
