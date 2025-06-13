using System;
using System.Collections.Generic;

namespace CrudCore.Models;

public partial class Cargo
{
    public int IDcArgo { get; set; }

    public string? Descricion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
