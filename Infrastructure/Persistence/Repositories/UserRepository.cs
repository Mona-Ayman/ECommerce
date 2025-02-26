using Domain.Users;
using Domain.Users.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Fields

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        #endregion

        #region Constructors

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #endregion

        #region Methods

        public async Task<User> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lookoutOnFailure)
        {
            return await signInManager.CheckPasswordSignInAsync(user, password, lookoutOnFailure);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        #endregion
    }
}
