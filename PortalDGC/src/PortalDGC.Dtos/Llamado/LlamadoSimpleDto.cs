using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Llamado
{
    public class LlamadoSimpleDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
