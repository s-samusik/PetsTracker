using Microsoft.AspNetCore.Components;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Registration;

public partial class RegistrationStepper : ComponentBase
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;
    [Parameter] public EventCallback OnFinish { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    private int index;
    private bool completed;

    private async Task OnRegisterClick()
    {
        completed = true;
        await OnFinish.InvokeAsync();
    }

    void GoToStart()
    {
        Nav.NavigateTo($"/card/{PetCardModel.Code}", forceLoad: true);
    }
}