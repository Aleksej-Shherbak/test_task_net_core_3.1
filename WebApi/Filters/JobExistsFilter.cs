using System;
using System.Threading.Tasks;
using EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repos.Abstract;
using WebApi.Helpers;

namespace WebApi.Filters
{
    public class JobExistsFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var jobRepository = context.HttpContext.RequestServices.GetService<IJobRepository>();
            var id = context.HttpContext.GetRouteValue("id");

            var anyJobs = await jobRepository.All.AnyAsync(x => x.JobId == Convert.ToInt32(id));

            if (anyJobs)
            {
                await next();
            }
            else
            {
                context.Result = new NotFoundObjectResult(Http.GenerateErrorAnswer($"Job with id {id} not found"));
            }
        }
    }
}