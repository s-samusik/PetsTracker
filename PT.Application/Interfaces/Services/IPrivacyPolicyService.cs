using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Interfaces.Services;

public interface IPrivacyPolicyService
{
    Task<PrivacyPolicy?> GetPrivacyPolicyAsync(UserType userType, CancellationToken ct = default);
}
