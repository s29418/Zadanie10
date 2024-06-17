using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Zadanie10.Context;
using Zadanie10.DTO_s;
using Zadanie10.Entities;
using Zadanie10.Helpers;

namespace Zadanie10.Services;

public class AccountService : IAccountService
{
    private readonly ClinicDbContext _context;
    private readonly IConfiguration _configuration;

    public AccountService(ClinicDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<bool> RegisterUserAsync(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<(bool Success, string AccessToken, string RefreshToken)> AuthenticateUserAsync(
        LoginRequest loginRequest)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginRequest.Login);

        if (user == null) return (false, null, null);

        var passwordHashFromDb = user.Password;
        var curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword) return (false, null, null);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, "user")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds);

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return (true, accessToken, user.RefreshToken);
    }

    public async Task<(bool Success, string AccessToken, string RefreshToken)> RefreshTokenAsync(
        RefreshTokenRequest refreshTokenRequest)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenRequest.RefreshToken);

        if (user == null || user.RefreshTokenExp < DateTime.Now) return (false, null, null);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, "user")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds);

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return (true, accessToken, user.RefreshToken);
    }
}