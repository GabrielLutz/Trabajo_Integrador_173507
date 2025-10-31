using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class ApoyoSolicitado
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public int ApoyoId { get; set; }
        public string? Justificacion { get; set; }
    }
}
