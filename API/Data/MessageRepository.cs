using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public MessageRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task AddMessageAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        _context.SaveChanges();
    }
    public async Task<IEnumerable<MessageDto>> GetMessagesAsync(
        string predicate,
        int userId,
        int otherUserId)
    {
        var messages = _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .AsQueryable();

        switch (predicate)
        {
            case "sent":
                return await messages
                    .Where(m => m.SenderId == userId && !m.SenderDeleted)
                    .OrderByDescending(m => m.DateSent)
                    .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            case "received":
                return await messages
                    .Where(m => m.RecipientId == userId && !m.RecipientDeleted)
                    .OrderByDescending(m => m.DateSent)
                    .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            default:
                return Enumerable.Empty<MessageDto>();
        }
    }

  public async Task<bool> DeleteMessageAsync(int messageId, int userId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null) return false;

        if (message.SenderId == userId) message.SenderDeleted = true;
        if (message.RecipientId == userId) message.RecipientDeleted = true;

        // remove only when both deleted
        if (message.SenderDeleted && message.RecipientDeleted)
            _context.Messages.Remove(message);

        return await _context.SaveChangesAsync() > 0;
    }



}
