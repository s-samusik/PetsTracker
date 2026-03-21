using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Registration;

public partial class ManualCodeEntryComponent
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    private string manualCode = null!;

    void GoToRegistration()
    {
        Nav.NavigateTo($"/card/{manualCode}", forceLoad: true);
    }

    void GoHome()
    {
        Nav.NavigateTo("/", forceLoad: true);
    }
}