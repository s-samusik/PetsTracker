using Microsoft.AspNetCore.Mvc;
using PT.Api.Requests.User;
using PT.Application.Interfaces.Services;

namespace PT.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;


    [HttpGet("{number:maxlength(20)}")]
    public async Task<IActionResult> GetByPhoneNumberAsync(string number, CancellationToken ct = default)
    {
        var response = await _userService.GetByPhoneAsync(number, ct);
        
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(UserRegisterRequest request, CancellationToken ct = default)
    {
        var response = await _userService.RegisterAsync(request.PhoneNumber, ct);

        return Created(string.Empty, response);
    }
}
