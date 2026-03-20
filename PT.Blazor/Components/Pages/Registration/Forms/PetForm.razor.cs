using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Pages.Registration.Forms;

public partial class PetForm
{
    [Parameter] public PetModel Model { get; set; }


    private bool isLoadingPhoto = false;

    private readonly string[] DefaultAvatars =
    [
        "/images/test2.jpg",
    ];

    private void SelectAvatar(string url)
    {
        Model.PhotoUrl = url;
    }
}