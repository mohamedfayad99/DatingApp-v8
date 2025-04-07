using System.Security.Claims;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Extension;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository _repository,IMapper _mapper,IPhotoServices _photoservice) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetAllUsers([FromQuery]UserParams userParams)
    {
        userParams.CurrentUserName = User.GetClaimUser();
        var users=await _repository.GetMembersAsync(userParams);
        Response.AddPaginnationHeader(users);
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
        var user=await _repository.GetUserByNameAsync(User.GetClaimUser());
        if(user==null)return BadRequest("could not found user");
        _mapper.Map(memberUpdatesDTO, user);
        if(await _repository.SavechangesAsync()) return NoContent();
        return BadRequest("faild to update user");
    }
    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file){
        var user=await _repository.GetUserByNameAsync(User.GetClaimUser());
        if(user==null) return BadRequest("can not apload images");
        var result=await _photoservice.AddphotoAsync(file);
        if(result.Error!=null) return BadRequest(result.Error.Message);
        var photo=new Photo{
            Url=result.SecureUrl.AbsoluteUri,
            publicId=result.PublicId
        };
        if(user.Photos.Count==0)photo.IsMain=true;
        user.Photos.Add(photo);
        if(await  _repository.SavechangesAsync())
             return  CreatedAtAction(nameof(GetUser),
             new {username=user.UserName}, _mapper.Map<PhotoDTO>(photo));
        return BadRequest("Problem in add photo");

    } 
    [HttpPut("set-main-photo/{photoid:int}")]
    public async Task<ActionResult> SetMainphoto(int photoid){
        var user=await _repository.GetUserByNameAsync(User.GetClaimUser());
        if(user==null) return BadRequest("could not find user");
        var photo= user.Photos.FirstOrDefault(x => x.Id==photoid);
        if(photo==null || photo.IsMain) return BadRequest("can not use this as main photo");
        var currentmain=user.Photos.FirstOrDefault(x => x.IsMain);
        if(currentmain !=null) currentmain.IsMain=false;
        photo.IsMain=true;
        if(await _repository.SavechangesAsync())return NoContent();
        return BadRequest("problem setting main photo");
    }

    [HttpDelete("delete-photo/{photoid:int}")]
    public async Task<ActionResult> Deletephoto(int photoid){
        var user=await _repository.GetUserByNameAsync(User.GetClaimUser());
        if (user==null) return BadRequest("user not found");
        var photo=user.Photos.FirstOrDefault(x => x.Id==photoid);
        if(photo==null || photo.IsMain) return BadRequest("Can not delete this image");
        if(photo.publicId != null){
            var result=await _photoservice.DeletephototoAsync(photo.publicId);
            if(result.Error !=null)return BadRequest(result.Error.Message);
        }
        user.Photos.Remove(photo);
        if(await _repository.SavechangesAsync()) return Ok();
        return BadRequest("problem at Deleting photo");
    }
}
