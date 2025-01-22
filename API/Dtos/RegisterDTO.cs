using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDTO
{
    [Required]
    [MaxLength(100)]
    public required string username { get; set; }
    [Required]
    public required string password { get; set; }
}
