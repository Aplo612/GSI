using System;
using System.Collections.Generic;

namespace GSI.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto? Producto { get; set; }
}
