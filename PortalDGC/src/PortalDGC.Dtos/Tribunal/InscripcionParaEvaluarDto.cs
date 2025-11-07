using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class InscripcionParaEvaluarDto
    {
        public int InscripcionId { get; set; }
        public int PostulanteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string CedulaIdentidad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
        public string EstadoInscripcion { get; set; } = string.Empty;
        public bool EsAfrodescendiente { get; set; }
        public bool EsTrans { get; set; }
        public bool TieneDiscapacidad { get; set; }
        public int PruebasEvaluadas { get; set; }
        public int PruebasTotales { get; set; }
        public int MeritosEvaluados { get; set; }
        public int MeritosTotales { get; set; }
        public decimal? PuntajePruebas { get; set; }
        public decimal? PuntajeMeritos { get; set; }
        public decimal? PuntajeTotal { get; set; }
        public bool? AproboPruebas { get; set; }
    }
}
