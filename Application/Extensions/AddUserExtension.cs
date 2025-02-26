using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Shared.Resources;
using System.Security.Claims;

namespace Application.Extensions
{
    public static class AddUserExtension
    {
        public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal user = httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NotFoundException(Localizations.NotFound);

            //return userId;

            //if (httpContextAccessor?.HttpContext?.User == null)
            //{
            //    return 0; // or throw a meaningful exception
            //}

            //var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            //if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            //{
            //    return 1; // or throw new Exception("User ID claim is missing");
            //}

            //if (int.TryParse(userIdClaim.Value, out int userId))
            //{
            //    return userId;
            //}

            //return 2;
        }
    }
}
