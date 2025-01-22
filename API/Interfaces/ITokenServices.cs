using System;
using System.Runtime.InteropServices;
using API.Entities;

namespace API.Interfaces;

public interface ITokenServices
{
    string CreateToken(AppUser appUser);
}