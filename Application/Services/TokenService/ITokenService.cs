using Domain.Users;

namespace Application.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
