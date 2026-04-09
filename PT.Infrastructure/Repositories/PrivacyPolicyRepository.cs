using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Enums;
using PT.Domain.Models;
using PT.Infrastructure.Common;
using PT.Infrastructure.Mappings;

namespace PT.Infrastructure.Repositories;

internal sealed class PrivacyPolicyRepository(PostgreSqlDbContext context) : IPrivacyPolicyRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task<PrivacyPolicy?> GetPrivacyPolicyAsync(UserType userType, CancellationToken ct = default)
    {
        var entity = await _context.PrivacyPolicies
            .AsNoTracking()
            .Where(x => x.UserType == userType)
            .OrderByDescending(x => x.Version)
            .FirstOrDefaultAsync(ct);
        
        return entity?.ToDomain();
    }
}
