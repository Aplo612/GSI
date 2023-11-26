using GSI.Models;
using GSI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index(int? pagina, string categoriaSeleccionada)
        {

            const int tamanoDePagina = 10;
            var numeroDePagina = pagina ?? 1;

            // Obtener la lista de categorías para el dropdown de filtrado
            var categorias = await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync();

            IQueryable<Producto> productosQuery = _context.Productos
                                                        .Include(p => p.Categoria)
                                                        .Include(p => p.Stocks)
                                                        .OrderBy(p => p.ProductoId);

            if (!string.IsNullOrEmpty(categoriaSeleccionada))
            {
                productosQuery = productosQuery.Where(p => p.Categoria.Nombre == categoriaSeleccionada);
            }

            // Creamos una instancia de PaginatedList con la consulta a la base de datos
            var productosPaginados = await PaginatedList<ProductoViewModel>.CreateAsync(
                productosQuery.Select(p => new ProductoViewModel
                {
                    ProductoID = p.ProductoId,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    CantidadStock = p.Stocks.Sum(s => s.Cantidad), // Asegúrate de que esta lógica es correcta para tus necesidades
                    Categoria = p.Categoria.Nombre,
                }),
                numeroDePagina,
                tamanoDePagina
            );

            var viewModel = new IndexViewModel
            {
                Productos = productosPaginados,
                Categorias = new SelectList(categorias, "Nombre", "Nombre"),
                CategoriaSeleccionada = categoriaSeleccionada
            };

            return View(viewModel);
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
                var producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.ProductoId == id);

                if (producto == null)
                {
                    return Json(new { success = false, message = "Producto no encontrado." });
                }

                var productoStock = await _context.Stocks
                    .FirstOrDefaultAsync(p => p.ProductoId == id);

                if (productoStock == null)
                {
                    productoStock = new Stock
                    {
                        ProductoId = id,
                        Cantidad = nuevoStock // Asegúrate de que 'nuevoStock' esté definido y sea el valor correcto que deseas asignar
                    };
                    _context.Stocks.Add(productoStock);
                }
                else
                {
                    productoStock.Cantidad = nuevoStock;
                }
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