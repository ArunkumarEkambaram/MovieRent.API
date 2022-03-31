using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MovieRent.API.Data.Models;
using System.Net;

namespace MovieRent.API.Extensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureUseExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    logger.LogInformation("Error Log from Global Exception Handler");
                    var features = context.Features.Get<IExceptionHandlerFeature>();
                    
                    if (features != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = features.Error.Message //"Internal Server Error, using Global Error Handling"
                        }.ToString()) ;
                        logger.LogError(features.Error.Message);
                    }
                });
            });
        }
    }
}
