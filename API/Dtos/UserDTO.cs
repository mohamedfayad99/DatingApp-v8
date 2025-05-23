using System;

namespace API.Dtos;

public class UserDTO
{
    public required string UserName { get; set;}
    public required string Token { get; set;}
    public string? Knownas { get; set;}
    public string? PhotoUrl { get; set;}
    public string? Gender { get; set;}
}
