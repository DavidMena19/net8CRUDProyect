using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net8Proyect.Data.Data.Repository;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;
using net8Proyect.Models.ViewModels;

namespace net8Proyect.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SliderController : Controller
    {
        #region ContenedorTrabajo
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SliderController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

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

        public IActionResult Create(Slider slider)
        {

            if (ModelState.IsValid)
            {

                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                //var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id);

                if (archivos.Count() > 0)
                {

                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\Slider");
                    var extension = Path.Combine(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    };

                    slider.UrlImagen = @"\imagenes\Slider\" + nombreArchivo + extension;
                    //artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }
            //artiVM.ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(slider);
        }

        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {

                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeBd = _contenedorTrabajo.Slider.Get(slider.id);

                if (archivos.Count() > 0)
                {

                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\Slider");
                    var extension = Path.Combine(archivos[0].FileName);

                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeBd.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    };

                    slider.UrlImagen = @"\imagenes\Slider\" + nombreArchivo + extension;

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderDesdeBd.UrlImagen;
                }

                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.save();
                return RedirectToAction(nameof(Index));

            }
            return View(slider);
        }

        #region Llamados API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _contenedorTrabajo.Slider.Get(id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "error borrando Slider" });
            }

            _contenedorTrabajo.Slider.Remove(objFromDb);
            _contenedorTrabajo.save();
            return Json(new { success = true, message = "Slider eliminado correctamente" });
        }
        #endregion
    }
}
