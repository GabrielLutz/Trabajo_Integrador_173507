using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class DetalleEvaluacionDto
    {
        public int InscripcionId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string CedulaIdentidad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public bool EsAfrodescendiente { get; set; }
        public bool EsTrans { get; set; }
        public bool TieneDiscapacidad { get; set; }
        public List<RequisitoPostulanteResponseDto> Requisitos { get; set; } = new();
        public List<EvaluacionPruebaDto> Pruebas { get; set; } = new();
        public List<MeritoParaEvaluarDto> Meritos { get; set; } = new();
        public decimal PuntajePruebas { get; set; }
        public decimal PuntajeMeritos { get; set; }
        public decimal PuntajeTotal { get; set; }
    }

    public class MeritoParaEvaluarDto
    {
        public int MeritoPostulanteId { get; set; }
        public string NombreItem { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal PuntajeMaximo { get; set; }
        public string? DocumentoRespaldo { get; set; }
        public bool FueEvaluado { get; set; }
        public decimal? PuntajeAsignado { get; set; }
        public bool? DocumentacionVerificada { get; set; }
        public string? Observaciones { get; set; }
        public string? Estado { get; set; }
    }
}
