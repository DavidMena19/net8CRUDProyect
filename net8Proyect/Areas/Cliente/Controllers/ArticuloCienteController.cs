using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Primitives;
using net8Proyect.Data;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;
using net8Proyect.Models.ViewModels;
using System.Security.Claims;

namespace net8Proyect.Areas.Cliente.Controllers
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
        public IActionResult Carrito(int usuarioId)
        {
            if (usuarioId == null || usuarioId == 0)
            {
                return RedirectToAction("Index", "Home"); // Redirige a una página principal en lugar de un 404
            }

            // Verifica si el usuario existe
            var usuariose = _contenedorTrabajo.Articulo.Get(usuarioId);
            if (usuariose == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Verifica si el carrito para este usuario existe
            var buscarCarritoUsuario = _contenedorTrabajo.Carrito.GetFirstOrDefault(/*u => u.ClienteId = usuarioId*/);
            if (buscarCarritoUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Si todo está bien, retorna la vista
            return View(buscarCarritoUsuario);
        }



        #endregion

        #region Logica sin vistas

        [HttpGet]
        public IActionResult AgregarAlCarrito(int articuloId)
        {
            //obtener usuario de la seccion
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(usuario == null)
            {
                return Json(new { success = false, message = "Debe iniciar seccion para poder comprar un articulo" });
            }

            // Obtener el artículo desde la base de datos
            var articulo = _contenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == articuloId);
            if (articulo == null)
            {
                return Json(new { success = false, message = "El artículo no existe" });
            }

            // Obtener el carrito del usuario
            var carrito = _contenedorTrabajo.Carrito.GetFirstOrDefault(c => c.ClienteId == usuario);
            if (carrito == null)
            {
                // Si no existe el carrito, lo creamos
                carrito = new Carrito
                {
                    ClienteId = usuario,
                    Detalles = new List<CarritoDetalle>()
                };
                _contenedorTrabajo.Carrito.Add(carrito);
                _contenedorTrabajo.save(); 
            }

            // Verificar si el artículo ya está en el carrito
            var detalleExistente = carrito.Detalles.FirstOrDefault(d => d.ArticuloId == articuloId);
            if (detalleExistente != null)
            {
                // Si ya existe, solo aumentamos la cantidad
                detalleExistente.Cantidad++;
            }
            else
            {
                // Si no existe, agregamos el nuevo artículo al carrito
                carrito.Detalles.Add(new CarritoDetalle
                {
                    ArticuloId = articulo.Id,
                    UsuarioId = usuario,
                    Nombre = articulo.Nombre,
                    PrecioUnitario = articulo.Precio,
                    Cantidad = 1
                });
            }

            _contenedorTrabajo.save(); // Guardamos los cambios

            return Json(new { success = true, message = "Artículo agregado al carrito" });
        }

        #endregion

        #region Llamadas a la API
        //To Do: no se carga el precio ni la categoria en el datatable
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Carrito.GetAll(includeProperties: "Categoria") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Carrito.Get(id);
            
            _contenedorTrabajo.Carrito.Remove(articuloDesdeBd);
            _contenedorTrabajo.save();
            return Json(new { success = true, message = "articulo eliminada correctamente" });
        }
        #endregion

    }
}
