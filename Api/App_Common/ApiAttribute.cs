using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api.App_Common
{
    public class ApiAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// Token
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {   
            string token = GetRequestHeaderValue(actionContext.Request, "Accept");
            if (token != "WangKeWenByToken")
            {
                var response = OperationResult.CreateFailResponse(0, "无效Token");
                actionContext.Response = ApiHelper.CreateBackMessage(response);
            }
        }

        public static string GetRequestHeaderValue(HttpRequestMessage request, string headerKey)
        {
            IEnumerable<string> values;
            if (request.Headers.TryGetValues(headerKey, out values))
            {
                return values.First();
            }
            return null;
        }
    }
}