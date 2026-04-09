using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Interfaces.Repositories;

public interface IPrivacyPolicyRepository
{
    Task<PrivacyPolicy?> GetPrivacyPolicyAsync(UserType userType, CancellationToken ct = default);
}
