using API.Controllers.Base;
using API.Helper;
using Application.Features.Carts.Commands.Create;
using Application.Features.Carts.Commands.Delete;
using Application.Features.Carts.Outputs;
using Application.Features.Carts.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CartController : ApiControllerBase
    {
        #region Fields

        private readonly ISender sender;

        #endregion

        #region Constructors

        public CartController(ISender sender)
        {
            this.sender = sender;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<GlobalResponse<string>> Add(AddItemCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        [HttpGet]
        public async Task<GlobalResponse<CartOutput>> Get()
        {
            CartOutput cartOutput = await sender.Send(new GetUserCartCommand());
            return ReturnResponse(cartOutput);
        }

        [HttpDelete]
        public async Task<GlobalResponse<string>> Delete(RemoveItemCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        #endregion
    }
}
