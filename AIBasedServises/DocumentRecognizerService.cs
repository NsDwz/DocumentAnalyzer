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
        
        
        return JsonNet.Serialize(result);
    }
    
    public string ExtractDocumentKeyValueInformation(Stream fs)
    {
        
        AnalyzeResult result = ExtractDocumentInformationUsingModel(fs, "prebuilt-document");;
        
        
        return JsonNet.Serialize(result);
    }
    
    private AnalyzeResult ExtractDocumentInformationUsingModel(Stream fs, string model)
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

        DocumentModel extractor = new DocumentModel(result);
        
        int count = 0;
        foreach (DocumentParagraph paragraph in result.Paragraphs)
        {
            
            Console.WriteLine($"------------------------------- PARAGRAPH FONT SIZE --------------------------------------");
            Console.WriteLine($"Paragraph {++count} has a font size of {extractor.GetFontSize(paragraph)}");
        }

        return result;
    }


    private IReadOnlyList<PointF> getTheLowerLine(IReadOnlyList<PointF> boundingBox)
    {
        PointF lowLeft = boundingBox.First(); 
        PointF lowRight = boundingBox.Last();

        foreach (var point in boundingBox)
        {
            if ( point.Y < lowLeft.Y && point.X <= lowLeft.X )
            {
                lowLeft = point;
            }

            if (point.Y < lowRight.Y && point.X >= lowRight.X)
            {
                lowRight = point;
            }
        }
        
        return new List<PointF>(){lowLeft, lowRight};
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