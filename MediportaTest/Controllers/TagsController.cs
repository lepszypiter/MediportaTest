using MediportaTest.Models;
using MediportaTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediportaTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagsRepository _tagsRepository;
    private readonly ILogger _logger;

    public TagsController(ITagsRepository tagsRepository, ILogger<TagsController> logger)
    {
        _tagsRepository = tagsRepository;
        _logger = logger;
    }

    // GET: api/Tags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags(
        int pageSize = 100,
        int page = 1,
        TagOrder order = TagOrder.Asc,
        TagSort sort = TagSort.Name)
    {
        _logger.LogInformation("GET: GetAllTags");
        var tags = await _tagsRepository.GetAllTags(pageSize, page, order, sort);
        return Ok(tags);
    }
    // Force refresh of the tags
    // GET: api/Tags/Refresh
    [HttpPost("Refresh")]
    public async Task<ActionResult> RefreshTags()
    {
        _logger.LogInformation("GET: RefreshTags");
        await _tagsRepository.RefreshTags();
        return Ok();
    }
}

public enum TagSort
{
    Name,
    Percent
}

public enum TagOrder
{
    Asc,
    Desc
}
    
