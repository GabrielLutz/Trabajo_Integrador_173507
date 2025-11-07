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
    public class PruebaRepository : Repository<Prueba>, IPruebaRepository
    {
        public PruebaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Prueba>> GetByLlamadoIdAsync(int llamadoId)
        {
            return await _dbSet
                .Where(p => p.LlamadoId == llamadoId)
                .OrderBy(p => p.OrdenEjecucion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Prueba>> GetPruebasActivasAsync(int llamadoId)
        {
            return await _dbSet
                .Where(p => p.LlamadoId == llamadoId &&
                           (p.Estado == "Programada" || p.Estado == "Realizada"))
                .OrderBy(p => p.OrdenEjecucion)
                .ToListAsync();
        }

        public async Task<Prueba?> GetByIdWithEvaluacionesAsync(int pruebaId)
        {
            return await _dbSet
                .Include(p => p.Llamado)
                .Include(p => p.EvaluacionesPruebas)
                    .ThenInclude(e => e.Inscripcion)
                        .ThenInclude(i => i.Postulante)
                .FirstOrDefaultAsync(p => p.Id == pruebaId);
        }
    }
}
