namespace DataModel.User.Vo;

public class UserVoWithToken : UserVo
{
    public string Token { get; set; }
    /// <summary>
    /// 有效期
    /// </summary>
    /// <value>string</value>
    public string ExpirationTime { get; set; }


    public void UpdateTokenAndExpiraction(string token, string expirationTime)
    {
        ExpirationTime = expirationTime;
        Token = token;
    }
}
