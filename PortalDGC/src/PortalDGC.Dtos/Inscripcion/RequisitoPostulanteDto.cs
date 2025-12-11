using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class RequisitoPostulanteDto
    {
        public int RequisitoId { get; set; }
        public bool Cumple { get; set; }
        public string? Observaciones { get; set; }
    }
}
