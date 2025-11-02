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
    public class ApoyoNecesarioRepository : Repository<ApoyoNecesario>, IApoyoNecesarioRepository
    {
        public ApoyoNecesarioRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<ApoyoNecesario>> GetByLlamadoIdAsync(int llamadoId)
        {
            return await _dbSet
                .Where(a => a.LlamadoId == llamadoId)
                .OrderBy(a => a.Tipo)
                .ToListAsync();
        }
        public override async Task<ApoyoNecesario?> GetByIdAsync(int apoyoId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.Id == apoyoId);
        }
    }
}
