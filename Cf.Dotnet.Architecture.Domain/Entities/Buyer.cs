using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Architecture.Domain.Entities;

/// <summary>
///     Representa un comprador en el dominio.
/// </summary>
public class Buyer : IEntity
{
    /// <summary>
    ///     Constructor para crear una instancia de Buyer con ID y nombre.
    /// </summary>
    /// <param name="id">El identificador único del comprador.</param>
    /// <param name="name">El nombre del comprador.</param>
    public Buyer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; } // Nombre del comprador.
    public decimal Balance { get; private set; } // Saldo actual del comprador.

    // Propiedades públicas de la clase Buyer.
    public int Id { get; init; } // Identificador único del comprador.

    /// <summary>
    ///     Actualiza el saldo del comprador.
    /// </summary>
    /// <param name="amount">El monto por el cual se actualizará el saldo.</param>
    public void UpdateBalance(decimal amount)
    {
        // La lógica actual reduce el saldo, se podría modificar para manejar diferentes casos.
        Balance -= amount;
    }
}