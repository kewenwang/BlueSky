using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Net;

namespace Api.App_Common
{
    public class ApiHelper
    {

        public static HttpResponseMessage CreateBackMessage(object obj = null)
        {
            OperationResult response = CreateOperationResult(obj);

            JsonSerializerSettings set = new JsonSerializerSettings();
            set.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            set.DefaultValueHandling = DefaultValueHandling.Ignore;
            string jsonString = JsonConvert.SerializeObject(response, Formatting.Indented, set);

            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
            msg.Content = new StringContent(jsonString);
            return msg;
        }

        private static OperationResult CreateOperationResult(object obj = null)
        {
            OperationResult response = obj as OperationResult;
            if (response != null)
            {
                return response;
            }
            return OperationResult.CreateSuccessResponse(obj);
        }


    }
}