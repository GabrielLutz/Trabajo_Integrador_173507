using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface ILlamadoRepository : IRepository<Llamado>
    {
        Task<Llamado?> GetByIdWithDetallesAsync(int id);
        Task<IEnumerable<Llamado>> GetLlamadosActivosAsync();
        Task<IEnumerable<Llamado>> GetLlamadosInactivosAsync();
        Task<Llamado?> GetByIdWithRequisitosAsync(int id);
        Task<Llamado?> GetByIdWithItemsPuntuablesAsync(int id);
        Task<Llamado?> GetByIdWithApoyosAsync(int id);
        Task<Llamado?> GetByIdWithDepartamentosAsync(int id);
        Task<bool> IsLlamadoAbierto(int llamadoId);
    }
}
