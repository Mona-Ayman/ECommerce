using MediatR;

namespace Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
