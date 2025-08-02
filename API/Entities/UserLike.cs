using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class UserLike
{
    [Key]
    public int sourceuserId { get; set; }
    public AppUser SourceUser {get; set;}=null!;
  public AppUser TargetUser{get; set;}=null!;    
 public int targetuserId{get; set;}
}
