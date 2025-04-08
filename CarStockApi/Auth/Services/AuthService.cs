using System.Security.Claims;
using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Requests;
using CarStockApi.Auth.Responses;
using CarStockApi.Data;
using Dapper;
using FastEndpoints.Security;
using Microsoft.Data.Sqlite;

namespace CarStockApi.Auth.Services;


public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task RegisterAsync(RegisterRequestModel req)
    {
        var existingDealer = await _authRepository.GetDealerByUsernameAsync(req.Username);
        if (existingDealer != null)
        {
            throw new Exception("Username already exists");
        }

        await _authRepository.AddDealerAsync(req.Username, req.Password);
    }

    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel req)
    {
        var dealer = await _authRepository.GetDealerByUsernameAsync(req.Username);
        if (dealer == null || dealer.PasswordHash != req.Password)
        {
            throw new Exception("Invalid username or password");
        }

        var claims = new[]
        {
            new Claim("DealerId", dealer.Id.ToString()),
            new Claim(ClaimTypes.Name, dealer.Username)
        };

        var token = JWTBearer.CreateToken(
            signingKey: "super-secret-key-super-secret-key",
            expireAt: DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingStyle: TokenSigningStyle.Symmetric
        );

        return new LoginResponseModel { Token = token };
    }
}