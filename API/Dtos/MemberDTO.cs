using System;

namespace API.Dtos;

public class MemberDTO
{
    public int Id { get; set;}
    public  string? Username { get; set;}
    public int Age { get; set;}
    public string? PhotoUrl { get; set;}
    public DateTime Created { get; set;}
    public DateTime LastActive { get; set;}
    public required string Gender { get; set;}
    public required string KnownAs { get; set;}
    public  string? Lookingfor { get; set;}
    public  string? Interests { get; set;}
    public string? Introduction { get; set;}
    public  string? City { get; set;}
    public  string? Country { get; set;}

    public  List<PhotoDTO>? Photos{ get; set;}

}
