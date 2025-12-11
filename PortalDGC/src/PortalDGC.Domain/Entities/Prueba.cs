using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Prueba
    {
        public int Id { get; set; }
        public int LlamadoId { get; set; }
        public string Tipo { get; set; } = string.Empty; 
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PuntajeMaximo { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Lugar { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty; 
        public bool EsObligatoria { get; set; }
        public int OrdenEjecucion { get; set; }

        public virtual Llamado Llamado { get; set; } = null!;
        public virtual ICollection<EvaluacionPrueba> EvaluacionesPruebas { get; set; } = new List<EvaluacionPrueba>();
    }
}
