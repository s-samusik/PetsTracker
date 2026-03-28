using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace PT.Blazor.Components.Registration;

public partial class PrivacyPolicyComponent
{
    [Parameter] public EventCallback OnAccept { get; set; }

    [Inject] private NavigationManager Nav { get; set; } = default!;

    void GoToStart()
    {
        OnAccept.InvokeAsync();
    }

    void GoHome()
    {
        Nav.NavigateTo("/", forceLoad: true);
    }

    private string text = "Privacy policy text";
}