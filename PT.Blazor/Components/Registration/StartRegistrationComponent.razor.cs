using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Registration;

public partial class StartRegistrationComponent
{
    [Parameter] public EventCallback OnStart { get; set; }
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    void GoToRegister()
    {
        OnStart.InvokeAsync();
    }

    void GoHome()
    {
        Nav.NavigateTo("/", forceLoad: true);
    }
}