using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IPruebaRepository : IRepository<Prueba>
    {
        Task<IEnumerable<Prueba>> GetByLlamadoIdAsync(int llamadoId);
        Task<IEnumerable<Prueba>> GetPruebasActivasAsync(int llamadoId);
        Task<Prueba?> GetByIdWithEvaluacionesAsync(int pruebaId);
    }
}
