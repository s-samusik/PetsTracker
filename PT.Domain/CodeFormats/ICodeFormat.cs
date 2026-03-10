namespace PT.Domain.CodeFormats;

public interface ICodeFormat
{
    string GenerateBase();
    char ComputeChecksum(string baseCode);
    bool ValidateFormat(string code);
    bool ValidateChecksum(string code);
}