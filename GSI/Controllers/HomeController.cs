using GSI.Models;
using GSI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GSI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InventarioproyectogsiContext _context;


        public HomeController(InventarioproyectogsiContext context, ILogger<HomeController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Stocks)
                .Select(p => new ProductoViewModel
                {
                    ProductoID = p.ProductoId,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    CantidadStock = p.Stocks.Sum(s => s.Cantidad), // Asume una relación uno a uno
                    Categoria = p.Categoria.Nombre,
                    // Aquí puedes calcular el estado o incluir transacciones si es necesario
                })
                .ToListAsync();

            return View(productos);
        }

        public async Task<IActionResult> EditarAsync(int id)
        {
            
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AjustarStock(int id, int nuevoStock)
        {
            try
            {
                var productoStock = await _context.Stocks
                    .FirstOrDefaultAsync(p => p.ProductoId == id);

                if (productoStock == null)
                {
                    return Json(new { success = false, message = "Producto no encontrado." });
                }

                productoStock.Cantidad = nuevoStock;
                await _context.SaveChangesAsync(); // Confirma los cambios en la base de datos

                return Json(new { success = true, message = "Stock actualizado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ajustar stock");

                return Json(new { success = false, message = "Error al actualizar el stock." });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}