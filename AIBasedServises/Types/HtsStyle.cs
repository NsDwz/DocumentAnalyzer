using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsStyle
{
    [JsonProperty("IsHandwritten")]
    public bool IsHandwritten { get; set; }
    
    [JsonProperty("Spans")]
    public List<HtsSpan> Spans { get; set; }
    
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
}