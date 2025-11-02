using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IRequisitoExcluyenteRepository : IRepository<RequisitoExcluyente>
    {
        Task<IEnumerable<RequisitoExcluyente>> GetByLlamadoIdAsync(int llamadoId);
        Task<RequisitoExcluyente?> GetByIdAsync(int requisitoId);
    }
}
