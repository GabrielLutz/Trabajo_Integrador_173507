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
    /// Repositorio que almacena las evaluaciones de pruebas escritas/orales del tribunal.
    /// </summary>
    public class EvaluacionPruebaRepository : Repository<EvaluacionPrueba>, IEvaluacionPruebaRepository
    {
        public EvaluacionPruebaRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EvaluacionPrueba>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(e => e.Prueba)
                .Where(e => e.InscripcionId == inscripcionId)
                .OrderBy(e => e.Prueba.OrdenEjecucion)
                .ToListAsync();
        }

            /// <inheritdoc />
        public async Task<IEnumerable<EvaluacionPrueba>> GetByPruebaIdAsync(int pruebaId)
        {
            return await _dbSet
                .Include(e => e.Inscripcion)
                    .ThenInclude(i => i.Postulante)
                .Where(e => e.PruebaId == pruebaId)
                .OrderByDescending(e => e.PuntajeObtenido)
                .ToListAsync();
        }

            /// <inheritdoc />
        public async Task<EvaluacionPrueba?> GetByInscripcionAndPruebaAsync(int inscripcionId, int pruebaId)
        {
            return await _dbSet
                .Include(e => e.Prueba)
                .FirstOrDefaultAsync(e => e.InscripcionId == inscripcionId &&
                                         e.PruebaId == pruebaId);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<int> GetCantidadAprobadosAsync(int pruebaId)
        {
            return await _dbSet
                .Where(e => e.PruebaId == pruebaId && e.Aprobado)
                .CountAsync();
        }
    }
}
