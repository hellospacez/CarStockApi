using CarStockApi.Models;

namespace CarStockApi.Auth.Interfaces;

public interface IAuthRepository
{
    Task<Dealer> GetDealerByUsernameAsync(string username);
    Task AddDealerAsync(string username, string passwordHash);
}