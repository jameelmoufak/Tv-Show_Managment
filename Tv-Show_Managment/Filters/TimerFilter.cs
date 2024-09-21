using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Tv_Show_Managment.Filters
{
    public class TimerFilter : IAsyncActionFilter
    {
        private readonly ILogger<TimerFilter> logger;

        public TimerFilter(ILogger<TimerFilter> logger)
        {
            this.logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next();
            stopWatch.Stop();
            logger.LogInformation($"Running for {stopWatch.ElapsedMilliseconds}");
        }
    }
}
