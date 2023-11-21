using System;
using System.Collections.Generic;

namespace GSI.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Stock> Stocks { get; } = new List<Stock>();

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
