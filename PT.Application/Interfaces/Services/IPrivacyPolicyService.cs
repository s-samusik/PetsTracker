using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Interfaces.Services;

public interface IPrivacyPolicyService
{
    Task<PrivacyPolicy?> GetLatestByUserTypeAsync(UserType userType, CancellationToken ct = default);
    Task<PrivacyPolicy?> AddByUserTypeAsync(string text, UserType userType, CancellationToken ct = default);
}
