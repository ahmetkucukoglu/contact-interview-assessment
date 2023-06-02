using CorrelationId.Abstractions;

namespace ContactApp.Shared.HttpServices.Middlewares;

public class RefitHeaderHandler : DelegatingHandler
{
    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    public RefitHeaderHandler(ICorrelationContextAccessor correlationContextAccessor)
    {
        _correlationContextAccessor = correlationContextAccessor;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("x-correlation-id", _correlationContextAccessor.CorrelationContext.CorrelationId);
        
        return base.SendAsync(request, cancellationToken);
    }
}