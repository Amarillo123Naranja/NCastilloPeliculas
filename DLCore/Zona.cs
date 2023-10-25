using System;
using System.Collections.Generic;

namespace DLCore;

public partial class Zona
{
    public int IdZona { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Cine> Cines { get; set; } = new List<Cine>();

    
}
