using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Api.App_Common
{
    public class Abnormal : ExceptionFilterAttribute
    {   
        /// <summary>
        /// 异常捕捉
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            WriteLog(context);
            var actionResponse = OperationResult.CreateFailResponse(0, "{0} ------------------ {1}", context.Exception.Message, context.Exception.StackTrace);
            context.Response = ApiHelper.CreateBackMessage(actionResponse);
        }

        private void WriteLog(HttpActionExecutedContext context)
        {   

            //可讲信息写入数据库
            //AddErrorLogRequest request = new AddErrorLogRequest();
            //request.ClientIpAddress = ApiHelper.GetClientIpAddress(context.Request);
            //request.CreatedTime = DateTime.Now;
            //request.ExceptionSource = context.Exception.Source;
            //request.ExceptionStackTrace = context.Exception.StackTrace;
            //request.ExceptionType = context.Exception.GetType().ToString();
            //request.Message = context.Exception.Message;
            //request.RequestHeaders = context.Request.Headers.ToString();

            //context.Request.Content.ReadAsStreamAsync().Result.Position = 0;
            //request.RequestMessage = context.Request.Content.ReadAsStringAsync().Result;
            //request.Url = context.Request.RequestUri.ToString();

            AppLog.Error("异常处理");
        }
    }
}