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
        public virtual Postulante Postulante { get; set; } = null!;
        public virtual Llamado Llamado { get; set; } = null!;
        public virtual Departamento Departamento { get; set; } = null!;
        public virtual AutodefinicionLey? AutodefinicionLey { get; set; }
        public virtual ICollection<RequisitoPostulante> RequisitosPostulante { get; set; } = new List<RequisitoPostulante>();
        public virtual ICollection<MeritoPostulante> MeritosPostulante { get; set; } = new List<MeritoPostulante>();
        public virtual ICollection<ApoyoSolicitado> ApoyosSolicitados { get; set; } = new List<ApoyoSolicitado>();
    }
}
