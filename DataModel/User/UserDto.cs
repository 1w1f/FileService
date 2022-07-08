using System.ComponentModel.DataAnnotations;
namespace DataModel.User;
public class UserDto
{
    public int Id { get; set; }
    public string Name{get;set;}
    public string PassWord{get;set;}
    public DateTime CreateTime{get;set;}

    public List<LoginRecordDto> LoginRecords{get;set;}=new List<LoginRecordDto>();
    
}
