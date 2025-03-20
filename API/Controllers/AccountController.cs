using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController(DataContext context,ITokenServices token,IMapper _mapper) :BaseApiController
{

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if(await IsExist(registerDTO.Username)) return BadRequest("Username is taken");
        using var hmac=new HMACSHA512();
        var user= _mapper.Map<AppUser>(registerDTO);
        user.UserName = registerDTO.Username;
        user.PasswordHash =hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password));
        user.PasswordSalt=hmac.Key;

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDTO(){
            UserName = user.UserName,
            Token=token.CreateToken(user),
            Knownas=user.KnownAs
        };
    }
    [HttpPost("Login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO){
        var user= await context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.UserName==loginDTO.UserName.ToLower());
        if(user==null) return Unauthorized("invalid Username");
        using var hmac=new HMACSHA512(user.PasswordSalt);
        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
        for( int i=0 ; i<ComputeHash.Length ; i++ ){
            if(ComputeHash[i]!=user.PasswordHash[i]) 
                return Unauthorized("Invalid Password");
        }
         return new UserDTO(){
            UserName = user.UserName,
             Knownas=user.KnownAs,
            Token=token.CreateToken(user),
            PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
        };
    }

    private async Task<bool> IsExist(string username){
        return await context.Users.AnyAsync(x => x.UserName.ToLower()== username.ToLower());
    }
}
