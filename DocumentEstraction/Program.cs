// See https://aka.ms/new-console-template for more information

using AIBasedServises;


var analizer = new DocumentRecognizerService("https://firststep.cognitiveservices.azure.com/", "0b6f1a4ed5a943a1a31f3b2a10c85031");
string path = "documento-unico-de-identidade.jpg";


using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
{
    analizer.ExtractDocumentInformation(fileStream);
}


// analizer.ExtractDocumentInformationFromURI(
//     "https://wcblind.org/wp-content/uploads/2020/04/a-wi-real-id-with-a-star-in-the-upper-right-.jpeg");
