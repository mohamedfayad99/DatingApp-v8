using System;
using API.Dtos;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Updateuser(AppUser user);
    Task<bool>SavechangesAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByNameAsync(string name);
    
    Task<IEnumerable<MemberDTO>> GetMembersAsync();
    Task<MemberDTO> GetMemberByIdAsync(int id);
    Task<MemberDTO> GetMemberByNameAsync(string name);
}
