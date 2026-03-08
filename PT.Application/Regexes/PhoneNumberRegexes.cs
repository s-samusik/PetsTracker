using System.Text.RegularExpressions;

namespace PT.Application.Regexes;

public static class PhoneRegexes
{
    public static readonly Regex BelarusMobileNumber = new(@"^\+375(25|29|33|44)\d{7}$", RegexOptions.Compiled);
}

