using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Services;

internal sealed class PrivacyPolicyService
    (IPrivacyPolicyRepository privacyPolicyRepository) : IPrivacyPolicyService
{
    private readonly IPrivacyPolicyRepository _privacyPolicyRepository = privacyPolicyRepository;
    
    public async Task<PrivacyPolicy?> GetPrivacyPolicyAsync(UserType userType, CancellationToken ct = default)
    {
        return await _privacyPolicyRepository.GetPrivacyPolicyAsync(userType, ct);
    }
}
