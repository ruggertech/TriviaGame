using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace TriviaGame.Dtos.Response;

public class BadRequestExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public BadRequestExceptionFilter(IHostEnvironment hostEnvironment) =>
        _hostEnvironment = hostEnvironment;

    public void OnException(ExceptionContext context)
    {
        if (!_hostEnvironment.IsDevelopment())
        {
            // Don't display exception details unless running in Development.
            return;
        }

        context.Result = new ContentResult
        {
            Content = context.Exception.ToString(),
            ContentType = "application/json",
            StatusCode = 404
        };
    }
}