using System;
using System.Collections.Generic;

namespace Inspecciones.Models
{
    public partial class RolOperacion
    {
        public int Id { get; set; }
        public int? IdRol { get; set; }
        public int? IdOperaciones { get; set; }

        public virtual Operaciones IdOperacionesNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
