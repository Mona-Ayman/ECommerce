using Application.Features.Users.Output;
using MediatR;

namespace Application.Features.Users.Commands.Login
{
    public class LoginCommand : IRequest<LoginOutput>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
