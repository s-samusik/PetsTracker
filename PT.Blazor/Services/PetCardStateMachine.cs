using PT.Blazor.Enums;

namespace PT.Blazor.Services;

public sealed class PetCardStateMachine
{
    public PetCardState Current { get; private set; } = PetCardState.Start;

    public bool CanRegister => Current == PetCardState.PrivacyPolicy;

    public void Reset()
    {
        Current = PetCardState.Start;
    }

    public void SetCodeNotFound()
    {
        Current = PetCardState.CodeNotFound;
    }

    public void SetActive()
    {
        Current = PetCardState.Active;
    }

    public void SetStart()
    {
        Current = PetCardState.PrivacyPolicy;
    }

    public void SetRegistering()
    {
        if (CanRegister)
        {
            Current = PetCardState.Registering;
        }
    }

    public void SetRegistered()
    {
        Current = PetCardState.Registered;
    }

    public void SetLoading()
    {
        Current = PetCardState.Loading;
    }

}
