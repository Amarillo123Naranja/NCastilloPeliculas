﻿using System;
using System.Collections.Generic;

namespace DLCore;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Nombre { get; set; }

    public string? Password { get; set; }
}
