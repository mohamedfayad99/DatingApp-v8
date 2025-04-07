using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Userrepository(DataContext _context,IMapper _mapper) : IUserRepository
{
    public async Task<MemberDTO> GetMemberByIdAsync(int id)
    {
        return await _context.Users.Where( m => m.Id == id).ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();    
    }

    public async Task<MemberDTO> GetMemberByNameAsync(string name)
    {
        return await _context.Users.Where( m => m.UserName == name).ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();    
    }

    public  async Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams)
    {
        var users= _context.Users.AsQueryable();
        users=users.Where(m => m.UserName != userParams.CurrentUserName);
        if(userParams.Gender != null){ //for filtering
            users=users.Where(x=>x.Gender == userParams.Gender);
        }
        users=userParams.OrderBy switch{
            "created" =>users.OrderByDescending(x=>x.Created),
            _ => users.OrderByDescending(x=>x.LastActive)
        };
        return await PagedList<MemberDTO>.CreateAsync(users.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider),userParams.PageNumber,userParams.PageSize);
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        var user=await _context.Users.FindAsync(id);
        return user;
    }

    public async Task<AppUser> GetUserByNameAsync(string username)
    {
        var user=await _context.Users.Include(u=>u.Photos).SingleOrDefaultAsync(u=>u.UserName==username);
        return user;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users.Include(u=>u.Photos).ToListAsync();
    }

    public async Task<bool> SavechangesAsync()
    {
        return await _context.SaveChangesAsync()>0;
    }

    public void Updateuser(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    Task<AppUser> IUserRepository.GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
