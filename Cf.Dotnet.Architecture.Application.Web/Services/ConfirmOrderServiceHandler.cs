using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.DotnetArchitecture.SeedWork;
using MediatR;

namespace Cf.Dotnet.Architecture.Application.Services;

internal sealed class ConfirmOrderServiceHandler : IRequestHandler<ConfirmOrderService, Unit>
{
    private readonly IRepository<Buyer> buyerRepository;
    private readonly IRepository<Order> orderRepository;

    public ConfirmOrderServiceHandler(IRepository<Order> orderRepository, IRepository<Buyer> buyerRepository)
    {
        this.orderRepository = orderRepository;
        this.buyerRepository = buyerRepository;
    }

    public async Task<Unit> Handle(ConfirmOrderService request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindAsync(request.OrderId);
        var buyer = await buyerRepository.FindAsync(order.BuyerId);

        buyer.UpdateBalance(order.GetTotal());
        buyerRepository.Update(buyer);

        order.Confirm();
        orderRepository.Update(order);

        await orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}