using Moq;
using PT.Application.Interfaces.Repositories;
using PT.Application.Services;
using PT.Domain.CodeFormats;
using PT.Domain.Enums;

namespace PT.Application.Tests.Services;

public class CodeServiceTests
{
    private readonly Mock<ICodeFormat> _format = new();
    private readonly Mock<ICodeRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();

    public CodeServiceTests()
    {
        _format.Setup(f => f.GenerateBase()).Returns("ABC123");
        _format.Setup(f => f.ComputeChecksum("ABC123")).Returns('Z');
        _format.Setup(f => f.ValidateFormat(It.IsAny<string>())).Returns(true);
        _format.Setup(f => f.ValidateChecksum(It.IsAny<string>())).Returns(true);
    }

    [Fact]
    public void Generate_ReturnsBasePlusChecksum()
    {
        var service = new CodeService(_format.Object, _repo.Object, _uow.Object);

        var result = service.Generate();

        Assert.Equal("ABC123Z", result);
    }

    [Fact]
    public void Validate_UsesFormatAndChecksum()
    {
        var service = new CodeService(_format.Object, _repo.Object, _uow.Object);

        var result = service.Validate("ANY");

        Assert.True(result);
    }

    [Fact]
    public async Task GetAsync_ReturnsNull_WhenNotFound()
    {
        _repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync((Code?)null);

        var service = new CodeService(_format.Object, _repo.Object, _uow.Object);

        var result = await service.GetAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByValueAsync_ReturnsNull_WhenNotFound()
    {
        _repo.Setup(r => r.GetByValueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync((Code?)null);

        var service = new CodeService(_format.Object, _repo.Object, _uow.Object);

        var result = await service.GetByValueAsync("NOT_EXIST");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllByStateAsync_ReturnsFromRepo()
    {
        var codes = new List<Code> { Code.Generate("ABC") };

        _repo.Setup(r => r.GetAllByStateAsync(CodeState.Generated, default))
             .ReturnsAsync(codes);

        var service = new CodeService(_format.Object, _repo.Object, _uow.Object);

        var result = await service.GetAllByStateAsync(CodeState.Generated);

        Assert.Single(result);
    }

    [Fact]
    public async Task UpdateAsync_CallsRepoAndUow()
    {
        var code = Code.Generate("ABC");

        var service = new CodeService(_format.Object, _repo.Object, _uow.Object);

        await service.UpdateAsync(code);

        _repo.Verify(r => r.Update(code), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }
}
