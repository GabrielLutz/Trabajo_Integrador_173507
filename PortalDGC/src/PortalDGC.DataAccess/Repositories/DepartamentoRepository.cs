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
    /// Repositorio de departamentos orientado a validar disponibilidad por llamado (RF-03).
    /// </summary>
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<bool> ExistsInLlamado(int departamentoId, int llamadoId)
        {
            return await _context.LlamadoDepartamentos
                .AnyAsync(ld => ld.DepartamentoId == departamentoId && ld.LlamadoId == llamadoId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Departamento>> GetActivosAsync()
        {
            return await _dbSet
                .Where(d => d.Activo)
                .OrderBy(d => d.Nombre)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Departamento?> GetByCodigoAsync(string codigo)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.Codigo == codigo);
        }
    }
}
