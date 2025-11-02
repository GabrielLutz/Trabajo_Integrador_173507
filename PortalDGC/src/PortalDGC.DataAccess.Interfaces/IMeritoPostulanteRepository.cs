using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IMeritoPostulanteRepository : IRepository<MeritoPostulante>
    {
        Task<IEnumerable<MeritoPostulante>> GetByInscripcionIdAsync(int inscripcionId);
        Task<MeritoPostulante> AddMeritoAsync(MeritoPostulante merito);
        Task AddRangeAsync(IEnumerable<MeritoPostulante> meritos);
        Task<decimal> CalcularPuntajeTotalAsync(int inscripcionId);
    }
}
