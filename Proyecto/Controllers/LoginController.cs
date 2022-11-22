using Microsoft.AspNetCore.Mvc;
using Proyecto.Context;
using System;
using System.Linq;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginUser(string user, string password)
        {
            try
            {
                var response = _context.Usuarios.Where(x => x.User == user && x.Password == password).ToList();
                if (response.Count() > 0)
                {
                    return Json(new { Success = true });

                }
                else
                {
                    return Json(new { Success = false });
                }



            }
            catch (Exception ex)
            {

                throw new Exception("Error msj " + ex.Message);
            }
        }
    }
}
