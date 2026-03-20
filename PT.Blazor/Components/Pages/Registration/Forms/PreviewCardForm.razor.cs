using Microsoft.AspNetCore.Components;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Pages.Registration.Forms;

public partial class PreviewCardForm
{
    [Parameter] public OwnerModel Owner { get; set; }
    [Parameter] public PetModel Pet { get; set; }
    [Parameter] public ManualEntryModel Manual { get; set; }

    [Parameter] public bool IsLoading { get; set; }
}