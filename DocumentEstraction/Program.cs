// See https://aka.ms/new-console-template for more information

using AIBasedServises;

Console.WriteLine("Hello, World!");
var analizer = new DocumentRecognizerService("https://firststep.cognitiveservices.azure.com/", "0b6f1a4ed5a943a1a31f3b2a10c85031");
string path = @"./documento-unico-de-identidade.jpg";

if (File.Exists(path))
{
    File.Delete(path);
}

using (FileStream fs = File.Create(path))
{
    
    analizer.ExtractDocumentInformation(fs);
}