using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proyecto.Context;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Controllers
{
    public class ArticuloController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

    
        public ArticuloController(ApplicationDbContext context)
        {
            _context = context;

        }



        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        //SpGetArticulos

        SqlConnection connection = new SqlConnection("Data Source=Majo; initial catalog = ProyectoMj; Integrated Security=True;");

    [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //await _context.Articulos.ToListAsync():
                //var response = await connection.QueryAsync<Articulo>("SpGetArticulos", new { }, commandType: CommandType.StoredProcedure);

                var response = await connection.QueryAsync<Articulo>("SpGetArticulos", new { }, commandType: CommandType.StoredProcedure);

                return View(response);

            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error " + ex.Message);
            }
        }




        [HttpPost]
        public async Task<IActionResult> Crear(Articulo response)
        {

            try
            {


                //await connection.QueryAsync<Articulo>("spInsertArticulo", new { response.Nombre, response.Descripcion, response.UrlImg }, commandType: CommandType.StoredProcedure);

                //string rutaprincipal = _webHost.WebRootPath;

                //var archivos = HttpContext.Request.Form.Files;
                //var nombreArchivo = response.UrlImg;

                //var subidas = Path.Combine(rutaprincipal, @"Imagen");


                Articulo articulo = new Articulo();

                articulo.Nombre = response.Nombre;
                articulo.Descripcion = response.Descripcion;
                articulo.UrlImg = response.UrlImg;


                _context.Articulos.Add(articulo);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error "+ex.Message);
            }

        }
    }
}
