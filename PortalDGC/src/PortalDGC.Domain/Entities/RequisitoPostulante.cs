using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class RequisitoPostulante
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public int RequisitoId { get; set; }
        public bool Cumple { get; set; }
        public string? Observaciones { get; set; }
        public virtual Inscripcion Inscripcion { get; set; } = null!;
        public virtual RequisitoExcluyente Requisito { get; set; } = null!;
    }
}
