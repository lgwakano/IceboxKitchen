using System.Diagnostics;
using ErrorOr;
using IceboxKitchen.Api.Common.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace IceboxKitchen.Api.Common.Errors;

public class IceboxKitchenProblemDetailsFactory(IOptions<ApiBehaviorOptions> options) : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options = options.Value;
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title ?? "An error occurred while processing your request.",
            Type = type,
            Detail = detail,
            Instance = instance
        };
        
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 400;

        var validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Title = title ?? "One or more validation errors occurred.",
            Type = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, validationProblemDetails, statusCode.Value);

        return validationProblemDetails;
    }


    public void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if(_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId is not null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        problemDetails.Extensions.Add("timestamp", DateTime.UtcNow);

        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
        if (errors is not null && errors.Any())
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }
    }
}