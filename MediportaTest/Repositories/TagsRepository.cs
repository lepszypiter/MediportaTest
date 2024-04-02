using MediportaTest.Context;
using MediportaTest.Controllers;
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
    
    public async Task<IReadOnlyCollection<Tag>> GetAllTags(int pageSize, int page, TagOrder order, TagSort sort)
    {
        int skip = pageSize * (page - 1);
        await _tagFetcher.Fetch();
        IQueryable<Tag> query = _context.Tags;
        if (sort == TagSort.Percent)
        {
            query = order == TagOrder.Asc ? query.OrderBy(x => x.Percent) : query.OrderByDescending(x => x.Percent);
        }
        else
        {
            query = order == TagOrder.Desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
        }
        
        
        var result = await query.Skip(skip).Take(pageSize).ToListAsync();
        _logger.LogTrace("GetAllTags {Count}",  result.Count);
        return result;
    }

    public async Task RefreshTags()
    {
        await _tagFetcher.RemoveAll();
        await _tagFetcher.Fetch();
    }
}