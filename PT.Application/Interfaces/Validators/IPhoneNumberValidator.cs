namespace PT.Application.Interfaces.Validators;

public interface IPhoneNumberValidator
{
    bool CanValidate(string phoneNumber);
    string NormalizeAndValidate(string phoneNumber);
    bool IsValid(string phone);
}
