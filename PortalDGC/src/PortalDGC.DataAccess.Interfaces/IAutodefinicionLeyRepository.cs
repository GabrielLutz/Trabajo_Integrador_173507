using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IAutodefinicionLeyRepository : IRepository<AutodefinicionLey>
    {
        Task<AutodefinicionLey?> GetByInscripcionIdAsync(int inscripcionId);
        Task<AutodefinicionLey> CreateOrUpdateAsync(AutodefinicionLey autodefinicion);
    }
}
