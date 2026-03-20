using Microsoft.AspNetCore.Components;
using PT.Application.Interfaces.Services;
using PT.Domain.Enums;

namespace PT.Blazor.Components.Pages.Registration;

public partial class PetCardRegistration : ComponentBase
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private ICodeService codeService { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private int index;
    private bool completed;
    private bool codeNotFound;

    protected override async Task OnInitializedAsync()
    {
        var entity = await TryGetEntityAsync();

        if (entity is null)
        {
            codeNotFound = true;
            return;
        }

        if (entity.State == CodeState.Activated)
            Nav.NavigateTo("/");
    }

    private async Task<Code?> TryGetEntityAsync()
    {
        if (!codeService.Validate(Code))
            return null;

        return await codeService.GetByValueAsync(Code);
    }
}