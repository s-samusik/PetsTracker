using PT.Domain.Entities;
using PT.Domain.Enums;

namespace PT.Application.Interfaces.Services;

public interface ICodeService
{
    string Generate();
    bool Validate(string code);
    Task UpdateAsync(Code code, CancellationToken ct = default);
    Task<IReadOnlyList<Code>> GenerateBatchAsync(int count, CancellationToken ct = default);
    Task<Code?> GetAsync(Guid Id,  CancellationToken ct = default);
    Task<Code?> GetByValueAsync(string code, CancellationToken ct = default);
    Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default);
}
