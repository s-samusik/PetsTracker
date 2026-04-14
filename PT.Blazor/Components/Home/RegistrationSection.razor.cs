using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Home;

public partial class RegistrationSection
{
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private string Code = null!;

    private void GoToRegistration()
        => Nav.NavigateTo($"/card/{FormattedCode(Code)}", forceLoad: true);

    private static string FormattedCode(string code)
    => code.Replace(" ", string.Empty).ToUpper();
}