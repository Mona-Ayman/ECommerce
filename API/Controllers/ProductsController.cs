using API.Controllers.Base;
using API.Helper;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Commands.UpdateState;
using Application.Features.Products.DTO;
using Application.Features.Products.Queries.GetAll;
using Application.Features.Products.Queries.GetById;
using Domain._Base.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductsController : ApiControllerBase
    {
        #region Fields

        private readonly ISender sender;

        #endregion

        #region Constructors

        public ProductsController(ISender sender)
        {
            this.sender = sender;
        }

        #endregion

        #region Methods

        [HttpGet()]
        public async Task<GlobalResponse<PaginatedModel<ProductOutput>>> GetAll(int? minPrice, int? maxPrice, string search, int pageNumber = 1, int pageSize = 1)
        {
            PaginatedModel<ProductOutput> products = await sender.Send(new GetAllProductsQuery(search, minPrice, maxPrice, pageSize, pageNumber));
            return ReturnResponse(products);
        }

        [HttpGet("Id")]
        public async Task<GlobalResponse<ProductOutput>> GetById(Guid id)
        {
            ProductOutput product = await sender.Send(new GetProductByIdQuery(id));
            return ReturnResponse(product);
        }

        [HttpPost]
        public async Task<GlobalResponse<string>> CreateProduct(CreateProductCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        [HttpPut]
        public async Task<GlobalResponse<string>> UpdateProduct(UpdateProductCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        [HttpDelete]
        public async Task<GlobalResponse<string>> DeleteProduct(Guid id)
        {
            await sender.Send(new DeleteProductCommand(id));
            return ReturnResponse(Localizations.Success);
        }

        [HttpPut("UpdateState")]
        public async Task<GlobalResponse<string>> UpdateState(UpdateStateCommand command)
        {
            await sender.Send(command);
            return ReturnResponse(Localizations.Success);
        }

        #endregion
    }
}
