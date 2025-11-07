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
    public class EvaluacionPruebaRepository : Repository<EvaluacionPrueba>, IEvaluacionPruebaRepository
    {
        public EvaluacionPruebaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EvaluacionPrueba>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(e => e.Prueba)
                .Where(e => e.InscripcionId == inscripcionId)
                .OrderBy(e => e.Prueba.OrdenEjecucion)
                .ToListAsync();
        }

        public async Task<IEnumerable<EvaluacionPrueba>> GetByPruebaIdAsync(int pruebaId)
        {
            return await _dbSet
                .Include(e => e.Inscripcion)
                    .ThenInclude(i => i.Postulante)
                .Where(e => e.PruebaId == pruebaId)
                .OrderByDescending(e => e.PuntajeObtenido)
                .ToListAsync();
        }

        public async Task<EvaluacionPrueba?> GetByInscripcionAndPruebaAsync(int inscripcionId, int pruebaId)
        {
            return await _dbSet
                .Include(e => e.Prueba)
                .FirstOrDefaultAsync(e => e.InscripcionId == inscripcionId &&
                                         e.PruebaId == pruebaId);
        }

        public async Task<decimal> GetPromedioByPruebaAsync(int pruebaId)
        {
            var evaluaciones = await _dbSet
                .Where(e => e.PruebaId == pruebaId && e.Aprobado)
                .ToListAsync();

            if (!evaluaciones.Any())
            {
                return 0;
            }

            return evaluaciones.Average(e => e.PuntajeObtenido);
        }

        public async Task<int> GetCantidadAprobadosAsync(int pruebaId)
        {
            return await _dbSet
                .Where(e => e.PruebaId == pruebaId && e.Aprobado)
                .CountAsync();
        }
    }
}
