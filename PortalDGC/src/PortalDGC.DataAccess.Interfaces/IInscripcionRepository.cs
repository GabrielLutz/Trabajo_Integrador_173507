using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IInscripcionRepository : IRepository<Inscripcion>
    {
        Task<Inscripcion?> GetByIdCompleteAsync(int id);
        Task<IEnumerable<Inscripcion>> GetByPostulanteIdAsync(int postulanteId);
        Task<IEnumerable<Inscripcion>> GetByLlamadoIdAsync(int llamadoId);
        Task<bool> ExistsInscripcionAsync(int postulanteId, int llamadoId);
        Task<Inscripcion> CreateInscripcionCompleteAsync(Inscripcion inscripcion);
        Task UpdateEstadoAsync(int inscripcionId, string estado);
        Task UpdatePuntajeTotalAsync(int inscripcionId, decimal puntaje);
    }
}
