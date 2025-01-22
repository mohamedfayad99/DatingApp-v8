using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
namespace API.Services;
public class TokenServices(IConfiguration config) : ITokenServices
{
        public string CreateToken(AppUser appUser)
        {
            var Tokenkey=config["TokenKey"]?? throw new Exception("Can not acces tokenkey from apsseting ");
            if(Tokenkey.Length<64) throw new Exception("your token key needs to be longer");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Tokenkey));

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.UserName)
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //to take sdescription
            var tokendescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendescription);

            return tokenhandler.WriteToken(token);
        }


}
