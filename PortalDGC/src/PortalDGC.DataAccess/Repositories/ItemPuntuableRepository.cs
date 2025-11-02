using Microsoft.EntityFrameworkCore;
using PortalDGC.DataAccess.Data;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Repositories
{
    public class ItemPuntuableRepository : Repository<ItemPuntuable>, IItemPuntuableRepository
    {
        public ItemPuntuableRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<ItemPuntuable>> GetByLlamadoIdAsync(int llamadoId)
        {
            return await _dbSet
                .Where(i => i.LlamadoId == llamadoId)
                .OrderBy(i => i.Categoria)
                .ThenBy(i => i.Nombre)
                .ToListAsync();
        }
        public override async Task<ItemPuntuable?> GetByIdAsync(int itemId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(i => i.Id == itemId);
        }
        public async Task<IEnumerable<ItemPuntuable>> GetByCategoriaAsync(int llamadoId, string categoria)
        {
            return await _dbSet
                .Where(i => i.LlamadoId == llamadoId && i.Categoria == categoria)
                .ToListAsync();
        }
    }
}
