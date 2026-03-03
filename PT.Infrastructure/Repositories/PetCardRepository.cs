using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class PetCardRepository(PostgreSqlDbContext context) : BaseRepository<PetCard>(context), IPetCardRepository
{
}
