using ManttoMVCCore.Data;
using ManttoMVCCore.Helper;
using ManttoMVCCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DBOperaciones repo;
        CodeStackCTX ctx;

        private readonly ILogger<HomeController> _logger;

        public HomeController(CodeStackCTX _ctx)
        {
            repo = new DBOperaciones();
            ctx = _ctx;
            //_logger = logger;
        }

        public IActionResult Index()
        {            
            ViewBag.listaMantenimiento = repo.Getunparam<Consultas>("sp_obtener_consultas_generales", 1).ToList();
            ViewBag.listaRaliza = repo.Getunparam<Consultas>("sp_obtener_consultas_generales", 2).ToList();
            ViewBag.listaSoporte = repo.Get<Consultas>("sp_grafica_soporte").ToList();

            return View(repo.Getunparam<Consultas>("sp_obtener_consultas_generales", 1).ToList());
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Registro()
        {
            return View();
        }

        [BindProperty]
        public Usuarios Usuario { get; set; }
        public async Task<IActionResult> Registrar()
        {
            var result = await ctx.Usuarios.Where(x => x.Nombre == Usuario.Nombre).SingleOrDefaultAsync();
            if (result != null)
            {
                return BadRequest(new JObject() {
                    { "Statuscode",  400 },
                    { "Message", "El usuario ya existe seleccione otro."  }
                });
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.SelectMany(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList());
                }
                else
                {
                    var hash = HashHelper.Hash(Usuario.Clave);
                    Usuario.Clave = hash.Password;
                    Usuario.Sal = hash.Salt;
                    ctx.Usuarios.Add(Usuario);
                    await ctx.SaveChangesAsync();
                    Usuario.Clave = "";
                    Usuario.Sal = "";
                    return Created($"/Usuarios/{Usuario.IdUsuario}", Usuario);
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
