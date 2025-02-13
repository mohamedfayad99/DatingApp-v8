using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDTO
{
    [Required]
    [MaxLength(100)]
    public  string username { get; set; }=string.Empty;
    [Required]
    [StringLength(8,MinimumLength =4)]
    public  string password { get; set; }=string.Empty ;
}
