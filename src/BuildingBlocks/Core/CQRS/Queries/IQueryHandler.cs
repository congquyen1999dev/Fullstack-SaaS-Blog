using BuildingBlocks.Core.CQRS.Requests;

namespace BuildingBlocks.Core.CQRS.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
   
}