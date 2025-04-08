using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Requests;
using CarStockApi.Auth.Responses;
using FastEndpoints;

namespace CarStockApi.Auth;

public class AuthEndpoint
{
    public class Login : Endpoint<LoginRequestModel, LoginResponseModel>
    {
        private readonly IAuthService _authService;
        public Login(IAuthService authService) => _authService = authService;

        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();
        }

        public override async Task<LoginResponseModel> ExecuteAsync(LoginRequestModel req, CancellationToken ct)
        {
            try { return await _authService.LoginAsync(req); }
            catch (Exception ex) { ThrowError(ex.Message); return null!; }
        }
    }

    public class Register : Endpoint<RegisterRequestModel>
    {
        private readonly IAuthService _authService;
        public Register(IAuthService authService) => _authService = authService;

        public override void Configure()
        {
            Post("/register");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequestModel req, CancellationToken ct)
        {
            try { await _authService.RegisterAsync(req); await SendOkAsync(ct); }
            catch (Exception ex) { AddError(r => r.Username, ex.Message); await SendErrorsAsync(cancellation: ct); }
        }
    }
}