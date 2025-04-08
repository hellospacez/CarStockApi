using CarStockApi.Auth.Requests;
using CarStockApi.Auth.Responses;

namespace CarStockApi.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponseModel> LoginAsync(LoginRequestModel requestModel);
    Task RegisterAsync(RegisterRequestModel requestModel);
}