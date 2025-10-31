using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class Postulante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CedulaIdentidad { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string? GeneroOtro { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string Domicilio { get; set; } = string.Empty;
        public string? DocumentoCedula { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
