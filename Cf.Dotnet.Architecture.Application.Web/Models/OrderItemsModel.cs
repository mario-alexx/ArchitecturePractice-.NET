using Cf.Dotnet.Architecture.Domain.Enums;

namespace Cf.Dotnet.Architecture.Application.Models;

public sealed record OrdersSummary(int Id, OrderStatus Status, decimal Total, string BuyerName);