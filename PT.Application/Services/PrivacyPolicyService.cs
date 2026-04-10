using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Services;

internal sealed class PrivacyPolicyService
    (IPrivacyPolicyRepository privacyPolicyRepository, IUnitOfWork uow) : IPrivacyPolicyService
{
    private readonly IPrivacyPolicyRepository _privacyPolicyRepository = privacyPolicyRepository;
    private readonly IUnitOfWork _uow = uow;

    public async Task<PrivacyPolicy?> AddByUserTypeAsync(string text, UserType userType, CancellationToken ct = default)
    {
        var latest = await GetLatestByUserTypeAsync(userType, ct);

        var newVersion = (latest?.Version ?? 0) + 1;

        var privacyPolicy = PrivacyPolicy.Create(newVersion, text, userType);

        await _privacyPolicyRepository.AddAsync(privacyPolicy, ct);
        await _uow.SaveChangesAsync(ct);

        return privacyPolicy;
    }

    public async Task<PrivacyPolicy?> GetLatestByUserTypeAsync(UserType userType, CancellationToken ct = default)
    {
        return await _privacyPolicyRepository.GetLatestByUserTypeAsync(userType, ct);
    }
}
