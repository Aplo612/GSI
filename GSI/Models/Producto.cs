using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GSI.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "Ingrese una descripción.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El precio del producto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "La categoría del producto es obligatoria.")]
    public int? CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Stock> Stocks { get; } = new List<Stock>();

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
