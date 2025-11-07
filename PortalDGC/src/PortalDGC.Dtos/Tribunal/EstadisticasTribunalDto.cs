using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Dtos.Tribunal
{
    public class EstadisticasTribunalDto
    {
        public int LlamadoId { get; set; }
        public string TituloLlamado { get; set; } = string.Empty;
        public int TotalInscripciones { get; set; }
        public int InscripcionesConPruebasCompletas { get; set; }
        public int InscripcionesConMeritosCompletos { get; set; }
        public int TotalPruebas { get; set; }
        public List<EstadisticaPruebaDto> DetallesPruebas { get; set; } = new();
        public int AprobadosPruebas { get; set; }
        public int AprobadosFinal { get; set; }
        public decimal PromedioGeneral { get; set; }
        public int TotalAfrodescendientes { get; set; }
        public int TotalTrans { get; set; }
        public int TotalDiscapacidad { get; set; }
        public bool OrdenamientoGenerado { get; set; }
        public DateTime? FechaOrdenamiento { get; set; }
    }

    public class EstadisticaPruebaDto
    {
        public string NombrePrueba { get; set; } = string.Empty;
        public int Evaluados { get; set; }
        public int Aprobados { get; set; }
        public decimal PromedioNota { get; set; }
    }
}
