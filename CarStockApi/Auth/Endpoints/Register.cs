using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Requests;
using FastEndpoints;

namespace CarStockApi.Auth;




public class Register : Endpoint<RegisterRequest>
{
    private readonly IAuthService _authService;

    public Register(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        try
        {
            await _authService.RegisterAsync(req);
            await SendOkAsync(ct);
        }
        catch (Exception ex)
        {
            AddError(r => r.Username, ex.Message);
            await SendErrorsAsync(cancellation: ct);
        }
    }
}