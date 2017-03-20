using Api.App_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api
{
    public class ValidateAttribute: ActionFilterAttribute
    {   

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var state in actionContext.ModelState.Values)
                {
                    if (state.Errors.Count > 0)
                    {
                        foreach (var error in state.Errors)
                        {
                            if (string.IsNullOrEmpty(error.ErrorMessage))
                            {
                                stringBuilder.Append(error.Exception.Message + "\n");
                            }
                            else
                            {
                                stringBuilder.Append(error.ErrorMessage + "\n");
                            }
                        }
                    }
                }
                var response = OperationResult.CreateOperationResult (1, null, stringBuilder.ToString());
                actionContext.Response = ApiHelper.CreateBackMessage(response);
            }
        }
    }
}