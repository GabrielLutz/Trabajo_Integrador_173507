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
    public class EvaluacionMeritoRepository : Repository<EvaluacionMerito>, IEvaluacionMeritoRepository
    {
        public EvaluacionMeritoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EvaluacionMerito>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(e => e.MeritoPostulante)
                    .ThenInclude(m => m.ItemPuntuable)
                .Where(e => e.MeritoPostulante.InscripcionId == inscripcionId)
                .ToListAsync();
        }

        public async Task<EvaluacionMerito?> GetByMeritoIdAsync(int meritoPostulanteId)
        {
            return await _dbSet
                .Include(e => e.MeritoPostulante)
                    .ThenInclude(m => m.ItemPuntuable)
                .FirstOrDefaultAsync(e => e.MeritoPostulanteId == meritoPostulanteId);
        }

        public async Task<decimal> GetPuntajeTotalMeritosAsync(int inscripcionId)
        {
            var evaluaciones = await _dbSet
                .Include(e => e.MeritoPostulante)
                .Where(e => e.MeritoPostulante.InscripcionId == inscripcionId &&
                           e.Estado == "Aprobado" &&
                           e.DocumentacionVerificada)
                .ToListAsync();

            return evaluaciones.Sum(e => e.PuntajeAsignado);
        }
    }
}
