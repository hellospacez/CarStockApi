using FastEndpoints;

namespace CarStockApi.Endpoints;

public class HelloRequest
{
    public string Name { get; set; }
}

public class HelloEndpoint : Endpoint<HelloRequest>
{
    public override void Configure()
    {
        Post("/hello");
        AllowAnonymous();
    }

    public override async Task HandleAsync(HelloRequest req, CancellationToken ct)
    {
        await SendAsync(new { Message = $"Hello, {req.Name}!" }, cancellation: ct);
    }
}
