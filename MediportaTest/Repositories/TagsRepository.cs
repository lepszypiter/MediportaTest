using MediportaTest.Context;
using MediportaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MediportaTest.Repositories;

class TagsRepository : ITagsRepository
{
    private readonly TagsDBContext _context;
    private readonly ILogger _logger;
    public TagsRepository(TagsDBContext context, ILogger<TagsRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IReadOnlyCollection<Tag>> GetAllTags()
    {
        var result = await _context.Tags.Take(15).ToListAsync();
        _logger.LogTrace("GetAllTags {Count}",  result.Count);
        return result;
    }
    
}