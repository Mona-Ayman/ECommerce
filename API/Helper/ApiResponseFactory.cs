using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace API.Helper
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            List<string> errors = context.ModelState.Where(c => c.Value.Errors.Any())
                .SelectMany(c => c.Value.Errors).Select(e => e.ErrorMessage).ToList();

            throw new ValidationErrorResponse()
            {
                Errors = errors,
            };
        }
    }
}
