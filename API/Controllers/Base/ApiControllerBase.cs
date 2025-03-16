using API.Helper;
using Microsoft.AspNetCore.Mvc;
using Shared.Resources;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public static GlobalResponse<Result> ReturnResponse<Result>(Result data)
        {
            return new GlobalResponse<Result>()
            {
                Message = Localizations.Success,
                Data = data
            };
        }
    }
}
