using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IDepartamentoRepository : IRepository<Departamento>
    {
        Task<IEnumerable<Departamento>> GetActivosAsync();
        Task<Departamento?> GetByCodigoAsync(string codigo);
        Task<bool> ExistsInLlamado(int departamentoId, int llamadoId);
    }
}
