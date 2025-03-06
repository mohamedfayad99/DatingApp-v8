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
public class UsersController(IUserRepository _repository) : BaseApiController
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
}
