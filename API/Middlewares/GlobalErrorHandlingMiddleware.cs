using API.Helper;
using Shared.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        #region Constructors
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        #endregion

        #region Fields
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> logger;
        #endregion

        #region Methods
        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await next(httpContent);
            }
            catch (Exception exception)
            {
                logger.LogError($"Something Went Wrong: {exception}");
                await HandleExceptionAsync(httpContent, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContent, Exception exception)
        {
            HttpResponse response = httpContent.Response;
            response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            GlobalResponse<string> globalResponse = new()
            {
                Message = exception.Message,
            };

            string jsonResponse = JsonSerializer.Serialize(globalResponse);
            await httpContent.Response.WriteAsync(jsonResponse);
        }
        #endregion
    }

}
