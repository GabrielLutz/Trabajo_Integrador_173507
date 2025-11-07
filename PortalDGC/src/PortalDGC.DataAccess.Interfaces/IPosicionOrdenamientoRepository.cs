using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IPosicionOrdenamientoRepository : IRepository<PosicionOrdenamiento>
    {
        Task<IEnumerable<PosicionOrdenamiento>> GetByOrdenamientoIdAsync(int ordenamientoId);
        Task AddRangeAsync(IEnumerable<PosicionOrdenamiento> posiciones);
    }
}
