using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Json.Net;

namespace AIBasedServises;

public class DocumentRecognizerService
{
    // private string _endpoint;
    // private string _apiKey;
    private DocumentAnalysisClient _client;
    
    public DocumentRecognizerService(string endpoint, string apiKey)
    {
        var credential = new AzureKeyCredential(apiKey);
        _client = new DocumentAnalysisClient(new Uri(endpoint), credential);
    }

    public string ExtractDocumentInformation(Stream fs)
    {
        AnalyzeDocumentOperation operation = _client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-layout", fs);
        AnalyzeResult result = operation.Value;
        
        foreach (DocumentPage page in result.Pages)
        {
            Console.WriteLine($"Page number :{page.PageNumber}");
            Console.WriteLine($"Page size : Height = {page.Height}, Width = {page.Width} ");
            Console.WriteLine($"Page lines number : {page.Lines.Count()}");
            
            for (int i = 0; i < page.Lines.Count; i++)
            {
                DocumentLine line = page.Lines[i];
                Console.WriteLine($"  Line {i} has content: '{line.Content}'.");
            
                Console.WriteLine($"    Its bounding polygon (points ordered clockwise):");
                
                Console.WriteLine($"    Bounding Polygon description: number of vertex = {line.BoundingPolygon.Count}");
                
                for (int j = 0; j < line.BoundingPolygon.Count; j++)
                {
                    Console.WriteLine($"      Point {j} => X: {line.BoundingPolygon[j].X}, Y: {line.BoundingPolygon[j].Y}");
                }
            }
        }

        return JsonNet.Serialize(result);
    }

    public async Task ExtractDocumentInformationFromURI(string uri)
    {
        Uri fileUri = new Uri(uri);

        AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-layout", fileUri);
        AnalyzeResult result = operation.Value;
        
        foreach (DocumentPage page in result.Pages)
        {
            Console.WriteLine("Test " + result.Pages.Count);
        }
        
        
    }
}