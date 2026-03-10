using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.CodeFormats;
using PT.Domain.Enums;

namespace PT.Application.Services;

internal sealed class CodeService
    (ICodeFormat format, ICodeRepository codeRepository, IUnitOfWork uow) : ICodeService
{
    private readonly ICodeFormat _format = format;
    private readonly ICodeRepository _codeRepository = codeRepository;
    private readonly IUnitOfWork _uow = uow;

    public string Generate()
    {
        var baseCode = _format.GenerateBase();
        var checksum = _format.ComputeChecksum(baseCode);

        return $"{baseCode}{checksum}";
    }

    public bool Validate(string code)
        => _format.ValidateFormat(code) && _format.ValidateChecksum(code);

    public async Task<Code?> GetAsync(Guid id, CancellationToken ct = default)
        => await _codeRepository.GetByIdAsync(id, ct);

    public async Task<Code?> GetByValueAsync(string value, CancellationToken ct = default)
        => await _codeRepository.GetByValueAsync(value, ct);

    public async Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default)
        => await _codeRepository.GetAllByStateAsync(state, ct);

    public async Task UpdateAsync(Code code, CancellationToken ct = default)
    {
        _codeRepository.Update(code);
        
        await _uow.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Code>> GenerateBatchAsync(int count, CancellationToken ct = default)
    {
        if (count is < 1 or > 1000)
            throw new ArgumentException("Count must be between 1 and 1000");

        var result = new List<Code>(count);

        while (result.Count < count)
        {
            try
            {
                var value = Generate();
                var code = Code.Generate(value);

                await _codeRepository.AddAsync(code, ct);
                await _uow.SaveChangesAsync(ct);

                result.Add(code);
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate"))
            {
                // the code is duplicate, just skip it
            }
        }

        return result;
    }
}
