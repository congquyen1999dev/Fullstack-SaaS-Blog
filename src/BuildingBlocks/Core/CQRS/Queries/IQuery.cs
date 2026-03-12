using BuildingBlocks.Core.CQRS.Requests;

namespace BuildingBlocks.Core.CQRS.Queries;

public interface IQuery<TResponse> : IRequest<TResponse>;