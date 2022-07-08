using Microsoft.AspNetCore.Mvc;

namespace FileServiceApi.Common
{
    public class CustomApiResult
    {
        public int Code { get; set; }
        public string Message { get; set; } = "请求成功";
        public object Data { get; set; }
        public string TraceId { get; set; }


    }
}