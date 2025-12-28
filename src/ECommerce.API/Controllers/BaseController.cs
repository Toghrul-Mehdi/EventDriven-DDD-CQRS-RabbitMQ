using ECommerce.SharedKernel.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return result.StatusCode switch
            {
                System.Net.HttpStatusCode.OK => Ok(new
                {
                    success = true,
                    message = result.Message,
                    statusCode = (int)result.StatusCode
                }),
                System.Net.HttpStatusCode.Created => StatusCode(201, new
                {
                    success = true,
                    message = result.Message,
                    statusCode = (int)result.StatusCode
                }),
                System.Net.HttpStatusCode.NoContent => NoContent(),
                _ => Ok(new
                {
                    success = true,
                    message = result.Message,
                    statusCode = (int)result.StatusCode
                })
            };
        }

        if (result.ValidationErrors.Any())
        {
            return BadRequest(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode,
                errors = result.ValidationErrors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                })
            });
        }

        return result.StatusCode switch
        {
            System.Net.HttpStatusCode.NotFound => NotFound(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            }),
            System.Net.HttpStatusCode.Conflict => Conflict(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            }),
            System.Net.HttpStatusCode.Unauthorized => Unauthorized(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            }),
            System.Net.HttpStatusCode.Forbidden => StatusCode(403, new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            }),
            _ => BadRequest(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            })
        };
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.StatusCode switch
            {
                System.Net.HttpStatusCode.OK => Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = result.Data,
                    statusCode = (int)result.StatusCode
                }),
                System.Net.HttpStatusCode.Created => StatusCode(201, new
                {
                    success = true,
                    message = result.Message,
                    data = result.Data,
                    statusCode = (int)result.StatusCode
                }),
                _ => Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = result.Data,
                    statusCode = (int)result.StatusCode
                })
            };
        }

        if (result.ValidationErrors.Any())
        {
            return BadRequest(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode,
                errors = result.ValidationErrors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                })
            });
        }

        return result.StatusCode switch
        {
            System.Net.HttpStatusCode.NotFound => NotFound(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            }),
            System.Net.HttpStatusCode.Conflict => Conflict(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            }),
            _ => BadRequest(new
            {
                success = false,
                message = result.Message,
                statusCode = (int)result.StatusCode
            })
        };
    }
}