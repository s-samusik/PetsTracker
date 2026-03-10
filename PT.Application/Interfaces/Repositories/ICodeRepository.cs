using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Interfaces.Repositories;

public interface ICodeRepository : IBaseRepository<Code>
{
    Task<Code?> GetByValueAsync(string value, CancellationToken ct = default);
    Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default);
}