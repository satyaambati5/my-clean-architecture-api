using MyProject.Application.DTOs;
using MyProject.Common.Models;

namespace MyProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto registerDto, string ipAddress);
        Task<Result<AuthResponseDto>> LoginAsync(LoginDto loginDto, string ipAddress);
        Task<Result<AuthResponseDto>> RefreshTokenAsync(string token, string ipAddress);
        Task<Result> RevokeTokenAsync(string token, string ipAddress);
        Task<Result<UserDto>> GetCurrentUserAsync(int userId);
    }
}