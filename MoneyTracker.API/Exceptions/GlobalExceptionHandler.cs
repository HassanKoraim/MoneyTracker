using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MoneyTracker.API.Exceptions
{
    public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails problem = exception switch
            {
                ValidationException ex => new ValidationProblemDetails(
                                    ex.Errors.GroupBy(g => g.PropertyName)
                                             .ToDictionary(g => g.Key,
                                                           g => g.Select(e => e.ErrorMessage).ToArray()
                                                           )
                                             )
                {
                    Title = "Validation Error",
                //    Detail = "One or more validation errors occurred while processing the request.",
                    Status = StatusCodes.Status400BadRequest
                },
                KeyNotFoundException notFound => new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = notFound.Message,
                    Status = StatusCodes.Status400BadRequest
                },
                ArgumentNullException argumentNullException => new ProblemDetails
                {
                    Title = "Argument Error",
                    Detail = argumentNullException.Message,
                    Status = StatusCodes.Status400BadRequest
                },
                ArgumentException ex => new ProblemDetails
                {
                    Title = "Argument Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                },
                _ => new ProblemDetails
                {
                    Title = "Server Error",
                    Detail = "An Handled exception occurred while processing the request.",
                    Status = StatusCodes.Status500InternalServerError
                }

            };
            httpContext.Response.StatusCode = problem.Status!.Value;
            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            });
            return true;
        }
    }
}
