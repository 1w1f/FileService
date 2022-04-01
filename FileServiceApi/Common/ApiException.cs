namespace FileServiceApi.Common
{
    public class ApiException:Exception
    {
        public int Code { get; set; }
        public string Message { get; set; }     
        public ApiException(int code,string message)
        {
            Code=code;
            Message=message;
        }
    }
}