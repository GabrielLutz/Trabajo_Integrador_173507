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
    public class OrdenamientoRepository : Repository<Ordenamiento>, IOrdenamientoRepository
    {
        public OrdenamientoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Ordenamiento?> GetByIdWithPosicionesAsync(int id)
        {
            return await _dbSet
                .Include(o => o.Llamado)
                .Include(o => o.Departamento)
                .Include(o => o.Posiciones)
                    .ThenInclude(p => p.Inscripcion)
                        .ThenInclude(i => i.Postulante)
                .Include(o => o.Posiciones)
                    .ThenInclude(p => p.Inscripcion)
                        .ThenInclude(i => i.Departamento)
                .Include(o => o.Posiciones)
                    .ThenInclude(p => p.Inscripcion)
                        .ThenInclude(i => i.AutodefinicionLey)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Ordenamiento>> GetByLlamadoIdAsync(int llamadoId)
        {
            return await _dbSet
                .Include(o => o.Llamado)
                .Include(o => o.Departamento)
                .Where(o => o.LlamadoId == llamadoId)
                .OrderByDescending(o => o.FechaGeneracion)
                .ToListAsync();
        }

        public async Task<Ordenamiento?> GetDefinitivoByLlamadoAndTipoAsync(int llamadoId, string tipo)
        {
            return await _dbSet
                .Include(o => o.Posiciones)
                    .ThenInclude(p => p.Inscripcion)
                        .ThenInclude(i => i.Postulante)
                .FirstOrDefaultAsync(o => o.LlamadoId == llamadoId &&
                                         o.Tipo == tipo &&
                                         o.Estado == "Definitivo");
        }
    }
}
