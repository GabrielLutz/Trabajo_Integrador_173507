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
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsInLlamado(int departamentoId, int llamadoId)
        {
            return await _context.LlamadoDepartamentos
                .AnyAsync(ld => ld.DepartamentoId == departamentoId && ld.LlamadoId == llamadoId);
        }

        public async Task<IEnumerable<Departamento>> GetActivosAsync()
        {
            return await _dbSet
                .Where(d => d.Activo)
                .OrderBy(d => d.Nombre)
                .ToListAsync();
        }

        public async Task<Departamento?> GetByCodigoAsync(string codigo)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.Codigo == codigo);
        }
    }
}
