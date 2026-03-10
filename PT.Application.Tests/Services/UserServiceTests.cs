using Moq;
using PT.Application.Interfaces.Repositories;
using PT.Application.Services;
using PT.Application.Validators.PhoneNumberValidators;
using PT.Domain.Models;

namespace PT.Application.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly PhoneNumberService _phone;

    public UserServiceTests()
    {
        var validator = new BelarusPhoneNumberValidator();
        var factory = new PhoneNumberValidatorFactory([validator]);

        _phone = new PhoneNumberService(factory);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenUserExists()
    {
        _repo.Setup(r => r.ExistsAsync("+375291234567", default))
             .ReturnsAsync(true);

        var service = new UserService(_repo.Object, _phone, _uow.Object);

        var result = await service.ExistsAsync("80291234567");

        Assert.True(result);
    }

    [Fact]
    public async Task GetByPhoneAsync_ReturnsNull_WhenNotFound()
    {
        _repo.Setup(r => r.GetByPhoneNumberAsync("+375291234567", default))
             .ReturnsAsync((User?)null);

        var service = new UserService(_repo.Object, _phone, _uow.Object);

        var result = await service.GetByPhoneAsync("80291234567");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByPhoneAsync_ReturnsUser_WhenExists()
    {
        var user = User.Create("+375291234567");

        _repo.Setup(r => r.GetByPhoneNumberAsync("+375291234567", default))
             .ReturnsAsync(user);

        var service = new UserService(_repo.Object, _phone, _uow.Object);

        var result = await service.GetByPhoneAsync("80291234567");

        Assert.Equal(user, result);
    }

    [Fact]
    public async Task RegisterAsync_CreatesUser_WhenNotExists()
    {
        _repo.Setup(r => r.ExistsAsync("+375291234567", default))
             .ReturnsAsync(false);

        var service = new UserService(_repo.Object, _phone, _uow.Object);

        var result = await service.RegisterAsync("80291234567");

        Assert.Equal("+375291234567", result.PhoneNumber);

        _repo.Verify(r => r.AddAsync(It.IsAny<User>(), default), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_Throws_WhenExists()
    {
        _repo.Setup(r => r.ExistsAsync("+375291234567", default))
             .ReturnsAsync(true);

        var service = new UserService(_repo.Object, _phone, _uow.Object);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(()
            => service.RegisterAsync("80291234567"));

        Assert.Equal("User '+375291234567' already exists", ex.Message);
    }

    [Fact]
    public async Task UpdateAsync_CallsRepoAndUow()
    {
        var user = User.Create("+375291234567");

        var service = new UserService(_repo.Object, _phone, _uow.Object);

        await service.UpdateAsync(user);

        _repo.Verify(r => r.Update(user), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }
}
