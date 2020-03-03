using System;
using System.Collections.Generic;

namespace Inspecciones.Models
{
    public partial class Usuario
    {
        public string Password { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Email { get; set; }
        public int? IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
    }
}
