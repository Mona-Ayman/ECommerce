using API.Controllers.Base;
using API.Helper;
using Application.Features.Rates.Commands.Create;
using Application.Features.Rates.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources;

namespace API.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
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

        //public async Task<GlobalResponse<string>> Test()
        //{
        //    using var context = new ECommerceContext();
        //    Guid id = Guid.Parse("17229812-3fbc-4807-b891-0d8db44dc350");
        //    var product = await context.Set<Product>().FindAsync(id);
        //    product.TotalCountOfUserRates += 1;
        //    //context.Set<Product>().Update(product);
        //    await context.SaveChangesAsync();
        //    return ReturnResponse(Localizations.Success);
        //}

        //public async Task<GlobalResponse<string>> Test()
        //{
        //    try
        //    {
        //        using var context = new ECommerceContext();

        //        Guid id = Guid.Parse("17229812-3fbc-4807-b891-0d8db44dc350"); // Ensure correct GUID format

        //        bool saveFailed;
        //        do
        //        {
        //            saveFailed = false;

        //            var product = await context.Set<Product>().FindAsync(id);
        //            if (product == null)
        //            {
        //                return ReturnResponse<string>("Product not found");
        //            }

        //            try
        //            {
        //                product.TotalCountOfUserRates += 1;
        //                await context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException ex)
        //            {
        //                saveFailed = true;
        //                var entry = ex.Entries.Single();
        //                var databaseValues = await entry.GetDatabaseValuesAsync();

        //                if (databaseValues == null)
        //                {
        //                    return ReturnResponse<string>("Product no longer exists");
        //                }

        //                var dbProduct = (Product)databaseValues.ToObject();

        //                // Apply latest database version, but keep the updated count
        //                product.RowVersion = dbProduct.RowVersion;
        //                product.TotalCountOfUserRates = dbProduct.TotalCountOfUserRates + 1;

        //                entry.OriginalValues.SetValues(databaseValues);
        //            }
        //        } while (saveFailed);

        //        return ReturnResponse(Localizations.Success);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ReturnResponse<string>($"An error occurred: {ex.Message}");
        //    }
        //}


        #endregion
    }
}
