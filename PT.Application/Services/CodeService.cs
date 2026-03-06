using Npgsql;
using PT.Application.Extensions;
using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.CodeFormats;
using PT.Domain.Entities;
using PT.Domain.Enums;

namespace PT.Application.Services;

internal sealed class CodeService(ICodeFormat format, ICodeRepository repo) : ICodeService
{
    private readonly ICodeFormat _format = format;
    private readonly ICodeRepository _codeRepository = repo;


    public string Generate()
    {
        var baseCode = _format.GenerateBase();
        var checksum = _format.ComputeChecksum(baseCode);

        return baseCode + checksum;
    }

    public bool Validate(string code)
    {
        return _format.ValidateFormat(code) && _format.ValidateChecksum(code);
    }

    public async Task<IReadOnlyList<Code>> GenerateBatchAsync(int count, CancellationToken ct = default)
    {
        var result = new List<Code>(count);

        while (result.Count < count)
        {
            try
            {
                var entity = new Code { Value = Generate(), State = CodeState.Generated };

                await _codeRepository.AddAsync(entity, ct);
                await _codeRepository.SaveChangesAsync(ct);

                result.Add(entity);
            }
            catch (Exception ex) when (ex.IsPostgreDbError(PostgresErrorCodes.UniqueViolation))
            {
                // the code is duplicate, just skip it
            }
            catch (Exception)
            {
                throw;
            }
        }

        return result;
    }

    public async Task<Code?> GetAsync(Guid Id, CancellationToken ct = default)
    {
        return await _codeRepository.GetByIdAsync(Id, ct);
    }

    public async Task<Code?> GetByValueAsync(string code, CancellationToken ct = default)
    {
        return await _codeRepository.GetByValueAsync(code, ct);
    }

    public async Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default)
    {
        return await _codeRepository.GetAllByStateAsync(state, ct);    
    }
}
