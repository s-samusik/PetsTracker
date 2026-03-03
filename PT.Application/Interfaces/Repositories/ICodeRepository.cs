using PT.Domain.Entities;

namespace PT.Application.Interfaces.Repositories;

public interface ICodeRepository : IBaseRepository<Code>
{
    Task<Code?> GetByValueAsync(string code, CancellationToken ct = default);
}
