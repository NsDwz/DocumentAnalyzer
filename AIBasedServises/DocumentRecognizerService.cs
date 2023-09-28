using System.Text.RegularExpressions;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using DocumentEstraction.Types;
using Newtonsoft.Json;

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
        return JsonConvert.SerializeObject(result);
    }

    public string ExtractDocumentKeyValueInformation(Stream fs)
    {
        AnalyzeResult result = ExtractDocumentInformationUsingModel(fs, "prebuilt-document");
        ;
        return JsonConvert.SerializeObject(result);
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
                    Console.WriteLine(
                        $"      Point {j} => X: {line.BoundingPolygon[j].X}, Y: {line.BoundingPolygon[j].Y}");
                }

                // Console.WriteLine($"    Angle = {getInclinationAngle(line.BoundingPolygon)}");
            }
        }

        return result;
    }
    
    public List<OcrProcessed> GetOcrProcessed(HtsResult result)
    {
        List<OcrProcessed> list = new List<OcrProcessed>();
        DocumentModel model = new DocumentModel(result);

        List<HtsSpan> handWritten = new List<HtsSpan>();

        foreach (var style in result.Styles)
        {
            if (style.IsHandwritten)
            {
                handWritten = handWritten.Concat(style.Spans).ToList();
            }
        }

        foreach (HtsParagraph p in result.Paragraphs)
        {
            bool doesContainHadWritting = false;
            string hasWritting = "";
            
            foreach (var paragraphSpan in p.Spans)
            {
                bool isSpanHadWritten = handWritten.Exists(x => x.Index == paragraphSpan.Index);
                doesContainHadWritting = doesContainHadWritting || isSpanHadWritten;

                if (isSpanHadWritten)
                {
                    hasWritting += "|" + p.Content;
                    doesContainHadWritting = true;
                }
            }

            hasWritting = Regex.Replace(hasWritting, "^\\|", "");

            var fontSize = model.GetFontSize(p);

            OcrProcessed processed = new(
                p.Content,
                getInclinationAngle(p.BoundingRegions[0].BoundingPolygon),
                fontSize,
                doesContainHadWritting,
                hasWritting,
                fontType: FontClassifierService.ClassifyFontSize(fontSize)
            );
            list.Add(processed);
        }

        return list;
    }
    
    private Double getInclinationAngle(IReadOnlyList<HtsPoint> boundingBox)
    {
        IReadOnlyList<HtsPoint> lowLine = boundingBox.Take(2).ToList();

        HtsPoint lowLeftP = lowLine.First();
        HtsPoint lowRightP = lowLine.Last();

        Double angle;
        if ((lowRightP.X - lowLeftP.X) != 0)
        {
            angle = Math.Atan((lowRightP.Y - lowLeftP.Y) / (lowRightP.X - lowLeftP.X));
        }
        else
        {
            angle = Math.PI / 2;
        }


        angle = angle / Math.PI * 180;

        return angle;
    }

    public async Task ExtractDocumentInformationFromURI(string uri)
    {
        Uri fileUri = new Uri(uri);

        AnalyzeDocumentOperation operation =
            await _client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-layout", fileUri);
        AnalyzeResult result = operation.Value;

        foreach (DocumentPage page in result.Pages)
        {
            Console.WriteLine("Test " + result.Pages.Count);
        }
    }
}