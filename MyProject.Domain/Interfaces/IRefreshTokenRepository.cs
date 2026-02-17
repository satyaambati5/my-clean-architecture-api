using MyProject.Domain.Entities;

namespace MyProject.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(int userId);
        Task AddAsync(RefreshToken refreshToken);
        Task UpdateAsync(RefreshToken refreshToken);
        Task RevokeTokenAsync(string token, string revokedByIp);
        Task RevokeAllUserTokensAsync(int userId);
    }
}