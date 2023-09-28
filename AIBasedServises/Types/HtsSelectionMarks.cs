using System.Net;
using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsSelectionMarks
{
    [JsonProperty("BoundingPolygon")]
    public List<HtsPoint> BoundingPolygon { get; set; }
    
    [JsonProperty("State")]
    public int State { get; set; }
    
    [JsonProperty("Span")]
    public HtsSpan Span { get; set; }
    
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
}