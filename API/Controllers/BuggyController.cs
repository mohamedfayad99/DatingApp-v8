using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    public class BuggyController(DataContext context) : BaseApiController
    {
        [HttpGet("auth")]
        public ActionResult<string> GetAuth(){
            return Unauthorized( "secret text");
        }
        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError(){
            var thing=context.Users.Find(-1) ??throw new Exception("A Bad thing has happend");
            return thing;
        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(){
            var thing = context.Users.Find(-1);
            if(thing==null){
                return NotFound();
            }
            return thing;
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(){
            return BadRequest("this is not good request");
        }

    
}
