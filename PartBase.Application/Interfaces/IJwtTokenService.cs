namespace PartBase.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateToken(
        string userId,
        string email,
        string fullName,
        IList<string> roles);
}