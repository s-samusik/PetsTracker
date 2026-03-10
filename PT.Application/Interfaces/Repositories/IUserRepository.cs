using PT.Domain.Models;

namespace PT.Application.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> ExistsAsync(string phoneNumber, CancellationToken ct = default);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default);
}