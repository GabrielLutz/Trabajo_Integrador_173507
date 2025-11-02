using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IApoyoNecesarioRepository : IRepository<ApoyoNecesario>
    {
        Task<IEnumerable<ApoyoNecesario>> GetByLlamadoIdAsync(int llamadoId);
        Task<ApoyoNecesario?> GetByIdAsync(int apoyoId);
    }
}
