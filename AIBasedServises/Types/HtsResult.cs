using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace DocumentEstraction.Types;

public class HtsResult
{
    [JsonProperty("ModelId")]
    public string ModelId { get; set; }
    
    [JsonProperty("Content")]
    public string Content { get; set; }
    
    [JsonProperty("Paragraphs")]
    public List<HtsParagraph> Paragraphs { get; set; }
    
    [JsonProperty("Styles")]
    public List<HtsStyle> Styles { get; set; }
    
    [JsonProperty("Pages")]
    public List<HtsPage> Pages { get; set; } 
}