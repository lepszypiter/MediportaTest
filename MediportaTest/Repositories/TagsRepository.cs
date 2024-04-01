using MediportaTest.Context;
using MediportaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MediportaTest.Repositories;

class TagsRepository : ITagsRepository
{
    private readonly TagsDBContext _context;
    private readonly TagFetcher _tagFetcher;
    private readonly ILogger _logger;
    public TagsRepository(TagsDBContext context, ILogger<TagsRepository> logger, TagFetcher tagFetcher)
    {
        _context = context;
        _logger = logger;
        _tagFetcher = tagFetcher;
    }
    
    public async Task<IReadOnlyCollection<Tag>> GetAllTags()
    {
        await _tagFetcher.Fetch();
        var result = await _context.Tags.Take(15).ToListAsync();
        _logger.LogTrace("GetAllTags {Count}",  result.Count);
        return result;
    }
}