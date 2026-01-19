using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MetricService.WebAPI.Controllers.Common;

public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        var firstError = errors.First();

        return Problem(errors, firstError);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach(var error in errors)
            modelStateDictionary.AddModelError(error.Code, error.Description);

        return ValidationProblem(modelStateDictionary);
    }

    private IActionResult Problem(List<Error> errors, Error firstError)
    {
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status404NotFound
        };

        return Problem(
            statusCode: statusCode,
            detail: firstError.Description,
            extensions: new Dictionary<string, object?>
            {
                {"errors", errors.Select(e => e.Code) }
            });
    }
}
