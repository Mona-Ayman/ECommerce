using Domain.Users.Enums;
using MediatR;

namespace Application.Features.Users.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Gender gender { get; set; }
    }
}
