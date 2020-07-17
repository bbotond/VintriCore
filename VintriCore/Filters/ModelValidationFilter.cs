using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VintriCore.Filters
{
    public class ModelValidationFilter
    {
    }

    //public class VintriFilter : ActionFilterAttribute
    //{

    //    public override  
    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        if (actionContext.ModelState.IsValid == false)
    //        {
    //            actionContext.Response = actionContext.Request.CreateErrorResponse(
    //                HttpStatusCode.BadRequest, actionContext.ModelState);
    //        }
    //    }
    //}
}
