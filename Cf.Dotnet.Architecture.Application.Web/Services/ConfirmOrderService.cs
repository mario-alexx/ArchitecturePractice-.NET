using MediatR;

namespace Cf.Dotnet.Architecture.Application.Services;

public sealed record ConfirmOrderService(int OrderId) : IRequest<Unit>;