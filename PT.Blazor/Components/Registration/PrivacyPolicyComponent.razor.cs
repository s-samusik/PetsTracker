using Microsoft.AspNetCore.Components;
using PT.Application.Interfaces.Services;
using PT.Domain.Enums;
using System.Reflection.Metadata;

namespace PT.Blazor.Components.Registration;

public partial class PrivacyPolicyComponent
{
    [Parameter] public EventCallback OnAccept { get; set; }

    [Inject] private NavigationManager Nav { get; set; } = default!;
    [Inject] private IPrivacyPolicyService privacyPolicyService { get; set; } = default!;

    private string privacyPolicy = string.Empty;
    private bool loading;

    protected override async Task OnInitializedAsync()
    {
        loading = true;

        var result = await privacyPolicyService.GetPrivacyPolicyAsync(UserType.Individual);

        privacyPolicy = result?.Value ?? string.Empty;

        loading = false;
    }

    void GoToStart()
    {
        OnAccept.InvokeAsync();
    }

    void GoHome()
    {
        Nav.NavigateTo("/", forceLoad: true);
    }
}