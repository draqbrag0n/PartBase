namespace PartBase.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    public bool Revoked { get; set; }

    public string UserId { get; set; } = string.Empty;
}