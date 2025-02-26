using Domain.Users.Enums;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public Gender gender { get; set; }
    }
}
