using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Data;

namespace Microsoft.AspNetCore.Mvc;

[AttributeUsage(AttributeTargets.All)]
public class UnitOfWorkAttribute : Attribute, IActionFilter
{
    protected IUnitOfWork? UnitOfWork { get; private set; }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        UnitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (UnitOfWork != null && context.Exception == null)
        {
            UnitOfWork.Complete();
        }
    }
}