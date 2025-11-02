using PortalDGC.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPostulanteRepository Postulantes => throw new NotImplementedException();

        public ILlamadoRepository Llamados => throw new NotImplementedException();

        public IDepartamentoRepository Departamentos => throw new NotImplementedException();

        public IInscripcionRepository Inscripciones => throw new NotImplementedException();

        public IAutodefinicionLeyRepository AutodefinicionesLey => throw new NotImplementedException();

        public IRequisitoExcluyenteRepository RequisitosExcluyentes => throw new NotImplementedException();

        public IRequisitoPostulanteRepository RequisitosPostulante => throw new NotImplementedException();

        public IItemPuntuableRepository ItemsPuntuables => throw new NotImplementedException();

        public IMeritoPostulanteRepository MeritosPostulante => throw new NotImplementedException();

        public IApoyoNecesarioRepository ApoyosNecesarios => throw new NotImplementedException();

        public IApoyoSolicitadoRepository ApoyosSolicitados => throw new NotImplementedException();

        public IConstanciaRepository Constancias => throw new NotImplementedException();

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
