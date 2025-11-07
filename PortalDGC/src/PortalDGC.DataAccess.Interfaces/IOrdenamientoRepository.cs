using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IOrdenamientoRepository : IRepository<Ordenamiento>
    {
        Task<Ordenamiento?> GetByIdWithPosicionesAsync(int id);
        Task<IEnumerable<Ordenamiento>> GetByLlamadoIdAsync(int llamadoId);
        Task<Ordenamiento?> GetDefinitivoByLlamadoAndTipoAsync(int llamadoId, string tipo);
    }
}
