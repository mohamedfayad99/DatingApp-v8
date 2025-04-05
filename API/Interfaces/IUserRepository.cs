using System;
using API.Dtos;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{
    void Updateuser(AppUser user);
    Task<bool>SavechangesAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByNameAsync(string name);
    
    Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
    Task<MemberDTO> GetMemberByIdAsync(int id);
    Task<MemberDTO> GetMemberByNameAsync(string name);
}
