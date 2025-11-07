using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class GenerarOrdenamientoDto
    {
        public int LlamadoId { get; set; }
        public bool AplicarCuotas { get; set; } = true;
        public decimal PuntajeMinimoAprobacion { get; set; } = 70;
        public bool EsDefinitivo { get; set; } = false;
    }

    public class ResultadoGeneracionOrdenamientoDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<OrdenamientoDto> OrdenamientosGenerados { get; set; } = new();
        public EstadisticasOrdenamientoDto? Estadisticas { get; set; }
    }

    public class EstadisticasOrdenamientoDto
    {
        public int TotalEvaluados { get; set; }
        public int TotalAprobados { get; set; }
        public int TotalReprobados { get; set; }
        public int CuotaAfroAplicada { get; set; }
        public int CuotaTransAplicada { get; set; }
        public int CuotaDiscapacidadAplicada { get; set; }
        public decimal PuntajePromedio { get; set; }
        public decimal PuntajeMaximo { get; set; }
        public decimal PuntajeMinimo { get; set; }
    }
}
