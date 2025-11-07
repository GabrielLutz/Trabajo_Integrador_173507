using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class EvaluacionPruebaDto
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public int PruebaId { get; set; }
        public string NombrePrueba { get; set; } = string.Empty;
        public string TipoPrueba { get; set; } = string.Empty;
        public decimal PuntajeMaximo { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public bool Aprobado { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public bool Verificado { get; set; }
    }

    public class CalificarPruebaDto
    {
        public int InscripcionId { get; set; }
        public int PruebaId { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public string? Observaciones { get; set; }
    }
}
