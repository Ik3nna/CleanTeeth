using CleanTeeth.Application.Common.Exceptions;
using MediatR;

namespace CleanTeeth.Application.Common.Behaviours;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (CustomValidationException)
        {
            throw; // let your middleware return a proper HTTP response
        }
        catch (Exception ex)
        {
            // Log exception here
            throw new MediatorException(
                $"An unhandled error occurred while processing {typeof(TRequest).Name}.",
                ex);
        }
    }
}

