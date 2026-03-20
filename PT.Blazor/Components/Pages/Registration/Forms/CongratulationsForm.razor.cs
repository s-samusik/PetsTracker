using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Pages.Registration.Forms;

public partial class CongratulationsForm
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    void GoToCard()
    {
        Nav.NavigateTo($"/{Code}");
    }

    void GoHome()
    {
        Nav.NavigateTo("/");
    }
}