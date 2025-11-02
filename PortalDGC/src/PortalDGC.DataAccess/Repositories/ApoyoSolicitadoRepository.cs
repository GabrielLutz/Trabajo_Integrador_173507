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
    public class ApoyoSolicitadoRepository : Repository<ApoyoSolicitado>, IApoyoSolicitadoRepository
    {
        public ApoyoSolicitadoRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<ApoyoSolicitado>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(a => a.Apoyo)
                .Where(a => a.InscripcionId == inscripcionId)
                .ToListAsync();
        }
        public async Task<ApoyoSolicitado> AddApoyoAsync(ApoyoSolicitado apoyo)
        {
            await _dbSet.AddAsync(apoyo);
            return apoyo;
        }
        public async Task AddRangeAsync(IEnumerable<ApoyoSolicitado> apoyos)
        {
            await _dbSet.AddRangeAsync(apoyos);
        }
    }
}
