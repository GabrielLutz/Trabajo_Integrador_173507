using Microsoft.EntityFrameworkCore.Storage;
using PortalDGC.DataAccess.Data;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Postulantes = new PostulanteRepository(_context);
            Llamados = new LlamadoRepository(_context);
            Departamentos = new DepartamentoRepository(_context);
            Inscripciones = new InscripcionRepository(_context);
            AutodefinicionesLey = new AutodefinicionLeyRepository(_context);
            RequisitosExcluyentes = new RequisitoExcluyenteRepository(_context);
            RequisitosPostulante = new RequisitoPostulanteRepository(_context);
            ItemsPuntuables = new ItemPuntuableRepository(_context);
            MeritosPostulante = new MeritoPostulanteRepository(_context);
            ApoyosNecesarios = new ApoyoNecesarioRepository(_context);
            ApoyosSolicitados = new ApoyoSolicitadoRepository(_context);
            Constancias = new ConstanciaRepository(_context);
        }
        public IPostulanteRepository Postulantes { get; private set; }
        public ILlamadoRepository Llamados { get; private set; }
        public IDepartamentoRepository Departamentos { get; private set; }
        public IInscripcionRepository Inscripciones { get; private set; }
        public IAutodefinicionLeyRepository AutodefinicionesLey { get; private set; }
        public IRequisitoExcluyenteRepository RequisitosExcluyentes { get; private set; }
        public IRequisitoPostulanteRepository RequisitosPostulante { get; private set; }
        public IItemPuntuableRepository ItemsPuntuables { get; private set; }
        public IMeritoPostulanteRepository MeritosPostulante { get; private set; }
        public IApoyoNecesarioRepository ApoyosNecesarios { get; private set; }
        public IApoyoSolicitadoRepository ApoyosSolicitados { get; private set; }
        public IConstanciaRepository Constancias { get; private set; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
