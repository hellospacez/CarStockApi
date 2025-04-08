using CarStockApi.Auth.Requests;
using CarStockApi.Auth.Responses;

namespace CarStockApi.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
}