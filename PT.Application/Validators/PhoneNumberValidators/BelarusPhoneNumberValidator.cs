using PT.Application.Regexes;

namespace PT.Application.Validators.PhoneNumberValidators;


public sealed class BelarusPhoneNumberValidator : PhoneNumberValidatorBase
{
    public override bool CanValidate(string phone)
    {
        var digits = ExtractDigits(phone);
        return digits.StartsWith("+375") || digits.StartsWith("375") || digits.StartsWith("80");
    }

    public override string NormalizeAndValidate(string phone)
    {
        var digits = ExtractDigits(phone);

        if (digits.StartsWith("80"))
        {
            digits = "+375" + digits[2..];
        }

        if (digits.StartsWith("375"))
        {
            digits = "+" + digits;
        }

        return PhoneRegexes.BelarusMobileNumber.IsMatch(digits)
            ? digits
            : throw new InvalidOperationException($"Invalid Belarus mobile phone number: '{phone}'");
    }
}
