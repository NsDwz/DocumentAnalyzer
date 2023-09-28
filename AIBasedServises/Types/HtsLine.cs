using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsLine
{
    [JsonProperty("BoundingPolygon")]
    public List<HtsPoint> BoundingPolygon { get; set; }
    
    [JsonProperty("Content")]
    public string Content { get; set; }
    
    [JsonProperty("Spans")]
    public List<HtsSpan> Spans { get; set; }
}