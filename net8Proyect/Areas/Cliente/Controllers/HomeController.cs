using Microsoft.AspNetCore.Mvc;
using net8Proyect.Models;
using System.Diagnostics;

namespace net8Proyect.Areas.Cliente.Controllers
{
    //esto antes no lo tenia y es que como hay dos areas, para que el program cs no se confunda pues tuvimos que ponerle que por defecto abra la pagina de client, no la de administrador, entonces asi le decimos a este controlador
    //que es el de cliente.
    [Area("Cliente")]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
