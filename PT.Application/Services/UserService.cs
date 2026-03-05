using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Entities;

namespace PT.Application.Services;

internal sealed class UserService(IUserRepository repo) : IUserService
{
    private readonly IUserRepository _userRepository = repo;


    public async Task<bool> ExistsAsync(string phoneNumber, CancellationToken ct = default)
    {
       return await _userRepository.ExistsAsync(phoneNumber, ct);
    }

    public async Task<User?> GetByPhoneAsync(string phoneNumber, CancellationToken ct = default)
    {
        return await _userRepository.GetByPhoneNumberAsync(phoneNumber, ct);
    }

    public async Task<User> RegisterAsync(string phoneNumber, CancellationToken ct = default)
    {
        if (await ExistsAsync(phoneNumber, ct))
        {
            throw new InvalidOperationException($"User with phone {phoneNumber} already exists.");
        }

        var user = new User { Id = Guid.NewGuid(), PhoneNumber = phoneNumber };

        await _userRepository.AddAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);

        return user;
    }
}
