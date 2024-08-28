﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace APICatalogo.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "############################################## Error ##############################################");
            context.Result = new ObjectResult("Ocorreu um erro ao tratar sua solicitação.")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
