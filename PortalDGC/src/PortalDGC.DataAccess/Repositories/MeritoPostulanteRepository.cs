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
    /// Repositorio que gestiona los méritos presentados por cada postulante.
    /// </summary>
    public class MeritoPostulanteRepository : Repository<MeritoPostulante>, IMeritoPostulanteRepository
    {
        public MeritoPostulanteRepository(ApplicationDbContext context) : base(context)
        {
        }
        /// <inheritdoc />
        public async Task<IEnumerable<MeritoPostulante>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(m => m.ItemPuntuable)
                .Where(m => m.InscripcionId == inscripcionId)
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<MeritoPostulante> AddMeritoAsync(MeritoPostulante merito)
        {
            await _dbSet.AddAsync(merito);
            return merito;
        }
        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<MeritoPostulante> meritos)
        {
            await _dbSet.AddRangeAsync(meritos);
        }
        /// <inheritdoc />
        public async Task<decimal> CalcularPuntajeTotalAsync(int inscripcionId)
        {
            return await _dbSet
                .Where(m => m.InscripcionId == inscripcionId && m.Verificado)
                .SumAsync(m => m.PuntajeObtenido);
        }
    }
}
