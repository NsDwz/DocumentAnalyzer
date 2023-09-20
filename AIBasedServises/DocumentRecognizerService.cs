using System.Collections.ObjectModel;
using System.Drawing;
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
        AnalyzeResult result = ExtractDocumentInformationUsingModel(fs, "prebuilt-layout");
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }
    
    public string ExtractDocumentKeyValueInformation(Stream fs)
    {
        AnalyzeResult result = ExtractDocumentInformationUsingModel(fs, "prebuilt-document");;
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }
    
    public AnalyzeResult ExtractDocumentInformationUsingModel(Stream fs, string model)
    {
        AnalyzeDocumentOperation operation = _client.AnalyzeDocument(WaitUntil.Completed, model, fs);
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
                
                Console.WriteLine($"    Angle = {getInclinationAngle(line.BoundingPolygon)}");
            }
        }
        return result;
    }

    public List<OcrProcessed> GetOcrProcessed(AnalyzeResult result)
    {
        List<OcrProcessed> list = new List<OcrProcessed>();
        DocumentModel model = new DocumentModel(result);

        foreach(DocumentParagraph p in result.Paragraphs)
        {
            OcrProcessed processed = new(p.Content, getInclinationAngle(p.BoundingRegions[0].BoundingPolygon), model.GetFontSize(p));
            list.Add(processed);
        }
        return list;
    }

    private IReadOnlyList<PointF> getTheLowerLine(IReadOnlyList<PointF> boundingBox)
    {
        PointF lowLeft = boundingBox.First(); 
        PointF lowRight = boundingBox.Last();
        
        List<PointF> list = boundingBox.ToList().OrderByDescending(point => point.Y).ToList().Take(2).ToList();

        list = list.OrderBy(point => point.X).ToList();

        return list;
    }
    private Double getInclinationAngle(IReadOnlyList<PointF> boundingBox)
    {
        IReadOnlyList<PointF> lowLine = getTheLowerLine(boundingBox);

        PointF lowLeftP = lowLine.First();
        PointF lowRightP = lowLine.Last();
        
        Double angle = Math.Atan((lowRightP.Y- lowLeftP.Y) / (lowRightP.X - lowLeftP.X));

        angle = angle / Math.PI * 180;
        
        return angle;
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