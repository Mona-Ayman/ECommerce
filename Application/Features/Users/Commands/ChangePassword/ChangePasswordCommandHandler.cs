using Domain.Users;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        #region Fields

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.FindByEmailAsync(request.Email) ?? throw new NotFoundException(Localizations.NotFound);

            IdentityResult result = await userRepository.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded) throw new ValidationException(Localizations.Error);
        }

        #endregion
    }
}
