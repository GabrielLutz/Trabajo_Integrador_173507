using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Constancia
    {
        public int Id { get; set; }
        public int PostulanteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Archivo { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; }
        public bool Validado { get; set; }
        public virtual Postulante Postulante { get; set; } = null!;
    }
}
