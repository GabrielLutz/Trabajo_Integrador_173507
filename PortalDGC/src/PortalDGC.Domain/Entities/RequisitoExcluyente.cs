using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class RequisitoExcluyente
    {
        public int Id { get; set; }
        public int LlamadoId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public bool Obligatorio { get; set; }
    }
}
