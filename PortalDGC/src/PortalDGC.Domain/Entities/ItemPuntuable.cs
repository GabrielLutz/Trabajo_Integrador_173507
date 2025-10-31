using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class ItemPuntuable
    {
        public int Id { get; set; }
        public int LlamadoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PuntajeMaximo { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
