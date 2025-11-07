using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IEvaluacionMeritoRepository : IRepository<EvaluacionMerito>
    {
        Task<IEnumerable<EvaluacionMerito>> GetByInscripcionIdAsync(int inscripcionId);
        Task<EvaluacionMerito?> GetByMeritoIdAsync(int meritoPostulanteId);
        Task<decimal> GetPuntajeTotalMeritosAsync(int inscripcionId);
    }
}
