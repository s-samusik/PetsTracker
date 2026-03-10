using Microsoft.AspNetCore.Mvc;
using PT.Api.Requests.PetCard;
using PT.Api.Responses.PetCard;
using PT.Application.Dtos;
using PT.Application.Interfaces.Services;

namespace PT.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PetCardController(IPetCardService petCardService) : ControllerBase
{
    private readonly IPetCardService _petCardService = petCardService;


    [HttpGet("{code:maxlength(7)}")]
    public async Task<IActionResult> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        var card = await _petCardService.GetByCodeAsync(code, ct);

        var response = new PetCardResponse
        (
            card.Id,
            code,
            card.PetName,
            card.PhotoUrl,
            card.State,
            [.. card.SocialLinks.Select(x => new SocialLinkResponse(x.Type, x.Username))]
        );

        return Ok(response);
    }


    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(PetCardRegisterRequest request, CancellationToken ct = default)
    {
        var dto = new RegisterPetCardDto
            (request.Code, request.PhoneNumber, request.PetName, request.SocialLinks);

        var card = await _petCardService.RegisterAsync(dto, ct);

        var response = new PetCardResponse
        (
            card.Id,
            request.Code,
            card.PetName,
            card.PhotoUrl,
            card.State,
            [.. card.SocialLinks.Select(x => new SocialLinkResponse(x.Type, x.Username))]
        );

        return Created(string.Empty, response);
    }
}
