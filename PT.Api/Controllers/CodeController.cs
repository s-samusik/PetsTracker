using Microsoft.AspNetCore.Mvc;
using PT.Api.Models;
using PT.Application.Interfaces.Services;
using PT.Domain.Entities;
using PT.Domain.Enums;

namespace PT.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CodeController(ICodeService codeService) : ControllerBase
{
    private readonly ICodeService _codeService = codeService;


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken ct = default)
    {
        var response = await _codeService.GetAsync(id, ct);

        return Ok(response);
    }

    [HttpGet("{code:maxlength(7)}")]
    public async Task<IActionResult> GetByValueAsync(string code, CancellationToken ct = default)
    {
        var response = await _codeService.GetByValueAsync(code, ct);

        return Ok(response);
    }

    [HttpGet("state")]
    public async Task<IActionResult> GetAllByStateAsync([FromQuery] CodeState state, CancellationToken ct = default)
    {
        var items = await _codeService.GetAllByStateAsync(state, ct);
        var response = new ListResponse<Code>(items.Count, Data: items);

        return Ok(response);
    }

    [HttpGet("generate/{count:int}")]
    public async Task<IActionResult> GenerateCodesAsync(int count, CancellationToken ct = default)
    {
        var items = await _codeService.GenerateBatchAsync(count, ct);
        var response = new ListResponse<Code>(items.Count, Data: items);

        return Ok(response);
    }
}
