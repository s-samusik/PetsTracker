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
        if (id == Guid.Empty)
        {
            return BadRequest("Id is required");
        }

        var response = await _codeService.GetAsync(id, ct);

        return response is null
            ? NotFound($"Code by Id '{id}' not found")
            : Ok(response);
    }

    [HttpGet("{code:maxlength(7)}")]
    public async Task<IActionResult> GetByValueAsync(string code, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return BadRequest("Code is required");
        }

        var response = await _codeService.GetByValueAsync(code, ct);

        return response is null
            ? NotFound($"Code '{code}' not found")
            : Ok(response);
    }

    [HttpGet("state")]
    public async Task<IActionResult> GetAllByStateAsync([FromQuery] CodeState state, CancellationToken ct = default)
    {
        if (!Enum.IsDefined(typeof(CodeState), state))
        {
            return BadRequest($"Code state '{state}' is invalid");
        }

        var items = await _codeService.GetAllByStateAsync(state, ct);

        var response = new ListResponse<Code>(items.Count, Data: items);

        return Ok(response);
    }

    [HttpGet("generate/{count:int}")]
    public async Task<IActionResult> GenerateCodesAsync(int count, CancellationToken ct = default)
    {
        if(count < 1 || count > 1000)
        {
            return BadRequest($"Count '{count}' is invalid, it should be between 1 and 1000");
        }

        var items = await _codeService.GenerateBatchAsync(count, ct);

        var response = new ListResponse<Code>(items.Count, Data: items);

        return Ok(response);
    }
}
