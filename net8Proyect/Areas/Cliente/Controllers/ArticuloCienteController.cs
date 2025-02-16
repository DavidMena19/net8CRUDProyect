using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using net8Proyect.Data;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;
using net8Proyect.Models.ViewModels;

namespace net8Proyect.Areas.Admin.Controllers
{
 
    [Area("Cliente")]

    public class ArticuloClienteController : Controller
    {
       
        #region ContenedorTrabajo
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticuloClienteController(IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;

        }
        #endregion

        #region Vistas



        [HttpGet]
        public IActionResult Carrito(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            if(articuloDesdeBd == null)
            {
                return NotFound();
            }
            return View(articuloDesdeBd);
        }

        #endregion

        #region Llamadas a la API
        //To Do: no se carga el precio ni la categoria en el datatable
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            if (articuloDesdeBd == null)
            {
                return Json(new { success = false, message = "error borrando Articulo" });
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeBd);
            _contenedorTrabajo.save();
            return Json(new { success = true, message = "articulo eliminada correctamente" });
        }
        #endregion

    }
}
