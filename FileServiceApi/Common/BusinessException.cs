using FileService.ApiCode;

namespace FileServiceApi.Common
{
    public class BusinessException : Exception
    {
        public int Code { get; set; }
        public new string Message { get; set; }
        public BusinessException(int code, string message)
        {
            Code = code;
            Message = message;
        }


      
    }
}