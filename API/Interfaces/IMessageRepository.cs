using API.Dtos;
using API.Entities;

namespace API.Interfaces;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
    Task<IEnumerable<MessageDto>> GetMessagesAsync(string predicate, int userId, int otherUserId);
    Task<bool> DeleteMessageAsync(int messageId, int userId);
}