using ManttoMVCCore.Data;
using ManttoMVCCore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        private DBOperaciones repo;

        public ConsultasController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Asistente")]
        public IActionResult IndexSoporte(string fecha1 = "", string fecha2 = "")
        {
            if (fecha1 == "" && fecha2 == "")
                return View();
            else
                return View(repo.Getdosparam1<SoporteVM>("sp_consultas_rango_fecha_opcion", new { @fecha1 = fecha1, @fecha2 = fecha2, @opcion = 1 }).ToList());
        }
    }
}
