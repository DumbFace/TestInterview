using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Teachers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.CustomAttribute
{
    public class CheckCountTeacherAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("lstViewModel"))
            {
                var lstForm = context.ActionArguments["lstViewModel"] as List<TeacherViewModel>;
                if (lstForm.Count < 2)
                {
                    context.Result = new BadRequestObjectResult("Cần nhập ít nhất là 232 Teacher");
                }
            }
        }
    }
}