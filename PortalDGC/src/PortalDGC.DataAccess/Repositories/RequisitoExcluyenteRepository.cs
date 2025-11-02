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
    public class RequisitoExcluyenteRepository : Repository<RequisitoExcluyente>, IRequisitoExcluyenteRepository
    {
        public RequisitoExcluyenteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<RequisitoExcluyente>> GetByLlamadoIdAsync(int llamadoId)
        {
            return await _dbSet
                .Where(r => r.LlamadoId == llamadoId)
                .OrderBy(r => r.Tipo)
                .ToListAsync();
        }
        public override async Task<RequisitoExcluyente?> GetByIdAsync(int requisitoId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.Id == requisitoId);
        }
    }
}
