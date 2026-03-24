using Microsoft.AspNetCore.Components;
using PT.Application.Interfaces.Services;
using PT.Blazor.Models;
using PT.Blazor.Services;
using PT.Domain.Enums;

namespace PT.Blazor.Pages;

public partial class PetCardPage : ComponentBase
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private ICodeService CodeService { get; set; } = default!;
    [Inject] private PetCardStateMachine State { get; set; } = default!;
    [Inject] private NavigationManager Nav {  get; set; } = default!;

    private PetCardModel PetCardModel = default!;

    protected override void OnInitialized()
    {
        PetCardModel = new PetCardModel { Code = Code };
    }

    protected override async Task OnParametersSetAsync()
    {
        State.SetLoading();
        
        var entity = await TryGetEntityAsync();

        if (entity is null)
        {
            State.SetCodeNotFound();
            return;
        }

        if (entity.State == CodeState.Activated)
        {
            State.SetActive();
            return;
        }

        State.Reset();
    }

    private async Task<Code?> TryGetEntityAsync()
    {
        if (!CodeService.Validate(Code))
            return null;

        return await CodeService.GetByValueAsync(Code);
    }

    private void HandleStartRegistration()
    {
        State.SetRegistering();
    }

    private void HandleFinishRegistration()
    {
        State.SetRegistered();
    }
}
