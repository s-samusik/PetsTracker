using PT.Application.Interfaces.Validators;
using System.Text.RegularExpressions;

namespace PT.Application.Validators.PhoneNumberValidators;

public abstract class PhoneNumberValidatorBase : IPhoneNumberValidator
{
    protected static string ExtractDigits(string phone)
    {
        return Regex.Replace(phone, @"[^\d+]", "");
    }

    public abstract bool CanValidate(string phone);
    public abstract string NormalizeAndValidate(string phone);
}
