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
    /// Repositorio de constancias/documentos asociados a postulantes (RF-06).
    /// </summary>
    public class ConstanciaRepository : Repository<Constancia>, IConstanciaRepository
    {
        public ConstanciaRepository(ApplicationDbContext context) : base(context)
        {
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Constancia>> GetByPostulanteIdAsync(int postulanteId)
        {
            return await _dbSet
                .Where(c => c.PostulanteId == postulanteId)
                .OrderByDescending(c => c.FechaSubida)
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<Constancia> SubirConstanciaAsync(Constancia constancia)
        {
            await _dbSet.AddAsync(constancia);
            return constancia;
        }
        /// <inheritdoc />
        public override async Task<Constancia?> GetByIdAsync(int constanciaId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Id == constanciaId);
        }
        /// <inheritdoc />
        public async Task<bool> ValidarConstanciaAsync(int constanciaId)
        {
            var constancia = await GetByIdAsync(constanciaId);
            if (constancia != null)
            {
                constancia.Validado = true;
                return true;
            }
            return false;
        }
    }
}
