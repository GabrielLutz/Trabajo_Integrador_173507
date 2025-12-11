using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class LlamadoDepartamento
    {
        public int Id { get; set; }
        public int LlamadoId { get; set; }
        public int DepartamentoId { get; set; }
        public int CantidadPuestos { get; set; }
        public virtual Llamado Llamado { get; set; } = null!;
        public virtual Departamento Departamento { get; set; } = null!;
    }
}
