using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Ordenamiento
    {
        public int Id { get; set; }
        public int LlamadoId { get; set; }
        public string Tipo { get; set; } = string.Empty; 
        public DateTime FechaGeneracion { get; set; }
        public string Estado { get; set; } = string.Empty; 
        public int? DepartamentoId { get; set; }

        public virtual Llamado Llamado { get; set; } = null!;
        public virtual Departamento? Departamento { get; set; }
        public virtual ICollection<PosicionOrdenamiento> Posiciones { get; set; } = new List<PosicionOrdenamiento>();
    }
}
