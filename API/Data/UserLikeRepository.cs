using System;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserLikeRepository : IUserLikes
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserLikeRepository(DataContext context ,IMapper mapper){
        _context = context;
        _mapper = mapper;
    }
    public void AddLike(UserLike userLike)
    {
        _context.LikedUsers.Add(userLike);
    }

    public void Deletelike(UserLike userLike)
    {
         _context.LikedUsers.Remove(userLike);
    }

    public async Task<IEnumerable<int>> GetCurrentUserId(int currentuserid)
    {
        return await _context.LikedUsers
            .Where(x=>x.sourceuserId==currentuserid)
            .Select(x=>x.targetuserId)
            .ToListAsync(); 
    }

    public async Task<UserLike?> GetUserLikeAsync(int sourceuserId ,int targetuserId)
    {
        return await _context.LikedUsers.FindAsync(sourceuserId,targetuserId);
    }

    public async Task<IEnumerable<MemberDTO>> GetUserLikesAsync(string predicate,int userid)
    {
        var likes=  _context.LikedUsers.AsQueryable();
        var testLikes = await _context.LikedUsers
    .Where(x => x.sourceuserId == userid || x.targetuserId == userid)
    .ToListAsync();

        switch (predicate)
        {
            case "liked":
                return await likes
                    .Where(x => x.sourceuserId == userid)
                    .Select(x => x.TargetUser)
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            case "likedBy":
                return await likes
                    .Where(x => x.targetuserId == userid)
                    .Select(x => x.SourceUser)
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            default:
                var likedUsers = await likes
              .Where(x => x.sourceuserId == userid)
              .Select(x => x.TargetUser)
              .ToListAsync();

                return await likes
                    .Where(x => likedUsers.Contains(x.SourceUser) && x.targetuserId == userid)
                    .Select(x => x.SourceUser)
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();                     
        }

    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync()>0;
    }
    
}
