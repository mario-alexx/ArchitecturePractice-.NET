using Cf.Dotnet.Architecture.Domain.Enums;
using Cf.Dotnet.Architecture.Domain.Exceptions;
using Cf.Dotnet.Architecture.Domain.SeedWork;

namespace Cf.Dotnet.Architecture.Domain.Entities;

/// <summary>
///     Representa una orden de compra en el dominio.
/// </summary>
public class Order : IEntity
{
    // Este es un patron que mantiene una lista que no puede ser modificada desde fuera de la clase.
    // Esto es para evitar que se agreguen o eliminen ítems de la orden sin pasar por los métodos del dominio.
    private readonly List<OrderItem> orderItems = [];

    /// <summary>
    ///     Inicializa una nueva instancia de la clase Order con un identificador y el ID del comprador.
    /// </summary>
    /// <param name="id">El identificador único de la orden.</param>
    /// <param name="buyerId">El identificador del comprador asociado a la orden.</param>
    public Order(int id, int buyerId)
    {
        Id = id;
        OrderStatus = OrderStatus.Created; // El estado inicial de la orden es 'Creado'.
        BuyerId = buyerId;
    }

    public int BuyerId { get; private set; } // Identificador del comprador asociado.
    public OrderStatus OrderStatus { get; private set; } // Estado actual de la orden.
    public IEnumerable<OrderItem> OrderItems => orderItems.AsReadOnly();

    // Propiedades públicas de la clase.
    public int Id { get; init; } // Identificador único de la orden.

    /// <summary>
    ///     Agrega un ítem a la orden.
    /// </summary>
    /// <param name="productId">ID del producto.</param>
    /// <param name="productName">Nombre del producto.</param>
    /// <param name="unitPrice">Precio unitario del producto.</param>
    /// <param name="discount">Descuento aplicable al producto.</param>
    /// <param name="units">Cantidad de unidades del producto (1 por defecto).</param>
    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
    {
        // Buscar si el producto ya existe en la orden.
        var existingOrderForProduct = orderItems.SingleOrDefault(o => o.Id == productId);

        // Si el producto existe, aumentar las unidades; si no, agregar un nuevo OrderItem.
        if (existingOrderForProduct is not null)
        {
            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            var orderItem = new OrderItem(
                default, // ID por defecto.
                productId,
                productName,
                unitPrice,
                units
            );

            orderItems.Add(orderItem);
        }
    }

    /// <summary>
    ///     Cancela la orden, solo si no ha sido enviada.
    /// </summary>
    /// <exception cref="OrderingDomainException">Si la orden ya ha sido enviada.</exception>
    public void Cancel()
    {
        // Verificar que la orden no haya sido enviada.
        if (OrderStatus == OrderStatus.Shipped)
            throw new OrderingDomainException($"Not possible to cancel order in {OrderStatus} status");

        OrderStatus = OrderStatus.Cancelled;
    }

    /// <summary>
    ///     Calcula el total de la orden sumando el precio de cada ítem.
    /// </summary>
    /// <returns>Total de la orden.</returns>
    public decimal GetTotal()
    {
        return orderItems.Sum(o => o.Units * o.UnitPrice);
    }

    /// <summary>
    ///     Confirma la orden, solo si no ha sido enviada o cancelada.
    /// </summary>
    /// <exception cref="OrderingDomainException">Si la orden ya ha sido enviada o cancelada.</exception>
    public void Confirm()
    {
        // Verificar que la orden no haya sido enviada o cancelada.
        if (OrderStatus is OrderStatus.Shipped or OrderStatus.Cancelled)
            throw new OrderingDomainException($"Not possible to cancel order in {OrderStatus} status");

        OrderStatus = OrderStatus.Confirmed;
    }
}