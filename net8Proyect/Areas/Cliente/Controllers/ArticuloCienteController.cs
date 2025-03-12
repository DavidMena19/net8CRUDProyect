using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Primitives;
using net8Proyect.Data;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;
using net8Proyect.Models.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;  
using System.Linq;  


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

        public IActionResult Carrito()
        {
            var cuentaUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (cuentaUsuario == null)
            {
                return Json(new { success = false, message = "Debe de registrarse para acceder a esta pagina" });
            }

            var carritoUsuario = _contenedorTrabajo.Carrito.GetAll(
                u => u.ClienteId == cuentaUsuario, includeProperties: "Detalles.Articulo").ToList();

            if (carritoUsuario == null)
            {
                return View(new CarritoVM { Detalles = new List<CarritoDetalle>() });
            }

            var carritoVM = new CarritoVM
            {
                Carritos = carritoUsuario
            };

            return View(carritoVM);
        }

        #endregion

        #region Logica sin vistas

        [HttpGet]
        public IActionResult AgregarAlCarrito(int articuloId)
        {
            // Obtener usuario autenticado
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuario == null)
                return Json(new { success = false, message = "Debe iniciar sesión para poder comprar un artículo" });

            // Buscar el artículo
            var articulo = _contenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == articuloId);
            if (articulo == null)
                return Json(new { success = false, message = "El artículo no existe" });

            // Obtener el carrito del usuario con sus detalles
            var carrito = _contenedorTrabajo.Carrito.GetFirstOrDefault(c => c.ClienteId == usuario, includeProperties: "Detalles");

            if (carrito == null)
            {
                // Si el carrito no existe, lo creamos
                carrito = new Carrito
                {
                    ClienteId = usuario,
                    Detalles = new List<CarritoDetalle>(),
                    Cantidad = 0,
                    
                };
                _contenedorTrabajo.Carrito.Add(carrito);
                _contenedorTrabajo.save(); // Guardamos el carrito para obtener su ID
            }
            var detalleExistente = _contenedorTrabajo.CarritoDetalle.GetFirstOrDefault(d => d.ArticuloId == articuloId && d.UsuarioId == usuario);

            if (detalleExistente != null)
            {
                // Si ya existe, incrementar la cantidad
                detalleExistente.Cantidad++;
                carrito.Cantidad++;
                _contenedorTrabajo.Carrito.Update(carrito);
                _contenedorTrabajo.CarritoDetalle.Update(detalleExistente);
            }
            else
            {
                // Si el artículo no está en el Detalle, agregarlo
                var nuevoDetalle = new CarritoDetalle
                {
                    ArticuloId = articulo.Id,
                    UsuarioId = usuario,
                    Nombre = articulo.Nombre,
                    PrecioUnitario = articulo.Precio,
                    Cantidad = 1,
                };
                carrito.Cantidad++;
                carrito.Detalles.Add(nuevoDetalle);
                _contenedorTrabajo.CarritoDetalle.Add(nuevoDetalle);

            }
            carrito.Total += articulo.Precio;         
            _contenedorTrabajo.Carrito.Update(carrito);
            _contenedorTrabajo.save();
            return Json(new { success = true, message = "Artículo agregado al carrito" });
        }

        public IActionResult CantidadEnCarrito()
        {
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuario == null)
                return Json(new { cantidad = 0 });

            int cantidad = _contenedorTrabajo.Carrito.Count(usuario);
            return Json(new { cantidad });
        }

        #endregion

        #region Llamadas a la API

        [HttpGet]
      
        //To Do: no se carga el precio ni la categoria en el datatable
        [HttpGet]
            public IActionResult GetAll()
            {
            var cuentaUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carritoUsuario = _contenedorTrabajo.CarritoDetalle.GetAll()
                                    .Where(c => c.UsuarioId == cuentaUsuario)
                                    .Select(c => new
                                    {
                                        c.Id,
                                        c.Nombre,
                                        c.Cantidad,
                                        c.PrecioUnitario
                                    }).ToList();

            return Json(new { data = carritoUsuario });
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

