using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class ApoyoSolicitadoResponseDto
    {
        public int Id { get; set; }
        public int ApoyoId { get; set; }
        public string DescripcionApoyo { get; set; } = string.Empty;
        public string TipoApoyo { get; set; } = string.Empty;
        public string? Justificacion { get; set; }
    }
}
