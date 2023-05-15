using Snippets.Entities;
using Snippets.Exceptions;
using System.Net;
using System.Text.Json;

namespace Snippets.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            CustomError customError = new CustomError();
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message = String.Empty;
            string trace = httpContext.TraceIdentifier;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                customError.Message = ex.Message;
                customError.Code = (int)HttpStatusCode.BadRequest;
                customError.Details = ex.StackTrace;
                customError.TraceIdentifier = trace;

            }
            else if (exceptionType == typeof(UnauthorizedException))
            {
                customError.Message = ex.Message;
                customError.Code = (int)HttpStatusCode.Unauthorized;
                customError.Details = ex.StackTrace;
                customError.TraceIdentifier = trace;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                customError.Message = ex.Message;
                customError.Code = (int)HttpStatusCode.NotFound;
                customError.Details = ex.StackTrace;
                customError.TraceIdentifier = trace;
            }           
            else
            {
                customError.Message = ex.Message;
                customError.Code = (int)HttpStatusCode.InternalServerError;
                customError.Details = ex.StackTrace;
                customError.TraceIdentifier = trace;
            }

            var exceptionResult = JsonSerializer.Serialize(customError);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = customError.Code;

            return httpContext.Response.WriteAsync(exceptionResult);
        }

    }
}
