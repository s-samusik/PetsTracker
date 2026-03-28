using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Registration;

public partial class CongratulationsComponent
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    void GoToCard()
    {
        Nav.NavigateTo($"/card/{Code}", forceLoad: true);
    }

    void GoHome()
    {
        Nav.NavigateTo("/", forceLoad: true);
    }
}