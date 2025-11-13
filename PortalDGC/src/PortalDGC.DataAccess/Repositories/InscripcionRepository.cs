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
    public class InscripcionRepository : Repository<Inscripcion>, IInscripcionRepository
    {
        public InscripcionRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Inscripcion?> GetByIdCompleteAsync(int id)
        {
            return await _dbSet
                .Include(i => i.Postulante)
                .Include(i => i.Llamado)
                .Include(i => i.Departamento)
                .Include(i => i.AutodefinicionLey)
                .Include(i => i.RequisitosPostulante)
                    .ThenInclude(rp => rp.Requisito)
                .Include(i => i.MeritosPostulante)
                    .ThenInclude(mp => mp.ItemPuntuable)
                .Include(i => i.ApoyosSolicitados)
                    .ThenInclude(ap => ap.Apoyo)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Inscripcion>> GetByPostulanteIdAsync(int postulanteId)
        {
            return await _dbSet
                .Include(i => i.Llamado)
                .Include(i => i.Departamento)
                .Where(i => i.PostulanteId == postulanteId)
                .OrderByDescending(i => i.FechaInscripcion)
                .ToListAsync();
        }
        public async Task<IEnumerable<Inscripcion>> GetByLlamadoIdAsync(int llamadoId)
        {
            return await _dbSet
                .Include(i => i.Postulante)
                .Include(i => i.Departamento)
                .Include(i => i.AutodefinicionLey)
                .Where(i => i.LlamadoId == llamadoId)
                .ToListAsync();
        }
        public async Task<bool> ExistsInscripcionAsync(int postulanteId, int llamadoId)
        {
            return await _dbSet
                .AnyAsync(i => i.PostulanteId == postulanteId && i.LlamadoId == llamadoId);
        }
        public async Task<Inscripcion> CreateInscripcionCompleteAsync(Inscripcion inscripcion)
        {
            await _dbSet.AddAsync(inscripcion);
            return inscripcion;
        }
        public async Task UpdateEstadoAsync(int inscripcionId, string estado)
        {
            var inscripcion = await GetByIdAsync(inscripcionId);
            if (inscripcion != null)
            {
                inscripcion.Estado = estado;
            }
        }
        public async Task UpdatePuntajeTotalAsync(int inscripcionId, decimal puntaje)
        {
            var inscripcion = await GetByIdAsync(inscripcionId);
            if (inscripcion != null)
            {
                inscripcion.PuntajeTotal = puntaje;
            }
        }
    }
}
