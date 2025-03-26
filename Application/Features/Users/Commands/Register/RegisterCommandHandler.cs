using Domain.Users;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        #region Fields

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User userEmail = await userRepository.FindByEmailAsync(request.Email);
            if (userEmail != null) throw new ValidationException(Localizations.Exist);

            User userName = await userRepository.FindByNameAsync(request.UserName);
            if (userName != null) throw new ValidationException(Localizations.Exist);

            User user = new(request.UserName, request.Email, request.Phone, request.FullName, request.gender);

            IdentityResult createdUser = await userRepository.CreateAsync(user, request.Password);
            if (!createdUser.Succeeded) throw new ValidationException(Localizations.Error);
        }

        #endregion
    }
}
