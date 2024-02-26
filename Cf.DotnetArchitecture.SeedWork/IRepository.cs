using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.DotnetArchitecture.SeedWork;

/// <summary>
///     Define una interfaz genérica para un repositorio que maneja entidades.
/// </summary>
/// <typeparam name="T">El tipo de entidad que el repositorio manejará.</typeparam>
public interface IRepository<T> : IQueryable<T> where T : IEntity
{
    /// <summary>
    ///     Obtiene la unidad de trabajo asociada con el repositorio.
    /// </summary>
    public IUnitOfWork UnitOfWork { get; }

    /// <summary>
    ///     Añade una entidad al repositorio.
    /// </summary>
    /// <param name="entity">La entidad a añadir.</param>
    void Add(T entity);

    /// <summary>
    ///     Actualiza una entidad en el repositorio.
    /// </summary>
    /// <param name="entity">La entidad a actualizar.</param>
    void Update(T entity);

    /// <summary>
    ///     Elimina una entidad del repositorio.
    /// </summary>
    /// <param name="entity">La entidad a eliminar.</param>
    void Remove(T entity);

    /// <summary>
    ///     Busca una entidad por su identificador de forma asíncrona.
    /// </summary>
    /// <param name="id">El identificador de la entidad a buscar.</param>
    /// <returns>La entidad si es encontrada, de lo contrario null.</returns>
    Task<T> FindAsync(int id);

    /// <summary>
    ///     Obtiene una lista de todas las entidades en el repositorio de forma asíncrona.
    /// </summary>
    /// <returns>Una lista de entidades.</returns>
    Task<List<T>> ToListAsync();
}