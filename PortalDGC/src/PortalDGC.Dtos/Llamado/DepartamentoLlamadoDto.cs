using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Llamado
{
    public class DepartamentoLlamadoDto
    {
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int CantidadPuestos { get; set; }
    }
}
