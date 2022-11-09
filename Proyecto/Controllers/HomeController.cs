using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proyecto.Context;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.Include(z => z.roles).ToListAsync());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> ListaRol()
        //{


        //    mbox  
        //}

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario response)
        {
            if (response != null)
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = response.Nombre,
                    User = response.User,
                    Password = response.Password,
                    FkRol = 1
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));  
                

            }
          
            return View();
        }


        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Usuario = _context.Usuarios.Find(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            return View(Usuario);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
