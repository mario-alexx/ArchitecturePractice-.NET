using Cf.Dotnet.Architecture.Application.Models;
using MediatR;

namespace Cf.Dotnet.Architecture.Application.Services;

public sealed record GetOrdersSummaryService : IRequest<List<OrdersSummary>>;