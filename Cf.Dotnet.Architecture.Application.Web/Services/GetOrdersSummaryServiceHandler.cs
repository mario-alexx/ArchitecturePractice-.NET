using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cf.Dotnet.Architecture.Application.Services;

internal sealed class GetOrdersSummaryServiceHandler : IRequestHandler<GetOrdersSummaryService, List<OrdersSummary>>
{
    private readonly DatabaseContext _context;

    public GetOrdersSummaryServiceHandler(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<OrdersSummary>> Handle(GetOrdersSummaryService request, CancellationToken cancellationToken)
    {
        var ordersSummaries = await _context.Database.SqlQueryRaw<OrdersSummary>(
                """
                  select
                      o.Id as Id,
                      o.OrderStatus as Status,
                      SUM(oi.UnitPrice * oi.Units) as Total,
                      b.Name as BuyerName
                  from Orders o
                  join OrderItems oi on o.Id = oi.OrderId
                  join Buyers b on o.BuyerId = b.Id
                  group by o.Id, o.OrderStatus, b.Name
                """)
            .ToListAsync(cancellationToken);

        return ordersSummaries;
    }
}