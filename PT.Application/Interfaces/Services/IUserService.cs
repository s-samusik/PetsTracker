using PT.Domain.Entities;

namespace PT.Application.Interfaces.Services;

public interface IUserService
{
    Task<User> GetByPhoneAsync(string phoneNumber, CancellationToken ct = default);
    Task<bool> ExistsAsync(string phoneNumber, CancellationToken ct = default);
    Task<User> RegisterAsync(string phoneNumber, CancellationToken ct = default);
}
