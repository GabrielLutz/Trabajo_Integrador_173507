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
    /// Repositorio que registra los requisitos presentados por cada postulante.
    /// </summary>
    public class RequisitoPostulanteRepository : Repository<RequisitoPostulante>, IRequisitoPostulanteRepository
    {
        public RequisitoPostulanteRepository(ApplicationDbContext context) : base(context)
        {
        }
        /// <inheritdoc />
        public async Task<IEnumerable<RequisitoPostulante>> GetByInscripcionIdAsync(int inscripcionId)
        {
            return await _dbSet
                .Include(rp => rp.Requisito)
                .Where(rp => rp.InscripcionId == inscripcionId)
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<RequisitoPostulante> AddRequisitoAsync(RequisitoPostulante requisito)
        {
            await _dbSet.AddAsync(requisito);
            return requisito;
        }
        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<RequisitoPostulante> requisitos)
        {
            await _dbSet.AddRangeAsync(requisitos);
        }
        /// <inheritdoc />
        public async Task<bool> CumpleTodosRequisitosObligatorios(int inscripcionId)
        {
            var requisitosObligatorios = await _dbSet
                .Include(rp => rp.Requisito)
                .Where(rp => rp.InscripcionId == inscripcionId && rp.Requisito.Obligatorio)
                .ToListAsync();

            return requisitosObligatorios.All(r => r.Cumple);
        }
    }
}
