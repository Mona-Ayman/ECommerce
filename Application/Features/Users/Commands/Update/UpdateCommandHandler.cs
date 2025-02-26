using Domain.Users;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        #region Fields

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors

        public UpdateCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods
        public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.FindByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException(Localizations.NotFound);

            user.Update(request.Phone, request.FullName, request.gender);
            IdentityResult result = await userRepository.UpdateAsync(user);
            if (!result.Succeeded) throw new ValidationException(Localizations.Error);
        }

        #endregion
    }
}
