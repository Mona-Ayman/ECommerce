using Microsoft.AspNetCore.Identity;

namespace Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lookoutOnFailure);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
    }
}
