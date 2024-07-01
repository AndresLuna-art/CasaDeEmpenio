using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CasaDeEmpeño.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CasaDeEmpeño.Controllers
{
    public class OfertasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfertasController
        [Authorize]
        public ActionResult Index()
        {
            var productosConOfertas = _context.Productos
             .Where(p => p.EstadoRegistro == true && p.Ofertas.Any(o => o.EstadoRegistro == true))
             .Select(p => new ProductoViewModel
             {
                 ProductoID = p.ProductoID,
                 NombreProducto = p.NombreProducto,
                 ValorCalculado = p.ValorCalculado,
                 Ofertas = p.Ofertas
                     .Where(o => o.EstadoRegistro == true)
                     .Select(o => new OfertaViewModel
                     {
                         OfertaID = o.OfertaID,
                         NombreOfertante = o.NombreOfertante,
                         NumeroCelular = o.NumeroCelular,
                         MontoOferta = o.MontoOferta,
                         FechaOferta = o.FechaOferta,
                         EstadoRegistro = o.EstadoRegistro
                     }).ToList()
             })
             .ToList();

            // Marcar la mejor oferta de todas
            foreach (var producto in productosConOfertas)
            {
                var mejorOferta = producto.Ofertas
                    .OrderByDescending(o => o.MontoOferta)
                    .FirstOrDefault();

                if (mejorOferta != null)
                {
                    mejorOferta.EsMejorOferta = true;
                }
            }

            return View(productosConOfertas);
        }

        [Authorize]
        [HttpPost]
        public IActionResult QuitarOferta(int OfertaID)
        {
            var oferta = _context.Ofertas.Find(OfertaID);
            if (oferta != null)
            {
                oferta.EstadoRegistro = false;  
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "La oferta no se encontró." });
        }

        // GET: OfertasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OfertasController/Create
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
                .Where(p => p.FechaVencimientoDevolucion < fechaActual && !_context.Devoluciones.Any(d => d.ProductoID == p.ProductoID))
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

        // POST: OfertasController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Oferta Model)
        {
            try
            {
                Oferta nuevaOferta = new Oferta
                {
                    ProductoID = Model.ProductoID,
                    NombreOfertante = Model.NombreOfertante,
                    NumeroCelular = Model.NumeroCelular,
                    MontoOferta = Model.MontoOferta,
                    EstadoRegistro = true,
                    FechaOferta = Model.FechaOferta
                };

                _context.Ofertas.Add(nuevaOferta);
                _context.SaveChanges();

                return Json(new { success = true, message = "¡Oferta guardada correctamente!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar la oferta. Por favor, inténtelo de nuevo." });
            }
        }

        // GET: OfertasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OfertasController/Edit/5
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

        // GET: OfertasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OfertasController/Delete/5
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
