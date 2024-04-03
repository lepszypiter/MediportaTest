using System.Net;
using System.Text.Json;
using MediportaTest.Context;
using MediportaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MediportaTest.Repositories;

public class TagFetcher
{
    const int TagsCount = 1000;
    private readonly ILogger _logger;
    private readonly TagsDBContext _context;

    private readonly HttpClient _client = new HttpClient(new HttpClientHandler()
        { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

    public TagFetcher(TagsDBContext context, ILogger<TagFetcher> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Fetch()
    {
        var count = await _context.Tags.CountAsync();
        if (count >= TagsCount)
        {
            return;
        }

        _logger.LogInformation("Fetching tags");
        await RemoveAll();
        var page = 1;
        while (count < TagsCount)
        {
            var tags = await ReadTags(page);
            await SaveTags(tags);

            page++;
            count += tags.Items.Length;
        }

        await CalculatePercent();
    }

    private async Task<SOTags> ReadTags(int page)
    {
        var requestUri =
            $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        _logger.LogDebug("Read tags page {Page}", page);
        var tags = JsonSerializer.Deserialize<SOTags>(await response.Content.ReadAsStringAsync());
        return tags;
    }

    private async Task SaveTags(SOTags? tags)
    {
        foreach (var tagDto in tags.Items)
        {
            var tag = new Tag
            {
                Name = tagDto.Name,
                Count = tagDto.Count
            };

            _context.Tags.Add(tag);
        }

        await _context.SaveChangesAsync();
    }

    private async Task CalculatePercent()
    {
        var sum = await _context.Tags.SumAsync(t => t.Count);
        foreach (var tag in _context.Tags)
        {
            tag.Percent = (double)tag.Count / sum * 100;
        }
        _logger.LogInformation("Calculating percent");
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAll()
    {
        // Remove all tags
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Tags");
        _logger.LogInformation("All tags removed");
    }
}