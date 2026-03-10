namespace PT.Domain.Models;

public sealed class User : BaseEntity
{
    public string PhoneNumber { get; private set; }

    private User(string phoneNumber)
        => PhoneNumber = phoneNumber;

    public User(Guid id, string phone, DateTimeOffset createdAt, DateTimeOffset? updatedAt)
    {
        Id = id;
        PhoneNumber = phone;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static User Create(string phoneNumber)
        => new(phoneNumber);
}