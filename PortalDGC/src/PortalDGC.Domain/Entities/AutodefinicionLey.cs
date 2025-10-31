using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.Domain.Entities
{
    public class AutodefinicionLey
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public bool EsAfrodescendiente { get; set; }
        public bool EsTrans { get; set; }
        public bool TieneDiscapacidad { get; set; }
    }
}
