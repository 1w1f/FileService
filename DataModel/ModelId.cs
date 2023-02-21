using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class ModelId
{
    [Key]
    [Required]
    public Guid Id { get; set; }
}
