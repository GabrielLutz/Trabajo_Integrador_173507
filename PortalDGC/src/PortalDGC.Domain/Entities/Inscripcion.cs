using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int PostulanteId { get; set; }
        public int LlamadoId { get; set; }
        public int DepartamentoId { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal PuntajeTotal { get; set; }
        public int? PosicionOrdenamiento { get; set; }
    }
}
