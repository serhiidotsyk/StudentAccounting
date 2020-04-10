using BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorDetails errorDetails = new ErrorDetails
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Internal Server Error"
            };
            if(exception is NotFoundException)
            {
                errorDetails.StatusCode = HttpStatusCode.NotFound;
                errorDetails.Message = exception.Message;
            }
            else if (exception is DBConcurrencyException)
            {
                errorDetails.StatusCode = HttpStatusCode.Conflict;
                errorDetails.Message = "Can't update entity, because it has already changed in db.";
            }
            else if (exception is UnauthorizedAccessException)
            {
                errorDetails.StatusCode = HttpStatusCode.Unauthorized;
                errorDetails.Message = "Can't identify user";
            }
            context.Response.StatusCode = (int)errorDetails.StatusCode;
            context.Response.WriteAsync(errorDetails.Message);

            return Task.FromResult(context);
        }

        private class ErrorDetails
        {
            public HttpStatusCode StatusCode { get; set; }
            public string Message { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
