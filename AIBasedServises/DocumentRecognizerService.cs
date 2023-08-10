using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

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

    public void ExtractDocumentInformation(Stream fs)
    {
        AnalyzeDocumentOperation operation = _client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-layout", fs);
        AnalyzeResult result = operation.Value;
        
        foreach (DocumentPage page in result.Pages)
        {
            Console.WriteLine("Test");
        }
    }

    
}