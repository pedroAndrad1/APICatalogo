using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("################################################################################");
            _logger.LogInformation("OnActionExecuting");
            _logger.LogInformation($"Executando: {context.Controller} as {DateTime.UtcNow}");
            _logger.LogInformation($"Model State: {context.ModelState.IsValid}");
            _logger.LogInformation("################################################################################");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("################################################################################");
            _logger.LogInformation("OnActionExecuted");
            _logger.LogInformation($"Resposta de: {context.Controller} as {DateTime.UtcNow}");
            _logger.LogInformation($"Status code: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("################################################################################");
        }
    }
}
