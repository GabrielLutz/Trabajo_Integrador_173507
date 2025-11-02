using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IItemPuntuableRepository : IRepository<ItemPuntuable>
    {
        Task<IEnumerable<ItemPuntuable>> GetByLlamadoIdAsync(int llamadoId);
        Task<ItemPuntuable?> GetByIdAsync(int itemId);
        Task<IEnumerable<ItemPuntuable>> GetByCategoriaAsync(int llamadoId, string categoria);
    }
}
