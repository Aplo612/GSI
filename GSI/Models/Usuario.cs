using System;
using System.Collections.Generic;

namespace GSI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string HashContraseña { get; set; } = null!;
}
