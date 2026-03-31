using PT.Domain.Models;

namespace PT.Application.Interfaces.Services;

public interface IUserService
{
    Task<User> GetByIdAsync(Guid userId, CancellationToken ct = default);
    Task<User?> GetByPhoneAsync(string phoneNumber, CancellationToken ct = default);
    Task<bool> ExistsAsync(string phoneNumber, CancellationToken ct = default);
    Task<User> RegisterAsync(string phoneNumber, CancellationToken ct = default);
    Task UpdateAsync(User user, CancellationToken ct = default);
}
