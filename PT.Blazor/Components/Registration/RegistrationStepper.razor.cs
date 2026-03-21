using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Registration;

public partial class RegistrationStepper : ComponentBase
{
    [Parameter] public EventCallback OnFinish { get; set; }

    private int index;
    private bool completed;

    private async Task OnRegisterClick()
    {
        completed = true;

        await OnFinish.InvokeAsync();
    }
}