using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;

namespace net8Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        #region BD
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        #endregion
        #region MetodosGetVistas
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }
        #endregion
        #region logica
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {

                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.save();

                return RedirectToAction(nameof(Index));
            }
            return View(categoria);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _contenedorTrabajo.Categoria.Get(id);

            if(categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        #endregion


        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll() {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id) {

            var objFromDb = _contenedorTrabajo.Categoria.Get(id);

            if (objFromDb == null) { 
                return Json(new {success = false, message = "error borrando categoria"});
            }

            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.save();
            return Json(new { success = true, message = "Categoria eliminada correctamente" });
        }
        #endregion
    }
}
