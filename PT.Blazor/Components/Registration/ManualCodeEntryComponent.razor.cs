using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PT.Blazor.Components.Registration;

public partial class ManualCodeEntryComponent
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;

    public PatternMask QrCodeMask = new("AA-11-AAA")
    {
        MaskChars = [new MaskChar('A', @"[a-zA-Z]"), new MaskChar('1', @"[1-9]")],
        Placeholder = ' ',
        CleanDelimiters = true,
        Transformation = AllUpperCase
    };

    private string manualCode = null!;

    private void GoToRegistration()
        => Nav.NavigateTo($"/card/{manualCode}", forceLoad: true);

    private void GoHome()
        => Nav.NavigateTo("/", forceLoad: true);

    private static char AllUpperCase(char c) 
        => c.ToString().ToUpperInvariant()[0];
}