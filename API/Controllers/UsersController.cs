using API.Controllers.Base;
using API.Helper;
using Application.Features.Users.Commands.ChangePassword;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Output;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources;

namespace API.Controllers
{
    public class UsersController : ApiControllerBase
    {
        #region Fields

        private readonly ISender sender;

        #endregion

        #region Constructors

        public UsersController(ISender sender)
        {
            this.sender = sender;
        }

        #endregion

        #region Methods

        [HttpPost("Register")]
        public async Task<GlobalResponse<string>> Register(RegisterCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        [HttpPost("Login")]
        public async Task<GlobalResponse<LoginOutput>> Login(LoginCommand command)
        {
            LoginOutput result = await sender.Send(command);
            return ReturnResponse(result);
        }

        [HttpPut]
        public async Task<GlobalResponse<string>> UpdateUser(UpdateCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        [HttpPut("ChangePassword")]
        public async Task<GlobalResponse<string>> UpdateUser(ChangePasswordCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        #endregion
    }
}
