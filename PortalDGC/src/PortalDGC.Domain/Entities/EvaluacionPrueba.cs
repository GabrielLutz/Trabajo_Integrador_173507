using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class EvaluacionPrueba
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public int PruebaId { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public string? Observaciones { get; set; }
        public bool Aprobado { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public bool Verificado { get; set; }

        public virtual Inscripcion Inscripcion { get; set; } = null!;
        public virtual Prueba Prueba { get; set; } = null!;
    }
}
