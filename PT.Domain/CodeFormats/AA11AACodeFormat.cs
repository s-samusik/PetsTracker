namespace PT.Domain.CodeFormats;

public sealed class AA11AACodeFormat : ICodeFormat
{
    private static readonly char[] letters = "ABCDEFGHJKMNPQRSTUVWXYZ".ToCharArray();
    private static readonly char[] digits = "23456789".ToCharArray();
    private static readonly Random rnd = new();

    public string GenerateBase()
    {
        Span<char> code =
        [
            letters[rnd.Next(letters.Length)],
            letters[rnd.Next(letters.Length)],
            digits[rnd.Next(digits.Length)],
            digits[rnd.Next(digits.Length)],
            letters[rnd.Next(letters.Length)],
            letters[rnd.Next(letters.Length)],
        ];

        return new string(code);
    }

    public char ComputeChecksum(string baseCode)
    {
        var sum = 0;

        foreach (var ch in baseCode)
        {
            int idx = char.IsDigit(ch) ? Array.IndexOf(digits, ch) : Array.IndexOf(letters, ch);

            sum ^= idx;
        }

        return letters[sum % letters.Length];
    }

    public bool ValidateFormat(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != 7)
        {
            return false;
        }

        return IsLetter(code[0]) &&
               IsLetter(code[1]) &&
               IsDigit(code[2]) &&
               IsDigit(code[3]) &&
               IsLetter(code[4]) &&
               IsLetter(code[5]) &&
               IsLetter(code[6]);
    }

    public bool ValidateChecksum(string code) => ComputeChecksum(code[..6]) == code[6];

    private static bool IsLetter(char c) => Array.IndexOf(letters, c) >= 0;

    private static bool IsDigit(char c) => Array.IndexOf(digits, c) >= 0;
}

