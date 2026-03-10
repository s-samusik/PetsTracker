using Moq;
using PT.Application.Interfaces.Validators;
using PT.Application.Services;
using PT.Application.Validators.PhoneNumberValidators;

namespace PT.Application.Tests.Services;

public class PhoneNumberServiceTests
{
    [Fact]
    public void NormalizeAndValidate_UsesCorrectValidator()
    {
        var validatorMock = new Mock<IPhoneNumberValidator>();
        validatorMock.Setup(v => v.CanValidate(It.IsAny<string>())).Returns(true);
        validatorMock.Setup(v => v.NormalizeAndValidate("raw"))
                     .Returns("normalized");

        var factory = new PhoneNumberValidatorFactory([validatorMock.Object]);
        var service = new PhoneNumberService(factory);

        var result = service.NormalizeAndValidate("raw");

        Assert.Equal("normalized", result);
        validatorMock.Verify(v => v.NormalizeAndValidate("raw"), Times.Once);
    }

    [Fact]
    public void NormalizeAndValidate_Throws_WhenNoValidator()
    {
        var factory = new PhoneNumberValidatorFactory([]);
        var service = new PhoneNumberService(factory);

        Assert.Throws<InvalidOperationException>(() => service.NormalizeAndValidate("raw"));
    }
}
