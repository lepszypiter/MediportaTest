using MediportaTest.Models;
using MediportaTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediportaTest.Controllers;

[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagsRepository _clientRepository;
    private readonly ILogger _logger;

    public TagsController(ITagsRepository clientRepository, ILogger<TagsController> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;

    }

    // GET: api/Client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        _logger.LogInformation("GET: GetAllTags");
        var clients = await _clientRepository.GetAllTags();
        return Ok(clients);
    }
}