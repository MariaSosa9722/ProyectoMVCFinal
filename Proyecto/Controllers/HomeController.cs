using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario response)
        {


            Usuario usuario = _context.Usuarios.Find(response.PkUsuario);

            usuario.Nombre = response.Nombre;
            usuario.User = response.User;
            usuario.FkRol = 1;

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }

        public async Task<IActionResult> DeleteUser (int Id)
        {
            var user = _context.Usuarios.Find(Id);

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



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
