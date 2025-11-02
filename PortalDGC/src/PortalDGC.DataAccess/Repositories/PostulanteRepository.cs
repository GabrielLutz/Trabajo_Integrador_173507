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
    public class PostulanteRepository : Repository<Postulante>, IPostulanteRepository
    {
        public PostulanteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Postulante?> GetByCedulaAsync(string cedulaIdentidad)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.CedulaIdentidad == cedulaIdentidad);
        }
        public async Task<Postulante?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Email == email);
        }
        public async Task<Postulante?> GetByIdWithInscripcionesAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Inscripciones)
                    .ThenInclude(i => i.Llamado)
                .Include(p => p.Inscripciones)
                    .ThenInclude(i => i.Departamento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> ExistsByCedulaAsync(string cedulaIdentidad)
        {
            return await _dbSet.AnyAsync(p => p.CedulaIdentidad == cedulaIdentidad);
        }
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(p => p.Email == email);
        }
        public async Task UpdateDatosPersonalesAsync(Postulante postulante)
        {
            _context.Entry(postulante).State = EntityState.Modified;
            await Task.CompletedTask;
        }
    }
}
