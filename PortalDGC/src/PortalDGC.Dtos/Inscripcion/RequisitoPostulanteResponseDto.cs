using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class RequisitoPostulanteResponseDto
    {
        public int Id { get; set; }
        public int RequisitoId { get; set; }
        public string DescripcionRequisito { get; set; } = string.Empty;
        public string TipoRequisito { get; set; } = string.Empty;
        public bool Obligatorio { get; set; }
        public bool Cumple { get; set; }
        public string? Observaciones { get; set; }
    }
}
