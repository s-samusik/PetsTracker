using Microsoft.AspNetCore.Components;

namespace PT.Blazor.Components.Pages.Registration;

public partial class PetCardRegistration
{
    [Parameter] public string Code { get; set; } = default!;

    private int _index;
    private bool _completed;
}