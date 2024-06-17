using Zadanie10.DTO_s;

namespace Zadanie10.Services;

public interface IAccountService
{
    Task<bool> RegisterUserAsync(RegisterRequest model);
    Task<(bool Success, string AccessToken, string RefreshToken)> AuthenticateUserAsync(LoginRequest loginRequest);
    Task<(bool Success, string AccessToken, string RefreshToken)> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
}