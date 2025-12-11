using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Llamado
{
    public class LlamadoDetalleDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Bases { get; set; } = string.Empty;
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public int CantidadPuestos { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<DepartamentoLlamadoDto> Departamentos { get; set; } = new();
        public List<RequisitoExcluyenteDto> RequisitosExcluyentes { get; set; } = new();
        public List<ItemPuntuableDto> ItemsPuntuables { get; set; } = new();
        public List<ApoyoNecesarioDto> ApoyosNecesarios { get; set; } = new();
    }
}
