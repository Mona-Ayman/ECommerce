using Application.Features.Users.Output;
using Application.Services.TokenService;
using Domain.Users;
using Domain.Users.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginOutput>
    {
        #region Fields

        private readonly ITokenService tokenService;
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors

        public LoginCommandHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            this.tokenService = tokenService;
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods

        public async Task<LoginOutput> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.FindByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException(Localizations.NotFound);

            SignInResult result = await userRepository.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new ValidationException(Localizations.Error);

            string token = tokenService.CreateToken(user);

            return new LoginOutput()
            {
                Token = token
            };
        }

        #endregion
    }
}
