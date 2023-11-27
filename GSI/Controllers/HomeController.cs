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

            var productosPaginados = await PaginatedList<ProductoViewModel>.CreateAsync(
                productosQuery.Select(p => new ProductoViewModel
                {
                    ProductoID = p.ProductoId,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    CantidadStock = p.Stocks.Sum(s => s.Cantidad), 
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
            using (var transaction = _context.Database.BeginTransaction())
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

                    int cantidadOriginal = productoStock?.Cantidad ?? 0;
                    int cantidadAjustada = nuevoStock - cantidadOriginal;

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

                    // Crear la transacción
                    var transaccion = new Transaccione
                    {
                        ProductoId = id,
                        Tipo = cantidadAjustada > 0 ? "Entrada" : "Salida",
                        Cantidad = Math.Abs(cantidadAjustada),
                        Fecha = DateTime.Now
                    };

                    _context.Transacciones.Add(transaccion);
                    await _context.SaveChangesAsync(); // Confirma los cambios en la base de datos
                    await transaction.CommitAsync();

                    return Json(new { success = true, message = "Stock actualizado correctamente." });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al ajustar stock");

                    return Json(new { success = false, message = "Error al actualizar el stock." });
                }
            }
        }

        public async Task<IActionResult> DashboardTransacciones()
        {
            // Obtener las transacciones del día
            var transacciones = await _context.Transacciones
                        .ToListAsync();

            // Agrupar las transacciones por día
            var transaccionesPorDia = transacciones
                        .GroupBy(t => t.Fecha.HasValue ? t.Fecha.Value.Date : DateTime.MinValue)
                        .Select(g => new { Fecha = g.Key, NumeroDeTransacciones = g.Count() })
                        .OrderBy(g => g.Fecha)
                        .ToList();

            // Preparar los datos para el gráfico
            ViewBag.Fechas = transaccionesPorDia.Select(t => t.Fecha.ToString("dd/MM/yyyy")).ToList();
            ViewBag.NumeroDeTransacciones = transaccionesPorDia.Select(t => t.NumeroDeTransacciones).ToList();

            return View(transacciones);
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