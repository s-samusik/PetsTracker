using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Models;
using PT.Infrastructure.Common;
using PT.Infrastructure.Mappings;

namespace PT.Infrastructure.Repositories;

internal sealed class UserRepository(PostgreSqlDbContext context) : IUserRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task<bool> ExistsAsync(string phoneNumber, CancellationToken ct = default)
        => await _context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, ct);

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, ct);

        return entity?.ToDomain();
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity?.ToDomain();
    }

    public async Task AddAsync(User model, CancellationToken ct = default)
    {
        var entity = model.ToEntity();
        await _context.Users.AddAsync(entity, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);

    public void Update(User model)
    {
        var entity = _context.Users.First(x => x.Id == model.Id);
        
        entity.PhoneNumber = model.PhoneNumber;
    }
}
