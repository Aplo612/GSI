using GSI.Models;
using GSI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GSI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly InventarioproyectogsiContext _context;


        public ProductoController(InventarioproyectogsiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index(int? pagina, string categoriaSeleccionada)
        {
            const int tamanoDePagina = 10;
            var numeroDePagina = pagina ?? 1;

            // Obtener la lista de categorías para el dropdown de filtrado
            var categorias = await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync();

            IQueryable<Producto> productosQuery = _context.Productos
                .Include(p => p.Categoria)
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
                    Categoria = p.Categoria.Nombre,
                    CategoriaID = p.CategoriaId,
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

            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "Nombre");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            try
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                TempData["ErrorMessage"] = "Ocurrió un error al guardar el producto.";
            }

            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProducto(Producto producto)
        {
            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Producto editado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                TempData["ErrorMessage"] = "Ocurrió un error al editar el producto.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "Producto no encontrado." });
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Producto eliminado correctamente." });
        }

        
    }
}
