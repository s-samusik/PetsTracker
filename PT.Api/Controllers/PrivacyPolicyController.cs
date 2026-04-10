using Microsoft.AspNetCore.Mvc;
using PT.Api.Requests.PrivacyPolicy;
using PT.Application.Interfaces.Services;
using PT.Domain.Enums;

namespace PT.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PrivacyPolicyController(IPrivacyPolicyService privacyPolicyService) : ControllerBase
{
    private readonly IPrivacyPolicyService _privacyPolicyService = privacyPolicyService;

    [HttpGet("user-type")]
    public async Task<IActionResult> GetLatestByUserTypeAsync([FromQuery] UserType userType , CancellationToken ct = default)
    {
        var response = await _privacyPolicyService.GetLatestByUserTypeAsync(userType, ct);

        return Ok(response);
    }

    [HttpPost()]
    public async Task<IActionResult> AddByUserTypeAsync(AddPrivacyPolicyRequest request, CancellationToken ct = default)
    {
        var response = await _privacyPolicyService.AddByUserTypeAsync(request.Text, request.UserType, ct);
        
        return Created(string.Empty, response);
    }
}
