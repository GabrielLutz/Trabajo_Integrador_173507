using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Unidad de trabajo que coordina los repositorios del Portal DGC y garantiza la integridad
    /// transaccional para los RF críticos (inscripciones, méritos, ordenamientos, etc.).
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Repositorio de postulantes (RF-01 y RF-02).</summary>
        IPostulanteRepository Postulantes { get; }
        /// <summary>Repositorio de llamados (RF-03 y RF-04).</summary>
        ILlamadoRepository Llamados { get; }
        /// <summary>Repositorio de departamentos habilitados (RF-03).</summary>
        IDepartamentoRepository Departamentos { get; }
        /// <summary>Repositorio de inscripciones completas (RF-05, RF-07, RF-08).</summary>
        IInscripcionRepository Inscripciones { get; }
        /// <summary>Repositorio de autodefiniciones Ley 19.122 (cuotas y apoyos).</summary>
        IAutodefinicionLeyRepository AutodefinicionesLey { get; }
        /// <summary>Repositorio de requisitos excluyentes por llamado.</summary>
        IRequisitoExcluyenteRepository RequisitosExcluyentes { get; }
        /// <summary>Repositorio de requisitos cargados por postulante.</summary>
        IRequisitoPostulanteRepository RequisitosPostulante { get; }
        /// <summary>Repositorio de ítems puntuables (RF-11 a RF-15).</summary>
        IItemPuntuableRepository ItemsPuntuables { get; }
        /// <summary>Repositorio de méritos presentados por postulante.</summary>
        IMeritoPostulanteRepository MeritosPostulante { get; }
        /// <summary>Repositorio de apoyos necesarios ofrecidos en llamados.</summary>
        IApoyoNecesarioRepository ApoyosNecesarios { get; }
        /// <summary>Repositorio de apoyos solicitados en cada inscripción.</summary>
        IApoyoSolicitadoRepository ApoyosSolicitados { get; }
        /// <summary>Repositorio de constancias/documentos (RF-06).</summary>
        IConstanciaRepository Constancias { get; }
        /// <summary>Repositorio de pruebas tomadas por el tribunal.</summary>
        IPruebaRepository Pruebas { get; }
        /// <summary>Repositorio de evaluaciones de pruebas (RF-12).</summary>
        IEvaluacionPruebaRepository EvaluacionesPruebas { get; }
        /// <summary>Repositorio de evaluaciones de méritos (RF-14).</summary>
        IEvaluacionMeritoRepository EvaluacionesMeritos { get; }
        /// <summary>Repositorio de ordenamientos (RF-15).</summary>
        IOrdenamientoRepository Ordenamientos { get; }
        /// <summary>Repositorio de posiciones dentro de cada ordenamiento.</summary>
        IPosicionOrdenamientoRepository PosicionesOrdenamiento { get; }

        /// <summary>
        /// Confirma los cambios pendientes en el contexto.
        /// </summary>
        /// <returns>
        /// Cantidad de registros afectados en la persistencia.
        /// </returns>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// Inicia una transacción explícita sobre la base de datos.
        /// </summary>
        Task BeginTransactionAsync();
        /// <summary>
        /// Confirma la transacción activa persistiendo los cambios.
        /// </summary>
        Task CommitTransactionAsync();
        /// <summary>
        /// Revierte la transacción activa para garantizar consistencia.
        /// </summary>
        Task RollbackTransactionAsync();
    }
}
