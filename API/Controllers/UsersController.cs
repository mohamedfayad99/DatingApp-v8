using System.Security.Claims;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository _repository,IMapper _mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetAllUsers(){
        var users=await _repository.GetMembersAsync();
        return Ok(users);
    }
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUser(string username){
        var user=await _repository.GetMemberByNameAsync(username);
        if(user==null) return NotFound() ;
        return user;
    }
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdatesDTO memberUpdatesDTO){
        var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(username==null) return BadRequest("No User found  in token");
        var user=await _repository.GetUserByNameAsync(username);
        if(user==null)return BadRequest("could not found user");
        _mapper.Map(memberUpdatesDTO, user);
        if(await _repository.SavechangesAsync()) return NoContent();
        return BadRequest("faild to update user");
    }
}
