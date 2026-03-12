using BuildingBlocks.Core.Results;

namespace BuildingBlocks.Core.Pipeline;

//Represents execute the next step in the pipeline and return the result.
public delegate Task<Result<TResponse>> RequestPipeline<TResponse>();