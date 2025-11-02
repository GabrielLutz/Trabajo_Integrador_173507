using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IRequisitoPostulanteRepository : IRepository<RequisitoPostulante>
    {
        Task<IEnumerable<RequisitoPostulante>> GetByInscripcionIdAsync(int inscripcionId);
        Task<RequisitoPostulante> AddRequisitoAsync(RequisitoPostulante requisito);
        Task AddRangeAsync(IEnumerable<RequisitoPostulante> requisitos);
        Task<bool> CumpleTodosRequisitosObligatorios(int inscripcionId);
    }
}
