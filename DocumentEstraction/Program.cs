// See https://aka.ms/new-console-template for more information

using AIBasedServises;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Newtonsoft.Json;

const int LIMIT = 50;
var analyzer = new DocumentRecognizerService("https://firststep.cognitiveservices.azure.com/", "0b6f1a4ed5a943a1a31f3b2a10c85031");
string outputDirName = "AI/OCR_processed/";
string inputDirName = "CO1000001_01_A4INPUT";

try
{
    var txtFiles = Directory.EnumerateFiles(inputDirName);

    int count = 0;
    foreach (string currentFile in txtFiles)
    {
        using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read))
        {

            AnalyzeResult result = analyzer.ExtractDocumentInformationUsingModel(fileStream, "prebuilt-layout");

            //JSON file
            using (StreamWriter outputFile = new StreamWriter(outputDirName + Path.GetFileNameWithoutExtension(currentFile) + "ocr.json", false))
            {
                var jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);
                outputFile.Write(jsonString);
            }

            //Plain Txt
            using (StreamWriter outputFile = new StreamWriter(outputDirName + Path.GetFileNameWithoutExtension(currentFile) + ".txt", false))
            {

                string content = result.Content;
                outputFile.Write(content);
            }

            //Processed
            using (StreamWriter outputFile = new StreamWriter(outputDirName + Path.GetFileNameWithoutExtension(currentFile) + ".ocrProcessed.json", false))
            {
                string jsonString = JsonConvert.SerializeObject(analyzer.GetOcrProcessed(result), Formatting.Indented);
                outputFile.Write(jsonString);
            }

        }
        if (++count == LIMIT)
            break;
    }

}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
