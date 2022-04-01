using Microsoft.AspNetCore.Mvc;

namespace FileServiceApi.Common
{
    public class CustomApiResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}