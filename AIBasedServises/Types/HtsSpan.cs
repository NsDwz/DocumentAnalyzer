using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsSpan
{
    [JsonProperty("Index")]
    public int Index { get; set; }
    [JsonProperty("Length")]
    public int Length { get; set; }
}