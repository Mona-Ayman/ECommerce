using Domain.Users;

namespace Application.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
