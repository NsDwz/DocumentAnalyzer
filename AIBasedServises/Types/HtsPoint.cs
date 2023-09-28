using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsPoint
{
    [JsonProperty("IsEmpty")]
    public bool IsEmpty { get; set; }
    
    [JsonProperty("X")]
    public double X { get; set; }
    
    [JsonProperty("Y")]
    public double Y { get; set; }
}