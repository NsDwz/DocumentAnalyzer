using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsParagraph
{
    [JsonProperty("Role")]
    public dynamic Role { get; set; }
    
    [JsonProperty("Content")]
    public string Content { get; set; }
    
    [JsonProperty("BoundingRegions")]
    public List<HtsBoundingRegion> BoundingRegions { get; set; }
    
    [JsonProperty("Spans")]
    public List<HtsSpan> Spans { get; set; }
}