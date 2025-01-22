using System;

namespace API.Dtos;

public class LoginDTO
{
    public required string UserName { get; set;}
    public required string Password { get; set;}

}
