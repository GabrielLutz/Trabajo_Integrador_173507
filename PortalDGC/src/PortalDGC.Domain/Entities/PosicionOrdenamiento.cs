using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class PosicionOrdenamiento
    {
        public int Id { get; set; }
        public int OrdenamientoId { get; set; }
        public int InscripcionId { get; set; }
        public int Posicion { get; set; }
        public decimal PuntajeTotal { get; set; }
        public bool AplicaCuota { get; set; }
        public string? TipoCuota { get; set; }

        public virtual Ordenamiento Ordenamiento { get; set; } = null!;
        public virtual Inscripcion Inscripcion { get; set; } = null!;
    }
}
