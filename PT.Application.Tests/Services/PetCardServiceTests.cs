using Moq;
using PT.Application.Dtos;
using PT.Application.Interfaces.Profiles;
using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Application.Profiles;
using PT.Application.Services;
using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Tests.Services;

public class PetCardServiceTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IPetCardRepository> _cards = new();
    private readonly Mock<ICodeService> _codes = new();
    private readonly Mock<IUserService> _users = new();
    private readonly Mock<IFileStorageService> _fileStorage = new();
    private readonly Mock<IImageProcessingProfileRegistry> _profiles = new();

    private readonly ImageProcessingService _imageProcessor;

    public PetCardServiceTests()
    {
        _profiles.Setup(p => p.Get(It.IsAny<MediaType>()))
                 .Returns(new AvatarProfile());

        _imageProcessor = new ImageProcessingService(_profiles.Object);
    }


    [Fact]
    public async Task GetAllByStateAsync_ReturnsFromRepo()
    {
        var list = new List<PetCard>();
        _cards.Setup(r => r.GetAllByStateAsync(CardState.Registered, default))
              .ReturnsAsync(list);

        var service = new PetCardService(_uow.Object, _cards.Object, _codes.Object, _imageProcessor, _fileStorage.Object, _users.Object);

        var result = await service.GetAllByStateAsync(CardState.Registered);

        Assert.Same(list, result);
    }

    [Fact]
    public async Task GetAllByUserAsync_ReturnsFromRepo()
    {
        var list = new List<PetCard>();
        _cards.Setup(r => r.GetAllByUserAsync(It.IsAny<Guid>(), default))
              .ReturnsAsync(list);

        var service = new PetCardService(_uow.Object, _cards.Object, _codes.Object, _imageProcessor, _fileStorage.Object, _users.Object);

        var result = await service.GetAllByUserAsync(Guid.NewGuid());

        Assert.Same(list, result);
    }

    [Fact]
    public async Task GetByCodeAsync_ReturnsFromRepo()
    {
        var card = PetCard.Register(User.Create("123"), Code.Generate("C"), "Barsik", "Brest", []);
        _cards.Setup(r => r.GetByCodeAsync("C", default))
              .ReturnsAsync(card);

        var service = new PetCardService(_uow.Object, _cards.Object, _codes.Object, _imageProcessor, _fileStorage.Object, _users.Object);

        var result = await service.GetByCodeAsync("C");

        Assert.Same(card, result);
    }

    [Fact]
    public async Task RegisterAsync_CreatesCard_AndCommits()
    {
        var dto = new RegisterPetCardDto
        (
            Code: "CODE1",
            PetName: "Barsik",
            Address: "Brest",
            PhoneNumber: "+375291234567",
            SocialLinks: []
        );

        var code = Code.Generate("CODE1");
        var user = User.Create("+375291234567");

        _codes.Setup(c => c.GetByValueAsync(dto.Code, default))
              .ReturnsAsync(code);
        _users.Setup(u => u.ExistsAsync(dto.PhoneNumber, default))
              .ReturnsAsync(false);
        _users.Setup(u => u.RegisterAsync(dto.PhoneNumber, default))
              .ReturnsAsync(user);

        var service = new PetCardService(_uow.Object, _cards.Object, _codes.Object, _imageProcessor, _fileStorage.Object, _users.Object);

        var result = await service.RegisterAsync(dto);

        Assert.Equal(dto.PetName, result.PetName);
        _cards.Verify(c => c.AddAsync(It.IsAny<PetCard>(), default), Times.Once);
        _uow.Verify(u => u.BeginTransactionAsync(default), Times.Once);
        _uow.Verify(u => u.CommitAsync(default), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_RollsBack_OnError()
    {
        var dto = new RegisterPetCardDto
        (
            Code: "CODE1",
            PetName: "Barsik",
            Address: "Brest",
            PhoneNumber: "+375291234567",
            SocialLinks: []
        );

        _codes.Setup(c => c.GetByValueAsync(dto.Code, default))
              .ThrowsAsync(new InvalidOperationException("fail"));

        var service = new PetCardService(_uow.Object, _cards.Object, _codes.Object, _imageProcessor, _fileStorage.Object, _users.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAsync(dto));

        _uow.Verify(u => u.BeginTransactionAsync(default), Times.Once);
        _uow.Verify(u => u.RollbackAsync(default), Times.Once);
    }
}
