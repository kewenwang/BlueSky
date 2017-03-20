using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.App_Common
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public static int SuccessCode = 0;

        /// <summary>
        /// 1=失败，0=成功，其它自定义
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 操作失败返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作成功返回信息
        /// </summary>
        public object Data { get; set; }

        public bool Success
        {
            get
            {
                return this.ResultCode == SuccessCode;
            }
        }

        public static OperationResult CreateSuccessResponse(object tag)
        {
            return CreateOperationResult(SuccessCode, tag, "Success");
        }

        public static OperationResult CreateSuccessResponse(object tag, string message)
        {
            return CreateOperationResult(SuccessCode, tag, message);
        }

        public static OperationResult CreateFailResponse(int resultCode, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            return CreateOperationResult(resultCode, null, msg);
        }

        public static OperationResult CreateOperationResult(int resultCode, object data, string message)
        {
            OperationResult response = new OperationResult() { ResultCode = resultCode, Data = data, Message = message };
            return response;
        }
    }
}