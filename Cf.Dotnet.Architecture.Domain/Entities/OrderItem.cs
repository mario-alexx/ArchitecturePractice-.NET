using Cf.Dotnet.Architecture.Domain.Exceptions;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.Dotnet.Architecture.Domain.Specifications;

namespace Cf.Dotnet.Architecture.Domain.Entities;

/// <summary>
///     Representa un ítem dentro de una orden de compra.
/// </summary>
public class OrderItem : IEntity
{
    /// <summary>
    ///     Constructor para crear una instancia de OrderItem.
    /// </summary>
    /// <param name="id">Identificador único del ítem.</param>
    /// <param name="productId">Identificador del producto asociado al ítem.</param>
    /// <param name="productName">Nombre del producto.</param>
    /// <param name="unitPrice">Precio por unidad del producto.</param>
    /// <param name="units">Cantidad de unidades del producto (por defecto es 1).</param>
    /// <exception cref="OrderingDomainException">Lanzada si el número de unidades no satisface la especificación.</exception>
    public OrderItem(int id, int productId, string productName, decimal unitPrice, int units = 1)
    {
        // Validación del número de unidades usando la especificación definida.
        if (new UnitsSpecification().IsNotSatisfiedBy(units))
            throw new OrderingDomainException($"Invalid number of units: {units}");

        Id = id;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Units = units;
    }

    public int Units { get; private set; } // Cantidad de unidades del ítem.
    public decimal UnitPrice { get; private set; } // Precio por unidad del ítem.
    public string ProductName { get; private set; } // Nombre del producto asociado.
    public int ProductId { get; private set; } // ID del producto asociado.

    // Propiedades de la clase OrderItem.
    public int Id { get; init; } // Identificador único del ítem.

    /// <summary>
    ///     Añade unidades adicionales al ítem de la orden.
    /// </summary>
    /// <param name="nextUnits">Cantidad de unidades a añadir.</param>
    /// <exception cref="OrderingDomainException">Lanzada si el número de unidades no satisface la especificación.</exception>
    public void AddUnits(int nextUnits)
    {
        // Validación del número de unidades adicionales.
        if (new UnitsSpecification().IsNotSatisfiedBy(nextUnits))
            throw new OrderingDomainException($"Invalid number of units: {nextUnits}");

        Units += nextUnits;
    }
}