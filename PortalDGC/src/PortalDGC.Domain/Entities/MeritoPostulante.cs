using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class MeritoPostulante
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public int ItemPuntuableId { get; set; }
        public string? DocumentoRespaldo { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public bool Verificado { get; set; }
        public virtual Inscripcion Inscripcion { get; set; } = null!;
        public virtual ItemPuntuable ItemPuntuable { get; set; } = null!;
    }
}
