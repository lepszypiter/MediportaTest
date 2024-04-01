using System.Text.Json.Serialization;

namespace MediportaTest.Repositories;
public class SOTags
{
    [JsonPropertyName("items")]
    public TagDTO[] Items { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("quota_max")]
    public long QuotaMax { get; set; }

    [JsonPropertyName("quota_remaining")]
    public long QuotaRemaining { get; set; }
}

public class TagDTO
{
    [JsonPropertyName("count")]
    public long Count { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
}
