using Blitz.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blitz.API.Configuration
{
    public class TransactionalActionFilter : IAsyncActionFilter
    {
        private ILogger<TransactionalActionFilter> _logger;
        public readonly BlitzContext _blitzContext;

        public TransactionalActionFilter(BlitzContext blitzContext, ILogger<TransactionalActionFilter> logger)
        {
            _blitzContext = blitzContext;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var transaction = _blitzContext.Database.BeginTransaction())
            {
                var result = await next();
                if ((result.Exception == null || result.ExceptionHandled) &&
                    IsSuccessStatusCode(context.HttpContext.Response))
                {
                    transaction.Commit();
                    return;
                }

                transaction.Rollback();
                _logger.LogError(transaction.TransactionId + "Has been rollbacked");
            }
        }
        public bool IsSuccessStatusCode(HttpResponse httpResponse)
        {
            return (httpResponse.StatusCode >= 200) && (httpResponse.StatusCode <= 299);
        }
    }
}