using ManttoMVCCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Controllers
{
    public class UsuariosController : Controller
    {
        readonly CodeStackCTX ctx;

        public UsuariosController(CodeStackCTX _ctx)
        {
            ctx = _ctx;
        }

        public async Task<IActionResult> Index()
        {
            return Ok(await ctx.Usuarios.Include("Roles.Rol").ToListAsync());
        }
    }
}
