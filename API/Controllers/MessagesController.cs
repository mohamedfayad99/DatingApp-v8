using API.Dtos;
using API.Entities;
using API.Extension;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessagesController(IMessageRepository messageRepo, IUserRepository userRepo)
    : BaseApiController
{
    // POST: /messages/{recipientId}
    [HttpPost]
    public async Task<ActionResult<MessageDto>> SendMessage(CreateMessageDto dto)
    {
        var sender = await userRepo.GetUserByNameAsync(User.GetClaimUser());

        if (sender.UserName == dto.RecipientUsername)
            return BadRequest("You cannot send a message to yourself.");

        var recipient = await userRepo.GetUserByNameAsync(dto.RecipientUsername);
        if (recipient == null) return NotFound("Recipient not found.");

        var message = new Message
        {
            SenderId = sender.Id,
            RecipientId = recipient.Id,
            Content = dto.Content
        };

        await messageRepo.AddMessageAsync(message);

        return Ok(new { message = "Message sent" });
    }


    // GET:
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages(string predicate)

    {
        var user = await userRepo.GetUserByNameAsync(User.GetClaimUser());

        var messages = await messageRepo.GetMessagesAsync(predicate, user.Id, 0);
        return Ok(messages);
    }

    // DELETE: /messages/{messageId}
    [HttpDelete("{messageId:int}")]
    public async Task<ActionResult> DeleteMessage(int messageId)
    {
        var user = await userRepo.GetUserByNameAsync(User.GetClaimUser());

        var success = await messageRepo.DeleteMessageAsync(messageId, user.Id);

        if (!success) return BadRequest("Failed to delete message.");

        return  NoContent();
    }
}
