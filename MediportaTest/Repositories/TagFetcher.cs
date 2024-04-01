using System.Net;
using System.Text.Json;
using MediportaTest.Context;
using MediportaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MediportaTest.Repositories;

public class TagFetcher
{
    private readonly TagsDBContext _context;
    private readonly HttpClient _client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }); 
    
    public TagFetcher(TagsDBContext context)
    {
        _context = context;
    }

    public async Task Fetch()
    {
        var count = await _context.Tags.CountAsync();
        var page = 1;
        while (count < 1000)
        {
            var tags = await ReadTags(page);
            await SaveTags(tags);
            
            page++;
            count += tags.Items.Length;
        }
    }

    private async Task<SOTags> ReadTags(int page)
    {
        var requestUri =
            $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        Console.WriteLine(await response.Content.ReadAsStringAsync());

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

    public async Task RemoveAll()
    {
        // Remove all tags
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Tags");
        
    }
    
}

