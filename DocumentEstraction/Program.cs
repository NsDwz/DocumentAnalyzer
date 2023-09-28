// See https://aka.ms/new-console-template for more information

using System.Collections;
using AIBasedServises;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.AI.FormRecognizer.Models;
using DocumentEstraction.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

const int LIMIT = 50;
var analyzer = new DocumentRecognizerService("https://firststep.cognitiveservices.azure.com/",
    "0b6f1a4ed5a943a1a31f3b2a10c85031");
string outputDirName = Path.Join("AI","OCR_processed");
string inputDirName = "CO1000001_01_A4INPUT";
string ocrJsonPath = "StaticFolder";

try
{
    var txtFiles = Directory.EnumerateFiles(inputDirName);
    
    int count = 0;

    try
    {
        foreach (string currentFile in txtFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(currentFile);

            AnalyzeResult result;
            
            using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read))
            {
                result = analyzer.ExtractDocumentInformationUsingModel(fileStream, "prebuilt-layout");

                //JSON file
                using (StreamWriter outputFile =
                       new StreamWriter(Path.Join(ocrJsonPath, Path.GetFileNameWithoutExtension(currentFile) + ".json"),
                           false))
                {
                    var jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);
                    outputFile.Write(jsonString);
                }
            }
        }
    }
    catch (Exception e)
    {
        // remove all processed images
        var generatedFiles = Directory.EnumerateFiles(ocrJsonPath);
        
        foreach (string currentFile in generatedFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(currentFile);
            
            File.Delete(Path.Join(inputDirName, $"{fileName}.jpg"));
        }
    }

    
    // remove ocr file already processed 
    var outputFiles = Directory.EnumerateFiles(outputDirName);
    foreach (string currentFile in outputFiles)
    {
        var fileName = Path.GetFileNameWithoutExtension(currentFile).Replace(".ocr","");
        
        File.Delete(Path.Join(ocrJsonPath, $"{fileName}.json"));
    }
    
    
    var preperedOcrFiles = Directory.EnumerateFiles(ocrJsonPath);
    foreach (string currentFile in preperedOcrFiles)
    {

        using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fileStream))
            {
                // result = JsonConvert.DeserializeObject<Object>(sr.ReadToEnd());
                HtsResult deserializedObject = JsonConvert.DeserializeObject<HtsResult>(sr.ReadToEnd());
                using (StreamWriter outputFile =
                       new StreamWriter(Path.Join(outputDirName, Path.GetFileNameWithoutExtension(currentFile) + ".ocr.json"),
                           false))
                {
                    var jsonString = JsonConvert.SerializeObject(deserializedObject, Formatting.Indented);
                    outputFile.Write(jsonString);
                }
                
                using (StreamWriter outputFile = new StreamWriter(Path.Join(outputDirName, Path.GetFileNameWithoutExtension(currentFile) + ".txt"),false))
                {
                    string content = deserializedObject.Content;
                    outputFile.Write(content);
                }


                using (StreamWriter outputFile = new StreamWriter(Path.Join(outputDirName,Path.GetFileNameWithoutExtension(currentFile) + ".ocrProcessed.json"), false))
                {
                    string jsonString = JsonConvert.SerializeObject(analyzer.GetOcrProcessed(deserializedObject), Formatting.Indented);
                    outputFile.Write(jsonString);
                }
            }
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}