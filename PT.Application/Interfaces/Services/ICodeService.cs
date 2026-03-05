using PT.Domain.Entities;

namespace PT.Application.Interfaces.Services;

public interface ICodeService
{
    string Generate();
    bool Validate(string code);
    Task<IReadOnlyList<Code>> GenerateBatchAsync(int count, CancellationToken ct = default);
}
