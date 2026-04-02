using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace PT.Blazor.Components.Registration;

public partial class ManualCodeEntryComponent
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    private string manualCode = null!;

    private void GoToRegistration()
        => Nav.NavigateTo($"/card/{manualCode.ToUpper()}", forceLoad: true);

    private void GoHome()
        => Nav.NavigateTo("/", forceLoad: true);
}