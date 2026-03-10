using PT.Application.Validators.PhoneNumberValidators;

namespace PT.Application.Tests.Validators;

public class BelarusPhoneNumberValidatorTests
{
    private readonly BelarusPhoneNumberValidator _validator = new();

    [Theory]
    [InlineData("+375291234567")]
    [InlineData("375291234567")]
    [InlineData("80291234567")]
    public void NormalizeAndValidate_ValidNumbers_ReturnsNormalized(string input)
    {
        var result = _validator.NormalizeAndValidate(input);

        Assert.Equal("+375291234567", result);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("+123456")]
    public void NormalizeAndValidate_Invalid_Throws(string input)
    {
        Assert.Throws<InvalidOperationException>(() => _validator.NormalizeAndValidate(input));
    }

    [Theory]
    [InlineData("+375291234567", true)]
    [InlineData("80291234567", true)]
    [InlineData("12345", false)]
    public void CanValidate_Works(string input, bool expected)
    {
        var result = _validator.CanValidate(input);
        Assert.Equal(expected, result);
    }
}
