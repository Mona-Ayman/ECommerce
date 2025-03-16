using API.Controllers.Base;
using API.Helper;
using Application.Features.Rates.Commands.Create;
using Application.Features.Rates.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductRatesController : ApiControllerBase
    {
        #region Fields

        private readonly ISender sender;

        #endregion

        #region Constructors

        public ProductRatesController(ISender sender)
        {
            this.sender = sender;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<GlobalResponse<string>> Add(CreateProductRateCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        [HttpPut]
        public async Task<GlobalResponse<string>> Update(UpdateProductRateCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        #endregion
    }
}
