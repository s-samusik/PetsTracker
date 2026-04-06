using PT.Application.Interfaces.Validators;
using PT.Application.Validators.PhoneNumberValidators;

namespace PT.Application.Services;

public sealed class PhoneNumberService(PhoneNumberValidatorFactory factory)
{
    private IPhoneNumberValidator Get(string phone) => factory.GetValidator(phone);


    public string NormalizeAndValidate(string phone) => Get(phone).NormalizeAndValidate(phone);

    public bool IsValid(string phone) => Get(phone).IsValid(phone);
}

