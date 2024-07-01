using CasaDeEmpeño.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CasaDeEmpeño.Controllers
{
    public class DevolucionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevolucionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DevolucionesController
        [Authorize]
        public ActionResult Index()
        {
            var devoluciones = _context.Devoluciones
            .Join(
                _context.Productos,
                d => d.ProductoID,
                p => p.ProductoID,
                (d, p) => new DevolucionViewModel
                {
                    DevolucionID = d.DevolucionID,
                    ProductoID = d.ProductoID,
                    Comentario = d.Comentario,
                    FechaDevolucion = d.FechaDevolucion,
                    NombreProducto = p.NombreProducto
                }
            )
            .ToList();

            return View(devoluciones);
        }

        // GET: DevolucionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DevolucionesController/Create
        [Authorize]
        public ActionResult Add()
        {
            var productos = _context.Productos
            .Select(p => new
            {
                p.ProductoID,
                p.NombreProducto,
                FechaVencimientoDevolucion = DateTime.Parse(p.FechaVencimientoDevolucion.ToString())
            })
            .ToList();

            var fechaActual = DateTime.Now;

            var productosDisponibles = productos
                .Where(p => p.FechaVencimientoDevolucion > fechaActual &&
                            !_context.Devoluciones.Any(d => d.ProductoID == p.ProductoID) &&
                            !_context.Ofertas.Any(o => o.ProductoID == p.ProductoID))
                .Select(p => new
                {
                    p.ProductoID,
                    p.NombreProducto
                })
                .ToList();

            productosDisponibles.Insert(0, new { ProductoID = 0, NombreProducto = "Seleccione" });
            ViewBag.Productos = new SelectList(productosDisponibles, "ProductoID", "NombreProducto");

            return View();
        }

        // POST: DevolucionesController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Devolucion Model)
        {
            try
            {
                Devolucion nuevaDevolucion = new Devolucion
                {
                    ProductoID = Model.ProductoID,
                    Comentario = Model.Comentario,
                    EstadoRegistro = true,
                    FechaDevolucion = Model.FechaDevolucion
                };

                _context.Devoluciones.Add(nuevaDevolucion);
                _context.SaveChanges();

                return Json(new { success = true, message = "¡Devolución guardada correctamente!" });
            
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar la devolución. Por favor, inténtelo de nuevo." });
            }
        }

        // GET: DevolucionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DevolucionesController/Edit/5
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

        // GET: DevolucionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DevolucionesController/Delete/5
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
