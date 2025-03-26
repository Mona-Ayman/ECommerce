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
        }
    }
}
