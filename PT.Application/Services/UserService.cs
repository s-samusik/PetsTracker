using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Entities;

namespace PT.Application.Services;

internal sealed class UserService(IUserRepository userRepository, PhoneNumberService phoneNumberService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly PhoneNumberService _phoneNumberService = phoneNumberService;

    public async Task<bool> ExistsAsync(string number, CancellationToken ct = default)
    {
        var phoneNumber = _phoneNumberService.NormalizeAndValidate(number);

        return await _userRepository.ExistsAsync(phoneNumber, ct);
    }

    public async Task<User> GetByPhoneAsync(string number, CancellationToken ct = default)
    {
        var phoneNumber = _phoneNumberService.NormalizeAndValidate(number);

        var result = await _userRepository.GetByPhoneNumberAsync(phoneNumber, ct);

        return result is null
            ? throw new KeyNotFoundException($"User by phone number '{phoneNumber}' not found")
            : result;
    }

    public async Task<User> RegisterAsync(string number, CancellationToken ct = default)
    {
        if (await ExistsAsync(number, ct))
        {
            throw new InvalidOperationException($"User with phone number '{number}' already exists.");
        }

        var phoneNumber = _phoneNumberService.NormalizeAndValidate(number);

        var user = new User { Id = Guid.NewGuid(), PhoneNumber = phoneNumber };

        await _userRepository.AddAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);

        return user;
    }
}
