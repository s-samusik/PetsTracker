using PT.Application.Validators.PhoneNumberValidators;

namespace PT.Application.Services;

public sealed class PhoneNumberService(PhoneNumberValidatorFactory factory)
{
    private readonly PhoneNumberValidatorFactory _factory = factory;

    public string NormalizeAndValidate(string phoneNumber)
    {
        var validator = _factory.GetValidator(phoneNumber);
        
        return validator.NormalizeAndValidate(phoneNumber);
    }
}

