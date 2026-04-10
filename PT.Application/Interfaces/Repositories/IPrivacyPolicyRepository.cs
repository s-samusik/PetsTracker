using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Interfaces.Repositories;

public interface IPrivacyPolicyRepository
{
    Task<PrivacyPolicy?> GetLatestByUserTypeAsync(UserType userType, CancellationToken ct = default);
    Task AddAsync(PrivacyPolicy privacyPolicy, CancellationToken ct = default);
}
