using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using net8Proyect.Data;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;
using net8Proyect.Models.ViewModels;

namespace net8Proyect.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class ArticuloController : Controller
    {
       
        #region ContenedorTrabajo
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticuloController(IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;

        }
        #endregion

        #region Vistas

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new net8Proyect.Models.Articulo(),
                ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(ArticuloVM artiVM)
        {

            if (ModelState.IsValid) {

                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id);

                if (artiVM.Articulo.Id == 0 && archivos.Count() > 0)
                {

                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\Articulo");
                    var extension = Path.Combine(archivos[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    };
                    artiVM.Articulo.UrlImagen = @"\imagenes\Articulo\" + nombreArchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.save();

                    return RedirectToAction(nameof(Index));
                }
                else {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }
            artiVM.ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)
        {

            if (ModelState.IsValid)
            {

                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id);

                //si . count es mayor que 0 es que se quiere reemplazar la imagen
                if (archivos.Count() > 0)
                {

                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\Articulo");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));

                    //si eliminar la imagen existente
                    if (!System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    //subir nueva imagen
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    };

                    artiVM.Articulo.UrlImagen = @"\imagenes\Articulo\" + nombreArchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    artiVM.Articulo.UrlImagen = articuloDesdeBd.UrlImagen;
                }

                _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                _contenedorTrabajo.save();

                return RedirectToAction(nameof(Index));
            }

            artiVM.ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(artiVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new net8Proyect.Models.Articulo(),
                ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            if (id != null) { 
                artiVM.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }
            return View(artiVM);
        }

        //[HttpGet]
        //public IActionResult Carrito(int id) {
        
        //}

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
