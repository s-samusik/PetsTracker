using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Models;

namespace PT.Application.Services;

internal sealed class UserService
    (IUserRepository userRepository, PhoneNumberService phoneNumberService, IUnitOfWork uow) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly PhoneNumberService _phoneNumberService = phoneNumberService;
    private readonly IUnitOfWork _uow = uow;

    public async Task<bool> ExistsAsync(string number, CancellationToken ct = default)
    {
        var phoneNumber = _phoneNumberService.NormalizeAndValidate(number);

        return await _userRepository.ExistsAsync(phoneNumber, ct);
    }

    public async Task<User?> GetByPhoneAsync(string number, CancellationToken ct = default)
    {
        var phoneNumber = _phoneNumberService.NormalizeAndValidate(number);
        
        return await _userRepository.GetByPhoneNumberAsync(phoneNumber, ct);
    }

    public async Task<User> RegisterAsync(string number, CancellationToken ct = default)
    {
        var phoneNumber = _phoneNumberService.NormalizeAndValidate(number);

        if (await _userRepository.ExistsAsync(phoneNumber, ct))
            throw new InvalidOperationException($"User '{phoneNumber}' already exists");

        var user = User.Create(phoneNumber);

        await _userRepository.AddAsync(user, ct);
        await _uow.SaveChangesAsync(ct);

        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _userRepository.Update(user);
        
        await _uow.SaveChangesAsync(ct);
    }
}
