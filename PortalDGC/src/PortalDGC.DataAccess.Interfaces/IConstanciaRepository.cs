using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IConstanciaRepository : IRepository<Constancia>
    {
        Task<IEnumerable<Constancia>> GetByPostulanteIdAsync(int postulanteId);
        Task<Constancia> SubirConstanciaAsync(Constancia constancia);
        Task<Constancia?> GetByIdAsync(int constanciaId);
        Task<bool> ValidarConstanciaAsync(int constanciaId);
    }
}
