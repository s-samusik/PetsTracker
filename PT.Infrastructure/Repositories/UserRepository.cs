using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class UserRepository(PostgreSqlDbContext context) : BaseRepository<User>(context), IUserRepository
{
}
