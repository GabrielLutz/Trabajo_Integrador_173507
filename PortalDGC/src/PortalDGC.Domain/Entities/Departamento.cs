using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public virtual ICollection<LlamadoDepartamento> LlamadoDepartamentos { get; set; } = new List<LlamadoDepartamento>();
        public virtual ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public virtual ICollection<Ordenamiento> Ordenamientos { get; set; } = new List<Ordenamiento>();

    }
}
