using System;
using System.Collections.Generic;

namespace DL;

public partial class RegistroActividad
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Tipo { get; set; }
}
