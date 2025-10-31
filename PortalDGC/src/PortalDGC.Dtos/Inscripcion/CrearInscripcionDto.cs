using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class CrearInscripcionDto
    {
        public int LlamadoId { get; set; }
        public int DepartamentoId { get; set; }
        public AutodefinicionLeyDto Autodefinicion { get; set; } = new();
        public List<RequisitoPostulanteDto> Requisitos { get; set; } = new();
        public List<MeritoPostulanteDto> Meritos { get; set; } = new();
        public List<int> ApoyosIds { get; set; } = new();
    }
}
