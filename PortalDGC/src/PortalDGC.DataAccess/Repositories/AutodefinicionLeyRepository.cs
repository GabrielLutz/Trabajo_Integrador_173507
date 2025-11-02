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
    public class AutodefinicionLeyRepository : Repository<AutodefinicionLey>, IAutodefinicionLeyRepository
    {
        public AutodefinicionLeyRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<AutodefinicionLey?> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.InscripcionId == inscripcionId);
        }
        public async Task<AutodefinicionLey> CreateOrUpdateAsync(AutodefinicionLey autodefinicion)
        {
            var existing = await GetByInscripcionIdAsync(autodefinicion.InscripcionId);

            if (existing != null)
            {
                existing.EsAfrodescendiente = autodefinicion.EsAfrodescendiente;
                existing.EsTrans = autodefinicion.EsTrans;
                existing.TieneDiscapacidad = autodefinicion.TieneDiscapacidad;
                return existing;
            }

            await _dbSet.AddAsync(autodefinicion);
            return autodefinicion;
        }
    }
}
