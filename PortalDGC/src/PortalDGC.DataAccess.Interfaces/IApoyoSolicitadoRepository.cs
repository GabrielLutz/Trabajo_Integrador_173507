using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IApoyoSolicitadoRepository : IRepository<ApoyoSolicitado>
    {
        Task<IEnumerable<ApoyoSolicitado>> GetByInscripcionIdAsync(int inscripcionId);
        Task<ApoyoSolicitado> AddApoyoAsync(ApoyoSolicitado apoyo);
        Task AddRangeAsync(IEnumerable<ApoyoSolicitado> apoyos);
    }
}
