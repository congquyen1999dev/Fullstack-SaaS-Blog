using BuildingBlocks.Core.CQRS.Requests;

namespace BuildingBlocks.Core.CQRS.Commands;

public interface ICommand<TResponse> : IRequest<TResponse>;