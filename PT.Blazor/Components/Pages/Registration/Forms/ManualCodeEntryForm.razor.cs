using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Pages.Registration.Forms;

public partial class ManualCodeEntryForm
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    private string manualCode;

    void GoToRegistration()
    {
        Nav.NavigateTo($"/{Code}");
    }

    void GoHome()
    {
        Nav.NavigateTo("/");
    }
}