using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Requests;
using CarStockApi.Auth.Responses;
using FastEndpoints;


namespace CarStockApi.Auth;


public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IAuthService _authService;

    public Login(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task<LoginResponse> ExecuteAsync(LoginRequest req, CancellationToken ct)
    {
        try
        {
            return await _authService.LoginAsync(req);
        }
        catch (Exception ex)
        {
            ThrowError(ex.Message);
            return null!;
        }
    }
}
