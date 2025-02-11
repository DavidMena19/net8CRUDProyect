using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net8Proyect.Data.Data.Repository.IRepository;
using System.Security.Claims;

namespace net8Proyect.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsuarioController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuarioController( IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //return View( _contenedorTrabajo.Usuario.GetAll());

            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_contenedorTrabajo.Usuario.GetAll(u=> u.Id != usuarioActual.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id) {

            if (id == null) {
                return NotFound();           
            }
            _contenedorTrabajo.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Desbloquear(string id) {

            if (id == null) {

                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloquearUsurio(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
