using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class ModelId
{
    [Key]
    [Required]
    public int Id { get; set; }
}
