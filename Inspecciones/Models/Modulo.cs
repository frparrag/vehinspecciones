using System;
using System.Collections.Generic;

namespace Inspecciones.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Operaciones = new HashSet<Operaciones>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Operaciones> Operaciones { get; set; }
    }
}
