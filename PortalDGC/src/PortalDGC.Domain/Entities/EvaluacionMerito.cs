using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class EvaluacionMerito
    {
        public int Id { get; set; }
        public int MeritoPostulanteId { get; set; }
        public decimal PuntajeAsignado { get; set; }
        public string? Observaciones { get; set; }
        public string Estado { get; set; } = string.Empty; 
        public DateTime FechaEvaluacion { get; set; }
        public bool DocumentacionVerificada { get; set; }

        public virtual MeritoPostulante MeritoPostulante { get; set; } = null!;
    }
}
