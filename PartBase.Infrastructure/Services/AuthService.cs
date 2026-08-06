using Microsoft.AspNetCore.Identity;
using PartBase.Application.DTOs.Auth;
using PartBase.Application.Interfaces;
using PartBase.Infrastructure.Identity;

namespace PartBase.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var exists = await _userManager.FindByEmailAsync(request.Email);

        if (exists != null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Bu e-posta adresi zaten kayıtlı."
            };
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Success = false,
                Message = string.Join(", ", result.Errors.Select(x => x.Description))
            };
        }

        return new AuthResponse
        {
            Success = true,
            Message = "Kullanıcı başarıyla oluşturuldu."
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        return new AuthResponse
        {
            Success = false,
            Message = "Henüz uygulanmadı."
        };
    }
}