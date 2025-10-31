using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Llamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Bases { get; set; } = string.Empty;
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public int CantidadPuestos { get; set; }
        public decimal PorcentajeAfrodescendiente { get; set; }
        public decimal PorcentajeTrans { get; set; }
        public decimal PorcentajeDiscapacidad { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
