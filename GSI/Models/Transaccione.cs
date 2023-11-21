using System;
using System.Collections.Generic;

namespace GSI.Models;

public partial class Transaccione
{
    public int TransaccionId { get; set; }

    public int? ProductoId { get; set; }

    public string? Tipo { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Producto? Producto { get; set; }
}
