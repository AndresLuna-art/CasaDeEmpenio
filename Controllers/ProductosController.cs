using CasaDeEmpeño.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CasaDeEmpeño.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string DateFormat = "dd/MM/yyyy HH:mm:ss";

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductosController
        [Authorize]
        public ActionResult Index()
        {
            var productos = _context.Productos
            .Select(p => new Producto
            {
                ProductoID = p.ProductoID,
                TipoProducto = p.TipoProducto,
                NombreProducto = p.NombreProducto,
                EstadoProducto = p.EstadoProducto,
                FechaIngreso = p.FechaIngreso,
                EstadoRegistro = p.EstadoRegistro,
                ValorCalculado = p.ValorCalculado,
                FechaVencimientoDevolucion = DateTime.Parse(p.FechaVencimientoDevolucion.ToString())
            })
            .ToList();

            return View(productos);
        }

        // GET: ProductosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: ProductosController/Create
        [Authorize]
        public ActionResult Add()
        {
            var tiposDeProductos = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccionar" },
                new SelectListItem { Value = "Electrónica", Text = "Electrónica" },
                new SelectListItem { Value = "Cocina", Text = "Cocina" },
                new SelectListItem { Value = "Línea blanca", Text = "Línea blanca" },
                new SelectListItem { Value = "Video Juego", Text = "Video Juego" },
                new SelectListItem { Value = "Herramientas", Text = "Herramientas" },
                new SelectListItem { Value = "Ropa y Accesorios", Text = "Ropa y Accesorios" }
            };

            var EstadoProductos = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccionar" },
                new SelectListItem { Value = "Nuevo", Text = "Nuevo" },
                new SelectListItem { Value = "Usado - Buen estado", Text = "Usado - Buen estado" },
                new SelectListItem { Value = "Usado - Desgastado", Text = "Usado - Desgastado" },
                new SelectListItem { Value = "Para reparar", Text = "Para reparar" }
            };

            ViewBag.TipoProducto = new SelectList(tiposDeProductos, "Value", "Text");
            ViewBag.EstadoProducto = new SelectList(EstadoProductos, "Value", "Text");

            return View();
        }

        // POST: ProductosController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Producto model)
        {
            try
            {
                Producto nuevoProducto = new Producto
                {
                    TipoProducto = model.TipoProducto,
                    NombreProducto = model.NombreProducto,
                    EstadoProducto = model.EstadoProducto,
                    FechaIngreso = model.FechaIngreso,
                    ValorCalculado = model.ValorCalculado,
                    EstadoRegistro = true,
                    FechaVencimientoDevolucion = model.FechaVencimientoDevolucion
                };

                _context.Productos.Add(nuevoProducto);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "¡Producto guardado correctamente!";
                TempData["ToastType"] = "success";
                TempData["ShowToast"] = true;

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el producto. Por favor, inténtelo de nuevo.");
                TempData["ToastType"] = "error";
                TempData["ShowToast"] = true;

                return Json(new { success = true });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult VenderProducto(int ProductoID, int OfertaID)
        {
            var producto = _context.Productos.Find(ProductoID);
            var oferta = _context.Ofertas.Find(OfertaID);

            if (producto != null && oferta != null)
            {
                producto.EstadoRegistro = false;  
                oferta.EstadoRegistro = false;   
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Producto o oferta no se encontró." });
        }

        // GET: ProductosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
