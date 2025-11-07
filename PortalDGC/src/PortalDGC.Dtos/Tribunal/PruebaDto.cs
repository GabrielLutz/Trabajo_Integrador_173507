using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class PruebaDto
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
        public int? CantidadEvaluados { get; set; }
        public int? CantidadAprobados { get; set; }
        public decimal? PromedioGeneral { get; set; }
    }
}
