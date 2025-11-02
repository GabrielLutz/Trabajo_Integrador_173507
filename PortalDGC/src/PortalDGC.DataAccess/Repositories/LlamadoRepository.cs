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
    public class LlamadoRepository : Repository<Llamado>, ILlamadoRepository
    {
        public LlamadoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Llamado?> GetByIdWithApoyosAsync(int id)
        {
            return await _dbSet
                .Include(l => l.ApoyosNecesarios)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Llamado?> GetByIdWithDepartamentosAsync(int id)
        {
            return await _dbSet
                .Include(l => l.LlamadoDepartamentos)
                    .ThenInclude(ld => ld.Departamento)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Llamado?> GetByIdWithDetallesAsync(int id)
        {
            return await _dbSet
                .Include(l => l.LlamadoDepartamentos)
                    .ThenInclude(ld => ld.Departamento)
                .Include(l => l.RequisitosExcluyentes)
                .Include(l => l.ItemsPuntuables)
                .Include(l => l.ApoyosNecesarios)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Llamado?> GetByIdWithItemsPuntuablesAsync(int id)
        {
            return await _dbSet
                .Include(l => l.ItemsPuntuables)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Llamado?> GetByIdWithRequisitosAsync(int id)
        {
            return await _dbSet
                .Include(l => l.RequisitosExcluyentes)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Llamado>> GetLlamadosActivosAsync()
        {
            return await _dbSet
                .Where(l => l.Estado == "Abierto" && l.FechaCierre >= DateTime.Now)
                .OrderByDescending(l => l.FechaApertura)
                .ToListAsync();
        }

        public async Task<IEnumerable<Llamado>> GetLlamadosInactivosAsync()
        {
            return await _context.Llamados
                .Where(l => l.Estado == "Cerrado" && l.FechaCierre < DateTime.Now)
                .OrderByDescending(l => l.FechaCierre)
                .ToListAsync();
        }

        public async Task<bool> IsLlamadoAbierto(int llamadoId)
        {
            return await _dbSet
                .AnyAsync(l => l.Id == llamadoId &&
                              l.Estado == "Abierto" &&
                              l.FechaCierre >= DateTime.Now);
        }
    }
}
