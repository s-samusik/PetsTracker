using Microsoft.AspNetCore.Components;
using MudBlazor;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Pages.Registration.Forms;

public partial class OwnerForm
{
    [Parameter] public OwnerModel Model { get; set; }

    private void AddSocial()
    {
        Model.SocialNicks.Add($"Social {Model.SocialNicks.Count + 1}", "");
    }


}