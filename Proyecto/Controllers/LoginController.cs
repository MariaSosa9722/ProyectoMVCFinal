using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Context;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public System.Web.Mvc.JsonRequestBehavior JsonRequestBehavior { get; set; }

        public LoginController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articulos.ToListAsync());
        }

        [HttpPost]
        public JsonResult LoginUser(string user, string password)
        {
            try
            {
                var response = _context.Usuarios.Include(z => z.roles)
                                .FirstOrDefault(x => x.User == user &&
                                x.Password == password);


                if (response != null )
                {
                    if (response.roles.Nombre == "admin")
                    {
                        return Json(new { Success = true, admin = true });
                    }
                    return Json(new { Success = true, admin = false });
                }
                else
                {
                    return Json(new { Success = false, admin= false });
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Error msj " + ex.Message);
            }
        }
    }
}
