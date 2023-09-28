using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsBoundingRegion : Object
{
    [JsonProperty("PageNumberX")]
    public double PageNumber { get; set; }
    
    [JsonProperty("BoundingPolygon")]
    public List<HtsPoint> BoundingPolygon { get; set; }
}