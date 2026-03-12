using BuildingBlocks.Core.CQRS.Requests;

namespace BuildingBlocks.Core.CQRS.Commands;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
   where TCommand : ICommand<TResponse>
{
}