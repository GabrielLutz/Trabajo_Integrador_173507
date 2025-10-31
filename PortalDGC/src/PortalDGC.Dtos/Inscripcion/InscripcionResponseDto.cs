using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class InscripcionResponseDto
    {
        public int Id { get; set; }
        public int PostulanteId { get; set; }
        public string NombrePostulante { get; set; } = string.Empty;
        public int LlamadoId { get; set; }
        public string TituloLlamado { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public string NombreDepartamento { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal PuntajeTotal { get; set; }
        public AutodefinicionLeyDto? Autodefinicion { get; set; }
        public List<RequisitoPostulanteResponseDto> Requisitos { get; set; } = new();
        public List<MeritoPostulanteResponseDto> Meritos { get; set; } = new();
        public List<ApoyoSolicitadoResponseDto> Apoyos { get; set; } = new();
    }
}
