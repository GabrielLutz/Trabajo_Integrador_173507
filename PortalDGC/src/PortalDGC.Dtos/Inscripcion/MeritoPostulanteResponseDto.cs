using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Inscripcion
{
    public class MeritoPostulanteResponseDto
    {
        public int Id { get; set; }
        public int ItemPuntuableId { get; set; }
        public string NombreItem { get; set; } = string.Empty;
        public string DescripcionItem { get; set; } = string.Empty;
        public decimal PuntajeMaximo { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string? DocumentoRespaldo { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public bool Verificado { get; set; }
    }
}
