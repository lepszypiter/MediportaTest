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
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        _logger.LogInformation("GET: GetAllTags");
        var tags = await _tagsRepository.GetAllTags();
        return Ok(tags);
    }
}