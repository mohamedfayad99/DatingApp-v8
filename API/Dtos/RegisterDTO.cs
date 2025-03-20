using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDTO
{
    [Required]
    public  string Username { get; set; }=string.Empty;
    [Required]
    public  string? Knownas { get; set; }
    [Required]
    public  string? Gender { get; set; } 
    [Required]
    public  string? Dateofbirth { get; set; } 
    [Required]
    public  string? City { get; set; } 
    [Required]
    public  string? Country { get; set; }
    
    [Required]
    [StringLength(8,MinimumLength =4)]
    public  string password { get; set; }=string.Empty ;
}
