using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsWord
{
    [JsonProperty("Content")]
    public string Content { get; set; }
    
    [JsonProperty("BoundingPolygon")]
    public List<HtsPoint> BoundingPolygon { get; set; }
    
    [JsonProperty("Span")]
    public HtsSpan Span { get; set; }
    
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
}