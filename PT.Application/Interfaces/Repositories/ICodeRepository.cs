using PT.Domain.Entities;
using PT.Domain.Enums;

namespace PT.Application.Interfaces.Repositories;

public interface ICodeRepository : IBaseRepository<Code>
{
    Task<Code?> GetByValueAsync(string code, CancellationToken ct = default);
    Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default);
}
