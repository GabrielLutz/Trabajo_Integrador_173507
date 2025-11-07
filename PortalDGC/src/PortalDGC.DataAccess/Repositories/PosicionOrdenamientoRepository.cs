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
    public class PosicionOrdenamientoRepository : Repository<PosicionOrdenamiento>, IPosicionOrdenamientoRepository
    {
        public PosicionOrdenamientoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PosicionOrdenamiento>> GetByOrdenamientoIdAsync(int ordenamientoId)
        {
            return await _dbSet
                .Include(p => p.Inscripcion)
                    .ThenInclude(i => i.Postulante)
                .Include(p => p.Inscripcion)
                    .ThenInclude(i => i.Departamento)
                .Include(p => p.Inscripcion)
                    .ThenInclude(i => i.AutodefinicionLey)
                .Where(p => p.OrdenamientoId == ordenamientoId)
                .OrderBy(p => p.Posicion)
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<PosicionOrdenamiento> posiciones)
        {
            await _dbSet.AddRangeAsync(posiciones);
        }
    }
}
