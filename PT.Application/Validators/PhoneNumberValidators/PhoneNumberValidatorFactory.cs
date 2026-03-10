using PT.Application.Interfaces.Validators;

namespace PT.Application.Validators.PhoneNumberValidators;

public sealed class PhoneNumberValidatorFactory(IEnumerable<IPhoneNumberValidator> validators)
{
    private readonly IEnumerable<IPhoneNumberValidator> _validators = validators;

    public IPhoneNumberValidator GetValidator(string phoneNumber)
    {
        foreach (var validator in _validators)
        {
            if (validator.CanValidate(phoneNumber))
            {
                return validator;
            }
        }

        throw new InvalidOperationException($"No validator found for phone number: '{phoneNumber}'");
    }
}
