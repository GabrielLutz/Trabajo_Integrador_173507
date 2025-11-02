using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IPostulanteRepository : IRepository<Postulante>
    {
        Task<Postulante?> GetByCedulaAsync(string cedulaIdentidad);
        Task<Postulante?> GetByEmailAsync(string email);
        Task<Postulante?> GetByIdWithInscripcionesAsync(int id);
        Task<bool> ExistsByCedulaAsync(string cedulaIdentidad);
        Task<bool> ExistsByEmailAsync(string email);
        Task UpdateDatosPersonalesAsync(Postulante postulante);
    }
}
