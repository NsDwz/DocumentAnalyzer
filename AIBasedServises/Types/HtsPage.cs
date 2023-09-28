using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsPage
{
    [JsonProperty("Unit")]
    public int Unit { get; set; }
    
    [JsonProperty("PageNumber")]
    public int PageNumber { get; set; }
    
    [JsonProperty("Angle")]
    public double Angle { get; set; }
    
    [JsonProperty("Width")]
    public double Width { get; set; }
    
    [JsonProperty("Height")]
    public double Height { get; set; }
    
    [JsonProperty("Spans")]
    public List<HtsSpan> Spans { get; set; }
    
    [JsonProperty("SelectionMarks")]
    public List<HtsSelectionMarks> SelectionMarks { get; set; }
    
    [JsonProperty("Lines")]
    public List<HtsLine> Lines { get; set; }
    
    [JsonProperty("Words")]
    public List<HtsWord> Words { get; set; }
}