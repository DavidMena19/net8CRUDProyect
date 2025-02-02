using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace net8Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {

       private readonly IContenedorTrabajo _contenedorTrabajo;
        public IActionResult Index()
        {
            return View();
        }
    }
}
