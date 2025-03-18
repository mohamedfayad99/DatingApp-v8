using System.Security.Claims;
namespace API.Extension;

public static class GetUser
{

    public static string GetClaimUser(this ClaimsPrincipal user){
        var username=user.FindFirstValue(ClaimTypes.NameIdentifier);
        if(username==null) throw new Exception("Can not get username from token");
        return username;
    }

}
