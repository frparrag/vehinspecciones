using System;
using System.Collections.Generic;

namespace Inspecciones.Models
{
    public partial class Operaciones
    {
        public Operaciones()
        {
            RolOperacion = new HashSet<RolOperacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdModulo { get; set; }

        public virtual Modulo IdModuloNavigation { get; set; }
        public virtual ICollection<RolOperacion> RolOperacion { get; set; }
    }
}
