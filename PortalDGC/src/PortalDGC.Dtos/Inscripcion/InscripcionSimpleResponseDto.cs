using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class InscripcionSimpleResponseDto
    {
        public int Id { get; set; }
        public string TituloLlamado { get; set; } = string.Empty;
        public string NombreDepartamento { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
