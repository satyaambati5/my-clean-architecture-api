using MyProject.Domain.Entities;

namespace MyProject.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        int? ValidateAccessToken(string token);
    }
}