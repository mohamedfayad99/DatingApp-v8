using System.Security.Claims;
using API.Dtos;
using API.Entities;
using API.Extension;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController(IUserLikes userLikes,IUserRepository reposotry) : BaseApiController
    {
        [HttpPost("{targetuserId:int}")]
        public async Task<ActionResult> ToggleLike(int targetuserId)
        {
            var user = await reposotry.GetUserByNameAsync(User.GetClaimUser());

            if (user.Id == targetuserId)
                return BadRequest("You cannot like yourself.");

            var existingLike = await userLikes.GetUserLikeAsync(user.Id, targetuserId);

            if (existingLike == null)
            {
                var like = new UserLike
                {
                    sourceuserId = user.Id,
                    targetuserId = targetuserId,
                };

                userLikes.AddLike(like);
            }
            else
            {
                userLikes.Deletelike(existingLike);
            }

            var success = await userLikes.SaveChanges();

            if (success)
                return Ok(new { message = existingLike == null ? "Liked" : "Unliked" });

            return BadRequest("Failed to update like status.");
        }


        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikeId()
        {
              var user=await reposotry.GetUserByNameAsync(User.GetClaimUser());
            return Ok(await userLikes.GetCurrentUserId(user.Id));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsersLike(string predicate)
        {
             var user=await reposotry.GetUserByNameAsync(User.GetClaimUser());

            var users=await  userLikes.GetUserLikesAsync(predicate,user.Id);
            return Ok(users);
        }

    }
}
