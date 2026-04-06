using PT.Application.Regexes;

namespace PT.Application.Validators.PhoneNumberValidators;


public sealed class BelarusPhoneNumberValidator : PhoneNumberValidatorBase
{
    public override bool CanValidate(string phone)
    {
        var digits = ExtractDigits(phone);
        return digits.StartsWith("+375") || digits.StartsWith("375") || digits.StartsWith("80");
    }

    private static string Normalize(string phone)
    {
        var digits = ExtractDigits(phone);

        return digits switch
        {
            // 80XXXXXXXXX → +375XXXXXXXXX
            var d when d.StartsWith("80") => "+375" + d[2..],

            // 375XXXXXXXXX → +375XXXXXXXXX
            var d when d.StartsWith("375") => "+" + d,

            var d when d.StartsWith("+375") => d,

            _ => digits
        };
    }

    public override bool IsValid(string phone)
    {
        var normalized = Normalize(phone);
        
        return PhoneRegexes.BelarusMobileNumber.IsMatch(normalized);
    }

    public override string NormalizeAndValidate(string phone)
    {
        var normalized = Normalize(phone);

        return PhoneRegexes.BelarusMobileNumber.IsMatch(normalized)
            ? normalized
            : throw new InvalidOperationException($"Invalid Belarus mobile phone number: '{phone}'");
    }
}
