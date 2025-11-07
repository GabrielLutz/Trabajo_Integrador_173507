using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IEvaluacionPruebaRepository : IRepository<EvaluacionPrueba>
    {
        Task<IEnumerable<EvaluacionPrueba>> GetByInscripcionIdAsync(int inscripcionId);
        Task<IEnumerable<EvaluacionPrueba>> GetByPruebaIdAsync(int pruebaId);
        Task<EvaluacionPrueba?> GetByInscripcionAndPruebaAsync(int inscripcionId, int pruebaId);
        Task<decimal> GetPromedioByPruebaAsync(int pruebaId);
        Task<int> GetCantidadAprobadosAsync(int pruebaId);
    }
}
