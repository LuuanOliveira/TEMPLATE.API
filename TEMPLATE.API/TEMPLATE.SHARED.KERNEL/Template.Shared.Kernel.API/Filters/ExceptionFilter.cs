using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Template.Shared.Kernel.Api.ViewModels;
using Template.Shared.Kernel.Domain.Exceptions;
using System.Collections.Generic;

namespace Template.Shared.Kernel.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException)
            {
                context.Result = new OkObjectResult(new ResponseResult
                {
                    Success = false,
                    Notifications = new List<string> { context.Exception.Message }
                });
            }
            else
            {
                context.Result = new BadRequestObjectResult(new ResponseResult
                {
                    Success = false,
                    Notifications = new List<string> { "Erro: Entre em contato com suporte." }
                });
            }

            context.ExceptionHandled = true;
        }
    }
}
