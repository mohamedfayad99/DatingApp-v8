using System;
using API.Dtos;
using API.Entities;

namespace API.Interfaces;

public interface IUserLikes
{
    Task<UserLike> GetUserLikeAsync(int sourceuserId,int targetuserId);
    Task<IEnumerable<MemberDTO>> GetUserLikesAsync(string predict,int userid);
    Task<IEnumerable<int>> GetCurrentUserId(int  currentuserid);
    void Deletelike(UserLike userLike);
    void AddLike(UserLike userLike);
    Task<bool>SaveChanges();




}
