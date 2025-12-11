using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class OrdenamientoDto
    {
        public int Id { get; set; }
        public int LlamadoId { get; set; }
        public string TituloLlamado { get; set; } = string.Empty;
        public int? DepartamentoId { get; set; }
        public string? NombreDepartamento { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int CantidadPosiciones { get; set; }
    }

    public class OrdenamientoDetalleDto
    {
        public int Id { get; set; }
        public string TituloLlamado { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
        public List<PosicionOrdenamientoDto> Posiciones { get; set; } = new();
    }

    public class PosicionOrdenamientoDto
    {
        public int Posicion { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string CedulaIdentidad { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public decimal PuntajeTotal { get; set; }
        public bool AplicaCuota { get; set; }
        public string? TipoCuota { get; set; }
        public decimal? PuntajePruebas { get; set; }
        public decimal? PuntajeMeritos { get; set; }
    }
}
