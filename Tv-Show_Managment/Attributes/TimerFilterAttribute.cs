using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Globalization;

namespace Tv_Show_Managment.Attributes
{
    public class TimerFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<TimerFilterAttribute>>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next();
            stopWatch.Stop();
            logger.LogInformation($"Running for {stopWatch.ElapsedMilliseconds}");
        }

    }
}
