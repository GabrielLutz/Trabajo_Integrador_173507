using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostulanteRepository Postulantes { get; }
        ILlamadoRepository Llamados { get; }
        IDepartamentoRepository Departamentos { get; }
        IInscripcionRepository Inscripciones { get; }
        IAutodefinicionLeyRepository AutodefinicionesLey { get; }
        IRequisitoExcluyenteRepository RequisitosExcluyentes { get; }
        IRequisitoPostulanteRepository RequisitosPostulante { get; }
        IItemPuntuableRepository ItemsPuntuables { get; }
        IMeritoPostulanteRepository MeritosPostulante { get; }
        IApoyoNecesarioRepository ApoyosNecesarios { get; }
        IApoyoSolicitadoRepository ApoyosSolicitados { get; }
        IConstanciaRepository Constancias { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
