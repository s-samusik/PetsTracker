using Microsoft.AspNetCore.Components;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Registration;

public partial class PreviewComponent
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;
}