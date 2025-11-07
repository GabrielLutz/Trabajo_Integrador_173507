using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class EvaluacionMeritoDto
    {
        public int Id { get; set; }
        public int MeritoPostulanteId { get; set; }
        public string NombreItem { get; set; } = string.Empty;
        public string CategoriaItem { get; set; } = string.Empty;
        public decimal PuntajeMaximo { get; set; }
        public decimal PuntajeAsignado { get; set; }
        public string? DocumentoRespaldo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public bool DocumentacionVerificada { get; set; }
        public DateTime FechaEvaluacion { get; set; }
    }

    public class ValorarMeritoDto
    {
        public int MeritoPostulanteId { get; set; }
        public decimal PuntajeAsignado { get; set; }
        public bool DocumentacionVerificada { get; set; }
        public string? Observaciones { get; set; }
    }
}
