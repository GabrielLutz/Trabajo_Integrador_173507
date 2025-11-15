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
    /// <summary>
    /// Repositorio que almacena los apoyos solicitados en cada inscripción para garantizar accesibilidad.
    /// </summary>
    public class ApoyoSolicitadoRepository : Repository<ApoyoSolicitado>, IApoyoSolicitadoRepository
    {
        public ApoyoSolicitadoRepository(ApplicationDbContext context) : base(context)
        {
        }
        /// <inheritdoc />
        public async Task<IEnumerable<ApoyoSolicitado>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(a => a.Apoyo)
                .Where(a => a.InscripcionId == inscripcionId)
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<ApoyoSolicitado> AddApoyoAsync(ApoyoSolicitado apoyo)
        {
            await _dbSet.AddAsync(apoyo);
            return apoyo;
        }
        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<ApoyoSolicitado> apoyos)
        {
            await _dbSet.AddRangeAsync(apoyos);
        }
    }
}
