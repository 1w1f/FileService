using System.ComponentModel.DataAnnotations;
namespace DataModel.User;
public class UserModel
{
    
    public int Id { get; set; }
    
    public string Name{get;set;}
    public string PassWord{get;set;}
    public DateTime CreateTime{get;set;}

    public List<LoginRecord> LoginRecords{get;set;}=new List<LoginRecord>();
    
}
